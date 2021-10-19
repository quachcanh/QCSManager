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
using System.Security.Cryptography;
//using System.Data;

namespace QCSManager
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn =new SqlConnection("Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True");
            string tk = txtTaiKhoan.Text;
            string mk = txtMatKhau.Text;
            if (tk == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản");
                txtTaiKhoan.Focus();
            }
            else if (mk == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                txtMatKhau.Focus();
            }
            else
            {
                try
                {
                    //Mã hóa mật khẩu
                    byte[] temp = ASCIIEncoding.ASCII.GetBytes(mk);
                    byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

                    string hasPass = "";

                    foreach(byte item in hasData)
                    {
                        hasPass += item;
                    }
                    conn.Open();
                    //string tk = txtTaiKhoan.Text;
                    //string mk = txtMatKhau.Text;
                    string sql = "Select *from NguoiDung where TaiKhoan = '" + tk + "' and MatKhau='" + hasPass + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dta = cmd.ExecuteReader();
                    if (dta.Read() == true)
                    {
                        //
                        //Mở form mới và đóng lại form cũ
                        //
                        this.Hide();
                        frmTrangChu frmTrChu = new frmTrangChu();
                        frmTrChu.ShowDialog();
                        this.Close();


                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu bị sai!", "Thông Báo", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtTaiKhoan.Focus();
                        txtTaiKhoan.SelectAll();

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi kết nối");
                }
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            //
            //Sau khi nhập mật khẩu xong nhấn Enter sẽ tự động đăng nhập
            //
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                //GỌi lại sự kiện của Button_Click
                btnDangNhap.PerformClick();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            //frmDangNhap frmdn = new frmDangNhap();
            frmDangKy frmdk = new frmDangKy();
            frmdk.ShowDialog();
            //frmdn.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //testINS tta = new testINS();
            //tta.ShowDialog();
        }

        private void btnDangNhap_MouseHover(object sender, EventArgs e)
        {
            this.btnDangNhap.BackColor = System.Drawing.Color.DarkOrange;
        }

        private void btnDangNhap_MouseLeave(object sender, EventArgs e)
        {
            this.btnDangNhap.BackColor = System.Drawing.Color.Gainsboro;
        }

        private void btnDangKy_MouseHover(object sender, EventArgs e)
        {
            this.btnDangKy.BackColor = System.Drawing.Color.DarkOrange;
        }

        private void btnDangKy_MouseLeave(object sender, EventArgs e)
        {
            this.btnDangKy.BackColor = System.Drawing.Color.Gainsboro;
        }

        private void btnThoat_MouseHover(object sender, EventArgs e)
        {
            this.btnThoat.BackColor = System.Drawing.Color.DarkOrange;
        }

        private void btnThoat_MouseLeave(object sender, EventArgs e)
        {
            this.btnThoat.BackColor = System.Drawing.Color.Gainsboro;
        }
    }
}
