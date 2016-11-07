using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinForm_Test;

namespace CashBorrowINFO.main.IndustryInformation
{
    public partial class IndustryInformation_form : baseForm
    {
        public IndustryInformation_form()
        {
            InitializeComponent();
        }


        private void IndustryInformation_form_Load(object sender, EventArgs e)
        {
            bindData();
        }

        private void dataGridEdit_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string res;
            frmWaitingBox f = new frmWaitingBox((obj, args) =>
            {
                Thread.Sleep(threadTime);
                string sysid = dataGridEdit.Rows[e.RowIndex].Cells[1].Value.ToString();
                string url = ConfigurationManager.AppSettings.Get("infourl");
                System.Diagnostics.Process.Start(url + "?sysid=" + sysid);
            }, waitTime, "Plase Wait...", false, false);
            f.ShowDialog(this);
            res = f.Message;
            if (!string.IsNullOrEmpty(res))
                MessageBox.Show(res);


        }

        public void bindData() {
            dataGridEdit.DataSource = null;
            string res;
            DataTable dt = new DataTable();
            int count = 0;
            frmWaitingBox f = new frmWaitingBox((obj, args) =>
            {
                Thread.Sleep(threadTime);
                count = Convert.ToInt32(edit_sql.GetTotal(" "));
                dt = edit_sql.QueryByWhere("  ", pagerControl1.PageIndex - 1, pagerControl1.PageSize);
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
                    dataGridEdit.DataSource = dt;
                }

            }

        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            bindData();
        }

        private void IndustryInformation_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MdiParent.MdiChildren.Length == 1)
            {
                this.MdiParent.Controls.Find("pictureBox1", true)[0].Visible = true;

            }
        }
    }
}
