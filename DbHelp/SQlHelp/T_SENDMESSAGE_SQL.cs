using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbHelp.SQlHelp
{
    public class T_SENDMESSAGE_SQL
    {
        private string connstring;


        public T_SENDMESSAGE_SQL(string conn)
        {
            connstring = conn;
        }


        public int Insert(MESSAGE m)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(S_SYSID) FROM T_SENDMESSAGE WHERE  SUBSTRING(S_SYSID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                    string s_sysid = string.Empty;
                    s_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(s_sysid))
                    {
                        s_sysid = DateTime.Now.ToString("yyyyMMdd")  + "4000001";
                    }
                    else
                        s_sysid = (Convert.ToInt64(s_sysid) + 1).ToString();


                    cmd.CommandText = string.Format(@"INSERT INTO T_SENDMESSAGE (S_SYSID,U_SYSID,S_TELEPHONE,S_SENDDATE,S_MESSAGE,S_COMMIT,S_FLAG) VALUES                      ('{0}','{1}',N'{2}','{3}',N'{4}',N'{5}',N'{6}')",
                         s_sysid, m.U_SYSID, m.S_TELEPHONE, m.S_SENDDATE, m.S_MESSAGE, m.S_COMMIT, m.S_FLAG);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public int Delete_User(string u_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_SENDMESSAGE SET S_ISDEL='0' WHERE U_SYSID='{0}'", u_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        public string GetTotal(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_SENDMESSAGE A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE S_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        public int Delete(string s_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_SENDMESSAGE SET S_ISDEL='0' WHERE S_SYSID='{0}'", s_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public List<MESSAGE> QueryByWhere_XP(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();


                string sql = string.Format("SELECT ROW_NUMBER()OVER (order by S_SENDDATE DESC) NUM,A.* FROM T_SENDMESSAGE A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE S_ISDEL='1' {0}", where);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToMessage(dt);

            }

        }

        public List<MESSAGE> QueryByWhere_XP(string where, int pageid, int num)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();


                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by S_SENDDATE DESC) NUM,A.* FROM T_SENDMESSAGE A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE S_ISDEL='1' {0}", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToMessage(dt);

            }

        }


        public DataTable QueryByWhere(string where, int pageid, int num)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();


                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by S_SENDDATE DESC) NUM,A.* FROM T_SENDMESSAGE A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE S_ISDEL='1' {0}", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }

        }

        public void Update(MESSAGE m){
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_SENDMESSAGE SET 
                    S_NUM='{0}',
                    S_TELEPHONE='{1}',
                    S_SENDDATE='{2}',
                    U_SYSID='{3}',
                    S_MESSAGE=N'{4}',
                    S_COMMIT=N'{5}',
                    S_FLAG='{6}'
                    WHERE S_SYSID='{7}'",m.S_NUM,m.S_TELEPHONE,m.S_SENDDATE,m.U_SYSID,m.S_MESSAGE,m.S_COMMIT,m.S_FLAG, m.S_SYSID);
                    cmd.ExecuteNonQuery();
                }


            }

        }

        public  List<MESSAGE> DataToMessage(DataTable dt)
        {
            List<MESSAGE> list = new List<MESSAGE>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MESSAGE m = new MESSAGE();
                    m.S_TELEPHONE = dt.Rows[i]["S_TELEPHONE"].ToString();
                    m.S_SYSID = dt.Rows[i]["S_SYSID"].ToString();
                    m.U_SYSID = dt.Rows[i]["U_SYSID"].ToString();
                    m.S_SENDDATE = dt.Rows[i]["S_SENDDATE"].ToString();
                    m.S_MESSAGE = dt.Rows[i]["S_MESSAGE"].ToString();
                    List<USER> lu = new T_USER_SQL(connstring).QueryByWhere_XP(string.Format(" AND U_SYSID='{0}'", dt.Rows[i]["U_SYSID"].ToString()));
                    if (lu.Count > 0)
                    {
                        m.USER = lu[0];
                    }
                    else {
                        USER u = new USER();
                        u.U_NAME = "测试";
                        m.USER = u;
                    }
                    m.S_COMMIT = dt.Rows[i]["S_COMMIT"].ToString();
                    m.NUM= dt.Rows[i]["NUM"].ToString();
                    m.S_FLAG= dt.Rows[i]["S_FLAG"].ToString();
                    m.S_NUM= dt.Rows[i]["S_NUM"].ToString();
                    list.Add(m);
                }


            }
            return list;

        }

    }
}
