using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

using AppServer.lib;

namespace AppServer
{
    public partial class ServerForm : Form
    {

        TcpListener listener;
        public List<ClientHandler> clients = new List<ClientHandler>();
        public int clientCount = 0;
        Thread th;
        Thread th1;

        public delegate void DelegateStatus(string text);
        public delegate void DelegateMsgReceive(string text);
        public delegate void DelegateSetListItem(List<ClientHandler> clients);

        private void SetListItem(List<ClientHandler> clients)
        {
            this.listClient.Items.Clear();
            foreach (ClientHandler item in clients)
            {
                this.listClient.Items.Add(item.username + " on " + item.remoteHostName + "(" + item.remoteHostAddress + ")");
            }
        }
        private void SetStatus(string text)
        {
            // Set the textbox text.
            this.txtStatus.Text = text;
        }
        private void SetMsgReceive(string text)
        {
            // Set the textbox text.
            this.txtMsgReceive.AppendText(text + "\n");
        }

        public ServerForm()
        {
            InitializeComponent();
            int port = 2000;
            listener = new TcpListener(port);
            listener.Start();
            txtStatus.Text = "Listening to port " + port;

            th = new Thread(new ThreadStart(listenClients));
            th.IsBackground = true;
            th.Start();

            th1 = new Thread(new ThreadStart(readClientMessages));
            th1.IsBackground = true;
            th1.Start();
        }
        public void listenClients()
        {
            while (true)
            {
                Socket client = listener.AcceptSocket();
                //txtStatus.Text = clients.Count + "client(s) active";

                clients.Add(new ClientHandler(client));
                txtStatus.Invoke(new DelegateStatus(this.SetStatus), new object[] { clients.Count + " client(s) active" });
            }
        }
        private void readClientMessages()
        {
            while (true)
            {
                //Console.WriteLine(clients.Count);
                if (clients.Count > 0)
                {

                    for (int i = 0; i < clients.Count; i++)
                    {
                        ClientHandler item = clients[i];
                        String msg;
                        if (item != null)
                        {
                            msg = item.readMessage();

                            if (msg != "")
                            {
                                if (msg.IndexOf("do update from") != -1)
                                {
                                    String[] spliter = msg.Split('|');
                                    String query = spliter[1];
                                    String from = spliter[2];
                                    item.sendQuery(query, from);
                                }
                                if (msg == "identified")
                                {
                                    Console.WriteLine(item.username);
                                    setList();
                                }
                                txtMsgReceive.Invoke(new DelegateMsgReceive(this.SetMsgReceive), new object[] { item.remoteHostName + " [" + item.username + "]" +  Converter.getTime() + " : " + msg + "\n" });
                                if (msg == "disconnected")
                                {
                                    item.disconnect();
                                    clients.Remove(item);
                                    txtStatus.Invoke(new DelegateStatus(this.SetStatus), new object[] { clients.Count + " client(s) active" });
                                    setList();
                                }
                            }
                            if (!item.isConnect)
                            {
                                item.disconnect();
                                clients.Remove(item);
                                txtStatus.Invoke(new DelegateStatus(this.SetStatus), new object[] { clients.Count + " client(s) active" });
                                setList();
                            }
                            
                        }
                    }



                }
            }
        }
        private void setList()
        {
            listClient.Invoke(new DelegateSetListItem(this.SetListItem), new object[] { clients });
                //Console.WriteLine(item.username);
        }
        private void ServerForm_Load(object sender, EventArgs e)
        {
            //server = new AppServer.lib.SocketServer();

        }
    }
}
