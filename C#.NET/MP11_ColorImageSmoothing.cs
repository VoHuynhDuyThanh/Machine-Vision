using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP11_ImageSmooth
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
            Bitmap SmoothImage3 = ColorImageSmoothing3x3(hinhgoc);
            Bitmap SmoothImage5 = ColorImageSmoothing5x5(hinhgoc);
            Bitmap SmoothImage7 = ColorImageSmoothing7x7(hinhgoc);
            Bitmap SmoothImage9 = ColorImageSmoothing9x9(hinhgoc);
            //Hiển thị kết quả ảnh làm mượt
            picBox_S3x3.Image = SmoothImage3;
            picBox_S5x5.Image = SmoothImage5;
            picBox_S7x7.Image = SmoothImage7;
            picBox_S9x9.Image = SmoothImage9;

        }

        //Hàm làm mượt cho mặt nạ 3x3
        public Bitmap ColorImageSmoothing3x3(Bitmap hinhgoc)
        {
            //Tạo 1 bitmap để chứa ảnh được làm mượt
            Bitmap SmoothImage = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Tiến hành quét các điểm ảnh trong hình gốc 3x3
            //Đối với mask 3x3 có thể bỏ qua đường viền 1 pixel ngoài cùng
            //Vì vậy, chỉ cần quét từ x=1 tới x = Width - 1, tượng tự với y
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {
                    //Các biến này dùng để chứa các giá trị cộng dồn của các pixel trong mặt nạ
                    //khai báo kiểu int để chứa các giá trị cộng dồn đó
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Tiến hành quét các điểm có trong mặt nạ //i=0,1,2
                    for (int i = x - 1; i < x + 2; i++)
                        for (int j = y - 1; j < y + 2; j++)
                        {
                            //Lấy thông tin màu R-G-B tại các điểm ảnh trong mặt nạ tại các vị trí (i,j)
                            Color color = hinhgoc.GetPixel(i, j);
                            byte R = color.R;
                            byte G = color.G;
                            byte B = color.B;

                            //Cộng dồn các điểm ảnh đó cho mỗi kênh R-G-B tương ứng 
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }
                    //Kết thúc quét và cộng dồn điểm ảnh trong mặt nạ 
                    //Tính trung bình cộng  cho mỗi kênh 
                    byte K = 3 * 3; //mặt nạ 3x3
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Set các điểm ảnh đã làm mượt và đưa vào ảnh bitmap
                    SmoothImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }
            //Tra anh lam muot ve cho ham
            return SmoothImage;
        }

        //Hàm làm mượt cho mặt nạ 5x5
        public Bitmap ColorImageSmoothing5x5(Bitmap hinhgoc)
        {
            //Tạo 1 bitmap để chứa ảnh được làm mượt
            Bitmap SmoothImage = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Tiến hành quét các điểm ảnh trong hình gốc 3x3
            //Đối với mask 3x3 có thể bỏ qua đường viền 2 pixel ngoài cùng
            //Vì vậy, chỉ cần quét từ x=2 tới x = Width - 2, tượng tự với y
            for (int x = 2; x < hinhgoc.Width - 2; x++)
                for (int y = 2; y < hinhgoc.Height - 2; y++)
                {
                    //Các biến này dùng để chứa các giá trị cộng dồn của các pixel trong mặt nạ
                    //khai báo kiểu int để chứa các giá trị cộng dồn đó
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Tiến hành quét các điểm có trong mặt nạ
                    for (int i = x - 2; i < x + 3; i++)
                        for (int j = y -2; j < y + 3; j++)
                        {
                            //Lấy thông tin màu R-G-B tại các điểm ảnh trong mặt nạ tại các vị trí (i,j)
                            Color color = hinhgoc.GetPixel(i, j);
                            byte R = color.R;
                            byte G = color.G;
                            byte B = color.B;

                            //Cộng dồn các điểm ảnh đó cho mỗi kênh R-G-B tương ứng 
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }
                    //Kết thúc quét và cộng dồn điểm ảnh trong mặt nạ 
                    //Tính trung bình cộng  cho mỗi kênh 
                    byte K = 5 * 5; //mặt nạ 5x5
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Set các điểm ảnh đã làm mượt và đưa vào ảnh bitmap
                    SmoothImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }
            //Tra anh lam muot ve cho ham
            return SmoothImage;
        }

        //Hàm làm mượt cho mặt nạ 7x7
        public Bitmap ColorImageSmoothing7x7(Bitmap hinhgoc)
        {
            //Tạo 1 bitmap để chứa ảnh được làm mượt
            Bitmap SmoothImage = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Tiến hành quét các điểm ảnh trong hình gốc 3x3
            //Đối với mask 3x3 có thể bỏ qua đường viền 3 pixel ngoài cùng
            //Vì vậy, chỉ cần quét từ x=3 tới x = Width - 3, tượng tự với y
            for (int x = 3; x < hinhgoc.Width - 3; x++)
                for (int y = 3; y < hinhgoc.Height - 3; y++)
                {
                    //Các biến này dùng để chứa các giá trị cộng dồn của các pixel trong mặt nạ
                    //khai báo kiểu int để chứa các giá trị cộng dồn đó
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Tiến hành quét các điểm có trong mặt nạ
                    for (int i = x - 3; i < x + 4; i++)
                        for (int j = y - 3; j < y + 4; j++)
                        {
                            //Lấy thông tin màu R-G-B tại các điểm ảnh trong mặt nạ tại các vị trí (i,j)
                            Color color = hinhgoc.GetPixel(i, j);
                            byte R = color.R;
                            byte G = color.G;
                            byte B = color.B;

                            //Cộng dồn các điểm ảnh đó cho mỗi kênh R-G-B tương ứng 
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }
                    //Kết thúc quét và cộng dồn điểm ảnh trong mặt nạ 
                    //Tính trung bình cộng  cho mỗi kênh 
                    byte K = 7 * 7; //mặt nạ 7x7
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Set các điểm ảnh đã làm mượt và đưa vào ảnh bitmap
                    SmoothImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }
            //Tra anh lam muot ve cho ham
            return SmoothImage;
        }

        //Hàm làm mượt cho mặt nạ 9x9
        public Bitmap ColorImageSmoothing9x9(Bitmap hinhgoc)
        {
            //Tạo 1 bitmap để chứa ảnh được làm mượt
            Bitmap SmoothImage = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Tiến hành quét các điểm ảnh trong hình gốc 3x3
            //Đối với mask 3x3 có thể bỏ qua đường viền 2 pixel ngoài cùng
            //Vì vậy, chỉ cần quét từ x=4 tới x = Width - 4, tượng tự với y
            for (int x = 4; x < hinhgoc.Width - 4; x++)
                for (int y = 4; y < hinhgoc.Height - 4; y++)
                {
                    //Các biến này dùng để chứa các giá trị cộng dồn của các pixel trong mặt nạ
                    //khai báo kiểu int để chứa các giá trị cộng dồn đó
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Tiến hành quét các điểm có trong mặt nạ
                    for (int i = x - 4; i < x + 5; i++)
                        for (int j = y - 4; j < y + 5; j++)
                        {
                            //Lấy thông tin màu R-G-B tại các điểm ảnh trong mặt nạ tại các vị trí (i,j)
                            Color color = hinhgoc.GetPixel(i, j);
                            byte R = color.R;
                            byte G = color.G;
                            byte B = color.B;

                            //Cộng dồn các điểm ảnh đó cho mỗi kênh R-G-B tương ứng 
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }
                    //Kết thúc quét và cộng dồn điểm ảnh trong mặt nạ 
                    //Tính trung bình cộng  cho mỗi kênh 
                    byte K = 9 * 9; //mặt nạ 9x9
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Set các điểm ảnh đã làm mượt và đưa vào ảnh bitmap
                    SmoothImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }
            //Tra anh lam muot ve cho ham
            return SmoothImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void picBox_S3x3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
