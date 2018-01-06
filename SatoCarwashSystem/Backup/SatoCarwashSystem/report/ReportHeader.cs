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
using SatoCarwashSystem.report;

namespace SatoCarwashSystem.report
{
    public partial class ReportHeader : Form
    {
        List<Invoice> invoices = new List<Invoice>();
        int large = 0;
        int medium = 0;
        int small = 0;
        int drinks = 0;
        public ReportHeader()
        {
            InitializeComponent();
        }

        private void ReportHeader_Load(object sender, EventArgs e)
        {
            invoices = Invoice.getTransaction(startDate.Value, endDate.Value, Session.locations[0].locationId+"");
            //MessageBox.Show(Converter.getTime(startDate.Value));
            
            //cmbLocation.Text = Session.locations[0].location + "";
            cmbLocation.DataSource = Session.locations;
            cmbLocation.DisplayMember = "location";
            cmbLocation.ValueMember = "locationId";
            showTransaction();
        }
        private void showTransaction()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt = new DataTable();

            dt.Columns.Add("Invoice");
            dt.Columns.Add("SO");
            dt.Columns.Add("SO Date");
            dt.Columns.Add("Invoice Date");

            dt.Columns.Add("Nopol");
            dt.Columns.Add("Merk - Type");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Cashier");
            //MessageBox.Show(invoices.Count.ToString());
            int total = 0;
            int count = 0;
            List<int> toBeVoid = new List<int>();
            foreach (Invoice item in invoices)
            {
                if (item.isVoid < 2)
                {
                    dt.Rows.Add(new[] { item.invoiceNumber, item.soNumber, Converter.getFormattedTime(item.soDate), Converter.getFormattedTime(item.invoiceDate), item.nopol, item.merk + " - " + item.tipe, Converter.currencyFormat(item.amount), item.employeeName });
                    if (item.isVoid == 1)
                    {
                        toBeVoid.Add(invoices.IndexOf(item));
                    }
                    if (item.isVoid < 1)
                    {
                        total += item.amount;
                        count++;
                    }
                }
            }
            txtTotal.Text = "Rp. " + Converter.currencyFormat(total);
            txtCount.Text = count + "";
            dataGridView1.DataSource = dt;
            
            if (dataGridView1.Columns["ViewDetail"] == null)
            {
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                col.Text = "View Detail";
                col.Name = "ViewDetail";
                dataGridView1.Columns.Add(col);
            }
            String btn = "Void";
            if ((String)Session.getAttribute("position") == "Admin")
            {
                btn = "Delete";
            }
            //MessageBox.Show(btn);
            if (dataGridView1.Columns[btn] == null)
            {
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                if ((String)Session.getAttribute("position") == "Cashier")
                {
                    col.Text = "Void";
                    col.Name = "Void";
                }
                else if ((String)Session.getAttribute("position") == "Admin")
                {
                    col.Text = "Delete";
                    col.Name = "Delete";
                }
                dataGridView1.Columns.Add(col);
            }

            countDetail();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Refresh();
            foreach (int item in toBeVoid)
            {
                dataGridView1.Rows[item].DefaultCellStyle.BackColor = Color.Red;
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetail(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex+"");
            if (e.ColumnIndex == 0)
            {
                showDetail(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            else if (e.ColumnIndex == 1)
            {
                if ((String)Session.getAttribute("position") == "Cashier")
                {
                    if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Red)
                    {
                        if (MessageBox.Show("Are you sure void this transaction ?", "Void Transaction", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            DataConnect dc = new DataConnect();
                            dc.connect();
                            dc.executeUpdate("update TACCSOHeader set isVoid=1 where soNumber='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'");
                            dc.disconnect();
                            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
                if ((String)Session.getAttribute("position") == "Admin")
                {
                    if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Red)
                    {
                        if (MessageBox.Show("Are you sure delete this transaction ?", "Delete Transaction", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            DataConnect dc = new DataConnect();
                            dc.connect();
                            dc.executeUpdate("update TACCSOHeader set isVoid=2 where soNumber='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'");
                            dc.disconnect();
                            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }
        public void countDetail()
        {
            large = 0;
            medium = 0;
            small = 0;
            drinks = 0;
            foreach (Invoice item in invoices)
            {
                if (item.isVoid < 1)
                {
                    foreach (InvoiceDetail itemDetail in item.invoiceDetail)
                    {
                        if (itemDetail.productId == 45)
                        {
                            small++;
                        }
                        else if (itemDetail.productId == 46)
                        {
                            medium++;
                        }
                        else if (itemDetail.productId == 47)
                        {
                            large++;
                        }
                        else if (itemDetail.prodCat == "Food and Beverages")
                        {
                            drinks++;
                        }
                    }
                }
            }
            txtSmall.Text = small + "";
            txtMedium.Text = medium + "";
            txtLarge.Text = large + "";
            txtDrinks.Text = drinks + "";
        }
        public void showDetail(String invoiceNumber)
        {
            foreach (Invoice item in invoices)
            {
                if (item.invoiceNumber == invoiceNumber)
                {
                    Session.setAttribute("invoice", item);
                    ReportDetail rd = new ReportDetail();
                    rd.Show();
                }
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            invoices = Invoice.getTransaction(startDate.Value, endDate.Value, cmbLocation.SelectedValue.ToString());
            showTransaction();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtDrinks_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cafe cf = new Cafe();
            Session.setAttribute("invoicesCafe", invoices);
            
            if (!isWindowExists(cf))
            {
                cf.MdiParent = this.MdiParent;
                cf.Show();
            }
        }
        private bool isWindowExists(Form form){
            Form[] forms = this.MdiChildren;
            foreach (Form row in forms)
            {
                if (row.Name == form.Name)
                {
                    row.BringToFront();
                    return true;
                }
            }
            return false;
        }
    }
}