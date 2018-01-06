using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;


namespace AppServer.lib
{
    class DataConnect
    {
        protected int asdf;
        private SqlConnection connection;
        private SqlCommand commandString;

        private DataSet dataset;
        private SqlDataAdapter adapter;
        private BindingSource binding;
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

        public BindingSource openRecord(String query, String table)
        {
            commandString = new SqlCommand();
            commandString.Connection = connection;
            commandString.CommandText = query;
            dataset = new DataSet();
            binding = new BindingSource();
            adapter = new SqlDataAdapter(commandString);
            try
            {
                adapter.Fill(dataset, table);
                binding.DataSource = dataset;
                binding.DataMember = dataset.Tables[table].ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }
            return binding;
        }
        public BindingSource openRecord(String query)
        {
            commandString = new SqlCommand();
            commandString.Connection = connection;
            commandString.CommandText = query;
            dataset = new DataSet();
            binding = new BindingSource();
            adapter = new SqlDataAdapter(commandString);
            try
            {
                adapter.Fill(dataset);
                binding.DataSource = dataset;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }
            //binding.DataMember = dataset.Tables[table].ToString();
            return binding;
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
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
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
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
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
        public BindingSource getBinding()
        {
            return binding;
        }

    }
}
