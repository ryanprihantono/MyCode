﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.data;
using SatoCarwashSystem.lib;


namespace SatoCarwashSystem.cashier
{
    public partial class FoodBeverages : Form
    {
        public FoodBeverages()
        {
            InitializeComponent();
        }

        private void FoodBeverages_Load(object sender, EventArgs e)
        {
            Product cust = new Product();
            dataGridView1.DataSource = cust.foodBeverages();
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.UseColumnTextForButtonValue = true;
            col.Text = "Choose";
            col.Name = "Choose";
            dataGridView1.Columns.Add(col);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex+"");
            List<Object> fromList = new List<Object> { dataGridView1.Rows[e.RowIndex].Cells[5].Value, dataGridView1.Rows[e.RowIndex].Cells[1].Value, dataGridView1.Rows[e.RowIndex].Cells[2].Value, 1 };
            Session.setAttribute("fromFoodList", fromList);
            this.Close();
        }

        
    }
}
