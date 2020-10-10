using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;// thư viện dùng kết nối sql
/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;*/
namespace _1712415_1712769_1712899
{
    // server my computer
    //Data Source=HanNguyen;Initial Catalog=BANHANGONLINE;Integrated Security=True
    // class kết nối SQL
    public class KetNoi
    {
        public static SqlConnection GetDBConnection()
        {
            // nhập server máy bạn vào đây
            string Server = @"Data Source=HanNguyen;Initial Catalog=HTBanHang;Integrated Security=True";
            SqlConnection conn = new SqlConnection(Server);
            return conn;
        }
    }
    public class BienToanCuc
    {
        // biến kiểm tra đã đăng nhập chưa
        public static bool flag = false;
        // biến kiểm tra khách hàng nhập thông tin để đánh giá sp chưa
        // biến kiểm tra có thanh toán thẻ hay không
        public static bool flag2 = false;
        public static bool flag1 = false;
        //Thông tin khách hàng đánh giá sản phẩm
        public static string HoTenDanhGia = "";
        public static string EmailDanhGia = "";
        public static string DiaChiDanhGia = "";
        // lưu thông tin tài khoản
        public static string MaNV = "";// lưu MaNV
        public static string Password = "";// lưu password
        public static string Role = ""; // Lưu chức vụ
        public static string[] MAMH = new string[50];
        public static string[] MANCC = new string[50];
        public static string[] TENMH = new string[50];
        public static string[] TENNCC = new string[50];
        public static string[] TenLoai = new string[50];
        public static int[] SoLuong = new int[50];
        public static long[] Gia = new long[50];
        public static long[] Thanhtien = new long[50];
        public static int length = 0;
        public static string tongtien="";
        // số lượng sản phẩm trong giỏ hàng;
        public static int TongSoLuong(int n, int[] SoLuong)
        {
            int result = 0;
            for(int i=0;i<n;i++)
            {
                result =result+ SoLuong[i];
            }
            return result;
        }
        public static long Tongtien(int sl, long gia)// Tổng tiền của một sp trong giỏ hàng
        {
            return sl * gia;
        }
    }
}
