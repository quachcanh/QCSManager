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
using System.Text.RegularExpressions;

namespace QCSManager
{
    public partial class frmDonHangDangShip : Form
    {
        /// <summary>
        /// Chuỗi kết nối với SQL
        /// </summary>
        string chuoiKetnoi = "Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True";
        public frmDonHangDangShip()
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
                string sql = "SELECT * from DonShip";
                SqlDataAdapter dt = new SqlDataAdapter(sql, conn);
                DataTable tb = new DataTable();
                dt.Fill(tb);
                dataGridViewDSDonShip.DataSource = tb;
                //
                // Đặt lại tên cho cột
                //
                dataGridViewDSDonShip.Columns["MaDH"].HeaderText = "Mã Đơn Hàng";
                dataGridViewDSDonShip.Columns["TenKH"].HeaderText = "Tên Khách Hàng";
                dataGridViewDSDonShip.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dataGridViewDSDonShip.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dataGridViewDSDonShip.Columns["LoaiHang"].HeaderText = "Loại Hàng";
                dataGridViewDSDonShip.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                //
                // Đặt lại kích thước cho các cột
                //
                this.dataGridViewDSDonShip.Columns["MaDH"].Width = 110;
                this.dataGridViewDSDonShip.Columns["TenKH"].Width = 130;
                this.dataGridViewDSDonShip.Columns["SDT"].Width = 100;
                this.dataGridViewDSDonShip.Columns["DiaChi"].Width = 210;
                this.dataGridViewDSDonShip.Columns["LoaiHang"].Width = 100;
                this.dataGridViewDSDonShip.Columns["ThanhTien"].Width = 100;

                conn.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void frmDonHangDangShip_Load(object sender, EventArgs e)
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
            string sql = "SELECT MaDH from DonShip where MaDH='" + mahang + "'";
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

