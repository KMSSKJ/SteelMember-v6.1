using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberDemandModel
    {
        public string Icon { get; set; }
        public string Category { get; set; }
        public string CategoryId { get; set; }
        public string MemberId { get; set; }
        public decimal? CollarNumber { get; set; }
        public string CreateMan { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Description { get; set; }
        public string SubProject { get; set; }
        public string SubProjectId { get; set; }
        public int? IsSubmit { get; set; }
        public int? IsReview { get; set; }
        public string MemberDemandId { get; set; }
        public string MemberName { get; set; }
        public decimal? MemberNumber { get; set; }
        public string MemberNumbering { get; set; }
        public decimal? ProductionNumber { get; set; }
        public decimal? ChangeQuantity { get; set; }
        public decimal? WarehousedQuantity { get; set; }
        public string ReviewMan { get; set; }
        public string UnitId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? MemberWeight { get; set; }
        public string CADDrawing { get; set; }
        public string ModelDrawing { get; set; }
        public string MemberWarehouseId { get; set; }
        #region 订单详情
        /// <summary>
        /// 订单详情主键
        /// </summary>
        public string InfoId { get; set; }
        /// <summary>
        /// 已生产量
        /// </summary>
        public decimal? ProductionedQuantity { get; set; }
        public decimal? SelfDetectNumber { get; set; }
        /// <summary>
        /// 自检备注
        /// </summary>
        /// <returns></returns>
        public string SelfDetectRemarks { get; set; }
        /// <summary>
        /// 监理质检合格量
        /// </summary>
        /// <returns></returns>
        public decimal? QualityInspectionNumber { get; set; }
        /// <summary>
        /// 已合格量
        /// </summary>
        /// <returns></returns>
        public decimal? QualifiedQuantity { get; set; }
        /// <summary> 
        /// 监理质检备注
        /// </summary>
        /// <returns></returns>
        public string QualityInspectionRemarks { get; set; }
        #endregion
    }
}