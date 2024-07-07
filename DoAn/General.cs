using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DoAn
{
    class General
    {
        private string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Class\Hè - 2024\CS 464 SA\DoAn\DoAn\CS464.mdf;Integrated Security=True";
        SqlConnection conn;
  
        public General()
        {
            conn = new SqlConnection(connString);
        }

        public int ThemSuaXoa(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
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
