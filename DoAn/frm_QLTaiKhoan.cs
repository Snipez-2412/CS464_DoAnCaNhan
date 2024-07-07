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
    public partial class frm_QLTaiKhoan : Form
    {
        General gen = new General();

        public frm_QLTaiKhoan()
        {
            InitializeComponent();
        }

        private void frm_QLTaiKhoan_Load(object sender, EventArgs e)
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

            loadTaiKhoan();
        }

        public void loadTaiKhoan()
        {
            string sql = "Select * from TaiKhoan";
            DataTable dt = gen.loadDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gv_TaiKhoan.DataSource = dt;
                Console.WriteLine("Data loaded successfully");
            }
            else
            {
                Console.WriteLine("No data found");
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string username = txt_User.Text;
            string password = txt_Password.Text;
            string hoTen = txt_HoTen.Text;

            string themUser = $"Insert into TaiKhoan values ('{username}', '{password}', N'{hoTen}')";
            int exc = gen.ThemSuaXoa(themUser);
            if (exc >= 1)   MessageBox.Show("Thêm thành công", "Thông báo");
            else            MessageBox.Show("Thêm không thành công", "Thông báo");

            txt_User.Clear();
            txt_Password.Clear();
            txt_HoTen.Clear();
            loadTaiKhoan();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string username = txt_User.Text;
            string password = txt_Password.Text;
            string hoten = txt_HoTen.Text;

            string suaUser = $"UPDATE TaiKhoan SET Password = '{password}', HoTen = '{hoten}' WHERE Username = '{username}'";

            int exc = gen.ThemSuaXoa(suaUser);
            if (exc >= 1) MessageBox.Show("Sửa thành công", "Thông báo");
            else MessageBox.Show("Sửa không thành công", "Thông báo");

            txt_User.Clear();
            txt_Password.Clear();
            txt_HoTen.Clear();
            loadTaiKhoan();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string username = txt_User.Text;

            string xoaUser = $"Delete from TaiKhoan where Username = {username}";
            int exc = gen.ThemSuaXoa(xoaUser);
            if (exc >= 1) MessageBox.Show("Xóa thành công", "Thông báo");
            else MessageBox.Show("Xóa không thành công", "Thông báo");

            txt_User.Clear();
            loadTaiKhoan();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gv_TaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gv_TaiKhoan.Rows.Count)
            {
                DataGridViewRow row = gv_TaiKhoan.Rows[e.RowIndex];

                if (row != null)
                {
                    txt_User.Text = row.Cells["Username"].Value?.ToString();
                    txt_Password.Text = row.Cells["Password"].Value?.ToString();
                    txt_HoTen.Text = row.Cells["HoTen"].Value?.ToString();
                }
            }
        }

        
    }
}
