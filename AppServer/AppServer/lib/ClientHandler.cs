using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

using System.Windows.Forms;
using System.Data.SqlClient;

namespace AppServer.lib
{
    public class ClientHandler
    {
        String location;

        Socket socket;
        Stream s;
        StreamReader sr;
        StreamWriter sw;
        String msg = "";
        String sendTo = "";

        public String username;
        public String remoteHostAddress;
        public String remoteHostName;

        public bool isConnect = false;

        public ClientHandler(Socket socket)
        {
            try
            {
                this.socket = socket;
                s = new NetworkStream(socket);
                sr = new StreamReader(s);
                sw = new StreamWriter(s);
                sw.AutoFlush = true;
                Thread th = new Thread(new ThreadStart(receive));
                th.IsBackground = true;
                th.Start();
                isConnect = true;
            }
            catch
            {
                disconnect();
            }
        }
        public String readMessage()
        {
            String str = "";
            if (msg != "")
            {
                str = msg;
                msg = "";
            }

            return str;
        }
        private void receive()
        {
            try
            {
                while (true)
                {
                    String temp = sr.ReadLine();
                    //Console.WriteLine(temp);
                    String[] splitter = temp.Split('|');

                    if (splitter[0] == "sysmsg")
                    {
                        if (splitter[1] == "connected")
                        {
                            remoteHostAddress = splitter[2];
                            remoteHostName = splitter[3];
                        }
                        else if (splitter[1] == "identity")
                        {
                            username = splitter[2];
                            msg = "identified";
                        }
                    }
                    else if (splitter[0] == "msg")
                    {
                        if (splitter[1] == "server")
                        {
                            msg = splitter[2];
                        }
                        else
                        {
                            msg = splitter[1] + splitter[2];

                        }
                    }
                    else if (splitter[0] == "exeUpdate")
                    {
                        //Console.WriteLine(temp);
                        //Console.WriteLine(splitter[0]);
                        DataConnect dc = new DataConnect();
                        dc.connect();
                        int aff = dc.executeUpdate(splitter[1]);
                        send("affected|" + aff);
                        dc.disconnect();
                        msg = "execute update \"" + splitter[1] + "\"";
                    }
                    else if (splitter[0] == "exeQuery")
                    {
                        DataConnect dc = new DataConnect();
                        dc.connect();
                        SqlDataReader reader = dc.executeQuery(splitter[1]);
                        sendResult(reader);
                        dc.disconnect();
                        msg = "execute query \"" + splitter[1] + "\"";
                    }
                    else if (splitter[0] == "cmdUpdate")
                    {
                        DataConnect dc = new DataConnect();
                        dc.connect();
                        dc.executeQuery(splitter[1]);
                        dc.disconnect();
                        msg = "do update " + splitter[1] + " from " + this.remoteHostName + "to " + splitter[2] + "|" + splitter[1] + "|" + this.remoteHostName;
                    }
                    else if (splitter[0] == "checkUpdate")
                    {
                        String clientFile = splitter[1];
                        string[] serverFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\app\\", "*.exe", SearchOption.AllDirectories);
                        //MessageBox.Show(Directory.GetCurrentDirectory()+"-"+splitter[1]);
                        string[] splitter1 = serverFiles[0].Split('\\');
                        String serverFile = splitter1[splitter1.Length - 1];
                        //MessageBox.Show(splitter[0] + "|" + splitter1);
                        if (clientFile != serverFile)
                        {
                            byte[] byteFile = File.ReadAllBytes("app\\" + serverFile);
                            msg = byteFile.Length + "";
                            int byteLength = byteFile.Length;

                            send("updater|download|" + serverFile + "|" + byteLength);
                            msg = this.remoteHostName + " download ";

                            int bufferSize = 1024;
                            int offset = 0;
                            
                            while (offset < byteLength)
                            {
                                Thread.Sleep(100);
                                if ((byteLength - offset) < bufferSize)
                                {
                                    bufferSize = byteLength - offset;
                                }
                                
                                byte[] buffer = new byte[bufferSize];
                                Buffer.BlockCopy(byteFile, offset, buffer, 0, bufferSize);
                                s.Write(buffer, 0, bufferSize);
                                
                                offset += 1024;
                            }
                        }
                        else
                        {
                            send("updater|nothing");
                            msg = this.remoteHostName + " nothing ";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                msg = "disconnected";
                //Console.WriteLine("disconnected");
                Console.WriteLine("-" + e.StackTrace);
                Console.WriteLine("-" + e.Message);
                disconnect();
            }
        }
        public void disconnect()
        {
            //Socket socket;
            //Stream s;
            //StreamReader sr;
            //StreamWriter sw;

            if (sw != null)
            {
                sw.Close();
            }
            if (sr != null)
            {
                sw.Close();
            }
            if (s != null)
            {
                s.Close();
            }
            if (socket != null)
            {
                socket.Close();
            }
            isConnect = false;
        }
        public void sendResult(SqlDataReader reader)
        {

            send("result|begin");
            msg = "send begin";
            String name = "";
            if (name == "")
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    name += reader.GetName(i) + "";
                    if (i < reader.FieldCount - 1)
                    {
                        name += "~";
                    }
                }
                //Console.WriteLine(name);
                send("result|" + name);
            }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    String row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader[i] + "" == "")
                        {
                            row += "null";
                        }
                        else
                        {
                            row += reader[i] + "";
                        }
                        if (i < reader.FieldCount - 1)
                        {
                            row += "~";
                        }
                    }
                    //Console.WriteLine(row);
                    send("result|" + row);

                }
                send("result|end");
                //Console.WriteLine("send end");
                msg = "send end";
            }
            else
            {
                send("result|end");
                //Console.WriteLine("send end");
                msg = "send end";
            }
        }
        public void sendQuery(String query, String from)
        {
            send("query|" + query + "|" + from);
        }
        private void send(String message)
        {
            try
            {
                sw.WriteLine(message);
            }
            catch
            {
                disconnect();
            }
        }

    }
}
