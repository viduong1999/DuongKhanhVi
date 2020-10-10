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
    public partial class QuanLyComment : Form
    {
        public QuanLyComment()
        {
            InitializeComponent();
        }
        //Hàm đọc thông tin comment từ cơ sở dữ liệu
        private DataSet DocThongKeCommment()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh= "SELECT*FROM THONGKECMT CM, MATHANG MH WHERE LOAICMT IS NOT NULL AND MH.MAMH = CM.MAMH";
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
        private DataSet DSThongKeCMT()
        {
            DataSet ds = new DataSet();
            ds = DocThongKeCommment();
            return ds;

        }
        // lấy thông tin comment
        private void QuanLyComment_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = DSThongKeCMT();
            // xoá dữ liệu có sẳn trên list view và dataset
            listView1.Items.Clear();
            //đọc dữ liệu từ dataset và add vào listview
            for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
            {
                listView1.Items.Add(ds.Tables[0].Rows[rows].ItemArray[0].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[1].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[9].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[3].ToString());
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[5].ToString());
                if (ds.Tables[0].Rows[rows].ItemArray[6].ToString()=="True")
                {
                    listView1.Items[rows].SubItems.Add("Tốt");
                }
                else
                {
                    listView1.Items[rows].SubItems.Add("Xấu");
                }
                listView1.Items[rows].SubItems.Add(ds.Tables[0].Rows[rows].ItemArray[7].ToString());

            }
        }
        //Lấy danh sách comment xấu
        private DataSet CommentXau(string Email)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh = "select count(*) from THONGKECMT where EMAIL='"+Email+"' and LOAICMT=0";
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
        //số comment xấu của email
        private int DemCommentXau(string Email)
        {
            DataSet ds = new DataSet();
            ds = CommentXau(Email);
            int result = 0;
            result = int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            return result;
        }

        //Lấy danh sách comment xấu
        private DataSet CommentTot(string Email)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = KetNoi.GetDBConnection();
            string lenh = "select count(*) from THONGKECMT where EMAIL='" + Email + "' and LOAICMT=1";
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
        //số comment xấu của email
        private int DemCommentTot(string Email)
        {
            DataSet ds = new DataSet();
            ds = CommentTot(Email);
            int result = 0;
            result = int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            return result;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                textBox1.Text = items.SubItems[1].Text;
                textBox2.Text = items.SubItems[2].Text;
                textBox7.Text = items.SubItems[3].Text;
                textBox4.Text = items.SubItems[4].Text;
                textBox5.Text = DemCommentXau(items.SubItems[3].Text).ToString();
                textBox6.Text = DemCommentTot(items.SubItems[3].Text).ToString();
            }
        }
        private static int ThemVaoDSDEN(string Email)
        {
            // insert dữ liệu vào bảng TK_MH của database
            // => mở kết nối
            SqlConnection conntemp = KetNoi.GetDBConnection();
            try
            {
                conntemp.Open();
                string sqltemp = "exec ThemDanhSachDen'"+Email+"'";
                SqlCommand cmd = new SqlCommand(sqltemp, conntemp);// vận chuyển câu lệnh 
                int data = cmd.ExecuteNonQuery();
                //  if (data1 != 0) MessageBox.Show("Thêm vào giỏ hàng thành công");
                // else MessageBox.Show("Thêm vào giỏ hàng thất bại");
                conntemp.Close();
                return data;
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống gặp sự cố, vui lòng thử lại sau");
                return 0;
            }
        }
        //kiểm tra thêm vào danh sách đen
        private bool KiemTraThemVaoDSDEN(string Email)
        {
            int data;
            data = ThemVaoDSDEN(Email);
            if(data==1)//thêm thành công
            {
                return true;
            }
            else if(data==0)
            {
                MessageBox.Show("Email này đã có trong danh sách đen");
                return false;
            }
            else
            {
                return false;
            }
        }
        //Thêm vào danh sách đen
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                MessageBox.Show("Vui lòng chọn email bạn muốn thêm");
                return;
            }
            if(KiemTraThemVaoDSDEN(textBox7.Text))
            {
                MessageBox.Show("Them danh sách đen thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
