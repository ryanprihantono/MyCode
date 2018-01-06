using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.settings
{
    public partial class ChangePassword : Form
    {
        private String pass = "";
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void submit()
        {
            if (txtOldPass.Text == "" || txtNewPass.Text == "" || txtConfPass.Text == "")
            {
                MessageBox.Show("All field must be filled");
            }
            else
            {
                if (txtOldPass.Text != pass)
                {
                    MessageBox.Show("Wrong old password");
                }
                else if (txtConfPass.Text != txtNewPass.Text)
                {
                    MessageBox.Show("New Password and Confirm password didn't match");
                }
                else
                {
                    DataConnect dc = new DataConnect();
                    dc.connect();
                    dc.executeUpdate("update THRMEmployee set pass='" + txtNewPass.Text + "' where employeeId=" + Session.getAttribute("employeeId"));
                    dc.disconnect();
                    this.Close();
                }
            }
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            String query = "select * from THRMEmployee where employeeId=" + Session.getAttribute("employeeId");
            DataConnect dc = new DataConnect();
            dc.connect();

            SqlResult result = dc.executeQuery(query);
            result.next();

            pass = result.getString("pass");

            dc.disconnect();
            

            txtOldPass.KeyDown += new KeyEventHandler(txt_KeyDown);
            txtNewPass.KeyDown += new KeyEventHandler(txt_KeyDown);
            txtConfPass.KeyDown += new KeyEventHandler(txt_KeyDown);
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
