using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 11:18
    /// 描 述：项目信息
    /// </summary>
    public class SystemConfigurationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// SystemConfigurationId
        /// </summary>
        /// <returns></returns>
        public string SystemConfigurationId { get; set; }

        /// <summary>
        /// SystemName
        /// </summary>
        /// <returns></returns>
        public string SystemName { get; set; }

        /// <summary>
        /// SystemLogo
        /// </summary>
        /// <returns></returns>
        public string SystemLogo { get; set; }

        /// <summary>
        /// ProjectBackground
        /// </summary>
        /// <returns></returns>
        public DateTime UploadDate { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.SystemConfigurationId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.SystemConfigurationId = keyValue;
        }
        #endregion
    }
}