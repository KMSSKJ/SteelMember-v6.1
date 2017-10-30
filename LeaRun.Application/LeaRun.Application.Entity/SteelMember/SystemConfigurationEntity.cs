using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 11:18
    /// �� ������Ŀ��Ϣ
    /// </summary>
    public class SystemConfigurationEntity : BaseEntity
    {
        #region ʵ���Ա
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

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.SystemConfigurationId = Guid.NewGuid().ToString();
            this.UploadDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
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