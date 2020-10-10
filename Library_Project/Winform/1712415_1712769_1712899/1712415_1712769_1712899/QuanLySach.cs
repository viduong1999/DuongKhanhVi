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
    public partial class QuanLySach : Form
    {
        public QuanLySach()
        {
            InitializeComponent();
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            textBox1.Text = BienToanCuc.HOTEN;
            textBox2.Text = BienToanCuc.Role;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaoSachMoiNhap TSMN = new TaoSachMoiNhap();
            TSMN.ShowDialog();
        }
    }
}
