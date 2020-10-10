using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _1712415_1712769_1712899
{
    public partial class ThongTinKhachHang : Form
    {
        public ThongTinKhachHang()
        {
            InitializeComponent();
        }
        //Hàm thêm hoá đơn
        private static int ThemHoaDon(string sql)
        {
            // => mở kết nối
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                int data = cmd.ExecuteNonQuery();
                conn.Close();
                return data;
            }
            catch (Exception)
            {
                MessageBox.Show(sql);
                MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                return 0;
            }
        }
        //Thêm hoá đơn
        private bool KiemTraThemHoaDon(string sql)
        {
            int data;
            data = ThemHoaDon(sql);
            if (data == 1)//thêm thành công
            {
                return true;
            }
            else
            {
               // MessageBox.Show("Đặt hàng thất bại");
                return false;
            }
        }
        //Lấy mã hoá đơn
        private DataSet LayMaHoaDon()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh = "select max(MAHD) as MAHD from HOADON";
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
                da.Fill(ds);// đổ dữ liệu vào data set
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui, lòng chọn lại sau");
            }
            return ds;
        }
        //kiểm tra mã hoá đơn
        private int MaHoaDon()
        {
            DataSet ds=new DataSet();
            ds = LayMaHoaDon();
            return int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString());
        }
        //Hàm thêm chi tiết hoá đơn
        private int ThemHD_MH(string sql)
        {
            // => mở kết nối
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                int data = cmd.ExecuteNonQuery();
                //  if (data1 != 0) MessageBox.Show("Thêm vào giỏ hàng thành công");
                // else MessageBox.Show("Thêm vào giỏ hàng thất bại");
                conn.Close();
                return data;
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                return 0;
            }
        }
        //kiểm tra thêm chi tiết hoá đơn
        private bool KiemTraThemChiTietHoaDon(string sql)
        {
            int data;
            data = ThemHD_MH(sql);
            if (data > 0)//thêm thành công
            {
                return true;
            }
            else
            {
                // MessageBox.Show("Đặt hàng thất bại");
                return false;
            }
        }
        //tiến hành đặt hàng
        private void DatHang_Click(object sender, EventArgs e)
        {
            //kiểm tra có thanh toán thẻ không
            // nếu có thì yêu cầu nhập mã thẻ ngược lại thì set chế độ readonly cho mã thẻ
            if (BienToanCuc.flag2)
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mã thẻ");
                    return;
                }
            }
            else
            {
                textBox4.ReadOnly = true;
            }
            //kiểm tra thông tin đầy đủ chưa
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            string sql;
            //tiến hành đặt hàng
            if (BienToanCuc.flag2)//có thẻ
            {
                sql = "exec ThemHoaDon '"+textBox1.Text+"','"+textBox2.Text+"',N'" +
                    textBox3.Text+"',"+BienToanCuc.tongtien+", '"+textBox4.Text+"'";
            }
            else//không thẻ
            {
                sql = "exec ThemHoaDon '" + textBox1.Text + "', '" + textBox2.Text + "', " +
    "N'" + textBox3.Text + "', "+BienToanCuc.tongtien+", null";
            }
            //đặt hàng
            if(KiemTraThemHoaDon(sql))//thêm hoá đơn thành công
            {
                //lấy mã hoá đơn;
                int MaHD = MaHoaDon();
                if(MaHD<1)
                {
                    MessageBox.Show("Đặt hàng thất bại, vui lòng thử lại sau");
                }
                //thêm chi tiết hoá đơn
                for(int i=0;i<BienToanCuc.length;i++)
                {
                    string sql1 = "exec ThemChiTietHoaDon " + MaHD.ToString() +
                        ", "+BienToanCuc.MAMH[i]+", "+BienToanCuc.SoLuong[i]+", "+BienToanCuc.Gia[i];
                    bool KTHD_MH = KiemTraThemChiTietHoaDon(sql1);
                    if(KTHD_MH==false)
                    {
                        MessageBox.Show("Đặt hàng thất bại, vui lòng thử lại sau");
                        return;
                    }
                }
                MessageBox.Show("Đặt hàng thành công");
                //xoá giỏ hàng tạm thời
                BienToanCuc.length = 0;
                this.Dispose();
            }
        }

        private void ThongTinKhachHang_Load(object sender, EventArgs e)
        {
            if(BienToanCuc.flag2==false)
            {
                textBox4.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
