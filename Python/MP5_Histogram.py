import cv2          #Thư viện xử lí OpenCV cho python
from PIL import Image #Thư viện xữ lý ảnh PILLOW hỗ trợ nhiều định dạng ảnh
import numpy as np       #Thư viện toán học, đặc biệt là tính toán ma trận
import matplotlib.pyplot as plt #Thu vien vs bieu do


#Chuyển đổi ảnh màu RGB sang ảnh mức xám
def ChuyenDoiRGBsangGrayLuminance(imgPIL):
    #Tạo 1 ảnh cùng kích thước và mode với ảnh imgPIL
    #Ảnh này chứa kết quả cuyển đổi RGB to Grayscale
    lum = Image.new(imgPIL.mode, imgPIL.size)

    #Lấy kích thước của ảnh từ imgPIL`
    width =lum.size[0]
    height = lum.size[1]

    #Đọc các giá trị pixel
    for x in range(width):
        for y in range(height):
            R, G, B = imgPIL.getpixel((x, y))

            #Dùng pp Luminance chuyển đổi RGB to Grayscale
            gray = np.uint8(0.2126*R + 0.7152*G + 0.0722*B)

            #Gán giá trị mức xám vừa tính cho ảnh xám
            lum.putpixel((x, y), (gray, gray, gray))
            #Ảnh có 3 kênh màu nên cho cùng giá trị

    return lum


#======================================================
#Tinh histogram cua anh xam
def TinhHistogram(HinhXamPIL):
    #Mỗi pĩel có giá trị từ 0-255
    #Khai báo môtk mảng có 256 phần tử để chứa số đếm của các pĩels có cùng giá trị
    his = np.zeros(256)

    #Kích thước ảnh
    w = HinhXamPIL.size[0]
    h = HinhXamPIL.size[1]

    for x in range(w):
        for y in range(h):
            #Lấy giá trị mức xám tại điểm (x,y)
            gR, gG, gB = HinhXamPIL.getpixel((x,y))

            #Giá trị gray tính ra cũng chính là phần tử thứ gray trong mảng his đã khai báo ở trên
            #Tăng số đếm của phần tử thứ gray lên 1
            his[gR] +=1

    return his #Trả về giá trị mảng Histogram

#=========================================================
#Vẽ biểu đồ Histogram
def VeBieuDoHistogram(his):
    w = 5
    h = 4
    plt.figure('Biểu đồ Histogram ảnh xám', figsize=(((w, h))), dpi=100)
    trucX = np.zeros(256)
    trucX = np.linspace(0, 256, 256) 
    plt.plot(trucX, his, color='orange')
    plt.title('Biểu đồ Histogram')
    plt.xlabel('Giá trị mức xám')
    plt.ylabel('Số điểm cùng giá trị mức xám')
    plt.show()

#=========================================================
#CHƯƠNG TRÌNH CHÍNH

# Khai báo đường dẫn file hình
filehinh = r'bird_small.jpg'

#Đọc ảnh màu sử dụng thư viện PIL. Ảnh PIL dùng để xử lý và tính toán
imgPIL = Image.open(filehinh)

#Chuyển ảnh sang mức xám
HinhXamPIL = ChuyenDoiRGBsangGrayLuminance(imgPIL)

#Tính Histogram
his = TinhHistogram(HinhXamPIL)

#Chuyển ảnh PIL sang OpenCv để hiển thị bằng thư viện OpenCv
HinhXamCV = np.array(HinhXamPIL)
cv2.imshow('Ảnh mức xám', HinhXamCV)

#Hiển thị biểu đồ Histogram
VeBieuDoHistogram(his)

# Bấm phím bất kỳ để đóng cửa sổ hiển thị
cv2.waitKey(0)

#Giải phóng bộ nhớ đã cấp phát cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()