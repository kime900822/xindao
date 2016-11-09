using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelp.SQlHelp
{
    public class T_DEBIT_HIS_SQL
    {
        private string connstring;

        public T_DEBIT_HIS_SQL(string conn)
        {
            connstring = conn;
        }


        public int Insert(DEBIT_HIS  d)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(D_SYSID) FROM T_DEBIT_HIS WHERE SUBSTRING(D_SYSID,1,8)='{0}';",  DateTime.Now.ToString("yyyyMMdd"));
                    string d_sysid = string.Empty;
                    d_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(d_sysid))
                    {
                        d_sysid = DateTime.Now.ToString("yyyyMMdd")+  "5000001";
                    }
                    else
                        d_sysid = (Convert.ToInt64(d_sysid) + 1).ToString();


                    cmd.CommandText = string.Format(@"INSERT INTO T_DEBIT_HIS (U_SYSID,D_REASON,D_AMOUNT,D_DATE,D_SYSID) VALUES                      ('{0}',N'{1}','{2}','{3}','{4}')",
                         d.U_SYSID, d.D_REASON, d.D_AMOUNT, d.D_DATE, d_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable QueryByWhere(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format("SELECT  ROW_NUMBER()OVER (order by D_DATE DESC) NUM,D_REASON AS '扣款原因',D_AMOUNT AS '扣款金额',D_DATE AS '扣款时间' FROM T_DEBIT_HIS A WHERE D_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }

        public string GetTotal(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_DEBIT_HIS A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE D_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        /// <summary>
        /// 根据用户删除
        /// </summary>
        /// <param name="u_sysid"></param>
        /// <returns></returns>
        public int Delete_User(string u_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_DEBIT_HIS SET D_ISDEL='0' WHERE U_SYSID='{0}'", u_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="d_sysid"></param>
        /// <returns></returns>
        public int Delete(string d_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_DEBIT_HIS SET D_ISDEL='0' WHERE D_SYSID='{0}'", d_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        public List<DEBIT_HIS> QueryByWhere_XP (string where)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format(@" SELECT ROW_NUMBER()OVER (order by D_DATE DESC) NUM,A.* FROM T_DEBIT_HIS A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID  WHERE D_ISDEL='1' {0} ", where);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToDebit(dt);

            }
        }

        public List<DEBIT_HIS> QueryByWhere_XP(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format(@" SELECT ROW_NUMBER()OVER (order by D_DATE DESC) NUM,A.* FROM T_DEBIT_HIS A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID  WHERE D_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToDebit(dt);

            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public int Update(DEBIT_HIS d)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_DEBIT_HIS SET D_REASON=N'{0}',D_AMOUNT='{1}',D_DATE='{2}' WHERE D_SYSID='{3}' AND D_ISDEL='1' ", d.D_REASON,d.D_AMOUNT,d.D_DATE,d.D_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }


        public List<DEBIT_HIS> DataToDebit(DataTable dt)
        {
            List<DEBIT_HIS> list = new List<DEBIT_HIS>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<USER> lu = new T_USER_SQL(connstring).Get_User(dt.Rows[i]["U_SYSID"].ToString());
                    DEBIT_HIS d = new DEBIT_HIS
                    {
                        U_SYSID = dt.Rows[i]["U_SYSID"].ToString(),
                        D_AMOUNT = dt.Rows[i]["D_AMOUNT"].ToString(),
                        D_REASON = dt.Rows[i]["D_REASON"].ToString(),
                        D_ISDEL = dt.Rows[i]["D_ISDEL"].ToString(),
                        D_DATE = dt.Rows[i]["D_DATE"].ToString(),
                        D_SYSID = dt.Rows[i]["D_SYSID"].ToString(),
                        USER = lu.Count > 0 ? lu[0] : new USER(),
                        NUM = dt.Rows[i]["NUM"].ToString()
                    };
                    list.Add(d);
                }
            }
            return list;

        }
    }
}
