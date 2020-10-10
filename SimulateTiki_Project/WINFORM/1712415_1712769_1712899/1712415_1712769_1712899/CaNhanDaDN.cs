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
    public partial class CaNhanDaDN : Form
    {
        public CaNhanDaDN()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DKTK_KH DK = new DKTK_KH();// đi đến bảng đăng ki
            DK.ShowDialog();
            if (BienToanCuc.flag)
            {
                groupBox2.Visible = true;
                groupBox1.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                CaNhanDaDN_Load(sender, e);
            }
            // load lại trang đăng nhập
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();// về trang chủ
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Tên tài khoản hoặc password không được để trống");
            }
            else
            {
               
                // Đăng nhập
                SqlConnection conn = KetNoi.GetDBConnection();
                try
                {
                    conn.Open();// mở kết nối
                    string sql = "select * from TKKHACHHANG WHERE TENTK=N'" + textBox1.Text + "' AND MATKHAU=N'" + textBox2.Text + "'";
                    SqlCommand cm = new SqlCommand(sql, conn);
                    SqlDataReader data = cm.ExecuteReader();
                    if (data.HasRows)
                    {
                        conn.Close();
                        MessageBox.Show("Đăng nhập thành công");
                        // XỬ LÝ LƯU THÔNG TIN
                        BienToanCuc.TenTK = textBox1.Text;
                        BienToanCuc.Password = textBox2.Text;
                        BienToanCuc.flag = true;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        groupBox2.Visible = true;
                        groupBox1.Visible = false;
                        groupBox3.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản hoặc password không đúng vui lòng đăng nhập lại");
                        conn.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Hệ thống đang gặp sự cố vui lòng quay lại sau vài phút");
                }

            }
            CaNhanDaDN_Load(sender, e);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // thay pass
            groupBox4.Visible = false;// ẩn bảng cập nhật
            groupBox3.Visible = false;// ẩn bảng thông tin tài khoản
            //groupBox4.Visible = false;// ẩn bảng cập nhật
            groupBox5.Visible = true;// hiện  bảng thay đổi pass
        }

        private void button9_Click(object sender, EventArgs e)
        {

            // cập nhật thông tin
            groupBox4.Visible = true;// hiện bảng cập nhật
            groupBox3.Visible = false;// ẩn bảng thông tin tài khoản
            //groupBox4.Visible = false;// ẩn bảng cập nhật
            groupBox5.Visible = false;// ẩn bảng thay đổi pass

        }
        private void CaNhanDaDN_Load(object sender, EventArgs e)
        {
            
            if (BienToanCuc.flag)
            {
                // đọc dữ liệu lên textbox
                //DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                //string sql = "select * from TKKHACHHANG, DIACHIKH WHERE TKKHACHHANG.TENTK=N'"+BienToanCuc.TenTK+"' AND ";
                //sql = sql + "TKKHACHHANG.MATK=DIACHIKH.MATK";
                string sql = "select * from TKKHACHHANG,DIACHIKH WHERE TKKHACHHANG.TENTK=N'"+BienToanCuc.TenTK+"' AND" +
                    " TKKHACHHANG.MATK=DIACHIKH.MATK";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);//conn là cái SqlConnection của bạn ấy nhé
                    DataSet dt = new DataSet();
                    da.Fill(dt);
                    conn.Close();
                    text_hoten.Text=textBox3.Text = dt.Tables[0].Rows[0]["HOTEN"].ToString();
                    text_email.Text=text_Email1.Text= dt.Tables[0].Rows[0]["EMAIL"].ToString();
                    text_gioitinh.Text= dt.Tables[0].Rows[0]["GIOITINH"].ToString();
                    text_ngaysinh.Text= dt.Tables[0].Rows[0]["NGAYSINH"].ToString();
                    text_diachi.Text= dt.Tables[0].Rows[0]["DIACHI"].ToString();
                    text_phuong.Text= dt.Tables[0].Rows[0]["PHUONG"].ToString();
                    text_huyen.Text= dt.Tables[0].Rows[0]["QUAN_HUYEN"].ToString();
                    text_tinh.Text= dt.Tables[0].Rows[0]["TINH_TP"].ToString();

                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng quay lại sau");
                }

                // XỬ LÝ TẠO GIỎ HÀNG
                if (BienToanCuc.length == 0)// NẾU GIỎ HÀNG TẠM THỜI CHƯA CÓ GÌ THÌ KO LÀM GÌ CẢ
                {

                }
                else // ngược lại thì tiến hành lưu giỏ hàng xuống database
                {
                    SqlConnection conn1 = KetNoi.GetDBConnection();
                    conn1.Open();// mở kết nối
                    for (int i = 0; i < BienToanCuc.length; i++)
                    {
                        string sql2 = "EXEC INSERT_GIOHANG N'" + BienToanCuc.TenTK + "'," + BienToanCuc.MAMH[i] + "," + BienToanCuc.MANCC[i] + "," + BienToanCuc.SoLuong[i].ToString();
                        SqlCommand cmm2 = new SqlCommand(sql2, conn1);
                        int da = cmm2.ExecuteNonQuery();// PHẢI CÓ DÒNG NÀY NÓ MỚI VẬN CHUYỂN
                    }
                    conn1.Close();
                    BienToanCuc.length = 0;// xoá giỏ hàng tạm thời
                }
            }
            else
            {
                groupBox2.Visible=false;// ẩn groupsbox đăng nhập rồi
                groupBox3.Visible=false;// ẩn bảng thông tin tài khoản
                groupBox4.Visible=false;// ẩn bảng cập nhật
                groupBox5.Visible=false;// ẩn bảng thay đổi pass
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();// ẩn tab cá nhân
            ChinhSachBaoHanh CSBH = new ChinhSachBaoHanh();
            CSBH.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            // ẩn tab cá nhân
            ChinhSachDoiTra CSDT = new ChinhSachDoiTra();
            CSDT.ShowDialog();// chuyển sang chính sách đổi trả và thoi tác trên csdt
            this.Close();// đóng tb sau khi thực hiện xong các thao tác trên tab chính sách đổi trả
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // quản lí đơn hàng
            this.Hide();
            // ẩn tab chính sách bảo hành
            QuanLiDonHang QLDH = new QuanLiDonHang();
            QLDH.ShowDialog();// chuyển sang chính sách đổi trả và thoi tác trên csdt
            this.Close();// đóng tb sau khi thực hiện xong các thao tác trên tab quan lí đơn hàng
        }


        private void button11_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox3.Visible = true;
            CaNhanDaDN_Load(sender, e);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // thoát khỏi bảng thay đổi mật khẩu
            groupBox5.Visible = false;
            groupBox3.Visible = true;
            CaNhanDaDN_Load(sender, e);
        }

        private void button13_MouseClick(object sender, MouseEventArgs e)
        {
            // thoát khỏi bảng thay đổi mật khẩu
            groupBox5.Visible = false;
            groupBox3.Visible = true;
            CaNhanDaDN_Load(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // CẬP NHẬT THÔNG TIN TK
            DateTime date = NgaySinh.Value;
            string ngay = date.Day.ToString();
            string thang = date.Month.ToString();
            string nam = date.Year.ToString();
            string ngaysinh = nam + "-" + thang + "-" + ngay;
            string gioitinh;
            if (checkBox1.Checked == true)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }

            if (textBox4.Text=="")
            { MessageBox.Show("Vui lòng điền họ tên"); textBox4.Focus(); }
            else if (checkBox1.Checked == false && checkBox2.Checked == false) { MessageBox.Show("vui lòng chọn giới tính"); }
            else if (textBox5.Text == "") { MessageBox.Show("Thông tin Email không được trống"); textBox5.Focus(); }
            else if (textBox6.Text == "") { MessageBox.Show("Vui lòng điền địa chỉ nhà"); textBox6.Focus(); }
            else if (textBox7.Text == "") { MessageBox.Show("Vui lòng điền tên phường của bạn"); textBox7.Focus(); }
            else if (text_QuanHuyen.Text == "")
            { MessageBox.Show("Vui lòng điền tên quận/huyện của bạn"); text_QuanHuyen.Focus(); }
            else if (text_TinhThanhPho.Text == "")
            { MessageBox.Show("Vui lòng điền tên tỉnh, thành phố của bạn"); text_TinhThanhPho.Focus(); }
            else// đã điền đầy đủ thông tin thì tiến hành cập nhật
            {
                SqlConnection conn = KetNoi.GetDBConnection();// KẾT NÓI DATABASE
                string sql = "exec CAPNHAT_TKKH  N'"+BienToanCuc.TenTK+"', N'"+textBox4.Text+"', N'"
                    +gioitinh+"', '"+ngaysinh+"',";
                sql = sql + "N'" + textBox5.Text + "', N'" + textBox6.Text + "', N'" + textBox7.Text + "', N'"
                    + text_QuanHuyen.Text + "', N'";
                sql = sql + text_TinhThanhPho.Text + "'";
                try
                {
                    conn.Open();// mở kết nối
                    SqlCommand cm = new SqlCommand(sql, conn);// vận chuyển câu lệnh
                    int data = cm.ExecuteNonQuery();// kết quả trả về là số dòng bị thay đổi
                    if (data != 0)// nếu đăng kí thành công
                    {
                        MessageBox.Show("Cập nhật thành công");
                        //CaNhanDaDN_Load(sender, e);
                        conn.Close();
                        checkBox1.Checked = checkBox2.Checked = false;
                        textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = text_QuanHuyen.Text = text_TinhThanhPho.Text = "";
                        groupBox4.Visible = false;// ẩn bảng cập nhật
                        groupBox3.Visible = true;// hiện bảng thông tin
                        CaNhanDaDN_Load(sender, e);

                    }
                    else
                    {
                        MessageBox.Show("Quá trình cập nhật xảy ra lỗi vui lòng cập nhật lại lại");
                        conn.Close();
                    }

                }
                catch
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng quay lại sau");
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//=>done
        {
            // chỉ được chọn 1 trong 2 (Nam, Nữ)
            if (checkBox1.Checked == true) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//=>done
        {
            // chỉ được chọn 1 trong 2 (Nam, Nữ)
            if (checkBox2.Checked == true) checkBox1.Checked = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // thay đổi pass
            if(text_oldpass.Text=="")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại của bạn");
                text_oldpass.Focus();
            }
            else if(text_newpass.Text=="")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mà bạn muốn đổi");
                text_newpass.Focus();
            }
            else if(text_confim.Text=="")
            {
                MessageBox.Show("vui lòng xác nhận lại mật khẩu");
                text_confim.Focus();
            }
            else
            {
                if(text_oldpass.Text!=BienToanCuc.Password)
                {
                    MessageBox.Show("Mật khẩu không đúng vui lòng nhập lại");
                    text_oldpass.Focus();
                }
                else if(text_newpass.Text!=text_confim.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không đúng");
                }
                else// tiến hành cập nhật
                {
                    SqlConnection conn = KetNoi.GetDBConnection();// KẾT NÓI DATABASE
                    string sql = "exec UPDATE_PASS N'"+BienToanCuc.TenTK+"', N'"+text_newpass.Text+"'";
                    try
                    {
                        conn.Open();// mở kết nối
                        SqlCommand cm = new SqlCommand(sql, conn);// vận chuyển câu lệnh
                        int data = cm.ExecuteNonQuery();// kết quả trả về là số dòng bị thay đổi
                        if (data != 0)// cập nhật thành công
                        {
                            MessageBox.Show("cật nhật thành công");
                            BienToanCuc.Password = text_newpass.Text;
                            text_oldpass.Text =text_newpass.Text=text_confim.Text= "";
                            groupBox2.Visible = true;
                            groupBox5.Visible = false;
                            groupBox3.Visible = true;

                        }
                        else
                        {
                            MessageBox.Show("quá trình cập nhật thất bại");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Hệ thống đang gặp sự cố vui lòng quay lại sau");
                    }
                }
            }
        }
    }
}
