using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.master
{
    public partial class MasterFoodBeverages : Form
    {
        private DataConnect db;
        public String query;
        public String tableName;
        public MasterFoodBeverages()
        {
            db = new DataConnect();
            db.connect();

            this.query = "select productCode,product,price,prodCat from TIWProduct"
                        + " join TIWTrProductCategory on TIWTrProductCategory.productId=TIWProduct.productId"
                        + " join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId"
                        + " where TIWProductCategory.prodCatId=3 and locationGroupId=" + Session.locations[0].locationGroupId ;
            this.tableName = "TIWProduct";
            

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

            showData(true);
        }

        private void showData(bool isNew)
        {
            dataGridView1.DataSource = db.openRecord(query);

            if (isNew)
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.Name = "Edit";
                btnEdit.Text = "Edit";

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.Name = "Delete";
                btnDelete.Text = "Delete";

                dataGridView1.Columns.Add(btnEdit);
                dataGridView1.Columns.Add(btnDelete);
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            
            dataGridView1.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFoodBeverages svc = new AddFoodBeverages();
            if (!isWindowExists(svc))
            {
                svc.MdiParent = this.MdiParent;
                svc.Disposed += new EventHandler(svc_Disposed);
                svc.Show();
            }
        }

        private void svc_Disposed(object sender, EventArgs e)
        {
            showData(false);
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
                String productCode = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;

                AddFoodBeverages svc = new AddFoodBeverages(productCode);
                if (!isWindowExists(svc))
                {
                    svc.MdiParent = this.MdiParent;
                    svc.Disposed += new EventHandler(svc_Disposed);
                    svc.Show();
                }
            }
            else if (e.ColumnIndex == 1)
            {
                String productCode = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;

                if (MessageBox.Show("Are you sure delete this product ?", "Delete product", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataConnect dc = new DataConnect();
                    dc.connect();
                    /*
                    String query = "select * from TIWProduct join TIWTrProductCategory on TIWTrProductCategory.productId=TIWProduct.productId join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId where productCode='" + productCode + "'";
                    System.Data.SqlClient.SqlDataReader reader = dc.executeQuery(query);
                    reader.Read();
                    int productId = (int)reader["productId"];
                    */
                    String query = "delete from TIWProduct where productCode='" + productCode + "'";
                    try
                    {
                        dc.executeUpdate(query);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error query on [" + query + "]\n" + ex.Message);
                    }
                    dc.disconnect();
                    showData(false);
                }
            }
        }

        
    }
}
