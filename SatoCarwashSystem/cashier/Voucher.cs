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
    public partial class Voucher : Form
    {
        public Voucher()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void submit()
        {
            if (txtStartNumber.Text == "" || cmbPacket.SelectedIndex == -1)
            {
                MessageBox.Show("All field must be filled!!");
            }
            else
            {
                int endNumber = Int32.Parse(txtStartNumber.Text) + 7;
                String endNumberString = endNumber+"";
                while (endNumberString.Length < 5)
                {
                    endNumberString = "0" + endNumberString;
                }
                String range = txtStartNumber.Text + "-" + endNumberString;
                int productId = 0;
                String product = "";
                int itemPrice = 0;
                int qty = 1;
                switch (cmbPacket.SelectedIndex)
                {
                    case 0:
                        productId = 42;
                        product = "Packet 1:" + range;
                        itemPrice = 240000;
                        break;
                    case 1:
                        productId = 43;
                        product = "Packet 2:" + range;
                        itemPrice = 290000;
                        break;
                    case 2:
                        productId = 44;
                        product = "Packet 3:" + range;
                        itemPrice = 260000;
                        break;
                }
                List<Object> fromList = new List<Object> { productId, product, itemPrice, qty };
                Session.setAttribute("fromVoucherList", fromList);
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtStartNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void cmbPacket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }
    }
}
