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
using System.Collections.Generic;

namespace SatoSystem.libraries
{
    public static class Sessions
    {
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
            foreach (String row in Sessions.sessionName)
            {
                if (row == sessionName)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                int idx = Sessions.sessionName.IndexOf(sessionName);
                Sessions.sessionVal[idx] = sessionVal;
            }
            else
            {
                Sessions.sessionName.Add(sessionName);
                Sessions.sessionVal.Add(sessionVal);
            }
        }
        public static Object getAttribute(String sessionName)
        {
            foreach (String row in Sessions.sessionName)
            {
                if (row == sessionName)
                {
                    return Sessions.sessionVal[Sessions.sessionName.IndexOf(row)];
                }
            }
            return null;
        }
        public static void removeAttribute(String sessionName)
        {
            for (int i = 0; i < Sessions.sessionName.Count; i++)
            {
                String row = Sessions.sessionName[i];
                if (row == sessionName)
                {
                    Sessions.sessionVal.RemoveAt(i);
                    Sessions.sessionName.RemoveAt(i);
                }
            }
        }
    }
    public class Location
    {
        public int locationId { get; set; }
        public String location { get; set; }
        public int locationGroupId { get; set; }
        public String locationAddress { get; set; }

        public Location(int locationId, String location, int locationGroupId, String locationAddress)
        {
            this.locationId = locationId;
            this.location = location;
            this.locationGroupId = locationGroupId;
            this.locationAddress = locationAddress;
        }
    }
}
