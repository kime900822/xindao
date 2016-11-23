 using DbHelp.CS;
using DbHelp.SQlHelp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace WageManagerSystem.UI
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        #region 数据库链接相关
        /// <summary>
        /// 链接
        /// </summary>
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;


        /// <summary>
        /// 用户管理
        /// </summary>
        public static T_USER_SQL user_sql =new T_USER_SQL(connString);

        /// <summary>
        /// 借款记录
        /// </summary>
        public static T_BORROW_SQL borrow_sql = new T_BORROW_SQL(connString);


        /// <summary>
        /// 消费记录
        /// </summary>
        public static T_DEBIT_HIS_SQL debit_his_sql = new T_DEBIT_HIS_SQL(connString);

        /// <summary>
        /// 还款记录
        /// </summary>
        public static T_REPAY_HIS_SQL repay_his_sql = new T_REPAY_HIS_SQL(connString);

        /// <summary>
        /// 充值
        /// </summary>
        public static T_PREPAID_HIS_SQL prepaid_his_sql = new T_PREPAID_HIS_SQL(connString);

        public static T_EDIT_SQL edit_sql = new T_EDIT_SQL(connString);

        public static T_PROVINCE_SQL province_sql = new T_PROVINCE_SQL(connString);

        public static T_CONTENT_SQL content_sql = new T_CONTENT_SQL(connString);

        public static T_SENDMESSAGE_SQL message_sql = new T_SENDMESSAGE_SQL(connString);

        #endregion

        #region 用户相关

        public class ListItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }


        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public object Logoin(string username, string password)
        {
            try
            {

                    string pass = user_sql.GetPassword_web(username);
                    if (pass!=password)
                    {
                        return new { state = -1, message = "账号或者密码错误" };
                    }
                    else
                    {
                        return new { state = 0, message = "登录成功" };
                    }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="u_sysid"></param>
        /// <returns></returns>
        [WebMethod]
        public object DeleteUser(string u_sysid)
        {
            try
            {
                if (user_sql.DeleteUser(u_sysid)==1)
                {
                    borrow_sql.Delete_User(u_sysid);
                    debit_his_sql.Delete_User(u_sysid);
                    //List<BORROW> borrow = borrow_sql.QueryByWhere_XP(string.Format(" AND U_SYSID='{0}'",u_sysid));
                    //borrow.ForEach(b => repay_his_sql.Delete_Borrow(b.B_SYSID));

                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="u_sysid"></param>
        /// <returns></returns>
        [WebMethod]
        public object ModPassword (string newpassword, string oldpassword)
        {
            try
            {
                List<USER> u = user_sql.Get_User("1");
                if (u.Count > 0) {
                    if (u[0].U_PASSWORD == oldpassword) {
                        if (user_sql.ModPassword(u[0], newpassword))
                        {

                            return new { state = 0, message = "修改成功" };
                        }
                        else
                        {
                            return new { state = -1, message = "修改失败" };
                        }
                    }
                    else
                        return new { state = -1, message = "旧密码错误" };

                }
                else
                    return new { state = -1, message = "未找到用户" };




            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }



        /// <summary>
        /// 查询用户资料
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public object GetUser(string name,string province,string city,string area, int pageid,string order)
        {
            try
            {
                string where = string.Empty;
                string orderby = "U_DATE";
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME LIKE '%{0}%'", name);
                }
                if (!string.IsNullOrEmpty(province.Trim()) && province != "null")
                {
                    where += string.Format(" AND U_PROVINCE = N'{0}'", province.Trim());
                }
                if (!string.IsNullOrEmpty(city.Trim()) && city != "null")
                {
                    where += string.Format(" AND U_CITY = N'{0}'", city.Trim());
                }
                if (!string.IsNullOrEmpty(area.Trim()) && area != "null")
                {
                    where += string.Format(" AND U_AREA = N'{0}'", area.Trim());
                }
                if (order == "amount")
                {
                    orderby = "U_BALANCE";
                }
                if (order == "message")
                {
                    orderby = "U_FREEMESSAGE";
                }


                List<USER> lu = user_sql.QueryByWhere_XP(where,pageid,10, orderby);


                if (lu.Count>0)
                {

                    return new { state = 0, data = lu };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalUser(string name,string province,string city,string area)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME LIKE '%{0}%'", name);
                }
                if (!string.IsNullOrEmpty(province.Trim())&& province!="null")
                {
                    where += string.Format(" AND U_PROVINCE = N'{0}'", province);
                }
                if (!string.IsNullOrEmpty(city.Trim()) && city !="null")
                {
                    where += string.Format(" AND U_CITY = N'{0}'", city);
                }
                if (!string.IsNullOrEmpty(area.Trim()) && area !="null")
                {
                    where += string.Format(" AND U_AREA = N'{0}'", area);
                }

                string total = user_sql.GetTotal(where);

                return new { state = 0, data = total };



            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        //[WebMethod]
        //public object GetUser_J(string name)
        //{
        //    try
        //    {
        //        string where = string.Empty;
        //        if (string.IsNullOrEmpty(name))
        //        {
        //            where += string.Format(" AND U_NAME LIKE '%{0}%'", name);
        //        }
        //        List<USER> lu = user_sql.SelectUser_C(where);


        //        if (lu.Count > 0)
        //        {

        //            return new { state = 0, data = new JavaScriptSerializer().Serialize(lu) };
        //        }
        //        else
        //        {
        //            return new { state = -1, message = "无数据" };
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        return new { state = -222, message = e.Message };

        //    }

        //}


        /// <summary>
        /// 根据u_sysid获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public object LoadUser(string u_sysid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(u_sysid))
                {
                    where += string.Format(" AND U_SYSID = '{0}'", u_sysid);
                }
                List<USER> lu = user_sql.QueryByWhere_XP(where);


                if (lu.Count > 0)
                {
                    if(lu[0].U_PROVINCE!="")
                        lu[0].U_PROVINCE = province_sql.GetValue_Province(lu[0].U_PROVINCE);
                    if (lu[0].U_CITY != "")
                        lu[0].U_CITY = province_sql.GetValue_City(lu[0].U_CITY);
                    if (lu[0].U_AREA != "")
                        lu[0].U_AREA = province_sql.GetValue_Area(lu[0].U_AREA);

                    return new { state = 0, data = lu[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        /// <summary>
        /// 加载机构
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public object LoadUName()
        {
            try
            {
                List<USER> lu = user_sql.QueryByWhere_XP("");


                if (lu.Count > 0)
                {
                    List<string> list = new List<string>();
                    lu.ForEach(u =>
                    {
                        list.Add(u.U_NAME + "-" + u.U_SYSID);
                    });



                    return new { state = 0, data = list };
                }
                else
                {
                    return new { state = -1, message = "无用户数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public object ModUser(string user)
        {
            try
            {
                USER u = new JavaScriptSerializer().Deserialize<USER>(user);

                if (user_sql.Update(u)==1)
                {

                    return new { state = 0, message = "修改成功" };
                }
                else
                {
                    return new { state = -1, message = "修改失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }




        [WebMethod]
        public object AddUser(string user)
        {
            try
            {
                USER u = new JavaScriptSerializer().Deserialize<USER>(user);
                string err=string.Empty;
                if (user_sql.CheckUser(u.U_ID, "id"))
                {
                    err += "该账号已注册！\r\n";
                }

                if (user_sql.CheckUser(u.U_MAIL, "mail"))
                {
                    err += "该邮件已注册！\r\n";
                }

                if (user_sql.CheckUser(u.U_TELEPHONE, "telephone"))
                {
                    err += "该手机已注册！\r\n";
                }
                if (string.IsNullOrEmpty(err)) {
                    if (user_sql.Insert(u) == 1)
                    {

                        return new { state = 0, message = "添加成功" };
                    }
                    else
                    {
                        return new { state = -1, message = "添加失败" };
                    }
                }
                else
                    return new { state = -2, message = err };



            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object GetProvice()
        {
            try
            {
                DataTable dtprovince = province_sql.GetProvince();
                List<ListItem> lprovince = new List<ListItem>();
                for (int i = 0; i < dtprovince.Rows.Count; i++) {
                    ListItem l = new ListItem();
                    l.Text = dtprovince.Rows[i]["PROVINCE"].ToString();
                    l.Value = dtprovince.Rows[i]["PROVINCEID"].ToString();
                    lprovince.Add(l);
                }



                return new { state = 0, data = lprovince};

            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object LoadLocal(string u_sysid)
        {
            try
            {

                string where="";
                if (!string.IsNullOrEmpty(u_sysid))
                {
                    where += string.Format(" AND U_SYSID = '{0}'", u_sysid);
                }
                USER u = user_sql.QueryByWhere_XP(where)[0];


                DataTable dtprovince = province_sql.GetProvince();
                List<ListItem> lprovince = new List<ListItem>();
                for (int i = 0; i < dtprovince.Rows.Count; i++)
                {
                    ListItem l = new ListItem();
                    l.Text = dtprovince.Rows[i]["PROVINCE"].ToString();
                    l.Value = dtprovince.Rows[i]["PROVINCEID"].ToString();
                    lprovince.Add(l);
                }

                DataTable dtcity = province_sql.GetCity(province_sql.GetValue_Province(u.U_PROVINCE));
                List<ListItem> lcity = new List<ListItem>();
                for (int i = 0; i < dtcity.Rows.Count; i++)
                {
                    ListItem l = new ListItem();
                    l.Text = dtcity.Rows[i]["CITY"].ToString();
                    l.Value = dtcity.Rows[i]["CITYID"].ToString();
                    lcity.Add(l);
                }

                DataTable dtarea = province_sql.GetArea(province_sql.GetValue_City(u.U_CITY));
                List<ListItem> larea = new List<ListItem>();
                for (int i = 0; i < dtarea.Rows.Count; i++)
                {
                    ListItem l = new ListItem();
                    l.Text = dtarea.Rows[i]["AREA"].ToString();
                    l.Value = dtarea.Rows[i]["AREAID"].ToString();
                    larea.Add(l);
                }

                return new { state = 0, province = lprovince ,city= lcity ,area= larea };

            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object ChangeProvince(string fid)
        {
            try
            {
                DataTable dt = province_sql.GetCity(fid);
                List<ListItem> list = new List<ListItem>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem l = new ListItem();
                    l.Text = dt.Rows[i]["CITY"].ToString();
                    l.Value = dt.Rows[i]["CITYID"].ToString();
                    list.Add(l);
                }


                return new { state = 0, data= list };

            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object ChangeCity(string fid)
        {
            try
            {
                DataTable dt = province_sql.GetArea(fid);
                List<ListItem> list = new List<ListItem>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem l = new ListItem();
                    l.Text = dt.Rows[i]["AREA"].ToString();
                    l.Value = dt.Rows[i]["AREAID"].ToString();
                    list.Add(l);
                }

                return new { state = 0, data = list };

            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        #endregion

        #region 消费相关


        /// <summary>
        /// 查询消费
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public object GetDebit(string name, int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME LIKE '%{0}%'", name);
                }
                List<DEBIT_HIS> ld = debit_his_sql.QueryByWhere_XP(where,pageid,10);

                if (ld.Count > 0)
                {

                    return new { state = 0, data = ld };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object TotalDebit(string name)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME LIKE '%{0}%'", name);
                }
                string total = debit_his_sql.GetTotal(where);



               return new { state = 0, data = total };



            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        /// <summary>
        /// 读取单个修改
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public object LoadDebit(string d_sysid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(d_sysid.Trim()))
                {
                    where += string.Format(" AND D_SYSID ='{0}'", d_sysid);
                }
                List<DEBIT_HIS> ld = debit_his_sql.QueryByWhere_XP(where);

                if (ld.Count > 0)
                {

                    return new { state = 0, data = ld[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [WebMethod]
        public object ModDebit(string debit)
        {
            try
            {
                DEBIT_HIS d = new JavaScriptSerializer().Deserialize<DEBIT_HIS>(debit);

                if ( debit_his_sql.Update(d) == 1)
                {

                    return new { state = 0, message = "修改成功" };
                }
                else
                {
                    return new { state = -1, message = "修改失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="u_sysid"></param>
        /// <returns></returns>
        [WebMethod]
        public object DeleteDebit(string d_sysid)
        {
            try
            {
                if (debit_his_sql.Delete(d_sysid) == 1)
                {

                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        #endregion

        #region 充值

        [WebMethod]
        public object GetPrepaid(string name,int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME like N'%{0}%'", name);
                }
                List<PREPAID_HIS> lp = prepaid_his_sql.QueryByWhere_XP(where,pageid,10);

                if (lp.Count > 0)
                {

                    return new { state = 0, data = lp };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalPrepaid(string name)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME like N'%{0}%'", name);
                }
                string total = prepaid_his_sql.GetTotal(where);

                    return new { state=0,data = total };


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }



        [WebMethod]
        public object DeletePrepaid(string p_sysid)
        {
            try
            {
                List<PREPAID_HIS> p = prepaid_his_sql.QueryByWhere_XP(string.Format(" AND P_SYSID='{0}'", p_sysid));
                if (prepaid_his_sql.Delete(p_sysid) == 1)
                {
                    user_sql.Update_pripard_d(p[0]);
                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object LoadPrepaid(string p_sysid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(p_sysid.Trim()))
                {
                    where += string.Format(" AND P_SYSID ='{0}'", p_sysid);
                }
                List<PREPAID_HIS> lp = prepaid_his_sql.QueryByWhere_XP(where);

                if (lp.Count > 0)
                {

                    return new { state = 0, data = lp[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [WebMethod]
        public object ModPrepaid(string prepaid)
        {
            try
            {
                PREPAID_HIS p = new JavaScriptSerializer().Deserialize<PREPAID_HIS>(prepaid);
                PREPAID_HIS po = prepaid_his_sql.QueryByWhere_XP(string.Format(" AND P_SYSID='{0}' ", p.P_SYSID))[0];

                if (prepaid_his_sql.Update(p) == 1)
                {
                    user_sql.Update_pripard_u(p, po);
                    return new { state = 0, message = "修改成功" };
                }
                else
                {
                    return new { state = -1, message = "修改失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object AddPrepaid(string prepaid)
        {
            try
            {
                PREPAID_HIS p = new JavaScriptSerializer().Deserialize<PREPAID_HIS>(prepaid);
                p.P_DATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string err = string.Empty;
                    if (prepaid_his_sql.Insert(p) == 1)
                    {
                        if(user_sql.Pripard(p)==1)
                             return new { state = 0, message = "充值成功" };
                        else
                             return new { state = -1, message = "充值记录保存成功，充值失败" };
                   }
                    else
                    {
                        return new { state = -1, message = "充值记录保存失败，充值失败" };
                    }



            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        #endregion

        #region 借款相关

        [WebMethod]
        public object GetBorrow(string name,string id,int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME like N'%{0}%'", name);
                }
                if (!string.IsNullOrEmpty(id.Trim())) {
                    where += string.Format(" AND C_ID like N'%{0}%'", id);
                }

                List<BORROW> b = borrow_sql.QueryByWhere_XP(where,pageid,10);

                if (b.Count > 0)
                {
                    b.ForEach(tb => tb.C_PIC = null);
                    return new { state = 0, data = b };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalBorrow(string name, string id)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME like N'%{0}%'", name);
                }
                if (!string.IsNullOrEmpty(id.Trim()))
                {
                    where += string.Format(" AND C_ID like N'%{0}%'", id);
                }

                string total = borrow_sql.GetTotal(where);


                return new { state = 0, data = total };
               


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object DeleteBorrow(string b_sysid)
        {
            try
            {
                if (borrow_sql.Delete(b_sysid) == 1)
                {

                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object LoadBorrow(string b_sysid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(b_sysid.Trim()))
                {
                    where += string.Format(" AND B_SYSID ='{0}'", b_sysid);
                }
                List<BORROW> ld = borrow_sql.QueryByWhere_XP(where,false);

                if (ld.Count > 0)
                {
                    ld[0].C_PIC = null;
                    return new { state = 0, data = ld[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object ModBorrow(string borrow)
        {
            try
            {
                BORROW b = new JavaScriptSerializer().Deserialize<BORROW>(borrow);

                if (borrow_sql.Update(b) == 1)
                {

                    return new { state = 0, message = "修改成功" };
                }
                else
                {
                    return new { state = -1, message = "修改失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        #endregion

        #region 还款相关

        [WebMethod]
        public object GetRepay(string name, string id, int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME like N'%{0}%'", name);
                }
                if (!string.IsNullOrEmpty(id.Trim()))
                {
                    where += string.Format(" AND C_ID like N'%{0}%'", id);
                }

                List<REPAY_HIS> r = repay_his_sql.QueryByWhere_XP(where,pageid,10);

                if (r.Count > 0)
                {
                    r.ForEach(tr => tr.BORROW.C_PIC = null);
                    return new { state = 0, data = r };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalRepay(string name, string id)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME like N'%{0}%'", name);
                }
                if (!string.IsNullOrEmpty(id.Trim()))
                {
                    where += string.Format(" AND C_ID like N'%{0}%'", id);
                }

                string total = repay_his_sql.GetTotal(where);


                return new { state = 0, data = total };


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object DeleteRepay(string r_sysid)
        {
            try
            {
               
                if (repay_his_sql.Delete(r_sysid) == 1)
                {
                    borrow_sql.UpdateByRepayDelete(repay_his_sql.QueryByWhere_Borrow(string.Format(" R_SYSID='{0}'", r_sysid)));
                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object LoadRepay(string r_sysid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(r_sysid.Trim()))
                {
                    where += string.Format(" AND R_SYSID ='{0}'", r_sysid);
                }
                List<REPAY_HIS> lr = repay_his_sql.QueryByWhere_XP(where);

                if (lr.Count > 0)
                {
                    lr[0].BORROW.C_PIC = null;
                    return new { state = 0, data = lr[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object ModRepay(string repay)
        {
            try
            {
                REPAY_HIS r = new JavaScriptSerializer().Deserialize<REPAY_HIS>(repay);
                REPAY_HIS ro = repay_his_sql.QueryByWhere_XP(string.Format(" AND R_SYSID='{0}'", r.R_SYSID))[0];
                if (repay_his_sql.Update(r) == 1)
                {
                    borrow_sql.UpdateByRepayUpdate(r,Convert.ToDouble(r.R_AMOUNT) - Convert.ToDouble(ro.R_AMOUNT));
                    return new { state = 0, message = "修改成功" };
                }
                else
                {
                    return new { state = -1, message = "修改失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        #endregion

        #region 发布编辑

        [WebMethod]
        public object GetPublish(string title, int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(title.Trim()))
                {
                    where += string.Format(" AND E_TITLE like N'%{0}%'", title);
                }

                List<EDIT> e = edit_sql.QueryByWhere_XP(where,pageid,10);

                if (e.Count > 0)
                {

                    return new { state = 0, data = e };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalPublish(string title)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(title.Trim()))
                {
                    where += string.Format(" AND E_TITLE like N'%{0}%'", title);
                }

                string total = edit_sql.GetTotal(where);


                return new { state = 0, data = total };



            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object DeletePublish(string e_sysid)
        {
            try
            {
                if (edit_sql.Delete(e_sysid) == 1)
                {

                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object LoadPublish(string e_sysid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(e_sysid.Trim()))
                {
                    where += string.Format(" AND E_SYSID ='{0}'", e_sysid);
                }
                List<EDIT> le = edit_sql.QueryByWhere_XP(where);

                if (le.Count > 0)
                {

                    return new { state = 0, data = le[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object ModPublish(string publish,string content,string titlepic)
        {
            try
            {
                EDIT e = new JavaScriptSerializer().Deserialize<EDIT>(publish);
                e.E_CONTENT = content;
                e.E_TITLEPIC = titlepic;
                e.E_DATETIME = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                if (edit_sql.Update(e) == 1)
                {

                    return new { state = 0, message = "修改成功" };
                }
                else
                {
                    return new { state = -1, message = "修改失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object AddPublish(string publish,string content, string titlepic)
        {
            try
            {
                EDIT e = new JavaScriptSerializer().Deserialize<EDIT>(publish);
                e.E_CONTENT = content;
                e.E_TITLEPIC = titlepic;
                e.E_DATETIME = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string err = string.Empty;
                if (edit_sql.Insert(e) == 1)
                {
                        return new { state = 0, message = "发布成功" };
                }
                else
                {
                    return new { state = -1, message = "发布失败" };
                }



            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        #endregion

        #region 主页相关


        [WebMethod]
        public object AddContent(string content)
        {
            try
            {
                CONTENT c = new JavaScriptSerializer().Deserialize<CONTENT>(content);
                string err = string.Empty;
                if (content_sql.Insert(c) == 1)
                {
                    return new { state = 0, message = "保存成功" };
                }
                else
                {
                    return new { state = -1, message = "保存失败" };
                }

            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object GetContent(string fdate,string tdate, int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(fdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(C_DATE,1,10)>='{0}'", fdate);
                }
                if (!string.IsNullOrEmpty(tdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(C_DATE,1,10)<='{0}'", tdate);
                }

                List<CONTENT> c = content_sql.QueryByWhere_XP(where, pageid,10);

                if (c.Count > 0)
                {

                    return new { state = 0, data = c };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalContent(string fdate, string tdate)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(fdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(C_DATE,1,10)>='{0}'", fdate);
                }
                if (!string.IsNullOrEmpty(tdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(C_DATE,1,10)<='{0}'", tdate);
                }

                string total = content_sql.GetTotal(where);


               return new { state = 0, data = total };


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object DeleteContent(string date)
        {
            try
            {
                if (content_sql.Delete(date) == 1)
                {

                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object LoadContent(string date)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(date.Trim()))
                {
                    where += string.Format(" AND C_DATE ='{0}'", date);
                }
                List<CONTENT> ld = content_sql.QueryByWhere_XP(where);

                if (ld.Count > 0)
                {

                    return new { state = 0, data = ld[0] };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        #endregion

        #region 短信发送

        [WebMethod]
        public object GetMessage(string name,string fdate, string tdate, int pageid)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME LIKE N'{0}'", name);
                }
                if (!string.IsNullOrEmpty(fdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(S_SENDDATE,1,10)>='{0}'", fdate);
                }
                if (!string.IsNullOrEmpty(tdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(S_SENDDATE,1,10)<='{0}'", tdate);
                }

                List<MESSAGE> m = message_sql.QueryByWhere_XP(where, pageid, 10);

                if (m.Count > 0)
                {

                    return new { state = 0, data = m };
                }
                else
                {
                    return new { state = -1, message = "无数据" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }

        [WebMethod]
        public object TotalMessage(string name,string fdate, string tdate)
        {
            try
            {
                string where = string.Empty;
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    where += string.Format(" AND U_NAME LIKE N'{0}'", name);
                }
                if (!string.IsNullOrEmpty(fdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(S_SENDDATE,1,10)>='{0}'", fdate);
                }
                if (!string.IsNullOrEmpty(tdate.Trim()))
                {
                    where += string.Format(" AND SUBSTRING(S_SENDDATE,1,10)<='{0}'", tdate);
                }

                string total = message_sql.GetTotal(where);


                return new { state = 0, data = total };


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }


        [WebMethod]
        public object DeleteMessage(string s_sysid)
        {
            try
            {
                if (message_sql.Delete(s_sysid) == 1)
                {

                    return new { state = 0, message = "删除成功" };
                }
                else
                {
                    return new { state = -1, message = "删除失败" };
                }


            }
            catch (Exception e)
            {
                return new { state = -222, message = e.Message };

            }

        }
        #endregion

    }
}
