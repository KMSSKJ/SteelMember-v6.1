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

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.SystemConfigurationId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.SystemConfigurationId = keyValue;
        }
        #endregion
    }
}