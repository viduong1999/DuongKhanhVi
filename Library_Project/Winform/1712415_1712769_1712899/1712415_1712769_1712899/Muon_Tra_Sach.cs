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
using System.Globalization;
using System.Runtime.CompilerServices;

namespace _1712415_1712769_1712899
{
    public partial class Muon_Tra_Sach : Form
    {
        public Muon_Tra_Sach()
        {
            InitializeComponent();
        }

        //tra cứu thông tin độc giả
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            BienToanCuc.SoLuongSachDangMuon = 0;
            BienToanCuc.MATHE = "";
            label3.Text = "Lịch sử giao dịch sách của độc giả";
            label13.Text = "Tổng số sách đang mượn:";
            textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            comboBox1.Text = textBox8.Text = textBox9.Text = "";
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            /*select LSGD.MASACH, SACH.TENSACH, LSGD.NGAYMUON, LSGD.HANTRA,LSGD.NGAYTRA,
             * LSGD.TINHTRANG, LSGD.ID from LSGIAODICH AS LSGD,SACH
             * where LSGD.MATHE='SV1' and LSGD.MASACH=SACH.MASACH*/
            //kiểm tra xem có độc giả đó hay không
            string lenh = "select* FROM THETHUVIEN WHERE MATHE='"
                + text_TraCuu.Text + "'";
            conn.Open();// mở kết nối
            try
            {

                SqlCommand cm = new SqlCommand(lenh, conn);
                SqlDataReader data = cm.ExecuteReader();
                if (data.HasRows)// nếu thẻ thư viện hợp lệ
                {
                    BienToanCuc.MATHE = text_TraCuu.Text;
                    while (data.Read())
                    {
                        label3.Text = label3.Text + " " + (string)data["HOTEN"];
                        BienToanCuc.HOTEN_DOCGIA= (string)data["HOTEN"];
                    }
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Không tìm thấy thẻ thư viện");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            // SqlConnection conn = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            string sql = "select LSGD.MASACH, SACH.TENSACH, LSGD.NGAYMUON, LSGD.HANTRA,LSGD.NGAYTRA, LSGD.TINHTRANG, LSGD.ID" +
                " from LSGIAODICH AS LSGD, SACH" +
                " where LSGD.MATHE='" + text_TraCuu.Text + "' and LSGD.MASACH=SACH.MASACH";
            DataSet ds = new DataSet();
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
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả");
                    return;
                }
                //đọc dữ liệu từ dataset và add vào listview
                for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                {

                    listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
                    //listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                    // đếm số sách chưa trả
                    if (ds.Tables[0].Rows[rows].ItemArray[4].ToString() == "")
                    {
                        BienToanCuc.SoLuongSachDangMuon++;
                        // làm dấu sách chưa trả bằng màu aqua
                        listView1.Items[rows].BackColor = Color.Aqua;
                        //làm dấu sách chưa trả và quá hạn bằng màu đỏ
                        DateTime myDate = DateTime.Parse(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                        // Đối tượng mô tả thời điểm hiện tại.
                        DateTime now = DateTime.Now;
                        int result = now.CompareTo(myDate);
                        if (result == 1)//ngày hiện tại lớn hơn hạn trà => sách quá hạn
                        {
                            listView1.Items[rows].BackColor = Color.Salmon;
                            BienToanCuc.CapNhatTinhTrang(ds.Tables[0].Rows[rows].ItemArray[6].ToString());
                            listView1.Items[rows].SubItems.Add("Quá hạn");
                        }
                        else
                        {
                            listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                        }
                    }
                    else
                    {
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                    }
                    listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[6].ToString());
                }
                // gán số lượng lên lable
                label13.Text = label13.Text + " " + BienToanCuc.SoLuongSachDangMuon.ToString() + " quyển";
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }
        }


