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
    public partial class Customer_form : baseForm
    {
        public Customer_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindData();
        }


        public void bindData()
        {
            dataGridCustomer.DataSource = null;
            string where = string.Format(" AND  U_SYSID='{0}'", logonUser.U_SYSID);
            if (dateS.Checked == true)
            {
                where += string.Format(" AND B_DATE >= '{0}' ", dateS.Text);
            }
            if (dateE.Checked == true)
            {
                where += string.Format(" AND B_DATE <= '{0}' ", dateE.Text);
            }

            if (!string.IsNullOrEmpty(edtCID.Text.Trim()))
            {
                where += " AND C_ID LIKE '%" + edtCID.Text.Trim() + "%' ";
            }

            int count=0;
            string res;
            DataTable dt = new DataTable();
            frmWaitingBox f = new frmWaitingBox((obj, args) =>
            {
                Thread.Sleep(threadTime);
                count = Convert.ToInt32(borrow_sql.GetTotalCustomer(where));
                dt = borrow_sql.CustomerQueryByWhere(where, pagerControl1.PageIndex-1,pagerControl1.PageSize);
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
                    dataGridCustomer.DataSource = dt;
                    dataGridCustomer.Columns[0].HeaderText = "序号";
                    dataGridCustomer.Columns[0].Width = 80;
                    dataGridCustomer.Columns[1].Width = 100;
                    dataGridCustomer.Columns[2].Width = 150;
                    dataGridCustomer.Columns[3].Width = 100;
                    dataGridCustomer.Columns[4].Width = 150;
                    dataGridCustomer.Columns[5].Width = 150;
                    dataGridCustomer.Columns[6].Width = 200;
                    dataGridCustomer.Columns[7].Width = 80;
                }
                   
            }
        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            bindData();
        }

        private void edtCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void Customer_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MdiParent.MdiChildren.Length == 1)
            {
                this.MdiParent.Controls.Find("pictureBox1", true)[0].Visible = true;

            }
        }
    }


}
