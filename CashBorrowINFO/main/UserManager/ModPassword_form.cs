using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinForm_Test;

namespace CashBorrowINFO.main.UserManager
{
    public partial class ModPassword_form : baseForm
    {
        public ModPassword_form()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show("你确定要修改密码？", "修改提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    string err = string.Empty;
                    if (string.IsNullOrEmpty(edtOPass.Text.Trim()))
                    {
                        err += "请输入旧密码！\r\n";
                    }
                    if (!string.IsNullOrEmpty(edtOPass.Text.Trim()) && edtOPass.Text.Trim() != logonUser.U_PASSWORD)
                    {
                        err += "旧密码错误，请重新输入！\r\n";
                    }
                    if (string.IsNullOrEmpty(edtNpass.Text.Trim()))
                    {
                        err += "请输入新密码！\r\n";
                    }
                    if (string.IsNullOrEmpty(edtRpass.Text.Trim()))
                    {
                        err += "请再输一次新密码！\r\n";
                    }
                    if (!string.IsNullOrEmpty(edtNpass.Text.Trim()) && !string.IsNullOrEmpty(edtRpass.Text.Trim()) && edtNpass.Text.Trim() != edtRpass.Text.Trim())
                    {
                        err += "二次密码不一致，请重新输入！\r\n";
                    }
                    if (string.IsNullOrEmpty(err))
                    {
                        string res;
                        bool b = false;
                        frmWaitingBox f = new frmWaitingBox((obj, args) =>
                        {
                            Thread.Sleep(threadTime);
                            b = user_sql.ModPassword(logonUser, edtNpass.Text.Trim());
                        }, waitTime, "Plase Wait...", false, false);
                        f.ShowDialog(this);
                        res = f.Message;
                        if (!string.IsNullOrEmpty(res))
                            MessageBox.Show(res);
                        else
                        {
                            if (b)
                            {
                                MessageBox.Show("修改成功!", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("修改失败", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show(err, "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }


            }
            catch (Exception e1) {
                MessageBox.Show(e1.Message, "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
