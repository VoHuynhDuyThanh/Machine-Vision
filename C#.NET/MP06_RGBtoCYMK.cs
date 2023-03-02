using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP6_CYMK
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

            // Hiển thị các kênh màu CMYK được chuyển đổi từ RGB
            List<Bitmap> CMYK = ChuyenDoiRGBSangCMYK(hinhgoc);      // HÀm chuyển đổi RGB sang CMYK
            picBox_cyan.Image = CMYK[0];
            picBox_magenta.Image = CMYK[1];
            picBox_yellow.Image = CMYK[2];
            picBox_black.Image = CMYK[3];
        }
        public List<Bitmap> ChuyenDoiRGBSangCMYK(Bitmap hinhgoc)
        {
            // Tạo 1 list chưa 4 kênh ảnh CMYK:
            List<Bitmap> CMYK = new List<Bitmap>();

            // Mỗi kênh được hiển thị bởi 1 hình Bitmap
            //Kích thước của mỗi hình = kt ảnh gốc để tính toán chuyển đổi kênh màu đúng cho từng pixel
            Bitmap Cyan = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Magenta = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Yellow = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Black = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    // Lấy gia tri điểm ảnh
                    Color pixel = hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Màu Cyan là ket hop G va B nen R=0
                    Cyan.SetPixel(x, y, Color.FromArgb(0, G, B));

                    // Màu Magenta la ket hop R va B nen G= 0
                    Magenta.SetPixel(x, y, Color.FromArgb(R, 0, B));

                    // Màu Yellow la ket hop R va G nen B=0
                    Yellow.SetPixel(x, y, Color.FromArgb(R, G, 0));

                    // Màu Black la lay Min của RGB:
                    byte K = Math.Min(R, Math.Min(G, B));
                    Black.SetPixel(x, y, Color.FromArgb(K, K, K));
                }
            // Add các hình tương ứng vào các kênh màu:
            CMYK.Add(Cyan);
            CMYK.Add(Magenta);
            CMYK.Add(Yellow);
            CMYK.Add(Black);

            return CMYK;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
