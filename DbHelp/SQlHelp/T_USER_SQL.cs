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
    public class T_USER_SQL
    {

        private string connstring;

        public T_USER_SQL(string conn) {
            connstring = conn;
        }


        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool logon(string name, string password, out USER logonUser) {
            bool flag = false;
            using (SqlConnection conn = new SqlConnection(connstring)) {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = string.Format("SELECT COUNT(*) FROM T_USER WHERE U_ID='{0}' AND U_PASSWORD='{1}' AND U_ISDEL='1' ", name, password);
                    string where = string.Empty;
                    DataTable dt = new DataTable();
                    if (cmd.ExecuteScalar().ToString() == "1") {
                        flag = true;
                        where = string.Format(" AND U_ID='{0}'", name);
                    }

                    cmd.CommandText = string.Format("SELECT COUNT(*) FROM T_USER WHERE U_TELEPHONE='{0}' AND U_PASSWORD='{1}' AND U_ISDEL='1'  ", name, password);
                    if (cmd.ExecuteScalar().ToString() == "1")
                    {
                        flag = true;
                        where = string.Format(" AND U_TELEPHONE='{0}'", name);
                    }

                    cmd.CommandText = string.Format("SELECT COUNT(*) FROM T_USER WHERE U_MAIL='{0}' AND U_PASSWORD='{1}' AND U_ISDEL='1' ", name, password);
                    if (cmd.ExecuteScalar().ToString() == "1")
                    {
                        flag = true;
                        where = string.Format(" AND  U_MAIL='{0}'", name);
                    }

                    if (flag == true)
                    {
                        logonUser = QueryByWhere_XP(where)[0];
                    }
                    else
                        logonUser = new USER();

                }

                return flag;
            }

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(USER user)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;SELECT MAX(U_SYSID) FROM T_USER WHERE SUBSTRING(U_SYSID,1,8)='{0}';", DateTime.Now.ToString("yyyyMMdd"));
                    string u_sysid = string.Empty;
                    u_sysid = cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(u_sysid))
                    {
                        u_sysid = DateTime.Now.ToString("yyyyMMdd") + "10001";
                    }
                    else
                        u_sysid = (Convert.ToInt64(u_sysid) + 1).ToString();

                    cmd.CommandText = string.Format(@"INSERT INTO T_USER (U_SYSID,U_ID,U_TELEPHONE,U_MAIL,U_PASSWORD,U_NAME,U_DATE,U_PROVINCE,U_CITY,U_AREA) VALUES (                      
                         '{0}','{1}','{2}','{3}','{4}',N'{5}','{6}',N'{7}',N'{8}',N'{9}' )", u_sysid, user.U_ID, user.U_TELEPHONE, user.U_MAIL, user.U_PASSWORD, user.U_NAME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user.U_PROVINCE, user.U_CITY, user.U_AREA);
                    return cmd.ExecuteNonQuery();
                }
            }

        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int DeleteUser(string u_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE T_USER SET U_ISDEL='0' WHERE U_SYSID={0};", u_sysid);
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
                    cmd.CommandText = string.Format("SELECT COUNT(1) FROM T_USER WHERE U_ISDEL='1' AND U_SYSID<>'1' {0}", where);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        /// <summary>
        /// 验证用户名,电话号码，邮箱
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckUser(string data, string type)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    if (type == "id")
                        cmd.CommandText = string.Format("SELECT COUNT(*) FROM T_USER WHERE U_ID='{0}' AND U_ISDEL='1' ", data);
                    else if ((type == "telephone"))
                        cmd.CommandText = string.Format("SELECT COUNT(*) FROM T_USER WHERE U_TELEPHONE='{0}' AND U_ISDEL='1' ", data);
                    else
                        cmd.CommandText = string.Format("SELECT COUNT(*) FROM T_USER WHERE U_MAIL='{0}' AND U_ISDEL='1' ", data);

                    if (cmd.ExecuteScalar().ToString() == "1")
                    {
                        return true;
                    }
                    else
                        return false;


                }
            }

        }

        /// <summary>
        /// 查找密码
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPassword(string telephone)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT U_PASSWORD FROM T_USER WHERE U_TELEPHONE='{0}' AND U_ISDEL='1' ", telephone);

                    return cmd.ExecuteScalar().ToString();
                }
            }

        }



        /// <summary>
        /// 后台登录验证
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public string GetPassword_web(string telephone)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT U_PASSWORD FROM T_USER WHERE U_TELEPHONE='{0}' AND U_SYSID='1' AND U_ISDEL='1' ", telephone);

                    return cmd.ExecuteScalar().ToString();
                }
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ModPassword(USER user, string npass)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE T_USER  SET U_PASSWORD='{0}' WHERE U_SYSID='{1}' AND U_ISDEL='1' ", npass, user.U_SYSID);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }

        }


        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataTable QueryByWhere(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                DataTable dt = new DataTable();

                string sql = string.Format("SELECT * FROM T_USER WHERE U_ISDEL='1'  AND U_SYSID<>'1' {0} ", where);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;

            }

        }

        public List<USER> QueryByWhere_XP(string where)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                DataTable dt = new DataTable();

                string sql = string.Format("SELECT ROW_NUMBER()OVER (order by U_DATE DESC) NUM,* FROM T_USER WHERE U_ISDEL='1' AND U_SYSID<>'1' {0}  ", where);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToUSER(dt);

            }

        }


        public List<USER>Get_User(string u_sysid)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                DataTable dt = new DataTable();

                string sql = string.Format("SELECT ROW_NUMBER()OVER (order by U_DATE DESC) NUM,* FROM T_USER WHERE  U_SYSID='{0}'  ", u_sysid);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToUSER(dt);

            }

        }

        public List<USER> QueryByWhere_XP(string where, int pageid, int num)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                DataTable dt = new DataTable();

                string sql_t = string.Format("SELECT ROW_NUMBER()OVER (order by U_DATE DESC) NUM,* FROM T_USER WHERE U_ISDEL='1' AND U_SYSID<>'1' {0}  ", where);
                string sql = string.Format("SELECT * FROM ({0}) T WHERE NUM>{1} AND NUM<={2}", sql_t, pageid * num, (pageid + 1) * num);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
                return DataToUSER(dt);

            }

        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public int Update(USER u)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = string.Format(@"UPDATE T_USER SET U_ID='{0}',U_TELEPHONE='{1}',U_MAIL='{2}',U_NAME=N'{3}',U_FREEMESSAGE='{4}',U_BALANCE='{5}',U_PASSWORD='{7}',U_PROVINCE=N'{8}',U_CITY=N'{9}',U_AREA=N'{10}',U_SHORT=N'{11}' WHERE U_SYSID='{6}' AND U_ISDEL='1' ", u.U_ID, u.U_TELEPHONE, u.U_MAIL, u.U_NAME, u.U_FREEMESSAGE, u.U_BALANCE, u.U_SYSID, u.U_PASSWORD, u.U_PROVINCE, u.U_CITY, u.U_AREA,u.U_SHORT);
                    return cmd.ExecuteNonQuery();

                }

            }

        }


        public int Pripard(PREPAID_HIS p)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_USER SET U_FREEMESSAGE=U_FREEMESSAGE+'{0}',U_BALANCE=U_BALANCE+'{1}' WHERE U_SYSID='{2}' AND U_ISDEL='1' ", p.P_FREEMESSAGE, p.P_AMOUNT, p.U_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }

        public int Update_pripard_u(PREPAID_HIS p, PREPAID_HIS po)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_USER SET U_FREEMESSAGE=U_FREEMESSAGE+'{0}',U_BALANCE=U_BALANCE+'{1}' WHERE U_SYSID='{2}' AND U_ISDEL='1' ", Convert.ToDouble(p.P_FREEMESSAGE)- Convert.ToDouble(po.P_FREEMESSAGE), Convert.ToDouble(p.P_AMOUNT)- Convert.ToDouble(po.P_AMOUNT), po.U_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }


        public int Update_pripard_d(PREPAID_HIS p)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_USER SET U_FREEMESSAGE=U_FREEMESSAGE-'{0}',U_BALANCE=U_BALANCE-'{1}' WHERE U_SYSID='{2}' AND U_ISDEL='1' ", p.P_FREEMESSAGE, p.P_AMOUNT, p.U_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }


        public int Debit_Amount(DEBIT_HIS d)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_USER SET U_BALANCE=U_BALANCE-'{0}' WHERE U_SYSID='{1}' AND U_ISDEL='1' ", d.D_AMOUNT, d.U_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }

        public int Debit_Message(MESSAGE m)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"UPDATE T_USER SET U_FREEMESSAGE=U_FREEMESSAGE-'{0}' WHERE U_SYSID='{1}' AND U_ISDEL='1' ", 1, m.U_SYSID);
                    return cmd.ExecuteNonQuery();

                }

            }

        }

        public int GetMessage(MESSAGE m) {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT U_FREEMESSAGE FROM T_USER WHERE U_SYSID='{0}'", m.U_SYSID);
                    return Convert.ToInt32(cmd.ExecuteScalar());

                }

            }
        }


        public  List<USER> DataToUSER(DataTable dt) {
            List<USER> list = new List<USER>();
            if (dt.Rows.Count > 0) {
                for (int i = 0; i < dt.Rows.Count; i++) {
                    USER user = new USER
                    {
                        U_SYSID = dt.Rows[i]["U_SYSID"].ToString(),
                        U_ID = dt.Rows[i]["U_ID"].ToString(),
                        U_TELEPHONE = dt.Rows[i]["U_TELEPHONE"].ToString(),
                        U_MAIL= dt.Rows[i]["U_MAIL"].ToString(),
                        U_PASSWORD= dt.Rows[i]["U_PASSWORD"].ToString(),
                        U_NAME= dt.Rows[i]["U_NAME"].ToString(),
                        U_FREEMESSAGE=dt.Rows[i]["U_FREEMESSAGE"].ToString(),
                        U_BALANCE= dt.Rows[i]["U_BALANCE"].ToString(),
                        U_DATE= dt.Rows[i]["U_DATE"].ToString(),
                        U_ISDEL= dt.Rows[i]["U_ISDEL"].ToString(),
                        U_PROVINCE= dt.Rows[i]["U_PROVINCE"].ToString(),
                        U_AREA = dt.Rows[i]["U_AREA"].ToString(),
                        U_CITY = dt.Rows[i]["U_CITY"].ToString(),
                        NUM = dt.Rows[i]["NUM"].ToString(),
                        U_SHORT= dt.Rows[i]["U_SHORT"].ToString()
                    };
                    list.Add(user);
                }
            }
            return list;

        }


    }
}
