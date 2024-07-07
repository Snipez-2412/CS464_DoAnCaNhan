using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn
{
    public partial class frm_Login : Form
    {

        public frm_Login()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text.Trim();
            string password = txt_Password.Text.Trim();

            if (KiemTraDangNhap(username, password))
            {
                this.Hide();
                frm_Main mainForm = new frm_Main();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Sai Username hoặc Mật khẩu","Thông báo");
            }
        }

        public bool KiemTraDangNhap(string username, string password)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Class\Hè - 2024\CS 464 SA\DoAn\DoAn\CS464.mdf;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "select COUNT(*) FROM TaiKhoan WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
