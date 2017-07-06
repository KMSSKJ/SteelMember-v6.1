using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 11:18
    /// 描 述：项目信息
    /// </summary>
    public class ProjectInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ProjectId
        /// </summary>
        /// <returns></returns>
        public int? ProjectId { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public int? ParentId { get; set; }
        /// <summary>
        /// TreeID
        /// </summary>
        /// <returns></returns>
        public int? TreeID { get; set; }
        /// <summary>
        /// ProjectName
        /// </summary>
        /// <returns></returns>
        public string ProjectName { get; set; }
        /// <summary>
        /// ProjectSystemTitel
        /// </summary>
        /// <returns></returns>
        public string ProjectSystemTitel { get; set; }
        /// <summary>
        /// ProjectLogo
        /// </summary>
        /// <returns></returns>
        public string ProjectLogo { get; set; }
        /// <summary>
        /// ProjectBackground
        /// </summary>
        /// <returns></returns>
        public string ProjectBackground { get; set; }
        /// <summary>
        /// State
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// ProjectAddress
        /// </summary>
        /// <returns></returns>
        public string ProjectAddress { get; set; }
        /// <summary>
        /// ConstructionPermitId
        /// </summary>
        /// <returns></returns>
        public int? ConstructionPermitId { get; set; }
        /// <summary>
        /// ConstructionUnit
        /// </summary>
        /// <returns></returns>
        public string ConstructionUnit { get; set; }
        /// <summary>
        /// ConstructionPrincipal
        /// </summary>
        /// <returns></returns>
        public string ConstructionPrincipal { get; set; }
        /// <summary>
        /// ConstructionPrincipalTEL
        /// </summary>
        /// <returns></returns>
        public string ConstructionPrincipalTEL { get; set; }
        /// <summary>
        /// DesignUnit
        /// </summary>
        /// <returns></returns>
        public string DesignUnit { get; set; }
        /// <summary>
        /// DesignPrincipal
        /// </summary>
        /// <returns></returns>
        public string DesignPrincipal { get; set; }
        /// <summary>
        /// DesignPrincipalTEL
        /// </summary>
        /// <returns></returns>
        public string DesignPrincipalTEL { get; set; }
        /// <summary>
        /// SupervisionUnit
        /// </summary>
        /// <returns></returns>
        public string SupervisionUnit { get; set; }
        /// <summary>
        /// SupervisionPrincipal
        /// </summary>
        /// <returns></returns>
        public string SupervisionPrincipal { get; set; }
        /// <summary>
        /// SupervisionPrincipalTEL
        /// </summary>
        /// <returns></returns>
        public string SupervisionPrincipalTEL { get; set; }
        /// <summary>
        /// GeneralContractor
        /// </summary>
        /// <returns></returns>
        public string GeneralContractor { get; set; }
        /// <summary>
        /// GeneralContractorPrincipal
        /// </summary>
        /// <returns></returns>
        public string GeneralContractorPrincipal { get; set; }
        /// <summary>
        /// GeneralContractorPrincipalTEL
        /// </summary>
        /// <returns></returns>
        public string GeneralContractorPrincipalTEL { get; set; }
        /// <summary>
        /// ProfessionalContractor
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractor { get; set; }
        /// <summary>
        /// ProfessionalContractorPrincipal
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractorPrincipal { get; set; }
        /// <summary>
        /// ProfessionalContractorPrincipalTEL
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractorPrincipalTEL { get; set; }
        /// <summary>
        /// DeleteFlag
        /// </summary>
        /// <returns></returns>
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// ModifiedTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifiedTime { get; set; }
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
        //public override void Create()
        //{
        //    this.ProjectId = Guid.NewGuid().ToString();
        //                                    }
        ///// <summary>
        ///// 编辑调用
        ///// </summary>
        ///// <param name="keyValue"></param>
        //public override void Modify(string keyValue)
        //{
        //    this.ProjectId = keyValue;
        //                                    }
        #endregion
    }
}