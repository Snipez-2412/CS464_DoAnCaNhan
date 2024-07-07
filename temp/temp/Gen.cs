using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace temp
{
    class GenSanPham
    {
        private string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";
        SqlConnection conn;

        public GenSanPham()
        {
            conn = new SqlConnection(connString);
        }

        public int ThemSuaXoa(string sql, List<SqlParameter> parameters)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.Parameters.AddRange(parameters.ToArray());
            conn.Open();
            int kq = comm.ExecuteNonQuery();
            conn.Close();
            return kq;
        }



        public object getValue(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            object kq = comm.ExecuteScalar();
            conn.Close();
            return kq;
        }

        public DataTable loadDataTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            return dt;
        }
    }

    class GenKhachHang
    {
        private string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\SanPham.mdf;Integrated Security=True";
        SqlConnection conn;

        public GenKhachHang()
        {
            conn = new SqlConnection(connString);
        }

        public int ThemSuaXoa(string sql, List<SqlParameter> parameters)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.Parameters.AddRange(parameters.ToArray());
            conn.Open();
            int kq = comm.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public object getValue(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            object kq = comm.ExecuteScalar();
            conn.Close();
            return kq;
        }

        public DataTable loadDataTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            return dt;
        }
    }
}
