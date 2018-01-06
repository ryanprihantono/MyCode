using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace AppServer.lib
{
    class SocketServer
    {
        TcpListener listener;
        public List<ClientHandler> clients = new List<ClientHandler>();
        public int clientCount = 0;
        public SocketServer()
        {
            int port=11000;
            listener = new TcpListener(port);
            listener.Start();
            Console.WriteLine("Listening to port " + port);
            Thread th = new Thread(new ThreadStart(readClientMessages));
            th.Start();
            //Thread th1 = new Thread(new ThreadStart(readClientMessages));
        }
        public void listenClients()
        {
            while (true)
            {
                Socket client = listener.AcceptSocket();
                clients.Add(new ClientHandler(client));
            }
        }
        private void readClientMessages()
        {
            while (true)
            {
                //Console.WriteLine(clients.Count);
                if (clients.Count > 0)
                {
                    foreach (ClientHandler item in clients)
                    {
                        String msg = item.readMessage();
                        
                        if (msg != "")
                        {
                            
                            Console.WriteLine("asdf : "+msg);
                        }
                    }
                }
            }
        }
    }
}
