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

namespace CashBorrowINFO.main.AmountRecharge
{
    public partial class DeductionsQuery_form : baseForm
    {
        public DeductionsQuery_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string where = string.Format(" AND U_SYSID = '{0}' ", logonUser.U_SYSID);
                if (dateS.Checked == true)
                {
                    where += string.Format(" AND SUBSTRING(D_DATE,1,10) >= '{0}' ", dateS.Text);
                }
                if (dateE.Checked == true)
                {
                    where += string.Format(" AND SUBSTRING(D_DATE,1,10) <= '{0}' ", dateE.Text);
                }

                string res;
                DataTable dt = new DataTable();
                frmWaitingBox f = new frmWaitingBox((obj, args) =>
                {
                    Thread.Sleep(threadTime);
                    dt = debit_his_sql.QueryByWhere(where);
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
                        dataGridDeductions.DataSource = null;
                    }
                    else
                        dataGridDeductions.DataSource = dt;
                }


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "报错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
