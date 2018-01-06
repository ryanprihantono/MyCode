using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;

using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.connect
{
    public class SocketClient
    {
        TcpClient client;
        Stream s;
        StreamReader sr;
        StreamWriter sw;
        String message = "";
        Thread th;
        Thread th1;
        public bool isConnected = false;
        List<String[]> result = null;

        public delegate void DelegateMsgReceive(string text);

        public void SetMsgReceive(string text)
        {
            //txtMsgReceive.Text = text;
        }

        public SocketClient()
        {
            connect();
        }
        public void connect()
        {
            th = new Thread(new ThreadStart(doConnect));
            th.IsBackground = true;
            th.Start();
        }
        
        private void doConnect()
        {
            while (!isConnected)
            {
                try
                {
                    client = new TcpClient(Connection.serverAddress, 11001);
                    
                    s = client.GetStream();

                    //NetworkStream stream = new NetworkStream(client, true);
                    
                    sr = new StreamReader(s);
                    sw = new StreamWriter(s);
                    
                    sw.AutoFlush = true;
                    //Console.WriteLine(sr.ReadLine());
                    th1 = new Thread(new ThreadStart(read));
                    th1.IsBackground = true;
                    th1.Start();
                    isConnected = true;

                    sendSysMessage("connected|" + LocalIPAddress() + "|" + Dns.GetHostName());

                    sendMessage("server|connected");
                    //s.Close();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine(e.Message);
                    //isConnected = false;
                    disconnect();
                }
            }
        }
        private void read()
        {
            try
            {
                while (true)
                {
                    String temp = sr.ReadLine();
                    
                    String []splitter = temp.Split('|');
                    if (splitter[0] == "msg")
                    {
                        message = splitter[1] + splitter[2];
                    }
                    else if (splitter[0] == "query")
                    {
                        DataConnect dc = new DataConnect();
                        dc.connect();
                        dc.executeUpdate(splitter[1]);
                        dc.disconnect();
                    }
                    else if (splitter[0] == "opc")
                    {
                        Connection.opc.cmdWrite(splitter[1], splitter[2]);
                    }
                    else if (splitter[0] == "result")
                    {

                        if (splitter[1] == "begin")
                        {
                            result = new List<String[]>();
                            message = "result~begin";
                        }
                        else if (splitter[1] == "end")
                        {
                            message = "result~end";
                        }
                        else
                        {
                            String[] row = splitter[1].Split('~');
                            //Console.WriteLine(splitter[1]);
                            //Console.WriteLine(row.Length+"");
                            result.Add(row);
                            message = "result~progress";
                        }
                    }
                    else if (splitter[0] == "affected")
                    {
                        message = splitter[0] + "|" + splitter[1];
                    }
                }
            }
            catch
            {
                isConnected = false;
                disconnect();
                doConnect();
            }
        }
        public List<String[]> readResult()
        {
            List<String[]> temp = null;
            
            do
            {
                temp = result;
            } while (result == null);
            result = null;
            return temp;

        }
        public String readMessage()
        {
            String str = "";
            if (message != "")
            {
                str = message;
                message = "";
            }
            return str;
        }
        public void sendExecuteUpdate(String query)
        {
            send("exeUpdate|" + query);
        }
        public void sendExecuteQuery(String query)
        {
            send("exeQuery|" + query);
        }
        public void sendMessage(String message)
        {
            send("msg|" + message);
        }
        public void sendMessageTo(String message,String recepient)
        {
            send("msg|" + recepient + "|" + message);
        }
        public void sendSysMessage(String message)
        {
            send("sysmsg|" + message);
        }
        public void cmdQuery(String query,String to)
        {
            send("cmdQuery|" + query + "|" + to);
        }
        private void send(String msg)
        {
            try
            {
                sw.WriteLine(msg);
            }
            catch
            {
                isConnected = false;
                disconnect();
            }
        }
        public void disconnect()
        {
            if (sw != null)
            {
                sw.Close();
            }
            if (sr != null)
            {
                sr.Close();
            }
            if (s != null)
            {
                s.Close();
            }
            if (client != null)
            {
                client.Close();
            }
            
        }
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
