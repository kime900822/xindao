using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.main.AmountRecharge
{
    public partial class Alipay_form : baseForm
    {
        public Alipay_form()
        {
            InitializeComponent();
    }

        private void Alipay_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MdiParent.MdiChildren.Length == 1)
            {
                this.MdiParent.Controls.Find("pictureBox1", true)[0].Visible = true;
                
            }
        }
    }
}
