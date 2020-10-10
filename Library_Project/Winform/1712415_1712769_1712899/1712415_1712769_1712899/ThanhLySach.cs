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
    public partial class ThanhLySach : Form
    {
        public ThanhLySach()
        {
            InitializeComponent();
        }

        // tra cứu danh mục thanh lý
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DanhMucThanhLy DMTL = new DanhMucThanhLy();
            DMTL.ShowDialog();
            this.Show();
        }
        //load form danh sách thanh lý sách
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            DanhSachThanhLy DSTL = new DanhSachThanhLy();
            DSTL.ShowDialog();
        }

        //Tạo danh mục thanh lý
        private void button3_Click(object sender, EventArgs e)
        {
            TaoDanhMucThanhLy TDMTL = new TaoDanhMucThanhLy();
            TDMTL.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            DanhMucThanhLyDaDuyet DMTLDD = new DanhMucThanhLyDaDuyet();
            DMTLDD.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
