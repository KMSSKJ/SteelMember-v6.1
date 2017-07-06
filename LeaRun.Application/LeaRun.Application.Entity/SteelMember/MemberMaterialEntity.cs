using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 09:49
    /// 描 述：构件原材料
    /// </summary>
    public class MemberMaterialEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// MemberMaterialId
        /// </summary>
        /// <returns></returns>
        public int? MemberMaterialId { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public int? RawMaterialId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// TreeId
        /// </summary>
        /// <returns></returns>
        public string TreeId { get; set; }
        /// <summary>
        /// RawMaterialModel
        /// </summary>
        /// <returns></returns>
        public string RawMaterialModel { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// RawMaterialNumber
        /// </summary>
        /// <returns></returns>
        public int? RawMaterialNumber { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.MemberMaterialId = Guid.NewGuid().ToString();
        //                                    }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        //public override void Modify(string keyValue)
        //{
        //    this.MemberMaterialId = keyValue;
        //                                    }
        #endregion
    }
}