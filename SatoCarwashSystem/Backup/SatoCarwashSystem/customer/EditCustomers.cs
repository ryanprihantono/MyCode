using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.customer
{
    public partial class EditCustomer : Form
    {
        string nopol;
        int customerId;
        bool isEdit = false;
        public EditCustomer()
        {
            InitializeComponent();
            this.Text = "Edit Customer";
        }
        public EditCustomer(string nopol)
        {
            InitializeComponent();
            DataConnect dc = new DataConnect();
            dc.connect();
            String query = "select * from (TCCCustomer join TCCCustomersDetail on TCCCustomer.customerId=TCCCustomersDetail.customerId) where nopol='" + nopol + "'";
            SqlResult result = dc.executeQuery(query);
            result.next();
            this.nopol = nopol;
            this.customerId = result.getInt("customerId");
            
            txtFirstName.Text = result.getString("firstName");



            txtLastName.Text = result.getString("lastName");
            
            txtMerk.Text = result.getString("merk");
            txtType.Text = result.getString("tipe");
            dc.disconnect();
            isEdit = true;
            this.Text = "Edit Customer";
        }
        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }
        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }
        private void txtMerk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            submit();
        }
        private void submit()
        {
            if (txtLastName.Text == "" || txtFirstName.Text == "" || txtMerk.Text == "" || txtType.Text == "")
            {
                MessageBox.Show("All field must be filled !");
            }

            System.Data.SqlClient.SqlDataReader reader;
            DataConnect dc = new DataConnect();
            dc.connect();

            String query;// = "insert into TCCCustomersDetail (firstName,lastName) values('" + txtFirstName.Text + "'," + txtLastName.Text + ")";

            
            query = "update TCCCustomer set firstName='" + txtFirstName.Text + "',lastName=" + txtLastName.Text + " where nopol='" + nopol + "'";
            
            try
            {
                dc.executeUpdate(query);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }
            dc.disconnect();
            this.Close();
        }
       
        private void EditCustomer_Load(object sender, EventArgs e)
        {
            Session.setAttribute("c",nopol);
            String a = (String)Session.getAttribute("c");
            Session.removeAttribute("c");
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            DataConnect dc = new DataConnect();
            dc.connect();
            String query = "update TCCCustomer set firstName='" + txtFirstName.Text + "',lastName='" + txtLastName.Text + "' where customerId=" + customerId;

            dc.executeUpdate(query);
            query="update TCCCustomersDetail set merk='" + txtMerk.Text + "',tipe='" + txtType.Text + "' where customerId=" + customerId;
            dc.executeUpdate(query);

            dc.disconnect();
            this.Close();
        }
    }


}
