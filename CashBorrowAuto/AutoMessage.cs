using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DbHelp.CS;
using DbHelp.SQlHelp;
using System.Configuration;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using Newtonsoft.Json;
using ConsoleApplication1;
using HL.Framework.Utility.Network;

namespace CashBorrowAuto
{
    public partial class AutoMessage : Office2007Form
    {
        public AutoMessage()
        {
            InitializeComponent();
        }


        #region 初始化
        public static T_SENDMESSAGE_SQL sendmessage_sql = new T_SENDMESSAGE_SQL(ConfigurationManager.ConnectionStrings["connString"].ConnectionString.ToString());

        public static T_BORROW_SQL borrow_sql = new T_BORROW_SQL(ConfigurationManager.ConnectionStrings["connString"].ConnectionString.ToString());

        public static T_USER_SQL user_sql = new T_USER_SQL(ConfigurationManager.ConnectionStrings["connString"].ConnectionString.ToString());

        public static string message = ConfigurationManager.AppSettings.Get("messgae");

        private static string Key_64 = "shdsfjdk";
        private static string Iv_64 = "zjxdtzgl";

        private string path = "C:\\XINDAILOG";


        #endregion

        #region 数据库链接加密解密

        /// <summary>
        /// MD5 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key_64"></param>
        /// <param name="Iv_64"></param>
        /// <returns></returns>
        public static string Decode(string data, string Key_64, string Iv_64)

        {

            string KEY_64 = Key_64;// "VavicApp";密钥

            string IV_64 = Iv_64;// "VavicApp"; 向量

            try

            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc;

                byEnc = Convert.FromBase64String(data); //把需要解密的字符串转为8位无符号数组

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(byEnc);

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);

                StreamReader sr = new StreamReader(cst);

                return sr.ReadToEnd();

            }

            catch (Exception x)

