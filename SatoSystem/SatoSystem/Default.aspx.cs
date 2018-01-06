using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SatoSystem.libraries;
using System.Data.SqlClient;

namespace SatoSystem
{
    public partial class _Default : System.Web.UI.Page
    {
        DataConnect dc = new DataConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            dc.connect();
        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {

            String query = "select * from THRMEmployee"
                        + " join THRMTrEmpPosition on THRMEmployee.employeeId=THRMTREmpPosition.employeeId"
                        + " join THRMEmpPosition on THRMTrEmpPosition.positionId=THRMEmpPosition.positionId"
                        + " join THRMTrEmployeeLocation on THRMTrEmployeeLocation.employeeId=THRMEmployee.employeeId"
                        + " join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId"
                        + " join TCITrLocationGroup on TCILocation.locationId=TCITrLocationGroup.locationId"
                        + " join TCILocationGroup on TCILocationGroup.locationGroupId=TCITrLocationGroup.locationGroupId"
                        + " where firstName='" + Login1.UserName + "'";
            
            if (Login1.UserName != "" && Login1.Password != "")
            {
                SqlDataReader rs = dc.executeQuery(query);
                
                if (rs.Read())
                {
                    String password = (String)rs["pass"];
                    if (Login1.Password == password)
                    {
                        Sessions.setAttribute("employeeId", rs["employeeId"]);
                        Sessions.setAttribute("positionId", rs["positionId"]);
                        Sessions.setAttribute("position", rs["position"]);
                        Sessions.setAttribute("username", rs["firstName"]);

                        Sessions.locations.Add(new Location((int)rs["locationId"], (String)rs["location"], (int)rs["locationGroupId"], (String)rs["locationGroup"]));
                        while (rs.Read())
                        {
                            Sessions.locations.Add(new Location((int)rs["locationId"], (String)rs["location"], (int)rs["locationGroupId"], (String)rs["locationGroup"]));
                        }
                        Server.Transfer("Report.aspx", true);
                    }
                    else
                    {
                        Login1.FailureText = "Wrong Password";
                    }
                }
                else
                {
                    Login1.FailureText = "Username not found";
                }
            }
        }
    }
}
