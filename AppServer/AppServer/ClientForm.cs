using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;


namespace AppServer
{
    public partial class ClientForm : Form
    {
        String location;

        Socket socket;
        Stream s;
        StreamReader sr;
        StreamWriter sw;
        String msg = "";

        public delegate void UpdateMsgReceiveCallback(string text);
        public delegate void UpdateBTFCallback();
        private void SetMsgReceive(string text)
        {
            // Set the textbox text.
            txtMsgReceive.Text = text;
        }
        private void btf()
        {
            BringToFront();
        }
        public ClientForm(Socket socket)
        {
            InitializeComponent();
            this.socket = socket;
            s = new NetworkStream(socket);
            sr = new StreamReader(s);
            sw = new StreamWriter(s);
            sw.AutoFlush = true;
            btnSend.Click += new EventHandler(btnSend_Click);
            Thread th = new Thread(new ThreadStart(receive));
            th.Start();
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
            while (true)
            {
                String txt = sr.ReadLine();
                //MessageBox.Show(sr.ReadLine());
                msg += txt + "\n";
                //Thread th = new Thread(new ThreadStart(cf.Show()));
                
                //cf.txtMsgReceive.Text = cf.txtMsgReceive.Text + msg + "\n";
                txtMsgReceive.Invoke(new UpdateMsgReceiveCallback(this.SetMsgReceive), new object[] { msg });
            
            }
        }

        void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMsgSend.Text != "")
            {
                send(txtMsgSend.Text);
            }
            txtMsgSend.Text = "";
        }
        public void send(String message)
        {
            sw.WriteLine(message, ConfigurationSettings.AppSettings.Count);
        }
    }
}
