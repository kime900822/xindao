using DbHelp.SQlHelp;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashBorrowINFO
{
    public class baseForm: Office2007Form
    {
        private StyleManager styleManager1;
        private System.ComponentModel.IContainer components;

        public static string userCode { get; set; }

        private static string Key_64 = "shdsfjdk";
        private static string Iv_64 = "zjxdtzgl";
        public static string server = CashBorrowINFO.CS.Help.Decode(ConfigurationManager.AppSettings.Get("server"), Key_64, Iv_64);
        public static string uid = CashBorrowINFO.CS.Help.Decode(ConfigurationManager.AppSettings.Get("uid"), Key_64, Iv_64);
        public static string pwd = CashBorrowINFO.CS.Help.Decode(ConfigurationManager.AppSettings.Get("pwd"), Key_64, Iv_64);
        public static string database = CashBorrowINFO.CS.Help.Decode(ConfigurationManager.AppSettings.Get("database"), Key_64, Iv_64);


        public static string  connString { get {
                if (ConfigurationManager.AppSettings.Get("connstype") == "0") {
                    return CashBorrowINFO.CS.Help.Decode(ConfigurationManager.ConnectionStrings["connString"].ConnectionString.ToString(), Key_64, Iv_64); ;
                }
                else {
                    return string.Format("server={0},uid={1},pwd={2},database={3}", server, uid, pwd, database);
                }
                }
        }



        public static DbHelp.CS.USER logonUser = new DbHelp.CS.USER();

        /// <summary>
        /// 用户管理
        /// </summary>
        public static T_USER_SQL user_sql { get { return new T_USER_SQL(connString); } }

        /// <summary>
        /// 借款记录
        /// </summary>
        public static T_BORROW_SQL borrow_sql { get { return new T_BORROW_SQL(connString); } }

        /// <summary>
        /// 消费记录
        /// </summary>
        public static T_DEBIT_HIS_SQL debit_his_sql { get { return new T_DEBIT_HIS_SQL(connString); } }

        /// <summary>
        /// 还款记录
        /// </summary>
        public static T_REPAY_HIS_SQL repay_his_sql { get { return new T_REPAY_HIS_SQL(connString); } }


        public static T_PREPAID_HIS_SQL prepadi_his_sql { get { return new T_PREPAID_HIS_SQL(connString); } }

        public static T_PROVINCE_SQL province_sql { get { return new T_PROVINCE_SQL(connString); } }

        public static T_EDIT_SQL edit_sql { get { return new T_EDIT_SQL(connString); } }

        public static T_SENDMESSAGE_SQL sendmessage_sql { get { return new T_SENDMESSAGE_SQL(connString); } }

        /// <summary>
        /// 等待时间
        /// </summary>
        public static int waitTime = 20;
        public static int threadTime = 500;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // baseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "baseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }

    



}
