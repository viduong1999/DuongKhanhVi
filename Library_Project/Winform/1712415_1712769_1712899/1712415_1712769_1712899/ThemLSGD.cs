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
    public partial class ThemLSGD : Form
    {
        public ThemLSGD()
        {
            InitializeComponent();
        }

        //Thêm lịch sử giao dịch
        private void button1_Click(object sender, EventArgs e)
        {
            if (MaSach.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã sách");
            }
            else
            {// insert dữ liệu vào bảng TK_MH của database
             // => mở kết nối
                SqlConnection conn = KetNoi.GetDBConnection();
                try
                {
                    conn.Open();
                    string sql = "EXEC sp_InLS '" + BienToanCuc.MATHE + "'," + MaSach.Text;
                    SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                    int data = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                    if(data!=-1)
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại");
                    }
                    conn.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                }
            }
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
