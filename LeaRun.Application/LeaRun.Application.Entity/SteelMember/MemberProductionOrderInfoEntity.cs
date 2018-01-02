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
        /// MEMBERDEMANDID
        /// </summary>
        /// <returns></returns>
        [Column("MEMBERDEMANDID")]
        public string MemberDemandId { get; set; }
        /// <summary>
        /// 生产数量
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONQUANTITY")]
        public Decimal? ProductionQuantity { get; set; }
   
        /// <summary>
        /// 已生产数量
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONEDQUANTITY")]
        public Decimal? ProductionedQuantity { get; set; }
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
        public Decimal? SelfDetectNumber { get; set; }
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
        public Decimal? QualityInspectionNumber { get; set; }

        /// <summary>
        /// 监理质检备注
        /// </summary>
        /// <returns></returns>
        [Column("QUALITYINSPECTIONREMARKS")]
        public string QualityInspectionRemarks { get; set; }
        /// <summary>
        /// 已入库量
        /// </summary>
        /// <returns></returns>
        [Column("WAREHOUSEDQUANTITY")]
        public Decimal? WarehousedQuantity { get; set; }
        /// <summary>
        /// 废品量
        /// </summary>
        /// <returns></returns>
        [Column("WASTEQUANTITY")]
        public Decimal? WasteQuantity { get; set; }

        /// <summary>
        /// 已收货量
        /// </summary>
        /// <returns></returns>
        [Column("RECEIVEDQUANTITY")]
        public Decimal? ReceivedQuantity { get; set; }

        /// <summary>
        /// 已出库量
        /// </summary>
        /// <returns></returns>
        [Column("COLLAREDQUANTITY")]
        public Decimal? CollaredQuantity { get; set; }

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