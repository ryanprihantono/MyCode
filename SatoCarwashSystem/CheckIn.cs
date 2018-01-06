using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.data;


namespace SatoCarwashSystem
{
    public partial class CheckIn : Form
    {
        public CheckIn()
        {
            InitializeComponent();

            groupBox1.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - (groupBox1.Width / 2), (SystemInformation.VirtualScreen.Height / 2) - (groupBox1.Height / 2));

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            checkIn();
        }

        private void txtNopol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkIn();
            }
        }

        private void txtMerk_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                checkIn();
            }
            
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkIn();
            }
            
        }

        private void inputTxt(ref TextBox txtBox, String letter)
        {
            
            
            txtBox.Text = txtBox.Text.Remove(0,1);
            txtBox.Text += letter;

            int start = txtBox.SelectionStart;
            int length = txtBox.SelectionLength;
            txtBox.SelectionStart = start;
            txtBox.SelectionLength = length;

        }
        private void toUpper(ref TextBox txtBox)
        {
            int start = txtBox.SelectionStart;
            int length = txtBox.SelectionLength;
            txtBox.Text = txtBox.Text.ToUpper();
            txtBox.SelectionStart = start;
            txtBox.SelectionLength = length;
        }
        private void checkIn()
        {
            if (txtNopol.Text != "" && txtMerk.Text != "" && txtType.Text != "")
            {
                Customer cust = new Customer();
                toUpper(ref txtNopol);
                toUpper(ref txtMerk);
                toUpper(ref txtType);

                if (cust.checkIn(txtNopol.Text, txtMerk.Text, txtType.Text))
                {
                    txtNopol.Text = "";
                    txtMerk.Text = "";
                    txtType.Text = "";
                    MessageBox.Show("Check In Success");
                }
            }
        }



        private void txtNopol_Leave(object sender, EventArgs e)
        {
            toUpper(ref txtNopol);
        }

        private void txtMerk_Leave(object sender, EventArgs e)
        {
            toUpper(ref txtMerk);
        }

        private void txtType_Leave(object sender, EventArgs e)
        {
            toUpper(ref txtType);
        }

        private void txtNopol_TextChanged(object sender, EventArgs e)
        {
            toUpper(ref txtNopol);
        }

        private void txtMerk_TextChanged(object sender, EventArgs e)
        {
            toUpper(ref txtMerk);
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            toUpper(ref txtType);
        }
    }
}
