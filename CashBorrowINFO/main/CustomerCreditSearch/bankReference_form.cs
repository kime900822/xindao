﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.main.CustomerCreditSearch
{
    public partial class bankReference_form : baseForm
    {
        public bankReference_form()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pbccrc.org.cn/");
        }
    }
}
