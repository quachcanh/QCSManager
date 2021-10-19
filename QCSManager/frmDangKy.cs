using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace QCSManager
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }


        
        private void frmDangKy_Load(object sender, EventArgs e)
        {
            //string conString = ConfigurationManager.ConnectionStrings["QCS"].ConnectionString.ToString();
            //con = new SqlConnection(conString);
            //con.Open();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //
            //Đóng form
            //
            frmDangKy frmdk = new frmDangKy();
            this.Close();
            frmdk.Close();

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            //
            //Đảm bảo đầy đủ thông tin và mật khẩu trùng nhau mới được đăng ký
            //
            if (txtHoTen.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập vào họ tên!");
                txtHoTen.Focus(); //Đưa con trỏ chuột về lại ô họ tên
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập vào Email!");
                txtEmail.Focus();
            }
            else if (txtPhone.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập vào số điện thoại!");
                txtPhone.Focus();
            }
            else if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập vào Tài Khoản!");
                txtTaiKhoan.Focus();
            }
            else if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập vào Mật Khẩu!");
                txtMatKhau.Focus();
            }
            else if (txtNhapLaiMk.Text == "")
            {
                MessageBox.Show("Bạn chưa xác nhận lại Mật Khẩu!");
                txtNhapLaiMk.Focus();
            }
            else if (txtMatKhau.Text != txtNhapLaiMk.Text)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không trùng nhau!");
                txtNhapLaiMk.Focus();
                txtNhapLaiMk.SelectAll();
            }

            //
            //Kiểm tra email và mật khẩu phải theo đúng định dạng (Gồm 7 ký tự cả chữ và số)
            //
            else
            {

                byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtMatKhau.Text);
                byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

                string hasPass = "";

                foreach (byte item in hasData)
                {
                    hasPass += item;
                }

                SqlConnection conn = new SqlConnection("Data Source=QUACHCANH;Initial Catalog=dbQCSManager;Integrated Security=True");
                conn.Open();
                string sqldk = "Insert Into NguoiDung Values (@TaiKhoan, @MatKhau, @HoTen, @Email, @Phone)";
                SqlCommand cmd = new SqlCommand(sqldk, conn);
                cmd.Parameters.AddWithValue("TaiKhoan", txtTaiKhoan.Text);
                cmd.Parameters.AddWithValue("MatKhau", hasPass);
                cmd.Parameters.AddWithValue("HoTen", txtHoTen.Text);
                cmd.Parameters.AddWithValue("Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("Phone", txtPhone.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đăng ký tài khoản thành công");
            }
        }
        

        private void frmDangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
