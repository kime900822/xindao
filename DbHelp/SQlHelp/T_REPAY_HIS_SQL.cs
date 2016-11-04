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
    public  class T_REPAY_HIS_SQL
    {
        private string connstring;

        public T_REPAY_HIS_SQL(string conn)
        {
            connstring = conn;
        }

        public int Insert(REPAY_HIS r)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(R_SYSID) FROM T_REPAY_HIS WHERE B_SYSID='{0}' ;", r.B_SYSID);
                    string r_sysid = string.Empty;
                    r_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(r_sysid))
                    {
                        r_sysid = r.B_SYSID + "01";
                    }
                    else
                        r_sysid = (Convert.ToInt64(r_sysid) + 1).ToString();

                    cmd.CommandText = string.Format(@"INSERT INTO T_REPAY_HIS (B_SYSID,R_AMOUNT,R_TYPE,R_OVERTIME,R_FINE,R_DATE,R_INTEREST,R_SYSID) VALUES
                      ('{0}','{1}',N'{2}','{3}',N'{4}','{5}','{6}','{7}')",
                      r.B_SYSID, r.R_AMOUNT,r.R_TYPE,r.R_OVERTIME,r.R_FINE,r.R_DATE,r.R_INTEREST, r_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="r_sysid"></param>
        /// <returns></returns>
        public int Delete(string r_sysid )
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE  T_REPAY_HIS SET R_ISDEL='0' WHERE R_SYSID='{0}'", r_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        /// <summary>
        /// 取总数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public string GetTotal(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_REPAY_HIS A LEFT JOIN T_BORROW B ON A.B_SYSID=B.B_SYSID LEFT JOIN T_USER C ON B.U_SYSID=C.U_SYSID WHERE R_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        
        public int Delete_Borrow(string b_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE  T_REPAY_HIS SET R_ISDEL='0' WHERE B_SYSID='{0}'", b_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int Update(REPAY_HIS r)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_REPAY_HIS
                    SET 
                     R_AMOUNT=N'{0}',
                     R_TYPE=N'{1}',
                     R_OVERTIME='{2}',
                     R_FINE='{3}',
                     R_DATE='{4}',
                     R_INTEREST='{6}'
                    WHERE R_SYSID='{5}'",r.R_AMOUNT,r.R_TYPE,r.R_OVERTIME,r.R_FINE,r.R_DATE,r.R_SYSID,r.R_INTEREST);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable QueryByWhere(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                DataTable dt = new DataTable();

                string sql = string.Format("SELECT R_DATE AS '还款日期', R_TYPE AS '还款方式',R_AMOUNT AS '还款本金',R_INTEREST AS '还款利息', R_OVERTIME AS '逾期时间',R_FINE AS '处罚金'  FROM T_REPAY_HIS WHERE  R_ISDEL='1'  {0} ", where);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;


            }

        }


        public string GetRepayAmount(string b_sysid) {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT SUM(CAST(R_AMOUNT as numeric(10,2)))  AS R_AMOUNT FROM T_REPAY_HIS WHERE  R_ISDEL='1' AND B_SYSID='{0}' ", b_sysid);
                    return cmd.ExecuteScalar().ToString();
                }


            }

        }

        public List<REPAY_HIS> QueryByWhere_XP(string where)
        {
            try {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    DataTable dt = new DataTable();

                    string sql = string.Format("SELECT  ROW_NUMBER()OVER (order by R_DATE DESC) NUM,* FROM T_REPAY_HIS A LEFT JOIN T_BORROW B ON A.B_SYSID=B.B_SYSID LEFT JOIN T_USER C ON B.U_SYSID=C.U_SYSID WHERE R_ISDEL='1' {0} ", where);
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToRepay(dt);


                }
            }
            catch {
                return new List<REPAY_HIS>();
            }

        }


        public REPAY_HIS QueryByWhere_Borrow(string where)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    DataTable dt = new DataTable();

                    string sql = string.Format("SELECT  ROW_NUMBER()OVER (order by R_DATE DESC) NUM,* FROM T_REPAY_HIS A LEFT JOIN T_BORROW B ON A.B_SYSID=B.B_SYSID LEFT JOIN T_USER C ON B.U_SYSID=C.U_SYSID WHERE  {0} ", where);
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToRepay(dt)[0];


                }
            }
            catch
            {
                return new REPAY_HIS();
            }

        }

        public List<REPAY_HIS> QueryByWhere_XP(string where, int pageid, int num)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                DataTable dt = new DataTable();

                string sql_t = string.Format("SELECT  ROW_NUMBER()OVER (order by R_DATE DESC) NUM,A.* FROM T_REPAY_HIS A LEFT JOIN T_BORROW B ON A.B_SYSID=B.B_SYSID LEFT JOIN T_USER C ON B.U_SYSID=C.U_SYSID WHERE R_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToRepay(dt);


            }

        }

        public  List<REPAY_HIS> DataToRepay(DataTable dt)
        {
            List<REPAY_HIS> list = new List<REPAY_HIS>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    REPAY_HIS r = new REPAY_HIS();
                    r.B_SYSID = dt.Rows[i]["B_SYSID"].ToString();
                    r.R_AMOUNT = dt.Rows[i]["R_AMOUNT"].ToString();
                    r.R_TYPE = dt.Rows[i]["R_TYPE"].ToString();
                    r.R_FINE = dt.Rows[i]["R_FINE"].ToString();
                    r.R_DATE = dt.Rows[i]["R_DATE"].ToString();
                    r.R_ISDEL = dt.Rows[i]["R_ISDEL"].ToString();
                    r.R_SYSID = dt.Rows[i]["R_SYSID"].ToString();
                    r.R_INTEREST = dt.Rows[i]["R_INTEREST"].ToString();
                    r.BORROW = new T_BORROW_SQL(connstring).QueryByWhere_XP(string.Format(" AND B_SYSID='{0}'", dt.Rows[i]["B_SYSID"].ToString()),false)[0];
                    r.NUM= dt.Rows[i]["NUM"].ToString();
                    list.Add(r);
                }


            }
            return list;

        }
    }
}
