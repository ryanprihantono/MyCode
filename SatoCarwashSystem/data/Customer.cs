using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.data
{
    class Customer:DataConnect
    {
        private SqlResult result;
        public Customer()
        {
            this.connect();
        }
        public bool checkIn(String nopol,String merk,String type)
        {
            int customerId = 0;
            if (!isOnSite(nopol))
            {
                if (!isCusomerExists(nopol))
                {
                    customerId = insertNewCustomer(nopol);
                    String query = "insert into TCCCustomersDetail (customerId,nopol,merk,tipe,counters,isOnSite,isProcess,isFinished,sound,locationId) values((select customerid from tcccustomer where remark='" + nopol + "'),'" + nopol + "','" + merk + "','" + type + "',1,1,0,0,0," + Session.locations[0].locationId + ")";
                    
                    this.executeUpdate(query);

                }
                else
                {
                    customerId = getCustomerId(nopol);
                    int nextCount = lastCount(customerId) + 1;
                    String query = "update TCCCustomersDetail set counters=" + nextCount + ", locationId=" + Session.locations[0].locationId + " , isOnSite=1 where customerId=" + customerId;
                    this.executeUpdate(query);
                }
                this.disconnect();
                return true;
            }
            else
            {
                this.disconnect();
                return false;
            }
        }

        private bool isOnSite(String nopol)
        {
            result = this.executeQuery("select * from TCCCustomersDetail where nopol='" + nopol + "'");
            //MessageBox.Show("select * from TCCCustomersDetail where nopol='" + nopol + "'");
            if (result.next())
            {
                int customerId = result.getInt("customerId");

                if (result.getInt("isOnSite") == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private int lastCount(int customerId)
        {
            result = this.executeQuery("select * from TCCCustomersDetail where customerId=" + customerId);
            result.next();

            return result.getInt("counters");
        }

        private int getCustomerId(String nopol)
        {
            String query = "select * from tcccustomer where remark='" + nopol + "'";
            result = this.executeQuery(query);
            result.next();
            return result.getInt("customerId");
        }
        private int insertNewCustomer(String nopol)
        {
            String query = "insert into tcccustomer (remark) values('"+nopol+"')";
            
            this.executeUpdate(query);

            query = "select * from tcccustomer where remark='"+nopol+"'";
            
            result = this.executeQuery(query);
            result.next();
            return result.getInt("customerId");
        }
        private bool isCusomerExists(String nopol)
        {
            result = this.executeQuery("select * from TCCCustomersDetail where nopol='" + nopol + "'");

            if (result.RowCount() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //get customer list
        public List<CustomerDetail> customerList()
        {
            List<CustomerDetail> customerDetail = new List<CustomerDetail>();

            String query = "select * from TCCCustomer join TCCCustomersDetail on TCCCustomer.customerId=TCCCustomersDetail.customerId where isOnSite=1 and locationId=" + Session.locations[0].locationId;

            SqlResult result = this.executeQuery(query);

            while (result.next())
            {
                customerDetail.Add(new CustomerDetail(result.getInt("customerId"),result.getString("customerCode"),result.getString("nopol"),result.getString("merk"),result.getString("tipe"),result.getInt("counters"),result.getInt("isOnSite"),result.getInt("isProcess"),result.getInt("isFinished"),result.getInt("sound")));
            }
            return customerDetail;
        }
    }
}
