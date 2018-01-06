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
    public partial class EmployeeList : Form
    {
        private DataConnect db;
        public String query;
        public String tableName;
        public EmployeeList()
        {
            db = new DataConnect();
            db.connect();
            this.query = "select THRMEmployee.employeeId,employeeCode,firstName,lastName,placeOfBirth,dateOfBirth,employeePhone,employeeAddress from THRMEmployee "
            + "left join THRMTrEmployeeLocation on THRMTrEmployeeLocation.employeeId=THRMEmployee.employeeId "
            + "join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId "
            + "left join THRMEmployeesDetail on THRMEmployee.employeeId=THRMEmployeesDetail.employeeId "
            + "where TCILocation.locationId=" + Session.getAttribute("locationId");
            this.tableName = "THRMEmployee";

            InitializeComponent();
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            
        }
        private void showData(bool isNew)
        {
            dataGridView1.DataSource = db.openRecord(query);

            if (isNew)
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.Name = "Edit";
                btnEdit.Text = "Edit";
                

                dataGridView1.Columns.Add(btnEdit);
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((dataGridView1.Rows[i].Cells[6].Value + "") != "null")
                {
                    String date = Converter.getFormattedDate(DateTime.Parse(dataGridView1.Rows[i].Cells[6].Value + ""));
                    dataGridView1.Rows[i].Cells[6].Value = date;
                   
                }
            }
            dataGridView1.Refresh();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddEmployee svc = new AddEmployee();
            if (!isWindowExists(svc))
            {
                svc.MdiParent = this.MdiParent;
                svc.Disposed += new EventHandler(svc_Disposed);
                svc.Show();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEmployee svc = new AddEmployee();
            if (!isWindowExists(svc))
            {
                svc.MdiParent = this.MdiParent;
                svc.Disposed += new EventHandler(svc_Disposed);
                svc.Show();
            }
        }
        void svc_Disposed(object sender, EventArgs e)
        {
            showData(false); ;
        }
        private bool isWindowExists(Form form)
        {
            Form[] forms = this.MdiParent.MdiChildren;
            foreach (Form row in forms)
            {
                if (row.Name == form.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                String employeeCode = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;

                AddEmployee svc = new AddEmployee(employeeCode);
                if (!isWindowExists(svc))
                {
                    svc.MdiParent = this.MdiParent;
                    svc.Disposed += new EventHandler(svc_Disposed);
                    svc.Show();
                }
            }
            else if (e.ColumnIndex == 1)
            {
                String employeeCode = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;

                if (MessageBox.Show("Are you sure delete this service ?", "Delete Service", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataConnect dc = new DataConnect();
                    dc.connect();
                    /*
                    String query = "select * from TIWProduct join TIWTrProductCategory on TIWTrProductCategory.productId=TIWProduct.productId join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategory.prodCatId where productCode='" + productCode + "'";
                    System.Data.SqlClient.SqlDataReader reader = dc.executeQuery(query);
                    reader.Read();
                    int productId = (int)reader["productId"];
                    */
                    String query = "delete from TIWProduct where productCode='" + employeeCode + "'";
                    try
                    {
                        dc.executeUpdate(query);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error query on [" + query + "]\n" + ex.Message);
                    }
                    dc.disconnect();
                    showData(false);
                }
            }
        }

        private void EmployeeList_Load(object sender, EventArgs e)
        {
            showData(true);
            showData(false);
        }


    }
}