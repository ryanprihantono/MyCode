using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.data;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.cashier
{
    public partial class CustomerList : Form
    {
        private String nopol = "";
        private List<CustomerDetail> customerList;
        public CustomerList()
        {
            customerList = new List<CustomerDetail>();
            InitializeComponent();
            showCustomer();
            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
        }

        void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            changetStyle();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            this.Cursor = dataGridView1.Cursor;
            KeyPreview = true;                                                                                  
            KeyDown += new KeyEventHandler(CustomerList_KeyDown);
        }

        void CustomerList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                btnStatus.PerformClick();
            }
            else if (e.KeyCode == Keys.S)
            {
                btnSound.PerformClick();
            }
        }
        private void showCustomer()
        {
            Customer cust = new Customer();

            customerList = cust.customerList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt = new DataTable();

            dt.Columns.Add("Nopol");
            dt.Columns.Add("Merk");
            dt.Columns.Add("Type");
            dt.Columns.Add("Status");
            dt.Columns.Add("SoundStatus");

            foreach (CustomerDetail item in customerList)
            {
                String status = "";
                String soundStatus = "";
                if (item.isProcess == 0 && item.isFinished == 0)
                {
                    status = "Waiting";
                    soundStatus = "-";
                }
                else if (item.isProcess == 1 && item.isFinished == 0)
                {

                    status = "Process";
                    soundStatus = "-";
                }
                else if (item.isProcess == 0 && item.isFinished == 1)
                {
                    status = "Finished";
                    if (item.sound < 5)
                    {
                        soundStatus = "Playing";
                    }
                    else
                    {
                        soundStatus = "Stopped";
                    }
                }
                dt.Rows.Add(new[] { item.nopol, item.merk, item.tipe, status, soundStatus });
            }
            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns["Choose"] == null)
            {
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                col.Text = "Choose";
                col.Name = "Choose";
                dataGridView1.Columns.Add(col);
            }

            btnStatus.Enabled = false;
            lblStatus.Visible = false;
            btnSound.Enabled = false;

            dataGridView1.Refresh();
        }
        private void changetStyle()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                //MessageBox.Show(dataGridView1.Rows[i].Cells[4].Value + "");
                if ((String)dataGridView1.Rows[i].Cells[4].Value == "Process")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;

                }
                else if ((String)dataGridView1.Rows[i].Cells[4].Value == "Finished")
                {

                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
 
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                List<Object> fromList = new List<Object> { customerList[e.RowIndex].nopol, customerList[e.RowIndex].customerCode,customerList[e.RowIndex].tipe };
                Session.setAttribute("fromCustomerList", fromList);
                this.Close();
            }
            //MessageBox.Show(e.ColumnIndex + "");
            else
            {
                btnStatus.Enabled = true;

                if (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() == "Playing")
                {
                    btnSound.Enabled = true;
                    btnSound.Text = "Stop Sound";
                    nopol = customerList[e.RowIndex].nopol;
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() == "Stopped")
                {
                    btnSound.Enabled = true;
                    btnSound.Text = "Play Sound";
                    nopol = customerList[e.RowIndex].nopol;
                }
                else
                {
                    btnSound.Enabled = false;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    btnStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value + "";
                    lblStatus.Text = e.RowIndex+"";
                }
            }
        }

        private void updateSound(String nopol, int sound)
        {
            DataConnect dc = new DataConnect();
            dc.connect();
            String query = "update TCCCustomersDetail set sound=" + sound + " where nopol='" + nopol + "'";
            dc.executeUpdate(query);
            dc.disconnect();
        }

        private void changeStatus(String nopol,String status)
        {
            int isOnSite = 0;
            int isProcess = 0;
            int isFinished = 0;
            
            if (status == "Process")
            {
                isOnSite = 1;
                isProcess = 1;
                isFinished = 0;
            }
            else if (status == "Finished")
            {
                isOnSite = 1;
                isProcess = 0;
                isFinished = 1;
            }
            else if (status == "Checkout")
            {
                isOnSite = 0;
                isProcess = 0;
                isFinished = 0;
            }

            String query = "update TCCCustomersDetail set isOnSite="+isOnSite+",isProcess="+isProcess+",isFinished="+isFinished+",sound=0 where nopol='"+nopol+"'";

            DataConnect dc = new DataConnect();
            dc.connect();
            dc.executeUpdate(query);
            dc.disconnect();
            showCustomer();
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            if (btnSound.Text == "Play Sound")
            {
                updateSound(nopol, 0);
            }
            else if (btnSound.Text == "Stop Sound")
            {
                updateSound(nopol, 6);
            }
            showCustomer();
        }

        private void btnStatus_Click_1(object sender, EventArgs e)
        {
            if (btnStatus.Text == "Waiting")
            {
                if (MessageBox.Show("Change to Process?", "Confirm Change Status", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    changeStatus((String)dataGridView1.Rows[Int32.Parse(lblStatus.Text)].Cells[1].Value, "Process");
                    dataGridView1.Rows[Int32.Parse(lblStatus.Text)].Cells[4].Value = "Process";
                    dataGridView1.Rows[Int32.Parse(lblStatus.Text)].DefaultCellStyle.BackColor = Color.Green;
                    dataGridView1.Refresh();
                }

            }
            else if (btnStatus.Text == "Process")
            {
                if (MessageBox.Show("Change to Finished?", "Confirm Change Status", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    changeStatus((String)dataGridView1.Rows[Int32.Parse(lblStatus.Text)].Cells[1].Value, "Finished");
                    dataGridView1.Rows[Int32.Parse(lblStatus.Text)].Cells[4].Value = "Finished";
                    dataGridView1.Rows[Int32.Parse(lblStatus.Text)].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView1.Refresh();
                }
            }
            else if (btnStatus.Text == "Finished")
            {
                if (MessageBox.Show("Are you sure to checkout " + dataGridView1.Rows[Int32.Parse(lblStatus.Text)].Cells[1].Value + " ?", "Confirm Change Status", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    changeStatus((String)dataGridView1.Rows[Int32.Parse(lblStatus.Text)].Cells[1].Value, "Checkout");
                    //MessageBox.Show(lblStatus.Text);
                    //dataGridView1.Rows.RemoveAt(Int32.Parse(lblStatus.Text));
                    
                    dataGridView1.Refresh();
                }
            }
        }
    }
}
