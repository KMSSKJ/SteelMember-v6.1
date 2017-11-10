using System;
using LeaRun.Application.Code;
using System.Collections.Generic;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-06-30 22:01
    /// 描 述：分项工程信息
    /// </summary>
    public class SubProjectEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// PrincipalId
        /// </summary>
        public string PrincipalId { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Levels
        /// </summary>
        /// <returns></returns>
        public int? Levels { get; set; }
        /// <summary>
        /// FullName
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
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