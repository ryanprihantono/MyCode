using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SatoCarwashSystem.lib;
//using Microsoft.DirectX.AudioVideoPlayback;

namespace SatoCarwashSystem
{
    public partial class WaitingList : Form
    {
        private DataConnect data;

        private List<Color> colors;

        private Timer timer;
        private Timer timer2;
        private int currentSound = -1;
        private List<CustomerDetail> sounds;
       
        //private Video video;

        public WaitingList()
        {
            sounds = new List<CustomerDetail>();

            
            
            timer = new Timer();
            timer.Interval = 25000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            

            InitializeComponent();
            data = new DataConnect();
            data.connect();

            colors = new List<Color>();
            
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 26F, GraphicsUnit.Pixel);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 25F, GraphicsUnit.Pixel);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);

            lblMarquee.Text = "Selamat datang di Carwash Sato 56. Kepuasan Anda adalah prioritas kami. Welcome to the Carwash Sato 56. Your satisfaction is our priority.";
            timer2 = new Timer();
            timer2.Interval = 500;
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Start();

            showWaitingList();
            this.Disposed += new EventHandler(WaitingList_Disposed);

            //video = new Video(System.IO.Directory.GetCurrentDirectory() + "\\video\\2w.wmv");
            //video.Owner = splitContainer2.Panel1;
            
            //video.Ending += new EventHandler(video_Ending);
            
            //video.Play();
            
        }

        void video_Ending(object sender, EventArgs e)
        {
            //video.Stop();
            //video.Play();
        }

        void WaitingList_Disposed(object sender, EventArgs e)
        {
            timer2.Stop();
            timer.Stop();
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            String text = lblMarquee.Text;
            lblMarquee.Text = text.Substring(1, text.Length - 1) + text.Substring(0, 1);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            showWaitingList();
        }

        void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            for (int i = 0; i < colors.Count; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = colors[i];
            }
            colors.RemoveRange(0, colors.Count);
        }
        public void showWaitingList()
        {
            CustomersList list = new CustomersList();

            DataTable dt = new DataTable();

            dt.Columns.Add("NOPOL");
            dt.Columns.Add("TYPE");
            dt.Columns.Add("STATUS");

            foreach (CustomerDetail item in list.customerDetail)
            {
                int idx = list.customerDetail.IndexOf(item);
                String status="";
                if (item.isProcess == 0 && item.isFinished == 0)
                {
                    status = "Waiting";
                    colors.Add(Color.White);
                }
                else if (item.isProcess == 1 && item.isFinished == 0)
                {
                    status = "Process";
                    colors.Add(Color.RoyalBlue);
                }
                else if (item.isProcess == 0 && item.isFinished == 1)
                {
                    status = "Finished";
                    colors.Add(Color.Red);
                    
                    if (currentSound == -1)
                    {
                        currentSound = list.customerDetail.IndexOf(item);
                        lblDisplayNopol.Text = item.nopol;
                    }
                    if (list.customerDetail.IndexOf(item) == currentSound)
                    {
                        if (item.sound < 5)
                        {
                            Sound sound = new Sound();
                            sound.playNopol(item.nopol);
                            item.sound++;
                            updateSound(item.customerId, item.sound);
                        }
                        else
                        {
                            if (currentSound == idx)
                            {
                                currentSound = -1;
                            }
                        }
                    }
                }
                dt.Rows.Add(new[] { item.nopol, item.tipe,  status});
            }
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            rowSize();
        }

        void rowSize()
        {
            
            
            DataGridViewRow row = dataGridView1.RowTemplate;
            row.Height = 50;
            dataGridView1.RowTemplate = row;

            //MessageBox.Show(dataGridView1.RowTemplate.Height + "");
        }

        private void WaitingList_Load(object sender, EventArgs e)
        {
            
        }
        private void updateSound(int customerId,int sound)
        {
            DataConnect dc = new DataConnect();
            dc.connect();
            String query = "update TCCCustomersDetail set sound=" + sound + " where customerId=" + customerId;
            dc.executeUpdate(query);
            dc.disconnect();
        }
    }
    public class CustomersList
    {
        private DataConnect dc;
        public List<CustomerDetail> customerDetail;

        public CustomersList()
        {
            dc = new DataConnect();
            dc.connect();

            customerDetail = new List<CustomerDetail>();
            String query = "select * from TCCCustomer join TCCCustomersDetail on TCCCustomer.customerId=TCCCustomersDetail.customerId where isOnSite = 1";

            SqlResult result = dc.executeQuery(query);
            while (result.next())
            {
                customerDetail.Add(new CustomerDetail(result.getInt("customerId"),result.getString("customerCode"),result.getString("nopol"),result.getString("merk"),result.getString("tipe"),result.getInt("counters"),result.getInt("isOnSite"),result.getInt("isProcess"),result.getInt("isFinished"),result.getInt("sound")));
            }
        }
    }
    public class CustomerDetail
    {

        public int customerId;
        public String customerCode;
        public String fullName;
        public String lastName;
        public String remark;
        public String nopol;
        public String merk;
        public String tipe;
        public int counters;
        public int isOnSite;
        public int isProcess;
        public int isFinished;
        public int sound;

        public CustomerDetail(int customerId,String customerCode,String nopol,String merk,String tipe,int counters,int isOnSite,int isProcess,int isFinished,int sound)
        {
            this.customerId = customerId;
            this.customerCode = customerCode;
            this.nopol = nopol;
            this.merk = merk;
            this.tipe = tipe;
            this.counters = counters;
            this.isOnSite = isOnSite;
            this.isProcess = isProcess;
            this.isFinished = isFinished;
            this.sound = sound;
        }
    }
}
