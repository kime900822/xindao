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

namespace CashBorrowINFO.main.CustomerManager
{
    public partial class Repay_form : baseForm
    {
        public Repay_form()
        {
            InitializeComponent();
        }

        private void Repay_form_Load(object sender, EventArgs e)
        {
            Init();
        }







        public void Init(){
            try {
                DataTable dt = borrow_sql.GetItem(logonUser.U_SYSID);
                for (int i = 0; i < dt.Rows.Count; i++) {
                    ddlBSysid.Items.Add(dt.Rows[i][0].ToString());
                }
            }
            catch (Exception e1) {
                MessageBox.Show(e1.Message, "数据初始化", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ddlBSysid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlBSysid.Text)) {
                string b_sysid = ddlBSysid.Text.Split('-')[0];
                string res;
                BORROW borrow = new BORROW();
                frmWaitingBox f = new frmWaitingBox((obj, args) =>
                {
                    Thread.Sleep(threadTime);
                    borrow = borrow_sql.QueryByWhere_XP(string.Format(" AND B_SYSID='{0}'", b_sysid))[0];
                }, waitTime, "Plase Wait...", false, false);
                f.ShowDialog(this);
                res = f.Message;
                if (!string.IsNullOrEmpty(res))
                    MessageBox.Show(res);
                else
                {
                    lblCName.Text = borrow.C_NAME;
                    lblCID.Text = borrow.C_ID;
                    lblBAMOUNT.Text = borrow.B_AMOUNT;
                    lblBLimit.Text = (Convert.ToDecimal(borrow.B_AMOUNT) - Convert.ToDecimal(borrow.B_REPAYMENT)).ToString();
                    Bind(b_sysid);
                }

            }

        }

        public REPAY_HIS FaceToDate() {
            REPAY_HIS repay = new REPAY_HIS();
            repay.B_SYSID= ddlBSysid.Text.Split('-')[0];
            repay.R_AMOUNT = edtRamount.Text.Trim();
            repay.R_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            repay.R_OVERTIME = edtOverTime.Text;
            repay.R_FINE = edtRFine.Text.Trim();
            repay.R_TYPE = edtRType.Text.Trim();
            repay.R_INTEREST = edtRInterest.Text.Trim();
            return repay;
        }


        public string CheckData() {
            string err = string.Empty;
            if (string.IsNullOrEmpty(ddlBSysid.Text.Trim()))
            {
                err += "未选择借款单！\r\n";
            }
            if (string.IsNullOrEmpty(edtRType.Text.Trim()))
            {
                err += "未输入还款方式！\r\n";
            }
            return err;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定保存？", "保存提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string err = CheckData();
                    if (string.IsNullOrEmpty(err))
                    {
                        REPAY_HIS r = FaceToDate();

                        string res;
                        int i = 0;
                        frmWaitingBox f = new frmWaitingBox((obj, args) =>
                        {
                            i = repay_his_sql.Insert(r);
                        }, waitTime, "Plase Wait...", false, false);
                        f.ShowDialog(this);
                        res = f.Message;
                        if (!string.IsNullOrEmpty(res))
                            MessageBox.Show(res);

                        if (i == 1)
                        {
                            borrow_sql.Repay(r.B_SYSID, r.R_AMOUNT);
                            Bind(ddlBSysid.Text.Split('-')[0]);
                            MessageBox.Show("保存成功", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("保存失败", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show(err, "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e1) {
                MessageBox.Show(e1.Message, "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public  void Bind(string b_sysid) {
            DataTable dt = repay_his_sql.QueryByWhere(string.Format(" AND B_SYSID='{0}'", b_sysid));
            dataGridRepay.DataSource = dt;

        }

        private void edtRamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void edtRInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void edtRFine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
    }
}
