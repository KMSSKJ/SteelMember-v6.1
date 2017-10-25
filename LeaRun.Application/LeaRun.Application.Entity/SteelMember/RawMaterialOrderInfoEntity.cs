using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-07 17:35
    /// 描 述：订单详情
    /// </summary>
    public class RawMaterialOrderInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        public string InfoId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// ProductionQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        /// <returns></returns>
        public DateTime? Time { get; set; }
        /// <summary>
        /// RawMaterialManufacturer
        /// </summary>
        /// <returns></returns>
        public string RawMaterialManufacturer { get; set; }
        /// <summary>
        /// RawMaterialSupplier
        /// </summary>
        /// <returns></returns>
        public string RawMaterialSupplier { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }

        /// <summary>
        /// RawMaterialAnalysisId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialAnalysisId { get; set; }
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