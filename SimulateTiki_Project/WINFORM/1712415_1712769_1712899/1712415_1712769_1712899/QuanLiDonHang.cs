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
    public partial class QuanLiDonHang : Form
    {

        public QuanLiDonHang()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // quay về trang chủ 
            this.Close();
        }

        private void DH_TN_Click(object sender, EventArgs e)
        {
            //Đơn hàng đã tiếp nhận
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            listView1.Items.Clear();
            listView2.Items.Clear();
            if (BienToanCuc.TenTK == "")
            {

            }
            else// đăng nhập rồi thì tiến hành kiểm tra những đơn hàng đã huỷ
            {
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "select HOADON.MAHD,HOADON.TONGTIEN,HOADON.TINHTRANG " +
                    "from TKKHACHHANG,HOADON WHERE TKKHACHHANG.MATK=HOADON.MATK " +
                    "AND TKKHACHHANG.TENTK = N'" + BienToanCuc.TenTK + "'AND HOADON.TINHTRANG=N'Đã xác nhận'";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                    //
                    //
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }


            }
        }

            private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            // ẩn tab quản lí đơn hàng
            ChinhSachDoiTra CSDT = new ChinhSachDoiTra();
            CSDT.ShowDialog();// chuyển sang chính sách đổi trả và thoi tác trên csdt
            this.Close();// đóng tb sau khi thực hiện xong các thao tác trên tab chính sách đổi 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Trang cá nhân
            this.Hide();
            // ẩn tab quan li don hàng
            CaNhanDaDN CN = new CaNhanDaDN();
            CN.ShowDialog();// chuyển sang chính sách đổi trả và thoi tác trên csdt
            this.Close();// đóng tb sau khi thực hiện xong các thao tác trên tab chính sách đổi trả
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();// ẩn tab quản lí đơn hàng
            ChinhSachBaoHanh CSBH = new ChinhSachBaoHanh();
            CSBH.ShowDialog();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                checkBox1.Checked = false;
            }
        }

        private void DH_DH_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            // đơn hàng đã huỷ
            // nếu chưa đăng nhập thì ko làm gì cả
            if(BienToanCuc.TenTK=="")
            {

            }
            else// đăng nhập rồi thì tiến hành kiểm tra những đơn hàng đã huỷ
            {
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "select HOADON.MAHD,HOADON.TONGTIEN,HOADON.TINHTRANG " +
                    "from TKKHACHHANG,HOADON WHERE TKKHACHHANG.MATK=HOADON.MATK " +
                    "AND TKKHACHHANG.TENTK = N'" +BienToanCuc.TenTK+"'AND HOADON.HUYDON=1";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                    //
                    //
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)// đơn hàng chờ xác nhận
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            listView1.Items.Clear();
            listView2.Items.Clear();
            // đơn hàng đã huỷ
            // nếu chưa đăng nhập thì ko làm gì cả
            if (BienToanCuc.TenTK == "")
            {

            }
            else// đăng nhập rồi thì tiến hành kiểm tra những đơn hàng đã huỷ
            {
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "select HOADON.MAHD,HOADON.TONGTIEN,HOADON.TINHTRANG " +
                    "from TKKHACHHANG,HOADON WHERE TKKHACHHANG.MATK=HOADON.MATK " +
                    "AND TKKHACHHANG.TENTK = N'" + BienToanCuc.TenTK + "'AND HOADON.TINHTRANG=N'Chờ xác nhận'";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                    //
                    //
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }

            }
        }

        private void DH_DVC_Click(object sender, EventArgs e)// đơn hàng chờ vận chuyển
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            listView1.Items.Clear();
            listView2.Items.Clear();
            if (BienToanCuc.TenTK == "")
            {

            }
            else// đăng nhập rồi thì tiến hành kiểm tra những đơn hàng đã huỷ
            {
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "select HOADON.MAHD,HOADON.TONGTIEN,HOADON.TINHTRANG " +
                    "from TKKHACHHANG,HOADON WHERE TKKHACHHANG.MATK=HOADON.MATK " +
                    "AND TKKHACHHANG.TENTK = N'" + BienToanCuc.TenTK + "'AND HOADON.TINHTRANG=N'Đang giao'";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                    //
                    //
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }
            }
        }

        private void DH_TC_Click(object sender, EventArgs e)// đơn hàng thành công
        {
            // đơn hàng thành công thì được dánh giá đơn hàng
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            listView1.Items.Clear();
            listView2.Items.Clear();
            if (BienToanCuc.TenTK == "")
            {

            }
            else// đăng nhập rồi thì tiến hành kiểm tra những đơn hàng đã huỷ
            {
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "select HOADON.MAHD,HOADON.TONGTIEN,HOADON.TINHTRANG " +
                    "from TKKHACHHANG,HOADON WHERE TKKHACHHANG.MATK=HOADON.MATK " +
                    "AND TKKHACHHANG.TENTK = N'" + BienToanCuc.TenTK + "'AND HOADON.TINHTRANG=N'Đã giao'";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                    //
                    //
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }
            }
        }

        private void QuanLiDonHang_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            listView1.Items.Clear();
            listView2.Items.Clear();
            textBox1.Text = textBox2.Text = "";
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //hiển thị chi tiết đơn Hàng
            //hiển thị lên textbox
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                textBox2.Text= items.SubItems[0].Text;
                textBox1.Text = items.SubItems[1].Text;
            }
           //đỗ dữ liệu xuống list view 2
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            string sql = "	select MH.MAMH,NCC.MA_NCC, MH.TENMH,NCC.TEN_NCC,MH.THUONGHIEU,MH.GIATIEN,MH_HD.SL FROM MATHANG MH,MH_HD, NHACUNGCAP NCC WHERE MH_HD.MAHD = "+textBox2.Text+" AND MH_HD.MAMH = MH.MAMH AND MH_HD.MAMH = NCC.MA_NCC";                                                   
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();
                //
                //
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
                    listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[6].ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)// gửi đánh giá 
        {
            // update đánh giá hoá đơn
            if (textBox2.Text == "") { }
            else
            {
                if (checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    MessageBox.Show("vui lòng chọn đánh giá mà bạn muốn");
                }
                else
                {
                    string danhgia;
                    if (checkBox1.Checked == false)
                    {
                        danhgia = "Tốt";
                    }
                    else
                    {
                        danhgia = "Không tốt";
                    }
                    //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                    SqlConnection conn = KetNoi.GetDBConnection();
                    string sql = "UPDATE HOADON SET DANHGIA=N'" + danhgia + "' WHERE MAHD=" + textBox2.Text;
                    try
                    {
                        conn.Open();// mở kết nối
                        SqlCommand cm = new SqlCommand(sql, conn);
                        int data = cm.ExecuteNonQuery();
                        MessageBox.Show("Đánh giá đã được gửi");

                    }
                    catch
                    {
                        MessageBox.Show("Hệ thống đang gặp sự cố vui lòng gửi lại sau");
                    }
                }
            }
            QuanLiDonHang_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {

            }
            else
            {
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "exec HUYHD " + textBox2.Text;
                try
                {
                    conn.Open();// mở kết nối
                    SqlCommand cm = new SqlCommand(sql, conn);
                    int data = cm.ExecuteNonQuery();
                    MessageBox.Show("Huỷ Thành công");

                }
                catch
                {
                    MessageBox.Show("Hệ thống đang gặp sự cố vui lòng gửi lại sau");
                }
            }
            QuanLiDonHang_Load(sender, e);
        }
    }
}
