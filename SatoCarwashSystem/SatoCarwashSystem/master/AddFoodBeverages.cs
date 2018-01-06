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
    public partial class AddFoodBeverages : Form
    {
        int productId;
        bool isEdit=false;

        public AddFoodBeverages()
        {
            InitializeComponent();
            this.Text = "Add Food & Beverages";
        }
        public AddFoodBeverages(String productCode)
        {
            InitializeComponent();
            DataConnect dc = new DataConnect();
            dc.connect();
            String query = "select * from TIWProduct join TIWTrProductCategory on TIWTrProductCategory.productId=TIWProduct.productId join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId where productCode='" + productCode + "'";
            SqlResult result = dc.executeQuery(query);
            result.next();
            this.productId = result.getInt("productId");
            txtProduct.Text = result.getString("product");
            int temp = result.getInt("price");
            txtPrice.Text = temp + ""; 
            //cmbCategory.Text = (String)reader["prodCat"];
            dc.disconnect();
            isEdit = true;
            btnAdd.Text = "Save Change";
            this.Text = "Edit Food & Beverages";
        }
        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
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
            if (txtProduct.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("All field must be filled !");
            }
            else
            {

                int prodCatId = 3;

                SqlResult result;
                DataConnect dc = new DataConnect();
                dc.connect();

                String query = "insert into TIWProduct (product,price,locationGroupId) values('" + txtProduct.Text + "'," + txtPrice.Text + "," + Session.locations[0].locationGroupId + ")";

                if (isEdit)
                {
                    query = "update TIWProduct set product='" + txtProduct.Text + "',price=" + txtPrice.Text + " where productId=" + productId;
                }
                try
                {
                    int resultCount = dc.executeUpdate(query);
                    //MessageBox.Show(result.ToString());
                    if (resultCount > 0)
                    {
                        if (!isEdit)
                        {
                            query = "select top 1 * from TIWProduct where product='" + txtProduct.Text + "' order by productId desc";
                            result = dc.executeQuery(query);
                            result.next();
                            productId = result.getInt("productId");
                        }

                        query = "insert into TIWTrProductCategory (productId,prodCatId) values(" + productId + "," + prodCatId + ")";
                        if (isEdit)
                        {
                            query = "update TIWTrProductCategory set prodCatId=" + prodCatId + " where productId="+productId;
                        }

                        dc.executeUpdate(query);
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error query on ["+query+"]\n"+e.Message);
                }
                dc.disconnect();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
