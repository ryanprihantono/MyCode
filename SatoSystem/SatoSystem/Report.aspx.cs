using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using SatoSystem.libraries;

namespace SatoSystem
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            while (cmbLocation.Items.Count > 0)
            {
                cmbLocation.Items.RemoveAt(cmbLocation.Items.Count-1);
                
            }
            foreach (Location location in Sessions.locations)
            {
                cmbLocation.Items.Add(new ListItem(location.location, location.locationId.ToString()));
            }
        }
    }
}
