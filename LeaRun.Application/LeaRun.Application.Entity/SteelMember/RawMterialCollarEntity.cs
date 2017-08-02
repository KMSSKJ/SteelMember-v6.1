using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public class RawMterialCollarEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }
        /// <summary>
        /// InventoryId
        /// </summary>
        /// <returns></returns>
        public string InventoryId { get; set; }
        /// <summary>
        /// CollarType
        /// </summary>
        /// <returns></returns>
        public int? CollarType { get; set; }
        /// <summary>
        /// CollarEngineering
        /// </summary>
        /// <returns></returns>
        public string CollarEngineering { get; set; }
        /// <summary>
        /// CollarTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CollarTime { get; set; }
        /// <summary>
        /// CollarQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? CollarQuantity { get; set; }
        /// <summary>
        /// CollarMan
        /// </summary>
        /// <returns></returns>
        public string CollarMan { get; set; }
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
            this.CollarId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CollarId = keyValue;
                                            }
        #endregion
    }
}