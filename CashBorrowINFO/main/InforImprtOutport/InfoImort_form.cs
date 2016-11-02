using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.main.InforImprtOutport
{
    public partial class InfoImort_form : baseForm
    {
        public InfoImort_form()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    edtPath.Text = openFileDialog.FileName;
                    dataGridBorrow.DataSource = null;
                    DataTable myds = new DataTable();
                    string strCon;
                    OleDbConnection olecon = new OleDbConnection();
                    OleDbDataAdapter myda = new OleDbDataAdapter();
                    try
                    {

                        strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=Excel 8.0;";
                        strCon = string.Format(strCon, openFileDialog.FileName);
                        olecon = new OleDbConnection(strCon);
                        myda = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strCon);
                        myda.Fill(myds);
                    }
                    catch (Exception e1) {
                        olecon.Dispose();
                        strCon = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0}; Extended Properties='Excel 12.0; HDR=Yes; IMEX=1';";
                        strCon = string.Format(strCon, openFileDialog.FileName);
                        olecon = new OleDbConnection(strCon);
                        myda = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strCon);
                        myda.Fill(myds);
                    }

                    olecon.Close();
                    dataGridBorrow.DataSource = myds;
                    for (int i = 0; i < myds.Rows.Count; i++) {
                        BORROW b = new BORROW();
                        b.C_NAME = myds.Rows[i][0].ToString();
                        b.C_ID = myds.Rows[i][1].ToString();
                        b.C_CONTACT = myds.Rows[i][2].ToString();
                        b.C_EMERGENCY = myds.Rows[i][3].ToString();
                        b.C_ADDRESS = myds.Rows[i][4].ToString();
                        b.C_SEX = myds.Rows[i][5].ToString();
                        b.G_NAME1 = myds.Rows[i][6].ToString();
                        b.G_ID1 = myds.Rows[i][7].ToString();
                        b.G_JOB1 = myds.Rows[i][8].ToString();
                        b.G_NAME2 = myds.Rows[i][9].ToString();
                        b.G_ID2 = myds.Rows[i][10].ToString();
                        b.G_JOB2 = myds.Rows[i][11].ToString();
                        b.G_NAME3 = myds.Rows[i][12].ToString();
                        b.G_ID3 = myds.Rows[i][13].ToString();
                        b.G_JOB3 = myds.Rows[i][14].ToString();
                        b.G_NAME4 = myds.Rows[i][15].ToString();
                        b.G_ID4 = myds.Rows[i][16].ToString();
                        b.G_JOB4 = myds.Rows[i][17].ToString();
                        b.B_AMOUNT = myds.Rows[i][18].ToString();
                        b.B_REPAYMENT = myds.Rows[i][19].ToString();
                        b.B_INTEREST = myds.Rows[i][20].ToString();
                        b.B_TERM= myds.Rows[i][21].ToString();
                        b.B_TYPE = myds.Rows[i][22].ToString();
                        b.B_REPAYDATE = myds.Rows[i][23].ToString();
                        b.B_REMINDDATE = myds.Rows[i][24].ToString();
                        b.B_DATE = DateTime.Parse(myds.Rows[i][25].ToString()).ToString("yyyy-MM-dd");
                        b.B_DATETMP = DateTime.Parse(myds.Rows[i][26].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                        b.U_SYSID = logonUser.U_SYSID;
                        b.C_PIC = new byte[0];

                        if (borrow_sql.Insert(b) != 1){
                            MessageBox.Show(b.C_NAME+" 借款资料导入失败，请修改导入资料，重新导入后续资料", "导入", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        progressBar.Value = (i + 1) * 100 / myds.Rows.Count;
                        Application.DoEvents();
                    }

                    MessageBox.Show("资料导入成！", "导入", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void lkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    if (CashBorrowINFO.CS.Help.DownloadFile(ConfigurationManager.AppSettings.Get("fileExcel"), saveFileDialog.FileName, null) == 1) {
                        MessageBox.Show("模板下载成功！", "下载提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception e1) {
                    MessageBox.Show(e1.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }
    }
}
