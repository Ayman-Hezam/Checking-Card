﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checking_Card
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i== 0)
                {
                    pictureBox1.Location = new Point(196,123);
                i = 1;
                }
            
                else
                {
                    pictureBox1.Location = new Point(196,138);
                i = 0;
                }
        }
    }
}