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
    public partial class QuanLyDKThe : Form
    {
        public QuanLyDKThe()
        {
            InitializeComponent();
        }

        // tra cứu mã thẻ sinh viên/ cán bộ
        private void button1_Click(object sender, EventArgs e)
        {
            if (text_TraCuu.Text != "")
            {
                DataSet ds = new DataSet();
                string lenh;
                string str1 = text_TraCuu.Text.Substring(0, 2);
                // kiểm tra xem cán bộ hay sinh viên
                if (str1 == "CB")
                {
                    lenh = "EXEC KiemTraThongTinThe null,'" + text_TraCuu.Text + "'";
                }
                else
                {
                    lenh = "EXEC KiemTraThongTinThe '" + text_TraCuu.Text + "', null";
                }
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
                    da.Fill(ds);
                    conn.Close();
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin thẻ");
                        return;
                    }
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }

            }
        }

        //cá nhân
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) checkBox1.Checked = false;
        }

        //thêm danh sách đăng kí thẻ
        private void button7_Click(object sender, EventArgs e)
        {
            string LOAIDK = "";
            if (checkBox1.Checked == true)
            {
                LOAIDK = "N'Cá nhân'";
            }
            else if (checkBox2.Checked == true)
            {
                LOAIDK = "N'Lớp'";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại đăng ký thẻ");
                return;
            }
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            string sql = "EXEC ThemDanhSachDangKyThe N'" + textBox1.Text + "'," + LOAIDK + ", N'" +
                textBox2.Text + "'";
            // THÊM VÀO DỮ LIỆU
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                int data = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                if (data == -1 || data == 0)
                {
                    MessageBox.Show("Thêm danh sách đăng ký thẻ thất bại");
                }
                else
                {
                    MessageBox.Show("Thêm danh sách đăng ký thẻ thành công");
                }
                conn.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống đang gặp sự cố, vui lòng thử lại sau");
            }
        }

        // tra cứu danh sách đăng ký thẻ
        private void button4_Click(object sender, EventArgs e)
        {
            int soluong = 0;
            label8.Text = "Tổng số lượng học viên chưa có lớp học:";
            string sql = "select* from DSDKTHE where MALOP is null";
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();
                // xoá dữ liệu có sẳn trên list view và dataset
                listView2.Items.Clear();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Danh sách đăng ký thẻ rỗng");
                    return;
                }
                //đọc dữ liệu từ dataset và add vào listview
                for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                {
                    BienToanCuc.soluonghocvienmolop++;
                    soluong++;
                    listView2.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                    listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                    listView2.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
            label8.Text = label8.Text + " " + soluong.ToString() + " học viên";
        }

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            label10.Text = "Số lượng: ";
            ListViewItem item = e.Item as ListViewItem;
            if (item != null)
            {
                if (item.Checked)
                {
                    BienToanCuc.soluonghocvienmolop++;
                    // xử lý lấy chuỗi dữ liệu mã học viên để tạo danh sách lớp học
                    BienToanCuc.DanhSachLopHoc[BienToanCuc.soluonghocvienmolop - 1] = item.Text;
                }
                else
                {
                    //Xử lý xoá số mã học viên bị bỏ đi
                    int vt=-1;
                    //tìm vị trí
                    for(int i=0;i<BienToanCuc.soluonghocvienmolop;i++)
                    {
                        if(BienToanCuc.DanhSachLopHoc[i]==item.Text)
                        {
                            vt = i;
                        }
                    }
                    //nếu tồn tại một vị trí thì tiến hành xoá
                    if(vt!=-1)
                    {
                        for(int i= vt; i<BienToanCuc.soluonghocvienmolop-1;i++)
                        {
                            BienToanCuc.DanhSachLopHoc[i] = BienToanCuc.DanhSachLopHoc[i + 1];
                        }
                    }
                    BienToanCuc.soluonghocvienmolop--;
                }
            }
            label10.Text = label10.Text+BienToanCuc.soluonghocvienmolop.ToString() +" học viên";
        }

        // tạo danh sách lớp học
        private void button8_Click(object sender, EventArgs e)
        {
            // Kiểm tra số lượng
            if(BienToanCuc.soluonghocvienmolop>=30 && BienToanCuc.soluonghocvienmolop<=300)
            {
                try
                {
                    BienToanCuc.GETMALOPHOC();
                    for (int i = 0; i < BienToanCuc.soluonghocvienmolop; i++)
                    {
                        //tiến hành cập nhật tình trạng lớp học của các học viên được chọn
                        BienToanCuc.TaoLopHoc(BienToanCuc.DanhSachLopHoc[i]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống đang gặp sự số, vui lòng thử lại sau");
                }
                //tiến hành thêm lớp học

            }
            else
            {
                MessageBox.Show("Số lượng học viên mở lớp phải từ 30 trở lên mới được mở lớp");
            }
            button4_Click(sender, e);
        }

        private void QuanLyDKThe_Load(object sender, EventArgs e)
        {

        }
    }
}
