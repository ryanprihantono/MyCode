using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.cashier
{
    public partial class Payment : Form
    {
        private int total;
        private int soId;
        public Payment(int total,int soId)
        {
            this.soId = soId;
            this.total = total;
            InitializeComponent();
        }

        private void txtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter();
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            enter();
        }
        private void enter()
        {
            if (txtCash.Text == "")
            {
                MessageBox.Show("Cannot be empty !!");
            }
            else
            {
                try
                {
                    int cash = Int32.Parse(txtCash.Text);
                    if (cash < total)
                    {
                        MessageBox.Show("Cash must greater than Total Amount !");
                    }
                    else
                    {
                        Session.setAttribute("cash", cash);
                        this.Close();
                    }
                }
                catch(Exception e)
                {
                    txtCash.Text = "";
                    MessageBox.Show("Input only number !!\n"+e.Message+"\n"+e.StackTrace);
                }
            }
        }
    }
}
