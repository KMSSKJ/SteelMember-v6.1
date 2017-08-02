using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-08 11:58
    /// 描 述：原材料采购管理
    /// </summary>
    public class RawMaterialPurchaseInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("INFOID")]
        public string InfoId { get; set; }
        /// <summary>
        /// 采购单据
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALPURCHASEID")]
        public string RawMaterialPurchaseId { get; set; }
        /// <summary>
        /// 分析主键
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALANALYSISID")]
        public string RawMaterialAnalysisId { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        /// <returns></returns>
        [Column("PURCHASEQUANTITY")]
        public decimal? PurchaseQuantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }
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