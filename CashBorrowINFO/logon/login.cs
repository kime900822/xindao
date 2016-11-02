using DbHelp.SQlHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm_Test;

namespace CashBorrowINFO.logon
{
    public partial class login : baseForm
    {
        public login()
        {
            InitializeComponent();
        }

        private void forgetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FindPassword_form f = new FindPassword_form();
            f.ShowDialog();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            try {
                if (edtID.Text.Trim() != "" && edtPassword.Text.Trim() != "") {
                    string res;
                    bool b = false;
                    frmWaitingBox f = new frmWaitingBox((obj, args) =>
                    {
                        Thread.Sleep(threadTime);
                        b = user_sql.logon(edtID.Text, edtPassword.Text, out logonUser);
                    }, waitTime, "Plase Wait...", false, false);
                    f.ShowDialog(this);
                    res = f.Message;
                    if (!string.IsNullOrEmpty(res))
                        MessageBox.Show(res);
                    else
                    {
                        if (b)
                        {
                            MessageBox.Show("登陆成功", "登陆通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            if (cbSaveName.Checked == true)
                            {
                                config.AppSettings.Settings["saveUsername"].Value = edtID.Text.Trim();
                                config.AppSettings.Settings["saveCheck"].Value = "true";
                                config.Save();
                            }
                            else
                            {
                                config.AppSettings.Settings["saveUsername"].Value = "";
                                config.AppSettings.Settings["saveCheck"].Value = "false";
                                config.Save();
                            }
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("密码或者账号错误", "登陆通知", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }else
                    MessageBox.Show("请输入账号和密码！", "登陆通知", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            }
            catch (Exception e1) {
                MessageBox.Show(e1.Message, "错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult == DialogResult.Cancel) {
                DialogResult dr = MessageBox.Show("确定要退出吗?", "退出系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }

        }

        private void link_Register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Regisert_form f = new Regisert_form();
            f.ShowDialog();
            
        }

        private void login_Load(object sender, EventArgs e)
        {
           edtID.Text= ConfigurationManager.AppSettings.Get("saveUsername");
            if (ConfigurationManager.AppSettings.Get("saveCheck") == "true")
                cbSaveName.Checked = true;
        }

        private void link_ShowRule_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowRule_form f = new ShowRule_form();
            f.ShowDialog();
        }
    }
}
