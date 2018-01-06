using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.IO;

namespace SatoSystem.libraries
{
    class DataConnect
    {
        protected int asdf;
        private SqlConnection connection;
        private SqlCommand commandString;

        private DataSet dataset;
        private SqlDataAdapter adapter;

        private SqlDataReader datareader;


        public void connect()
        {
            String con = "Data Source=HYPERION;Initial Catalog=SatoISDB;Persist Security Info=True;User ID=sa;Password=satolift123!";
            connection = new SqlConnection(con);
            connection.Open();
        }

        public SqlConnection getConnection()
        {
            return connection;
        }

        
        public void update(String table)
        {
            adapter.Update(dataset, table);
        }
        public SqlDataReader executeQuery(String query)
        {
            if (datareader != null)
            {
                datareader.Close();
                datareader = null;
            }
            commandString = new SqlCommand();
            commandString.Connection = connection;
            commandString.CommandText = query;
            //MessageBox.Show("test");
            try
            {
                datareader = commandString.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error query on [" + query + "]\n" + e.Message);
            }
            return datareader;
        }
        public int executeUpdate(String query)
        {

            if (datareader != null)
            {
                datareader.Close();
                datareader = null;
            }
            commandString = new SqlCommand();
            commandString.Connection = connection;
            commandString.CommandText = query;

            //MessageBox.Show("test");
            try
            {
                int re = commandString.ExecuteNonQuery();

                return re;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error query on [" + query + "]\n" + e.Message);
            }

            return -2;
        }

        public void refresh()
        {
            connection.Close();
            connection.Open();
        }
        public void disconnect()
        {
            connection.Close();
        }


    }
}
