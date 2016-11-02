using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbHelp.SQlHelp
{
    public class T_PROVINCE_SQL
    {
        private string connstring;

        public T_PROVINCE_SQL(string conn)
        {
            connstring = conn;
        }


        public DataTable GetProvince() {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM T_PROVINCE");
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }

        public DataTable GetCity(string fid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM T_CITY WHERE FATHERID='{0}'",fid);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }


        public DataTable GetArea(string fid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM T_AREA WHERE FATHERID='{0}'", fid);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }


        public string GetValue_Province(string text)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT PROVINCEID FROM T_PROVINCE WHERE PROVINCE=N'{0}'", text);
                    return cmd.ExecuteScalar().ToString();
                }


            }
        }


        public string GetValue_City(string text)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT CITYID FROM T_CITY WHERE CITY=N'{0}'", text);
                    return cmd.ExecuteScalar().ToString();
                }


            }
        }


        public string GetValue_Area(string text)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT AREAID FROM T_AREA WHERE AREA=N'{0}'", text);
                    return cmd.ExecuteScalar().ToString();
                }


            }
        }
    }
}
