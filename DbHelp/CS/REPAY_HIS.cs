using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelp.CS
{
    public class REPAY_HIS
    {

        public string NUM { set; get; }

        /// <summary>
        /// 借款编号
        /// </summary>
        public string B_SYSID { get; set; }


        /// <summary>
        /// 客户
        /// </summary>
        public BORROW BORROW { get; set; }


        /// <summary>
        /// 用户编号
        /// </summary>
        public string R_AMOUNT { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public string R_OVERTIME { get; set; }

        /// <summary>
        /// 处罚金
        /// </summary>
        public string R_FINE { get; set; }


        /// <summary>
        /// 日期
        /// </summary>
        public string R_DATE { get; set; }

        /// <summary>
        /// 还款方式
        /// </summary>
        public string R_TYPE { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public string R_ISDEL { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string R_SYSID { get; set; }

        /// <summary>
        /// 还款利息
        /// </summary>
        public string R_INTEREST { get; set; }
    }
}
