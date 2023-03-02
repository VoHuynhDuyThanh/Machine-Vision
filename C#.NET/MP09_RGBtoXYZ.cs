using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP9_HinhXYZ
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

            List<Bitmap> XYZ = ChuyenDoiRGBSangXYZ(hinhgoc);      // HÀm chuyển đổi RGB sang XYZ
            picBox_X.Image = XYZ[0];
            picBox_Y.Image = XYZ[1];
            picBox_Z.Image = XYZ[2];
            picBox_XYZ.Image = XYZ[3];
        }

        //Viêt hàm chuyển đổi RGB sang XYZ
        public List<Bitmap> ChuyenDoiRGBSangXYZ(Bitmap hinhgoc)
        {
            // Tạo 1 mảng động List chứa 4 kênh ảnh khi trả về:
            List<Bitmap> XYZ = new List<Bitmap>();

            // Mỗi kênh được hiển thị bởi 1 hình Bitmap
            //Kích thước của mỗi hình = kt ảnh gốc để tính toán chuyển đổi kênh màu đúng cho từng pixel

            //Tạo 3 kênh màu để chứa các kênh màu X-Y-Z
            Bitmap X = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Y = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Z = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Hình XYZ là kết hợp giua 3 kênh màu X-Y-Z
            Bitmap imgXYZ = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    // Lấy gia tri điểm ảnh tại (x,y)
                    Color pixel = hinhgoc.GetPixel(x, y);

                    //Các giá trị trả về kiểu số thực nên khai báo double
                    double R = pixel.R;
                    double G = pixel.G;
                    double B = pixel.B;

                    //Tinh XYZ dua tren cong thuc
                    double Value_X = 0.4124564 * R + 0.3575761 * G + 0.1804375 * B;
                    double Value_Y = 0.2126729 * R + 0.7151522 * G + 0.0721750 * B;
                    double Value_Z = 0.0193339 * R + 0.1191920 * G + 0.9503041 * B;

                    //Set các điểm ảnh của kênh giá trị X,Y,Z vào ảnh Bitmap
                    //Ép kiểu byte để hình Bitmap có thể hiểu và hiển thị 
                    X.SetPixel(x, y, Color.FromArgb((byte)Value_X, (byte)Value_X, (byte)Value_X));
                    Y.SetPixel(x, y, Color.FromArgb((byte)Value_Y, (byte)Value_Y, (byte)Value_Y));
                    Z.SetPixel(x, y, Color.FromArgb((byte)Value_Z, (byte)Value_Z, (byte)Value_Z));
                    imgXYZ.SetPixel(x, y, Color.FromArgb((byte)Value_X, (byte)Value_Y, (byte)Value_Z));
                }
            //Thêm các giá trị sau chuyển đổi vào mảng động List
            XYZ.Add(X);
            XYZ.Add(Y);
            XYZ.Add(Z);
            XYZ.Add(imgXYZ);
            
            //Trả mảng hình kết quả sau chuyển đổi cho hàm
            return XYZ;
        }
            private void picBox_hinhgoc_Click(object sender, EventArgs e)
        {

        }
    }
}
