using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP8_HSV
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
            List<Bitmap> HSV = ChuyenDoiRGBSangHSV(hinhgoc);      // HÀm chuyển đổi RGB sang CMYK
            picBox_H.Image = HSV[0];
            picBox_S.Image = HSV[1];
            picBox_V.Image = HSV[2];
            picBox_HSV.Image = HSV[3];

        }

        //Viêt hàm chuyển đổi RGB sang HSI
        public List<Bitmap> ChuyenDoiRGBSangHSV(Bitmap hinhgoc)
        {
            // Tạo 1 mảng động List chứa 4 kênh ảnh khi trả về:
            List<Bitmap> HSV = new List<Bitmap>();

            // Mỗi kênh được hiển thị bởi 1 hình Bitmap
            //Kích thước của mỗi hình = kt ảnh gốc để tính toán chuyển đổi kênh màu đúng cho từng pixel

            //Tạo 3 kênh màu để chứa các kênh màu H-S-V
            Bitmap Hue = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Saturation = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Value = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Hình HSI là kết hợp giua 3 kênh màu H-S-V
            Bitmap ImgHSV = new Bitmap(hinhgoc.Width, hinhgoc.Height);

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
                    double V = Math.Max(R, Math.Max(G, B));

                    //Set các điểm ảnh của kênh giá trị H,S,V vào ảnh Bitmap
                    //Ép kiểu byte để hình Bitmap có thể hiểu và hiển thị
                    Hue.SetPixel(x, y, Color.FromArgb((byte)H, (byte)H, (byte)H));
                    Saturation.SetPixel(x, y, Color.FromArgb((byte)S, (byte)S, (byte)S));
                    Value.SetPixel(x, y, Color.FromArgb((byte)V, (byte)V, (byte)V));
                    ImgHSV.SetPixel(x, y, Color.FromArgb((byte)H, (byte)S, (byte)V));
                }
            //Thêm các giá trị sau chuyển đổi vào mảng động List
            HSV.Add(Hue);
            HSV.Add(Saturation);
            HSV.Add(Value);
            HSV.Add(ImgHSV);

            //Trả mảng hình kết quả sau chuyển đổi cho hàm
            return HSV;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
