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
        /// 生产数量
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONQUANTITY")]
        public decimal? ProductionQuantity { get; set; }
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