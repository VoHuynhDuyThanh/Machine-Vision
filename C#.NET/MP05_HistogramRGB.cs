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

namespace MP5._1_HistogramRGB
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

            //Gọi các hàm đã viết để vẽ biểu đồ Histogram
            //Tính Histogram
            double[,] histogram = TinhHistogram(hinhgoc);

            //Chuyen doi kieu du lieu
            List<PointPairList> points = ChuyenDoiHistogram(histogram);

            //Ve bd Histogram va cho hien thi
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
        }

        //Tinh Histogram cua anh muc xam
        public double[,] TinhHistogram(Bitmap hinhrgb)
        {
            //Dùng mảng 2 chiều để chứa hông tin histogram cho 3 kênh màu RGB
            // 3: là 3 kênh màu
            // 256: cần 256 vị trí cho các giá trị màu từ 0-255
            double[,] histogram = new double[3,256];

            for (int x = 0; x < hinhrgb.Width; x++)
                for (int y = 0; y < hinhrgb.Height; y++)
                {
                    // Doc giá trị pixel tại điểm ảnh có giá trị (x,y)
                    Color color = hinhrgb.GetPixel(x, y);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;
                    
                    histogram[0, R]++; //Histogram kenh R
                    histogram[1, G]++;//Histogram kenh G
                    histogram[2, B]++;//Histogram kenh B
                }
            return histogram; //tra lai mang 2 chieu chua thong tin histogram RGB
        }

        List<PointPairList> ChuyenDoiHistogram(double[,] histogram)
        {
            //Dùng một mảng không cần khai báo giá trị để chứa các giá trị chuyển đổi 
            List<PointPairList> points = new List<PointPairList>();
            PointPairList redPoints = new PointPairList(); //Chuyen doi Histogram kenh R
            PointPairList greenPoints = new PointPairList(); //Chuyen doi Histogram kenh G
            PointPairList bluePoints = new PointPairList(); //Chuyen doi Histogram kenh B
            for (int i = 0; i < 256; i++)
            {
                //i là gt trục nằm ngang(0-255)
                //histogram[i] lf trục đứng, số pixel cung muc xam
                redPoints.Add(i, histogram[0, i]); //Chuyen doi cho kenh R
                greenPoints.Add(i, histogram[1, i]); //Chuyen doi cho kenh G
                bluePoints.Add(i, histogram[2, i]); //Chuyen doi cho kenh B
            }

            //Sau khi kết thúc vòng lặp for thì thông tinh histogram đã được chuyển đổi
            //Add các kênh màu vào mảng points để trả về cho hàm
            points.Add(redPoints);
            points.Add(greenPoints);
            points.Add(bluePoints);
            return points;
        }

        //Thiết lập một biểu đồ trong ZedGraph
        public GraphPane BieuDoHistogram(List<PointPairList> histogram)
        {
            //GraphPane là đối tượng biểu đồ trong ZedGraph
            GraphPane gp = new GraphPane();

            gp.Title.Text = @"Biểu đồ Histogram"; //Tên của biểu đồ
            gp.Rect = new Rectangle(0, 0, 700, 500);    //khung chua bieu do

            //Thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị màu của các điểm ảnh";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 255;
            gp.XAxis.Scale.MajorStep = 5;//Mỗi bước chính là 5
            gp.XAxis.Scale.MajorStep = 1;//Mỗi bước trong một bước chính là 1

            //Tương tự, thiết lập cho trục đứng
            gp.XAxis.Title.Text = @"Số điểm ảnh có cùng giá trị màu";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 20000; //so lon hon kich thuoc anh(500x381)
            gp.XAxis.Scale.MajorStep = 5;//Mỗi bước chính là 5
            gp.XAxis.Scale.MajorStep = 1;//Mỗi bước trong một bước chính là 1

            //Dùng biểu đồ dạng bả để biểu diễn histogram
            gp.AddBar("Histogram's Red", histogram[0], Color.Red);
            gp.AddBar("Histogram's Green", histogram[1], Color.Green);
            gp.AddBar("Histogram's Blue", histogram[2], Color.Blue);
            return gp;
        }

        private void picBox_hinhgoc_Click(object sender, EventArgs e)
        {

        }
    }
}
