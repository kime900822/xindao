﻿using System;
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

        public static string message = ConfigurationManager.AppSettings.Get("messgae");

        private static string Key_64 = "shdsfjdk";
        private static string Iv_64 = "zjxdtzgl";


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

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!backgroundWorker1.IsBusy) {
                backgroundWorker1.RunWorkerAsync();
            }

                //if (DateTime.Now.Hour > 7 && DateTime.Now.Hour < 10)
                //{
                //    //List<MESSAGE> lm = sendmessage_sql.QueryByWhere_XP(string.Format("  AND SUBSTR(S_SENDDATE,1,8)='{0}' AND S_FALG='1'", DateTime.Now.ToString("yyyy-MM-dd")));
                //    //List<string> l = new List<string>();
                //    //lm.ForEach(m => l.Add(m.S_TELEPHONE));
                //    List<BORROW> lb = borrow_sql.QueryByWhere_XP(string.Format(" and B_REMINDDATE='{0}'  AND B_CONTACT NOT IN (SELECT S_TELEPHONE FROM T_SENDMESSAGE WHERE S_ISDEL='1' AND S_FLAG='1' AND  SUBSTR(S_SENDDATE,1,8)='{1}')", DateTime.Now.Day, DateTime.Now.ToString("yyyy-MM-dd")));

                ////var queryResult = from b in lb
                ////                  where !l.Contains(b.C_CONTACT)
                ////                  select b;
                ////List<BORROW> rb = queryResult.ToList();
                //    for (int i = 0; i < rb.Count; i++)
                //    {
                //        MESSAGE m = new MESSAGE();
                //        m.S_MESSAGE = string.Format(message, rb[i].C_NAME, rb[i].B_AMOUNT, rb[i].B_INTEREST, rb[i].B_REPAYDATE);
                //        m.S_TELEPHONE = rb[i].C_CONTACT;
                //        m.S_SENDDATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                //        m.U_SYSID = rb[i].U_SYSID;
                //        string PostUrl = ConfigurationManager.AppSettings.Get("PostUrl");
                //        string username = ConfigurationManager.AppSettings.Get("username");
                //        string password = ConfigurationManager.AppSettings.Get("password");
                //        string timestamps = (DateTime.Now.Ticks / 1000).ToString();
                //        StringBuilder PostData = new StringBuilder();
                //        PostData.Append(string.Format("account={0}", username));
                //        PostData.Append(string.Format("&password={0}", md5(password + m.S_TELEPHONE + timestamps)));
                //        PostData.Append(string.Format("&mobile={0}", m.S_TELEPHONE));
                //        PostData.Append(string.Format("&content={0}", m.S_MESSAGE));
                //        PostData.Append(string.Format("&timestamps={0}", timestamps));
                        
                //    try
                //    {
                //        m.S_COMMIT = RequestData(PostUrl + "?" + PostData.ToString());
                //    }
                //    catch (Exception e1)
                //    {
                //        m.S_COMMIT = e1.Message;
                //        m.S_FLAG = "0";
                //    }
                //    sendmessage_sql.Insert(m);


                //    }


                //}





        }

        private void send_Click(object sender, EventArgs e)
        {

                if ( !string.IsNullOrEmpty(edtPhone.Text.Trim()))
                {
                    string PostUrl = ConfigurationManager.AppSettings.Get("PostUrl");
                    string username = ConfigurationManager.AppSettings.Get("username");
                    string password = ConfigurationManager.AppSettings.Get("password");
                    string timestamps = (DateTime.Now.Ticks / 1000).ToString();
                    StringBuilder PostData = new StringBuilder();
                    PostData.Append(string.Format("account={0}", username));
                    PostData.Append(string.Format("&password={0}", md5(password + edtPhone.Text.Trim() + timestamps)));
                    PostData.Append(string.Format("&mobile={0}", edtPhone.Text.Trim()));
                    PostData.Append(string.Format("&content={0}", message));
                    PostData.Append(string.Format("&timestamps={0}", timestamps));
                    MESSAGE m = new MESSAGE();
                    m.S_SENDDATE = DateTime.Now.ToString("yyy-MM-dd hh:mm:ss");
                    m.S_TELEPHONE = edtPhone.Text.Trim();
                    m.S_MESSAGE = message;
                    m.U_SYSID = "1";
                    m.S_COMMIT = "短信发送测试";
                try
                {
                    m.S_FLAG = RequestData(PostUrl + "?" + PostData.ToString()).Split('|')[3].Split(':')[1].Trim();

                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                    m.S_COMMIT = e1.Message;
                    m.S_FLAG = "0";
                }
                if (m.S_COMMIT == "0")
 
                sendmessage_sql.Insert(m);
                 MessageBox.Show("短信发送返回值:"+ m.S_FLAG);


            }


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

            List<BORROW> lb = borrow_sql.QueryByWhere_XP(string.Format(" and B_REMINDDATE='{0}' AND B_AMOUNT<>B_REPAYMENT   AND B_SYSID NOT IN (SELECT S_COMMIT FROM T_SENDMESSAGE WHERE S_ISDEL='1' AND S_FLAG='0' AND  SUBSTRING(S_SENDDATE,1,10)='{1}')", DateTime.Now.Day, DateTime.Now.ToString("yyyy-MM-dd")),false);

                for (int i = 0; i < lb.Count; i++)
                {
                    MESSAGE m = new MESSAGE();
                    m.S_MESSAGE = string.Format(message, lb[i].C_NAME, lb[i].B_AMOUNT, (Convert.ToDecimal(lb[i].B_AMOUNT) *(1+ Convert.ToDecimal(lb[i].B_INTEREST))/Convert.ToDecimal(lb[i].B_TERM)/100).ToString("#0.00"), lb[i].B_REPAYDATE);
                    m.S_TELEPHONE = lb[i].C_CONTACT;
                    m.S_SENDDATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    m.U_SYSID = lb[i].U_SYSID;
                    m.S_COMMIT = lb[i].B_SYSID;
                    string PostUrl = ConfigurationManager.AppSettings.Get("PostUrl");
                    string username = ConfigurationManager.AppSettings.Get("username");
                    string password = ConfigurationManager.AppSettings.Get("password");
                    string timestamps = (DateTime.Now.Ticks / 1000).ToString();
                    StringBuilder PostData = new StringBuilder();
                    PostData.Append(string.Format("account={0}", username));
                    PostData.Append(string.Format("&password={0}", md5(password + m.S_TELEPHONE + timestamps)));
                    PostData.Append(string.Format("&mobile={0}", m.S_TELEPHONE));
                    PostData.Append(string.Format("&content={0}", m.S_MESSAGE));
                    PostData.Append(string.Format("&timestamps={0}", timestamps));
                    
                    try
                    {
                       m.S_FLAG = RequestData(PostUrl + "?" + PostData.ToString()).Split('|')[3].Split(':')[1].Trim();
                    }
                    catch (Exception e1)
                    {
                        m.S_COMMIT = e1.Message;
                        m.S_FLAG = "0";
                    }
                    sendmessage_sql.Insert(m);
                    Thread.Sleep(30000);

            }


        }
    }
    public class Rets
    {
        public string Rspcode { get; set; }
        public string Msg_Id { get; set; }
        public string Mobile { get; set; }
        public string Fee { get; set; }
    }
}
