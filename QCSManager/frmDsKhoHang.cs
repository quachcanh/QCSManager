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

namespace QCSManager
{
    public partial class frmDsKhoHang : Form
    {
        /// <summary>
        /// Chuỗi kết nối với SQL
        /// </summary>
        string chuoiKetnoi = "Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True";
        public frmDsKhoHang()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Phương thức cập nhật lại bảng dữ liệu DataGridView - Hiển thị dữ liệu lên lưới
        /// </summary>
        public void load()
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                conn.Open();
                string sql = "SELECT * from MatHang";
                SqlDataAdapter dt = new SqlDataAdapter(sql, conn);
                DataTable tb = new DataTable();
                dt.Fill(tb);
                dataGridView1.DataSource = tb;
                //
                // Đặt lại tên cho cột
                //
                dataGridView1.Columns["MaHang"].HeaderText = "Mã Mặt Hàng";
                dataGridView1.Columns["TenHang"].HeaderText = "Tên Mặt Hàng";
                dataGridView1.Columns["LoaiHang"].HeaderText = "Loại Hàng";
                dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
                dataGridView1.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dataGridView1.Columns["GiaBan"].HeaderText = "Giá Bán";

                //
                // Đặt lại kích thước cho các cột
                //
                this.dataGridView1.Columns["MaHang"].Width = 110;
                this.dataGridView1.Columns["TenHang"].Width = 230;
                this.dataGridView1.Columns["LoaiHang"].Width = 200;
                this.dataGridView1.Columns["SoLuong"].Width = 80;
                this.dataGridView1.Columns["GiaNhap"].Width = 80;
                this.dataGridView1.Columns["GiaBan"].Width = 80;

                conn.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void frmDsKhoHang_Load(object sender, EventArgs e)
        {
            load();
        }

        private void btnCapNhatDsMatHang_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sqlTimKiem = "SELECT * FROM MatHang WHERE MaHang = @MaHang";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("MaHang", txtTimKiem.Text);
            //cmd.Parameters.AddWithValue("TenKH", txtTenKH1.Text);
            //cmd.Parameters.AddWithValue("SDT", txtSDT1.Text);
            //cmd.Parameters.AddWithValue("DiaChi", txtDiaChi1.Text);
            //cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang1.Text);
            //cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien1.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Đưa dữ liệu từ DataGridView lên các ô textbox
            //
            txtTimKiem.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
