using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP7_HSI
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

            // Hiển thị các kênh màu HSI được chuyển đổi từ RGB
            List<Bitmap> HSI = ChuyenDoiRGBSangHSI(hinhgoc);      // HÀm chuyển đổi RGB sang CMYK
            picBox_H.Image = HSI[0];
            picBox_S.Image = HSI[1];
            picBox_I.Image = HSI[2];
            picBox_HSI.Image = HSI[3];

        }

        //Viêt hàm chuyển đổi RGB sang HSI
        public List<Bitmap> ChuyenDoiRGBSangHSI(Bitmap hinhgoc)
        {
            // Tạo 1 mảng động List chứa 4 kênh ảnh khi trả về:
            List<Bitmap> HSI = new List<Bitmap>();

            // Mỗi kênh được hiển thị bởi 1 hình Bitmap
            //Kích thước của mỗi hình = kt ảnh gốc để tính toán chuyển đổi kênh màu đúng cho từng pixel

            //Tạo 3 kênh màu để chứa các kênh màu H-S-I
            Bitmap Hue = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Saturation = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Intensity = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Hình HSI là kết hợp giauwx 3 kênh mài H-S-I
            Bitmap ImgHSI = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    // Lấy gia tri điểm ảnh tại (x,y)
                    Color pixel = hinhgoc.GetPixel(x, y);

                    //Các giá trị trả về kiểu số thực nên khai báo double
                    double R = pixel.R;
                    double G = pixel.G;
                    double B = pixel.B;

                    //Công thức chuyển đổi RGB sang HSI
                    double t1 = ((R - B) + (R - G)) / 2;    //Phan tu cua cong thuc tinh goc theta
                    double t2 = Math.Sqrt((R - G) * (R - G) + (R - B) * (G - B)); //Phan mau cua cong thuc tinh goc theta
                    double theta = Math.Acos(t1 / t2);//Trong C#, Acos tinh = rad

                    //Cong thuc tinh gia tri Hue
                    double H = 0;
                    if (B <= G)
                        H = theta;
                    else//B > G
                        H = 2 * Math.PI - theta;//theta tinh = rad nen can lay 2pi tru

                    H = H * 180 / Math.PI;//Chuyen doi gia tri radian sang degree

                    //Tinh gia tri S
                    double S = 1 - 3 * Math.Min(R, Math.Min(G, B)) / (R + G + B);
                    //Do gia tri tinh ra cua S trong khoang [0,1]
                    //Convert S sang khoang [0,255] de Bitmap co the hien thi
                    S = S * 255;

                    //Tinh gia tri I
                    double I = (R + G + B) / 3;

                    //Set các điểm ảnh của kênh giá trị H,S,I vào ảnh Bitmap
                    //Ép kiểu byte để hình Bitmap có thể hiểu và hiển thị
                    Hue.SetPixel(x, y, Color.FromArgb((byte)H, (byte)H, (byte)H));
                    Saturation.SetPixel(x, y, Color.FromArgb((byte)S, (byte)S, (byte)S));
                    Intensity.SetPixel(x, y, Color.FromArgb((byte)I, (byte)I, (byte)I));
                    ImgHSI.SetPixel(x, y, Color.FromArgb((byte)H, (byte)S, (byte)I));
                }
            //Thêm các giá trị sau chuyển đổi vào mảng động List
            HSI.Add(Hue);
            HSI.Add(Saturation);
            HSI.Add(Intensity);
            HSI.Add(ImgHSI);

            //Trả mảng hình kết quả sau chuyển đổi cho hàm
            return HSI;
        }

            private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
