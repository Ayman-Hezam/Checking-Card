using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO.Ports;
using System.Speech.Synthesis;
using System.Threading;

namespace Checking_Card
{
    public partial class Form1 : Form
    {
        public static OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\students\\students.accdb");
        public static OleDbCommand cmd;
        public static OleDbDataReader dr;
        // public static DataTable dt;
       public SerialPort serialPort1 = new SerialPort();
        static string[] F_seral()
        {
            string[] port_list = SerialPort.GetPortNames();
            
            return port_list;
        }
       
        public Form1()
        {
            InitializeComponent();
            //this.ShowInTaskbar = false;
            Task Tspeak = new Task(()=> {
                string sp = "Welcome To Checking Card Program ";
                sp.Speak();
                Thread.Sleep(1000);
                sp = "Please Check Card";
                sp.Speak();
            });
            Tspeak.Start();
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.BackColor = Color.Gray;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.Transparent;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
                

                if (i == 0)
                {
                    pictureBox1.Location = new Point(520, 334);
                    i = 1;
                }

                else
                {
                    pictureBox1.Location = new Point(532, 334);
                    i = 0;
                }
            
        }
        private void get_database()
        {

        }
        private void txtid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("SELECT * from students WHERE reg_no LIKE '" + txtid.Text + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    Form2 frm2 = new Form2();
                    frm2.txtreg_no.Text = dr["reg_no"].ToString();
                    frm2.txtname.Text = dr["student_name"].ToString();
                    frm2.txtdep.Text = dr["department"].ToString();
                    frm2.txtlevel.Text = dr["level"].ToString();
                    frm2.labname.Text = dr["student_name"].ToString();
                    cmd = new OleDbCommand("SELECT [Subjects],[Tests],[Exam],[Tot_Marke],[Status] from Marks WHERE reg_no LIKE '" + txtid.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    
                    dt.Load(dr);
                    frm2.dataGridView1.DataSource = new DataGridView().DataSource;
                    frm2.dataGridView1.AutoGenerateColumns = true;
                    frm2.dataGridView1.Refresh();
                    frm2.dataGridView1.DataSource = dt;
                    this.Hide();
                    frm2.Show();
                    //this.timer1.Enabled = false;
                }
                else { MessageBox.Show("Not Found", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DataBase");
            }
            finally
            {

                con.Close();

            }


        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //----------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            //------------------

            //try
            //{

            //    serialPort1.
            //        serialPort1.BaudRate = 9600;
            //        serialPort1.PortName = "COM24";
            //        serialPort1.Open();
            //        while (true)
            //        {
            //            string st = serialPort1.ReadLine();
            //            txtid.Text = st;
            //            //Console.WriteLine(st);
            //            return;

            //        }

                
            //}


            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        
        
    
            //---------------------
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.MouseDown += new MouseEventHandler(AppFormBase_MouseDown);
                this.MouseMove += new MouseEventHandler(AppFormBase_MouseMove);
                this.MouseUp += new MouseEventHandler(AppFormBase_MouseUp);
            }

            base.OnLoad(e);
        }

        void AppFormBase_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            downPoint = new Point(e.X, e.Y);
        }

        void AppFormBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (downPoint == Point.Empty)
            {
                return;
            }
            Point location = new Point(
                this.Left + e.X - downPoint.X,
                this.Top + e.Y - downPoint.Y);
            this.Location = location;
        }

        void AppFormBase_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            downPoint = Point.Empty;
        }

        public Point downPoint = Point.Empty;

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);

        }
        public string state_port = string.Empty;
        public ComboBox comb_port = new ComboBox();
        public Button btPort_con = new Button();
        public Button btPort_decon = new Button();
        public void button1_Click(object sender, EventArgs e)
        {
            Form frm_selectPort = new Form();
            frm_selectPort.WindowState = this.WindowState;
            frm_selectPort.ShowInTaskbar = false;
            frm_selectPort.StartPosition = this.StartPosition;
            frm_selectPort.MaximizeBox = false;
            frm_selectPort.BackgroundImage = this.BackgroundImage;
            frm_selectPort.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm_selectPort.Text = "Checking Card";
            ComboBox comb_port = new ComboBox();
            frm_selectPort.Controls.Add(comb_port);
            comb_port.Location = new Point(90, 50);
            comb_port.FlatStyle = FlatStyle.Flat;
            comb_port.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_port.SelectedIndex = -1;
            //-----
            
            frm_selectPort.Controls.Add(btPort_con);
            btPort_con.Location = new Point(110, 170);
            btPort_con.BackColor = Color.Transparent;
            btPort_con.FlatStyle = FlatStyle.Popup;
            btPort_con.ForeColor= Color.White;
            btPort_con.Text = "Connect";
           
            frm_selectPort.Controls.Add(btPort_decon);
            btPort_decon.Location = new Point(110, 200);
            btPort_decon.BackColor = Color.Transparent;
            btPort_decon.FlatStyle = FlatStyle.Popup;
            btPort_decon.ForeColor = Color.White;
            btPort_decon.Text = "Disconnect";
            //---
            Label lbstate = new Label();
            if (serialPort1.IsOpen)
            {
                lbstate.Text = "Connected";//state_port;
            }
            else lbstate.Text = "Disconnect";//state_port;

            frm_selectPort.Controls.Add(lbstate);
            lbstate.Location=  new Point(120,230 );
            lbstate.ForeColor = Color.OrangeRed;
            lbstate.BackColor = Color.Transparent;
            //----
            btPort_con.Click += new EventHandler(ButtonCliked1);
            btPort_decon.Click += new EventHandler(ButtonCliked2);



            string[] port_list = F_seral();
            if (port_list.Count()!=0)
            {
                foreach (string p in port_list)
                {
                    comb_port.Items.Add(p);
                }
                frm_selectPort.ShowDialog();
            }
            else
                MessageBox.Show("Port Not Found","ERROR");
        }
       private void ButtonCliked1(object sender,EventArgs e)
        {
            try
            {
                //here when click on connect
                if (comb_port.SelectedIndex > -1)
                {
                    serialPort1.BaudRate = 9600;
                    serialPort1.PortName = comb_port.SelectedText;
                    serialPort1.Open();
                    btPort_con.Enabled = false;
                    btPort_decon.Enabled = true;
                }
                else { MessageBox.Show("please select port"); comb_port.Focus(); }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         }
        private void ButtonCliked2(object sender, EventArgs e)
        {
            //here when click on disconnect
            if (serialPort1.IsOpen) {
                serialPort1.Close();
                btPort_con.Enabled = true;
                btPort_decon.Enabled = false;
            }
        }
    }
    
static class Extenstion
    {
        public static void Speak(this string Test)
        {
            new SpeechSynthesizer().Speak(Test);
        }
    }
}
