using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace DoAn
{
    public partial class frm_QLKhachHang : Form
    {
        General gen = new General();

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

            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Class\Hè - 2024\CS 464 SA\DoAn\DoAn\CS464.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connString))
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
            string sql = "SELECT * FROM KhachHang";
            DataTable dt = gen.loadDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gv_KhachHang.DataSource = dt;
            }
            else
            {
                gv_KhachHang.DataSource = null;
            }
        }

        private void SetupDataGridView()
        {
            gv_KhachHang.AutoGenerateColumns = false;
            gv_KhachHang.Columns.Clear();

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKH",
                HeaderText = "Mã Khách Hàng"
            });

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "Họ Và Tên"
            });

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DiaChi",
                HeaderText = "Địa Chỉ"
            });

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SDT",
                HeaderText = "Số Điện Thoại"
            });

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioiTinh",
                HeaderText = "Giới Tính"
            });

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày Sinh"
            });

            gv_KhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayDangKi",
                HeaderText = "Ngày Đăng Kí"
            });
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string getMaxKhachHangCode = "SELECT MAX(MaKH) FROM KhachHang";
            object result = gen.getValue(getMaxKhachHangCode);
            int newKhachHangCode = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;

            string hovaten = txt_HoTen.Text;
            string diachi = txt_DiaChi.Text;
            string sodienthoai = txt_SDT.Text;
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

            string themKhachHang = "INSERT INTO KhachHang (MaKH, HoTen, DiaChi, SDT, GioiTinh, NgaySinh, NgayDangKi) " +
                "VALUES (@MaKH, @HoTen, @DiaChi, @SDT, @GioiTinh, @NgaySinh, @NgayDangKi)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaKH", newKhachHangCode),
                new SqlParameter("@HoTen", hovaten),
                new SqlParameter("@DiaChi", diachi),
                new SqlParameter("@SDT", sodienthoai),
                new SqlParameter("@GioiTinh", gioitinh),
                new SqlParameter("@NgaySinh", ngaysinh),
                new SqlParameter("@NgayDangKi", ngaydangki)
            };

            int exc = gen.ThemSuaXoa(themKhachHang);
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
            txt_HoTen.Clear();
            txt_DiaChi.Clear();
            txt_SDT.Clear();
            cb_GioiTinh.SelectedIndex = -1;
            dt_NgaySinh.Value = DateTime.Today; // Reset DateTimePicker to today's date
            dt_NgayDangKi.Value = DateTime.Today; // Reset DateTimePicker to today's date
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txt_CanTim.Text, out int KhachHangCode))
                {
                    string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        con.Open();
                        string query = "SELECT * FROM KhachHang WHERE MaKH = @MaKH";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@MaKH", KhachHangCode);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_HoTen.Text = reader["HoTen"].ToString();
                            txt_DiaChi.Text = reader["DiaChi"].ToString();
                            txt_SDT.Text = reader["SDT"].ToString();
                            cb_GioiTinh.SelectedItem = reader["GioiTinh"].ToString();
                            dt_NgaySinh.Text = reader["NgaySinh"].ToString();
                            dt_NgayDangKi.Text = reader["NgayDangKi"].ToString();
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy khách hàng có mã {KhachHangCode}.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string xoaKhachHang = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@MaKH", maKhachHangXoa)
                };

                int exc = gen.ThemSuaXoa(xoaKhachHang);
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
