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
    public class T_BORROW_SQL
    {
        private string connstring;

        public T_BORROW_SQL(string conn)
        {
            connstring = conn;
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

                string sql = string.Format("SELECT B_SYSID AS '借款单号',case when B_AMOUNT>B_REPAYMENT THEN '未还清' else  '已还清' end AS '借款状态',C_NAME AS '姓名',C_ID AS '身份证号',C_CONTACT AS '联系方式',C_EMERGENCYNAME AS '紧急联系人姓名',C_EMERGENCY AS '紧急联系人方式',C_ADDRESS AS '联系地址',C_SEX AS '性别',G_NAME1 AS '担保人姓名1',G_ID1 AS '担保人身份证号1',G_JOB1 AS '担保人职业1',G_NAME2 AS '担保人姓名2',G_ID2 AS '担保人身份证号2',G_JOB2 AS '担保人职业2',G_NAME3 AS '担保人姓名3',G_ID3 AS '担保人身份证号3',G_JOB3 AS '担保人职业3',G_NAME4 AS '担保人姓名4',G_ID4 AS '担保人身份证号4',G_JOB4 AS '担保人职业4', B_AMOUNT AS '借款金额',B_REPAYMENT AS '已还金额',B_INTEREST AS '年利率',B_TERM AS '期数',B_TYPE AS '借款方式',B_REPAYDATE AS '还款日',B_REMINDDATE AS '提醒日', B_DATE AS '借款日',B_DATETMP AS '借款时间' FROM T_BORROW WHERE B_ISDEL='1' {0} ", where);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }

        public DataTable QueryByWhere(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM, B_SYSID AS '借款单号',case when B_AMOUNT>B_REPAYMENT THEN '未还清' else  '已还清' end AS '借款状态',C_NAME AS '姓名',C_ID AS '身份证号',C_CONTACT AS '联系方式',C_EMERGENCYNAME AS '紧急联系人姓名',C_EMERGENCY AS '紧急联系人方式',C_ADDRESS AS '联系地址',C_SEX AS '性别',G_NAME1 AS '担保人姓名1',G_ID1 AS '担保人身份证号1',G_JOB1 AS '担保人职业1',G_NAME2 AS '担保人姓名2',G_ID2 AS '担保人身份证号2',G_JOB2 AS '担保人职业2',G_NAME3 AS '担保人姓名3',G_ID3 AS '担保人身份证号3',G_JOB3 AS '担保人职业3',G_NAME4 AS '担保人姓名4',G_ID4 AS '担保人身份证号4',G_JOB4 AS '担保人职业4', B_AMOUNT AS '借款金额',B_REPAYMENT AS '已还金额',B_INTEREST AS '利息',B_TERM AS '期数',B_TYPE AS '借款方式',B_REPAYDATE AS '还款日',B_REMINDDATE AS '提醒日', B_DATE AS '借款日',B_DATETMP AS '借款时间' FROM T_BORROW WHERE B_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }

        public DataTable QueryByWhere_CB(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM, B_SYSID AS '借款单号',case when B_AMOUNT>B_REPAYMENT THEN '未还清' else  '已还清' end AS '借款状态',C_NAME AS '姓名',C_ID AS '身份证号',C_CONTACT AS '联系方式',C_EMERGENCYNAME AS '紧急联系人姓名',C_EMERGENCY AS '紧急联系人方式',C_ADDRESS AS '联系地址',C_SEX AS '性别',G_NAME1 AS '担保人姓名1',G_ID1 AS '担保人身份证号1',G_JOB1 AS '担保人职业1',G_NAME2 AS '担保人姓名2',G_ID2 AS '担保人身份证号2',G_JOB2 AS '担保人职业2',G_NAME3 AS '担保人姓名3',G_ID3 AS '担保人身份证号3',G_JOB3 AS '担保人职业3',G_NAME4 AS '担保人姓名4',G_ID4 AS '担保人身份证号4',G_JOB4 AS '担保人职业4',B.U_NAME AS '投资公司', B_AMOUNT AS '借款金额',B_REPAYMENT AS '已还金额',B_INTEREST AS '利息',B_TERM AS '期数',B_TYPE AS '借款方式',B_REPAYDATE AS '还款日',B_REMINDDATE AS '提醒日', B_DATE AS '借款日',B_DATETMP AS '借款时间' FROM T_BORROW A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE B_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }


        public string GetTotal(string where) {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_BORROW WHERE B_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }


        public string GetTotalCustomer(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(distinct(C_ID)) FROM T_BORROW WHERE B_ISDEL='1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }

        }



        public DataTable CustomerQueryByWhere(string where, int pageid, int num)
        {

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM, C_NAME AS '姓名',C_ID AS '身份证号',C_CONTACT AS '联系方式',C_EMERGENCYNAME AS '紧急联系人姓名',C_EMERGENCY AS '紧急联系人方式',C_ADDRESS AS '联系地址',C_SEX AS '性别'  FROM T_BORROW WHERE B_ISDEL='1' {0} ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }
        }

        public DataTable GetItem(string u_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql = string.Format("SELECT B_SYSID+'-'+C_NAME FROM T_BORROW WHERE U_SYSID='{0}' AND B_AMOUNT<>B_REPAYMENT AND B_ISDEL='1'", u_sysid);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;
            }
        }


        public List<BORROW> Get_Borrow(string b_sysid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {

                    conn.Open();
                string  sql = string.Format(@"SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM ,
                 A.B_SYSID        ,
                 A.U_SYSID        ,
                 A.C_NAME         ,
                 A.C_ID           ,
                 A.C_CONTACT      ,
                 A.C_EMERGENCY    ,
                 A.C_ADDRESS      ,
                 A.C_SEX          ,
                 A.G_NAME1        ,
                 A.G_ID1          ,
                 A.G_JOB1         ,
                 A.G_NAME2        ,
                 A.G_ID2          ,
                 A.G_JOB2         ,
                 A.G_NAME3        ,
                 A.G_ID3          ,
                 A.G_JOB3         ,
                 A.G_NAME4        ,
                 A.G_ID4          ,
                 A.G_JOB4         ,
                 A.B_AMOUNT       ,
                 A.B_REPAYMENT    ,
                 A.B_INTEREST     ,
                 A.B_TYPE         ,
                 A.B_REPAYDATE    ,
                 A.B_REMINDDATE   ,
                 A.B_DATE         ,
                 A.B_DATETMP      ,
                 A.B_ISDEL        ,
                 A.B_TERM         ,
                 A.C_EMERGENCYNAME                 
                    FROM T_BORROW A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE B_SYSID= '{0}' ", b_sysid);


                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToCustomer(dt, false);

                }
            }
            catch
            {
                return new List<BORROW>();
            }

        }



        public List<BORROW> QueryByWhere_XP(string where,bool flag)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {

                    conn.Open();
                    string sql = "";
                    if (flag)
                    {
                        sql = string.Format(@"SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM ,
                 A.B_SYSID        ,
                 A.U_SYSID        ,
                 A.C_NAME         ,
                 A.C_ID           ,
                 A.C_PIC          ,
                 A.C_CONTACT      ,
                 A.C_EMERGENCY    ,
                 A.C_ADDRESS      ,
                 A.C_SEX          ,
                 A.G_NAME1        ,
                 A.G_ID1          ,
                 A.G_JOB1         ,
                 A.G_NAME2        ,
                 A.G_ID2          ,
                 A.G_JOB2         ,
                 A.G_NAME3        ,
                 A.G_ID3          ,
                 A.G_JOB3         ,
                 A.G_NAME4        ,
                 A.G_ID4          ,
                 A.G_JOB4         ,
                 A.B_AMOUNT       ,
                 A.B_REPAYMENT    ,
                 A.B_INTEREST     ,
                 A.B_TYPE         ,
                 A.B_REPAYDATE    ,
                 A.B_REMINDDATE   ,
                 A.B_DATE         ,
                 A.B_DATETMP      ,
                 A.B_ISDEL        ,
                 A.B_TERM         ,
                 A.C_EMERGENCYNAME                   
                    FROM T_BORROW A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE B_ISDEL='1' {0} ", where);
                    }
                    else
                    {
                        sql = string.Format(@"SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM ,
                 A.B_SYSID        ,
                 A.U_SYSID        ,
                 A.C_NAME         ,
                 A.C_ID           ,
                 A.C_CONTACT      ,
                 A.C_EMERGENCY    ,
                 A.C_ADDRESS      ,
                 A.C_SEX          ,
                 A.G_NAME1        ,
                 A.G_ID1          ,
                 A.G_JOB1         ,
                 A.G_NAME2        ,
                 A.G_ID2          ,
                 A.G_JOB2         ,
                 A.G_NAME3        ,
                 A.G_ID3          ,
                 A.G_JOB3         ,
                 A.G_NAME4        ,
                 A.G_ID4          ,
                 A.G_JOB4         ,
                 A.B_AMOUNT       ,
                 A.B_REPAYMENT    ,
                 A.B_INTEREST     ,
                 A.B_TYPE         ,
                 A.B_REPAYDATE    ,
                 A.B_REMINDDATE   ,
                 A.B_DATE         ,
                 A.B_DATETMP      ,
                 A.B_ISDEL        ,
                 A.B_TERM         ,
                 A.C_EMERGENCYNAME                 
                    FROM T_BORROW A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE B_ISDEL='1' {0} ", where);

                    }

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return DataToCustomer(dt, flag);

                }
            }
            catch {
                return new List<BORROW>();
            }

        }

        public List<BORROW> QueryByWhere_XP(string where,int pageid,int num)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by B_DATETMP DESC) NUM ,A.* FROM T_BORROW A LEFT JOIN T_USER B ON A.U_SYSID=B.U_SYSID WHERE B_ISDEL='1' {0} ", where);
                string sql=string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}",sql_t, pageid* num,(pageid + 1)*num );
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToCustomer(dt,false);

            }

        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="b"></param>
        /// <param name="u_sysid"></param>
        /// <returns></returns>
        public int Insert(BORROW b)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(B_SYSID) FROM T_BORROW WHERE  SUBSTRING(B_SYSID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                    string b_sysid = string.Empty;
                    b_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(b_sysid))
                    {
                        b_sysid = DateTime.Now.ToString("yyyyMMdd") + "20001";
                    }
                    else
                        b_sysid = (Convert.ToInt64(b_sysid) + 1).ToString();


                    if (b.C_PIC != null)
                    {
                        cmd.CommandText = string.Format(@"INSERT INTO T_BORROW (B_SYSID,U_SYSID,C_NAME,C_ID,C_PIC,C_CONTACT,C_EMERGENCY,C_ADDRESS,C_SEX,G_NAME1,G_ID1,G_JOB1,G_NAME2,G_ID2,G_JOB2,G_NAME3,G_ID3,G_JOB3,G_NAME4,G_ID4,G_JOB4,B_AMOUNT,B_REPAYMENT,B_INTEREST,B_TYPE,B_REPAYDATE,B_REMINDDATE,B_DATE,B_DATETMP,B_TERM,C_EMERGENCYNAME ) VALUES                      ('{0}','{1}',N'{2}','{3}',@C_PIC,'{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}','{20}','{21}','{22}',N'{23}','{24}','{25}','{26}','{27}','{28}',N'{29}')",
b_sysid, b.U_SYSID, b.C_NAME, b.C_ID, b.C_CONTACT, b.C_EMERGENCY, b.C_ADDRESS, b.C_SEX, b.G_NAME1, b.G_ID1, b.G_JOB1, b.G_NAME2, b.G_ID2, b.G_JOB2, b.G_NAME3, b.G_ID3, b.G_JOB3, b.G_NAME4, b.G_ID4, b.G_JOB4, b.B_AMOUNT, b.B_REPAYMENT, b.B_INTEREST, b.B_TYPE, b.B_REPAYDATE, b.B_REMINDDATE, b.B_DATE, b.B_DATETMP, b.B_TERM,b.C_EMERGENCYNAME);
                        cmd.Parameters.Add("@C_PIC", SqlDbType.Image).Value = b.C_PIC;
                    }
                    else
                    {
                        cmd.CommandText = string.Format(@"INSERT INTO T_BORROW (B_SYSID,U_SYSID,C_NAME,C_ID,C_CONTACT,C_EMERGENCY,C_ADDRESS,C_SEX,G_NAME1,G_ID1,G_JOB1,G_NAME2,G_ID2,G_JOB2,G_NAME3,G_ID3,G_JOB3,G_NAME4,G_ID4,G_JOB4,B_AMOUNT,B_REPAYMENT,B_INTEREST,B_TYPE,B_REPAYDATE,B_REMINDDATE,B_DATE,B_DATETMP,B_TERM,C_EMERGENCYNAME ) VALUES                      ('{0}','{1}',N'{2}','{3}','{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}','{20}','{21}','{22}',N'{23}','{24}','{25}','{26}','{27}','{28}',N'{29}')",
b_sysid, b.U_SYSID, b.C_NAME, b.C_ID, b.C_CONTACT, b.C_EMERGENCY, b.C_ADDRESS, b.C_SEX, b.G_NAME1, b.G_ID1, b.G_JOB1, b.G_NAME2, b.G_ID2, b.G_JOB2, b.G_NAME3, b.G_ID3, b.G_JOB3, b.G_NAME4, b.G_ID4, b.G_JOB4, b.B_AMOUNT, b.B_REPAYMENT, b.B_INTEREST, b.B_TYPE, b.B_REPAYDATE, b.B_REMINDDATE, b.B_DATE, b.B_DATETMP, b.B_TERM,b.C_EMERGENCYNAME);
                    }
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public int Delete(string sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_BORROW SET B_ISDEL='0' WHERE B_SYSID='{0}'", sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        /// <summary>
        /// 用户删除时清空借款情况
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public int Delete_User(string u_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_BORROW SET B_ISDEL='0' WHERE U_SYSID='{0}'", u_sysid);
                    return cmd.ExecuteNonQuery();
                }


            }

        }

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public int Update(BORROW b)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    if (b.C_PIC!= null)
                    {
                        cmd.CommandText = string.Format(@"UPDATE T_BORROW
                    SET 
                     C_NAME=N'{0}',
                     C_ID='{1}',
                     C_PIC=@C_PIC,
                     C_CONTACT='{2}',
                     C_EMERGENCY='{3}',
                     C_ADDRESS=N'{4}',
                     C_SEX=N'{5}',
                     G_NAME1=N'{6}',
                     G_ID1='{7}',
                     G_JOB1=N'{8}',
                     G_NAME2=N'{9}',
                     G_ID2='{10}',
                     G_JOB2=N'{11}',
                     G_NAME3=N'{12}',
                     G_ID3='{13}',
                     G_JOB3=N'{14}',
                     G_NAME4=N'{15}',
                     G_ID4='{16}',
                     G_JOB4=N'{17}',
                     B_AMOUNT='{18}',
                     B_REPAYMENT='{19}',
                     B_INTEREST='{20}',
                     B_TYPE=N'{21}',
                     B_REPAYDATE='{22}',
                     B_REMINDDATE='{23}',
                     B_DATE='{24}',
                     B_DATETMP='{25}',
                     B_TERM='{27}',
                     C_EMERGENCYNAME=N'{28}'                
                    WHERE B_SYSID='{26}'", b.C_NAME, b.C_ID, b.C_CONTACT, b.C_EMERGENCY, b.C_ADDRESS, b.C_SEX, b.G_NAME1, b.G_ID1, b.G_JOB1, b.G_NAME2, b.G_ID2, b.G_JOB2, b.G_NAME3, b.G_ID3, b.G_JOB3, b.G_NAME4, b.G_ID4, b.G_JOB4, b.B_AMOUNT, b.B_REPAYMENT, b.B_INTEREST, b.B_TYPE, b.B_REPAYDATE, b.B_REMINDDATE, b.B_DATE, b.B_DATETMP, b.B_SYSID, b.B_TERM, b.C_EMERGENCYNAME);
                        cmd.Parameters.Add("@C_PIC", SqlDbType.Image).Value = b.C_PIC;
                    }
                    else
                    {
                        cmd.CommandText = string.Format(@"UPDATE T_BORROW
                    SET 
                     C_NAME=N'{0}',
                     C_ID='{1}',
                     C_CONTACT='{2}',
                     C_EMERGENCY='{3}',
                     C_ADDRESS=N'{4}',
                     C_SEX=N'{5}',
                     G_NAME1=N'{6}',
                     G_ID1='{7}',
                     G_JOB1=N'{8}',
                     G_NAME2=N'{9}',
                     G_ID2='{10}',
                     G_JOB2=N'{11}',
                     G_NAME3=N'{12}',
                     G_ID3='{13}',
                     G_JOB3=N'{14}',
                     G_NAME4=N'{15}',
                     G_ID4='{16}',
                     G_JOB4=N'{17}',
                     B_AMOUNT='{18}',
                     B_REPAYMENT='{19}',
                     B_INTEREST='{20}',
                     B_TYPE=N'{21}',
                     B_REPAYDATE='{22}',
                     B_REMINDDATE='{23}',
                     B_DATE='{24}',
                     B_DATETMP='{25}',
                     B_TERM={27},
                     C_EMERGENCYNAME=N'{28}'
                    WHERE B_SYSID='{26}'", b.C_NAME, b.C_ID, b.C_CONTACT, b.C_EMERGENCY, b.C_ADDRESS, b.C_SEX, b.G_NAME1, b.G_ID1, b.G_JOB1, b.G_NAME2, b.G_ID2, b.G_JOB2, b.G_NAME3, b.G_ID3, b.G_JOB3, b.G_NAME4, b.G_ID4, b.G_JOB4, b.B_AMOUNT, b.B_REPAYMENT, b.B_INTEREST, b.B_TYPE, b.B_REPAYDATE, b.B_REMINDDATE, b.B_DATE, b.B_DATETMP, b.B_SYSID, b.B_TERM, b.C_EMERGENCYNAME);
                    }

                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public int Update_web(BORROW b)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {


                    cmd.CommandText = string.Format(@"UPDATE T_BORROW
                    SET 
                     C_NAME=N'{0}',
                     C_ID='{1}',
                     C_CONTACT='{2}',
                     C_EMERGENCY='{3}',
                     C_ADDRESS=N'{4}',
                     C_SEX=N'{5}',
                     G_NAME1=N'{6}',
                     G_ID1='{7}',
                     G_JOB1=N'{8}',
                     G_NAME2=N'{9}',
                     G_ID2='{10}',
                     G_JOB2=N'{11}',
                     G_NAME3=N'{12}',
                     G_ID3='{13}',
                     G_JOB3=N'{14}',
                     G_NAME4=N'{15}',
                     G_ID4='{16}',
                     G_JOB4=N'{17}',
                     B_AMOUNT='{18}',
                     B_REPAYMENT='{19}',
                     B_INTEREST='{20}',
                     B_TYPE=N'{21}',
                     B_REPAYDATE='{22}',
                     B_REMINDDATE='{23}',
                     B_DATE='{24}',
                     B_DATETMP='{25}',
                     B_TERM={27},
                     C_EMERGENCYNAME=N'{28}'
                    WHERE B_SYSID='{26}'", b.C_NAME, b.C_ID, b.C_CONTACT, b.C_EMERGENCY, b.C_ADDRESS, b.C_SEX, b.G_NAME1, b.G_ID1, b.G_JOB1, b.G_NAME2, b.G_ID2, b.G_JOB2, b.G_NAME3, b.G_ID3, b.G_JOB3, b.G_NAME4, b.G_ID4, b.G_JOB4, b.B_AMOUNT, b.B_REPAYMENT, b.B_INTEREST, b.B_TYPE, b.B_REPAYDATE, b.B_REMINDDATE, b.B_DATE, b.B_DATETMP, b.B_SYSID ,b.B_TERM,b.C_EMERGENCYNAME);
                    return cmd.ExecuteNonQuery();
                }


            }

        }


        public void UpdateByRepayDelete(REPAY_HIS r) {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_BORROW SET B_REPAYMENT=B_REPAYMENT-{0} WHERE B_SYSID='{1}'", r.R_AMOUNT,r.B_SYSID);
                    cmd.ExecuteNonQuery();
                }


            }

        }


        public void UpdateByRepayInsert(REPAY_HIS r)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_BORROW SET B_REPAYMENT=B_REPAYMENT+{0} WHERE B_SYSID='{1}'", r.R_AMOUNT, r.B_SYSID);
                    cmd.ExecuteNonQuery();
                }


            }

        }

        public void UpdateByRepayUpdate(REPAY_HIS r,double diff)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_BORROW SET B_REPAYMENT=B_REPAYMENT+{0} WHERE B_SYSID='{1}'", diff, r.B_SYSID);
                    cmd.ExecuteNonQuery();
                }


            }

        }


        public  List<BORROW> DataToCustomer(DataTable dt,bool flag)
        {
            List<BORROW> list = new List<BORROW>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BORROW b = new BORROW();
                    b.NUM= dt.Rows[i]["NUM"].ToString();
                    b.B_SYSID = dt.Rows[i]["B_SYSID"].ToString();
                    b.U_SYSID= dt.Rows[i]["U_SYSID"].ToString();
                    b.C_NAME = dt.Rows[i]["C_NAME"].ToString();
                    b.C_ID = dt.Rows[i]["C_ID"].ToString();
                    if (flag) {
                        if (dt.Rows[i]["C_PIC"].ToString() == "")
                            b.C_PIC = null;
                        else
                            b.C_PIC = (byte[])dt.Rows[i]["C_PIC"];
                    }
                    b.C_CONTACT = dt.Rows[i]["C_CONTACT"].ToString();
                    b.C_EMERGENCY = dt.Rows[i]["C_EMERGENCY"].ToString();
                    b.C_ADDRESS = dt.Rows[i]["C_ADDRESS"].ToString();
                    b.C_SEX = dt.Rows[i]["C_SEX"].ToString();
                    b.G_NAME1 = dt.Rows[i]["G_NAME1"].ToString();
                    b.G_ID1 = dt.Rows[i]["G_ID1"].ToString();
                    b.G_JOB1= dt.Rows[i]["G_JOB1"].ToString();
                    b.G_NAME2 = dt.Rows[i]["G_NAME2"].ToString();
                    b.G_ID2 = dt.Rows[i]["G_ID2"].ToString();
                    b.G_JOB2 = dt.Rows[i]["G_JOB2"].ToString();
                    b.G_NAME3 = dt.Rows[i]["G_NAME3"].ToString();
                    b.G_ID3 = dt.Rows[i]["G_ID3"].ToString();
                    b.G_JOB3 = dt.Rows[i]["G_JOB3"].ToString();
                    b.G_NAME4 = dt.Rows[i]["G_NAME4"].ToString();
                    b.G_ID4 = dt.Rows[i]["G_ID4"].ToString();
                    b.G_JOB4 = dt.Rows[i]["G_JOB4"].ToString();
                    b.B_AMOUNT = dt.Rows[i]["B_AMOUNT"].ToString();

                    b.B_REPAYMENT = dt.Rows[i]["B_REPAYMENT"].ToString();
                    //b.B_REPAYMENT = new T_REPAY_HIS_SQL(connstring).GetRepayAmount(b.B_SYSID);

                    b.B_INTEREST = dt.Rows[i]["B_INTEREST"].ToString();
                    b.B_TYPE = dt.Rows[i]["B_TYPE"].ToString();
                    b.B_REPAYDATE = dt.Rows[i]["B_REPAYDATE"].ToString();
                    b.B_REMINDDATE = dt.Rows[i]["B_REMINDDATE"].ToString();
                    b.B_DATE = dt.Rows[i]["B_DATE"].ToString();
                    b.B_DATETMP = dt.Rows[i]["B_DATETMP"].ToString();
                    b.B_ISDEL = dt.Rows[i]["B_ISDEL"].ToString();
                    b.B_TERM= dt.Rows[i]["B_TERM"].ToString();
                    List<USER> lu = new T_USER_SQL(connstring).Get_User(dt.Rows[i]["U_SYSID"].ToString());
                    if (lu.Count > 0)
                        b.USER = lu[0];
                    else
                        b.USER = new USER();
                    b.C_EMERGENCYNAME= dt.Rows[i]["C_EMERGENCYNAME"].ToString();
                    list.Add(b);
                }


            }
            return list;

        }

    }
}
