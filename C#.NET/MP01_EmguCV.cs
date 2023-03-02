using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace MP_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Image<Bgr, byte> hinhhienthi = new Image<Bgr, byte>(@"D:\TaiLieu\XLA\lena.jpg");
            imgBox_HinhHienThi.Image = hinhhienthi;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void imgBox_HinhHienThi_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
