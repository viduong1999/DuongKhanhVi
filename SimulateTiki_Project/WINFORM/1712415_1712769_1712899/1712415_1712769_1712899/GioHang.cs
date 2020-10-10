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
    public partial class GioHang : Form
    {
        private string MAMH;
        public string MANCC;
        public GioHang()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // VỀ TRANG CHỦ
            this.Close();
        }

        private void GioHang_Load(object sender, EventArgs e)
        {
            int TongThanhToan=0;
            
            // hiển thị thông tin giỏ hàng
            // kiểm tra đăng nhập
            if(BienToanCuc.flag==true)// đăng nhập rồi
            {
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
                string sql= "EXEC VIEW_GIOHANG N'"+BienToanCuc.TenTK+"'";

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
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[6].ToString());
                        string gia = ds.Tables[0].Rows[rows].ItemArray[5].ToString();
                        string soluong = ds.Tables[0].Rows[rows].ItemArray[6].ToString();
                        int temp = int.Parse(gia);
                        int temp1 = int.Parse(soluong);
                        int temp2 = temp1 * temp;
                        string thanhtien = temp2.ToString();
                        listView1.Items[rows].SubItems.Add(thanhtien);
                        TongThanhToan = TongThanhToan + temp2;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }


            }
            else// chưa đăng nhập
            {
                listView1.Items.Clear();
                for (int i =0;i<BienToanCuc.length;i++)
                {
                    listView1.Items.Add(BienToanCuc.MAMH[i]);
                    listView1.Items[i].SubItems.Add(BienToanCuc.MANCC[i]);
                    listView1.Items[i].SubItems.Add(BienToanCuc.TENMH[i]);
                    listView1.Items[i].SubItems.Add(BienToanCuc.TENNCC[i]);
                    listView1.Items[i].SubItems.Add(BienToanCuc.THUONGHIEU[i]);
                    listView1.Items[i].SubItems.Add(BienToanCuc.Gia[i].ToString());
                    listView1.Items[i].SubItems.Add(BienToanCuc.SoLuong[i].ToString());
                    BienToanCuc.Thanhtien[i] = BienToanCuc.Gia[i] * BienToanCuc.SoLuong[i];
                    listView1.Items[i].SubItems.Add(BienToanCuc.Thanhtien[i].ToString());
                    TongThanhToan = TongThanhToan + BienToanCuc.Thanhtien[i];
                }

            }
            textBox1.Text = TongThanhToan.ToString();

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //hiển thị lên textbox
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                MAMH = items.SubItems[0].Text;
                MANCC = items.SubItems[1].Text;
                text_MH.Text = items.SubItems[2].Text;
                text_NCC.Text = items.SubItems[3].Text;
                text_TH.Text = items.SubItems[4].Text;
                text_Gia.Text = items.SubItems[5].Text;
                textBox5.Text = items.SubItems[6].Text;// số lượng
                text_ThanhTien.Text = items.SubItems[7].Text; // thành tiền

            }
        }

        private void button3_Click(object sender, EventArgs e)// click vào mua hàng
        {
            // kiểm tra xem giỏ hàng tạm thời có gì hay không, nếu có thì thêm hoá đơn, nếu ko thì thông báo giỏ hàng rỗng
            // phần này làm trong load_cá nhân

            if (BienToanCuc.flag == false)
            {
                MessageBox.Show("Vui lòng đăng nhập để được mua hàng");
            }
            else
            {
                SqlConnection conn = KetNoi.GetDBConnection();

                try
                {
                    conn.Open();
                    string sql = "exec THANHLAPHOADON N'" +BienToanCuc.TenTK+"'";
                    SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                    int data1 = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                    MessageBox.Show("Đặt hàng thành công");
                    conn.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống đang gặp sự cố vui lòng chọn lại sau");
                }

            }
            GioHang_Load(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // tăng hàng
            // kiểm tra có đăng nhập chưa
            if (BienToanCuc.flag == true)// nếu đăng nhập rồi thì update lại giỏ hàng trong database
            {
                if (textBox5.Text != "")
                {
                    SqlConnection conn = KetNoi.GetDBConnection();
                    conn.Open();
                    string stringvalue1 = textBox5.Text;// số lượng
                    int temp1 = int.Parse(stringvalue1);// chuyển string sang số
                    temp1++;
                    if(temp1 > 0)//
                    {
                        textBox5.Text = temp1.ToString();
                        string stringvalue2 = text_Gia.Text;// giá
                        int temp2 = int.Parse(stringvalue2);// chuyển giá tiền 1 sp sang số
                        int ThanhTien = temp1 * temp2;// thành tiền bắng giá nhân số lượng
                        text_ThanhTien.Text = ThanhTien.ToString();// chuyển thành chuỗi

                        try
                        {
                            string sql1 = "exec UPDATE_GIOHANG N'" + BienToanCuc.TenTK + "', N'"
                                + MAMH + "', N'" + MANCC + "'," + textBox5.Text;
                            SqlCommand cmd = new SqlCommand(sql1, conn);// vận chuyển câu lệnh 
                            int data1 = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                            conn.Close();

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Hệ thống đang gặp sự cố vui lòng chọn lại sau");
                        }
                    }

                }
            }
            else// ngược lại chưa đăng nhập
            {
                // Tìm vị trí của mặt hàng
                int vt = -1;
                for (int i = 0; i < BienToanCuc.length; i++)
                {
                    if (MAMH == BienToanCuc.MAMH[i] && MANCC == BienToanCuc.MANCC[i])
                    {
                        vt = i;
                        break;
                    }
                }
                // sau khi tìm được vị trí ta tiến hành cập nhật
                int temp;
                temp = int.Parse(textBox5.Text);// số lượng mà khách muốn đổi
                temp++;
                if (temp > 0)// ngược lại thì cập nhật giỏ hàng
                {
                    // cập nhật số lượng ở vị trí vt

                    BienToanCuc.SoLuong[vt] = temp;
                    textBox5.Text = temp.ToString();
                    // cập nhật thành tiền
                    BienToanCuc.Thanhtien[vt] = BienToanCuc.Tongtien(BienToanCuc.SoLuong[vt], BienToanCuc.Gia[vt]);
                    text_ThanhTien.Text = BienToanCuc.Thanhtien[vt].ToString();
                }
            }
            GioHang_Load(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)// GIẢM
        {
            if (textBox5.Text != "")
            {

                // giảm hàng
                // kiểm tra có đăng nhập chưa
                if (BienToanCuc.flag == true)// nếu đăng nhập rồi thì update lại giỏ hàng trong database
                {
                    if (textBox5.Text != "")
                    {
                        SqlConnection conn = KetNoi.GetDBConnection();
                        conn.Open();
                        string stringvalue1 = textBox5.Text;// số lượng
                        int temp1 = int.Parse(stringvalue1);// chuyển string sang số
                        temp1--;
                        if (temp1 == 0)// NẾU KO CÒN THÌ XOÁ KHỎI GIỎ HÀNG
                        {
                            string sql = "exec XOA_GIOHANG N'" + BienToanCuc.TenTK + "', " + MAMH + "," + MANCC;
                            SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                            SqlDataReader dr10 = cmd.ExecuteReader();
                            textBox5.Text = "";
                            text_MH.Text = "";
                            text_NCC.Text = "";
                            text_TH.Text = "";
                            text_Gia.Text = "";
                            text_ThanhTien.Text = "";
                        }
                        else if (temp1 > 0)
                        {
                            textBox5.Text = temp1.ToString();
                            string stringvalue2 = text_Gia.Text;// giá
                            int temp2 = int.Parse(stringvalue2);// chuyển giá tiền 1 sp sang số
                            int ThanhTien = temp1 * temp2;// thành tiền bắng giá nhân số lượng
                            text_ThanhTien.Text = ThanhTien.ToString();// chuyển thành chuỗi

                            try
                            {
                                string sql1 = "exec UPDATE_GIOHANG N'" + BienToanCuc.TenTK + "', N'"
                                    + MAMH + "', N'" + MANCC + "'," + textBox5.Text; SqlCommand cmd = new SqlCommand(sql1, conn);// vận chuyển câu lệnh 
                                int data1 = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                                conn.Close();

                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Hệ thống đang gặp sự cố vui lòng chọn lại sau");
                            }
                        }

                    }
                }
                else// ngược lại chưa đăng nhập
                {
                    // Tìm vị trí của mặt hàng
                    int vt = -1;
                    for (int i = 0; i < BienToanCuc.length; i++)
                    {
                        if (MAMH == BienToanCuc.MAMH[i] && MANCC == BienToanCuc.MANCC[i])
                        {
                            vt = i;
                            break;
                        }
                    }
                    // sau khi tìm được vị trí ta tiến hành cập nhật
                    int temp;
                    temp = int.Parse(textBox5.Text);// số lượng mà khách muốn đổi
                    temp--;
                    if (temp == 0)// nếu giảm xuống 0 thì xoá khỏi giỏ hàng
                    {
                        for (int i = vt; i < BienToanCuc.length - 1; i++)
                        {
                            BienToanCuc.MAMH[i] = BienToanCuc.MAMH[i + 1];
                            BienToanCuc.MANCC[i] = BienToanCuc.MANCC[i + 1];
                            BienToanCuc.TENMH[i] = BienToanCuc.TENMH[i + 1];
                            BienToanCuc.TENNCC[i] = BienToanCuc.TENNCC[i + 1];
                            BienToanCuc.THUONGHIEU[i] = BienToanCuc.THUONGHIEU[i + 1];
                            BienToanCuc.Gia[i] = BienToanCuc.Gia[i + 1];
                            BienToanCuc.SoLuong[i] = BienToanCuc.SoLuong[i + 1];

                        }
                        BienToanCuc.length--;// cập nhật lại length
                        textBox5.Text = "";
                        text_MH.Text = "";
                        text_NCC.Text = "";
                        text_TH.Text = "";
                        text_Gia.Text = "";
                        text_ThanhTien.Text = "";
                    }
                    else if (temp > 0)// ngược lại thì cập nhật giỏ hàng
                    {
                        // cập nhật số lượng ở vị trí vt

                        BienToanCuc.SoLuong[vt] = temp;
                        textBox5.Text = temp.ToString();
                        // cập nhật thành tiền
                        BienToanCuc.Thanhtien[vt] = BienToanCuc.Tongtien(BienToanCuc.SoLuong[vt], BienToanCuc.Gia[vt]);
                        text_ThanhTien.Text = BienToanCuc.Thanhtien[vt].ToString();
                    }
                }
            }
            GioHang_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (text_MH.Text == "")
            {

            }
            else
            {
                // kiểm tra đăng nhập chưa
                if (BienToanCuc.flag == true)// nếu rồi thì xoá giỏ hàng dưới database
                {
                    SqlConnection conn = KetNoi.GetDBConnection();
                    conn.Open();
                    string sql = "exec XOA_GIOHANG N'" + BienToanCuc.TenTK + "', " + MAMH + "," + MANCC;
                    SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                    SqlDataReader dr = cmd.ExecuteReader();
                    textBox5.Text = "";
                    text_MH.Text = "";
                    text_NCC.Text = "";
                    text_TH.Text = "";
                    text_Gia.Text = "";
                    text_ThanhTien.Text = "";
                    conn.Close();
                }
                else // ngược lại thì xoá biến toàn cục
                {
                    // Tìm vị trí của mặt hàng
                    int vt = -1;
                    for (int i = 0; i < BienToanCuc.length; i++)
                    {
                        if (MAMH == BienToanCuc.MAMH[i] && MANCC == BienToanCuc.MANCC[i])
                        {
                            vt = i;
                            break;
                        }
                    }
                    // xoá
                    for (int i = vt; i < BienToanCuc.length - 1; i++)
                    {
                        BienToanCuc.MAMH[i] = BienToanCuc.MAMH[i + 1];
                        BienToanCuc.MANCC[i] = BienToanCuc.MANCC[i + 1];
                        BienToanCuc.TENMH[i] = BienToanCuc.TENMH[i + 1];
                        BienToanCuc.TENNCC[i] = BienToanCuc.TENNCC[i + 1];
                        BienToanCuc.THUONGHIEU[i] = BienToanCuc.THUONGHIEU[i + 1];
                        BienToanCuc.Gia[i] = BienToanCuc.Gia[i + 1];
                        BienToanCuc.SoLuong[i] = BienToanCuc.SoLuong[i + 1];

                    }
                    BienToanCuc.length--;// cập nhật lại length
                    textBox5.Text = "";
                    text_MH.Text = "";
                    text_NCC.Text = "";
                    text_TH.Text = "";
                    text_Gia.Text = "";
                    text_ThanhTien.Text = "";
                }
            }
            GioHang_Load(sender, e);
        }
      
    }
}
