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
    public partial class XacNhanDonHang : Form
    {
        public XacNhanDonHang()
        {
            InitializeComponent();
        }

        // tìm kiếm hoá đơn
        private void textBox3_Enter(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }
        //Đọc danh sách hoá đơn
        private DataSet DocDanhSachHoaDon()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh = "select* from HOADON where NVBANHANG is null";
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
                da.Fill(ds);// đổ dữ liệu vào data set
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
            return ds;
        }
        private DataSet DSHoaDon()
        {
            DataSet ds = new DataSet();
            ds = DocDanhSachHoaDon();
            return ds;

        }

        //Hiển thị tất cả các hoá đơn chưa xác nhận
        private void XacNhanDonHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hTBanHangDataSet4.XemNhanVien' table. You can move, or remove it, as needed.
            this.xemNhanVienTableAdapter.Fill(this.hTBanHangDataSet4.XemNhanVien);
            DataSet ds = new DataSet();
            ds = DSHoaDon();
            // xoá dữ liệu có sẳn trên list view và dataset
            listView2.Items.Clear();
            //đọc dữ liệu từ dataset và add vào listview
            for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
            {
                listView2.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThongKeCmt TKCMT= new ThongKeCmt();
            TKCMT.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Lấy thông tin hoá đơn
        private DataSet DocThongTinHoaDon(string sql)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);// đổ dữ liệu vào data set
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
            return ds;
        }
        //thông tin hoá đơn
        private DataSet ThongTinHoaDon(string sql)
        {
            DataSet ds = new DataSet();
            ds = DocThongTinHoaDon(sql);
            return ds;

        }

        //Lấy thông tin hoá đơn
        private DataSet DocThongTinChiTietHoaDon(string sql)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);// đổ dữ liệu vào data set
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
            return ds;
        }
        //thông tin hoá đơn
        private DataSet ThongTinChiTietHoaDon(string sql)
        {
            DataSet ds = new DataSet();
            ds = DocThongTinChiTietHoaDon(sql);
            return ds;

        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView2.SelectedItems)
            {
                //Tông tin hoá đơn
                DataSet ds = new DataSet();
                string sql1 = "select* from HOADON where MAHD=" + items.SubItems[0].Text;
                ds = ThongTinHoaDon(sql1);
                textBox1.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                textBox2.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                textBox7.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                textBox8.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                textBox4.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                textBox5.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                //Chi tiết hoá đơn
                string sql2 = "select * from HD_MH, MATHANG MH WHERE MAHD = "+
                    items.SubItems[0].Text+" and HD_MH.MAMH=MH.MAMH";
                DataSet ds1 = new DataSet();
                ds1 = ThongTinChiTietHoaDon(sql2);
                listView1.Items.Clear();
                for (int rows = 0; rows < ds1.Tables[0].Rows.Count; rows++)
                {
                    listView1.Items.Add(ds1.Tables[0].Rows[rows].ItemArray[1].ToString());
                    listView1.Items[rows].SubItems.Add(ds1.Tables[0].Rows[rows].ItemArray[2].ToString());
                    listView1.Items[rows].SubItems.Add(ds1.Tables[0].Rows[rows].ItemArray[7].ToString());
                    listView1.Items[rows].SubItems.Add(ds1.Tables[0].Rows[rows].ItemArray[3].ToString());
                    listView1.Items[rows].SubItems.Add(ds1.Tables[0].Rows[rows].ItemArray[4].ToString());
                    listView1.Items[rows].SubItems.Add(ds1.Tables[0].Rows[rows].ItemArray[5].ToString());
                }
            }
        }

        private static int CapNhatHoaDon(string sql)
        {
            int data = 0;
            // insert dữ liệu vào bảng TK_MH của database
            // => mở kết nối
            SqlConnection conntemp = KetNoi.GetDBConnection();
            try
            {
                conntemp.Open();
                SqlCommand cmd = new SqlCommand(sql, conntemp);// vận chuyển câu lệnh 
                data = cmd.ExecuteNonQuery();
                conntemp.Close();
                return data;
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                return 0;
            }
        }
        //kiểm tra thêm vào danh sách đen
        private bool KiemTraCapNhatHoaDon(string sql)
        {
            int data;
            data = CapNhatHoaDon(sql);
            if (data == 1)//thêm thành công
            {
                return true;
            }
            else if (data == 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        //Xác nhận đơn hàng
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Vui lòng chọn hoá đơn muốn cập nhật");
                return;
            }
            else if(comboBox1.Text=="")
            {
                MessageBox.Show("Vui lòng chọn nhân viên giao hàng");
                return;
            }
            string sql = "EXEC CapNhatHoaDon "+textBox1.Text+", "+BienToanCuc.MaNV+
                ", N'"+comboBox1.Text+"'";
            bool kt = KiemTraCapNhatHoaDon(sql);
            if(kt)
            {
                MessageBox.Show("Xác nhận đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Xác nhận đơn hàng thất bại, vui lòng thử lại sau");
            }
            XacNhanDonHang_Load(sender,e);
        }

        //Tìm Kiếm
        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox3.Text=="")
            {
                MessageBox.Show("Vui lòng nhập mã hoá đơn");
            }
            //Tông tin hoá đơn
            DataSet ds = new DataSet();
            string sql = "select* from HOADON where MAHD=" + textBox3.Text+ "and NVGIAOHANG is null";
            ds = ThongTinHoaDon(sql);
            if(ds.Tables[0].Rows.Count==0)
            {
                MessageBox.Show("Khong tìm thấy mã hoá đơn");
            }
            listView2.Items.Clear();
            //đọc dữ liệu từ dataset và add vào listview
            for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
            {
                listView2.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
            }

        }
    }
}
