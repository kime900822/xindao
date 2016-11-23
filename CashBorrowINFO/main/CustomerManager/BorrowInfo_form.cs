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
    public partial class BorrowInfo_form : baseForm
    {
        public BorrowInfo_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Borrow_form f = new Borrow_form(dataGridBorrow.Rows[e.RowIndex].Cells[1].Value.ToString());
            f.Show();
        }

        private void edtCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }

        }

        public void bindData() {

            try
            {
                dataGridBorrow.DataSource = null;
                string where = string.Format(" AND  U_SYSID='{0}'", logonUser.U_SYSID);
                if (dateS.Checked == true)
                {
                    where += string.Format(" AND B_DATE >= '{0}' ", dateS.Text);
                }
                if (dateE.Checked == true)
                {
                    where += string.Format(" AND B_DATE <= '{0}' ", dateE.Text);
                }

                if (ddlType.Text == "已还清")
                {
                    where += " AND B_AMOUNT <= B_REPAYMENT ";
                }
                else if(ddlType.Text == "未还清")
                {
                    where += " AND B_AMOUNT > B_REPAYMENT  ";
                }

                if (!string.IsNullOrEmpty(edtCID.Text.Trim()))
                {
                    where += " AND C_ID LIKE '%" + edtCID.Text.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(edtGID.Text.Trim()))
                {
                    where += " AND ( G_ID1 LIKE '%" + edtCID.Text.Trim() + "%' OR G_ID2 LIKE '%" + edtCID.Text.Trim() + "%' OR G_ID3 LIKE '%" + edtCID.Text.Trim() + "%'OR G_ID4 LIKE '%" + edtCID.Text.Trim() + "%') ";
                }

                int count = 0;
                string res;
                DataTable dt = new DataTable();
                frmWaitingBox f = new frmWaitingBox((obj, args) =>
                {
                    Thread.Sleep(threadTime);
                    count = Convert.ToInt32(borrow_sql.GetTotal(where));
                    dt = borrow_sql.QueryByWhere(where, pagerControl1.PageIndex - 1, pagerControl1.PageSize);
                }, waitTime, "Plase Wait...", false, false);
                f.ShowDialog(this);
                res = f.Message;
                if (!string.IsNullOrEmpty(res))
                    MessageBox.Show(res);
                else {
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("无数据", "查询结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else {
                        pagerControl1.DrawControl(count);
                        dataGridBorrow.DataSource = dt;
                        dataGridBorrow.Columns[0].HeaderText = "序号";
                        dataGridBorrow.Columns[0].Width = 80;
                        dataGridBorrow.Columns[1].Width = 100;
                        dataGridBorrow.Columns[2].Width = 80;
                        dataGridBorrow.Columns[3].Width = 100;
                        dataGridBorrow.Columns[4].Width = 100;
                        dataGridBorrow.Columns[5].Width = 100;
                        dataGridBorrow.Columns[6].Width = 150;
                        dataGridBorrow.Columns[7].Width = 150;
                        dataGridBorrow.Columns[8].Width = 100;
                        dataGridBorrow.Columns[9].Width = 80;
                        dataGridBorrow.Columns[10].Width = 100;
                        dataGridBorrow.Columns[11].Width = 150;
                        dataGridBorrow.Columns[12].Width = 100;
                        dataGridBorrow.Columns[13].Width = 100;
                        dataGridBorrow.Columns[14].Width = 150;
                        dataGridBorrow.Columns[15].Width = 100;
                        dataGridBorrow.Columns[16].Width = 100;
                        dataGridBorrow.Columns[17].Width = 150;
                        dataGridBorrow.Columns[18].Width = 100;
                        dataGridBorrow.Columns[19].Width = 100;
                        dataGridBorrow.Columns[20].Width = 150;
                        dataGridBorrow.Columns[21].Width = 100;
                        dataGridBorrow.Columns[22].Width = 100;
                        dataGridBorrow.Columns[23].Width = 100;
                        dataGridBorrow.Columns[24].Width = 80;
                        dataGridBorrow.Columns[25].Width = 80;
                        dataGridBorrow.Columns[26].Width = 100;
                        dataGridBorrow.Columns[27].Width = 100;
                        dataGridBorrow.Columns[28].Width = 100;
                        dataGridBorrow.Columns[29].Width = 100;
                        dataGridBorrow.Columns[30].Width = 100;
                        dataGridBorrow.Columns[31].Width = 150;
                    }
                        
                }


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "报错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            bindData();
        }

        private void BorrowInfo_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MdiParent.MdiChildren.Length == 1)
            {
                this.MdiParent.Controls.Find("pictureBox1", true)[0].Visible = true;

            }
        }
    }
}
