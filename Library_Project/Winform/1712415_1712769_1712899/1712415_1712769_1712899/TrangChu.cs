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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        //Load Trang chủ
        private void TrangChu_Load(object sender, EventArgs e)
        {
            // ẩn cột thứ 0 của listview
            listView1.Columns[3].Width = 0;
            listView2.Visible = false;
        }
        //Trang chu
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // chỉ được chọn 1 trong 2
        // tìm kiếm theo tên sách
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) checkBox2.Checked = false;
        }

        // tìm kiếm theo nhà cung cấp
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) checkBox1.Checked = false;
        }

        // Tìm kiếm
        private void button5_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            string lenh;
            if(text_TimKiem.Text=="")
            {
                lenh = "select TENSACH,TENTACGIA,LOAI,MASACH,TOMTAT from SACH";
            }
            else
            {
                if (checkBox2.Checked == true)// tìm kiếm theo tên sách
                {
                    lenh = "select TENSACH,TENTACGIA,LOAI,MASACH,TOMTAT from SACH where TENTACGIA=N'"
                        + text_TimKiem.Text + "'";
                }
                else
                {
                    /*select TENSACH,TENTACGIA,LOAI,MASACH,TOMTAT from SACH 
                     where TENSACH=N'Sống có kế hoạch'*/
                    lenh = "select TENSACH,TENTACGIA,LOAI,MASACH,TOMTAT from SACH where TENSACH=N'"
                        + text_TimKiem.Text + "'";
                }
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
                if(ds.Tables[0].Rows.Count==0)
                { MessageBox.Show("Không tìm thấy kết quả");
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
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }

        }

        //Gioi thieu
        private void button2_Click(object sender, EventArgs e)
        {

        }

        //Noi quy
        private void button4_Click(object sender, EventArgs e)
        {

        }

        //Nhan vien
        private void button3_Click(object sender, EventArgs e)
        {

            //Ẩn form trang chủ
            this.Hide();
            //Đăng nhập
            // Kiểm tra đã đăng nhập chưa nếu chưa thì hiển thị form đăng nhập
            //Nếu rồi thì kiểm tra chức vụ và hiển thị form QuanLySach hoac QLDKThe
            NhanVienDangNhap NVDN = new NhanVienDangNhap();
            NVDN.ShowDialog();// hiện form nhân viên đăng nhập
            this.Show();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                textBox1.Text = items.SubItems[0].Text;
                textBox2.Text = items.SubItems[1].Text;
                textBox3.Text = items.SubItems[2].Text;
                textBox4.Text = items.SubItems[4].Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // hiện listview2
            listView2.Visible = true;
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
            string lenh;
            lenh = "select distinct(LOAI) from SACH";
            try
            {
                conn.Open();// mở kết nối
                SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
                da.Fill(ds);
                conn.Close();
                // xoá dữ liệu có sẳn trên list view và dataset
                listView1.Items.Clear();
                listView2.Items.Clear();
                //đọc dữ liệu từ dataset và add vào listview
                for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                {
                    listView2.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
            }


        }

        // tìm kiếm sách theo loại
        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView2.SelectedItems)
            {
                string temp= items.SubItems[0].Text;
                DataSet ds = new DataSet();
                //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
                SqlConnection conn = KetNoi.GetDBConnection();
                // nếu không check vào mục nhà cung cấp thì tìm kiếm theo nhà cung
                string lenh= "select TENSACH,TENTACGIA,LOAI,MASACH,TOMTAT from SACH where LOAI=N'"
                    + temp + "'";
                try
                {
                    conn.Open();// mở kết nối
                    SqlDataAdapter da = new SqlDataAdapter(lenh, conn);
                    da.Fill(ds);
                    conn.Close();
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
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hệ thống gặp sự cố vui lòng chọn lại sau");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThanhLySach TLS = new ThanhLySach();
            TLS.ShowDialog();
            this.Show();
        }
    }
}
