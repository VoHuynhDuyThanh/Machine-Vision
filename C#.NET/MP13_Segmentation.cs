using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP13_Segmentation
{
    public partial class Form1 : Form
    {
        Bitmap hinhgoc;//Chuyển thành biến toàn cục(global) để sử dụng cho nhiều hàm khác
        public Form1()
        {
            InitializeComponent();
            // Tạo biến chứa đường dẫn của hình gốc cần tách 
            string filehinh = @"D:\TaiLieu\XLA\lena.jpg"; // kí tự @ giúp C#.NET hiểu là chuỗi unicode, khong bao loi

            // Tạo một biến chứa hình Bitmap được load từ file hình trên.
            hinhgoc = new Bitmap(filehinh);

            // Hiển thị hình trong picBox_hinhgoc đã tạo
            picBox_hinhgoc.Image = hinhgoc;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //Do value của thanh cuộn là int nên cần phải ép kiểm byte
            byte nguong = (byte)vScrollBar_segmentation.Value;
            
            //Cho hiển thị giá trị ngưỡng
            lbnguong.Text = nguong.ToString();
        }

        private void lbnguong_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ các textbox và chuyển từ kiểu kí tự sang số
            int x1 = Convert.ToInt16(textBox_x1.Text);
            int x2 = Convert.ToInt16(textBox_x2.Text);
            int y1 = Convert.ToInt16(textBox_y1.Text);
            int y2 = Convert.ToInt16(textBox_y2.Text);
            int nguong = vScrollBar_segmentation.Value;

            double aRtb = 0, aGtb = 0, aBtb = 0;

            //Tính vector màu trung bình (average color vector) 
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                {
                    Color pixel = hinhgoc.GetPixel(x, y);
                    aRtb += pixel.R; //vector a = [aR aG aB], chứa ba thần phần trung bình màu cho từng kênh R-G-B 
                    aGtb += pixel.G;
                    aBtb += pixel.B;
                }
            //double Size =((x2-x1+1)* (y2-y1+1))
            // Tại mỗi kênh màu R-G-B, lấy trung bình cộng của tất cả pixel trong vùng ảnh đã chọn 
            double Size = Math.Abs(x2 - x1) * Math.Abs(y2 - y1);
            aRtb /= Size;
            aGtb /= Size;
            aBtb /= Size;

            //Phân đoạn ảnh
            //Tạo 1 ảnh bitmap chứa hình segmentation
            Bitmap ImgSeg = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            //vector z là điểm ảnh tại vị trị (x, y) đang tính để xem nó là điểm thuộc nền(background) hay đối tượng(object)
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    Color pixel2 = hinhgoc.GetPixel(x, y);
                    double zR = pixel2.R;
                    double zG = pixel2.G;
                    double zB = pixel2.B;

                    //Áp dụng công thức tính Euclidean Distance giữa hai vector a và z
                    double D = Math.Sqrt(Math.Pow(zR - aRtb, 2) + Math.Pow(zG - aGtb, 2) + Math.Pow(zB - aBtb, 2));

                    //Sau khi tính được giá trị D(z, a), so sánh với giá trị ngưỡng 
                    if ((int)D <= nguong) // là background thì set màu trắng (255)
                        ImgSeg.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    else// là object thì giữ nguyên màu
                        ImgSeg.SetPixel(x, y, Color.FromArgb((int)zR, (int)zG, (int)zB));

                }
            // Hiển thị ảnh đã phân đoạn
            picBox_seg.Image = ImgSeg;

        }
    }
}
