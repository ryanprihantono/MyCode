using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.data
{
    class OrderDetail:DataConnect
    {
        public int soId;
        public int productId;
        public String product;
        public int itemPrice;
        public int qty;

        public OrderDetail(int soId, int productId, String product, int itemPrice, int qty, bool sync)
        {
            if (sync)
            {
                this.connect();
                this.soId = soId;
                this.productId = productId;
                this.product = product;
                this.itemPrice = itemPrice;
                this.qty = qty;
            }
            else
            {
                this.connect();
                this.soId = soId;
                this.productId = productId;
                this.product = product;
                this.itemPrice = itemPrice;
                this.qty = qty;

                String query = "select * from TACCSOHeader where soId=" + soId;

                String query2 = "insert into TACCSODetail (soId,productId,itemPrice,qty) values(" + soId + "," + productId + "," + itemPrice + "," + qty + ")";

                if (product.IndexOf(":") != -1)
                {
                    String[] splitter = product.Split(':');
                    query2 = "insert into TACCSODetail (soId,productId,itemPrice,qty,remark) values(" + soId + "," + productId + "," + itemPrice + "," + qty + ",'" + splitter[1] + "')";
                }

                try
                {
                    int rowNum = 0;
                    SqlResult result = null;
                    while (rowNum == 0)
                    {
                        result = this.executeQuery(query);
                        if (result.RowCount() > 0)
                        {
                            rowNum = 1;
                        }
                    }
                    this.executeUpdate(query2);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error query on [" + query2 + "]\n" + e.StackTrace + "\n" + e.Message);
                }
            }
        }
        public void updateQty(int qty)
        {
            this.qty = qty;

            String query = "update TACCSODetail set qty="+qty+" where productId="+productId+" and soId="+soId;
            //MessageBox.Show(query);
            try
            {
                this.executeUpdate(query);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.StackTrace + "\n" + e.Message);
            }
        }
        public int getSubtotal()
        {
            return itemPrice * qty;
        }
        public void voucherPayment(String remark)
        {

            String query = "update TACCSODetail set remark='" + remark + "',qty=" + qty + " where productId=" + productId + " and soId=" + soId;
            //MessageBox.Show(query);
            try
            {
                this.executeUpdate(query);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.StackTrace + "\n" + e.Message);
            }
        }
    }
}
