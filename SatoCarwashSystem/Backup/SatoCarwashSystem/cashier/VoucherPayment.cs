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
    public partial class VoucherPayment : Form
    {
        public VoucherPayment()
        {
            InitializeComponent();
        }

        private void txtVoucherNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }
        private void submit()
        {
            if (txtVoucherNumber.Text == "" || cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("All field must be field!!");
            }
            else
            {
                List<String> list = new List<string> { txtVoucherNumber.Text, cmbType.Text };
                Session.setAttribute("voucher",list);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            submit();
        }
    }
}