            {

                return x.Message;

            }

        }


        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key_64"></param>
        /// <param name="Iv_64"></param>
        /// <returns></returns>
        public static string Encode(string data, string Key_64, string Iv_64)

        {

            string KEY_64 = Key_64;// "VavicApp";

            string IV_64 = Iv_64;// "VavicApp";

            try

            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                int i = cryptoProvider.KeySize;

                MemoryStream ms = new MemoryStream();

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);

                sw.Write(data);

                sw.Flush();

                cst.FlushFinalBlock();

                sw.Flush();

                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

            }

            catch (Exception x)

            {

                return x.Message;

            }

        }

        #endregion

        #region MD5加密
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string md5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }
        #endregion

        #region http请求
        public static string RequestData(string POSTURL)
        {
            ////发送请求的数据
            //WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            //myHttpWebRequest.Method = "GET";
            //UTF8Encoding encoding = new UTF8Encoding();
            //byte[] byte1 = encoding.GetBytes(PostData);
            //myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            //myHttpWebRequest.ContentLength = byte1.Length;
            //Stream newStream = myHttpWebRequest.GetRequestStream();
            //newStream.Write(byte1, 0, byte1.Length);
            //newStream.Close();

            ////发送成功后接收返回的XML信息
            //HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            //string lcHtml = string.Empty;
            //Encoding enc = Encoding.GetEncoding("UTF-8");
            //Stream stream = response.GetResponseStream();
            //StreamReader streamReader = new StreamReader(stream, enc);
            //lcHtml = streamReader.ReadToEnd();
            //return lcHtml;

            System.Net.HttpWebRequest request;
            // 创建一个HTTP请求
            request = (System.Net.HttpWebRequest)WebRequest.Create(POSTURL);
            request.Method="get";
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string read = "";
            string responseText = "";
            while ((read = myreader.ReadLine()) != null)
            {
                responseText += read + "|";
            }
            //string responseText = myreader.ReadToEnd();
            myreader.Close();
            //MessageBox.Show(responseText);
            return responseText.Replace('\n', ' ').Replace('\t', ' ').Replace('\"', ' ').Replace(',',' ');

        }
        #endregion





        private void AutoMessage_Load(object sender, EventArgs e)
        {


        }

        private void AutoMessage_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.notifyIcon1.Visible = true;
            }
        }

        private void 启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            timer1.Start();
            notifyIcon1.BalloonTipTitle = "程序启动！";
            notifyIcon1.BalloonTipText = "程序启动！";
            notifyIcon1.Text = "运行中！";
            MessageBox.Show("启动", "启动提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            notifyIcon1.BalloonTipTitle = "程序停止！";
            notifyIcon1.BalloonTipText = "程序停止！";
            notifyIcon1.Text = "停止！";
            MessageBox.Show("停止", "启动提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出？", "退出提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                this.Close();
        }



        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="log"></param>
        public void WriteFile(MESSAGE m)
        {
            string filename = DateTime.Now.ToString("yyyy-MM-dd") + "短信发送记录.txt";
            if (Directory.Exists(path))
            {
                if (!File.Exists(path + "\\" + filename))
                {
                    File.Create(path + "\\" + filename).Close();

                }
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();
            }

            StringBuilder log = new StringBuilder();
            if (m.S_NUM =="3")
                log.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"   发送失败3次，不再发送！" + "\r\n");
            else
                log.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"      第"+m.S_NUM+"次发送   \r\n");
            log.Append("                  手机号码: " + m.S_TELEPHONE + "\r\n");
            log.Append("                  借款单号: " + m.S_COMMIT + "\r\n");
            log.Append("                  发送状态: " + m.S_FLAG+ "\r\n");
            StreamWriter sw = File.AppendText(path + "\\" + filename);
            sw.WriteLine(log.ToString());
            sw.Close();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!backgroundWorker1.IsBusy) {
                backgroundWorker1.RunWorkerAsync();
            }






        }


        #region 短信发送
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendMessage(string phone,string smessage) {
            Random _random = new Random();
            string _tradeno = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(100, 1000);
            string _username = ConfigurationManager.AppSettings.Get("username");
            string _password = ConfigurationManager.AppSettings.Get("password");
            string _key = ConfigurationManager.AppSettings.Get("key");
            string _url = ConfigurationManager.AppSettings.Get("PostUrl");
            string _content = smessage;
            string _phone = phone;
            string json = JsonConvert.SerializeObject(new
            {
                userPassword = _password,
                tradeNo = _tradeno,
                phones = _phone,
                etnumber = "",
                userName = _username,
                content = _content,
            });
            string _sign = AES.Encrypt(json, _key, _key);
            _password = MD5Helper.Encrypt(_password);
            string parm_json = JsonConvert.SerializeObject(new
            {
                tradeNo = _tradeno,
                userName = _username,
                userPassword = _password,
                phones = _phone,
                content = _content,
                etnumber = "",
                sign = _sign
            });
            WebAction _web = new WebAction();
            string returnStr = HttpHelper.Post(_url, parm_json);
            return GetResult(returnStr.Split(',')[1].Split(':')[1].Replace('"', ' ').Trim());

        }
        #endregion


        private void send_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(edtPhone.Text.Trim()))
            {
                try
                {
                    MESSAGE m = new MESSAGE();
                    m.S_SENDDATE = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    m.S_TELEPHONE = edtPhone.Text.Trim();
                    m.S_MESSAGE = message;
                    m.U_SYSID = "1";
                    m.S_COMMIT = "短信发送测试";
                    m.S_FLAG = SendMessage(edtPhone.Text.Trim(), message);
                    sendmessage_sql.Insert(m);
                    WriteFile(m);
                    MessageBox.Show("短信发送返回值:" + m.S_FLAG);
                }
                catch (Exception e1)
                {
                    MessageBox.Show("错误:" + e1.Message);
                }

            }












            //    if ( !string.IsNullOrEmpty(edtPhone.Text.Trim()))
            //    {
            //        string PostUrl = ConfigurationManager.AppSettings.Get("PostUrl");
            //        string username = ConfigurationManager.AppSettings.Get("username");
            //        string password = ConfigurationManager.AppSettings.Get("password");
            //        string timestamps = (DateTime.Now.Ticks / 1000).ToString();
            //        StringBuilder PostData = new StringBuilder();
            //        PostData.Append(string.Format("account={0}", username));
            //        PostData.Append(string.Format("&password={0}", md5(password + edtPhone.Text.Trim() + timestamps)));
            //        PostData.Append(string.Format("&mobile={0}", edtPhone.Text.Trim()));
            //        PostData.Append(string.Format("&content={0}", message));
            //        PostData.Append(string.Format("&timestamps={0}", timestamps));
            //        MESSAGE m = new MESSAGE();
            //        m.S_SENDDATE = DateTime.Now.ToString("yyy-MM-dd hh:mm:ss");
            //        m.S_TELEPHONE = edtPhone.Text.Trim();
            //        m.S_MESSAGE = message;
            //        m.U_SYSID = "1";
            //        m.S_COMMIT = "短信发送测试";
            //    try
            //    {
            //        m.S_FLAG = GetResult(RequestData(PostUrl + "?" + PostData.ToString()).Split('|')[3].Split(':')[1].Trim());

            //    }
            //    catch (Exception e1)
            //    {
            //        MessageBox.Show(e1.Message);
            //        m.S_COMMIT = e1.Message;
            //        m.S_FLAG = "0";
            //    }
            //    if (m.S_COMMIT == "0")

            //    sendmessage_sql.Insert(m);
            //    MessageBox.Show("短信发送返回值:"+ m.S_FLAG);


            //}


        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            base.ShowInTaskbar = true;
            this.Activate();
            this.Show();
            base.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            edtrshource.Text = Encode(edtsource.Text.Trim(), Key_64, Iv_64);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edtrshource.Text = Decode(edtsource.Text.Trim(), Key_64, Iv_64);
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            //取先息后款数据
            //List<BORROW> lb = borrow_sql.QueryByWhere_XP(string.Format(" and (((B_REMINDDATE='{0}' OR B_REPAYDATE='{0}') AND SUBSTRING(B_DATE,1,7)<>'{2}') OR B_DATE='{1}')  AND B_AMOUNT>B_REPAYMENT AND B_REPAYTYPE=N'先息后款'  AND B_SYSID NOT IN (SELECT S_COMMIT FROM T_SENDMESSAGE WHERE S_ISDEL='1' AND (S_FLAG ='交易请求成功' OR S_NUM>=3) AND  SUBSTRING(S_SENDDATE,1,10)='{1}') ", DateTime.Now.Day, DateTime.Now.ToString("yyyy-MM-dd"),DateTime.Now.ToString("yyyy-MM-dd").Substring(0,7)), false);

            //List<BORROW> lb_1 = borrow_sql.QueryByWhere_XP(string.Format(" and ((B_REMINDDATE='{0}' OR B_REPAYDATE='{0}') AND SUBSTRING(B_DATE,1,7)<>'{2}')  AND B_AMOUNT>B_REPAYMENT AND B_REPAYTYPE=N'等额本息'  AND B_SYSID NOT IN (SELECT S_COMMIT FROM T_SENDMESSAGE WHERE S_ISDEL='1' AND (S_FLAG ='交易请求成功' OR S_NUM>=3) AND  SUBSTRING(S_SENDDATE,1,10)='{1}') ", DateTime.Now.Day, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 7)), false);
            List<BORROW> lb = borrow_sql.QueryByWhere_XP(string.Format(" and (((B_REMINDDATE='{0}' OR B_REPAYDATE='{0}') AND SUBSTRING(B_DATE,1,7)<>'{2}') OR B_DATE='{1}')  AND {3}-CONVERT(INT,REPLACE(SUBSTRING(B_DATE,1,7),'-',''))<=CONVERT(INT,B_TERM) AND B_REPAYTYPE=N'先息后款'  AND B_SYSID NOT IN (SELECT S_COMMIT FROM T_SENDMESSAGE WHERE S_ISDEL='1' AND (S_FLAG ='交易请求成功' OR S_NUM>=3) AND  SUBSTRING(S_SENDDATE,1,10)='{1}') ", DateTime.Now.Day, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 7), Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6))), false);

            List<BORROW> lb_1 = borrow_sql.QueryByWhere_XP(string.Format(" and ((B_REMINDDATE='{0}' OR B_REPAYDATE='{0}') AND SUBSTRING(B_DATE,1,7)<>'{2}')  AND {3}-CONVERT(INT,REPLACE(SUBSTRING(B_DATE,1,7),'-',''))<=CONVERT(INT,B_TERM)  AND B_REPAYTYPE=N'等额本息'  AND B_SYSID NOT IN (SELECT S_COMMIT FROM T_SENDMESSAGE WHERE S_ISDEL='1' AND (S_FLAG ='交易请求成功' OR S_NUM>=3) AND  SUBSTRING(S_SENDDATE,1,10)='{1}') ", DateTime.Now.Day, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 7), Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6))), false);

            lb_1.ForEach(b =>
            {
                lb.Add(b);
            });

            for (int i = 0; i < lb.Count; i++)
                {
                MESSAGE m = new MESSAGE();
                List<MESSAGE> lm = sendmessage_sql.QueryByWhere_XP(string.Format(" AND S_COMMIT ='{0}' AND SUBSTRING(S_SENDDATE,1,10)='{1}' ", lb[i].B_SYSID, DateTime.Now.ToString("yyyy-MM-dd")));
                if (lm.Count > 0)
                {
                    m = lm[0];
                    m.S_NUM = (Convert.ToInt16(m.S_NUM) +1).ToString();
                    m.S_SENDDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    if (lb[i].B_REPAYTYPE == "先息后款")
                    {
                        if ((Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6)) - Convert.ToInt64(lb[i].B_DATE.Replace("-", "").Substring(0, 6))).ToString() == lb[i].B_TERM)
                            m.S_MESSAGE = string.Format(message, lb[i].USER.U_SHORT, lb[i].C_NAME, lb[i].B_AMOUNT, (Convert.ToDecimal(lb[i].B_AMOUNT)).ToString("#0.00"), lb[i].B_REPAYDATE);
                        else
                            m.S_MESSAGE = string.Format(message, lb[i].USER.U_SHORT, lb[i].C_NAME, lb[i].B_AMOUNT, (Convert.ToDecimal(lb[i].B_AMOUNT) * Convert.ToDecimal(lb[i].B_INTEREST) / 12 / 100).ToString("#0.00"), lb[i].B_REPAYDATE);
                    }
                    else {
                        m.S_MESSAGE = string.Format(message, lb[i].USER.U_SHORT, lb[i].C_NAME, lb[i].B_AMOUNT, (Convert.ToDecimal(lb[i].B_AMOUNT)/ Convert.ToDecimal(lb[i].B_TERM) + Convert.ToDecimal(lb[i].B_AMOUNT) * Convert.ToDecimal(lb[i].B_INTEREST) / 12 / 100).ToString("#0.00"), lb[i].B_REPAYDATE);
                    }
                    m.S_TELEPHONE = lb[i].C_CONTACT;
                    m.S_SENDDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    m.U_SYSID = lb[i].U_SYSID;
                    m.S_COMMIT = lb[i].B_SYSID;
                    m.S_NUM = "1";
                }
                   

                    //m.S_MESSAGE = string.Format(message, lb[i].C_NAME, lb[i].B_AMOUNT, (Convert.ToDecimal(lb[i].B_AMOUNT) *(1+ Convert.ToDecimal(lb[i].B_INTEREST))/Convert.ToDecimal(lb[i].B_TERM)/100).ToString("#0.00"), lb[i].B_REPAYDATE);
                    //m.S_TELEPHONE = lb[i].C_CONTACT;
                    //m.S_SENDDATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //m.U_SYSID = lb[i].U_SYSID;
                    //m.S_COMMIT = lb[i].B_SYSID;
                    //string PostUrl = ConfigurationManager.AppSettings.Get("PostUrl");
                    //string username = ConfigurationManager.AppSettings.Get("username");
                    //string password = ConfigurationManager.AppSettings.Get("password");
                    //string timestamps = (DateTime.Now.Ticks / 1000).ToString();
                    //StringBuilder PostData = new StringBuilder();
                    //PostData.Append(string.Format("account={0}", username));
                    //PostData.Append(string.Format("&password={0}", md5(password + m.S_TELEPHONE + timestamps)));
                    //PostData.Append(string.Format("&mobile={0}", m.S_TELEPHONE));
                    //PostData.Append(string.Format("&content={0}", m.S_MESSAGE));
                    //PostData.Append(string.Format("&timestamps={0}", timestamps));
                if (user_sql.GetMessage(m) > 0) {
                    try
                    {
                        //m.S_FLAG = GetResult(RequestData(PostUrl + "?" + PostData.ToString()).Split('|')[3].Split(':')[1].Trim());
                        m.S_FLAG= SendMessage(m.S_TELEPHONE, m.S_MESSAGE);
                    }
                    catch (Exception e1)
                    {
                        m.S_COMMIT = e1.Message;
                        m.S_FLAG = "e";
                    }
                    if (m.S_NUM == "1")
                        sendmessage_sql.Insert(m);
                    else
                        sendmessage_sql.Update(m);

                    if (m.S_FLAG == "交易请求成功")
                        user_sql.Debit_Message(m);

                    WriteFile(m);
                    Thread.Sleep(5000);

                }


            }


        }


        /// <summary>
        /// 手动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBid.Text.Trim()))
            {
                List<BORROW> lb = borrow_sql.QueryByWhere_XP(string.Format(" AND B_SYSID='{0}' ", txtBid.Text.Trim()), false);
                if (lb.Count > 0)
                {
                    MESSAGE m = new MESSAGE();
                    if ((Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6)) - Convert.ToInt64(lb[0].B_DATE.Replace("-", "").Substring(0, 6))).ToString() == lb[0].B_TERM)
                        m.S_MESSAGE = string.Format(message, lb[0].USER.U_SHORT, lb[0].C_NAME, lb[0].B_AMOUNT, (Convert.ToDecimal(lb[0].B_AMOUNT)).ToString("#0.00"), lb[0].B_REPAYDATE);
                    else
                        m.S_MESSAGE = string.Format(message, lb[0].USER.U_SHORT, lb[0].C_NAME, lb[0].B_AMOUNT, (Convert.ToDecimal(lb[0].B_AMOUNT) * Convert.ToDecimal(lb[0].B_INTEREST) / 12 / 100).ToString("#0.00"), lb[0].B_REPAYDATE);
                    m.S_TELEPHONE = lb[0].C_CONTACT;
                    m.S_SENDDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    m.U_SYSID = lb[0].U_SYSID;
                    m.S_COMMIT = lb[0].B_SYSID;
                    //string PostUrl = ConfigurationManager.AppSettings.Get("PostUrl");
                    //string username = ConfigurationManager.AppSettings.Get("username");
                    //string password = ConfigurationManager.AppSettings.Get("password");
                    //string timestamps = (DateTime.Now.Ticks / 1000).ToString();
                    //StringBuilder PostData = new StringBuilder();
                    //PostData.Append(string.Format("account={0}", username));
                    //PostData.Append(string.Format("&password={0}", md5(password + m.S_TELEPHONE + timestamps)));
                    //PostData.Append(string.Format("&mobile={0}", m.S_TELEPHONE));
                    //PostData.Append(string.Format("&content={0}", m.S_MESSAGE));
                    //PostData.Append(string.Format("&timestamps={0}", timestamps));
                    if (user_sql.GetMessage(m) > 0)
                    {
                        try
                        {
                            //m.S_FLAG = GetResult(RequestData(PostUrl + "?" + PostData.ToString()).Split('|')[3].Split(':')[1].Trim());
                            m.S_FLAG = SendMessage(m.S_TELEPHONE, m.S_MESSAGE);
                        }
                        catch (Exception e1)
                        {
                            m.S_COMMIT = e1.Message;
                            m.S_FLAG = "e";
                        }
                        sendmessage_sql.Insert(m);
                        if (m.S_FLAG == "交易请求成功")
                            user_sql.Debit_Message(m);
                        WriteFile(m);
                    }

                    MessageBox.Show("短信发送返回值:" + m.S_FLAG);

                }
                else {
                    MessageBox.Show("无此借款单号！");
                }

            }
        }

        public string GetResult(string type)
        {


            switch (type)
            {
                case "P00000":
                    return "交易请求成功";
                case "P00001":
                    return "非法用户";
                case "P00002":
                    return "参数错误";
                case "P00003":
                    return "鉴权失败";
                case "P00004":
                    return "系统异常";
                case "P00005":
                    return "账户余额不足";
                case "P00009":
                    return "其他未知错误";
                default:
                    return type;
            }

            //switch (type) {
            //    case "0":
            //        return "成功";
            //    case "1":
            //        return "用户鉴权错误";
            //    case "2":
            //        return "IP鉴权错误";
            //    case "3":
            //        return "手机号码在黑名单";
            //    case "4":
            //        return "手机号码格式错误";
            //    case "5":
            //        return "短信内容有误";
            //    case "7":
            //        return "手机号数量超限";
            //    case "8":
            //        return "账户已停用";
            //    case "9":
            //        return "未知错误";
            //    case "10":
            //        return "时间戳已过期";
            //    case "11":
            //        return "发送频率过快（默认间隔30秒）";
            //    case "12":
            //        return "当天发送次数超限默认10次";
            //    case "13":
            //        return "内容包含敏感字";
            //    case "99":
            //        return "账户余额不足";
            //    default:
            //        return type;
            //} 
        }
    }
}
