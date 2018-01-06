using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.settings
{
    public partial class ConnectionSettings : Form
    {
        
        private StreamWriter writer;
        
        public ConnectionSettings()
        {
            InitializeComponent();
        }

        private void ConnectionSettings_Load(object sender, EventArgs e)
        {
            txtConString.Text = Connection.dbConnectionString;
            txtHostAddress.Text = Connection.serverAddress;
        }
        private void writeConf()
        {
            Connection.dbConnectionString = txtConString.Text;
            Connection.serverAddress = txtHostAddress.Text;
            writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\sato.conf");
            writer.AutoFlush = true;
            writer.WriteLine(Connection.dbConnectionString + "~" + Connection.serverAddress);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            writeConf();
            writer.Close();
            this.Dispose();
        }
    }
}
