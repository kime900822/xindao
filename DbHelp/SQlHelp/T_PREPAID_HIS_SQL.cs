using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbHelp.SQlHelp
{
    public class T_PREPAID_HIS_SQL
    {
        private string connstring;

        public T_PREPAID_HIS_SQL(string conn)
        {
            connstring = conn;
        }


        public int Insert(PREPAID_HIS p)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(P_SYSID) FROM T_PREPAID_HIS WHERE  SUBSTRING(P_SYSID,1,8)='{0}' AND U_SYSID='{1}' ;",  DateTime.Now.ToString("yyyyMMdd"), p.U_SYSID);
                    string p_sysid = string.Empty;
                    p_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(p_sysid))
                    {
                        p_sysid =  DateTime.Now.ToString("yyyyMMdd")+p.U_SYSID.Substring(9,4) + "60001";
                    }
                    else
                        p_sysid = (Convert.ToInt64(p_sysid) + 1).ToString();


                    cmd.CommandText = string.Format(@"INSERT INTO T_PREPAID_HIS (U_SYSID,P_SYSID,P_AMOUNT,P_FREEMESSAGE,P_TYPE,P_DATE) VALUES                      ('{0}','{1}','{2}','{3}',N'{4}','{5}')",
                         p.U_SYSID, p_sysid, p.P_AMOUNT,p.P_FREEMESSAGE,p.P_TYPE,p.P_DATE);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public DataTable QueryByWhere(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format(@" SELECT ROW_NUMBER()OVER (order by P_DATE DESC) NUM,P_SYSID AS '充值编号', P_FREEMESSAGE AS '短信数' ,P_AMOUNT AS '金额',P_TYPE AS '充值方式',P_DATE AS '充值时间' FROM T_PREPAID_HIS  WHERE P_ISDEL='1' {0} ", where);
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
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_PREPAID_HIS WHERE P_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        public int Delete(string p_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_PREPAID_HIS SET P_ISDEL='0' WHERE P_SYSID='{0}'", p_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public int Update(PREPAID_HIS p)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_PREPAID_HIS SET P_TYPE=N'{0}',P_AMOUNT='{1}',P_FREEMESSAGE='{2}',P_DATE='{3}' WHERE P_SYSID='{4}' AND P_ISDEL='1' ", p.P_TYPE,p.P_AMOUNT,p.P_FREEMESSAGE,p.P_DATE,p.P_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }


        public List<PREPAID_HIS> QueryByWhere_XP(string where)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format(@" SELECT  ROW_NUMBER()OVER (order by P_DATE DESC) NUM,* FROM T_PREPAID_HIS  WHERE P_ISDEL='1' {0} ", where);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToPrepaid(dt);

            }
        }

        public List<PREPAID_HIS> QueryByWhere_XP(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format(@" SELECT  ROW_NUMBER()OVER (order by P_DATE DESC) NUM,A.* FROM T_PREPAID_HIS A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID  WHERE P_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToPrepaid(dt);

            }
        }

        public List<PREPAID_HIS> DataToPrepaid(DataTable dt)
        {
            List<PREPAID_HIS> list = new List<PREPAID_HIS>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PREPAID_HIS p = new PREPAID_HIS
                    {
                        U_SYSID = dt.Rows[i]["U_SYSID"].ToString(),
                        USER = new T_USER_SQL(connstring).QueryByWhere_XP(string.Format(" AND U_SYSID='{0}'", dt.Rows[i]["U_SYSID"].ToString()))[0],
                        P_AMOUNT = dt.Rows[i]["P_AMOUNT"].ToString(),
                        P_FREEMESSAGE = dt.Rows[i]["P_FREEMESSAGE"].ToString(),
                        P_DATE = dt.Rows[i]["P_DATE"].ToString(),
                        P_SYSID = dt.Rows[i]["P_SYSID"].ToString(),
                        P_TYPE = dt.Rows[i]["P_TYPE"].ToString(),
                        P_ISDEL= dt.Rows[i]["P_ISDEL"].ToString(),
                        NUM= dt.Rows[i]["NUM"].ToString()
                    };
                    list.Add(p);
                }
            }
            return list;

        }
    }
}
