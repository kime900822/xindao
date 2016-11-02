using DbHelp.SQlHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinForm_Test;

namespace CashBorrowINFO.logon
{
    public partial class Regisert_form : baseForm
    {
        public Regisert_form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 只能输入数字和字母
        /// </summary>
        private string pattern_NumEng = "^[A-Za-z0-9]+$";


        private int time = 60;



        public bool IsWholeString(string strNum)
        {
            Regex notWholePattern = new Regex(@"^[0-9a-zA-Z\$]+$");
            return notWholePattern.IsMatch(strNum, 0);
        }




        public void match(TextBox textbox,string pattern) {
            string param = "";
            Match m = Regex.Match(textbox.Text, pattern_NumEng);
            if (!m.Success)   // 输入的不是数字
            {
                textbox.Text = param;   // textBox内容不变

                // 将光标定位到文本框的最后
                textbox.SelectionStart = textbox.Text.Length;
            }
            else   // 输入的是数字
            {
                param = textbox.Text;   // 将现在textBox的值保存下来
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string err=string.Empty;
            if (edtUid.Text.Trim() == "") {
                err += "请输入账号！\r\n";
                edtUid.BackColor = System.Drawing.Color.Red;
            }
            if (edtUmail.Text.Trim() == "")
            {
                err += "请输入邮箱！\r\n";
                edtUmail.BackColor = System.Drawing.Color.Red;
            }
            if (edtUTelephone.Text.Trim() == "") {
                err += "请输入手机号！\r\n";
                edtUTelephone.BackColor = System.Drawing.Color.Red;
            }

            if (edtUname.Text.Trim() == "") {
                err += "请输入公司名称！\r\n";
                edtUname.BackColor = System.Drawing.Color.Red;
            }

            if (edtPassword.Text.Trim() == "") {
                err += "请输密码！\r\n";
                edtPassword.BackColor = System.Drawing.Color.Red;
            }

            if (edtPsddeord_R.Text.Trim() == "") {
                err += "请输入确认密码！\r\n";
                edtPsddeord_R.BackColor = System.Drawing.Color.Red;
            }

            if (edtPassword.Text.Trim() != "" && edtPsddeord_R.Text.Trim() != "" && edtPassword.Text.Trim() != edtPsddeord_R.Text.Trim()) {
                err += "2次密码不一致！\r\n";
                edtPsddeord_R.BackColor = System.Drawing.Color.Red;
            }


            if (string.IsNullOrEmpty(err)) {
                if (user_sql.CheckUser(edtUid.Text.Trim(), "id")) {
                    err += "该账号已注册！\r\n";
                    edtUid.BackColor = System.Drawing.Color.Red;
                }

                if (user_sql.CheckUser( edtUmail.Text.Trim(), "mail")) {
                    err += "该邮件已注册！\r\n";
                    edtUmail.BackColor = System.Drawing.Color.Red;
                }
                   
                if (user_sql.CheckUser( edtUTelephone.Text.Trim(), "telephone")){
                    err += "该手机已注册！\r\n";
                    edtUTelephone.BackColor = System.Drawing.Color.Red;
                }
                    
                if (string.IsNullOrEmpty(err)) {

                    try
                    {
                        DbHelp.CS.USER user = new DbHelp.CS.USER();

                        user.U_ID = edtUid.Text.Trim();
                        user.U_MAIL = edtUmail.Text.Trim();
                        user.U_NAME = edtUname.Text.Trim();
                        user.U_TELEPHONE = edtUTelephone.Text.Trim();
                        user.U_PASSWORD = edtPassword.Text.Trim();
                        user.U_PROVINCE = ddlProvince.Text;
                        user.U_CITY = ddlCity.Text;
                        user.U_AREA = ddlArea.Text;

                        string res;
                        int b = 0;
                        frmWaitingBox f = new frmWaitingBox((obj, args) =>
                        {
                            Thread.Sleep(threadTime);
                            b = user_sql.Insert(user);
                        }, waitTime, "Plase Wait...", false, false);
                        f.ShowDialog(this);
                        res = f.Message;
                        if (!string.IsNullOrEmpty(res))
                            MessageBox.Show(res);
                        else {
                            if (b == 1)
                            {
                                MessageBox.Show("注册成功!", "注册通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                                MessageBox.Show("注册失败，请再试一下!", "注册通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message, "错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show(err, "错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
                MessageBox.Show(err, "错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Regisert_form_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(edtUid, "请输入数字或英文字母");
            toolTip1.SetToolTip(edtPassword, "请输入数字或英文字母");
            toolTip1.SetToolTip(edtUmail , "请输入邮件");
            toolTip1.SetToolTip(edtUname, "请输入公司名称");
            toolTip1.SetToolTip(edtUTelephone, "请输入手机号码");
            toolTip1.SetToolTip(edtPassword, "请输入数字或英文字母");
            toolTip1.SetToolTip(edtPsddeord_R, "请输入数字或英文字母");

            DataTable dt1 = province_sql.GetProvince();
            ddlProvince.DataSource = dt1;
            ddlProvince.DisplayMember = "PROVINCE"; 
            ddlProvince.ValueMember = "PROVINCEID";

            DataTable dt2 = province_sql.GetCity(ddlProvince.SelectedValue.ToString());
            ddlCity.DataSource = dt2;
            ddlCity.DisplayMember = "CITY";
            ddlCity.ValueMember = "CITYID";

            DataTable dt3 = province_sql.GetArea(ddlCity.SelectedValue.ToString());
            ddlArea.DataSource = dt3;
            ddlArea.DisplayMember = "AREA";
            ddlArea.ValueMember = "AREAID";
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlCity.DataSource = null;
            DataTable dt = province_sql.GetCity(ddlProvince.SelectedValue.ToString());
            ddlCity.DataSource = dt;
            ddlCity.DisplayMember = "CITY";
            ddlCity.ValueMember = "CITYID";
        }

        private void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = province_sql.GetArea(ddlCity.SelectedValue.ToString());
            ddlArea.DataSource = dt;
            ddlArea.DisplayMember = "AREA";
            ddlArea.ValueMember = "AREAID";
        }
    }
}
