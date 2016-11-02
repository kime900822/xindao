using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbHelp.CS
{
    public class MESSAGE
    {

        public string NUM { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public string S_SYSID { set; get; }

        /// <summary>
        /// 用户
        /// </summary>
        public USER USER { set; get; }

        /// <summary>
        /// 号码
        /// </summary>
        public string S_TELEPHONE { set; get; }


        /// <summary>
        /// 日期
        /// </summary>
        public string S_SENDDATE { set; get; }


        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_SYSID { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        public string S_MESSAGE { set; get; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public string S_ISDEL { set; get; }

        /// <summary>
        /// 返回值
        /// </summary>
        public string S_COMMIT { set; get; }

        /// <summary>
        /// 发送标志
        /// </summary>
        public string S_FLAG { set; get; }

    }
}
