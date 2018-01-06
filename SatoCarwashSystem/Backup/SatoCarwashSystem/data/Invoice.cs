using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.data
{
    class Invoice:DataConnect
    {
        
        public int invoiceId;
        public String invoiceNumber;
        public int soId;
        public String soNumber;
        public DateTime soDate;
        public DateTime invoiceDate;
        public int employeeId;
        public String employeeName;
        public int customerId = 0;
        public String nopol;
        public String merk;
        public String tipe;

        public int currencyId;
        public int amount;
        public int isVoid;
        public int isPaid;

        public List<InvoiceDetail> invoiceDetail;

        public Invoice(int invoiceId, String invoiceNumber, int soId, String soNumber, DateTime soDate, DateTime invoiceDate, int employeeId, String employeeName, int customerId, String nopol, int amount, int isPaid,int isVoid)
        {
            this.invoiceId = invoiceId;
            this.invoiceNumber = invoiceNumber;
            this.soId = soId;
            this.soNumber = soNumber;
            this.soDate = soDate;
            this.invoiceDate = invoiceDate;
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.customerId = customerId;
            this.nopol = nopol;
            this.isVoid = isVoid;
            
            DataConnect dc = new DataConnect();
            dc.connect();
            SqlResult result = dc.executeQuery("select * from tcccustomersdetail where customerid="+customerId);
            result.next();
            merk = result.getString("merk");
            tipe = result.getString("tipe");
            dc.disconnect();
            this.amount = amount;

            this.isPaid = isPaid;
            invoiceDetail = InvoiceDetail.getDetail(invoiceId);
        }
        public static List<Invoice> getTransaction(DateTime startDate,DateTime endDate,String locationId)
        {
            //MessageBox.Show(locationId);
            String where = "where invoiceDate>'" + Converter.getDate(startDate) + "' and invoiceDate<'" + Converter.getDate(endDate.AddDays(1)) + "' ";
            if (Converter.getDate(startDate) == Converter.getDate(endDate))
            {
                where = "where YEAR(invoiceDate)=" + startDate.Year + 
                    " AND MONTH(invoiceDate)="+startDate.Month+
                    " AND DAY(invoiceDate)="+startDate.Day;
            }
            String query = "select "+
                "invoiceId,invoiceNumber,TACCSOHeader.soId,soNumber,soDate,invoiceDate,THRMEmployee.employeeId,firstName,TCCCustomersDetail.customerId,nopol,TACCSOHeader.isVoid,isPaid,merk,tipe,TACCSOHeader.amount " +
                "from TACCInvoiceHeader " +
                "JOIN TACCSOHeader ON TACCSOHeader.soId=TACCInvoiceHeader.soId " +
                "JOIN TCCCustomersDetail ON TCCCustomersDetail.customerId=TACCSOHeader.customerId " +
                "join THRMEmployee on TACCSOHeader.employeeId=THRMEmployee.employeeId " +
                "join THRMTrEmployeeLocation on THRMTrEmployeeLocation.employeeId=THRMEMployee.employeeId " +
                "join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId " +
                where +
                " and TACCInvoiceHeader.isVoid is NULL and TCILocation.locationId=" + locationId;
            //Console.WriteLine(query);
            DataConnect dc = new DataConnect();
            dc.connect(); 
            
            SqlResult result = dc.executeQuery(query);
            List<Invoice> invoices = new List<Invoice>();
            while (result.next())
            {
                invoices.Add(new Invoice(result.getInt("invoiceId"), result.getString("invoiceNumber"), result.getInt("soId"), result.getString("soNumber"), result.getDateTime("soDate"), result.getDateTime("invoiceDate"), result.getInt("employeeId"), result.getString("firstName"), result.getInt("customerId"), result.getString("nopol"), result.getInt("amount"), result.getInt("isPaid"), result.getInt("isVoid")));
            }
            dc.disconnect();
            return invoices;
        }

    }
}
