using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace temp
{
    public partial class frm_QLKhachHang : Form
    {
        GenKhachHang gen = new GenKhachHang();

        public frm_QLKhachHang()
        {
            InitializeComponent();
            SetupDataGridView();
            cb_GioiTinh.SelectedIndex = cb_GioiTinh.FindStringExact("Nam");
        }

        private void QLKhachHang_Load(object sender, EventArgs e)
        {
            dt_NgaySinh.Format = DateTimePickerFormat.Custom;
            dt_NgaySinh.CustomFormat = "dd/MM/yyyy";
            dt_NgaySinh.Value = DateTime.Today;

            dt_NgayDangKi.Format = DateTimePickerFormat.Custom;
            dt_NgayDangKi.CustomFormat = "dd/MM/yyyy";
            dt_NgayDangKi.Value = DateTime.Today;

            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection successful!");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            loadKhachHang();
        }

        public void loadKhachHang()
        {
            string sql = "SELECT * FROM Customer";
            DataTable dt = gen.loadDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKhachHang",
                HeaderText = "Mã Khách Hàng"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoVaTen",
                HeaderText = "Họ Và Tên"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DiaChi",
                HeaderText = "Địa Chỉ"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoDienThoai",
                HeaderText = "Số Điện Thoại"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioiTinh",
                HeaderText = "Giới Tính"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày Sinh"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayDangKi",
                HeaderText = "Ngày Đăng Kí"
            });
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string getMaxCustomerCode = "SELECT MAX(MaKhachHang) FROM Customer";
            object result = gen.getValue(getMaxCustomerCode);
            int newCustomerCode = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;

            string hovaten = txt_HoVaTen.Text;
            string diachi = txt_DiaChi.Text;
            string sodienthoai = txt_SoDienThoai.Text;
            string gioitinh = cb_GioiTinh.SelectedItem?.ToString();
            string ngaysinh = dt_NgaySinh.Text;
            string ngaydangki = dt_NgayDangKi.Text;

            if (string.IsNullOrEmpty(hovaten) || string.IsNullOrEmpty(diachi) ||
                string.IsNullOrEmpty(sodienthoai) || string.IsNullOrEmpty(gioitinh) ||
                string.IsNullOrEmpty(ngaysinh) || string.IsNullOrEmpty(ngaydangki))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin cho các trường bắt buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string themKhachHang = "INSERT INTO Customer (MaKhachHang, HoVaTen, DiaChi, SoDienThoai, GioiTinh, NgaySinh, NgayDangKi) " +
                "VALUES (@MaKhachHang, @HoVaTen, @DiaChi, @SoDienThoai, @GioiTinh, @NgaySinh, @NgayDangKi)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaKhachHang", newCustomerCode),
                new SqlParameter("@HoVaTen", hovaten),
                new SqlParameter("@DiaChi", diachi),
                new SqlParameter("@SoDienThoai", sodienthoai),
                new SqlParameter("@GioiTinh", gioitinh),
                new SqlParameter("@NgaySinh", ngaysinh),
                new SqlParameter("@NgayDangKi", ngaydangki)
            };

            int exc = gen.ThemSuaXoa(themKhachHang, parameters);
            if (exc >= 1)
                MessageBox.Show("Thêm thành công", "Thông báo");
            else
                MessageBox.Show("Thêm không thành công", "Thông báo");

            ClearInputFields();
            loadKhachHang();
            cb_GioiTinh.SelectedIndex = cb_GioiTinh.FindStringExact("Nam");
        }

        private void ClearInputFields()
        {
            txt_HoVaTen.Clear();
            txt_DiaChi.Clear();
            txt_SoDienThoai.Clear();
            cb_GioiTinh.SelectedIndex = -1;
            dt_NgaySinh.Value = DateTime.Today; // Reset DateTimePicker to today's date
            dt_NgayDangKi.Value = DateTime.Today; // Reset DateTimePicker to today's date
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txt_CanTim.Text, out int CustomerCode))
                {
                    string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();
                        string query = "SELECT * FROM Customer WHERE MaKhachHang = @MaKhachHang";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@MaKhachHang", CustomerCode);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_HoVaTen.Text = reader["HoVaTen"].ToString();
                            txt_DiaChi.Text = reader["DiaChi"].ToString();
                            txt_SoDienThoai.Text = reader["SoDienThoai"].ToString();
                            cb_GioiTinh.SelectedItem = reader["GioiTinh"].ToString();
                            dt_NgaySinh.Text = reader["NgaySinh"].ToString();
                            dt_NgayDangKi.Text = reader["NgayDangKi"].ToString();
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy khách hàng có mã {CustomerCode}.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        reader.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một số hợp lệ cho Mã Khách Hàng.", "Nhập không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string maKhachHangXoa = txt_CanXoa.Text.Trim();
            if (string.IsNullOrEmpty(maKhachHangXoa))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng có mã {maKhachHangXoa}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string xoaKhachHang = "DELETE FROM Customer WHERE MaKhachHang = @MaKhachHang";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@MaKhachHang", maKhachHangXoa)
                };

                int exc = gen.ThemSuaXoa(xoaKhachHang, parameters);
                if (exc >= 1)
                    MessageBox.Show($"Đã xóa khách hàng có mã {maKhachHangXoa}", "Thông báo");
                else
                    MessageBox.Show($"Không thể xóa khách hàng có mã {maKhachHangXoa}", "Lỗi");

                ClearInputFields();
                loadKhachHang();
            }
        }
    }
}
