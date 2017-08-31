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
        /// ProjectInfoId
        /// </summary>
        /// <returns></returns>
        public string ProjectInfoId { get; set; }
        /// <summary>
        /// ProjectId
        /// </summary>
        /// <returns></returns>
        public string ProjectId { get; set; }
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
        /// 项目地址
        /// </summary>
        /// <returns></returns>
        public string ProjectAddress { get; set; }
      
        /// <summary>
        /// 建设规模
        /// </summary>
        /// <returns></returns>
        public string ConstructionScale { get; set; }
        /// <summary>
        /// 单体工程数量
        /// </summary>
        /// <returns></returns>
        public string SingleEngineeringQuantity { get; set; }
        /// <summary>
        /// 用地面积
        /// </summary>
        /// <returns></returns>
        public string LandArea { get; set; }
        /// <summary>
        /// 结构类型
        /// </summary>
        /// <returns></returns>
        public string StructureType { get; set; }
        /// <summary>
        /// 开工时间
        /// </summary>
        /// <returns></returns>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 竣工时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CompletedTime { get; set; }

        /// <summary>
        /// 业主单位
        /// </summary>
        /// <returns></returns>
        public string OwnerUnit { get; set; }
        /// <summary>
        /// 业主负责人
        /// </summary>
        /// <returns></returns>
        public string OwnerPrincipal { get; set; }
        /// <summary>
        ///  业主负责人电话
        /// </summary>
        /// <returns></returns>
        public string OwnerPrincipalTEL { get; set; }

        /// <summary>
        /// 勘测单位
        /// </summary>
        /// <returns></returns>
        public string SurveyUnit { get; set; }
        /// <summary>
        /// 勘测负责人
        /// </summary>
        /// <returns></returns>
        public string SurveyPrincipal { get; set; }
        /// <summary>
        ///  勘测负责人电话
        /// </summary>
        /// <returns></returns>
        public string SurveyPrincipalTEL { get; set; }

        /// <summary>
        /// 施工单位
        /// </summary>
        /// <returns></returns>
        public string ConstructionUnit { get; set; }
        /// <summary>
        /// 施工负责人
        /// </summary>
        /// <returns></returns>
        public string ConstructionPrincipal { get; set; }
        /// <summary>
        /// 施工负责人联系电话
        /// </summary>
        /// <returns></returns>
        public string ConstructionPrincipalTEL { get; set; }
        /// <summary>
        /// 设计单位
        /// </summary>
        /// <returns></returns>
        public string DesignUnit { get; set; }
        /// <summary>
        /// 设计负责人
        /// </summary>
        /// <returns></returns>
        public string DesignPrincipal { get; set; }
        /// <summary>
        /// 设计负责人电话
        /// </summary>
        /// <returns></returns>
        public string DesignPrincipalTEL { get; set; }
        /// <summary>
        /// 监理单位
        /// </summary>
        /// <returns></returns>
        public string SupervisionUnit { get; set; }
        /// <summary>
        /// 监理负责人
        /// </summary>
        /// <returns></returns>
        public string SupervisionPrincipal { get; set; }
        /// <summary>
        ///  监理负责人电话
        /// </summary>
        /// <returns></returns>
        public string SupervisionPrincipalTEL { get; set; }
        /// <summary>
        /// 总承包商
        /// </summary>
        /// <returns></returns>
        public string GeneralContractor { get; set; }
        /// <summary>
        /// 总承包商负责人
        /// </summary>
        /// <returns></returns>
        public string GeneralContractorPrincipal { get; set; }
        /// <summary>
        /// 总承包商负责人电话
        /// </summary>
        /// <returns></returns>
        public string GeneralContractorPrincipalTEL { get; set; }
        /// <summary>
        /// 分包商
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractor { get; set; }
        /// <summary>
        /// 分包商负责人
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractorPrincipal { get; set; }
        /// <summary>
        /// 分包商负责人电话
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractorPrincipalTEL { get; set; }
      
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
        public override void Create()
        {
            this.ProjectInfoId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ProjectInfoId = keyValue;
        }
        #endregion
    }
}