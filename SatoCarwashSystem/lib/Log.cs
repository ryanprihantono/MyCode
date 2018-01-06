using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatoCarwashSystem.lib
{
    public static class Log
    {
        private static String getTime()
        {
            DateTime dt = DateTime.Now;
            return dt.Day + " " + getMonth(dt.Month) + " " + dt.Year + " " + roundTime(dt.Hour) + ":" + roundTime(dt.Minute) + ":" + roundTime(dt.Second);
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
