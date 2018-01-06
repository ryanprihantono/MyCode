using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SatoCarwashSystem.lib;
using SatoCarwashSystem.connect;

namespace SatoCarwashSystem.data
{
    class Employee : DataConnect
    {
        public Employee()
        {
            this.connect();
        }
        private static List<int> getLocations()
        {
            String query = "select TCILocation.locationId from THRMEmployee"
                           + " join THRMTrEmployeeLocation on THRMEmployee.employeeId=THRMTrEmployeeLocation.employeeId"
                           + " join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId"
                           + " join TCITrLocationGroup on TCITrLocationGroup.locationId=TCILocation.locationId"
                           + " where TCILocation.locationId=" + Session.locations[0].location;
            DataConnect dc = new DataConnect();
            dc.connect();
            SqlResult result = dc.executeQuery(query);
            List<int> temp = new List<int>();
            while (result.next())
            {
                temp.Add(result.getInt("locationId"));
            }
            return temp;
        }
        public static bool loginQuery(String empCode, String password)
        {
            //empCode = empCode.ToUpper();
            //password = password.ToUpper();
            String query = "select * from THRMEmployee"
                            + " join THRMTrEmpPosition on THRMEmployee.employeeId=THRMTrEmpPosition.employeeId"
                            + " join THRMEmpPosition on THRMTrEmpPosition.positionId=THRMEmpPosition.positionId"
                            + " join THRMTrEmployeeLocation on THRMTrEmployeeLocation.employeeId=THRMEmployee.employeeId"
                            + " join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId"
                            + " join TCITrLocationGroup on TCILocation.locationId=TCITrLocationGroup.locationId"
                            + " join TCILocationGroup on TCILocationGroup.locationGroupId=TCITrLocationGroup.locationGroupId"
                            + " where firstName='" + empCode + "'";
            //MessageBox.Show(query);
            //Console.WriteLine(query);
            SqlResult result;

            if (empCode == "admin")
            {
                if (password == "woxihuanni")
                {
                    Session.setAttribute("employeeId", "000");
                    Session.setAttribute("position", "admin");
                    Session.setAttribute("username", "admin");
                    
                    return true;
                }
            }
            else
            {
                try
                {
                    DataConnect dc = new DataConnect();
                    dc.connect();
                    result = dc.executeQuery(query);
                    
                    if (result.next())
                    {
                        //Console.WriteLine(result.getString("firstName"));
                        if (result.getString("firstName") == empCode)
                        {
                            if (result.getString("pass") == password)
                            {
                                //MessageBox.Show("Sukses");
                                
                                List<int> locationGroupId = new List<int>();
                                Session.setAttribute("employeeId", result.getInt("employeeId"));
                                Session.setAttribute("positionId",result.getInt("positionId"));
                                Session.setAttribute("position", result.getString("position"));
                                Session.setAttribute("username", result.getString("firstName"));
                                
                                Session.locations.Add(new Location(result.getInt("locationId"), result.getString("location"), result.getInt("locationGroupId"), result.getString("locationAddress")));
                                
                                if (result.RowCount() > 1)
                                {
                                    while (result.next())
                                    {
                                        Session.locations.Add(new Location(result.getInt("locationId"), result.getString("location"), result.getInt("locationGroupId"), result.getString("locationAddress")));
                                    }
                                }

                                dc.disconnect();
                                return true;
                            }
                            else
                                MessageBox.Show("Wrong Password");
                        }
                        else
                            MessageBox.Show("Account is not registered");
                    }
                    else
                        MessageBox.Show("User Not Found");
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error Query\n"+e.Message+"\n"+e.StackTrace);
                }
            }
            return false;
        }
    }
}
