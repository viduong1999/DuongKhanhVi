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

        //  Quay lai trang chu
        private void button2_Click(object sender, EventArgs e)
        {
            // tắt form đăng nhập
            this.Dispose();
        }

        //Đăng nhập
        private void button1_Click(object sender, EventArgs e)
        {
            // kiểm tra thông tin đầu vào khong được trống
            if (MaNV.Text == "" || Password.Text == "")
            {
                MessageBox.Show("MaNV hoặc mật khẩu không được để trống");
            }
            else // nếu đã điền đầy đủ thì tiến hành kiểm tra đăng nhập
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
                        MessageBox.Show("Đăng nhập thành công");
                        // XỬ LÝ LƯU THÔNG TIN
                        while (data.Read())
                        {
                            BienToanCuc.Role = (string)data["CHUCVU"];
                            BienToanCuc.HOTEN= (string)data["HOTEN"];
                        }
                        BienToanCuc.MaNV = MaNV.Text;
                        BienToanCuc.Password = Password.Text;
                        //MessageBox.Show(BienToanCuc.Password);
                        BienToanCuc.flag = true;
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("MaNV hoặc mật khẩu không đúng, vui lòng đăng nhập lại");
                        MaNV.Text = "";
                        Password.Text = "";
                        conn.Close();
                    }
                    // load trang
                    if(BienToanCuc.Role=="Cán bộ quản lý thẻ")
                    {
                        this.Hide();
                        QuanLyDKThe QLDKT = new QuanLyDKThe();
                        QLDKT.ShowDialog();// hiện form nhân viên đăng nhập
                        this.Dispose();
                    }
                    else if(BienToanCuc.Role=="Thủ thư")
                    {
                        this.Hide();
                        Muon_Tra_Sach MTS = new Muon_Tra_Sach();
                        MTS.ShowDialog();
                        this.Dispose();
                    }
                    else if(false)
                    {

                    }
                    else
                    {
                        this.Hide();
                        QuanLySach QLS = new QuanLySach();
                        QLS.ShowDialog();
                        this.Dispose();
                    }
                }
                catch
                {
                    MessageBox.Show("Hệ thống đang gặp sự cố, vui lòng quay lại sau vài phút");
                }

            }
            //CaNhanDaDN_Load(sender, e);
        }
        private void NhanVienDangNhap_Load(object sender, EventArgs e)
        {
            Password.UseSystemPasswordChar = true;//ẩn password
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
