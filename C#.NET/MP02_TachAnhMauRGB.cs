using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_1._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Tạo biến chứa đường dẫn của hình gốc cần tách 
            string filehinh = @"D:\TaiLieu\XLA\lena_SC.jpg"; // kí tự @ giúp C#.NET hiểu là chuỗi unicode, khong bao loi


            // Tạo một biến chứa hình Bitmap được load từ file hình trên.
            Bitmap hinhgoc = new Bitmap(filehinh);

            // Hiển thị hình trong picBox_hinhgoc đã tạo
            picBox_hinhgoc.Image = hinhgoc;

            // Khai báo 3 hình Bitmap để load các hình R, G, B
            Bitmap red = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap green = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap blue = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            // Mỗi hình là ma trận 2 chiều nên cần 2 dòng for để quét theo 2 chiều 
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    // Doc giá trị pixel tại điểm ảnh có giá trị (x,y)
                    Color pixel = hinhgoc.GetPixel(x, y);

                    // Mỗi pixel gồm 4 thông tin chứa 4 giá trị màu R, G ,B và giá trị màu trong suốt
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;
                    byte A = pixel.A;

                    //Set gía trị pixel đọc được vào các kênh màu
                    red.SetPixel(x, y, Color.FromArgb(A, R, 0, 0));
                    green.SetPixel(x, y, Color.FromArgb(A, 0, G, 0));
                    blue.SetPixel(x, y, Color.FromArgb(A, 0, 0, B));

                }

            // Hiển thị hình trong picBox
            picBox_red.Image = red;
            picBox_green.Image = green;
            picBox_blue.Image = blue;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void picBox_hinhgoc_Click(object sender, EventArgs e)
        {

        }

        private void picBox_red_Click(object sender, EventArgs e)
        {

        }
    }
}
