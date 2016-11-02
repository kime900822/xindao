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
            try
            {
                string where = string.Format(" AND  U_SYSID='{0}'",logonUser.U_SYSID);
                if (dateS.Checked == true) {
                    where += string.Format(" AND B_DATE >= '{0}' ", dateS.Text);
                }
                if (dateE.Checked == true) {
                    where += string.Format(" AND B_DATE <= '{0}' ", dateE.Text);
                }

                if (!string.IsNullOrEmpty(edtCID.Text.Trim())) {
                    where += " AND C_ID LIKE '%" + edtCID.Text.Trim() + "%' ";
                }
                if (!string.IsNullOrEmpty(edtGID.Text.Trim()))
                {
                    where += " AND ( G_ID1 LIKE '%" + edtCID.Text.Trim() + "%' OR G_ID2 LIKE '%" + edtCID.Text.Trim() + "%' OR G_ID3 LIKE '%" + edtCID.Text.Trim() + "%'OR G_ID4 LIKE '%" + edtCID.Text.Trim() + "%') ";
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
                else {
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("无数据", "查询结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        dataGridBorrow.DataSource = dt;
                }


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "报错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Borrow_form f = new Borrow_form(dataGridBorrow.Rows[e.RowIndex].Cells[0].Value.ToString());
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
    }
}
