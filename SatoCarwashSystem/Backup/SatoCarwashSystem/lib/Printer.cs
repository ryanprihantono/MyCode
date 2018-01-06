using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SatoCarwashSystem.data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.IO;
using System.Globalization;

namespace SatoCarwashSystem.lib
{
    class Printer:DataConnect
    {
        const int MAX_LINE_WIDTHS = 2;

        public bool flag=true;

        Order order;
        PosPrinter m_Printer = null;
        
        public Printer()
        {
            this.init();
            this.connect();
        }
        private void init()
        {
            //<<<step1>>>--Start
            //Use a Logical Device Name which has been set on the SetupPOS.
            string strLogicalName = "PosPrinter";

            //Current Directory Path
            string strCurDir = Directory.GetCurrentDirectory();

            string strFilePath = strCurDir + "\\img\\";

            strFilePath += "satologo1.bmp";
            //MessageBox.Show(strFilePath);
            try
            {
                //Create PosExplorer
                PosExplorer posExplorer = new PosExplorer();

                DeviceInfo deviceInfo = null;
                
                try
                {
                    deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName);
                    
                    m_Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
                }
                catch (Exception)
                {
                    MessageBox.Show("Printer Error");
                    flag = false;
                    return;
                }

                //Open the device
                m_Printer.Open();

                //Get the exclusive control right for the opened device.
                //Then the device is disable from other application.
                m_Printer.Claim(1000);

                //Enable the device.
                m_Printer.DeviceEnabled = true;

                //<<<step3>>>--Start
                //Output by the high quality mode
                m_Printer.RecLetterQuality = true;

                if (m_Printer.CapRecBitmap == true)
                {

                    bool bSetBitmapSuccess = false;
                    for (int iRetryCount = 0; iRetryCount < 5; iRetryCount++)
                    {
                        try
                        {
                            //<<<step5>>>--Start
                            //Register a bitmap
                            
                            m_Printer.SetBitmap(1, PrinterStation.Receipt,
                                strFilePath, m_Printer.RecLineWidth / 2,
                                PosPrinter.PrinterBitmapCenter);
                            //<<<step5>>>--End
                            bSetBitmapSuccess = true;
                            
                            break;
                        }
                        catch (PosControlException pce)
                        {
                            //MessageBox.Show(pce.Message + "\n" + pce.StackTrace);
                            if (pce.ErrorCode == ErrorCode.Failure && pce.ErrorCodeExtended == 0 && pce.Message == "It is not initialized.")
                            {
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                    }
                    if (!bSetBitmapSuccess)
                    {
                        MessageBox.Show("Failed to set bitmap.", "Sato Carwash Management System"
                                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //<<<step3>>>--End

                //<<<step5>>>--Start
                // Even if using any printers, 0.01mm unit makes it possible to print neatly.
                m_Printer.MapMode = MapMode.Metric;
                //<<<step5>>>--End
            }
            catch (PosControlException)
            {
                flag = false;
                MessageBox.Show("Printer Error");
            }
            //<<<step1>>>--End
        }
        public void unInit()
        {
            if (m_Printer != null)
            {
                try
                {
                    //Cancel the device
                    m_Printer.DeviceEnabled = false;

                    //Release the device exclusive control right.
                    m_Printer.Release();

                }
                catch (PosControlException)
                {
                }
                finally
                {
                    //Finish using the device.
                    m_Printer.Close();
                }
            }
        }

        private long GetRecLineChars(ref int[] RecLineChars)
        {
            long lRecLineChars = 0;
            long lCount;
            int i;

            // Calculate the element count.
            lCount = m_Printer.RecLineCharsList.GetLength(0);

            if (lCount == 0)
            {
                lRecLineChars = 0;
            }
            else
            {
                if (lCount > MAX_LINE_WIDTHS)
                {
                    lCount = MAX_LINE_WIDTHS;
                }

                for (i = 0; i < lCount; i++)
                {
                    RecLineChars[i] = m_Printer.RecLineCharsList[i];
                }

                lRecLineChars = lCount;
            }

            return lRecLineChars;
        }

        public String MakePrintString(int iLineChars, String strBuf, String strPrice)
        {
            int iSpaces = 0;
            String tab = "";
            try
            {
                iSpaces = iLineChars - (strBuf.Length + strPrice.Length);
                for (int j = 0; j < iSpaces; j++)
                {
                    tab += " ";
                }
            }
            catch (Exception)
            {
            }
            return strBuf + tab + strPrice;
        }

        private String getTime(DateTime dt)
        {
            return dt.Day + " " + getMonth(dt.Month-1) + " " + dt.Year + " " + roundTime(dt.Hour) + ":" + roundTime(dt.Minute) + ":" + roundTime(dt.Second);
        }
        private String roundTime(int time)
        {
            String timeString;
            if (time < 10)
            {
                timeString = "0" + time;
            }
            else
            {
                timeString = "" + time;
            }
            return timeString;
        }
        private String getMonth(int month)
        {
            String[] monthArr = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return monthArr[month];
        }

        public void print(Order order,int cash,String empName,String nopol,String customerName,String telp)
        {
            //<<<step2>>>--Start
            //Initialization							//System date

            this.order = order;
            
            string strbcData = "4902720005074";
            int[] RecLineChars = new int[MAX_LINE_WIDTHS] { 0, 0 };
            long lRecLineCharsCount;
            
            //<<<step6>>>--Start
            //When outputting to a printer,a mouse cursor becomes like a hourglass.
            Cursor.Current = Cursors.WaitCursor;
            //<<<step6>>>--End

            if (m_Printer.CapRecPresent)
            {

                try
                {
                    //<<<step6>>>--Start
                    //Batch processing mode
                    DateTime date = order.soDate;
                    m_Printer.TransactionPrint(PrinterStation.Receipt
                        , PrinterTransactionControl.Transaction);

                    //<<<step3>>>--Start
                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B");
                    //<<<step3>>>--End

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "\u001b|cA" + Session.locations[0].location + "\nShine Your Ride" + "\n");

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + Session.locations[0].locationAddress + "\n");


                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC" + MakePrintString(m_Printer.RecLineChars, " ", " ") + "\n");

                    //m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|rA"
                        //+ "TEL 9999-99-9999   C#2\n");

                    //<<<step5>>--Start
                    //Make 2mm speces
                    //ESC|#uF = Line Feed
                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|200uF");
                    //<<<step5>>>-End

                    m_Printer.PrintNormal(PrinterStation.Receipt, MakePrintString(m_Printer.RecLineChars, order.soNumber,getTime(order.soDate) + "\n"));

                    m_Printer.PrintNormal(PrinterStation.Receipt, MakePrintString(m_Printer.RecLineChars, "Cashier", empName + "\n"));

                    //lRecLineCharsCount = GetRecLineChars(ref RecLineChars);
                    //if (lRecLineCharsCount >= 2)
                    //{
                    //    m_Printer.RecLineChars = RecLineChars[1];
                    //    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + getTime(date) + "\n");
                    //    m_Printer.RecLineChars = RecLineChars[0];
                    //}
                    //else
                    //{
                    //    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + getTime(date) + "\n");
                    //}

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC" + MakePrintString(m_Printer.RecLineChars, " ", " ") + "\n");
                    //<<<step5>>>--Start
                    //Make 5mm speces
                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                    //Print buying goods
                    int total = 0;
                    string strPrintData = "";

                    foreach(OrderDetail item in order.orderDetail)
                    {
                        strPrintData = MakePrintString(m_Printer.RecLineChars, item.product, Converter.currencyFormat(item.itemPrice*item.qty));

                        m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + "\n");

                        strPrintData = MakePrintString(m_Printer.RecLineChars, item.qty+" x @"+Converter.currencyFormat(item.itemPrice), " ");

                        m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + "\n");

                        total += (item.itemPrice*item.qty);

                    }

                    //Make 2mm speces
                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|200uF");

                    //Print the total cost
                    //strPrintData = MakePrintString(m_Printer.RecLineChars, "Tax excluded."
                        //, "$" + total.ToString("F"));

                    //m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + strPrintData + "\n");

                    //strPrintData = MakePrintString(m_Printer.RecLineChars, "Tax 5.0%", "$"
                        //+ (total * 0.05).ToString("F"));

                    //m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC" + strPrintData + "\n");


                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC" + MakePrintString(m_Printer.RecLineChars, " ", " ") + "\n");


                    strPrintData = MakePrintString(m_Printer.RecLineChars / 2, "Total", "Rp "
                        + Converter.currencyFormat(total));

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C"
                        + strPrintData + "\n");

                    strPrintData = MakePrintString(m_Printer.RecLineChars, "Cash", Converter.currencyFormat(cash));

                    m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + "\n");

                    strPrintData = MakePrintString(m_Printer.RecLineChars, "Change", Converter.currencyFormat(cash - total));

                    m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + "\n");

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                    m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|cA"+"Terimakasih telah berkunjung!");

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC" + MakePrintString(m_Printer.RecLineChars, " ", " ") + "\n");

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "\u001b|cA" + customerName + "\n" + nopol + "\n" + telp + "\n");

                    //Make 5mm speces
                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                    //<<<step4>>>--Start
                    if (m_Printer.CapRecBarCode == true)
                    {
                        //Barcode printing
                        m_Printer.PrintBarCode(PrinterStation.Receipt, strbcData,
                            BarCodeSymbology.EanJan13, 1000,
                            m_Printer.RecLineWidth, PosPrinter.PrinterBarCodeLeft,
                            BarCodeTextPosition.Below);
                    }
                    //<<<step4>>>--End
                    //<<<step5>>>--End

                    m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
                    //<<<step2>>>--End

                    //print all the buffer data. and exit the batch processing mode.
                    m_Printer.TransactionPrint(PrinterStation.Receipt
                        , PrinterTransactionControl.Normal);
                    //<<<step6>>>--End
                }
                catch (PosControlException)
                {
                }
            }

            //<<<step6>>>--Start
            // When a cursor is back to its default shape, it means the process ends
            Cursor.Current = Cursors.Default;
            //<<<step6>>>--End
            
        }


    }
}
