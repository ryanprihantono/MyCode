using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SatoCarwashSystem.data;
using SatoCarwashSystem.lib;


namespace SatoCarwashSystem.report
{
    struct Product
    {
        public int productId;
        public String productCode;
        public String product;
        public int price;
    }
    public partial class Cafe : Form
    {
        private DataConnect db;
        public String query;
        public String tableName;
        private List<Invoice> invoices;
        public Cafe()
        {
            InitializeComponent();
            
            //MessageBox.Show(Session.getAttribute("invoicesCafe").ToString());
            
        }
        public void showCafe()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt = new DataTable();

            List<Product> products = new List<Product>();

            DataConnect dc = new DataConnect();
            dc.connect();
            SqlResult result = dc.executeQuery("select * from TIWProduct join TIWTrProductCategory on TIWTrProductCategory.productId=TIWProduct.productId join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId where TIWProductCategory.prodCatId=3");

            while (result.next())
            {
                Product product = new Product();
                product.productId = result.getInt("productId");
                product.product = result.getString("product");
                product.productCode = result.getString("productCode");
                product.price = result.getInt("price");
                products.Add(product);
            }
            dt.Columns.Add("product");
            dt.Columns.Add("productCode");
            dt.Columns.Add("price");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Subtotal");

            //MessageBox.Show(invoices.Count.ToString());
            int total = 0;
            int count = 0;
            foreach (Product item2 in products)
            {
                int qty = 0;
                foreach (Invoice item in invoices)
                {
                    foreach (InvoiceDetail item1 in item.invoiceDetail)
                    {
                        if (item1.productId == item2.productId)
                        {
                            qty++;
                        }
                    }
                }
                int subtotal = item2.price * qty;
                dt.Rows.Add(new[] { item2.product, item2.productCode, Converter.currencyFormat(item2.price), qty + "", Converter.currencyFormat(subtotal) });
            }
            

            dataGridView1.DataSource = dt;


            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Refresh();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            String searchValue = "somestring";
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }
        

        private void Cafe_Load(object sender, EventArgs e)
        {
            this.invoices = (List<Invoice>)Session.getAttribute("invoicesCafe");

            
            
            showCafe();
            Session.removeAttribute("invoicesCafe");
        }
    }
}
          