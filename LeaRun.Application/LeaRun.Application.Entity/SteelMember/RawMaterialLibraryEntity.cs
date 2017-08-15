using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 10:42
    /// 描 述：原材料管理
    /// </summary>
    public class RawMaterialLibraryEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>		
        public string Category { get; set; }

        /// <summary>
        /// RawMaterialName
        /// </summary>
        /// <returns></returns>
        public string RawMaterialName { get; set; }
        /// <summary>
        /// RawMaterialStandard
        /// </summary>
        /// <returns></returns>
        public string RawMaterialStandard { get; set; }
        /// <summary>
        /// UnitId
        /// </summary>
        /// <returns></returns>
        public string Unit { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// RawMaterialModel
        /// </summary>
        /// <returns></returns>
        public string RawMaterialModel { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// DeleteFlag
        /// </summary>
        /// <returns></returns>
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// Sort
        /// </summary>
        /// <returns></returns>
        public int? Sort { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.RawMaterialId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.RawMaterialId = keyValue;
        }
        #endregion
    }

}