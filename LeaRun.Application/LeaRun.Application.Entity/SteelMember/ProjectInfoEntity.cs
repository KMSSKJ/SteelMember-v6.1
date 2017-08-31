using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 11:18
    /// �� ������Ŀ��Ϣ
    /// </summary>
    public class ProjectInfoEntity : BaseEntity
    {
        #region ʵ���Ա
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
        /// ��Ŀ��ַ
        /// </summary>
        /// <returns></returns>
        public string ProjectAddress { get; set; }
      
        /// <summary>
        /// �����ģ
        /// </summary>
        /// <returns></returns>
        public string ConstructionScale { get; set; }
        /// <summary>
        /// ���幤������
        /// </summary>
        /// <returns></returns>
        public string SingleEngineeringQuantity { get; set; }
        /// <summary>
        /// �õ����
        /// </summary>
        /// <returns></returns>
        public string LandArea { get; set; }
        /// <summary>
        /// �ṹ����
        /// </summary>
        /// <returns></returns>
        public string StructureType { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CompletedTime { get; set; }

        /// <summary>
        /// ҵ����λ
        /// </summary>
        /// <returns></returns>
        public string OwnerUnit { get; set; }
        /// <summary>
        /// ҵ��������
        /// </summary>
        /// <returns></returns>
        public string OwnerPrincipal { get; set; }
        /// <summary>
        ///  ҵ�������˵绰
        /// </summary>
        /// <returns></returns>
        public string OwnerPrincipalTEL { get; set; }

        /// <summary>
        /// ���ⵥλ
        /// </summary>
        /// <returns></returns>
        public string SurveyUnit { get; set; }
        /// <summary>
        /// ���⸺����
        /// </summary>
        /// <returns></returns>
        public string SurveyPrincipal { get; set; }
        /// <summary>
        ///  ���⸺���˵绰
        /// </summary>
        /// <returns></returns>
        public string SurveyPrincipalTEL { get; set; }

        /// <summary>
        /// ʩ����λ
        /// </summary>
        /// <returns></returns>
        public string ConstructionUnit { get; set; }
        /// <summary>
        /// ʩ��������
        /// </summary>
        /// <returns></returns>
        public string ConstructionPrincipal { get; set; }
        /// <summary>
        /// ʩ����������ϵ�绰
        /// </summary>
        /// <returns></returns>
        public string ConstructionPrincipalTEL { get; set; }
        /// <summary>
        /// ��Ƶ�λ
        /// </summary>
        /// <returns></returns>
        public string DesignUnit { get; set; }
        /// <summary>
        /// ��Ƹ�����
        /// </summary>
        /// <returns></returns>
        public string DesignPrincipal { get; set; }
        /// <summary>
        /// ��Ƹ����˵绰
        /// </summary>
        /// <returns></returns>
        public string DesignPrincipalTEL { get; set; }
        /// <summary>
        /// ����λ
        /// </summary>
        /// <returns></returns>
        public string SupervisionUnit { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string SupervisionPrincipal { get; set; }
        /// <summary>
        ///  �������˵绰
        /// </summary>
        /// <returns></returns>
        public string SupervisionPrincipalTEL { get; set; }
        /// <summary>
        /// �ܳа���
        /// </summary>
        /// <returns></returns>
        public string GeneralContractor { get; set; }
        /// <summary>
        /// �ܳа��̸�����
        /// </summary>
        /// <returns></returns>
        public string GeneralContractorPrincipal { get; set; }
        /// <summary>
        /// �ܳа��̸����˵绰
        /// </summary>
        /// <returns></returns>
        public string GeneralContractorPrincipalTEL { get; set; }
        /// <summary>
        /// �ְ���
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractor { get; set; }
        /// <summary>
        /// �ְ��̸�����
        /// </summary>
        /// <returns></returns>
        public string ProfessionalContractorPrincipal { get; set; }
        /// <summary>
        /// �ְ��̸����˵绰
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

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ProjectInfoId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ProjectInfoId = keyValue;
        }
        #endregion
    }
}