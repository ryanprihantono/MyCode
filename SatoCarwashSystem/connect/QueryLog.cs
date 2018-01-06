using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.connect
{
    public class QueryLog
    {
        StreamWriter writer;
        StreamReader reader;
        List<String> queries;
        public QueryLog()
        {
            queries = new List<String>();
            
        }
        public void readLog()
        {
            if (!isLogNULL())
            {
                reader = new StreamReader(Directory.GetCurrentDirectory() + "\\querylog.conf");
                do
                {
                    queries.Add(reader.ReadLine());
                } while (reader.Peek() != -1);
                reader.Close();
            }
        }
        public void clearLog()
        {
            writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\querylog.conf");
            writer.AutoFlush = true;
            writer.WriteLine("NULL");
            writer.Close();
        }
        public bool isLogNULL()
        {
            reader = new StreamReader(Directory.GetCurrentDirectory() + "\\querylog.conf");

            String temp = reader.ReadLine();
            if (temp == "NULL")
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        public void writeLog(String query)
        {
            if (isLogNULL())
            {
                writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\querylog.conf");
            }
            else
            {
                writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\querylog.conf", true);
            }
            writer.AutoFlush = true;
            writer.WriteLine(query);
            writer.Close();
        }
    }
}
