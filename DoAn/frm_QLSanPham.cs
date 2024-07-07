using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;


namespace DoAn
{
    public partial class frm_QLSanPham : Form
    {
        General gen = new General();

        public frm_QLSanPham()
        {
            InitializeComponent();
            gv_SanPham.MouseWheel += new MouseEventHandler(gv_SanPham_MouseWheel);
            SetupDataGridView();
            cb_TinhTrang.SelectedIndex = cb_TinhTrang.FindStringExact("Còn Hàng");
        }

        private void frm_QLSanPham_Load(object sender, EventArgs e)
        {
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
            loadSanPham();
        }

        public void loadSanPham()
        {
            string sql = "SELECT * FROM SanPham";
            DataTable dt = gen.loadDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gv_SanPham.DataSource = dt;
            }
            else
            {
                gv_SanPham.DataSource = null;
            }
        }

        private void gv_SanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gv_SanPham.Columns.Contains("HinhAnh") && e.ColumnIndex == gv_SanPham.Columns["HinhAnh"].Index && e.Value != null)
            {
                byte[] imageBytes = e.Value as byte[];
                if (imageBytes != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        e.Value = Image.FromStream(ms);
                    }
                }
            }
        }

        private void SetupDataGridView()
        {
            gv_SanPham.AutoGenerateColumns = false;
            gv_SanPham.Columns.Clear();

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaSP",
                HeaderText = "Mã Sản Phẩm"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenSP",
                HeaderText = "Tên Sản Phẩm"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HangSanXuat",
                HeaderText = "Hãng Sản Xuất"
            });

            gv_SanPham.Columns.Add(new DataGridViewImageColumn
            {
                DataPropertyName = "HinhAnh",
                HeaderText = "Hình Ảnh",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaCa",
                HeaderText = "Giá Cả"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MoTaChung",
                HeaderText = "Mô Tả Chung"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TinhTrang",
                HeaderText = "Tình Trạng"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ChieuDai",
                HeaderText = "Chiều Dài"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ChieuRong",
                HeaderText = "Chiều Rộng"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrongLuong",
                HeaderText = "Trọng Lượng"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MauSac",
                HeaderText = "Màu Sắc"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AmThanh",
                HeaderText = "Bộ Nhớ"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HeDieuHanh",
                HeaderText = "Hệ Điều Hành"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TheNho",
                HeaderText = "Thẻ Nhớ"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "KetNoi",
                HeaderText = "Kết Nối"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Camera",
                HeaderText = "Camera"
            });

            gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Pin",
                HeaderText = "Pin"
            }); gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BaoHanh",
                HeaderText = "Bảo Hành"
            }); gv_SanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "Số Lượng"
            });

            gv_SanPham.CellFormatting += gv_SanPham_CellFormatting;
        }

        private void gv_SanPham_MouseWheel(object sender, MouseEventArgs e)
        {
            int scrollAmount = e.Delta > 0 ? -3 : 3;
            int newIndex = gv_SanPham.FirstDisplayedScrollingRowIndex + scrollAmount;
            if (newIndex >= 0 && newIndex < gv_SanPham.RowCount)
                gv_SanPham.FirstDisplayedScrollingRowIndex = newIndex;
        }

        private void btn_ChenAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string tensanpham = txt_TenSP.Text;
            string hangsanxuat = txt_HangSanXuat.Text;
            string giaca = txt_GiaCa.Text;
            string tinhtrang = cb_TinhTrang.SelectedItem?.ToString();
            string kichthuoc_dai = txt_ChieuDai.Text;
            string kichthuoc_rong = txt_ChieuRong.Text;
            string trongluong = txt_TrongLuong.Text;
            string mausac = txt_MauSac.Text;
            string amthanh = txt_AmThanh.Text;
            string motachung = txt_MoTaChung.Text;
            string bonho = txt_BoNho.Text;
            string hedieuhanh = txt_HeDieuHanh.Text;
            string thenho = txt_TheNho.Text;
            string ketnoi = txt_KetNoi.Text;
            string camera = txt_Camera.Text;
            string pin = txt_Pin.Text;
            string baohanh = txt_BaoHanh.Text;
            string soluong = txt_SoLuong.Text;
            Image image = pictureBox1.Image;

            if (string.IsNullOrEmpty(tensanpham) || string.IsNullOrEmpty(hangsanxuat) ||
                string.IsNullOrEmpty(giaca) || string.IsNullOrEmpty(tinhtrang) ||
                string.IsNullOrEmpty(trongluong) || string.IsNullOrEmpty(kichthuoc_dai) ||
                string.IsNullOrEmpty(kichthuoc_rong) || string.IsNullOrEmpty(motachung) ||
                string.IsNullOrEmpty(soluong))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin cho các trường bắt buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(giaca, out int giaca_int) ||
                !int.TryParse(kichthuoc_dai, out int kichthuoc_dai_int) ||
                !int.TryParse(kichthuoc_rong, out int kichthuoc_rong_int) ||
                !int.TryParse(trongluong, out int trongluong_int) ||
                !int.TryParse(soluong, out int soluong_int))
            {
                MessageBox.Show("Giá Cả, Chiều Dài và Chiều Rộng phải là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string getMaxMaSP = "SELECT MAX(MaSP) FROM SanPham";
            object result = gen.getValue(getMaxMaSP);
            int newMaSP = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;

            byte[] imageBytes = null;
            if (image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }

            string themSanPham = "INSERT INTO SanPham (MaSP, TenSP, HangSanXuat, GiaCa, TinhTrang, ChieuDai, ChieuRong, TrongLuong, MauSac, AmThanh, MoTaChung, BoNho, HeDieuHanh, TheNho, KetNoi, Camera, Pin, BaoHanh, SoLuong, HinhAnh) VALUES (@MaSP, @TenSP, @HangSanXuat, @GiaCa, @TinhTrang, @ChieuDai, @ChieuRong, @TrongLuong, @MauSac, @AmThanh, @MoTaChung, @BoNho, @HeDieuHanh, @TheNho, @KetNoi, @Camera, @Pin, @BaoHanh, @SoLuong, @HinhAnh)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaSP", newMaSP),
                new SqlParameter("@TenSP", tensanpham),
                new SqlParameter("@HangSanXuat", hangsanxuat),
                new SqlParameter("@GiaCa", giaca),
                new SqlParameter("@TinhTrang", tinhtrang),
                new SqlParameter("@ChieuDai", kichthuoc_dai),
                new SqlParameter("@ChieuRong", kichthuoc_rong),
                new SqlParameter("@TrongLuong", trongluong),
                new SqlParameter("@MauSac", mausac),
                new SqlParameter("@AmThanh", amthanh),
                new SqlParameter("@MoTaChung", motachung),
                new SqlParameter("@BoNho", bonho),
                new SqlParameter("@HeDieuHanh", hedieuhanh),
                new SqlParameter("@TheNho", thenho),
                new SqlParameter("@KetNoi", ketnoi),
                new SqlParameter("@Camera", camera),
                new SqlParameter("@Pin", pin),
                new SqlParameter("@BaoHanh", baohanh),
                new SqlParameter("@SoLuong", soluong),
                new SqlParameter("@HinhAnh", imageBytes ?? (object)DBNull.Value)
            };

            int exc = gen.ThemSuaXoa(themSanPham);
            if (exc >= 1)
                MessageBox.Show("Thêm thành công", "Thông báo");
            else
                MessageBox.Show("Thêm không thành công", "Thông báo");

            ClearInputFields();
            loadSanPham();
            cb_TinhTrang.SelectedIndex = cb_TinhTrang.FindStringExact("Còn Hàng");
        }

        private void ClearInputFields()
        {
            txt_TenSP.Clear();
            txt_HangSanXuat.Clear();
            txt_GiaCa.Clear();
            cb_TinhTrang.SelectedIndex = -1;
            txt_ChieuDai.Clear();
            txt_ChieuRong.Clear();
            txt_TrongLuong.Clear();
            txt_MauSac.Clear();
            txt_AmThanh.Clear();
            txt_MoTaChung.Clear();
            txt_BoNho.Clear();
            txt_HeDieuHanh.Clear();
            txt_TheNho.Clear();
            txt_KetNoi.Clear();
            txt_Camera.Clear();
            txt_Pin.Clear();
            txt_BaoHanh.Clear();
            txt_SoLuong.Clear();
            pictureBox1.Image = null;
        }

        private void gv_SanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow selectedRow = gv_SanPham.Rows[e.RowIndex];

                    // Check if column "TenSP" exists
                    if (gv_SanPham.Columns.Contains("TenSP"))
                    {
                        // Accessing "TenSP" cell
                        DataGridViewCell tenSanPhamCell = selectedRow.Cells[gv_SanPham.Columns["TenSP"].DataPropertyName];

                        if (tenSanPhamCell != null && tenSanPhamCell.Value != null)
                        {
                            txt_TenSP.Text = tenSanPhamCell.FormattedValue?.ToString() ?? string.Empty;
                            txt_HangSanXuat.Text = selectedRow.Cells[gv_SanPham.Columns["HangSanXuat"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_GiaCa.Text = selectedRow.Cells[gv_SanPham.Columns["GiaCa"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_MoTaChung.Text = selectedRow.Cells[gv_SanPham.Columns["MoTaChung"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            cb_TinhTrang.SelectedItem = selectedRow.Cells[gv_SanPham.Columns["TinhTrang"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_ChieuDai.Text = selectedRow.Cells[gv_SanPham.Columns["ChieuDai"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_ChieuRong.Text = selectedRow.Cells[gv_SanPham.Columns["ChieuRong"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_TrongLuong.Text = selectedRow.Cells[gv_SanPham.Columns["TrongLuong"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_MauSac.Text = selectedRow.Cells[gv_SanPham.Columns["MauSac"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_AmThanh.Text = selectedRow.Cells[gv_SanPham.Columns["AmThanh"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_HeDieuHanh.Text = selectedRow.Cells[gv_SanPham.Columns["HeDieuHanh"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_TheNho.Text = selectedRow.Cells[gv_SanPham.Columns["TheNho"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_KetNoi.Text = selectedRow.Cells[gv_SanPham.Columns["KetNoi"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_Camera.Text = selectedRow.Cells[gv_SanPham.Columns["Camera"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_Pin.Text = selectedRow.Cells[gv_SanPham.Columns["Pin"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_BaoHanh.Text = selectedRow.Cells[gv_SanPham.Columns["BaoHanh"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_SoLuong.Text = selectedRow.Cells[gv_SanPham.Columns["SoLuong"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;

                            // Display the image if exists
                            byte[] imageBytes = selectedRow.Cells[gv_SanPham.Columns["HinhAnh"].DataPropertyName].Value as byte[];
                            if (imageBytes != null)
                            {
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Column 'TenSP' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Class\Hè - 2024\CS 464 SA\DoAn\DoAn\CS464.mdf;Integrated Security=True";

            try
            {
                // Parse the text in txt_CanTim as an integer (assuming MaSP is numeric)
                if (int.TryParse(txt_CanTim.Text, out int productCode))
                {

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        con.Open();
                        string query = "SELECT * FROM SanPham WHERE MaSP = @MaSP";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@MaSP", productCode);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_TenSP.Text = reader["TenSP"].ToString();
                            txt_HangSanXuat.Text = reader["HangSanXuat"].ToString();
                            txt_GiaCa.Text = reader["GiaCa"].ToString();
                            txt_MoTaChung.Text = reader["MoTaChung"].ToString();
                            cb_TinhTrang.SelectedItem = reader["TinhTrang"].ToString();
                            txt_ChieuDai.Text = reader["ChieuDai"].ToString();
                            txt_ChieuRong.Text = reader["ChieuRong"].ToString();
                            txt_TrongLuong.Text = reader["TrongLuong"].ToString();
                            txt_MauSac.Text = reader["MauSac"].ToString();
                            txt_AmThanh.Text = reader["AmThanh"].ToString();
                            txt_HeDieuHanh.Text = reader["HeDieuHanh"].ToString();
                            txt_TheNho.Text = reader["TheNho"].ToString();
                            txt_KetNoi.Text = reader["KetNoi"].ToString();
                            txt_Camera.Text = reader["Camera"].ToString();
                            txt_Pin.Text = reader["Pin"].ToString();
                            txt_BaoHanh.Text = reader["BaoHanh"].ToString();
                            txt_SoLuong.Text = reader["SoLuong"].ToString();

                            // Display the image if exists
                            if (!(reader["HinhAnh"] is DBNull))
                            {
                                byte[] imageBytes = (byte[])reader["HinhAnh"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy sản phẩm có mã {productCode}.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        reader.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một số hợp lệ cho Mã Sản Phẩm.", "Nhập không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the text in txt_CanXoa as an integer (assuming MaSP is numeric)
                if (int.TryParse(txt_CanXoa.Text, out int productCode))
                {
                    string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Class\Hè - 2024\CS 464 SA\DoAn\DoAn\CS464.mdf;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        con.Open();
                        string query = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@MaSP", productCode);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Đã xóa sản phẩm có mã {productCode}.", "Đã xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear all textboxes and picture box after deletion
                            ClearInputFields();
                            loadSanPham();
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy sản phẩm có mã {productCode} để xóa.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một số hợp lệ cho Mã Sản Phẩm để xóa.", "Nhập không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
