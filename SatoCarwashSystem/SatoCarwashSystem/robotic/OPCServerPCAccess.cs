using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.robotic
{
    public class OPCServerPCAccess
    {
        private OpcServer TheSrv;
        private OpcGroup TheGrp;

        private List<OPCItemDef> ItemDefs = new List<OPCItemDef>();

        private List<String> items = new List<string>();
        private List<int> itemsId = new List<int>();
        private List<int> itemIntParam = new List<int>();
        private List<String> itemVals = new List<string>();

        private List<int> HandlesSrv = new List<int>();

        OPCItemResult[] rItm;

        const string ServerProgID = "S7200.OPCServer";


        const string GROUPNAME = "Group1";

        private bool isGroupActive = false;

        private int dataChangeCounter = 0;

        private int foamCounter = 0;
        private int totalCounter = 0;


        private String winDir = System.Environment.GetEnvironmentVariable("windir");
        private NotifyIcon notifyIcon1;



        DataConnect dc = new DataConnect();

        private void init()
        {
            SqlResult result = dc.executeQuery("select * from TOPCClient " +
                "join TOPCTrItemStatus on TOPCClient.opcClientId=TOPCTrItemStatus.opcClientId " +
                "join TOPCItem on TOPCItem.opcItemId=TOPCTrItemStatus.opcItemId where locationId=" + Session.locations[0].locationId);
            while (result.next())
            {
                items.Add(result.getString("opcItem"));
                itemVals.Add(result.getString("opcItemStatus"));
                itemIntParam.Add(result.getInt("opcItemInt"));
                itemsId.Add(result.getInt("opcItemId"));
            }
        }

        public void connect()
        {
            init();
            dc.connect();
            cmdConnect();
            isGroupActive = true;
            cmdAddGroup("Group1");
            cmdAddItem();
        }
        private void cmdConnect()
        {
            try
            {
                // deactivated for debugging 

                TheSrv = new OpcServer();
                TheSrv.Connect(ServerProgID);

                // Set Button Enable 
                //cmdConnect.Enabled = false;
                //cmdDisconnect.Enabled = true;

                //cmdAddGroup.Enabled = true;
            }
            catch (Exception ex)
            {
                // Show friendly error message. 
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        public void disconnect()
        {
            dc.disconnect();

            cmdExit();

            cmdRemoveItem();

            cmdRemoveGroup();

            cmdDisconnect();
        }
        private void cmdDisconnect()
        {
            // Set Button Enable 
            //cmdDisconnect.Enabled = false;
            //cmdAddGroup.Enabled = false;
            //cmdConnect.Enabled = true;

            

            if ((TheSrv != null))
            {

                int[] aE = new int[3];

                try
                {
                    TheSrv.Disconnect();
                }
                catch (Exception ex)
                {
                    // Show friendly error message. 
                    MessageBox.Show(ex.ToString(), "Error");
                }

                TheSrv = null;

            }
        }
        private void cmdAddGroup(String groupName)
        {
            TheGrp = TheSrv.AddGroup(groupName, false, 900);

            

            if (!TheGrp.Active)
            {
                TheGrp.SetEnable(true);
                TheGrp.Active = true;
            }

            TheGrp.DataChanged += theGrp_DataChange;
            TheGrp.ReadCompleted += theGrp_ReadComplete;
            TheGrp.WriteCompleted += theGrp_WriteComplete;
        }
        private void cmdRemoveGroup()
        {
            if ((TheGrp != null))
            {
                // 

                int[] aE = new int[3];

                try
                {
                    TheGrp.DataChanged -= theGrp_DataChange;
                    TheGrp.ReadCompleted -= theGrp_ReadComplete;
                    TheGrp.WriteCompleted -= theGrp_WriteComplete;

                    TheGrp.Remove(false);
                }
                catch (Exception ex)
                {
                    // Show friendly error message. 
                    MessageBox.Show(ex.ToString(), "Error");
                }

                TheGrp = null;
            }
        }
        private void cmdAddItem()
        {
            // add two items and save server handles 

            for (int i = 0; i < items.Count; i++)
            {
                ItemDefs.Add(new OPCItemDef("2:161.218.182.132:1000:1000," + items[i] + ",BYTE,RW", true, itemIntParam[i], VarEnum.VT_EMPTY));
            }

            TheGrp.AddItems(ItemDefs.ToArray(), out rItm);

            if (rItm == null) return;

            foreach (OPCItemResult item in rItm)
            {
                if (item.Error > 0)
                {
                    // Show friendly error message. 
                    MessageBox.Show("AddItems - some failed", "Error");
                    return;
                }
                HandlesSrv.Add(item.HandleServer);
            }
        }
        private void cmdRemoveItem()
        {
            int[] aE = new int[itemIntParam.Count];

            if ((TheGrp != null))
            {
                TheGrp.RemoveItems(HandlesSrv.ToArray(), out aE);
            }
        }
        public void cmdWrite(String item,String itemVal)
        {
            //MessageBox.Show(item+"-"+itemVal);
            int idx = items.IndexOf(item);
            if (idx != -1)
            {
                itemVals[idx] = itemVal;
                cmdWriteSync();
                itemVals[idx] = "0";
            }
        }
        private void cmdWriteSync()
        {
            // Sync Write 
            int[] arrErr = new int[items.Count];

            try
            {
                TheGrp.SyncWrite(HandlesSrv.ToArray(), itemVals.ToArray(), out arrErr);

                // Show friendly error message. 
                for (int i = 0; i < arrErr.Length;i++ )
                {
                    if (arrErr[i] > 0)
                        MessageBox.Show("Item "+items[i]+" FAILED. Error Code = " + arrErr[i].ToString(), "Error");
                }
            }
            catch (Exception ex)
            {
                // Show friendly error message. 
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void cmdWriteAsync()
        {
            int CancelID = 0;
            int[] aE = new int[items.Count];
            // asynch write 

            try
            {

                TheGrp.AsyncWrite(HandlesSrv.ToArray(), itemVals.ToArray(), 55667788, out CancelID, out aE);
            }
            catch (Exception ex)
            {
                // Show friendly error message. 
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void cmdReadSync()
        {
            int[] aE = new int[items.Count];

            // Sync Read 
            OPCItemState[] arrStat = null;

            try
            {
                TheGrp.SyncRead(OPCDATASOURCE.OPC_DS_DEVICE, HandlesSrv.ToArray(), out arrStat);

                for (int i = 0; i < arrStat.Length; i++)
                {
                    if (arrStat[i].Quality == 192)
                    {
                        itemVals[i] = arrStat[i].DataValue.ToString();
                        //_txtReadVal_0.BackColor = Color.White;
                    }
                    else
                    {
                        itemVals[i] = GetQualityText(arrStat[i].Quality);
                        //_txtReadVal_0.BackColor = Color.Red;
                        MessageBox.Show("Bad Connection, please check your PPI cable connectivity with the computer");
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Show friendly error message. 
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void cmdReadAsync()
        {
            int CancelID = 0;
            int[] aE = new int[items.Count];

            try
            {
                TheGrp.AsyncRead(HandlesSrv.ToArray(), 55667788, out CancelID, out aE);
            }
            catch (Exception ex)
            {
                // Show friendly error message. 
                MessageBox.Show(ex.ToString(), "Error");
            }

        }
        private void cmdExit()
        {
            int[] aE = new int[2];

            //if ((ItemDefs[0] != null) || (ItemDefs[1] != null) || (ItemDefs[2] != null))
            //{
            if ((TheGrp != null))
            {
                TheGrp.RemoveItems(HandlesSrv.ToArray(), out aE);
            }
            //}

            if ((TheGrp != null))
            {

                try
                {

                    TheGrp.DataChanged -= theGrp_DataChange;
                    TheGrp.ReadCompleted -= theGrp_ReadComplete;
                    TheGrp.WriteCompleted -= theGrp_WriteComplete;

                    TheGrp.Remove(false);
                }
                catch (Exception ex)
                {
                    // Show friendly error message. 
                    MessageBox.Show(ex.ToString(), "Error");
                }
                TheGrp = null;
            }

            if ((TheSrv != null))
            {

                try
                {
                    TheSrv.Disconnect();
                }
                catch (Exception ex)
                {
                    // Show friendly error message. 
                    MessageBox.Show(ex.ToString(), "Error");
                }
                TheSrv = null;
            }
        }
        private string GetQualityText(short Quality)
        {
            string functionReturnValue = null;
            switch (Quality)
            {
                case 0:
                    functionReturnValue = "BAD";
                    break;
                case 64:
                    functionReturnValue = "UNCERTAIN";
                    break;
                case 192:
                    functionReturnValue = "GOOD";
                    break;
                case 8:
                    functionReturnValue = "NOT_CONNECTED";
                    break;
                case 13:
                    functionReturnValue = "DEVICE_FAILURE";
                    break;
                case 16:
                    functionReturnValue = "SENSOR_FAILURE";
                    break;
                case 20:
                    functionReturnValue = "LAST_KNOWN";
                    break;
                case 24:
                    functionReturnValue = "COMM_FAILURE";
                    break;
                case 28:
                    functionReturnValue = "OUT_OF_SERVICE";
                    break;
                case 132:
                    functionReturnValue = "LAST_USABLE";
                    break;
                case 144:
                    functionReturnValue = "SENSOR_CAL";
                    break;
                case 148:
                    functionReturnValue = "EGU_EXCEEDED";
                    break;
                case 152:
                    functionReturnValue = "SUB_NORMAL";
                    break;
                case 216:
                    functionReturnValue = "LOCAL_OVERRIDE";

                    break;
                default:
                    functionReturnValue = "UNKNOWN QUALITY";
                    break;
            }
            return functionReturnValue;
        }

        private void theGrp_DataChange(object source, DataChangeEventArgs e)
        {
            // Debug.Print("DataChange event: " + e.transactionID.ToString());

            dataChangeCounter += 1;

            //OPCItemState s; // = default(OPCItemState);
            foreach (OPCItemState s in e.sts) //(  s in  e.sts)
            {
                if (s.Error > 0)
                {
                    for (int i = 0; i < itemIntParam.Count; i++)
                    {
                        if (s.HandleClient == itemIntParam[i])
                        {
                            // Show friendly error message. 
                            MessageBox.Show("Item " + items[i] + " FAILED. Error Code = " + s.Error.ToString(), "Error");
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < itemIntParam.Count; j++)
                    {
                        if (s.HandleClient == itemIntParam[j])
                        {
                            if (s.Quality == 192)
                            {
                                if ((s.DataValue != null))
                                {
                                    itemVals[j] = s.DataValue.ToString();
                                    //SetText(ref _txtChangeVal_0, s.DataValue.ToString());
                                    //_txtChangeVal_0.BackColor = Color.White;
                                }
                            }
                            else
                            {
                                itemVals[j] = GetQualityText(s.Quality);
                                //SetText(ref _txtChangeVal_0, GetQualityText(s.Quality));
                                //_txtChangeVal_0.BackColor = Color.Red;
                            }
                            //dc.executeUpdate("update TOPCTrItemStatus set opcItemStatus='" + itemVals[j] + "' where opcItemId='" + itemsId[j] + "'");
                        }
                    }
                }
            }
        }

        private void theGrp_ReadComplete(object source, ReadCompleteEventArgs e)
        {
            // Console.WriteLine("ReadComplete event: " + e.transactionID.ToString()) 

            //OPCItemState s; // = default(OPCItemState);
            foreach (OPCItemState s in e.sts)
            {
                if (s.Error > 0)
                {
                    for (int i = 0; i < itemIntParam.Count; i++)
                    {
                        if (s.HandleClient == itemIntParam[i])
                        {
                            // Show friendly error message. 
                            MessageBox.Show("Item " + items[i] + " FAILED. Error Code = " + s.Error.ToString(), "Error");
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < itemIntParam.Count; j++)
                    {
                        if (s.HandleClient == itemIntParam[j])
                        {
                            if (s.Quality == 192)
                            {
                                if ((s.DataValue != null))
                                {
                                    itemVals[j] = s.DataValue.ToString();
                                }
                            }
                            else
                            {
                                itemVals[j] = s.DataValue.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void theGrp_WriteComplete(object source, WriteCompleteEventArgs e)
        {
            // Console.WriteLine("WriteComplete event: " + e.transactionID.ToString()) 

            //OPCWriteResult r = default(OPCWriteResult);
            foreach (OPCWriteResult r in e.res)
            {
                if (r.Error > 0)
                {
                    for (int i = 0; i < itemIntParam.Count; i++)
                    {
                        if (r.HandleClient == itemIntParam[i])
                        {
                            // Show friendly error message. 
                            MessageBox.Show("Item " + items[i] + " FAILED. Error Code = " + r.Error.ToString(), "Error");
                        }
                    }
                }
            }
        }
    }
}
