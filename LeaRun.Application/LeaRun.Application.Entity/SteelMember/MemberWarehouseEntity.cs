using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-28 11:34
    /// 描 述：构件库存
    /// </summary>
    public class MemberWarehouseEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// MemberWarehouseId
        /// </summary>
        /// <returns></returns>
        public string MemberWarehouseId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// EngineeringId
        /// </summary>
        public string EngineeringId { get; set; }
        /// <summary>
        /// InStock
        /// </summary>
        /// <returns></returns>
        public int? InStock { get; set; }
        /// <summary>
        /// ModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Librarian
        /// </summary>
        /// <returns></returns>
        public string Librarian { get; set; }
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
            this.MemberWarehouseId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberWarehouseId = keyValue;
                                            }
        #endregion
    }
}