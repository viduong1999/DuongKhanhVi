using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;// thư viện dùng kết nối sql
using System.Windows.Forms;
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
    //Data Source=HANNGUYEN;Initial Catalog=HTThuVien;Integrated Security=True
    // class kết nối SQL
    public class KetNoi
    {
        public static SqlConnection GetDBConnection()
        {
            // nhập server máy bạn vào đây
            string Server = @"Data Source=HanNguyen;Initial Catalog=HTThuVien;Integrated Security=True";
            SqlConnection conn = new SqlConnection(Server);
            return conn;
        }
    }
    public class BienToanCuc
    {
        // biến kiểm tra đã đăng nhập chưa
        public static bool flag = false;
        // lưu thông tin tài khoản
        public static string MaNV = "";// lưu MaNV
        public static string Password = "";// lưu password
        public static string Role = ""; // Lưu chức vụ
        public static string HOTEN = ""; // Lưu HỌ TÊN
        public static string HOTEN_DOCGIA = ""; // Lưu HỌ TÊN
        public static string MATHE = "";
        public static int SoLuongSachDangMuon = 0;
        public static string ID = "";
        public static bool KT_CapNhat = false;
        public static int soluonghocvienmolop = 0;
        public static string[] DanhSachLopHoc = new string[300];
        // số lượng sản phẩm trong giỏ hàng;
        public static int TongSoLuong(int n, int[] SoLuong)
        {
            int result = 0;
            for (int i = 0; i < n; i++)
            {
                result = result + SoLuong[i];
            }
            return result;
        }
        public static int Tongtien(int sl, int gia)// Tổng tiền của một sp trong giỏ hàng
        {
            return sl * gia;
        }
        // hàm tạo danh sách lớp học
        public static void TaoLopHoc(string MaHV)
        {
            // => mở kết nối
            SqlConnection conntemp = KetNoi.GetDBConnection();
            try
            {
                conntemp.Open();
                string sqltemp = "UPDATE DSDKTHE SET MALOP = " +
                    BienToanCuc.MALOPHOC+" where MAHV=N'"+MaHV+"'";
                SqlCommand cmd = new SqlCommand(sqltemp, conntemp);// vận chuyển câu lệnh 
                int datatemp = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                conntemp.Close();

            }
            catch (Exception)
            {
                return;
            }
        }
        // cập nhật tình trạng quá hạn của lịch sử giao dịch
        public static void CapNhatTinhTrang(string ID)
        {
            // => mở kết nối
            SqlConnection conntemp = KetNoi.GetDBConnection();
            try
            {
                conntemp.Open();
                string sqltemp = "UPDATE LSGIAODICH SET TINHTRANG=N'Quá hạn' where id=" + ID;
                SqlCommand cmd = new SqlCommand(sqltemp, conntemp);// vận chuyển câu lệnh 
                int datatemp = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                //  if (data1 != 0) MessageBox.Show("Thêm vào giỏ hàng thành công");
                // else MessageBox.Show("Thêm vào giỏ hàng thất bại");
                conntemp.Close();

            }
            catch (Exception)
            {
                return;
            }
        }
        //tính tiền phat
        public static int TienPhat(int ngay, string TinhTrang, int MaSach)
        {
            int result = 0;
            int tiensach = 0;
            //TÌM GIÁ TIỀN của quyển sách
            SqlConnection conntemp1 = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            /*select LSGD.MASACH, SACH.TENSACH, LSGD.NGAYMUON, LSGD.HANTRA,LSGD.NGAYTRA,
             * LSGD.TINHTRANG, LSGD.ID from LSGIAODICH AS LSGD,SACH
             * where LSGD.MATHE='SV1' and LSGD.MASACH=SACH.MASACH*/
            //kiểm tra xem có độc giả đó hay không
            string lenhtemp1 = "select* FROM SACH WHERE MASACH=" + MaSach.ToString();
            conntemp1.Open();// mở kết nối
            try
            {

                SqlCommand cmtemp1 = new SqlCommand(lenhtemp1, conntemp1);
                SqlDataReader datatemp1 = cmtemp1.ExecuteReader();
                if (datatemp1.HasRows)// nếu thẻ thư viện hợp lệ
                {
                    while (datatemp1.Read())
                    {
                        string temp = datatemp1["GIA"].ToString();
                        tiensach = int.Parse(temp);
                    }
                    conntemp1.Close();
                }
                else
                {
                    conntemp1.Close();
                }
            }
            catch (Exception)
            {
                return 0;
            }
            if (TinhTrang == "Quá hạn")
            {
                for (int i = 1; i <= ngay; i++)
                {
                    if (i < 31)
                    {
                        result = result + 1000;
                    }
                    else
                    {
                        result = result + 2000;
                    }
                }
            }
            else// hư hổng hoặc quá hạn
            {
                result = tiensach + 50000;
            }
            return result;
        }
        public static string MALOPHOC = "";
        public static void GETMALOPHOC()
        {
            int malop = 0;
            //TÌM GIÁ TIỀN của quyển sách
            SqlConnection conntemp1 = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            /*select LSGD.MASACH, SACH.TENSACH, LSGD.NGAYMUON, LSGD.HANTRA,LSGD.NGAYTRA,
             * LSGD.TINHTRANG, LSGD.ID from LSGIAODICH AS LSGD,SACH
             * where LSGD.MATHE='SV1' and LSGD.MASACH=SACH.MASACH*/
            //kiểm tra xem có độc giả đó hay không
            string lenhtemp1 = "select max(MALOP) AS MALOP FROM DSDKTHE";
            conntemp1.Open();// mở kết nối
            try
            {

                SqlCommand cmtemp1 = new SqlCommand(lenhtemp1, conntemp1);
                SqlDataReader datatemp1 = cmtemp1.ExecuteReader();
                if (datatemp1.HasRows)// nếu thẻ thư viện hợp lệ
                {
                    while (datatemp1.Read())
                    {
                        string temp = datatemp1["MALOP"].ToString();
                        if(temp!="")
                        {
                            malop = int.Parse(temp);
                            malop = malop + 1;
                            BienToanCuc.MALOPHOC = malop.ToString();
                        }
                        else
                        {
                            BienToanCuc.MALOPHOC = "1";
                        }
                    }
                    conntemp1.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
            }
        }
    }
}
