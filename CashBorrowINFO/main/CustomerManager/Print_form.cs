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

namespace CashBorrowINFO.main.CustomerManager
{
    public partial class Print_form : baseForm
    {

        public static BORROW borrow = new BORROW();
        public Print_form(BORROW b)
        {
            InitializeComponent();
            string res;
            List<BORROW> lborrow=new List<BORROW>();
            frmWaitingBox f = new frmWaitingBox((obj, args) =>
            {
                Thread.Sleep(threadTime);
                lborrow = borrow_sql.QueryByWhere_XP(string.Format(" AND B_SYSID='{0}'", b.B_SYSID),false);
            }, waitTime, "Plase Wait...", false, false);
            f.ShowDialog(this);
            res = f.Message;
            if (!string.IsNullOrEmpty(res))
                MessageBox.Show(res);
            else
            {
                if (lborrow.Count > 0)
                    borrow = lborrow[0];
                else
                {
                    MessageBox.Show("数据读取异常！");
                    btnPrePrint.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSetPrint.Enabled = false;
                }
            }


            

            this.printDocument1.OriginAtMargins = true;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
        }

        private void btnSetPrint_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
        }

        private void btnPrePrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string s = edtprint.Text.Replace("_借款人", borrow.C_NAME).Replace("_身份证号码", borrow.C_ID).Replace("_贷款人", borrow.USER.U_NAME).Replace("_借款方式", borrow.B_TYPE).Replace("_借款金额", borrow.B_AMOUNT).Replace("_利息", borrow.B_INTEREST).Replace("_还款期数", borrow.B_TERM).Replace("_还款日期", borrow.B_REPAYDATE).Replace("_提醒日期", borrow.B_REMINDDATE).Replace("_借款日期", borrow.B_DATETMP);
            Font font = new Font("宋体", 12);
            Brush bru = Brushes.Black;
            e.Graphics.DrawString(s, font, bru, 20, 20);

            //Font titleFont = new Font("新宋体", 10, FontStyle.Bold);//字体                
            //Brush brush = new SolidBrush(Color.Black);//画刷        
            //Point po = new Point(10, 10);  //坐标

            //float left = e.PageSettings.Margins.Left;//打印区域的左边界
            //float top = e.PageSettings.Margins.Top;//打印区域的上边界
            //float width = e.PageSettings.PaperSize.Width - left - e.PageSettings.Margins.Right;//计算出有效打印区域的宽度
            //float height = e.PageSettings.PaperSize.Height - top - e.PageSettings.Margins.Bottom;//计算出有效打印区域的高度


            //int wid = e.PageSettings.PaperSize.Width;
            ////标题
            //string name = "99街区KTV";
            ////这里想要居中打印标题
            //e.Graphics.DrawString(name, titleFont, brush, 80 - name.Length + 2, 80 - name.Length + 2);

        }

    }
}
