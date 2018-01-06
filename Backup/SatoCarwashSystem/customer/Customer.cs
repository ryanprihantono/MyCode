using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SatoCarwashSystem.customer
{
    public partial class Customer : Form
    {
        private DataConnect db;
        public String query;
        public String tableName;
        public Customer()
        {
            db = new DataConnect();
            db.connect();

            this.query = "select nopol, merk,tipe,firstName,lastName from (TCCCustomer join TCCCustomersDetail on TCCCustomer.customerId=TCCCustomersDetail.customerId)";
            this.tableName = "TCCCustomer";


            /*example product
             * select productCode,product,price,prodCat from (TIWProduct 
             * join TIWTrProductCategory on TIWTrProductCategory.productId=TIWProduct.productId 
             * join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId) 
             * where TIWProductCategory.prodCatId=1 or TIWProductCategory.prodCatId=2
             * 
             * TIWProduct //tableName
            */
            InitializeComponent();

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            showData(true, "");
        }

        private void showData(bool isNew, String where)
        {
            //this.query = "select nopol, merk,tipe,firstName,lastName from (TCCCustomer join TCCCustomersDetail on TCCCustomer.customerId=TCCCustomersDetail.customerId)";
            String temp = this.query;
            if (where != "")
            {
                temp = this.query+" " + where;
            }
            dataGridView1.DataSource = db.openRecord(temp);

            if (isNew)
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.Name = "Edit";
                btnEdit.Text = "Edit";

                dataGridView1.Columns.Add(btnEdit);
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


                dataGridView1.Refresh();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditCustomer svc = new EditCustomer();
            if (!isWindowExists(svc))
            {
                svc.MdiParent = this.MdiParent;
                svc.Disposed += new EventHandler(svc_Disposed);
                svc.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // svc = new ();
            //if (!isWindowExists(svc))
            //{
            //    svc.MdiParent = this.MdiParent;
            //    svc.Disposed += new EventHandler(svc_Disposed);
            //    svc.Show();
            //}
        }

        void svc_Disposed(object sender, EventArgs e)
        {
            showData(false, "");
        }
        private bool isWindowExists(Form form)
        {
            Form[] forms = this.MdiParent.MdiChildren;
            foreach (Form row in forms)
            {
                if (row.Name == form.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString());
            if (e.ColumnIndex == 0)
            {
                String nopol = (String)dataGridView1.Rows[e.RowIndex].Cells[1].Value;

                EditCustomer svc = new EditCustomer(nopol);
                if (!isWindowExists(svc))
                {
                    svc.MdiParent = this.MdiParent;
                    svc.Disposed += new EventHandler(svc_Disposed);
                    svc.Show();
                }
            }

            showData(false, "");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            search(Search.Text);
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search(Search.Text);
            }
        }
        private void search(String keyword)
        {
            if (Search.Text == "")
            {
                showData(false, "");
            }
            else
            {
                showData(false, "where customerCode like '%" + keyword + "%' or nopol like '%" + keyword + "%' or firstName like '%" + keyword + "%' or lastName like '%" + keyword + "%' or merk like '%" + keyword + "%' or tipe like '%" + keyword + "%'");
            }
        }

    }
}
                                                     
