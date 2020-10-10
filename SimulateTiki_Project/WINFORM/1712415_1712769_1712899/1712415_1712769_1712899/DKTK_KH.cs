using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;// kết nối sql
/*
 */
namespace _1712415_1712769_1712899
{
    public partial class DKTK_KH : Form
    {
        public DKTK_KH()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // thoát
            this.Close();
        }

        private void DKTK_Enter(object sender, EventArgs e)
        {
            // đưa con trỏ chuột đến textbox
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

        private void button1_Click(object sender, EventArgs e)
        {
            //đăng kí thông tin khách hàng
            // nếu điền đủ thông tin thì tiến hành đăng kí
            // lấy thông tin ngày giờ
            DateTime date = NgaySinh.Value;
            string ngay = date.Day.ToString();
            string thang = date.Month.ToString();
            string nam = date.Year.ToString();
            //MessageBox.Show(ngay);

            SqlConnection conn = KetNoi.GetDBConnection();// kết nói

            //=> kiểm tra thông tin đăng kí không được trống

            if (text_TenTK.Text == "") { MessageBox.Show("Vui lòng điền tên tài khoản"); text_TenTK.Focus(); }
            else if (text_Password.Text == "") { MessageBox.Show( "Vui lòng nhập password"); text_Password.Focus(); }
            else if (text_HoTen.Text == "") { MessageBox.Show("Vui lòng điền họ tên");text_HoTen.Focus(); }
            else if(checkBox1.Checked==false && checkBox2.Checked==false){ MessageBox.Show("vui lòng chọn giới tính"); }
            else if (text_Email.Text == "") { MessageBox.Show( "Thông tin Email không được trống"); text_Email.Focus(); }
            else if (text_DiaChi.Text == "") { MessageBox.Show("Vui lòng điền địa chỉ nhà"); text_DiaChi.Focus(); }
            else if (text_Phuong.Text == "") { MessageBox.Show( "Vui lòng điền tên phường của bạn"); text_Phuong.Focus(); }
            else if (text_QuanHuyen.Text=="")
            { MessageBox.Show( "Vui lòng điền tên quận/huyện của bạn"); text_QuanHuyen.Focus(); }
            else if(text_TinhThanhPho.Text=="")
            { MessageBox.Show("Vui lòng điền tên tỉnh, thành phố của bạn"); text_TinhThanhPho.Focus(); }
            else// trường hơp thông tin đã điền đầy đủ bây giờ kiểm tra trùng lắp
            {
                try
                { 
                conn.Open();
                // tên tài khoản không được trùng
                string sql1 = "select * from TKKHACHHANG where TENTK = N'" + text_TenTK.Text+"'";
                SqlCommand cm1 = new SqlCommand(sql1, conn);
                SqlDataReader data = cm1.ExecuteReader();

                    /*
                     SqlDataReader.HasRows() cho biết có dòng dữ liệu nào không
                     SqlDataReader.Read() nạp dữ liệu dòng tiếp theo,
                     nếu trả về true là có dòng dữ liệu nạp về thành công,
                     nếu false là đã hết dữ liệu nạp về. Sau khi gọi phương thực này
                     thì các cột của dòng có thể đọc bằng các toán tử [cột],
                     hoặc các hàm đọc dữ liệu như .GetInt32(cột), .GetString(cột) ...
                     */
                    if (data.HasRows)// kiểm tra xem có dòng dữ liệu nào không, nếu có thì thông báo
                    {
                        MessageBox.Show("Tên tài khoản này đã có người sử dụng, vui lòng nhập tên khác");
                        text_TenTK.Focus();
                    }
                    else// ngược lại thì insert dữ liệu vào database và thông báo đăng kí thành công
                    {
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
                        string sql2 = "EXEC DangKiTaiKhoan N'" + text_HoTen.Text + "', N'" + gioitinh + "', '" + ngaysinh + "', N'";
                        sql2 = sql2 + text_Email.Text + "', N'" + text_TenTK.Text + "', N'" + text_Password.Text + "', N'" + text_DiaChi.Text + "', N'";
                        sql2 = sql2 + text_Phuong.Text + "', N'" + text_QuanHuyen.Text + "', N'" + text_TinhThanhPho.Text + "'";
                        SqlConnection conn1 = KetNoi.GetDBConnection();
                        conn1.Open();
                        SqlCommand cm2 = new SqlCommand(sql2, conn1);// vận chuyển câu lệnh
                        int data1 = cm2.ExecuteNonQuery();// kết quả trả về là số dòng bị thay đổi
                        if (data1 != 0)// nếu đăng kí thành công
                        {
                            MessageBox.Show("Đăng kí tài khoản thành công");
                            // lưu thông tin tài khoản
                            BienToanCuc.flag = true;
                            BienToanCuc.TenTK = text_TenTK.Text;
                            BienToanCuc.Password = text_Password.Text;
                            //CaNhanDaDN_Load(sender, e);
                            conn1.Close();
                            this.Close();
                            
                        }
                        else
                        {
                            MessageBox.Show("Quá trình đăng kí xảy ra lỗi vui lòng đăng kí lại");
                            conn1.Close();
                        }
                    }
                }catch
                {
                    MessageBox.Show("Hệ thống đang gặp dự cố vui lòng quay lại sau");
                }
            }
        }

        private void NgaySinh_ValueChanged(object sender, EventArgs e)
        {
            // không làm gì cả
        }

        private void DKTK_KH_Load(object sender, EventArgs e)
        {
            text_TenTK.Focus();
            text_Password.Focus();
            text_HoTen.Focus();
            text_Email.Focus();
            text_DiaChi.Focus();
            text_Phuong.Focus();
            text_QuanHuyen.Focus();
            text_TinhThanhPho.Focus();

        }
    }
}
