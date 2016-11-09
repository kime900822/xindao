﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinForm_Test;

namespace CashBorrowINFO.main.InforImprtOutport
{
    public partial class InfoOut_form : baseForm
    {
        public InfoOut_form()
        {
            InitializeComponent();
        }

        private void InfoOut_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindDate();
        }
        public void bindDate() {

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

                if (!string.IsNullOrEmpty(edtCID.Text.Trim()))
                {
                    where += " AND C_ID LIKE '%" + edtCID.Text.Trim() + "%' ";
                }


                DataTable dt = new DataTable();
                string res;
                int count = 0;
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
                else
                {
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
                        dataGridBorrow.Columns[30].Width = 150;
                    }

                }



            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "报错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public DataTable GetData() {
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
            return borrow_sql.QueryByWhere(where);

        }

        private void edtOut_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();
            if (dt.Rows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.CreatePrompt = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "借款信息导出-" + DateTime.Now.ToString("yyyy-MM-dd");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    progressBar.Value = 0;
                        Stream myStream;
                        myStream = dlg.OpenFile();
                        StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                        string columnTitle = "";
                        try
                        {
                            //写入列标题     
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (i > 0)
                                {
                                    columnTitle += "\t";
                                }
                                columnTitle += dt.Columns[i].ColumnName;
                            }
                            sw.WriteLine(columnTitle);

                            //写入列内容     
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string columnValue = "";
                                for (int k = 0; k < dt.Columns.Count; k++)
                                {
                                    if (k > 0)
                                    {
                                        columnValue += "\t";
                                    }
                                    if (dt.Rows[j][k].ToString() == null)
                                        columnValue += "";
                                    else
                                        columnValue += dt.Rows[j][k].ToString();
                                }
                                sw.WriteLine(columnValue);
                            progressBar.Value = (j + 1) * 100 / dt.Rows.Count;
                            Application.DoEvents();
                            }
                            sw.Close();
                            myStream.Close();
                            MessageBox.Show("导出成功", "导出信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.Message, "导出报错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            sw.Close();
                            myStream.Close();
                        }

                   
                }
            }
            else
            {
                MessageBox.Show("没数据", "导出信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void edtCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            bindDate();
        }

        private void InfoOut_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MdiParent.MdiChildren.Length == 1)
            {
                this.MdiParent.Controls.Find("pictureBox1", true)[0].Visible = true;

            }
        }
    }
}
