using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;// thư viện dùng kết nối sql

namespace _1712415_1712769_1712899
{
    public partial class TrangChu : Form
    {
        private String MAMH;

        private string MANCC;
        public TrangChu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Trang chủ
        }

        private void button3_Click(object sender, EventArgs e) // chính sách bảo hành
        {
            this.Hide();// ẩn tab trang chủ
            ChinhSachBaoHanh CSBH = new ChinhSachBaoHanh();
            CSBH.ShowDialog();
            this.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // chính sách đổi trả => done
            this.Hide();
            ChinhSachDoiTra CSDT = new ChinhSachDoiTra();
            CSDT.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            // quản lí đơn hàng
            QuanLiDonHang QLDH = new QuanLiDonHang();
            QLDH.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // ẩn tab trang chủ
            this.Hide();
            // cá nhân gồm khách hàng, nhân viên, nhà cung cấp
            CaNhanDaDN CNDN = new CaNhanDaDN();
            CNDN.ShowDialog();
            this.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GioHang GH = new GioHang();
            GH.ShowDialog();
            this.Show();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            text_TimKiem.Focus();
            listView1.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            string lenh;
            if (checkBox2.Checked == true)
            {
                /*
                 SELECT MH_NCC.MA_NCC,NCC.TEN_NCC,MH_NCC.MAMH,MH.TENMH,MH.THUONGHIEU,MH.GIATIEN 
                 FROM NHACUNGCAP NCC,MH_NCC,MATHANG MH 
                 WHERE NCC.TEN_NCC =N'SVSAD' AND NCC.MA_NCC = MH_NCC.MA_NCC AND MH_NCC.MAMH= MH.MAMH
                 */
                lenh = "SELECT MH_NCC.MAMH,MH_NCC.MA_NCC,MH.TENMH,NCC.TEN_NCC,MH.THUONGHIEU," +
                    "MH.GIATIEN FROM NHACUNGCAP NCC,MH_NCC,MATHANG MH WHERE NCC.TEN_NCC = N'" 
                    + text_TimKiem.Text + "' AND NCC.MA_NCC = MH_NCC.MA_NCC AND MH_NCC.MAMH = MH.MAMH";
            }
            else
            {
                lenh= "SELECT MH.MAMH,MH_NCC.MA_NCC,MH.TENMH,NCC.TEN_NCC,MH.THUONGHIEU," +
                    "MH.GIATIEN FROM NHACUNGCAP NCC,MH_NCC,MATHANG MH WHERE MH.TENMH = N'"
                    + text_TimKiem.Text + "' AND NCC.MA_NCC = MH_NCC.MA_NCC AND MH_NCC.MAMH = MH.MAMH";
            }

                try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
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
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // chỉ được chọn 1 trong 2
            if (checkBox1.Checked == true) checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) checkBox1.Checked = false;
        }

        private void button8_Click(object sender, EventArgs e)// Thêm sản phẩm vào giỏ hàng
        {
            //kiểm tra nếu đã đăng nhập tài khoảng khách hàng chưa
            //nếu đăng nhập rồi thì lưu dữ liệu xuống database để khi khách hàng đăng nhập lại thì tình trạng giỏ hàng
            //vẫn được lưu lại
            // nếu chưa đăng nhập thì lưu tạm trên biến toàn cục

            // kiểm tra đã chọn mặc hàng chưa nếu chưa thì không thêm
            if(textBox1.Text==""&&textBox2.Text==""&&textBox3.Text==""&&textBox4.Text=="")
            {
                // không làm gì cả
            }
            else// ngược lại thì tiến hành kiểm tra thông tin đăng nhập
            {

                if (BienToanCuc.TenTK == "" || BienToanCuc.Password == "")
                {
                    if (BienToanCuc.length > 20)
                    {
                        MessageBox.Show("Vui lòng đăng nhập để được tiếp tục thêm sản phẩm váo giỏ hàng");
                    }
                    else
                    {
                        // chưa đăng nhập thì lưu thông tin vào biến toàn cục
                        bool kt = false;// kiểm tra xem mặt hàng thêm vào có trong giỏ hàng chưa NẾU CÓ THÌ CẬP NHẬT
                        for (int i = 0; i < BienToanCuc.length; i++)
                        {
                            string stringvalue = textBox5.Text;// số lượng
                            int temp = int.Parse(stringvalue);
                            if (BienToanCuc.MAMH[i] == MAMH && BienToanCuc.MANCC[i] == MANCC)
                            // nếu sản phẩm có trong giỏ hàng thì tăng thêm số lượng và thoát khỏi vóng for
                            {
                                BienToanCuc.SoLuong[i] = BienToanCuc.SoLuong[i] + temp;
                                BienToanCuc.Thanhtien[i] = BienToanCuc.Tongtien(BienToanCuc.SoLuong[i], BienToanCuc.Gia[i]);
                                kt = true;
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
                            BienToanCuc.MAMH[BienToanCuc.length] = MAMH;
                            BienToanCuc.MANCC[BienToanCuc.length] = MANCC;
                            BienToanCuc.Gia[BienToanCuc.length] = temp;
                            BienToanCuc.SoLuong[BienToanCuc.length] = temp1;
                            BienToanCuc.TENMH[BienToanCuc.length] = textBox1.Text;
                            BienToanCuc.TENNCC[BienToanCuc.length] = textBox2.Text;
                            BienToanCuc.THUONGHIEU[BienToanCuc.length] = textBox3.Text;
                            BienToanCuc.Thanhtien[BienToanCuc.length] = BienToanCuc.Tongtien(temp1, temp);
                            BienToanCuc.length++;// cập nhật lại length
                        }
                    }
                }
                else// nếu đăng nhập tài khoản rồi
                {
                    // insert dữ liệu vào bảng TK_MH của database
                    // => mở kết nối
                    SqlConnection conn = KetNoi.GetDBConnection();
                    try
                    {
                        conn.Open();

                        string sql1 = "EXEC INSERT_GIOHANG N'" + BienToanCuc.TenTK + "'," + MAMH + ", " + MANCC + ", " + textBox5.Text;
                        SqlCommand cmd = new SqlCommand(sql1, conn);// vận chuyển câu lệnh 
                        int data1 = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                      //  if (data1 != 0) MessageBox.Show("Thêm vào giỏ hàng thành công");
                      // else MessageBox.Show("Thêm vào giỏ hàng thất bại");
                        conn.Close();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hệ thống đang gặp sự cố vui lòng chọn lại sau");
                    }
                }

            }
            

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                MAMH = items.SubItems[0].Text;
                MANCC = items.SubItems[1].Text;
                textBox1.Text = items.SubItems[2].Text;
                textBox2.Text = items.SubItems[3].Text;
                textBox3.Text = items.SubItems[4].Text;
                textBox4.Text = items.SubItems[5].Text;
                textBox5.Text = "1";
                textBox6.Text = textBox4.Text;// thành tiền
            }
        }

        private void button10_Click(object sender, EventArgs e)// click vào dấu cộng
        {
            if (textBox5.Text != "")
            {
                string stringvalue1 = textBox5.Text;// số lượng
                int temp1 = int.Parse(stringvalue1);// chuyển string sang số
                temp1++;
                textBox5.Text = temp1.ToString();

                string stringvalue2 = textBox4.Text;// giá
                int temp2 = int.Parse(stringvalue2);// chuyển giá tiền 1 sp sang số
                int ThanhTien = temp1 * temp2;//thành tiền bằng giá nhân số lương
                textBox6.Text = ThanhTien.ToString();// chuyển thành chuỗi
            }

        }

        private void button9_Click(object sender, EventArgs e)// click vào dấu trừ
        {
            if (textBox5.Text != "")
            {
                string stringvalue1 = textBox5.Text;// số lượng
                int temp1 = int.Parse(stringvalue1);// chuyển string sang số
                if (temp1 > 0)
                {
                    temp1--;
                }
                textBox5.Text = temp1.ToString();

                string stringvalue2 = textBox4.Text;// giá
                int temp2 = int.Parse(stringvalue2);// chuyển giá tiền 1 sp sang số
                int ThanhTien = temp1 * temp2;// thành tiền bắng giá nhân số lượng
                textBox6.Text = ThanhTien.ToString();// chuyển thành chuỗi
            }
        }

        //private void textBox5_TextChanged(object sender, EventArgs e)
        //{
        //    string stringvalue1 = textBox5.Text;// số lượng
        //    int temp1 = int.Parse(stringvalue1);// chuyển string sang số
        //    string stringvalue2 = textBox4.Text;
        //    int temp2 = int.Parse(stringvalue2);// chuyển giá tiền 1 sp sang số
        //    int ThanhTien = temp1 * temp2;
        //    textBox4.Text = ThanhTien.ToString();// chuyển thành chuỗi
        //}
    }
}
