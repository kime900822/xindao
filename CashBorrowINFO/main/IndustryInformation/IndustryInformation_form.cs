using DbHelp.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.main.IndustryInformation
{
    public partial class IndustryInformation_form : baseForm
    {
        public IndustryInformation_form()
        {
            InitializeComponent();
        }


        private void IndustryInformation_form_Load(object sender, EventArgs e)
        {
            DataTable dt = edit_sql.QueryByWhere(" ORDER BY E_DATETIME DESC ");
            int i = dt.Rows.Count;
            dataGridEdit.DataSource = dt;
        }

        private void dataGridEdit_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string sysid = dataGridEdit.Rows[e.RowIndex].Cells[1].Value.ToString();
            string url = ConfigurationManager.AppSettings.Get("infourl");
            System.Diagnostics.Process.Start(url+"?sysid="+ sysid);
        }
    }
}
