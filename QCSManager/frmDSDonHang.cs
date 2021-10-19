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
    public partial class frmDSDonHang : Form
    {
        /// <summary>
        /// Chuỗi kết nối với SQL
        /// </summary>
        string chuoiKetnoi = "Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True";

        public frmDSDonHang()
        {
            InitializeComponent();
        }

        private void frmDSDonHang_Load(object sender, EventArgs e)
        {
            load();
           
        }
        public void load()
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                conn.Open();
                string sql = "SELECT * from DonHang";
                SqlDataAdapter dt = new SqlDataAdapter(sql, conn);
                DataTable tb = new DataTable();
                dt.Fill(tb);
                dataGridViewdsDonHang.DataSource = tb;
                //
                // Đặt lại tên cho cột
                //
                dataGridViewdsDonHang.Columns["MaDH"].HeaderText = "Mã Đơn Hàng";
                dataGridViewdsDonHang.Columns["TenKH"].HeaderText = "Tên Khách Hàng";
                dataGridViewdsDonHang.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dataGridViewdsDonHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dataGridViewdsDonHang.Columns["LoaiHang"].HeaderText = "Loại Hàng";
                dataGridViewdsDonHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                //
                // Đặt lại kích thước cho các cột
                //
                this.dataGridViewdsDonHang.Columns["MaDH"].Width = 110;
                this.dataGridViewdsDonHang.Columns["TenKH"].Width = 130;
                this.dataGridViewdsDonHang.Columns["SDT"].Width = 110;
                this.dataGridViewdsDonHang.Columns["DiaChi"].Width = 330;
                this.dataGridViewdsDonHang.Columns["LoaiHang"].Width = 100;
                this.dataGridViewdsDonHang.Columns["ThanhTien"].Width = 110;

                

                conn.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnSXTang_Click(object sender, EventArgs e)
        {
            //
            //Sắp xếp tăng dần
            //
            dataGridViewdsDonHang.Sort(dataGridViewdsDonHang.Columns[5], ListSortDirection.Ascending);
        }

        private void btnSXGiam_Click(object sender, EventArgs e)
        {
            //
            //Sắp xếp giảm dần
            //
            dataGridViewdsDonHang.Sort(dataGridViewdsDonHang.Columns[5], ListSortDirection.Descending);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sqlTimKiem = "SELECT * FROM DonHang WHERE MaDH = @MaDH";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("MaDH", txtTimKiem.Text);
            //cmd.Parameters.AddWithValue("TenKH", txtTenKH1.Text);
            //cmd.Parameters.AddWithValue("SDT", txtSDT1.Text);
            //cmd.Parameters.AddWithValue("DiaChi", txtDiaChi1.Text);
            //cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang1.Text);
            //cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien1.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewdsDonHang.DataSource = dt;
        }

        private void dataGridViewdsDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Đưa dữ liệu từ DataGridView lên các ô textbox
            //
            txtTimKiem.Text = dataGridViewdsDonHang.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
