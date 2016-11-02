using DbHelp.CS;
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
    public partial class UserInfo_form : baseForm
    {
        public UserInfo_form()
        {
            InitializeComponent();
        }

        private void UserInfo_form_Load(object sender, EventArgs e)
        {

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

            DataToFace(logonUser);
        }



        public void DataToFace(USER u) {
            edtUid.Text = u.U_ID;
            edtUmail.Text = u.U_MAIL;
            edtUBalance.Text = u.U_BALANCE;
            edtUFreeMessage.Text = u.U_FREEMESSAGE;
            edtUTelephone.Text = u.U_TELEPHONE;
            edtUName.Text = u.U_NAME;
            ddlProvince.Text = u.U_PROVINCE.Trim();
            ddlCity.Text = u.U_CITY.Trim();
            ddlArea.Text = u.U_AREA.Trim();
        }



        public void FaceToDate() {
            logonUser.U_ID = edtUid.Text;
            logonUser.U_MAIL = edtUmail.Text;
            logonUser.U_CITY = ddlCity.Text;
            logonUser.U_AREA = ddlArea.Text;
            logonUser.U_PROVINCE = ddlProvince.Text;

        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定修改？", "修改提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string err = string.Empty;
                    if (string.IsNullOrEmpty(edtUid.Text.Trim()))
                    {
                        err += "请输入账号！\r\n";
                    }
                    if (string.IsNullOrEmpty(edtUmail.Text.Trim()))
                    {
                        err += "请输入邮箱！\r\n";
                    }
                    if (string.IsNullOrEmpty(err))
                    {
                        FaceToDate();

                        string res;
                        int i = 0;
                        frmWaitingBox f = new frmWaitingBox((obj, args) =>
                        {
                            Thread.Sleep(threadTime);
                            i = user_sql.Update(logonUser);
                        }, waitTime, "Plase Wait...", false, false);
                        f.ShowDialog(this);
                        res = f.Message;
                        if (!string.IsNullOrEmpty(res))
                            MessageBox.Show(res);
                        else
                        {
                            if (i == 1)
                            {
                                MessageBox.Show("修改成功！", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("修改失败！", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }



                    }
                    else
                    {
                        MessageBox.Show(err, "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message,"修改提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
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
