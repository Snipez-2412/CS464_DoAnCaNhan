using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class frm_QLNhanVien : Form
    {
        General gen = new General();

        public frm_QLNhanVien()
        {
            InitializeComponent();
        }

        private void frm_QLNhanVien_Load(object sender, EventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Class\Hè - 2024\CS 464 SA\DoAn\DoAn\CS464.mdf;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
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

            loadNhanVien();
        }

        public void loadNhanVien()
        {
            string sql = "Select * from NhanVien";
            DataTable dt = gen.loadDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gv_NhanVien.DataSource = dt;
                Console.WriteLine("Data loaded successfully");
            }
            else
            {
                Console.WriteLine("No data found");
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;
            string hoTen = txt_HoTen.Text;
            string SDT = txt_SDT.Text;
            string diaChi = txt_DiaChi.Text;
            string email = txt_Email.Text;

            string themNV = $"Insert into NhanVien values ('{maNV}', N'{hoTen}', N'{SDT}', N'{diaChi}', N'{email}')";
            int exc = gen.ThemSuaXoa(themNV);
            if (exc >= 1) MessageBox.Show("Thêm thành công", "Thông báo");
            else MessageBox.Show("Thêm không thành công", "Thông báo");

            txt_MaNV.Clear();
            txt_HoTen.Clear();
            txt_SDT.Clear();
            txt_DiaChi.Clear();
            txt_Email.Clear();
            loadNhanVien();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;
            string hoTen = txt_HoTen.Text;
            string SDT = txt_SDT.Text;
            string diaChi = txt_DiaChi.Text;
            string email = txt_Email.Text;

            string suaNV = $"UPDATE NhanVien SET HoTen = '{hoTen}', SDT = '{SDT}', DiaChi = '{diaChi}', Email = '{email}' WHERE MaNV = '{maNV}'";

            int exc = gen.ThemSuaXoa(suaNV);
            if (exc >= 1) MessageBox.Show("Sửa thành công", "Thông báo");
            else MessageBox.Show("Sửa không thành công", "Thông báo");

            txt_MaNV.Clear();
            txt_HoTen.Clear();
            txt_SDT.Clear();
            txt_DiaChi.Clear();
            txt_Email.Clear();
            loadNhanVien();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;

            string xoaNV = $"Delete from NhanVien where MaNV = {maNV}";
            int exc = gen.ThemSuaXoa(xoaNV);
            if (exc >= 1) MessageBox.Show("Xóa thành công", "Thông báo");
            else MessageBox.Show("Xóa không thành công", "Thông báo");

            txt_MaNV.Clear();
            loadNhanVien();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gv_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gv_NhanVien.Rows.Count)
            {
                DataGridViewRow row = gv_NhanVien.Rows[e.RowIndex];

                if (row != null)
                {
                    txt_MaNV.Text = row.Cells["MaNV"].Value?.ToString();
                    txt_HoTen.Text = row.Cells["HoTen"].Value?.ToString();
                    txt_SDT.Text = row.Cells["SDT"].Value?.ToString();
                    txt_DiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                    txt_Email.Text = row.Cells["Email"].Value?.ToString();
                }
            }
        }


    }
}
