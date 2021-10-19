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
    public partial class frmNhapKho : Form
    {
        /// <summary>
        /// Chuỗi kết nối với SQL
        /// </summary>
        string chuoiKetnoi = "Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True";
        public frmNhapKho()
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
                dataGridViewMatHang.DataSource = tb;
                //
                // Đặt lại tên cho cột
                //
                dataGridViewMatHang.Columns["MaHang"].HeaderText = "Mã Mặt Hàng";
                dataGridViewMatHang.Columns["TenHang"].HeaderText = "Tên Mặt Hàng";
                dataGridViewMatHang.Columns["LoaiHang"].HeaderText = "Loại Hàng";
                dataGridViewMatHang.Columns["SoLuong"].HeaderText = "Số Lượng";
                dataGridViewMatHang.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dataGridViewMatHang.Columns["GiaBan"].HeaderText = "Giá Bán";

                //
                // Đặt lại kích thước cho các cột
                //
                this.dataGridViewMatHang.Columns["MaHang"].Width = 110;
                this.dataGridViewMatHang.Columns["TenHang"].Width = 150;
                this.dataGridViewMatHang.Columns["LoaiHang"].Width = 140;
                this.dataGridViewMatHang.Columns["SoLuong"].Width = 80;
                this.dataGridViewMatHang.Columns["GiaNhap"].Width = 80;
                this.dataGridViewMatHang.Columns["GiaBan"].Width = 80;

                conn.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void frmNhapKho_Load(object sender, EventArgs e)
        {
            load();
        }
        /// <summary>
        /// Phương thức Kiểm tra xem mã mặt hàng đã có trong CSDL chưa?
        /// </summary>
        /// <param name="mahang"></param>
        /// <returns></returns>
        public bool kiemTraMaHang(string mahang)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sql = "SELECT MaHang from MatHang where MaHang='" + mahang + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)  //Nếu đã tồn tại mã hàng này trong csdl rồi thì trả về true
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        private void btnThemMatHang_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                if (txtMaHang.Text != "" && txtTenHang.Text != "")
                {
                    if (kiemTraMaHang(txtMaHang.Text) == true)
                    {
                        MessageBox.Show("Mặt hàng này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        ///
                        /// Thêm dữ liệu vào CSDL
                        /// 
                        conn.Open();
                        string sql = "insert into MatHang values(N'" + txtMaHang.Text + "',N'" + txtTenHang.Text + "',N'" + txtLoaiHang.Text + "','" + txtSoLuong.Text + "','" + txtGiaNhap.Text + "','" + txtGiaBan.Text + "')";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int kq = (int)cmd.ExecuteNonQuery();
                        if (kq > 0)
                        {
                            MessageBox.Show("Thêm mặt hàng mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            load();
                            txtMaHang.Clear();
                            txtTenHang.Clear();
                            txtLoaiHang.Clear();
                            txtSoLuong.Clear();
                            txtGiaNhap.Clear();
                            txtGiaBan.Clear();
                            txtMaHang.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Thêm mặt hàng mới thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        conn.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Chưa nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void btnSuaMatHang_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                conn.Open();
                //string sql = "UPDATE DonHang SET TenKH=N'" + txtTenKH1.Text + "', SDT=N'" + txtSDT1.Text + "', DiaChi=N'" + txtDiaChi1.Text + "', LoaiHang=N'" + txtLoaiHang1.Text + "', ThanhTien=N'" + txtThanhTien1 + "' where MaDH='" + txtMaDH1.Text + "'";
                string sql = "UPDATE MatHang Set TenHang = @TenHang, LoaiHang = @LoaiHang, SoLuong = @SoLuong, GiaNhap = @GiaNhap, GiaBan = @GiaBan WHERE MaHang = @MaHang";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("MaHang", txtMaHang.Text);
                cmd.Parameters.AddWithValue("TenHang", txtTenHang.Text);
                cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang.Text);
                cmd.Parameters.AddWithValue("SoLuong", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("GiaNhap", txtGiaNhap.Text);
                cmd.Parameters.AddWithValue("GiaBan", txtGiaBan.Text);
                int kq = (int)cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    load();
                    txtMaHang.Clear();
                    txtTenHang.Clear();
                    txtLoaiHang.Clear();
                    txtSoLuong.Clear();
                    txtGiaNhap.Clear();
                    txtGiaBan.Clear();
                    txtMaHang.Focus();
                }
                else
                {
                    MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void btnXoaMatHang_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn xóa hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (thongbao == DialogResult.OK)
            {
                SqlConnection conn = new SqlConnection(chuoiKetnoi);
                conn.Open();
                string sql = "delete from MatHang where MaHang='" + txtMaHang.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công!");
                load();
                txtMaHang.Clear();
                txtTenHang.Clear();
                txtLoaiHang.Clear();
                txtSoLuong.Clear();
                txtGiaNhap.Clear();
                txtGiaBan.Clear();
                txtMaHang.Focus();

                conn.Close();
            }
        }

        private void dataGridViewMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Đưa dữ liệu từ DataGridView lên các ô textbox
            //
            txtMaHang.Text = dataGridViewMatHang.CurrentRow.Cells[0].Value.ToString();
            txtTenHang.Text = dataGridViewMatHang.CurrentRow.Cells[1].Value.ToString();
            txtLoaiHang.Text = dataGridViewMatHang.CurrentRow.Cells[2].Value.ToString();
            txtSoLuong.Text = dataGridViewMatHang.CurrentRow.Cells[3].Value.ToString();
            txtGiaNhap.Text = dataGridViewMatHang.CurrentRow.Cells[4].Value.ToString();
            txtGiaBan.Text = dataGridViewMatHang.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sqlTimKiem = "SELECT * FROM MatHang WHERE MaHang = @MaHang";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("MaHang", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("TenHang", txtTenHang.Text);
            cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang.Text);
            cmd.Parameters.AddWithValue("SoLuong", txtSoLuong.Text);
            cmd.Parameters.AddWithValue("GiaNhap", txtGiaNhap.Text);
            cmd.Parameters.AddWithValue("GiaBan", txtGiaBan.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewMatHang.DataSource = dt;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaHang.Clear();
            txtTenHang.Clear();
            txtLoaiHang.Clear();
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
            txtMaHang.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}

