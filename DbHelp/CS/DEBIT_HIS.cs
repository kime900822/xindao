using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelp.CS
{
    public class DEBIT_HIS
    {

        public string NUM { set; get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_SYSID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public USER USER { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string D_REASON { get; set; }


        /// <summary>
        /// 花费金额
        /// </summary>
        public string D_AMOUNT { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string D_DATE { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public string D_ISDEL { get; set; }


        /// <summary>
        /// 编号
        /// </summary>
        public string D_SYSID { get; set; }

    }
}
