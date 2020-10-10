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
using System.Drawing.Design;


namespace _1712415_1712769_1712899
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        //Hàm dọc dữ liệu tầng nghiệp vụ
        private DataSet DocDanhSachSanPham(string lenh)
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
                lenh = "select MH.MAMH, MH.TENMH, MH.GIA, LOAI.TENLOAI, NCC.TENNCC, MH.SLTON " +
                    "from MATHANG MH, LOAI, NHACUNGCAP NCC" +
                    " where MH.TENMH = N'" + text_TimKiem.Text + "' AND MH.LOAI = LOAI.MALOAI" +
                    " AND NCC.MANCC = MH.MANCC";
            }
            ds = DocDanhSachSanPham(lenh);
            return ds;

        }
        //Tìm kiếm
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
                listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
            }
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            text_TimKiem.Focus();// đưa con trỏ đến ô tìm kiếm
        }

        //Giỏ hàng
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();// ẩn tab trang chủ
            GioHang GH = new GioHang();
            GH.ShowDialog();
            this.Show();
        }

        //Nhân viên
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();// ẩn tab trang chủ
            NhanVienDangNhap NVDN = new NhanVienDangNhap();
            NVDN.ShowDialog();
            this.Show();
        }

        
        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView2.SelectedItems)
            {
                textBox8.Text = items.SubItems[0].Text;
                textBox1.Text = items.SubItems[1].Text;
                textBox2.Text = items.SubItems[3].Text;
                textBox3.Text = items.SubItems[4].Text;
                textBox4.Text = items.SubItems[2].Text.Remove(items.SubItems[2].Text.Length - 5, 5);
                textBox5.Text = "1";
                textBox7.Text = items.SubItems[5].Text;
                textBox6.Text = items.SubItems[2].Text.Remove(items.SubItems[2].Text.Length - 5, 5);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                int soluong = int.Parse(textBox5.Text);
                soluong = soluong + 1;
                long thanhtien = long.Parse(textBox6.Text);
                //kiểm tra số lượng
                if (soluong <= (int.Parse(textBox7.Text)))
                {
                    textBox5.Text = soluong.ToString();
                    thanhtien = thanhtien + int.Parse(textBox4.Text);
                    textBox6.Text = thanhtien.ToString();
                }
                else
                {
                    MessageBox.Show("Mặt hàng hiện tại chỉ còn " + textBox7.Text + " sản phẩm");
                    return;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                int soluong = int.Parse(textBox5.Text);
                soluong = soluong - 1;
                long thanhtien = long.Parse(textBox6.Text);
                if (soluong != 0)
                {
                    textBox5.Text = soluong.ToString();
                    thanhtien = thanhtien - int.Parse(textBox4.Text);
                    textBox6.Text = thanhtien.ToString();
                }
                else
                {
                    textBox5.Text = "";
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
                    textBox6.Text = textBox7.Text = "";
                }
            }
        }

        // thêm vào giỏ
        private void button8_Click(object sender, EventArgs e)
        {
            // kiểm tra đã chọn mặc hàng chưa nếu chưa thì không thêm
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "")
            {
                // không làm gì cả
            }
            else// ngược lại thì tiến hành kiểm tra thông tin đăng nhập
            {

                if (BienToanCuc.length > 50)
                {
                    MessageBox.Show("Giỏ hàng chỉ chứa tối đa 50 mặt hàng");
                }
                else
                {
                    // chưa đăng nhập thì lưu thông tin vào biến toàn cục
                    bool kt = false;// kiểm tra xem mặt hàng thêm vào có trong giỏ hàng chưa NẾU CÓ THÌ CẬP NHẬT
                    for (int i = 0; i < BienToanCuc.length; i++)
                    {
                        string stringvalue = textBox5.Text;// số lượng
                        int temp = int.Parse(stringvalue);
                        if (BienToanCuc.MAMH[i] == textBox8.Text)
                        // nếu sản phẩm có trong giỏ hàng thì tăng thêm số lượng và thoát khỏi vóng for
                        {
                            BienToanCuc.SoLuong[i] = BienToanCuc.SoLuong[i] + temp;
                            BienToanCuc.Thanhtien[i] = BienToanCuc.Tongtien(BienToanCuc.SoLuong[i], BienToanCuc.Gia[i]);
                            kt = true;
                            MessageBox.Show("Thêm thành công");
                            break;

                        }
                    }
                    if (kt)
                    {
                        // nếu trùng và cập nhật rồi thì không làm gì nữa
                    }
                    else //nếu chưa thì thêm vào
                    {
                        string stringvalue1 = textBox5.Text;// số lượng
                        int temp1 = int.Parse(stringvalue1);
                        string stringvalue = textBox4.Text;// giá
                        int temp = int.Parse(stringvalue);
                        BienToanCuc.MAMH[BienToanCuc.length] = textBox8.Text;
                        BienToanCuc.Gia[BienToanCuc.length] = temp;
                        BienToanCuc.SoLuong[BienToanCuc.length] = temp1;
                        BienToanCuc.TENMH[BienToanCuc.length] = textBox1.Text;
                        BienToanCuc.TenLoai[BienToanCuc.length] = textBox2.Text;
                        BienToanCuc.TENNCC[BienToanCuc.length] = textBox3.Text;
                        BienToanCuc.Thanhtien[BienToanCuc.length] = BienToanCuc.Tongtien(temp1, temp);
                        BienToanCuc.length++;// cập nhật lại length
                        MessageBox.Show("Thêm thành công");
                    }
                }

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
        private void button5_Click(object sender, EventArgs e)
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
        //Xem mặt hàng
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
        //Xem mặt hàng theo loại
        private DataSet XemMatHangTheoLoai(string lenh)
        {
            DataSet ds = new DataSet();
            ds = XemMatHang(lenh);
            return ds;
        }
        // tìm kiếm mặt hàng theo loại
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
        //Phân trang cho nhân viên
        private void LoadNhanVien()
        {
            if (BienToanCuc.Role == "Bán hàng")
            {
                XacNhanDonHang xndh = new XacNhanDonHang();
                xndh.ShowDialog();
            }
            else if (BienToanCuc.Role == "Quản lí")
            {
                QuanLyComment QLCMT = new QuanLyComment();
                QLCMT.ShowDialog();
            }
            else if(BienToanCuc.Role == "Thủ quỹ")
            {
                ThuQuy TQ = new ThuQuy();
                TQ.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hiện tại chưa có chức năng cho "+BienToanCuc.Role);
                BienToanCuc.flag = false;
            }
        }
        //load form đăng nhập
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!BienToanCuc.flag)
            {
                this.Hide();
                NhanVienDangNhap nvdn = new NhanVienDangNhap();
                nvdn.ShowDialog();
                //load giao diện nhân viên
                if (BienToanCuc.flag)
                {
                    LoadNhanVien();
                }
                this.Show();
            }
            else //nếu đăng nhập r thì đưa đến trang nhân viên
            {
                this.Hide();
                LoadNhanVien();
                this.Show();
            }
        }

        //đánh giá sản phẩm
        private void button9_Click(object sender, EventArgs e)
        {
            if(BienToanCuc.flag1)
            {
                this.Hide();
                KhacHangDanhGiaSanPham KHDGSP = new KhacHangDanhGiaSanPham();
                KHDGSP.ShowDialog();
                this.Show();
            }
            else
            {
                ThongTinKHDanhGia TTKHDG = new ThongTinKHDanhGia();
                TTKHDG.ShowDialog();
            }
            if (BienToanCuc.flag1)
            {
                this.Hide();
                KhacHangDanhGiaSanPham KHDGSP = new KhacHangDanhGiaSanPham();
                KHDGSP.ShowDialog();
                this.Show();
            }
        }
    }
}
