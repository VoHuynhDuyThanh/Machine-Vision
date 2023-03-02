using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP10_YCrCb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Tạo biến chứa đường dẫn của hình gốc cần tách 
            string filehinh = @"D:\TaiLieu\XLA\lena.jpg"; // kí tự @ giúp C#.NET hiểu là chuỗi unicode, khong bao loi

            // Tạo một biến chứa hình Bitmap được load từ file hình trên.
            Bitmap hinhgoc = new Bitmap(filehinh);

            // Hiển thị hình trong picBox_hinhgoc đã tạo
            picBox_hinhgoc.Image = hinhgoc;

            List<Bitmap> YCrCb = ChuyenDoiRGBSangYCrCb(hinhgoc);      // HÀm chuyển đổi RGB sang XYZ
            picBox_Y.Image = YCrCb[0];
            picBox_Cr.Image = YCrCb[1];
            picBox_Cb.Image = YCrCb[2];
            picBox_YCrCb.Image = YCrCb[3];
        }

        //Viêt hàm chuyển đổi RGB sang YCrCb
        public List<Bitmap> ChuyenDoiRGBSangYCrCb(Bitmap hinhgoc)
        {
            // Tạo 1 mảng động List chứa 4 kênh ảnh khi trả về:
            List<Bitmap> YCrCb = new List<Bitmap>();

            // Mỗi kênh được hiển thị bởi 1 hình Bitmap
            //Kích thước của mỗi hình = kt ảnh gốc để tính toán chuyển đổi kênh màu đúng cho từng pixel

            //Tạo 3 kênh màu để chứa các kênh màu X-Y-Z
            Bitmap Y = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Cr = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Cb = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Hình YCrCb là kết hợp giua 3 kênh màu X-Y-Z
            Bitmap imgYCrCb = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    // Lấy gia tri điểm ảnh tại (x,y)
                    Color pixel = hinhgoc.GetPixel(x, y);

                    //Các giá trị trả về kiểu số thực nên khai báo double
                    double R = pixel.R;
                    double G = pixel.G;
                    double B = pixel.B;

                    //Tinh YCrCb dua tren cong thuc
                    double Value_Y = 16 + (65.738 / 256) * R + (129.057 / 256) * G + (25.064 / 256) * B;
                    double Value_Cr = 128 - (37.945 / 256) * R - (74.494 / 256) * G + (112.439 / 256) * B;
                    double Value_Cb = 128 + (112.439 / 256) * R - (94.154 / 256) * G - (18.285 / 256) * B;

                    //Set các điểm ảnh của kênh giá trị Y,Cr,Cb vào ảnh Bitmap
                    //Ép kiểu byte để hình Bitmap có thể hiểu và hiển thị 
                    Y.SetPixel(x, y, Color.FromArgb((byte)Value_Y, (byte)Value_Y, (byte)Value_Y));
                    Cr.SetPixel(x, y, Color.FromArgb((byte)Value_Cr, (byte)Value_Cr, (byte)Value_Cr));
                    Cb.SetPixel(x, y, Color.FromArgb((byte)Value_Cb, (byte)Value_Cb, (byte)Value_Cb));
                    imgYCrCb.SetPixel(x, y, Color.FromArgb((byte)Value_Y, (byte)Value_Cr, (byte)Value_Cb));
                }
            //Thêm các giá trị sau chuyển đổi vào mảng động List
            YCrCb.Add(Y);
            YCrCb.Add(Cr);
            YCrCb.Add(Cb);
            YCrCb.Add(imgYCrCb);

            //Trả mảng hình kết quả sau chuyển đổi cho hàm
            return YCrCb;
        }

        private void picBox_hinhgoc_Click(object sender, EventArgs e)
        {

        }
    }
}
