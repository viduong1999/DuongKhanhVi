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
    public partial class GioHang : Form
    {
        public GioHang()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();// tắt trang giỏ hàng
        }

        //load form trang chủ
        private void GioHang_Load(object sender, EventArgs e)
        {
            long thanhtien = 0;
            listView1.Items.Clear();
            //show tất cả giỏ hàng lên listview
            for (int i=0;i<BienToanCuc.length;i++)
            {
                listView1.Items.Add(BienToanCuc.MAMH[i]);
                listView1.Items[i].SubItems.Add(BienToanCuc.TENMH[i]);
                listView1.Items[i].SubItems.Add(BienToanCuc.TENNCC[i]);
                listView1.Items[i].SubItems.Add(BienToanCuc.TenLoai[i]);
                listView1.Items[i].SubItems.Add(BienToanCuc.Gia[i].ToString());
                listView1.Items[i].SubItems.Add(BienToanCuc.SoLuong[i].ToString());
                thanhtien = thanhtien + (long)BienToanCuc.Thanhtien[i];
            }
            textBox1.Text = thanhtien.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {

                checkBox2.Checked = false;
                BienToanCuc.flag2 = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                checkBox1.Checked = false;
                BienToanCuc.flag2 = true;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                textBox2.Text = items.SubItems[0].Text;
                text_MH.Text = items.SubItems[1].Text;
                text_NCC.Text = items.SubItems[2].Text;
                text_TH.Text = items.SubItems[3].Text;
                text_Gia.Text = items.SubItems[4].Text;
                textBox5.Text = items.SubItems[5].Text;
                long thanhtien=BienToanCuc.Tongtien(int.Parse(items.SubItems[5].Text),long.Parse(text_Gia.Text));
                text_ThanhTien.Text = thanhtien.ToString();
            }
        }

        //thêm số lượng
        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                return;
            }
            //cập nhật số lượng trong giỏ hàng
            for(int i=0;i<BienToanCuc.length;i++)
            {
                if(textBox2.Text == BienToanCuc.MAMH[i])
                {
                    //cập nhật số lương
                    int soluong = BienToanCuc.SoLuong[i];
                    soluong = soluong + 1;
                    BienToanCuc.SoLuong[i] = soluong;
                    textBox5.Text = soluong.ToString();
                    BienToanCuc.Thanhtien[i] = BienToanCuc.Tongtien(soluong, BienToanCuc.Gia[i]);
                    text_ThanhTien.Text = BienToanCuc.Thanhtien[i].ToString();
                }
            }
            GioHang_Load(sender,e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int vt = -1;
            if (textBox2.Text == "")
            {
                return;
            }
            //cập nhật số lượng trong giỏ hàng
            for (int i = 0; i < BienToanCuc.length; i++)
            {
                if (textBox2.Text == BienToanCuc.MAMH[i])
                {
                    //cập nhật số lương
                    int soluong = BienToanCuc.SoLuong[i];
                    soluong = soluong - 1;
                    if (soluong == 0)//xoá khỏi giỏ hàng
                    {
                        vt = 1;
                    }
                    else
                    {
                        BienToanCuc.SoLuong[i] = soluong;
                        textBox5.Text = soluong.ToString();
                        BienToanCuc.Thanhtien[i] = BienToanCuc.Tongtien(soluong, BienToanCuc.Gia[i]);
                        text_ThanhTien.Text = BienToanCuc.Thanhtien[i].ToString();
                    }
                }
            }
            if(vt!=-1)//tiến hành xoá
            {
                // xoá
                for (int i = vt; i < BienToanCuc.length - 1; i++)
                {
                    BienToanCuc.MAMH[i] = BienToanCuc.MAMH[i + 1];
                    BienToanCuc.MANCC[i] = BienToanCuc.MANCC[i + 1];
                    BienToanCuc.TENMH[i] = BienToanCuc.TENMH[i + 1];
                    BienToanCuc.TENNCC[i] = BienToanCuc.TENNCC[i + 1];
                    BienToanCuc.TenLoai[i] = BienToanCuc.TenLoai[i + 1];
                    BienToanCuc.Gia[i] = BienToanCuc.Gia[i + 1];
                    BienToanCuc.SoLuong[i] = BienToanCuc.SoLuong[i + 1];
                    BienToanCuc.Thanhtien[i] = BienToanCuc.Thanhtien[i + 1];

                }
                BienToanCuc.length--;// cập nhật lại length
                textBox5.Text = "";
                text_MH.Text = "";
                text_NCC.Text = "";
                text_TH.Text = "";
                text_Gia.Text = "";
                text_ThanhTien.Text = "";
                textBox2.Text = "";
            }
            GioHang_Load(sender, e);
        }
        //xoá khỏi giỏ hàng
        private void button2_Click(object sender, EventArgs e)
        {
            int vt = -1;
            //tìm vị trí xoá
            if(textBox2.Text=="")
            {
                MessageBox.Show("Vui lòng chọn mặt hàng bạn muốn xoá");
                return;
            }
            for(int i=0;i<BienToanCuc.length;i++)
            {
                if(textBox2.Text==BienToanCuc.MAMH[i])
                {
                    vt = i;
                }
            }
            if(vt!=-1)//xoá
            {
                // xoá
                for (int i = vt; i < BienToanCuc.length - 1; i++)
                {
                    BienToanCuc.MAMH[i] = BienToanCuc.MAMH[i + 1];
                    BienToanCuc.MANCC[i] = BienToanCuc.MANCC[i + 1];
                    BienToanCuc.TENMH[i] = BienToanCuc.TENMH[i + 1];
                    BienToanCuc.TENNCC[i] = BienToanCuc.TENNCC[i + 1];
                    BienToanCuc.TenLoai[i] = BienToanCuc.TenLoai[i + 1];
                    BienToanCuc.Gia[i] = BienToanCuc.Gia[i + 1];
                    BienToanCuc.SoLuong[i] = BienToanCuc.SoLuong[i + 1];
                    BienToanCuc.Thanhtien[i] = BienToanCuc.Thanhtien[i + 1];

                }
                BienToanCuc.length--;// cập nhật lại length
                textBox5.Text = "";
                text_MH.Text = "";
                text_NCC.Text = "";
                text_TH.Text = "";
                text_Gia.Text = "";
                text_ThanhTien.Text = "";
                textBox2.Text = "";
            }
            GioHang_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kiểm tra hình thức thanh toán
            if(checkBox1.Checked==false&&checkBox2.Checked==false)
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán");
                return;
            }
            else if(textBox1.Text=="0")
            {
                MessageBox.Show("Giỏ hàng trống");
                return;
            }
            else// đặt hàng
            {
               BienToanCuc.tongtien = textBox1.Text;
                ThongTinKhachHang TTKH = new ThongTinKhachHang();
                TTKH.ShowDialog();
                GioHang_Load(sender, e);
            }
        }
    }
}
