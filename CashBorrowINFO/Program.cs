﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashBorrowINFO
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            logon.login log = new logon.login();
            log.ShowDialog();
            if (log.DialogResult == DialogResult.OK) {
                Application.Run(new Main());
            }
        }



    }
}
