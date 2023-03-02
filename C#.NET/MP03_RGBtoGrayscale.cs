using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
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

            //Hiển thị mức xám
            picBox_hinhlightness.Image = ChuyenHinhRGBsangGrayLighness(hinhgoc);
            picBox_hinhaverage.Image = ChuyenHinhRGBsangGrayAverage(hinhgoc);
            picBox_hinhluminance.Image = ChuyenHinhRGBsangGrayLuminance(hinhgoc);
        }

        public Bitmap ChuyenHinhRGBsangGrayLighness(Bitmap hinhgoc)
        {
            // Khai báo hình Bitmap để load hình mưc xám
            Bitmap hinhmucxam = new Bitmap(hinhgoc.Width, hinhgoc.Height);

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
                    //pp Lightness
                    //Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte max = Math.Max(R, Math.Max(G, B));
                    byte min = Math.Min(R, Math.Min(G, B));
                    byte gray = (byte)((max + min) / 2);
                    //gray khai báo là byte, max với min là kiêu số thưc 
                    //nên cần phải ép kiểu về byte

                    //Set gía trị pixel đọc được vào các kênh màu
                    hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            return hinhmucxam;
        }

        public Bitmap ChuyenHinhRGBsangGrayAverage(Bitmap hinhgoc)
        {
            // Khai báo hình Bitmap để load hình mưc xám
            Bitmap hinhmucxam = new Bitmap(hinhgoc.Width, hinhgoc.Height);

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

                    //Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte gray = (byte)((R + G + B) / 3);
                    //gray khai báo là byte, max với min là kiêu số thưc 
                    //nên cần phải ép kiểu về byte

                    //Set gía trị pixel đọc được vào các kênh màu
                    hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            return hinhmucxam;
        }

        public Bitmap ChuyenHinhRGBsangGrayLuminance(Bitmap hinhgoc)
        {
            // Khai báo hình Bitmap để load hình mưc xám
            Bitmap hinhmucxam = new Bitmap(hinhgoc.Width, hinhgoc.Height);

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

                    //Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte gray = (byte)(R*0.2126 + G*0.7152 + B*0.0722);
                    //gray khai báo là byte, max với min là kiêu số thưc 
                    //nên cần phải ép kiểu về byte

                    //Set gía trị pixel đọc được vào các kênh màu
                    hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            return hinhmucxam;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
