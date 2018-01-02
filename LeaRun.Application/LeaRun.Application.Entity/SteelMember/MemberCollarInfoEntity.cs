using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-09-13 23:45
    /// 描 述：出库构件信息
    /// </summary>
    public class MemberCollarInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        public string InfoId { get; set; }
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }

        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// MemberWarehouseId
        /// </summary>
        /// <returns></returns>
        public string MemberWarehouseId { get; set; }
        /// <summary>
        /// MemberDemandId
        /// </summary>
        /// <returns></returns>
        public string MemberOrderInfoId { get; set; }
        /// <summary>
        /// CollarQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? CollarQuantity { get; set; }

        ///// <summary>
        ///// CollaredQuantity
        ///// </summary>
        ///// <returns></returns>
        //public decimal? CollaredQuantity { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
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