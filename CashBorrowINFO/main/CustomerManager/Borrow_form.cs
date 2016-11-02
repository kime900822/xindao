﻿using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinForm_Test;

namespace CashBorrowINFO.main.CustomerManager
{
    public partial class Borrow_form : baseForm
    {
        private string b_sysid;
        public Borrow_form(string b_sysid)
        {
            InitializeComponent();
            this.b_sysid = b_sysid;
        }



        private void btenOk_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show("确定保存？", "保存提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    string err = CheckDate();
                    if (string.IsNullOrEmpty(err))
                    {
                        BORROW borrow = FaceToData();
                        if (string.IsNullOrEmpty(b_sysid))
                        {
                            string res;
                            int i = 0;
                            frmWaitingBox f = new frmWaitingBox((obj, args) =>
                            {
                                Thread.Sleep(threadTime);
                                i = borrow_sql.Insert(borrow);
                            }, waitTime, "Plase Wait...", false, false);
                            f.ShowDialog(this);
                            res = f.Message;
                            if (!string.IsNullOrEmpty(res))
                                MessageBox.Show(res);
                            else
                            {
                                if (i == 1)
                                {
                                    MessageBox.Show("保存成功！", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnOk.Enabled = false;
                                    btnPrint.Enabled = true;
                                }
                                else
                                    MessageBox.Show("保存失败！", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        else
                        {
                            string res;
                            int i = 0;
                            frmWaitingBox f = new frmWaitingBox((obj, args) =>
                            {
                                i = borrow_sql.Update(borrow);
                            }, waitTime, "Plase Wait...", false, false);
                            f.ShowDialog(this);
                            res = f.Message;
                            if (!string.IsNullOrEmpty(res))
                                MessageBox.Show(res);

                            if (i == 1)
                            {
                                MessageBox.Show("保存成功！", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("保存失败！", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show(err, "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e1) {
                MessageBox.Show(e1.Message, "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        public string CheckDate() {
            string err = string.Empty;
            if (string.IsNullOrEmpty(edtCName.Text.Trim())) {
                err += "借款人姓名未输！\r\n";
            }
            if (string.IsNullOrEmpty(edtCID.Text.Trim()))
            {
                err += "借款人身份证号未输！\r\n";
            }
            if (string.IsNullOrEmpty(edtCAddress.Text.Trim()))
            {
                err += "借款人联系地址未输！\r\n";
            }
            if (string.IsNullOrEmpty(edtCContact.Text.Trim()))
            {
                err += "借款人联系方式未输！\r\n";
            }
            if (string.IsNullOrEmpty(edtCEmergencyName.Text.Trim()))
            {
                err += "借款人紧急联系人名字未输！\r\n";
            }
            if (string.IsNullOrEmpty(edtCEmergency.Text.Trim()))
            {
                err += "借款人紧急联系人方式未输！\r\n";
            }
            if (string.IsNullOrEmpty(ddlCSex.Text.Trim()))
            {
                err += "借款人性别未选择！\r\n";
            }
            //if (pbID.Image==null)
            //{
            //    err += "借款人身份证照片未选择！\r\n";
            //}         
            if (string.IsNullOrEmpty(edtBTerm.Text.Trim()))
            {
                err += "还款期数未输！\r\n";
            }
            //if (string.IsNullOrEmpty(edtGID1.Text.Trim()))
            //{
            //    err += "担保人身份证号未输！\r\n";
            //}
            //if (string.IsNullOrEmpty(ddlGJob1.Text.Trim()))
            //{
            //    err += "担保人职业未选择！\r\n";
            //}
            if (string.IsNullOrEmpty(edtBAmount.Text.Trim()))
            {
                err += "借款金额未输！\r\n";
            }
            if (string.IsNullOrEmpty(ddlType.Text.Trim()))
            {
                err += "借款方式未选择！\r\n";
            }
            if (string.IsNullOrEmpty(edtBInterest.Text.Trim()))
            {
                err += "利息未输入！\r\n";
            }
            if (!string.IsNullOrEmpty(pbID.ImageLocation)) {
                if (CashBorrowINFO.CS.Help.GetFileSize(pbID.ImageLocation) > 1048576)
                {
                    err += "图片大小不能超过1M！\r\n";
                }
            }
            return err;
        }


        public BORROW FaceToData() {
            BORROW b = new BORROW();
            b.B_AMOUNT = edtBAmount.Text.Trim();
            b.C_NAME = edtCName.Text.Trim();
            b.C_ID = edtCID.Text.Trim();
            b.C_SEX = ddlCSex.Text;
            if (pbID.Image != null)
                b.C_PIC = CashBorrowINFO.CS.Help.ReturnByte(pbID.Image);
            else
                b.C_PIC = null;
            b.C_EMERGENCY = edtCEmergency.Text.Trim();
            b.C_CONTACT = edtCContact.Text.Trim();
            b.C_ADDRESS = edtCAddress.Text.Trim();
            b.G_ID1 = edtGID1.Text.Trim();
            b.G_NAME1 = edtGName1.Text.Trim();
            b.G_JOB1 = ddlGJob1.Text;
            b.G_ID2 = edtGID2.Text.Trim();
            b.G_NAME2 = edtGName2.Text.Trim();
            b.G_JOB2 = ddlGJob2.Text;
            b.G_ID3 = edtGID3.Text.Trim();
            b.G_NAME3 = edtGName3.Text.Trim();
            b.G_JOB3 = ddlGJob3.Text;
            b.G_ID4 = edtGID4.Text.Trim();
            b.G_NAME4 = edtGName4.Text.Trim();
            b.G_JOB4 = ddlGJob4.Text;
            b.B_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            b.B_INTEREST = edtBInterest.Text.Trim();
            b.B_DATETMP= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            b.B_REPAYMENT = "0";
            b.B_REPAYDATE = edtRepay.Text;
            b.B_REMINDDATE = edtRemind.Text;
            b.U_SYSID = logonUser.U_SYSID;
            b.B_TYPE = ddlType.Text;
            b.B_SYSID = b_sysid;
            b.B_TERM = edtBTerm.Text.Trim();
            b.C_EMERGENCYNAME = edtCEmergencyName.Text.Trim();
            return b;
        }


        public void DataToFace(BORROW b) {
            edtBAmount.Text = b.B_AMOUNT;
            edtCName.Text = b.C_NAME;
            edtCID.Text = b.C_ID;
            ddlCSex.Text = b.C_SEX;
            edtCEmergency.Text = b.C_EMERGENCY;
            edtCContact.Text = b.C_CONTACT;
            edtCAddress.Text = b.C_ADDRESS;
            edtGID1.Text = b.G_ID1;
            edtGName1.Text = b.G_NAME1;
            ddlGJob1.Text = b.G_JOB1;
            edtGID2.Text = b.G_ID2;
            edtGName2.Text = b.G_NAME2;
            ddlGJob2.Text = b.G_JOB2;
            edtGID3.Text = b.G_ID3;
            edtGName3.Text = b.G_NAME3;
            ddlGJob3.Text = b.G_JOB3;
            edtGID4.Text = b.G_ID4;
            edtGName4.Text = b.G_NAME4;
            ddlGJob4.Text = b.G_JOB4;
            edtBInterest.Text = b.B_INTEREST;
            edtRepay.Text = b.B_REPAYDATE;
            edtRemind.Text = b.B_REMINDDATE;
            if(b.C_PIC!=null)
               pbID.Image = CashBorrowINFO.CS.Help.ReturnPhoto(b.C_PIC);
            edtBTerm.Text = b.B_TERM;
            ddlType.Text = b.B_TYPE;
            edtCEmergencyName.Text = b.C_EMERGENCYNAME;
            lblAmount.Text = lblAmount.Text = (Convert.ToDouble(edtBAmount.Text) * Convert.ToDouble(edtBInterest.Text) / 100).ToString("#0.00") + "元";
        }

        private void pbID_Click(object sender, EventArgs e)
        {
            openPic.Title = "选择身份证图片";
            openPic.ShowHelp = true;
            if (openPic.ShowDialog() == DialogResult.OK)
            {

                pbID.ImageLocation = openPic.FileName;
            }
        }

        private void Borrow_form_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(b_sysid))
            {
                string res;
                BORROW borrow = new BORROW();
                frmWaitingBox f = new frmWaitingBox((obj, args) =>
                {
                    Thread.Sleep(threadTime);
                    borrow = borrow_sql.QueryByWhere_XP(string.Format(" AND B_SYSID='{0}'", b_sysid))[0];
                }, waitTime, "Plase Wait...", false, false);
                f.ShowDialog(this);
                res = f.Message;
                if (!string.IsNullOrEmpty(res))
                    MessageBox.Show(res);
                else
                    DataToFace(borrow);
            }
            else
            {
                btnPrint.Enabled = false;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print_form f = new Print_form(FaceToData());
            f.Show();
        }

        private void edtBInterest_TextChanged(object sender, EventArgs e)
        {
            if (edtBAmount.Text != "")
                lblAmount.Text = (Convert.ToDouble(edtBAmount.Text) * Convert.ToDouble(edtBInterest.Text)/100).ToString("#0.00")+"元";

        }

        private void edtBAmount_TextChanged(object sender, EventArgs e)
        {
            if (edtBInterest.Text != "")
                lblAmount.Text = (Convert.ToDouble(edtBAmount.Text) * Convert.ToDouble(edtBInterest.Text)/100).ToString("#0.00")+"元";
        }

        private void edtBInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void edtRepay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void edtBTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void edtRemind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void edtBAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void edtCEmergency_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void edtCContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void edtCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar)&& e.KeyChar!= 88)
            {
                e.Handled = true;
            }
        }

        private void edtGID1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void edtGID2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void edtGID3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }

        private void edtGID4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != 88)
            {
                e.Handled = true;
            }
        }
    }
}
