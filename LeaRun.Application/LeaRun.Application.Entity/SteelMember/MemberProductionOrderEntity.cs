using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-11 10:12
    /// 描 述：构件生产订单
    /// </summary>
    public class MemberProductionOrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ORDERNUMBERING")]
        public string OrderNumbering { get; set; }
        /// <summary>
        /// 工程
        /// </summary>
        /// <returns></returns>
        [Column("CATEGORY")]
        public string Category { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        /// <returns></returns>
        [Column("CREATEMAN")]
        public string CreateMan { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        /// <returns></returns>
        [Column("PRIORITY")]
        public int? Priority { get; set; }
        /// <summary>
        /// 是否专用
        /// </summary>
        /// <returns></returns>
        [Column("ISDDDICATED")]
        public int? IsDddicated { get; set; }
        /// <summary>
        /// 提交状态
        /// </summary>
        /// <returns></returns>
        [Column("ISSUBMIT")]
        public int? IsSubmit { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <returns></returns>
        [Column("ISPASSED")]
        public int? IsPassed { get; set; }
        /// <summary>
        /// 领取状态
        /// </summary>
        /// <returns></returns>
        [Column("ISRECEIVE")]
        public int? IsReceive { get; set; }
        /// <summary>
        /// 材料领取状态
        /// </summary>
        /// <returns></returns>
        [Column("ISRECEIVERAWMATERIAL")]
        public int? IsReceiveRawMaterial { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        /// <returns></returns>
        [Column("ORDERSTATUS")]
        public int? OrderStatus { get; set; }
        /// <summary>
        /// ProductionStatus
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONSTATUS")]
        public int? ProductionStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN")]
        public string ReviewMan { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWTIME")]
        public DateTime? ReviewTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.OrderId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
                                            }
        #endregion
    }
}