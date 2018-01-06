using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.data
{
    class Product:DataConnect
    {
        public Product()
        {
            this.connect();
        }
        public DataTable servicesList()
        {
            String query = "select TIWProduct.product ,TIWProduct.price,TIWProductCategory.prodCat, TIWProduct.productCode,TIWProduct.productId from TIWProduct"
                            + " join TIWTrProductCategory on TIWProduct.productId=TIWTrProductCategory.productId"
                            + " join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId"
                            + " join TCILocationGroup on TCILocationGroup.locationGroupId=TIWProduct.locationGroupId"
                            + " where (prodCat='Wash' or prodCat='Salon') and TCILocationGroup.locationGroupId=" + Session.locations[0].locationGroupId;
            return this.openRecord(query);
        }
        public String getProductCode(int productId)
        {
            //MessageBox.Show(productId+"");
            String query = "select * from TIWProduct where productId=" + productId;
            try
            {
                SqlResult result = this.executeQuery(query);
                result.next();
                return result.getString("productCode");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on ["+query+"]\n"+e.Message);
                return null;
            }
        }
        public DataTable foodBeverages()
        {
            String query = "select TIWProduct.product ,TIWProduct.price,TIWProductCategory.prodCat, TIWProduct.productCode,TIWProduct.productId from TIWProduct"
                            + " join TIWTrProductCategory on TIWProduct.productId=TIWTrProductCategory.productId"
                            + " join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId"
                            + " join TCILocationGroup on TCILocationGroup.locationGroupId=TIWProduct.locationGroupId"
                            + " where TIWProductCategory.prodCatId=3 and TCILocationGroup.locationGroupId=" + Session.locations[0].locationGroupId;
            return this.openRecord(query);
        }
        public DataTable others()
        {
            String query = "select TIWProduct.product ,TIWProduct.price,TIWProductCategory.prodCat, TIWProduct.productCode,TIWProduct.productId from TIWProduct"
                            + " join TIWTrProductCategory on TIWProduct.productId=TIWTrProductCategory.productId"
                            + " join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId"
                            + " join TCILocationGroup on TCILocationGroup.locationGroupId=TIWProduct.locationGroupId"
                            + " where TIWProductCategory.prodCatId=4 and TCILocationGroup.locationGroupId=" + Session.locations[0].locationGroupId;
            return this.openRecord(query);
        }
    }
}
