using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1712415_1712769_1712899
{
    public partial class ThongTinKHDanhGia : Form
    {
        public ThongTinKHDanhGia()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Lưu tạm thông tin khách hàng để đánh giá sản phẩm
        private void Login_Click(object sender, EventArgs e)
        {
            //kiểm tra xem có để trống hay không
            if(textBox1.Text==""|| textBox2.Text == ""|| textBox3.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin bên dưới");
            }
            else
            {
                BienToanCuc.HoTenDanhGia = textBox1.Text;
                BienToanCuc.EmailDanhGia = textBox2.Text;
                BienToanCuc.DiaChiDanhGia = textBox3.Text;
                BienToanCuc.flag1 = true;
                this.Dispose();
            }
        }
    }
}
