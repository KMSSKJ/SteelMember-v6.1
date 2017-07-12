using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberProductionOrderModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public int InfoId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单主键")]
        public string OrderId { get; set; }
        /// <summary>
        /// 构件主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("构件主键")]
        public string MemberId { get; set; }
        /// <summary>
        /// 需求主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("需求主键")]
        public string ProjectDemandId { get; set; }
        /// <summary>
        /// 构件编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("构件编号")]
        public string MemberNumbering { get; set; }
        /// <summary>
        /// 构件名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("构件名称")]
        public string MemberName { get; set; }
        /// <summary>
        /// 构件型号
        /// </summary>
        /// <returns></returns>
        [DisplayName("构件型号")]
        public string MemberModel { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [DisplayName("单位")]
        public string MemberUnit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("数量")]
        public string ProductionQuantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        [DisplayName("单价")]
        public string Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        [DisplayName("金额")]
        public string PriceAmount { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        /// <returns></returns>
        [DisplayName("说明")]
        public string Description { get; set; }
    }
}