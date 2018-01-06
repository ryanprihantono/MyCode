using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SatoCarwashSystem.lib;
using System.Diagnostics;

namespace SatoCarwashSystem
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Process updaterProcess = new Process();
            updaterProcess.StartInfo.FileName = "Updater.exe";
            updaterProcess.EnableRaisingEvents = true;
            updaterProcess.Start();
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            
        }

    }
    public static class Converter
    {
        public static String currencyFormat(int value)
        {
            String valueString = value.ToString();
            
            int len = valueString.Length / 3;

            if (valueString.Length % 3 == 0)
            {
                len -= 1;
            }

            for (int i = 0; i < len; i++)
            {
                valueString = valueString.Insert(valueString.Length - (((i+1) * 3)+i), ",");
                
            }
            return valueString;
        }
        public static String getTime()
        {
            DateTime dt = DateTime.Now;
            return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        }
        public static String getTime(DateTime dt)
        {
            return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        }
        public static String getDate()
        {
            DateTime dt = DateTime.Now;
            return dt.Year + "-" + roundTime(dt.Month) + "-" + roundTime(dt.Day);
        }
        public static String getDate(DateTime dt)
        {
            return dt.Year + "-" + roundTime(dt.Month) + "-" + roundTime(dt.Day);
        }
        public static String getFormattedDate()
        {
            DateTime dt = DateTime.Now;
            return dt.Day + " " + getMonth(dt.Month - 1) + " " + dt.Year;
        }
        public static String getFormattedDate(DateTime dt)
        {
            return dt.Day + " " + getMonth(dt.Month - 1) + " " + dt.Year;
        }
        public static String getFormattedTime()
        {
            DateTime dt = DateTime.Now;
            return dt.Day + " " + getMonth(dt.Month - 1) + " " + dt.Year + " " + roundTime(dt.Hour) + ":" + roundTime(dt.Minute) + ":" + roundTime(dt.Second);
        }
        public static String getFormattedTime(DateTime dt)
        {
            return dt.Day + " " + getMonth(dt.Month - 1) + " " + dt.Year + " " + roundTime(dt.Hour) + ":" + roundTime(dt.Minute) + ":" + roundTime(dt.Second);
        }
        private static String roundTime(int time)
        {
            String timeString;
            if (time < 10)
            {
                timeString = "0" + time;
            }
            else
            {
                timeString = "" + time;
            }
            return timeString;
        }
        private static String getMonth(int month)
        {
            String[] monthArr = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return monthArr[month];
        }
    }
}
