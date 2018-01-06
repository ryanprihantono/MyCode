using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using Microsoft.PointOfService;
using System.IO;
using System.Globalization;
using SatoCarwashSystem.report;
using SatoCarwashSystem.lib;
using SatoCarwashSystem.communicator;
using SatoCarwashSystem.connect;
using SatoCarwashSystem.customer;
using SatoCarwashSystem.settings;
using SatoCarwashSystem.employee;
using SatoCarwashSystem.robotic;

namespace SatoCarwashSystem
{
    public partial class SatoCarwashSystem : Form
    {
        public SatoCarwashSystem()
        {
            InitializeComponent();
            initCon();
        }
        private void initCon()
        {
            Connection.queryLog = new QueryLog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Connection.opc = new OPCServerPCAccess();
            Connection.opc.connect();

            

            if ((String)Session.getAttribute("position") == "Cashier")
            {
                Cashier ca = new Cashier();

                if (!isWindowExists(ca))
                {
                    ca.MdiParent = this;
                    ca.Show();
                }
            }
            else if ((String)Session.getAttribute("position") == "admin")
            {
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cashierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Connection.opc.cmdWrite("M3.0", "1");
            Cashier ca = new Cashier();
            if (!isWindowExists(ca))
            {
                ca.MdiParent = this;
                ca.Show();
            }
        }
        private bool isWindowExists(Form form){
            Form[] forms = this.MdiChildren;
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

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.MasterServices msSv = new master.MasterServices();
            if (!isWindowExists(msSv))
            {
                msSv.MdiParent = this;
                msSv.Show();
            }
        }

        private void waitingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaitingList wl = new WaitingList();
            if (!isWindowExists(wl))
            {
                wl.Show();
                wl.Disposed += new EventHandler(wl_Disposed);
                this.Visible = false;
            }
        }

        void wl_Disposed(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void foodBeveragesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.MasterFoodBeverages mfb = new master.MasterFoodBeverages();
            if (!isWindowExists(mfb))
            {
                mfb.MdiParent = this;
                mfb.Show();
            }
        }

        private void otherProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.MasterOthers mo = new master.MasterOthers();
            if (!isWindowExists(mo))
            {
                mo.MdiParent = this;
                mo.Show();
            }
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckIn ci = new CheckIn();
            ci.Show();
            ci.Disposed += new EventHandler(ci_Disposed);
            this.Hide();
        }

        void ci_Disposed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void invoicedTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportHeader rh = new ReportHeader();
            if (!isWindowExists(rh))
            {
                rh.MdiParent = this;
                rh.Show();
            }
        }

        private void messengerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessengerForm mf = new MessengerForm();
            if (!isWindowExists(mf))
            {
                mf.MdiParent = this;
                mf.Show();
            }
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer cu = new Customer();
            if (!isWindowExists(cu))
            {
                cu.MdiParent = this;
                cu.Show();
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword cu = new ChangePassword();
            if (!isWindowExists(cu))
            {
                cu.MdiParent = this;
                cu.Show();
            }
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeList el = new EmployeeList();
            if (!isWindowExists(el))
            {
                el.MdiParent = this;
                el.Show();
            }
        }

        private void attendanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Attendance att = new Attendance();
            if (!isWindowExists(att))
            {
                att.MdiParent = this;
                att.Show();
            }
        }
    }
}
