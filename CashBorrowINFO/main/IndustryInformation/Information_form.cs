using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.main.IndustryInformation
{
    public partial class Information_form : baseForm
    {
        public Information_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser.Url = new Uri("http://www.baidu.com");

        }

        private void Information_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MdiParent.MdiChildren.Length == 1)
            {
                this.MdiParent.Controls.Find("pictureBox1", true)[0].Visible = true;

            }
        }
    }
}
