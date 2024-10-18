using System;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace thuchanh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDN.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTenDN.Text == "daomanhtung" && txtMatKhau.Text == "daomanhtung")
            {
                frmHang.userName = txtTenDN.Text;
                this.Hide(); 
                frmHang frm = new frmHang();
                frm.ShowDialog(); 
                this.Close(); 
            }
            else
            {
                string connectionString = "Data Source=DESKTOP-PS5S7N7;Initial Catalog=daomanhtung_banhang;User ID=sa;Password=123;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tblUser WHERE userName=@userName AND passWord=@passWord";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", txtTenDN.Text);
                    cmd.Parameters.AddWithValue("@passWord", txtMatKhau.Text);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        frmHang.userName = txtTenDN.Text; 
                        this.Hide(); 
                        frmHang frm = new frmHang(); 
                        frm.ShowDialog();  
                        this.Close();  
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập đúng tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


    }
}
