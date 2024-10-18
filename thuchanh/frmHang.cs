using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; 
using System.Windows.Forms;

namespace thuchanh
{
    public partial class frmHang : Form
    {
        public static string userName = "";  
        private string connectionString = "Data Source=DESKTOP-PS5S7N7;Initial Catalog=daomanhtung_banhang;User ID=sa;Password=123;";


        public frmHang()
        {
            InitializeComponent();
        }

        private void frmHang_Load(object sender, EventArgs e)
        {
           
            lblDangNhap.Text = "Xin chào: " + userName; 
            LoadData(); 
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Kết nối thành công.");

                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblHang", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    MessageBox.Show("Dữ liệu đã được tải.");

                    dgvHang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
        
            if (string.IsNullOrWhiteSpace(txtMaHang.Text) || string.IsNullOrWhiteSpace(txtTenHang.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã hàng và Tên hàng.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO tblHang (MaHang, TenHang, ChatLieu, SoLuong, DonGiaNhap, DonGiaBan, GhiChu, Anh) " +
                                   "VALUES (@MaHang, @TenHang, @ChatLieu, @SoLuong, @DonGiaNhap, @DonGiaBan, @GhiChu, @Anh)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHang", txtMaHang.Text);
                        cmd.Parameters.AddWithValue("@TenHang", txtTenHang.Text);
                        cmd.Parameters.AddWithValue("@ChatLieu", cboChatLieu.SelectedItem);
                        cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                        cmd.Parameters.AddWithValue("@DonGiaNhap", decimal.Parse(txtDonGiaNhap.Text));
                        cmd.Parameters.AddWithValue("@DonGiaBan", decimal.Parse(txtDonGiaBan.Text));
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmd.Parameters.AddWithValue("@Anh", pbAnh.ImageLocation); 

                        cmd.ExecuteNonQuery();
                    }

                    LoadData(); 
                    MessageBox.Show("Thêm sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message); 
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHang.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để sửa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tblHang SET TenHang = @TenHang, ChatLieu = @ChatLieu, SoLuong = @SoLuong, " +
                                   "DonGiaNhap = @DonGiaNhap, DonGiaBan = @DonGiaBan, GhiChu = @GhiChu, Anh = @Anh " +
                                   "WHERE MaHang = @MaHang";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHang", txtMaHang.Text);
                        cmd.Parameters.AddWithValue("@TenHang", txtTenHang.Text);
                        cmd.Parameters.AddWithValue("@ChatLieu", cboChatLieu.SelectedItem);
                        cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                        cmd.Parameters.AddWithValue("@DonGiaNhap", decimal.Parse(txtDonGiaNhap.Text));
                        cmd.Parameters.AddWithValue("@DonGiaBan", decimal.Parse(txtDonGiaBan.Text));
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmd.Parameters.AddWithValue("@Anh", pbAnh.ImageLocation);

                        cmd.ExecuteNonQuery(); 
                    }

                    LoadData(); 
                    MessageBox.Show("Sửa sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message); 
                }
            }
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHang.CurrentRow != null)
            {
                txtMaHang.Text = dgvHang.CurrentRow.Cells["MaHang"].Value.ToString();
                txtTenHang.Text = dgvHang.CurrentRow.Cells["TenHang"].Value.ToString();
                cboChatLieu.SelectedItem = dgvHang.CurrentRow.Cells["ChatLieu"].Value.ToString();
                txtSoLuong.Text = dgvHang.CurrentRow.Cells["SoLuong"].Value.ToString();
                txtDonGiaNhap.Text = dgvHang.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
                txtDonGiaBan.Text = dgvHang.CurrentRow.Cells["DonGiaBan"].Value.ToString();
                txtGhiChu.Text = dgvHang.CurrentRow.Cells["GhiChu"].Value.ToString();
                pbAnh.ImageLocation = dgvHang.CurrentRow.Cells["Anh"].Value.ToString(); 
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(txtMaHang.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM tblHang WHERE MaHang = @MaHang";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHang", txtMaHang.Text);
                        cmd.ExecuteNonQuery(); 
                    }

                    LoadData(); 
                    MessageBox.Show("Xóa sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message); 
                }
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbAnh.Image = new Bitmap(openFileDialog.FileName);
                pbAnh.ImageLocation = openFileDialog.FileName; 
         
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }


    }
}
