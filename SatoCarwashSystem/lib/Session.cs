using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace SatoCarwashSystem.lib
{
    public static class Session
    {

        public static DataGridViewRow Row = null;

        static int _globalValue;

        public static int GlobalValue
        {
            get
            {
                return _globalValue;
            }
            set
            {
                _globalValue = value;
            }
        }

        public static bool GlobalBoolean;

        private static List<Object> sessionVal = new List<Object>();
        private static List<String> sessionName = new List<String>();
        public static List<Location> locations = new List<Location>();

        public static void setAttribute(String sessionName, Object sessionVal)
        {
            bool flag = false;
            foreach (String row in Session.sessionName)
            {
                if (row == sessionName)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                int idx = Session.sessionName.IndexOf(sessionName);
                Session.sessionVal[idx] = sessionVal;
            }
            else
            {
                Session.sessionName.Add(sessionName);
                Session.sessionVal.Add(sessionVal);
            }
        }
        public static Object getAttribute(String sessionName)
        {
            foreach (String row in Session.sessionName)
            {
                if (row == sessionName)
                {
                    return Session.sessionVal[Session.sessionName.IndexOf(row)];
                }
            }
            return null;
        }
        public static void removeAttribute(String sessionName)
        {
            for (int i = 0; i < Session.sessionName.Count;i++ )
            {
                String row = Session.sessionName[i];
                if (row == sessionName)
                {
                    Session.sessionVal.RemoveAt(i);
                    Session.sessionName.RemoveAt(i);
                }
            }
        }
        public static bool isIntInList(String sessionName, int obj)
        {
            foreach (String row in Session.sessionName)
            {
                if (row == sessionName)
                {
                    List<int> temp = (List<int>) Session.sessionVal[Session.sessionName.IndexOf(row)];
                    foreach (int item in temp)
                    {
                        if (item == obj)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static String whereLocation()
        {
            String temp = "";
            for (int i = 0; i < locations.Count; i++)
            {
                temp += " (TCILocation.locationId=" + locations[i].locationId + " ";
                if (i < locations.Count - 2)
                {
                    temp += "or";
                }
                if (i == locations.Count - 1)
                {
                    temp += ")";
                }
            }
            return temp;
        }
    }
    public class Location
    {
        public int locationId { get; set; }
        public String location { get; set; }
        public int locationGroupId { get; set; }
        public String locationAddress { get; set; }
        
        public Location(int locationId,String location,int locationGroupId,String locationAddress)
        {
            this.locationId = locationId;
            this.location = location;
            this.locationGroupId = locationGroupId;
            this.locationAddress = locationAddress;
        }
    }
}
