using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 09:51
    /// �� ���������Ƴ�
    /// </summary>
    public class MemberProcessEntity : BaseEntity
    {
        #region ʵ���Ա
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

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.MemberProcessId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberProcessId = keyValue;
        }
        #endregion
    }
}