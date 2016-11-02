using DbHelp.SQlHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.logon
{
    public partial class FindPassword_form : baseForm
    {
        public FindPassword_form()
        {
            InitializeComponent();
        }


        private int time = 60;
        private void lbGetMessage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            time = 60;
            timer1.Start();
            lbGetMessage.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                lbGetMessage.Text = time + "秒后重发";
            }
            else {
                timer1.Stop();
                lbGetMessage.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (edtTelephone.Text.Trim() != "" && edtChechNum.Text.Trim() != "") {
                try
                {
                    string password = user_sql.GetPassword( edtTelephone.Text.Trim());
                }
                catch (Exception e1) {
                    MessageBox.Show(e1.Message, "错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }                


        }
    }
}
