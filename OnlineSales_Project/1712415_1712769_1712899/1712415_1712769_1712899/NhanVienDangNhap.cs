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
    public partial class NhanVienDangNhap : Form
    {
        public NhanVienDangNhap()
        {
            InitializeComponent();
        }
        //Hàm đọc thông tin
        private void DocThongTinNhanVien()
        {
            // lấy kết nối cơ sở dữ liệu
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();// mở kết nối
                string sql = "SELECT* FROM NHANVIEN AS NV WHERE ((NV.MANV=" + MaNV.Text + ")AND(NV.MATKHAU='" + Password.Text + "'))";
                SqlCommand cm = new SqlCommand(sql, conn);
                SqlDataReader data = cm.ExecuteReader();
                if (data.HasRows)
                {
                    // XỬ LÝ LƯU THÔNG TIN
                    while (data.Read())
                    {
                        BienToanCuc.Role = (string)data["CHUCVU"];
                        //MessageBox.Show(BienToanCuc.Role);
                    }
                    BienToanCuc.MaNV = MaNV.Text;
                    BienToanCuc.Password = Password.Text;
                    BienToanCuc.flag = true;
                    MaNV.Text = "";
                    Password.Text = "";
                    //groupBox2.Visible = true;
                    //groupBox1.Visible = false;
                    //groupBox3.Visible = true;
                    conn.Close();
                }
                else
                {
                    MaNV.Text = "";
                    Password.Text = "";
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Hệ thống đang gặp sự cố, vui lòng quay lại sau vài phút");
            }
        }
        //Hàm kiểm tra
        private bool KiemTraThongTinNhanVien()
        {
            // đọc thông tin
            DocThongTinNhanVien();
            if(BienToanCuc.MaNV!="")
            {
                return true;
            }
            return false;
        }
        //login
        private void Login_Click(object sender, EventArgs e)
        {
            // kiểm tra thông tin đầu vào khong được trống
            if (MaNV.Text == "" || Password.Text == "")
            {
                MessageBox.Show("MaNV hoặc password không được để trống");
                MaNV.Text = "";
                Password.Text = "";
            }
            else // nếu đã điền đầy đủ thì tiến hành kiểm tra đăng nhập
            {
                if(KiemTraThongTinNhanVien())
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("MaNV hoặc password không đúng, vui lòng đăng nhập lại");
                }
            }
            //CaNhanDaDN_Load(sender, e);
            this.Dispose();
        }

        private void NhanVienDangNhap_Load(object sender, EventArgs e)
        {
            Password.UseSystemPasswordChar = true;//ẩn password
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();//Thoát form
        }
    }
}
