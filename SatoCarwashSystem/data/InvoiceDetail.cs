using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.data
{
    class InvoiceDetail:DataConnect
    {
        public int invoiceDetailId;
        public int invoiceId;
        public int productId;
        public String productCode;
        public String product;
        public int itemPrice;
        public int qty;
        public int subtotal;
        public String prodCat;

        public InvoiceDetail(int invoiceId, int productId,String productCode, String product, int itemPrice, int qty)
        {
            this.invoiceId = invoiceId;
            
            this.productId = productId;
            this.productCode = productCode;
            this.product = product;
            this.itemPrice = itemPrice;
            this.qty = qty;
            this.subtotal = itemPrice * qty;
            setCategory();
        }
        public void setCategory()
        {
            this.connect();
            //Console.WriteLine("select * from tiwproduct join tiwproductcategory where tiwproduct.prodcatid=tiwproductcategory.prodcatid where productid=" + productId);
            SqlResult result = this.executeQuery("select * from tiwproduct join tiwtrproductcategory on tiwproduct.productid=tiwtrproductcategory.productid join tiwproductcategory on tiwproductcategory.prodcatid=tiwtrproductcategory.prodcatid where tiwproduct.productid=" + productId);
            
            result.next();
            prodCat = result.getString("prodCat");
            this.disconnect();
        }
        public static List<InvoiceDetail> getDetail(int invoiceId)
        {
            String query = "select * from TACCInvoiceHeader "+
                "join TACCSODetail on TACCInvoiceHeader.soId=TACCSODetail.soId " +
                "join TIWProduct on TACCSODetail.productId=TIWProduct.productId " +
                "where TACCInvoiceHeader.invoiceId=" + invoiceId;
            DataConnect dc = new DataConnect();
            dc.connect();
            SqlResult result = dc.executeQuery(query);
            List<InvoiceDetail> invoiceDetail = new List<InvoiceDetail>();
            while (result.next())
            {
                String product = result.getString("product");
                if (result.getString("remark") != "null")
                {
                    product = result.getString("product") + ":" + result.getString("remark");
                }
                invoiceDetail.Add(new InvoiceDetail(invoiceId,result.getInt("productId"),result.getString("productCode"),product,result.getInt("itemPrice"),result.getInt("qty")));
            }
            dc.disconnect();
            return invoiceDetail;
        }
    }
}
