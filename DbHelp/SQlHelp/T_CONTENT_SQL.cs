using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbHelp.SQlHelp
{
    public class T_CONTENT_SQL
    {
        private string connstring;

        public T_CONTENT_SQL(string conn)
        {
            connstring = conn;
        }



        public int Insert(CONTENT c)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = string.Format(@"INSERT INTO T_CONTENT (C_NAME,C_TELEPHONE,C_MAIL,C_CONTENT,C_DATE) VALUES                      (N'{0}','{1}','{2}',N'{3}','{4}')",c.C_NAME,c.C_TELEPHONE,c.C_MAIL,c.C_CONTENT,DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public int Delete(string date)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_CONTENT SET C_ISDEL='0' WHERE C_DATE='{0}'", date);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public List<CONTENT> QueryByWhere_XP(string where)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format(@" SELECT ROW_NUMBER()OVER (order by C_DATE DESC) NUM,* FROM T_CONTENT  WHERE C_ISDEL='1' {0} ", where);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToContent(dt);

            }
        }

        public List<CONTENT> QueryByWhere_XP(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format(@" SELECT ROW_NUMBER()OVER (order by C_DATE DESC) NUM,* FROM T_CONTENT  WHERE C_ISDEL='1' {0} ", where); DataTable dt = new DataTable();
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToContent(dt);

            }
        }

        public string GetTotal(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_CONTENT WHERE C_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        public List<CONTENT> DataToContent(DataTable dt)
        {
            List<CONTENT> list = new List<CONTENT>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CONTENT c = new CONTENT
                    {
                            C_CONTENT = dt.Rows[i]["C_CONTENT"].ToString(),
                            C_DATE = dt.Rows[i]["C_DATE"].ToString(),
                            C_ISDEL = dt.Rows[i]["C_ISDEL"].ToString(),
                            C_MAIL = dt.Rows[i]["C_MAIL"].ToString(),
                            C_NAME = dt.Rows[i]["C_NAME"].ToString(),
                            C_TELEPHONE = dt.Rows[i]["C_TELEPHONE"].ToString(),
                            NUM= dt.Rows[i]["NUM"].ToString()
                    };
                    list.Add(c);
                }
            }
            return list;

        }
    }
}
