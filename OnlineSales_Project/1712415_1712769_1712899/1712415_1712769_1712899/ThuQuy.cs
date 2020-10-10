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
    public partial class ThuQuy : Form
    {
        public ThuQuy()
        {
            InitializeComponent();
        }

        //tìm kiếm hoá đơn
        private void textBox1_Enter(object sender, EventArgs e)
        {

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


        //tìm kiếm mã hoá đơn
        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "select* from HOADON where MAHD=" +
                textBox1.Text+" and NVBANHANG is not null and XACNHANTHANHTOAN is null";
            DataSet ds = new DataSet();
            ds = ThongTinHoaDon(sql);
            // xoá dữ liệu có sẳn trên list view và dataset
            listView1.Items.Clear();
            if(ds.Tables[0].Rows.Count==0)
            {
                MessageBox.Show("Mã hoá đơn đã xác nhận hoặc không tồn tại, vui lòng nhập mã hoá đơn khác");
            }
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
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[7].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[8].ToString());
            }
        }

        //Đọc danh sách hoá đơn
        private DataSet DocDanhSachHoaDon()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh = "select* from HOADON where NVBANHANG is not null and XACNHANTHANHTOAN is null";
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
        private void ThuQuy_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = DSHoaDon();
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
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[7].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[8].ToString());
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                if(items.SubItems[7].Text!="")
                {
                    textBox2.Text = items.SubItems[0].Text;
                    textBox3.Text = items.SubItems[7].Text;
                }
                else
                {
                    textBox2.Text = textBox3.Text = "";

                }
                textBox4.Text= items.SubItems[0].Text;
                textBox5.Text = items.SubItems[1].Text;
                textBox6.Text = items.SubItems[2].Text;
                textBox7.Text = items.SubItems[3].Text;
                textBox12.Text = items.SubItems[4].Text;
                textBox8.Text = items.SubItems[6].Text;
                textBox9.Text = items.SubItems[5].Text;
                textBox10.Text = items.SubItems[7].Text;

            }
        }
        private static int XacThucThanhToanThe(string sql)
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
                //MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                return 0;
            }
        }
        //kiểm tra thêm vào danh sách đen
        private bool KiemTraXacThucThanhToanThe(string sql)
        {
            int data;
            data = XacThucThanhToanThe(sql);
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
        //Tạo hoá đơn thanh toán thẻ
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text!=""&&textBox3.Text!="")
            {
                string sql = "insert into HDTHE values("+textBox2.Text+",'"+textBox3.Text+"',1)";
                bool kt = KiemTraXacThucThanhToanThe(sql);
                if(kt)
                {
                    MessageBox.Show("Xác nhận thông tin thanh toán thẻ thành công");
                }
                else
                {
                    MessageBox.Show("Thông tin thanh toán thẻ này đã được xác thực trước đó");
                }
            }
        }
        //Hàm xác thực hoá đơn
        private static int XacThucThanhToan(string sql)
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
        //kiểm tra xác thực thanh toán
        private bool KiemTraXacThucThanhToan(string sql)
        {
            int data;
            data = XacThucThanhToan(sql);
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
        //Hàm đọc thông tin hoá đơn thẻ
        private DataSet DocHDThe(string sql)
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
        private bool KiemTraHDThe(string sql)
        {
            DataSet ds = new DataSet();
            ds = DocHDThe(sql);
            if(ds.Tables[0].Rows.Count==0)
            {
                return false;
            }
            return true;
        }
        //Xác nhận thanh toán
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                MessageBox.Show("Vui lòng click chọn 'xác thực thanh toán'");
                return;
            }
            if(textBox4.Text=="")
            {
                MessageBox.Show("Vui lòng chọn hoá đơn muốn xác thực");
                return;
            }
            if(textBox10.Text=="")//Nếu thanh toán trực tiếp thì tiến hành xác thực
            {
                string sql = "update HOADON set XACNHANTHANHTOAN=1 WHERE MAHD="+textBox4.Text;
                bool kt = KiemTraXacThucThanhToan(sql);
                if(kt)
                {
                    MessageBox.Show("Xác nhận thanh toán thành công");
                }
                else
                {
                    MessageBox.Show("Xác nhận thanh toán thất bại");
                }
            }
            else
            {
                //kiểm tra đã xác thực thông tin thanh toán thẻ chưa
                string sql = "select * from HDTHE where MAHD="+textBox4.Text+"" +
                    " and MATHE='"+textBox10.Text+"'";
                bool kt = KiemTraHDThe(sql);
                //Nếu chưa thì thông báo chưa và kết thúc
                //Nếu rồi thì tiến hành xác thực thanh toán cho hoá đơn
                if(!kt)
                {
                    MessageBox.Show("Vui lòng xác thực thông tin thanh toán thẻ để thực hiện chức năng này");
                    return;
                }
                else
                {
                    string sql1 = "update HOADON set XACNHANTHANHTOAN=1 WHERE MAHD=" + textBox4.Text;
                    bool kt1 = KiemTraXacThucThanhToan(sql1);
                    if (kt1)
                    {
                        MessageBox.Show("Xác nhận thanh toán thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xác nhận thanh toán thất bại");
                    }
                }
            }
            ThuQuy_Load(sender,e);
        }
    }
}
