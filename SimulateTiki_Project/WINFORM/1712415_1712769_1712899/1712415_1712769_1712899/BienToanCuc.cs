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
    // Data Source = DESKTOP - B09AIR6; Initial Catalog = NGHIEPVU; Integrated Security = True
    //Data Source=DESKTOP-B09AIR6;Initial Catalog=NGHIEPVU;Integrated Security=True
    // class kết nối SQL
    public class KetNoi
    {
        public static SqlConnection GetDBConnection()
        {
            string Server = @"Data Source=DESKTOP-B09AIR6;Initial Catalog=NGHIEPVU;Integrated Security=True";// nhập server máy bạn vào đây
            SqlConnection conn = new SqlConnection(Server);
            return conn;
        }
    }
    public class BienToanCuc
    {
        // biến kiểm tra đã d9an8ng nhập chưa
        public static bool flag = false;
        // true là tìm kiếm theo mặc hàng, fall là tìm kiếm theo nhà cung cấp
        // lưu thông tin tài khoản
        public static string TenTK = "";// lưu TenTK
        public static string Password="";// lưu password
        public static string[] MAMH = new string[50];
        public static string[] MANCC = new string[50];
        public static string[] TENMH = new string[50];
        public static string[] TENNCC = new string[50];
        public static string[] THUONGHIEU = new string[50];
        public static int[] SoLuong = new int[50];
        public static int[] Gia = new int[50];
        public static int[] Thanhtien = new int[50];
        public static string GioiTinh = ""; // giới tính 0 là nữ, 1 là nam
        public static int length = 0;// số lượng sản phẩm trong giỏ hàng;
        public static int Tongtien(int sl, int gia)
        {
            return sl * gia;
        }
    }
}
