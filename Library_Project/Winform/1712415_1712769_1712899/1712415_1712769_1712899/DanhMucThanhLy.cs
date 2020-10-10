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
    public partial class DanhMucThanhLy : Form
    {
        public DanhMucThanhLy()
        {
            InitializeComponent();
        }

        //tạo danh mục thanh lý
        private void button3_Click(object sender, EventArgs e)
        {
            TaoDanhMucThanhLy TDMTL = new TaoDanhMucThanhLy();
            TDMTL.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DanhSachThanhLy DSTL = new DanhSachThanhLy();
            DSTL.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DanhMucThanhLyDaDuyet DMTLDD = new DanhMucThanhLyDaDuyet();
            this.ShowDialog();
        }
    }
}
