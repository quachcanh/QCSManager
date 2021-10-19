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
    public partial class frmBanHang : Form
    {
        /// <summary>
        /// Chuỗi kết nối với SQL
        /// </summary>
        string chuoiKetnoi = "Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True";

        public frmBanHang()
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
                string sql = "SELECT * from DonHang";
                SqlDataAdapter dt = new SqlDataAdapter(sql, conn);
                DataTable tb = new DataTable();
                dt.Fill(tb);
                dtaGriVDonHang.DataSource = tb;
                //
                // Đặt lại tên cho cột
                //
                dtaGriVDonHang.Columns["MaDH"].HeaderText = "Mã Đơn Hàng";
                dtaGriVDonHang.Columns["TenKH"].HeaderText = "Tên Khách Hàng";
                dtaGriVDonHang.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dtaGriVDonHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dtaGriVDonHang.Columns["LoaiHang"].HeaderText = "Loại Hàng";
                dtaGriVDonHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                //
                // Đặt lại kích thước cho các cột
                //
                this.dtaGriVDonHang.Columns["MaDH"].Width = 90;
                this.dtaGriVDonHang.Columns["TenKH"].Width = 110;
                this.dtaGriVDonHang.Columns["SDT"].Width = 100;
                this.dtaGriVDonHang.Columns["DiaChi"].Width = 200;
                this.dtaGriVDonHang.Columns["LoaiHang"].Width = 80;
                this.dtaGriVDonHang.Columns["ThanhTien"].Width = 90;

                conn.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            load();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            load();
            txtMaDH1.Clear();
            txtTenKH1.Clear();
            txtSDT1.Clear();
            txtLoaiHang1.Clear();
            txtDiaChi1.Clear();
            txtThanhTien1.Clear();
            txtMaDH1.Focus();
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
            string sql = "SELECT MaDH from DonHang where MaDH='" + mahang + "'";
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

        private void btnLuuvaThanhToan_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                if (txtMaDH2.Text != "" && txtTenKH2.Text != "")
                {
                    if (kiemTraMaHang(txtMaDH2.Text) == true)
                    {
                        MessageBox.Show("Mã hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        ///
                        /// Thêm dữ liệu vào CSDL
                        /// 
                        conn.Open();
                        string sql = "insert into DonHang values('" + txtMaDH2.Text + "',N'" + txtTenKH2.Text + "',N'" + txtSDT2.Text + "',N'" + txtDiaChi2.Text + "',N'" + txtLoaiHang2.Text + "',N'" + txtThanhTien2.Text + "')";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int kq = (int)cmd.ExecuteNonQuery();
                        if (kq > 0)
                        {
                            MessageBox.Show("Thêm đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            load();
                            //txtMaDH2.Clear();
                            //txtTenKH2.Clear();
                            //txtSDT2.Clear();
                            //txtLoaiHang2.Clear();
                            //txtEmail.Clear();
                            //txtDiaChi2.Clear();
                            //txtGioiTinh.Clear();
                            //txtThanhTien2.Clear();
                            txtMaDH2.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Thêm đơn hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void btnSuaDH_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                conn.Open();
                //string sql = "UPDATE DonHang SET TenKH=N'" + txtTenKH1.Text + "', SDT=N'" + txtSDT1.Text + "', DiaChi=N'" + txtDiaChi1.Text + "', LoaiHang=N'" + txtLoaiHang1.Text + "', ThanhTien=N'" + txtThanhTien1 + "' where MaDH='" + txtMaDH1.Text + "'";
                string sql = "UPDATE DonHang Set TenKH = @TenKH, SDT = @SDT, DiaChi = @DiaChi, LoaiHang = @LoaiHang, ThanhTien = @ThanhTien WHERE MaDH = @MaDH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("MaDH", txtMaDH1.Text);
                cmd.Parameters.AddWithValue("TenKH", txtTenKH1.Text);
                cmd.Parameters.AddWithValue("SDT", txtSDT1.Text);
                cmd.Parameters.AddWithValue("DiaChi", txtDiaChi1.Text);
                cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang1.Text);
                cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien1.Text);
                int kq = (int)cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    load();
                    txtMaDH1.Clear();
                    txtTenKH1.Clear();
                    txtSDT1.Clear();
                    txtLoaiHang1.Clear();
                    txtDiaChi1.Clear();
                    txtThanhTien1.Clear();
                    txtMaDH1.Focus();
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

        private void btnXoaDH_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn xóa hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (thongbao == DialogResult.OK)
            {
                SqlConnection conn = new SqlConnection(chuoiKetnoi);
                conn.Open();
                string sql = "delete from DonHang where MaDH='" + txtMaDH1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công!");
                load();
                txtMaDH1.Clear();
                txtTenKH1.Clear();
                txtSDT1.Clear();
                txtLoaiHang1.Clear();
                txtDiaChi1.Clear();
                txtThanhTien1.Clear();
                txtMaDH1.Focus();

                conn.Close();
            }
        }

        private void dtaGriVDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Đưa dữ liệu từ DataGridView lên các ô textbox
            //
            txtMaDH1.Text = dtaGriVDonHang.CurrentRow.Cells[0].Value.ToString();
            txtTenKH1.Text = dtaGriVDonHang.CurrentRow.Cells[1].Value.ToString();
            txtSDT1.Text = dtaGriVDonHang.CurrentRow.Cells[2].Value.ToString();
            txtDiaChi1.Text = dtaGriVDonHang.CurrentRow.Cells[3].Value.ToString();
            txtLoaiHang1.Text = dtaGriVDonHang.CurrentRow.Cells[4].Value.ToString();
            txtThanhTien1.Text = dtaGriVDonHang.CurrentRow.Cells[5].Value.ToString();
        }

        private void txtThanhTien2_TextChanged(object sender, EventArgs e)
        {
            txtThanhToan.Text = txtThanhTien2.Text;
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn in hóa đơn không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (thongbao == DialogResult.OK)
            {
                MessageBox.Show("CHI TIẾT HÓA ĐƠN" + "\n\nMã đơn hàng: " + txtMaDH2.Text + "\nHọ Tên: " + txtTenKH2.Text + "\nSố điện thoại: " + txtSDT2.Text + "\nEmail: " + txtEmail.Text + "\nĐịa chỉ: " + txtDiaChi2.Text + "\nLoại hàng: " + txtLoaiHang2.Text + "\nGiới tính: " + txtGioiTinh.Text + "\nNgày đặt hàng: " + dateTimePicker1.Value + "\n==========================" + "\nTổng tiền: " + txtThanhToan.Text, "IN HÓA ĐƠN");
                txtMaDH2.Clear();
                txtTenKH2.Clear();
                txtSDT2.Clear();
                txtLoaiHang2.Clear();
                txtEmail.Clear();
                txtDiaChi2.Clear();
                txtGioiTinh.Clear();
                txtThanhTien2.Clear();
                txtMaDH2.Focus();
            }
                
        }

        private void btnTimKiemDH_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sqlTimKiem = "SELECT * FROM DonHang WHERE MaDH = @MaDH";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("MaDH", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("TenKH", txtTenKH1.Text);
            cmd.Parameters.AddWithValue("SDT", txtSDT1.Text);
            cmd.Parameters.AddWithValue("DiaChi", txtDiaChi1.Text);
            cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang1.Text);
            cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien1.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dtaGriVDonHang.DataSource = dt;
        }
    }
}
