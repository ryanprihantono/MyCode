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
    public partial class Attendance : Form
    {
        private DataConnect db;
        public String query;
        public String tableName;
        public Attendance()
        {
            db = new DataConnect();
            db.connect();

            this.query = "select employeeCode,firstName, from THRMEmployee"
                        + " left join THRMEmployeeAttendance on THRMEmployee.employeeId=THRMEmployeeAttendance.employeeId"
                        + " left join THRMTrEmployeeLocation on THRMTrEmployeeLocation.employeeId=THRMEmployee.employeeId"
                        + " left join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId"
                        + " where TCILocation.locationId=" + Session.getAttribute("locationId")
                        + " or (day(attDate)=day("+Converter.getTime()+")";

            this.query = "checkAttDate " + Session.getAttribute("locationId") + ",'" + Converter.getDate() + "'";

            InitializeComponent();

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            showData(true, "");
            
        }
        private void showData(bool isNew, String where)
        {
            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
            String temp = this.query;
            if (where != "")
            {
                temp = this.query + " " + where;
            }
            dataGridView1.DataSource = db.openRecord(temp);

            if (isNew)
            {
                DataGridViewButtonColumn btnCheckIn = new DataGridViewButtonColumn();
                btnCheckIn.UseColumnTextForButtonValue = true;
                btnCheckIn.Name = "Clock In";
                btnCheckIn.Text = "Clock In";
                btnCheckIn.UseColumnTextForButtonValue = true;

                DataGridViewButtonColumn btnCheckOut = new DataGridViewButtonColumn();
                btnCheckOut.UseColumnTextForButtonValue = true;
                btnCheckOut.Name = "Clock Out";
                btnCheckOut.Text = "Clock Out";
                btnCheckOut.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Add(btnCheckIn);
                dataGridView1.Columns.Add(btnCheckOut);
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


                dataGridView1.Refresh();
            }
            
        }
            
        private void Attendance_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                DataConnect dc = new DataConnect();
                dc.connect();
                if (e.ColumnIndex == 0)
                {
                    if ((dataGridView1.Rows[e.RowIndex].Cells[7].Value + "") == "null")
                    {
                        dc.executeUpdate("update THRMEmployeeAttendance set checkIn='" + Converter.getTime() + "' where attId=" + dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[7].Value+"");
                    if ((dataGridView1.Rows[e.RowIndex].Cells[8].Value + "") == "null")
                    {
                        
                        dc.executeUpdate("update THRMEmployeeAttendance set checkOut='" + Converter.getTime() + "' where attId=" + dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    }
                }
                dc.disconnect();
            }
            showData(false, "");

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.RowPostPaint -= new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
            changetStyle();
        }
        private void changetStyle()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //MessageBox.Show(dataGridView1.Rows[i].Cells[4].Value + "");
                if ((String)dataGridView1.Rows[i].Cells[7].Value != "null")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.SpringGreen;

                }
                if ((String)dataGridView1.Rows[i].Cells[8].Value != "null")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }
    }
}
