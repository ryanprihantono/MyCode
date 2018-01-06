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


namespace SatoCarwashSystem.report
{
    public partial class ReportDetail : Form
    {
        Invoice invoice;
        public ReportDetail()
        {
            InitializeComponent();
        }

        private void showDetail()
        {
            invoice = (Invoice)Session.getAttribute("invoice");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt = new DataTable();

            txtAmount.Text = "Rp. "+Converter.currencyFormat(invoice.amount);
            txtCashier.Text = invoice.employeeName;
            txtInvoiceDate.Text = Converter.getFormattedTime(invoice.invoiceDate);
            txtSoDate.Text = Converter.getFormattedTime(invoice.soDate);
            txtInvoiceNumber.Text = invoice.invoiceNumber;
            txtSONumber.Text = invoice.soNumber;
            txtNopol.Text = invoice.nopol;

            dt.Columns.Add("Item");
            dt.Columns.Add("Item ID");
            dt.Columns.Add("Item Price");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Subtotal");
            foreach (InvoiceDetail item in invoice.invoiceDetail)
            {
                dt.Rows.Add(new[] { item.product, item.productCode, Converter.currencyFormat(item.itemPrice), item.qty+"", Converter.currencyFormat(item.subtotal) });
                
            }
            
            dataGridView1.DataSource = dt;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Refresh();
        }

        private void ReportDetail_Load(object sender, EventArgs e)
        {
            showDetail();
        }
    }
}
