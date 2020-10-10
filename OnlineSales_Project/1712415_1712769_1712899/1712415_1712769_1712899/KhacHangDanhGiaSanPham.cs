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
    public partial class KhacHangDanhGiaSanPham : Form
    {
        public KhacHangDanhGiaSanPham()
        {
            InitializeComponent();
        }

        //Hàm dọc dữ liệu tầng nghiệp vụ
        private DataSet XemMatHang(string lenh)
        {
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
            return ds;
        }
        //Hàm lấy danh sách sản phẩm ở tầng nghiệp vụ
        private DataSet LayDanhSachSanPham()
        {
            DataSet ds = new DataSet();
            string lenh;
            if (text_TimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng cung cấp thông tin sàn phẩm bạn muốn tìm kiếm");
                return ds;
            }
            else
            {
                lenh = "select MH.MAMH, MH.TENMH, MH.GIA, LOAI.TENLOAI, NCC.TENNCC " +
                    "from MATHANG MH, LOAI, NHACUNGCAP NCC" +
                    " where MH.TENMH = N'" + text_TimKiem.Text + "' AND MH.LOAI = LOAI.MALOAI" +
                    " AND NCC.MANCC = MH.MANCC";
            }
            ds = XemMatHang(lenh);
            return ds;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            DataSet ds = LayDanhSachSanPham();
            // xoá dữ liệu có sẳn trên list view và dataset
            listView2.Items.Clear();
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả");
                return;
            }
            //đọc dữ liệu từ dataset và add vào listview
            for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
            {
                listView2.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView2.SelectedItems)
            {
                textBox1.Text = items.SubItems[0].Text;
                textBox8.Text = items.SubItems[1].Text;
                textBox3.Text = items.SubItems[2].Text.Remove(items.SubItems[2].Text.Length - 5, 5);
                textBox2.Text = items.SubItems[3].Text;
                textBox4.Text = items.SubItems[4].Text;
            }
        }

        //Đọc loại hàng
        private DataSet XemLoaiHang()
        {
            //select * from LOAI
            // hiện listview2
            listView1.Visible = true;
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh;
            lenh = "select TENLOAI from LOAI";
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
        //Hàm kiểm tra loại hàng
        private DataSet TraCuuLoaiHang()
        {
            DataSet ds = new DataSet();
            ds = XemLoaiHang();
            return ds;

        }
        //tìm kiếm loại hàng
        private void button2_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = TraCuuLoaiHang();
            // xoá dữ liệu có sẳn trên list view và dataset
            listView1.Items.Clear();
            listView2.Items.Clear();
            //đọc dữ liệu từ dataset và add vào listview
            for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
            {
                listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
            }
        }

        private DataSet XemMatHangTheoLoai(string lenh)
        {
            DataSet ds = new DataSet();
            ds = XemMatHang(lenh);
            return ds;
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                string temp = items.SubItems[0].Text;
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
                string lenh = "select MH.MAMH, MH.TENMH, MH.GIA, LOAI.TENLOAI, " +
                    "NCC.TENNCC, MH.SLTON FROM MATHANG MH, LOAI, NHACUNGCAP NCC " +
                    "WHERE LOAI.TENLOAI=N'" + temp + "' and LOAI.MALOAI=MH.LOAI  " +
                    "AND NCC.MANCC = MH.MANCC";
                try
                {
                    ds = XemMatHangTheoLoai(lenh);
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView2.Items.Clear();
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView2.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                        listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                        listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
                        listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }
            }

        }
        //Hàm đọc thông tin từ danh sách đen
        private DataSet DocThongTin(string email)
        {
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh;
            lenh = "select * from DSDEN where EMAIL='"+email+"'";
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
        //Hàm kiểm tra email có tồn tại trong danh sách đen hay ko
        //nếu có thì trả về false
        private bool KiemTraThongTin(string email)
        {
            DataSet ds = new DataSet();
            ds = DocThongTin(email);
            if(ds.Tables[0].Rows.Count != 0)
            {
                return false;
            }
            return true;
        }
        //Hàm thêm cmt vào sản phẩm
        private int ThemCmt()
        {
            int data1=0;
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();
                string sql = "EXEC sp_InTKCmt N'"+BienToanCuc.HoTenDanhGia+
                    "',"+textBox1.Text+",'"+BienToanCuc.EmailDanhGia+
                    "',N'"+BienToanCuc.DiaChiDanhGia+"',N'"+textBox5.Text+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                data1 = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống đang gặp sự cố vui lòng chọn lại sau");
            }
            return data1;
        }
        //Hàm thêm thông tin cmt
        private void ThemThongTinCMT()
        {
            int kt = ThemCmt();
            if (kt!=0 && kt!= -1)
            {
                MessageBox.Show("Thêm đánh giá thành công");
                return;
            }
            MessageBox.Show("Thêm đánnh giá thất bại");
        }
        //Đánh giá
        private void button8_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Vui lòng chọn mặt hàng muốn đánh giá");
                return;
            }
            else if(textBox5.Text=="")
            {
                MessageBox.Show("Vui lòng nhập nội dung muốn đánh giá");
                return;
            }
            if(KiemTraThongTin(BienToanCuc.EmailDanhGia))
            {
                //Tiến hành đánh giá
                ThemThongTinCMT();
            }
            else
            {
                MessageBox.Show("Email của bạn đã bị chặn tính năng này");
            }
        }
    }
}
