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
        ///// <summary>
        ///// ProjectId
        ///// </summary>
        ///// <returns></returns>
        //public string ProjectId { get; set; }
        /// <summary>
        /// ProjectName
        /// </summary>
        /// <returns></returns>
        public string ProjectName { get; set; }
   
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
        /// ҵ����λ
        /// </summary>
        /// <returns></returns>
        public string OwnerUnit { get; set; }
       
        /// <summary>
        /// ʩ����λ
        /// </summary>
        /// <returns></returns>
        public string ConstructionUnit { get; set; }
      
        /// <summary>
        /// ��Ƶ�λ
        /// </summary>
        /// <returns></returns>
        public string DesignUnit { get; set; }
      
        /// <summary>
        /// ����λ
        /// </summary>
        /// <returns></returns>
        public string SupervisionUnit { get; set; }

        /// <summary>
        /// ���̸ſ�
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// ModifiedTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CompletedTime { get; set; }
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