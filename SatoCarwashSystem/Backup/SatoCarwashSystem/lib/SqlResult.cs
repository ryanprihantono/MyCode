using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

namespace SatoCarwashSystem.lib
{
    class SqlResult
    {
        private int pointer;
        private List<String> name = null;
        private List<List<String>> records;

        public SqlResult(Object result)
        {
            pointer = -1;
            records = new List<List<string>>();
            if (result is List<String[]>)
            {
                List<String[]> resultTo = (List<String[]>)result;
                
                foreach (String[] item in resultTo)
                {
                    if (name == null)
                    {
                        name = new List<string>();
                        foreach (String item1 in item)
                        {
                            //Console.WriteLine(item1);
                            name.Add(item1);
                        }
                        
                    }
                    else
                    {
                        List<String> list = new List<string>();
                        foreach (String item2 in item)
                        {
                            list.Add(item2);
                        }
                        records.Add(list);
                    }
                }
                
            }
            else if (result is SqlDataReader)
            {
                SqlDataReader reader = (SqlDataReader)result;
                while (reader.Read())
                {
                    if (name == null)
                    {
                        name = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            name.Add(reader.GetName(i));
                        }
                    }
                    List<String> row = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i] + "");
                    }
                    records.Add(row);
                }
            }
        }
        public String getFieldName(int fieldIndex)
        {
            return name[fieldIndex];
        }
        public List<String> getFieldNames()
        {
            return name;
        }
        public bool next()
        {
            pointer++;
            //Console.WriteLine(pointer+":"+records.Count);
            if (pointer < records.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int FieldCount()
        {
            return name.Count;
        }
        public int RowCount()
        {
            return records.Count;
        }
        public int getInt(String fieldName)
        {
            int columnIndex = name.IndexOf(fieldName);
            
            List<String> list = records[pointer];
            //Console.WriteLine(list[columnIndex]);
            return Int32.Parse(list[columnIndex]);
        }
        public String getString(String fieldName)
        {
            int columnIndex = name.IndexOf(fieldName);
            List<String> list = records[pointer];
            return list[columnIndex];
        }
        public int getInt(int fieldIndex)
        {
            List<String> list = records[pointer];
            return Int32.Parse(list[fieldIndex]);
        }
        public String getString(int fieldIndex)
        {
            List<String> list = records[pointer];
            return list[fieldIndex];
        }
        public DateTime getDateTime(String fieldName)
        {
            int columnIndex = name.IndexOf(fieldName);
            List<String> list = records[pointer];
            return DateTime.Parse(list[columnIndex]);
        }
        public DateTime getDateTime(int fieldIndex)
        {
            List<String> list = records[pointer];
            return DateTime.Parse(list[fieldIndex]);
        }
        public Object getObject(String fieldName)
        {
            int columnIndex = name.IndexOf(fieldName);
            List<String> list = records[pointer];
            return list[columnIndex];
        }
        public Object getObject(int fieldIndex)
        {
            List<String> list = records[pointer];
            return list[fieldIndex];
        }
    }
}
