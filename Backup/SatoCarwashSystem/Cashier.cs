using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoCarwashSystem.cashier;
using SatoCarwashSystem.lib;
using SatoCarwashSystem.data;
using Microsoft.PointOfService;


namespace SatoCarwashSystem
{
    public partial class Cashier : Form
    {
        private Timer timer = new Timer();
        private Order order = null;
        private int total = 0;
        private Product product;
        private bool flag = true;
        private Printer printer = new Printer();

        public Cashier()
        {
            product = new Product();
            InitializeComponent();
            this.Disposed += new EventHandler(Cashier_Disposed);
        }

        void Cashier_Disposed(object sender, EventArgs e)
        {
            printer.unInit();
        }
        private void initButton(bool stat)
        {
            btnServices.Enabled = stat;
            btnFoodAndBeverages.Enabled = stat;
            btnOthers.Enabled = stat;
            btnVoucherList.Enabled = stat;
            btnPayment.Enabled = stat;
            btnVoucherPayment.Enabled = stat;
            //btnVoid.Enabled = stat;
        }
        private void Cashier_Load(object sender, EventArgs e)
        {

            btnVoucherList.Visible = false;
            btnVoucherPayment.Visible = false;

            if (Session.locations[0].locationGroupId == 1)
            {
                btnVoucherList.Visible = true;
                btnVoucherPayment.Visible = true;
            }
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
            lblOp.Text = (String)Session.getAttribute("username");
            initButton(false);
            
            lblCustomer.Text = "-";
            lblCustomerCode.Text = "-";

            gridDetail.DefaultCellStyle.Font = new Font("Arial", 20F, GraphicsUnit.Pixel);
            gridDetail.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 20F, GraphicsUnit.Pixel);
            gridDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridDetail.CellContentClick += new DataGridViewCellEventHandler(gridDetail_CellContentClick);

            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Cashier_KeyDown);
        }

        void Cashier_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("asdf");
            if (e.KeyCode == Keys.C)
            {
                btnCustomers.PerformClick();
            }
            else if (e.KeyCode == Keys.S)
            {
                btnServices.PerformClick();
            }
            else if (e.KeyCode == Keys.F)
            {
                btnFoodAndBeverages.PerformClick();
            }
            else if (e.KeyCode == Keys.P)
            {
                btnPayment.PerformClick();
            }
            else if (e.KeyCode == Keys.O)
            {
                btnOthers.PerformClick();
            }
        }

        void gridDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex+"-"+flag);
            if (flag)
            {
                if (e.ColumnIndex == 5)
                {
                    //MessageBox.Show(e.ColumnIndex + "");
                    //MessageBox.Show(gridDetail.Rows[e.RowIndex].Cells[1].Value.ToString());
                    order.removeSODetail((String)gridDetail.Rows[e.RowIndex].Cells[1].Value);
                    showOrders();
                }
            }
            else
            {
                if (e.ColumnIndex == 0)
                {
                    order.removeSODetail((String)gridDetail.Rows[e.RowIndex].Cells[2].Value);
                    showOrders();
                }
            }
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = Converter.getFormattedTime();
        }

        

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            CustomerList cl = new CustomerList();
            if (!isWindowExists(cl))
            {
                cl.MdiParent = this.MdiParent;
                cl.Disposed += new EventHandler(cl_Disposed);
                cl.Show();
            }
        }

        private void cl_Disposed(object sender, EventArgs e)
        {
            List<Object> fromList = (List<Object>)Session.getAttribute("fromCustomerList");
            //Session.removeAttribute("fromCustomerList");
            if (fromList != null)
            {
                gridDetail.Enabled = true;
                lblCustomerCode.Text = (String)fromList[1];
                lblCustomer.Text = (String)fromList[0];
                lblTipe.Text = (String)fromList[2];
                initButton(true);
                order = new Order(lblCustomerCode.Text);
                showOrders();
                
                Session.removeAttribute("fromCustomerList");
                lblCash.Text = "Rp. 0";
                lblChange.Text = "Rp. 0";
            }
        }
        private bool isWindowExists(Form form)
        {
            Form[] forms = this.MdiParent.MdiChildren;
            foreach (Form row in forms)
            {
                if (row.Name == form.Name)
                {
                    row.BringToFront();
                    return true;
                }
            }
            return false;
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            Services sv = new Services();
            if (!isWindowExists(sv))
            {
                sv.MdiParent = this.MdiParent;
                sv.Disposed += new EventHandler(sv_Disposed);
                sv.Show();
            }           
        }

        private void sv_Disposed(object sender, EventArgs e)
        {
            List<Object> fromList = (List<Object>)Session.getAttribute("fromServicesList");
            //Session.removeAttribute("fromServicesList");
            if (fromList != null)
            {
                addOrder(fromList);
                Session.removeAttribute("fromServicesList");
            }
        }
        private void showOrders()
        {
            if (order == null)
            {
                order = new Order(lblCustomerCode.Text);
            }
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Item");
            dt.Columns.Add("Item ID");
            dt.Columns.Add("Price");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Subtotal");

            foreach (OrderDetail item in order.orderDetail)
            {
                dt.Rows.Add(new[] { item.product,product.getProductCode(item.productId), Converter.currencyFormat(item.itemPrice), item.qty+"", Converter.currencyFormat(item.getSubtotal()) });
            }
            gridDetail.DataSource = dt;
            //BindingList<OrderDetail> bindingList = new BindingList<OrderDetail>();

            //foreach (OrderDetail item in order.orderDetail)
            //{
            //    bindingList.Add(item);
            //}
            //gridDetail.DataSource = bindingList;
            
            total = order.getTotal();
            lblTotal.Text = "Rp. " + Converter.currencyFormat(order.getTotal());

            if (gridDetail.Columns["Action"] == null)
            {
                gridDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gridDetail.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                gridDetail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridDetail.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gridDetail.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                col.Text = "Remove";
                col.Name = "Action";
                gridDetail.Columns.Add(col);
            }
        }
        private void addOrder(List<Object> list)
        {
            flag = false;
            if (order == null)
            {
                order = new Order(lblCustomerCode.Text);
            }
            //System.Threading.Thread.Sleep(1000);
            //MessageBox.Show(list[0] + "-" + list[1] + "-" + list[2] + "-" + list[3]);
            order.addSODetail(Int32.Parse(list[0] + ""), (String)list[1], Int32.Parse(list[2] + ""), Int32.Parse(list[3] + ""));
            
            //System.Threading.Thread.Sleep(1000);
            showOrders();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Payment py = new Payment(total,order.soId);
            if (!isWindowExists(py))
            {
                py.MdiParent = this.MdiParent;
                py.Show();
                py.Disposed += new EventHandler(py_Disposed);
            }
        }

        private void py_Disposed(object sender, EventArgs e)
        {
            if (Session.getAttribute("cash") != null)
            {
                if (printer.flag)
                {
                    initButton(false);
                    int cash = (int)Session.getAttribute("cash");
                    lblCash.Text = "Rp. " + Converter.currencyFormat(cash);
                    lblChange.Text = "Rp. " + Converter.currencyFormat((cash - total));
                    order.invoice();
                    Session.removeAttribute("cash");

                    DataConnect dc = new DataConnect();
                    SqlResult result = dc.executeQuery("select * from TCCCustomer where customerCode=" + lblCustomerCode.Text);
                    if (result.next())
                    {
                        printer.print(order, cash, lblOp.Text, lblCustomer.Text, result.getString("firstName"), result.getString("telp"));
                    }
                    
                    String query = "update TCCCustomersDetail set isOnSite=0,isProcess=0,isFinished=0,sound=0 where nopol='" + lblCustomer.Text + "'";
                    dc.executeUpdate(query);
                    dc.disconnect();
                    gridDetail.Enabled = false;
                }
            }
        }

        private void btnFoodAndBeverages_Click(object sender, EventArgs e)
        {
            FoodBeverages fb = new FoodBeverages();
            if (!isWindowExists(fb))
            {
                fb.MdiParent = this.MdiParent;
                fb.Disposed += new EventHandler(fb_Disposed);
                fb.Show();
            }
        }

        void fb_Disposed(object sender, EventArgs e)
        {
            List<Object> fromList = (List<Object>)Session.getAttribute("fromFoodList");
            //Session.removeAttribute("fromServicesList");
            if (fromList != null)
            {
                addOrder(fromList);
                Session.removeAttribute("fromFoodList");
            }
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {
            Others ot = new Others();
            if (!isWindowExists(ot))
            {
                ot.MdiParent = this.MdiParent;
                ot.Disposed += new EventHandler(ot_Disposed);
                ot.Show();
            }     
        }

        void ot_Disposed(object sender, EventArgs e)
        {
            List<Object> fromList = (List<Object>)Session.getAttribute("fromOtherList");
            if (fromList != null)
            {
                addOrder(fromList);
                Session.removeAttribute("fromOtherList");
            }
        }

        private void btnVoucherList_Click(object sender, EventArgs e)
        {
            Voucher vo = new Voucher();
            if (!isWindowExists(vo))
            {
                vo.MdiParent = this.MdiParent;
                vo.Disposed += new EventHandler(vo_Disposed);
                vo.Show();
            }  
        }

        void vo_Disposed(object sender, EventArgs e)
        {
            List<Object> fromList = (List<Object>)Session.getAttribute("fromVoucherList");
            if (fromList != null)
            {
                addOrder(fromList);
                Session.removeAttribute("fromVoucherList");
            }
        }

        private void btnVoucherPayment_Click(object sender, EventArgs e)
        {
            VoucherPayment vp = new VoucherPayment();
            if (!isWindowExists(vp))
            {
                vp.MdiParent = this.MdiParent;
                vp.Disposed += new EventHandler(vp_Disposed);
                vp.Show();
            }  
        }

        void vp_Disposed(object sender, EventArgs e)
        {
            List<String> fromList = (List<String>)Session.getAttribute("voucher");
            if (fromList != null)
            {
                if (order.voucherPayment(fromList[0], fromList[1]))
                {
                    showOrders();
                }
                else
                {
                    MessageBox.Show("Please choose " + fromList[1] + " service first");
                }
                Session.removeAttribute("voucher");
            }
        }
    }
}
