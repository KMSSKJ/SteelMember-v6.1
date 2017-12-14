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

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.SystemLogo”的 XML 注释
        public string SystemLogo { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.SystemLogo”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.SystemName”的 XML 注释
        public string SystemName { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.SystemName”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.ConstructUnit”的 XML 注释
        public string ConstructUnit { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.ConstructUnit”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Cu_principal”的 XML 注释
        public string Cu_principal { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Cu_principal”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.InvestigationUnit”的 XML 注释
        public string InvestigationUnit { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.InvestigationUnit”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Iu_principal”的 XML 注释
        public string Iu_principal { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Iu_principal”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.DesignUnit”的 XML 注释
        public string DesignUnit { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.DesignUnit”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Du_principal”的 XML 注释
        public string Du_principal { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Du_principal”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.ConstructionUnit”的 XML 注释
        public string ConstructionUnit { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.ConstructionUnit”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Ctu_principal”的 XML 注释
        public string Ctu_principal { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Ctu_principal”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.SupervisionUnit”的 XML 注释
        public string SupervisionUnit { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.SupervisionUnit”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Su_principal”的 XML 注释
        public string Su_principal { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.Su_principal”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.EngineeringName”的 XML 注释
        public string EngineeringName { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.EngineeringName”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.StartDate”的 XML 注释
        public DateTime StartDate { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.StartDate”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.ExpectedDuration”的 XML 注释
        public DateTime ExpectedDuration { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.ExpectedDuration”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.UploadDate”的 XML 注释
        public DateTime UploadDate { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.UploadDate”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.EngineeringImg”的 XML 注释
        public string EngineeringImg { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.EngineeringImg”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.EngineeringOverview”的 XML 注释
        public string EngineeringOverview { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SystemConfigurationEntity.EngineeringOverview”的 XML 注释

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