using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace Checking_Card
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            lab_time.Text = DateTime.Now.ToString();
            
        }
        void loading() {
            try
            {
                string url = "https://fb-s-c-a.akamaihd.net/h-ak-fbx/v/t1.0-9/17202876_1281484278599046_7335869793313607597_n.jpg?oh=02160c4ad2d06734ea7c3f56a7c3c0fd&oe=5A124771&__gda__=1516090010_cb2bfc4750e15ff20fb18bc0c07962c0";
                pictureBox1.Load(url);
                // pictureBox1.Image = Image.FromFile("C:\\Users\\Ayman\\Pictures\\Utilman.jpg");
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
            Task.Run(() => { string sp = "Welcome " + labname.Text; sp.Speak(); }).ContinueWith((Taskprev)=> {loading();});
            //t1.Start();
            
           
        }
        bool back = false;
        private void myimageButton1_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            back = true;
            this.Close();
            return;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!back)
            {
                Application.Exit();
                return;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //---------------------------------
        protected override void OnLoad(EventArgs e)
        {
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

        private void txtname_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void myimageButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void txtname_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtname, txtname.Text);
        }

        private void myimageButton1_MouseClick(object sender, MouseEventArgs e)
        {
            Application.OpenForms[0].Show();
            back = true;
            this.Close();
            return;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }
    }
   
}
