using CashBorrowINFO.main.AmountRecharge;
using CashBorrowINFO.main.CustomerCreditSearch;
using CashBorrowINFO.main.CustomerManager;
using CashBorrowINFO.main.IndustryInformation;
using CashBorrowINFO.main.InforImprtOutport;
using CashBorrowINFO.main.UserManager;
using System;
using System.Windows.Forms;

namespace CashBorrowINFO
{
    public partial class Main : baseForm
    {
        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            tsShowUser.Text = "登录用户：" + logonUser.U_NAME + "             ";
            tsShowTime.Text = "当前时间：" + DateTime.Now.ToString();
            timer1.Start();
        }

        public bool closeFlag = false;

        private bool ShowChildrenForm(string p_ChildrenFormText,bool isChild)
        {
            if(isChild)
                 pictureBox1.Visible = false;
            //if (this.MdiChildren.Length > 5)
            //{
            //    MessageBox.Show("最多打开5个窗口", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return true;
            //}
            //else {
            int i;     //依次检测当前窗体的子窗体     
            for (i = 0; i < this.MdiChildren.Length; i++)
            {         //判断当前子窗体的Text属性值是否与传入的字符串值相同
                if (this.MdiChildren[i].Text == p_ChildrenFormText)
                {             //如果值相同则表示此子窗体为想要调用的子窗体，激活此子窗体并返回true值   
                    this.MdiChildren[i].Activate();
                    return true;
                }
            }     //如果没有相同的值则表示要调用的子窗体还没有被打开，返回false值     
            //}
            return false;
        }



        private void N70_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出登录么？", "退出登陆", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                closeFlag = true;
                Application.Exit();
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeFlag == false) {
                if (MessageBox.Show("你确定要退出系统么？", "退出系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tsShowTime.Text = "当前时间：" + DateTime.Now.ToString();
        }

        private void M61_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("查看/修改用户信息",false))
            {
                UserInfo_form f = new UserInfo_form();
                f.Show();

            }

        }

        private void M62_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("修改密码", false))
            {
                ModPassword_form f = new ModPassword_form();
                f.Show();
            }

        }

        private void M12_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("借款", true))
            {
                Borrow_form f = new Borrow_form("");
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M13_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("还款", true))
            {
                Repay_form f = new Repay_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M22_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("借款信息批量导出", true))
            {
                InfoOut_form f = new InfoOut_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M21_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("借款信息批量导入", true))
            {
                InfoImort_form f = new InfoImort_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M31_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("银联支付", true))
            {
                UnionPay_form f = new UnionPay_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M32_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("支付宝支付", true))
            {
                Alipay_form f = new Alipay_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M33_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("短信扣费", true))
            {
                Message_form f = new Message_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M34_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("查询扣费", true))
            {
                DeductionsQuery_form f = new DeductionsQuery_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M41_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("民间借贷（投资）查询", true))
            {
                CreditInCB_form f = new CreditInCB_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M42_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("失信人员查询", true))
            {
                Dishonesty_form f = new Dishonesty_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M43_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("企业信用信息查询", true))
            {
                Corporateloans_form f = new Corporateloans_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M44_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("银行征信", true))
            {
                bankReference_form f = new bankReference_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M50_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("行业资讯", true))
            {
                IndustryInformation_form f = new IndustryInformation_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void N35_Click(object sender, EventArgs e)
        {

            if (!ShowChildrenForm("充值记录查询", true))
            {
                PrepaidQuery_form f = new PrepaidQuery_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            

        }

        private void M112_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("借款信息查询", true))
            {
                BorrowInfo_form f = new BorrowInfo_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M111_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("客户查询", true))
            {
                Customer_form f = new Customer_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private void M113_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("短信发送查询", true))
            {
                MessageQuery_form f = new MessageQuery_form();
                f.MdiParent = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }
    }
}
