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
    public partial class frm_QLHoaDon : Form
    {
        General gen = new General();

        public frm_QLHoaDon()
        {
            InitializeComponent();
        }

        private void frm_QLHoaDon_Load(object sender, EventArgs e)
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

            loadHoaDon();
        }

        public void loadHoaDon()
        {
            string sql = "Select * from HoaDon";
            DataTable dt = gen.loadDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gv_HoaDon.DataSource = dt;
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
            string maKH = txt_MaKH.Text;
            string maHD = txt_MaHD.Text;
            string tongTien = txt_TongTien.Text;

            string themHD = $"Insert into HoaDon values ('{maHD}', N'{maKH}', N'{maNV}', N'{tongTien}')";
            int exc = gen.ThemSuaXoa(themHD);
            if (exc >= 1) MessageBox.Show("Thêm thành công", "Thông báo");
            else MessageBox.Show("Thêm không thành công", "Thông báo");

            txt_MaNV.Clear();
            txt_MaKH.Clear();
            txt_TongTien.Clear();
            loadHoaDon();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;
            string maKH = txt_MaKH.Text;
            string maHD = txt_MaHD.Text;
            string tongTien = txt_TongTien.Text;

            string suaHD = $"UPDATE HoaDon SET MaKH = '{maKH}', MaNV = '{maNV}', TongTien = '{tongTien}' WHERE MaHD = '{maHD}'";

            int exc = gen.ThemSuaXoa(suaHD);
            if (exc >= 1) MessageBox.Show("Sửa thành công", "Thông báo");
            else MessageBox.Show("Sửa không thành công", "Thông báo");

            txt_MaNV.Clear();
            txt_MaKH.Clear();
            txt_TongTien.Clear();
            loadHoaDon();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string maHD = txt_MaHD.Text;

            string xoaHD = $"Delete from HoaDon where MaNV = {maHD}";
            int exc = gen.ThemSuaXoa(xoaHD);
            if (exc >= 1) MessageBox.Show("Xóa thành công", "Thông báo");
            else MessageBox.Show("Xóa không thành công", "Thông báo");

            txt_MaNV.Clear();
            loadHoaDon();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gv_HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gv_HoaDon.Rows.Count)
            {
                DataGridViewRow row = gv_HoaDon.Rows[e.RowIndex];

                if (row != null)
                {
                    txt_MaHD.Text = row.Cells["MaHD"].Value?.ToString();
                    txt_MaKH.Text = row.Cells["MaKH"].Value?.ToString();
                    txt_MaKH.Text = row.Cells["MaNV"].Value?.ToString();
                    txt_TongTien.Text = row.Cells["TongTien"].Value?.ToString();
                }
            }
        }


    }
}