        // về trang chủ
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // hiển thị chi tiết lịch sử khi nhấp chuột vào item của listview
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            { 
                BienToanCuc.ID= items.SubItems[6].Text;
                textBox2.Text = items.SubItems[0].Text;
                textBox3.Text = items.SubItems[1].Text;
                textBox4.Text = items.SubItems[2].Text;
                textBox5.Text = items.SubItems[3].Text;
                if (items.SubItems[4].Text != "")
                {
                    dateTimePicker1.Checked = true;
                    DateTime enteredDate = DateTime.Parse(items.SubItems[4].Text);
                    dateTimePicker1.Value = enteredDate;
                }
                else// để trống 
                {
                    dateTimePicker1.Checked = false;
                    BienToanCuc.KT_CapNhat = true;
                }
                if (!dateTimePicker1.Checked)
                {
                    // hide date value since it's not set
                    //dateTimePicker1.CustomFormat = " ";
                    dateTimePicker1.CustomFormat = null;
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    dateTimePicker1.CustomFormat = null;
                    dateTimePicker1.Format = DateTimePickerFormat.Custom; // set the date format you want.
                }
                //nếu tình trả khác chưa trả thì readonly
                if (items.SubItems[5].Text == "Chưa trả")
                {
                    comboBox1.SelectedIndex = 0;
                }
                else if(items.SubItems[5].Text == "Đã trả")
                {
                    comboBox1.SelectedIndex = 1;
                }
                else if(items.SubItems[5].Text == "Quá hạn")
                {
                    comboBox1.SelectedIndex = 2;
                }
                else if (items.SubItems[5].Text == "Mất sách")
                {
                    comboBox1.SelectedIndex = 3;
                }
                else
                {
                    comboBox1.SelectedIndex = 4;
                }
                //kiểm tra phạt
                if(comboBox1.Text == "Mất sách"|| comboBox1.Text == "Hư hỏng"|| comboBox1.Text == "Quá hạn")
                {
                    DateTime HanTra = DateTime.Parse(items.SubItems[3].Text);
                    DateTime now = DateTime.Now;
                    TimeSpan Time = now - HanTra;
                    int TongSoNgay = Time.Days;
                    int tien = BienToanCuc.TienPhat(TongSoNgay, comboBox1.Text, int.Parse(textBox2.Text));
                    textBox8.Text = tien.ToString();
                }
                else if(comboBox1.Text == "Đã trả")// đã trả
                {
                    DateTime HanTra = DateTime.Parse(items.SubItems[3].Text);
                    DateTime NgayTra = DateTime.Parse(items.SubItems[4].Text);
                    TimeSpan Time = NgayTra - HanTra;
                    int TongSoNgay = Time.Days;
                    if(TongSoNgay>0)
                    {
                        int tien = BienToanCuc.TienPhat(TongSoNgay, comboBox1.Text, int.Parse(textBox2.Text));
                        textBox8.Text = tien.ToString();
                    }
                    else
                    {
                        textBox8.Text = "0";
                    }
                }
                else
                {
                    textBox8.Text = "0";
                }
            }
        }

        private void Muon_Tra_Sach_Load(object sender, EventArgs e)
        {
            BienToanCuc.KT_CapNhat = false;
            listView1.Items.Clear();
            BienToanCuc.HOTEN_DOCGIA = "";
            BienToanCuc.SoLuongSachDangMuon = 0;
            label3.Text = "Lịch sử giao dịch sách của độc giả";
            label13.Text = "Tổng số sách đang mượn:";
            textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            textBox8.Text = textBox9.Text = "";
            comboBox1.SelectedIndex = 5;
            BienToanCuc.ID = "";
            if (BienToanCuc.MATHE == "")
            {

            }
            else
            {
                text_TraCuu.Text = BienToanCuc.MATHE;
                SqlConnection conn = KetNoi.GetDBConnection();
                string sql = "select LSGD.MASACH, SACH.TENSACH, LSGD.NGAYMUON, LSGD.HANTRA,LSGD.NGAYTRA, LSGD.TINHTRANG, LSGD.ID" +
                " from LSGIAODICH AS LSGD, SACH" +
                " where LSGD.MATHE='" + BienToanCuc.MATHE + "' and LSGD.MASACH=SACH.MASACH";
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                    // xoá dữ liệu có sẳn trên list view và dataset
                    listView1.Items.Clear();
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy kết quả");
                        return;
                    }
                    //đọc dữ liệu từ dataset và add vào listview
                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {

                        listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
                        // đếm số sách chưa trả
                        if (ds.Tables[0].Rows[rows].ItemArray[4].ToString() == "")
                        {
                            BienToanCuc.SoLuongSachDangMuon++;
                            // làm dấu sách chưa trả bằng màu aqua
                            listView1.Items[rows].BackColor = Color.Aqua;
                            //làm dấu sách chưa trả và quá hạn bằng màu đỏ
                            DateTime myDate = DateTime.Parse(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                            // Đối tượng mô tả thời điểm hiện tại.
                            DateTime now = DateTime.Now;
                            int result = now.CompareTo(myDate);
                            if (result == 1)//ngày hiện tại lớn hơn hạn trà => sách quá hạn
                            {
                                listView1.Items[rows].BackColor = Color.Salmon;
                                // cập nhật tình trạng là quá hạn
                                BienToanCuc.CapNhatTinhTrang(ds.Tables[0].Rows[rows].ItemArray[6].ToString());
                                listView1.Items[rows].SubItems.Add("Quá hạn");
                            }
                            else
                            {
                                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                            }
                        }
                        else
                        {
                            listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                        }
                        listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[6].ToString());
                    }
                    // gán số lượng lên lable
                    label13.Text = label13.Text + " " + BienToanCuc.SoLuongSachDangMuon.ToString() + " quyển";
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }
            }
        }

        // cập nhật lịch sử giao dịch
        private void button3_Click(object sender, EventArgs e)
        {
            if (BienToanCuc.KT_CapNhat == false)
            {
                MessageBox.Show("Lịch sử gia dịch này không được phép cập nhật");
            }
            else
            {
                int Phat = 0;
                string tienphat = "0";
                // kiểm tra xem có bị trễ hạn không
                // nếu không thì cho phạt,lý do =null và tiến hành cập nhật
                if (comboBox1.Text == "Chưa trả")
                {
                    Phat = 0;
                    MessageBox.Show("Vui lòng cập nhật lại tình trạng");
                    return;
                }
                else if (comboBox1.Text == "Quá hạn" || comboBox1.Text == "Hư hỏng" || comboBox1.Text == "Mất sách")
                {
                    Phat = 1;
                    tienphat = textBox8.Text;
                }
                //chuyển ngày trả thành chuổi
                //lấy thông tin ngày giờ
                DateTime date = dateTimePicker1.Value;
                string ngay = date.Day.ToString();
                string thang = date.Month.ToString();
                string nam = date.Year.ToString();
                string NgayTra = "'" + nam + "-" + thang + "-" + ngay + "'";

                DateTime HanTra = DateTime.Parse(textBox5.Text);
                TimeSpan Time = date - HanTra;
                int TongSoNgay = Time.Days;
                int tien = BienToanCuc.TienPhat(TongSoNgay, comboBox1.Text, int.Parse(textBox2.Text));
                //Tiến hành cập nhật dữ liệu
                SqlConnection conn = KetNoi.GetDBConnection();
                try
                {
                    conn.Open();
                    string sql = "EXEC UpDateLSGD " + BienToanCuc.ID + ", N'" + comboBox1.Text + "', " + NgayTra +
                        ", " + Phat + ", N'" + textBox3.Text + "', N'" + BienToanCuc.HOTEN_DOCGIA + "', " + tien.ToString() +
                        ", " + BienToanCuc.MaNV + ", N'" + textBox9.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                    int data = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                    if (data != -1)
                    {
                        MessageBox.Show("Cập nhật thành công");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại");
                    }
                    conn.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                }
            }
            Muon_Tra_Sach_Load(sender, e);
        }
        //Thêm lịch sử giao dịch
        private void button7_Click(object sender, EventArgs e)
        {
            if(BienToanCuc.MATHE!="")
            {
                // kiểm tra xem có quá số lượng không
                if (BienToanCuc.SoLuongSachDangMuon >= 3)
                {
                    MessageBox.Show("Mỗi độc giả chỉ mượn tối đa 3 quyển");
                }
                else// nếu không thì cho thêm sách
                {
                    ThemLSGD themlsgd = new ThemLSGD();
                    themlsgd.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Vui long quét the thư viện");
            }
            Muon_Tra_Sach_Load(sender, e);
        }

        //thoát
        private void button6_Click(object sender, EventArgs e)
        {
            BienToanCuc.KT_CapNhat = false;
            text_TraCuu.Text = "";
            listView1.Items.Clear();
            BienToanCuc.SoLuongSachDangMuon = 0;
            BienToanCuc.MATHE = "";
            BienToanCuc.HOTEN_DOCGIA = "";
            label3.Text = "Lịch sử giao dịch sách của độc giả";
            label13.Text = "Tổng số sách đang mượn:";
            textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            textBox8.Text = textBox9.Text = "";
            comboBox1.SelectedIndex = 5;
            Muon_Tra_Sach_Load(sender, e);
        }

        //Xóa dữ liệu
        private void button4_Click(object sender, EventArgs e)
        {
            // kiểm tra đã chọn chưa
            if (textBox2.Text == "")
            {
                MessageBox.Show("Vui lòng chọn lịch sử bạn muốn xoá");
            }
            else
            {
                foreach (ListViewItem items in listView1.SelectedItems)
                {
                    SqlConnection conn = KetNoi.GetDBConnection();
                    try
                    {
                        conn.Open();
                        string sql = "exec XoaLichSuGiaoDich " + items.SubItems[6].Text;
                        SqlCommand cmd = new SqlCommand(sql, conn);// vận chuyển câu lệnh 
                        int datatemp = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                                                             //  if (data1 != 0) MessageBox.Show("Thêm vào giỏ hàng thành công");
                                                             // else MessageBox.Show("Thêm vào giỏ hàng thất bại");
                        conn.Close();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hệ thống đang gặp sự cố, vui lòng quay lại sau");
                    }
                }
            }
            Muon_Tra_Sach_Load(sender, e);
        }
    }
}
