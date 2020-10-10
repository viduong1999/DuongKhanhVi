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
    public partial class ChinhSachDoiTra : Form
    {
        public ChinhSachDoiTra()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // quay về trang chủ
            this.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();// ẩn tab chính sách đổi trả
            ChinhSachBaoHanh CSBH = new ChinhSachBaoHanh();
            CSBH.ShowDialog();
            this.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // quản lí đơn hàng
            this.Hide();
            // ẩn tab chính sách bảo hành
            QuanLiDonHang QLDH = new QuanLiDonHang();
            QLDH.ShowDialog();// chuyển sang chính sách đổi trả và thoi tác trên csdt
            this.Close();// đóng tb sau khi thực hiện xong các thao tác trên tab chính sách đổi trả
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Trang cá nhân
            this.Hide();
            // ẩn tab chính sách bảo hành
            CaNhanDaDN CN = new CaNhanDaDN();
            CN.ShowDialog();// chuyển sang chính sách đổi trả và thoi tác trên csdt
            this.Close();// đóng tb sau khi thực hiện xong các thao tác trên tab chính sách đổi trả
        }
    }
}
