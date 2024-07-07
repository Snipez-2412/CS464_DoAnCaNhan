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


namespace temp
{
    public partial class frm_QLSanPham : Form
    {
        GenSanPham gen = new GenSanPham();
        
        public frm_QLSanPham()
        {
            InitializeComponent();
            dataGridView1.MouseWheel += new MouseEventHandler(dataGridView1_MouseWheel);
            SetupDataGridView();
            cb_TinhTrang.SelectedIndex = cb_TinhTrang.FindStringExact("Còn Hàng");
        }

        private void frm_QLSanPham_Load(object sender, EventArgs e)
        {
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
            loadSanPham();
        }

        public void loadSanPham()
        {
            string sql = "SELECT * FROM Products";
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns.Contains("HinhAnh") && e.ColumnIndex == dataGridView1.Columns["HinhAnh"].Index && e.Value != null)
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
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductCode",
                HeaderText = "Mã Sản Phẩm"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenSanPham",
                HeaderText = "Tên Sản Phẩm"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HangSanXuat",
                HeaderText = "Hãng Sản Xuất"
            });

            dataGridView1.Columns.Add(new DataGridViewImageColumn
            {
                DataPropertyName = "HinhAnh",
                HeaderText = "Hình Ảnh",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaCa",
                HeaderText = "Giá Cả"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MoTaChung",
                HeaderText = "Mô Tả Chung"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TinhTrang",
                HeaderText = "Tình Trạng"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ChieuDai",
                HeaderText = "Chiều Dài"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ChieuRong",
                HeaderText = "Chiều Rộng"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrongLuong",
                HeaderText = "Trọng Lượng"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MauSac",
                HeaderText = "Màu Sắc"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AmThanh",
                HeaderText = "Bộ Nhớ"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HeDieuHanh",
                HeaderText = "Hệ Điều Hành"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TheNho",
                HeaderText = "Thẻ Nhớ"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "KetNoi",
                HeaderText = "Kết Nối"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Camera",
                HeaderText = "Camera"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Pin",
                HeaderText = "Pin"
            }); dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BaoHanh",
                HeaderText = "Bảo Hành"
            }); dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "Số Lượng"
            });

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }
        
        private void dataGridView1_MouseWheel(object sender, MouseEventArgs e)
        {
            int scrollAmount = e.Delta > 0 ? -3 : 3;
            int newIndex = dataGridView1.FirstDisplayedScrollingRowIndex + scrollAmount;
            if (newIndex >= 0 && newIndex < dataGridView1.RowCount)
                dataGridView1.FirstDisplayedScrollingRowIndex = newIndex;
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
            string tensanpham = txt_TenSanPham.Text;
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
            
            string getMaxProductCode = "SELECT MAX(ProductCode) FROM Products";
            object result = gen.getValue(getMaxProductCode);
            int newProductCode = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;
            
            byte[] imageBytes = null;
            if (image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }
            
            string themSanPham = "INSERT INTO Products (ProductCode, TenSanPham, HangSanXuat, GiaCa, TinhTrang, ChieuDai, ChieuRong, TrongLuong, MauSac, AmThanh, MoTaChung, BoNho, HeDieuHanh, TheNho, KetNoi, Camera, Pin, BaoHanh, SoLuong, HinhAnh) VALUES (@ProductCode, @TenSanPham, @HangSanXuat, @GiaCa, @TinhTrang, @ChieuDai, @ChieuRong, @TrongLuong, @MauSac, @AmThanh, @MoTaChung, @BoNho, @HeDieuHanh, @TheNho, @KetNoi, @Camera, @Pin, @BaoHanh, @SoLuong, @HinhAnh)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProductCode", newProductCode),
                new SqlParameter("@TenSanPham", tensanpham),
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

            int exc = gen.ThemSuaXoa(themSanPham, parameters);
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
            txt_TenSanPham.Clear();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Check if column "TenSanPham" exists
                    if (dataGridView1.Columns.Contains("TenSanPham"))
                    {
                        // Accessing "TenSanPham" cell
                        DataGridViewCell tenSanPhamCell = selectedRow.Cells[dataGridView1.Columns["TenSanPham"].DataPropertyName];

                        if (tenSanPhamCell != null && tenSanPhamCell.Value != null)
                        {
                            txt_TenSanPham.Text = tenSanPhamCell.FormattedValue?.ToString() ?? string.Empty;
                            txt_HangSanXuat.Text = selectedRow.Cells[dataGridView1.Columns["HangSanXuat"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_GiaCa.Text = selectedRow.Cells[dataGridView1.Columns["GiaCa"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_MoTaChung.Text = selectedRow.Cells[dataGridView1.Columns["MoTaChung"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            cb_TinhTrang.SelectedItem = selectedRow.Cells[dataGridView1.Columns["TinhTrang"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_ChieuDai.Text = selectedRow.Cells[dataGridView1.Columns["ChieuDai"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_ChieuRong.Text = selectedRow.Cells[dataGridView1.Columns["ChieuRong"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_TrongLuong.Text = selectedRow.Cells[dataGridView1.Columns["TrongLuong"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_MauSac.Text = selectedRow.Cells[dataGridView1.Columns["MauSac"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_AmThanh.Text = selectedRow.Cells[dataGridView1.Columns["AmThanh"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_HeDieuHanh.Text = selectedRow.Cells[dataGridView1.Columns["HeDieuHanh"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_TheNho.Text = selectedRow.Cells[dataGridView1.Columns["TheNho"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_KetNoi.Text = selectedRow.Cells[dataGridView1.Columns["KetNoi"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_Camera.Text = selectedRow.Cells[dataGridView1.Columns["Camera"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_Pin.Text = selectedRow.Cells[dataGridView1.Columns["Pin"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_BaoHanh.Text = selectedRow.Cells[dataGridView1.Columns["BaoHanh"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;
                            txt_SoLuong.Text = selectedRow.Cells[dataGridView1.Columns["SoLuong"].DataPropertyName].FormattedValue?.ToString() ?? string.Empty;

                            // Display the image if exists
                            byte[] imageBytes = selectedRow.Cells[dataGridView1.Columns["HinhAnh"].DataPropertyName].Value as byte[];
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
                        MessageBox.Show("Column 'TenSanPham' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                // Parse the text in txt_CanTim as an integer (assuming ProductCode is numeric)
                if (int.TryParse(txt_CanTim.Text, out int productCode))
                {
                    string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();
                        string query = "SELECT * FROM Products WHERE ProductCode = @ProductCode";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductCode", productCode);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_TenSanPham.Text = reader["TenSanPham"].ToString();
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
                // Parse the text in txt_CanXoa as an integer (assuming ProductCode is numeric)
                if (int.TryParse(txt_CanXoa.Text, out int productCode))
                {
                    string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();
                        string query = "DELETE FROM Products WHERE ProductCode = @ProductCode";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductCode", productCode);

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
