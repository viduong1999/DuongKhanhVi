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
    public partial class ThongKeCmt : Form
    {
        public ThongKeCmt()
        {
            InitializeComponent();
        }

        //Thống kê cmt
        private void button3_Click(object sender, EventArgs e)
        {
            //load lại trang
            ThongKeCmt_Load(sender,e);
        }

        //XemCmt
        private DataSet XemCMT()
        {
            DataSet ds = new DataSet();
            //SqlConnection conn = KetNoi.GetDBConnection();// kết nối database
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh;
            lenh = "SELECT*FROM THONGKECMT CM, MATHANG MH WHERE LOAICMT IS NULL AND MH.MAMH = CM.MAMH";
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
        //Xem thong ke cmt
        private DataSet XemThongKeCMT()
        {
            DataSet ds = new DataSet();
            ds = XemCMT();
            return ds;

        }
        //hiển thị danh sách cmt chưa phân loại
        private void ThongKeCmt_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = XemThongKeCMT();
            // xoá dữ liệu có sẳn trên list view và dataset
            listView1.Items.Clear();
            //đọc dữ liệu từ dataset và add vào listview
            for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
            {
                listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[9].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[2].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[4].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                textBox1.Text = items.SubItems[0].Text;
                textBox3.Text = items.SubItems[1].Text;
                textBox4.Text = items.SubItems[2].Text;
                textBox2.Text = items.SubItems[3].Text;
                textBox7.Text = items.SubItems[4].Text;
                textBox5.Text = items.SubItems[5].Text;
                textBox6.Text = items.SubItems[6].Text;
            }
        }
        //hàm cập nhật
        private int CapNhatCMT(string lenh)
        {
            int data1 = 0;
            SqlConnection conn = KetNoi.GetDBConnection();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(lenh, conn);// vận chuyển câu lệnh 
                data1 = cmd.ExecuteNonQuery();// KẾT QUẢ DATA LÀ SỐ DÒNG BỊ ẢNH HƯỞNG
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống đang gặp sự cố vui lòng chọn lại sau");
            }
            return data1;
        }
        //Hàm cập nhật thống kê cmt
        private void CapNhatThongKeCMT(string lenh)
        {
            int kt = CapNhatCMT(lenh);
            if (kt != 0 && kt != -1)
            {
                MessageBox.Show("Phân loại thành công");
                return;
            }
            MessageBox.Show("Phân loại thất bại");
        }
        //phân loại
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Vui lòng chọn comment bạn muốn đánh giá");
                return;
            }
            //kiểm tra đã phân loại chưa
            if(checkBox1.Checked==false&&checkBox2.Checked==false)
            {
                MessageBox.Show("Vui lòng chọn loại comment");
                return;
            }
            else
            {
                string loai = "";
                if(checkBox1.Checked==true)
                {
                    loai = "1";
                }
                else
                {
                    loai = "0";
                }
                BienToanCuc.MaNV = "1";
                string lenh = "update THONGKECMT set LOAICMT="+loai+", nvtk="+BienToanCuc.MaNV+
                    " WHERE ID="+textBox1.Text;
                // tiến hành phân loại
                CapNhatThongKeCMT(lenh);
                ThongKeCmt_Load(sender,e);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                checkBox2.Checked = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
