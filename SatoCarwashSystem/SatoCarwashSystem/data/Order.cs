using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.data
{
    class Order:DataConnect
    {
        private SqlResult result;
        
        public int soId;
        public String soNumber;
        public DateTime soDate;
        public int employeeId;
        public int customerId = 0;
        public int currencyId;
        public int amount;
        public int isVoid;
        public int isInvoice;

        public List<OrderDetail> orderDetail;

        public Order(String customerCode)
        {
            this.connect();
            this.customerId = getCustomerId(customerCode);
            orderDetail = new List<OrderDetail>();
            initTransaction(customerId);
        }
        private int getCustomerId(String customerCode)
        {
            String query = "select * from TCCCustomer where customerCode='"+customerCode+"'";
            try
            {    
                result = this.executeQuery(query);
                result.next();
                return result.getInt("customerId");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on ["+query+"]\n"+e.StackTrace+"\n"+e.Message);
            }

            return 0;
        }
        private void initTransaction(int customerId)
        {
            String query = "select * from TACCSOHeader where customerId="+customerId+" and isVoid=0 and isInvoice=0";
            this.executeQuery("update TCCCustomersDetail set isProcess=1 where customerId=" + customerId);
            result = this.executeQuery(query);
            try
            {
                if (result.next())
                {
                    soId = result.getInt("soId");
                    soNumber = result.getString("soNumber");
                    soDate = result.getDateTime("soDate");
                    employeeId = result.getInt("employeeId");
                    amount = result.getInt("amount");
                    isVoid = result.getInt("isVoid"); ;
                    isInvoice = result.getInt("isInvoice");
                    syncOrderDetail(soId);
                }
                else
                {
                    soDate = DateTime.Parse(getTime());
                    employeeId = (int)Session.getAttribute("employeeId");
                    amount = 0;
                    isVoid = 0;
                    isInvoice = 0;

                    query = "insert into TACCSOHeader (soDate,employeeId,customerId,amount,isVoid,isInvoice) values('" + getTime(soDate) + "'," + employeeId + "," + customerId + ",0,0,0)";
                    this.executeUpdate(query);

                    query = "select top 1 * from TACCSoHeader where soDate='" + getTime(soDate) + "' and employeeId=" + employeeId + " and customerId=" + customerId;
                    result = this.executeQuery(query);
                    result.next();
                    soId = result.getInt("soId");
                    soNumber = result.getString("soNumber");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }
        }
        public void addSODetail(int productId,String product,int itemPrice,int qty)
        {
            if (!isProductExists(productId, qty))
            {
                this.orderDetail.Add(new OrderDetail(soId, productId, product, itemPrice, qty, false));
            }
        }
        public void removeSODetail(String productCode)
        {
            String query = "select * from TIWProduct where productCode like '%" + productCode + "%'";

            SqlResult result = this.executeQuery(query);
            result.next();
            int productId = result.getInt("productId");

            query = "delete from TACCSODetail where soId="+soId+" and productId="+productId;
            this.executeUpdate(query);

            for (int i = 0; i < orderDetail.Count; i++)
            {
                if (orderDetail[i].productId == productId)
                {
                    orderDetail.RemoveAt(i);
                }
            }
        }
        private bool isProductExists(int productId, int qty)
        {
            foreach (OrderDetail item in orderDetail)
            {
                if (item.productId == productId)
                {
                    item.qty += qty;
                    item.updateQty(item.qty);
                    return true;
                }
            }
            return false;
        }
        public int getTotal()
        {
            int total = 0 ;
            foreach (OrderDetail od in orderDetail)
            {
                total += od.getSubtotal();
            }
            return total;
        }
        private String getTime()
        {
            DateTime dt = DateTime.Now;
            return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        }
        private String getTime(DateTime dt)
        {
            return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        }
        public void syncOrderDetail(int soId)
        {
            String query = "select * from TACCSODetail join TIWProduct on TACCSODetail.productId=TIWProduct.productId where soId="+soId;
            SqlResult result;
            try
            {
                result = this.executeQuery(query);
                String product = "";
                while (result.next())
                {
                    product = result.getString("product");
                    
                    if (result.getString("remark") != "null")
                    {
                        product = result.getString("product") + ":" + result.getString("remark");
                    }
                    this.orderDetail.Add(new OrderDetail(soId, result.getInt("productId"), product, result.getInt("itemPrice"), result.getInt("qty"), true));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }
        }
        public void invoice()
        {
            String query = "update TACCSOHeader set isInvoice=1,amount=" + getTotal() + " where soId=" + soId;
            amount = getTotal();
            try
            {
                this.executeUpdate(query);

                int invoiceId = 0;
                String time = getTime();

                query = "insert into TACCInvoiceHeader (soId,amount,invoiceDate,isPaid) values(" + soId + "," + amount + ",'" + time + "',0)";
                this.executeUpdate(query);

                query = "select * from TACCInvoiceHeader where invoiceDate='" + time + "' and soId=" + soId;
                
                SqlResult result = this.executeQuery(query);
                result.next();
                invoiceId = result.getInt("invoiceId");

                
                foreach(OrderDetail od in orderDetail)
                {
                    query = "insert into TACCInvoiceDetail (invoiceId,productId,itemPrice,qty) values(" + invoiceId + "," + od.productId + "," + od.itemPrice + "," + od.qty + ")";
                    this.executeUpdate(query);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }
        }
        public Boolean voucherPayment(String voucherNumber,String type)
        {
            String voucherFormat = "Voucher(" + voucherNumber + ")";
            foreach (OrderDetail item in orderDetail)
            {
                if (item.product == type)
                {
                    item.product = item.product + ":" + voucherFormat;
                    item.qty -= 1;
                    item.voucherPayment(voucherFormat);
                    return true;
                }
            }
            return false;
        }
    }
}
