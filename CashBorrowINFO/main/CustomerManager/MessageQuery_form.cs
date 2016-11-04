﻿using System;
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
    public partial class MessageQuery_form : baseForm
    {
        public MessageQuery_form()
        {
            InitializeComponent();
        }

        private void edtCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindData();
        }


        public void bindData()
        {
            dataGridMessage.DataSource = null;
            string where = string.Format(" AND  U_SYSID='{0}'", logonUser.U_SYSID);
            if (dateS.Checked == true)
            {
                where += string.Format(" AND B_DATE >= '{0}' ", dateS.Text);
            }
            if (dateE.Checked == true)
            {
                where += string.Format(" AND B_DATE <= '{0}' ", dateE.Text);
            }

            if (!string.IsNullOrEmpty(edtSTelephone.Text.Trim()))
            {
                where += " AND S_TELEPHONE LIKE '%" + edtSTelephone.Text.Trim() + "%' ";
            }

            int count = 0;
            string res;
            DataTable dt = new DataTable();
            frmWaitingBox f = new frmWaitingBox((obj, args) =>
            {
                Thread.Sleep(threadTime);
                count = Convert.ToInt32(sendmessage_sql.GetTotal(where));
                dt = sendmessage_sql.QueryByWhere(where, pagerControl1.PageIndex - 1, pagerControl1.PageSize);
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
                else {
                    pagerControl1.DrawControl(count);
                    dataGridMessage.DataSource = dt;
                }

            }
        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            bindData();
        }
    }
}
