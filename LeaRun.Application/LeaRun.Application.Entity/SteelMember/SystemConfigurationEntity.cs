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

        public string SystemLogo { get; set; }
        public string SystemName { get; set; }
        public string ConstructUnit { get; set; }
        public string Cu_principal { get; set; }
        public string InvestigationUnit { get; set; }
        public string Iu_principal { get; set; }
        public string DesignUnit { get; set; }
        public string Du_principal { get; set; }
        public string ConstructionUnit { get; set; }
        public string Ctu_principal { get; set; }
        public string SupervisionUnit { get; set; }
        public string Su_principal { get; set; }
        public string EngineeringName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedDuration { get; set; }
        public DateTime UploadDate { get; set; }
        public string EngineeringImg { get; set; }
        public string EngineeringOverview { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.SystemConfigurationId = Guid.NewGuid().ToString();
            this.UploadDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.SystemConfigurationId = keyValue;
            this.UploadDate = DateTime.Now;
        }
        #endregion
    }
}