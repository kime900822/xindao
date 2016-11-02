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

namespace CashBorrowINFO.main.CustomerCreditSearch
{
    public partial class CreditInCB_form : baseForm
    {
        public CreditInCB_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("查询全部信息消费5元", "查询提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK) {
                    DEBIT_HIS d = new DEBIT_HIS();
                    d.U_SYSID = logonUser.U_SYSID;
                    d.D_REASON = "平台内借款信息查询";
                    d.D_AMOUNT = "5";
                    d.D_DATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string balance = user_sql.QueryByWhere_XP(string.Format(" AND U_SYSID='{0}'", logonUser.U_SYSID))[0].U_BALANCE;
                    if (Convert.ToInt32(balance) < 5)
                    {
                        MessageBox.Show("余额不足", "查询提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else {
                        if (debit_his_sql.Insert(d) == 1)
                        {
                            user_sql.Debit_Amount(d);
                            logonUser.U_BALANCE = (Convert.ToInt32(balance) - 5).ToString();
                            string where = string.Empty;
                            if (dateS.Checked == true)
                            {
                                where += string.Format(" AND B_DATE >= '{0}' ", dateS.Text);
                            }
                            if (dateE.Checked == true)
                            {
                                where += string.Format(" AND B_DATE <= '{0}' ", dateE.Text);
                            }

                            if (!string.IsNullOrEmpty(edtCName.Text.Trim()))
                            {
                                where += " AND C_ID LIKE '%" + edtCName.Text.Trim() + "%' ";
                            }
                            if (!string.IsNullOrEmpty(edtGName.Text.Trim()))
                            {
                                where += " AND ( G_ID1 LIKE '%" + edtGName.Text.Trim() + "%'OR G_ID2 LIKE '%" + edtGName.Text.Trim() + "%' OR G_ID3 LIKE '%" + edtGName.Text.Trim() + "%' OR G_ID4 LIKE '%" + edtGName.Text.Trim() + "%' )";
                            }

                            string res;
                            DataTable dt = new DataTable();
                            frmWaitingBox f = new frmWaitingBox((obj, args) =>
                            {
                                Thread.Sleep(threadTime);
                                dt = borrow_sql.QueryByWhere(where);
                            }, waitTime, "Plase Wait...", false, false);
                            f.ShowDialog(this);
                            res = f.Message;
                            if (!string.IsNullOrEmpty(res))
                                MessageBox.Show(res);
                            else
                            {
                                if (dt.Rows.Count == 0)
                                {
                                    MessageBox.Show("无数据", "查询结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    dataGridBorrow.DataSource = dt;
                            }

                        }
                        else
                        {
                            MessageBox.Show("失败，请重试", "查询信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                  

                }


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "报错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void edtCName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void edtGName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }
    }
}
