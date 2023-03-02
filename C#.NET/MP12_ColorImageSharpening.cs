using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP12_ImageSharpen
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
            //Sử dụng hàm làm mượt
            Bitmap SharpenImage = ColorImageSharpening(hinhgoc);
            //Hiển thị kết quả ảnh làm mượt
            picBox_Sh.Image = SharpenImage;
        }

        public Bitmap ColorImageSharpening(Bitmap hinhgoc)
        {
            //Ma tran mat na
            int[,] matrix = { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
            //Tạo 1 bitmap để chứa ảnh được làm mượt
            Bitmap SharpenImage = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Tiến hành quét các điểm ảnh trong hình gốc 3x3
            //Đối với mask 3x3 có thể bỏ qua đường viền 1 pixel ngoài cùng
            //Vì vậy, chỉ cần quét từ x=1 tới x = Width - 1, tượng tự với y
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {
                    //Các biến này dùng để chứa các giá trị cộng dồn của các pixel trong mặt nạ
                    //khai báo kiểu int để chứa các giá trị cộng dồn đó
                    int Rs = 0, Gs = 0, Bs = 0;
                    int SharpenR = 0, SharpenG = 0, SharpenB = 0;

                    //Tiến hành quét các điểm có trong mặt nạ //i=0,1,2
                    for (int i = x - 1; i < x + 2; i++)
                        for (int j = y - 1; j < y + 2; j++)
                        {
                            //Lấy thông tin màu R-G-B tại các điểm ảnh trong mặt nạ tại các vị trí (i,j)
                            Color color = hinhgoc.GetPixel(i, j);
                            int R = color.R;
                            int G = color.G;
                            int B = color.B;

                            //Nhân tích chập tất cả điểm ảnh đó cho mỗi kênh R-G-B tương ứng 
                            Rs += R * matrix[i - x + 1, j - y + 1] ;
                            Gs += G * matrix[i - x + 1, j - y + 1];
                            Bs += B * matrix[i - x + 1, j - y + 1];
                        }
                    //Kết thúc quét và cộng dồn điểm ảnh trong mặt nạ 
                    //Tính điểm sắc nét theo công thức 
                    Color color1 = hinhgoc.GetPixel(x, y);
                    int R1 = color1.R;
                    int G1 = color1.G;
                    int B1 = color1.B;

                    SharpenR = R1 + Rs;
                    SharpenG = G1 + Gs;
                    SharpenB = B1 + Bs;

                    //Giới hạn các giá trị điểm ảnh
                    if (SharpenR < 0)
                        SharpenR = 0;
                    else if (SharpenR > 255)
                        SharpenR = 255;
                    if (SharpenG < 0)
                        SharpenG = 0;
                    else if (SharpenG > 255)
                        SharpenG = 255;
                    if (SharpenB < 0)
                        SharpenB = 0;
                    else if (SharpenB > 255)
                        SharpenB = 255;
                    //Set các điểm ảnh đã làm mượt và đưa vào ảnh bitmap
                    SharpenImage.SetPixel(x, y, Color.FromArgb(SharpenR, SharpenG, SharpenB));
                }
            //Tra anh lam net ve cho ham
            return SharpenImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
