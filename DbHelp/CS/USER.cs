using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelp.CS
{
    public class USER
    {

        public string NUM { set; get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_SYSID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string U_ID { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string U_TELEPHONE { get; set; }
        
        /// <summary>
        /// 邮件
        /// </summary>
        public string U_MAIL { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string U_PASSWORD { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string U_NAME { get; set; }

        /// <summary>
        /// 免费短信
        /// </summary>
        public string U_FREEMESSAGE { get; set; }

        /// <summary>
        /// 用户余额
        /// </summary>
        public string U_BALANCE { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string U_DATE { get; set; }

        /// <summary>
        /// 是否失效
        /// </summary>
        public string U_ISDEL { get; set; }


        public string U_PROVINCE { get; set; }


        public string U_CITY { get; set; }


        public string U_AREA { get; set; }

        public string U_SHORT { get; set; }
    }
}
