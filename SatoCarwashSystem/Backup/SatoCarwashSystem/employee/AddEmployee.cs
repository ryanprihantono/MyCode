using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.lib;

namespace SatoCarwashSystem.employee
{
    public partial class AddEmployee : Form
    {
        string employeeCode;
        int employeeId;
        bool isEdit = false;
        public AddEmployee()
        {
            InitializeComponent();
            this.Text = "Add Employee";
        }
        public AddEmployee(String employeeCode)
        {
            InitializeComponent();
            
            DataConnect dc = new DataConnect();
            dc.connect();
            String query = "select THRMEmployee.employeeId,employeeCode,firstName,lastName,placeOfBirth,dateOfBirth,employeePhone,employeeAddress from THRMEmployee"
                            +" left join THRMEmployeesDetail on THRMEmployeesDetail.employeeId=THRMEmployee.employeeId"
                            +" where employeeCode='" + employeeCode + "'";
            SqlResult result = dc.executeQuery(query);
            result.next();
            this.employeeCode = employeeCode;
            this.employeeId = result.getInt("employeeId");
            txtFirstName.Text = result.getString("firstName");

            txtLastName.Text = result.getString("lastName");
            txtPlaceofBirth.Text = result.getString("placeOfBirth");
            if (result.getString("dateOfBirth")!="null")
            {
                dtpDateofBirth.Value = result.getDateTime("dateOfBirth");
            }
            txtPhone.Text = result.getString("employeePhone");
            txtAddress.Text = result.getString("employeeAddress");
            cmbLocation.Text = Session.getAttribute("location")+"";
            cmbLocation.Enabled = false;
            
            dc.disconnect();
            isEdit = true;
            if (isEdit)
            {
                this.Text = "Edit Employee";
                btnAdd.Text = "Save";
            }
            else
            {
                btnAdd.Text = "Add";
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            submit();
        }
        private void submit()
        {
            if (txtLastName.Text == "" || txtFirstName.Text == "" || txtPlaceofBirth.Text == "" || txtPhone.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("All field must be filled !");
            }
            else
            {
                DataConnect dc = new DataConnect();
                dc.connect();

                String query;// = "insert into TCCCustomersDetail (firstName,lastName) values('" + txtFirstName.Text + "'," + txtLastName.Text + ")";
                String query1 = "";
                String query2 = "";
                query = "insert into THRMEmployee (firstName,lastName)values('" + txtFirstName.Text + "','" + txtLastName.Text + "')";


                if (isEdit)
                {
                    query = "update THRMEmployee set firstName='" + txtFirstName.Text + "',lastName='" + txtLastName.Text + "' where employeeCode='" + employeeCode + "'";
                    query1 = "update THRMEmployeesDetail set  placeOfBirth ='" + txtPlaceofBirth.Text + "', dateOfBirth='" + Converter.getDate(dtpDateofBirth.Value) + "', employeeAddress='" + txtAddress.Text + "',employeePhone='" + txtPhone.Text + "' where employeeId=(select employeeId from THRMEmployee where employeeCode='" + employeeCode + "')";
                }
                else
                {
                    query1 = "insert into THRMEmployeesDetail (employeeId,placeOfBirth,dateOfBirth,employeePhone,employeeAddress) values((select top 1 employeeId from THRMEmployee order by employeeId desc),'" + txtPlaceofBirth.Text + "','" + Converter.getDate(dtpDateofBirth.Value) + "','" + txtPhone.Text + "','" + txtAddress.Text + "')";
                    query2 = "insert into THRMTrEmployeeLocation (employeeId,LocationId) values((select top 1 employeeId from THRMEmployee order by employeeId desc)," + Session.getAttribute("locationId") + ")";
                }


                try
                {
                    dc.executeUpdate(query);
                    dc.executeUpdate(query1);
                    if (!isEdit)
                        dc.executeUpdate(query2);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error query on [" + query + "]\n" + e.Message);
                }
                dc.disconnect();
            }
            this.Close();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            Session.setAttribute("e", employeeCode);
            String a = (String)Session.getAttribute("e");
            Session.removeAttribute("e");
        }
      

        private void txtPlaceofBirth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }

        private void dtpDateofBirth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit();
            }
        }
    }
}
