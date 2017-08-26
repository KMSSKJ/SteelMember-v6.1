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
    public class MemberProductionOrderInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        [Column("INFOID")]
        public string InfoId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        [Column("MEMBERID")]
        public string MemberId { get; set; }
        /// <summary>
        /// 生产数量
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONQUANTITY")]
        public int? ProductionQuantity { get; set; }
        /// <summary>
        /// 已生产数量
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONEDQUANTITY")]
        public int? ProductionedQuantity { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 自检合格量
        /// </summary>
        /// <returns></returns>
        [Column("SELFDETECTNUMBER")]
        public int? SelfDetectNumber { get; set; }
        /// <summary>
        /// 自检备注
        /// </summary>
        /// <returns></returns>
        [Column("SELFDETECTREMARKS")]
        public string SelfDetectRemarks { get; set; }
        /// <summary>
        /// 监理质检合格量
        /// </summary>
        /// <returns></returns>
        [Column("QUALITYINSPECTIONNUMBER")]
        public int? QualityInspectionNumber { get; set; }

        /// <summary>
        /// 监理质检备注
        /// </summary>
        /// <returns></returns>
        [Column("QUALITYINSPECTIONREMARKS")]
        public string QualityInspectionRemarks { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.InfoId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.InfoId = keyValue;
                                            }
        #endregion
    }
}