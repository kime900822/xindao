using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbHelp.SQlHelp
{
    public class T_EDIT_SQL
    {

        private string connstring;

        public T_EDIT_SQL(string conn)
        {
            connstring = conn;
        }



        public int Insert(EDIT e)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(E_SYSID) FROM T_EDIT WHERE  SUBSTRING(E_SYSID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                    string e_sysid = string.Empty;
                    e_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(e_sysid))
                    {
                        e_sysid =  DateTime.Now.ToString("yyyyMMdd") + "70001";
                    }
                    else
                        e_sysid = (Convert.ToInt64(e_sysid) + 1).ToString();


                    cmd.CommandText = string.Format(@"INSERT INTO T_EDIT (E_SYSID,E_TITLE,E_EDITOR,E_CONTENT,E_DATETIME,E_TITLEPIC) VALUES                      ('{0}',N'{1}',N'{2}',N'{3}','{4}',N'{5}')",
                         e_sysid, e.E_TITLE,e.E_EDITOR,e.E_CONTENT,e.E_DATETIME,e.E_TITLEPIC);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        public int Update(EDIT e)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_EDIT SET E_TITLE=N'{0}',E_EDITOR=N'{1}',E_CONTENT=N'{2}',E_DATETIME='{3}',E_TITLEPIC=N'{4}' WHERE E_SYSID='{5}' AND E_ISDEL='1' ", e.E_TITLE,e.E_EDITOR,e.E_CONTENT,e.E_DATETIME,e.E_TITLEPIC,e.E_SYSID);
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
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_EDIT WHERE E_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        public int Delete(string e_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_EDIT SET E_ISDEL='0' WHERE E_SYSID='{0}'", e_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public DataTable QueryByWhere(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format(@" SELECT ROW_NUMBER()OVER (order by E_DATETIME DESC) AS  NUM ,E_SYSID AS  '编号', E_TITLE AS '标题', E_DATETIME AS '时间' FROM T_EDIT  WHERE E_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }

        public List<EDIT> QueryByWhere_XP(string where)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format(@" SELECT  ROW_NUMBER()OVER (order by E_DATETIME DESC) NUM ,* FROM T_EDIT  WHERE E_ISDEL='1' {0} ", where);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToEdit(dt);

            }
        }

        public List<EDIT> QueryByWhere_XP(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format(@" SELECT  ROW_NUMBER()OVER (order by E_DATETIME DESC) NUM ,* FROM T_EDIT   WHERE E_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToEdit(dt);

            }
        }

        public List<EDIT> DataToEdit(DataTable dt)
        {
            List<EDIT> list = new List<EDIT>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EDIT d = new EDIT
                    {
                        E_SYSID = dt.Rows[i]["E_SYSID"].ToString(),
                        E_CONTENT = dt.Rows[i]["E_CONTENT"].ToString(),
                        E_TITLE = dt.Rows[i]["E_TITLE"].ToString(),
                        E_EDITOR = dt.Rows[i]["E_EDITOR"].ToString(),
                        E_DATETIME = dt.Rows[i]["E_DATETIME"].ToString(),
                        E_ISDEL = dt.Rows[i]["E_ISDEL"].ToString(),
                        NUM= dt.Rows[i]["NUM"].ToString(),
                        E_TITLEPIC = dt.Rows[i]["E_TITLEPIC"].ToString()
                    };
                    list.Add(d);
                }
            }
            return list;

        }



    }
}
