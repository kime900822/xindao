using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelp.CS
{
    public class BORROW
    {

        public string NUM { set; get; }
        /// <summary>
        /// 借款编号
        /// </summary>
        public string B_SYSID { set; get; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_SYSID { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public USER USER { set; get; }

        /// <summary>
        /// 客户名字
        /// </summary>
        public string C_NAME { set; get; }

        /// <summary>
        /// 客户身份证
        /// </summary>
        public string C_ID { set; get; }

        /// <summary>
        /// 客户身份证图片
        /// </summary>
        public byte[] C_PIC { set; get; }

        /// <summary>
        /// 客户联系方式
        /// </summary>
        public string C_CONTACT { set; get; }

        /// <summary>
        /// 客户紧急联系人
        /// </summary>
        public string C_EMERGENCY { set; get; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string C_ADDRESS { set; get; }

        /// <summary>
        /// 客户性别
        /// </summary>
        public string C_SEX { set; get; }

        /// <summary>
        /// 担保人姓名
        /// </summary>
        public string G_NAME1 { set; get; }

        /// <summary>
        /// 担保人身份证号
        /// </summary>
        public string G_ID1 { set; get; }

        /// <summary>
        /// 担保人职业
        /// </summary>
        public string G_JOB1 { set; get; }

        /// <summary>
        /// 担保人姓名
        /// </summary>
        public string G_NAME2 { set; get; }

        /// <summary>
        /// 担保人身份证号
        /// </summary>
        public string G_ID2 { set; get; }

        /// <summary>
        /// 担保人职业
        /// </summary>
        public string G_JOB2 { set; get; }

        /// <summary>
        /// 担保人姓名
        /// </summary>
        public string G_NAME3 { set; get; }

        /// <summary>
        /// 担保人身份证号
        /// </summary>
        public string G_ID3 { set; get; }

        /// <summary>
        /// 担保人职业
        /// </summary>
        public string G_JOB3 { set; get; }

        /// <summary>
        /// 担保人姓名
        /// </summary>
        public string G_NAME4 { set; get; }

        /// <summary>
        /// 担保人身份证号
        /// </summary>
        public string G_ID4 { set; get; }

        /// <summary>
        /// 担保人职业
        /// </summary>
        public string G_JOB4 { set; get; }

        /// <summary>
        /// 借款金额
        /// </summary>
        public string B_AMOUNT { set; get; }

        /// <summary>
        /// 已还金额
        /// </summary>
        public string B_REPAYMENT { set; get; }


        /// <summary>
        /// 利息
        /// </summary>
        public string B_INTEREST { set; get; }

        /// <summary>
        /// 借款方式
        /// </summary>
        public string B_TYPE { set; get; }


        /// <summary>
        /// 还款日期
        /// </summary>
        public string B_REPAYDATE { set; get; }


        /// <summary>
        /// 提醒日期
        /// </summary>
        public string B_REMINDDATE { set; get; }

        /// <summary>
        /// 借款日期
        /// </summary>
        public string B_DATE { set; get; }


        /// <summary>
        /// 借款时间
        /// </summary>
        public string B_DATETMP { set; get; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public string B_ISDEL { set; get; }


        /// <summary>
        /// 期数
        /// </summary>
        public string B_TERM { set; get; }

        /// <summary>
        /// 紧急联系人姓名
        /// </summary>
        public string C_EMERGENCYNAME { set; get; }

        /// <summary>
        /// 还款方式
        /// </summary>
        public string B_REPAYTYPE { set; get; }
    }
}
