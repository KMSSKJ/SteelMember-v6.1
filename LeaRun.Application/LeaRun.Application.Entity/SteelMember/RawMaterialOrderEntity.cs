using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-07 17:28
    /// 描 述：材料订单
    /// </summary>
    public class RawMaterialOrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// OrderNumbering
        /// </summary>
        /// <returns></returns>
        public string OrderNumbering { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>
        /// <returns></returns>
        public string CreateMan { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        /// <returns></returns>
        public int? Priority { get; set; }
        /// <summary>
        /// IsDedicated
        /// </summary>
        /// <returns></returns>
        public int? IsDedicated { get; set; }
        /// <summary>
        /// IsSubmit
        /// </summary>
        /// <returns></returns>
        public int? IsSubmit { get; set; }
        /// <summary>
        /// IsPassed
        /// </summary>
        /// <returns></returns>
        public int? IsPassed { get; set; }
        /// <summary>
        /// OrderWarehousingStatus
        /// </summary>
        /// <returns></returns>
        public int? OrderWarehousingStatus { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan { get; set; }
        /// <summary>
        /// ReviewTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ReviewTime { get; set; }
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