using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

using SatoCarwashSystem.lib;

namespace SatoCarwashSystem
{
    class DataConnect
    {
        private SqlConnection connection;
        private SqlCommand commandString;

        private DataSet dataset;
        private SqlDataAdapter adapter;
        private BindingSource binding;
        private SqlDataReader datareader;


        public void connect()
        {
            //String con = Connection.dbConnectionString;
            //connection = new SqlConnection(con);
            //connection.Open();
        }
        
        public SqlConnection getConnection()
        {
            return connection;
        }

        //public BindingSource openRecord(String query, String table)
        //{
        //    commandString = new SqlCommand();
        //    commandString.Connection = connection;
        //    commandString.CommandText = query;
        //    dataset = new DataSet();
        //    binding = new BindingSource();
        //    adapter = new SqlDataAdapter(commandString);

        //    try
        //    {
        //        adapter.Fill(dataset, table);
        //        binding.DataSource = dataset;
        //        binding.DataMember = dataset.Tables[table].ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
        //    }
        //    return binding;
        //}

        public DataTable openRecord(String query)
        {
            SqlResult reader = null;
            if (datareader != null)
            {
                datareader.Close();
                datareader = null;
            }
            //commandString = new SqlCommand();
            //commandString.Connection = connection;
            //commandString.CommandText = query;

            if (Connection.socketClient.isConnected)
            {
                Connection.socketClient.sendExecuteQuery(query);
                String message = "";
                do
                {
                    message = Connection.socketClient.readMessage();
                    if (message == "result~end")
                    {
                        reader = new SqlResult(Connection.socketClient.readResult());
                    }
                } while (message != "result~end");
            }
            else
            {
                //reader = new SqlResult(commandString.ExecuteReader());
            }
            DataTable dt = new DataTable();
            foreach (String item in reader.getFieldNames())
            {
                dt.Columns.Add(item);
            }
            while (reader.next())
            {
                Object[] obj=new Object[reader.FieldCount()];
                for (int i = 0; i < reader.FieldCount(); i++)
                {
                    obj[i] = reader.getObject(i);
                }
                dt.Rows.Add(obj);
            }
            return dt;
        }
        public SqlResult executeQuery(String query)
        {
            SqlResult reader = null;
            if (datareader != null)
            {
                datareader.Close();
                datareader = null;
            }
            //commandString = new SqlCommand();
            //commandString.Connection = connection;
            //commandString.CommandText = query;
            //MessageBox.Show("test");
            try
            {
                if (Connection.socketClient.isConnected)
                {
                    Connection.socketClient.sendExecuteQuery(query);
                    String message = "";
                    do
                    {
                        message = Connection.socketClient.readMessage();
                        if (message == "result~end")
                        {
                            reader = new SqlResult(Connection.socketClient.readResult());
                        }
                    } while (message != "result~end");
                }
                else
                {
                    //reader = new SqlResult(commandString.ExecuteReader());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
            }

            return reader;
        }
        //public BindingSource openRecord(String query)
        //{
        //    commandString = new SqlCommand();
        //    commandString.Connection = connection;
        //    commandString.CommandText = query;
        //    dataset = new DataSet();
        //    binding = new BindingSource();
        //    adapter = new SqlDataAdapter(commandString);

        //    try
        //    {
        //        adapter.Fill(dataset);
        //        binding.DataSource = dataset;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error query on ["+query+"]\n"+e.Message);
        //    }
        //        //binding.DataMember = dataset.Tables[table].ToString();
        //    return binding;
        //}
        public void update(String table)
        {
            adapter.Update(dataset, table);
        }
        
        public int executeUpdate(String query)
        {
            
            if (datareader != null)
            {
                datareader.Close();
                datareader = null;
            }
            //commandString = new SqlCommand();
            //commandString.Connection = connection;
            //commandString.CommandText = query;
            
            //MessageBox.Show("test");
            try
            {
                
                int re=0;
                if (Connection.socketClient.isConnected)
                {
                    Connection.socketClient.sendExecuteUpdate(query);
                    String message = "";
                    String[] splitter;
                    do
                    {
                        message = Connection.socketClient.readMessage();
                        splitter = message.Split('|');
                        if (splitter[0] == "affected")
                        {
                            re = Int32.Parse(splitter[1]);
                        }
                    } while (splitter[0] != "affected");
                }
                else
                {
                    //re = commandString.ExecuteNonQuery();
                    //Connection.queryLog.writeLog(query);
                }
                
                return re;
            }
            catch(Exception e)
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
            if (connection != null)
            {
                connection.Close();
            }
        }
        public BindingSource getBinding()
        {
            return binding;
        }
        
    }
}
