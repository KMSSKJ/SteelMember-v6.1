using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 09:51
    /// 描 述：构件制程
    /// </summary>
    public class MemberProcessEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// MemberProcessId
        /// </summary>
        /// <returns></returns>
        public string MemberProcessId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// ProcessName
        /// </summary>
        /// <returns></returns>
        public string ProcessName { get; set; }
        /// <summary>
        /// OperationTime
        /// </summary>
        /// <returns></returns>
        public string OperationTime { get; set; }
        /// <summary>
        /// ProcessRequirements
        /// </summary>
        /// <returns></returns>
        public string ProcessRequirements { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// SortCode
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ProcessMan
        /// </summary>
        /// <returns></returns>
        public string ProcessMan { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.MemberProcessId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberProcessId = keyValue;
        }
        #endregion
    }
}