using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SatoCarwashSystem.data;
using SatoCarwashSystem.lib;
using SatoCarwashSystem.settings;
using SatoCarwashSystem.connect;


namespace SatoCarwashSystem
{
    public partial class Login : Form
    {
        private StreamReader reader;
        public Login()
        {
            InitializeComponent();
            
            readConf();
            Connection.socketClient = new SocketClient();
        }

        private String readConf()
        {
            String a = "";
            try
            {
                reader = new StreamReader(Directory.GetCurrentDirectory() + "\\sato.conf");
                
                do
                {
                    a += reader.ReadLine();
                } while (reader.Peek() != -1);
                
                //MessageBox.Show(a);
                
                String[] splitter = a.Split('~');
                
                Connection.dbConnectionString = splitter[0];
                Connection.serverAddress = splitter[1];
                
                //MessageBox.Show(a);

            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to initialize " + e.StackTrace + e.Message);
            }
            if (reader != null)
                reader.Close();

            return a;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            doLogin();
        }
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doLogin();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doLogin();
            }
        }
        private void doLogin()
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                
                if (Employee.loginQuery(txtUsername.Text, txtPassword.Text))
                {
                    Connection.socketClient.sendSysMessage("identity|" + txtUsername.Text + "|" + Session.locations[0].locationId);
                    if ((String)Session.getAttribute("position") == "Cashier" || (String)Session.getAttribute("position") == "admin")
                    {
                        SatoCarwashSystem scs = new SatoCarwashSystem();
                        scs.Disposed += new EventHandler(child_Disposed);
                        scs.Show();
                        this.Visible = false;
                    }
                    else if ((String)Session.getAttribute("position") == "Operator")
                    {
                        CheckIn ci = new CheckIn();
                        ci.Disposed += new EventHandler(child_Disposed);
                        ci.Show();
                        this.Visible = false;
                    }
                    else if ((String)Session.getAttribute("position") == "Admin")
                    {
                        SatoCarwashSystem scs = new SatoCarwashSystem();
                        scs.Disposed += new EventHandler(child_Disposed);
                        scs.Show();
                        this.Visible = false;
                    }
                    else
                    {

                        MessageBox.Show("You are not authorized to enter this application");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password !");
                }
            }
            else
            {
                MessageBox.Show("Username and Password can't be empty !");
            }
        }

        void child_Disposed(object sender, EventArgs e)
        {
            this.Visible = true;
            Connection.socketClient.disconnect();
            Connection.opc.disconnect();
            this.Close();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            ConnectionSettings cs = new ConnectionSettings();
            cs.Disposed += new EventHandler(cs_Disposed);
            cs.Show();
            this.Visible = false;
        }

        void cs_Disposed(object sender, EventArgs e)
        {
            this.Visible = true;
        }

    }
}