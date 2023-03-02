using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace MP5_Histogram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Tạo biến chứa đường dẫn của hình gốc cần tách 
            string filehinh = @"D:\TaiLieu\XLA\bird_small.jpg"; // kí tự @ giúp C#.NET hiểu là chuỗi unicode, khong bao loi

            // Tạo một biến chứa hình Bitmap được load từ file hình trên.
            Bitmap hinhgoc = new Bitmap(filehinh);

            // Hiển thị hình trong picBox_hinhgoc đã tạo
            picBox_hinhgoc.Image = hinhgoc;

            //Hiển thị mức xám
            Bitmap hinhmucxam = ChuyenHinhRGBsangGrayLuminance(hinhgoc);
            picBox_hinhmucxam.Image = hinhmucxam;


            //Gọi các hàm đã viết để vẽ biểu đồ Histogram
            //Tính Histogram
            double[] histogram = TinhHistogram(hinhmucxam);

            //Chuyen doi kieu du lieu
            PointPairList points = ChuyenDoiHistogram(histogram);

            //Ve bd Histogram va cho hien thi
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
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
                    byte gray = (byte)(R * 0.2126 + G * 0.7152 + B * 0.0722);
                    //gray khai báo là byte, max với min là kiêu số thưc 
                    //nên cần phải ép kiểu về byte

                    //Set gía trị pixel đọc được vào các kênh màu
                    hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            return hinhmucxam;  //tra lai mang 2 chieu chua thong tin histogram hinhmucxam
        }

        //Tinh Histogram cua anh muc xam
        public double[] TinhHistogram(Bitmap hinhmucxam)
        {
            //Mỗi pĩxel mức xám có giá trị từ 0-255, do vậy ta khai báo một mảng có 256 phần tử
            //Dùng kiểu double vì tổng số đếm của các pixel có thể rất lớn, phụ thộc vào kích thước ảnh 
            double[] histogram = new double[256];

            for (int x = 0; x < hinhmucxam.Width; x++)
                for (int y = 0; y < hinhmucxam.Height; y++)
                {
                    // Doc giá trị pixel tại điểm ảnh có giá trị (x,y)
                    Color color = hinhmucxam.GetPixel(x, y);
                    byte gray = color.R;    //trong hinh muc xam gt kenh R cung giong G hoac B

                    //Giá trị gray tính ra cũng chính là phần tủ thứ gray trong mảng histogram đã khai báo
                    //Tăng số đếm cảu phần tử thứ gray lên 1.
                    histogram[gray]++;
                }
            return histogram;
        }

        PointPairList ChuyenDoiHistogram(double[] histogram)
        {
            //PointPairList laf kieu du lieu cua ZedGraph de ve do thi
            PointPairList points = new PointPairList();

            for (int i = 0; i < histogram.Length; i++)
            {
                //i là gt trục nằm ngang(0-255)
                //histogram[i] laf trục đứng, số pixel cung muc xam
                points.Add(i, histogram[i]);
            }
            return points;
        }

        //Thiết lập một biểu đồ trong ZedGraph
        public GraphPane BieuDoHistogram(PointPairList histogram)
        {
            //GraphPane là đối tượng biểu đồ trong ZedGraph
            GraphPane gp = new GraphPane();

            gp.Title.Text = @"Biểu đồ Histogram"; //Tên của biểu đồ
            gp.Rect = new Rectangle(0, 0, 700, 500);    //khung chua bieu do

            //Thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị mức xám của các điểm ảnh";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 255;
            gp.XAxis.Scale.MajorStep = 5;//Mỗi bước chính là 5
            gp.XAxis.Scale.MajorStep = 1;//Mỗi bước trong một bước chính là 1

            //Tương tự, thiết lập cho trục đứng
            gp.XAxis.Title.Text = @"Số điểm ảnh có cùng giá trị mức xám ";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 20000; //so lon hon kich thuoc anh(500x381)
            gp.XAxis.Scale.MajorStep = 5;//Mỗi bước chính là 5
            gp.XAxis.Scale.MajorStep = 1;//Mỗi bước trong một bước chính là 1

            //Dùng biểu đồ dạng bả để biểu diễn histogram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);

            return gp;
        }

            private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
