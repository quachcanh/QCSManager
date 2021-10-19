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
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            //
            //CHuỗi kết nối SQL và mở kết nối
            //
            string connetionString;
            SqlConnection conn;
            connetionString = "Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            //
            //Hiển thì data có trong bẳng lên DataGriView
            //
            string slqSELECT = "Select * from BXHNhanVien";
            SqlCommand cmd = new SqlCommand(slqSELECT, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dtagrvBXHNhanVien.DataSource = dt;
            //Đặt tên cho các cột
            //Đặt tên cho các cột
            dtagrvBXHNhanVien.Columns["TenNV"].HeaderText = "Nhân Viên";
            dtagrvBXHNhanVien.Columns["Diem"].HeaderText = "Điểm";
            //
            // Đặt lại kích thước cho các cột
            //
            this.dtagrvBXHNhanVien.Columns["TenNV"].Width = 150;
            this.dtagrvBXHNhanVien.Columns["Diem"].Width = 70;
        }
        private void btnTangDan_Click(object sender, EventArgs e)
        {
            //
            //Sắp xếp tăng dần
            //
            dtagrvBXHNhanVien.Sort(dtagrvBXHNhanVien.Columns[1], ListSortDirection.Ascending);
        }

        private void btnGiamDan_Click(object sender, EventArgs e)
        {
            //
            //Sắp xếp giảm dần
            //
            dtagrvBXHNhanVien.Sort(dtagrvBXHNhanVien.Columns[1], ListSortDirection.Descending);
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            frmBanHang frmbh = new frmBanHang();
            frmbh.Show();
        }

        private void btnDSDonHang_Click(object sender, EventArgs e)
        {
            frmDSDonHang frmdsdh = new frmDSDonHang();
            frmdsdh.Show();
        }

        private void btnShipHang_Click(object sender, EventArgs e)
        {
            frmDonHangDangShip frmdhdsh = new frmDonHangDangShip();
            frmdhdsh.Show();
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            frmNhapKho frmNhapKho = new frmNhapKho();
            frmNhapKho.Show();
        }

        private void btnDSKhoHang_Click(object sender, EventArgs e)
        {
            frmDsKhoHang frmdsKhoHang = new frmDsKhoHang();
            frmdsKhoHang.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNoiQuy_Click(object sender, EventArgs e)
        {
            frmNoiQuyCuaHang frmnqch = new frmNoiQuyCuaHang();
            frmnqch.Show();
        }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
            frmThongBao frmtb1 = new frmThongBao();
            frmtb1.Show();
        }
    }
}