        private void btnTaoDonShip_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                if (txtMaDS.Text != "" && txtHoTen.Text != "")
                {
                    if (kiemTraMaHang(txtMaDS.Text) == true)
                    {
                        MessageBox.Show("Mã hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        ///
                        /// Thêm dữ liệu vào CSDL
                        /// 
                        conn.Open();
                        string sql = "insert into DonShip values('" + txtMaDS.Text + "',N'" + txtHoTen.Text + "',N'" + txtSDT.Text + "',N'" + txtDiaChi.Text + "',N'" + txtLoaiHang.Text + "',N'" + txtThanhTien.Text + "')";
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
                            txtMaDS.Focus();
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

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn in hóa đơn không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (thongbao == DialogResult.OK)
            {
                MessageBox.Show("CHI TIẾT HÓA ĐƠN" + "\n\nMã đơn hàng: " + txtMaDS.Text + "\nHọ Tên: " + txtHoTen.Text + "\nSố điện thoại: " + txtSDT.Text + "\nĐịa chỉ: " + txtDiaChi.Text + "\nLoại hàng: " + txtLoaiHang.Text + "\nNgày đặt hàng: " + dateTimePickerNgayDatHang.Value + "\n==========================" + "\nTổng tiền: " + txtThanhTien.Text, "IN HÓA ĐƠN");
                txtMaDS.Clear();
                txtHoTen.Clear();
                txtSDT.Clear();
                txtLoaiHang.Clear();
                txtDiaChi.Clear();
                txtThanhTien.Clear();
                txtMaDS.Focus();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn xóa hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (thongbao == DialogResult.OK)
            {
                SqlConnection conn = new SqlConnection(chuoiKetnoi);
                conn.Open();
                string sql = "delete from DonShip where MaDH='" + txtMaDS.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công!");
                load();
                txtMaDS.Clear();
                txtHoTen.Clear();
                txtSDT.Clear();
                txtLoaiHang.Clear();
                txtDiaChi.Clear();
                txtThanhTien.Clear();
                txtMaDS.Focus();

                conn.Close();
            }
        }

        private void dataGridViewDSDonShip_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Đưa dữ liệu từ DataGridView lên các ô textbox
            //
            txtMaDS.Text = dataGridViewDSDonShip.CurrentRow.Cells[0].Value.ToString();
            txtHoTen.Text = dataGridViewDSDonShip.CurrentRow.Cells[1].Value.ToString();
            txtSDT.Text = dataGridViewDSDonShip.CurrentRow.Cells[2].Value.ToString();
            txtDiaChi.Text = dataGridViewDSDonShip.CurrentRow.Cells[3].Value.ToString();
            txtLoaiHang.Text = dataGridViewDSDonShip.CurrentRow.Cells[4].Value.ToString();
            txtThanhTien.Text = dataGridViewDSDonShip.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sqlTimKiem = "SELECT * FROM DonShip WHERE MaDH = @MaDH";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("MaDH", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("TenKH", txtHoTen.Text);
            cmd.Parameters.AddWithValue("SDT", txtSDT.Text);
            cmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text);
            cmd.Parameters.AddWithValue("LoaiHang", txtLoaiHang.Text);
            cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewDSDonShip.DataSource = dt;
        }

        private void btnLuuDS_Click(object sender, EventArgs e)
        {
            //SqlConnection conn = new SqlConnection(chuoiKetnoi);
            //try
            //{
            //    conn.Open();
            //    string sql = "SELECT * from DonShip";
            //    saveFileDialog1.InitialDirectory = "C:";
            //    saveFileDialog1.Title = "Save as Excel File";
            //    saveFileDialog1.FileName = "";
            //    saveFileDialog1.Filter = "Excel Worksheets (*.xlsx)|*.xlsx|xls file (*.xls)|*.xls|All files (*.*)|*.*";
            //    if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            //    {
            //        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //        ExcelApp.Application.Workbooks.Add(Type.Missing);

            //        ExcelApp.Columns.ColumnWidth = 20;

            //        for (int i = 1; i < dataGridViewDSDonShip.Columns.Count + 1; i++)
            //        {
            //            ExcelApp.Cells[1, i] = dataGridViewDSDonShip.Columns[i - 1].HeaderText;

            //        }

            //        for (int i = 0; i < dataGridViewDSDonShip.Rows.Count; i++)
            //        {
            //            for (int j = 0; j < dataGridViewDSDonShip.Columns.Count; j++)
            //            {
            //                ExcelApp.Cells[i + 2, j + 1] = dataGridViewDSDonShip.Rows[i].Cells[j].Value.ToString();
            //            }
            //        }
            //        ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
            //        ExcelApp.ActiveWorkbook.Saved = true;
            //        ExcelApp.Quit();
            //    }
            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi kết nối");
            //}
            ////saveFileDialog1.InitialDirectory = "C:";
            ////saveFileDialog1.Title = "Save as Excel File";
            ////saveFileDialog1.FileName = "";
            ////saveFileDialog1.Filter = "Excel Worksheets (*.xlsx)|*.xlsx|xls file (*.xls)|*.xls|All files (*.*)|*.*";
            ////if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            ////{
            ////    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ////    ExcelApp.Application.Workbooks.Add(Type.Missing);

            ////    ExcelApp.Columns.ColumnWidth = 20;

            ////    for (int i = 1; i < dataGridViewDSDonShip.Columns.Count + 1; i++)
            ////    {
            ////        ExcelApp.Cells[1, i] = dataGridViewDSDonShip.Columns[i - 1].HeaderText;

            ////    }

            ////    for (int i = 0; i < dataGridViewDSDonShip.Rows.Count; i++)
            ////    {
            ////        for (int j = 0; j < dataGridViewDSDonShip.Columns.Count; j++)
            ////        {
            ////            ExcelApp.Cells[i + 2, j + 1] = dataGridViewDSDonShip.Rows[i].Cells[j].Value.ToString();
            ////        }
            ////    }
            ////    ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
            ////    ExcelApp.ActiveWorkbook.Saved = true;
            ////    ExcelApp.Quit();
            ////}

            ////var dia = new System.Windows.Forms.SaveFileDialog();
            ////dia.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ////dia.Filter = "Excel Worksheets (*.xlsx)|*.xlsx|xls file (*.xls)|*.xls|All files (*.*)|*.*";
            ////if (dia.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            ////{
            ////    DataTable data = null;// Sử dụng DataSource của DataGridView tại đây
            ////    var excel = new SaveFileDialog.ExcelPackage();
            ////    var ws = excel.Workbook.Worksheets.Add("worksheet-name");
            ////    // you can also use LoadFromCollection with an `IEnumerable<SomeType>`
            ////    ws.Cells["A1"].LoadFromDataTable(data, true, OfficeOpenXml.Table.TableStyles.Light1);
            ////    ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();

            ////    using (var file = File.Create(dia.FileName))
            ////        excel.SaveAs(file);
            ////}
        }
    }
}
