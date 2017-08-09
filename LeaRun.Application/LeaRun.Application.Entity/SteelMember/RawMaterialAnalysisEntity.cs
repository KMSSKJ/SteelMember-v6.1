using System;
using LeaRun.Application.Code;
using System.Collections.Generic;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 22:03
    /// 描 述：原材料分析
    /// </summary>
    public class RawMaterialAnalysisEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// RawMaterialDosage
        /// </summary>
        /// <returns></returns>
        public int RawMaterialDosage { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// IsSubmitReview
        /// </summary>
        public int IsSubmitReview { get; set; }
        /// <summary>
        /// IsPassed
        /// </summary>
        public int IsPassed { get; set; }
        /// <summary>
        /// Unit
        /// </summary>
        /// <returns></returns>
        public string Unit { get; set; }
        /// <summary>
        /// RawMaterialLibraryEntitys
        /// </summary>
        public RawMaterialLibraryEntity RawMaterialEntitys { get; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}