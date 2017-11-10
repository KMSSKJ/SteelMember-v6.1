using System;
using LeaRun.Application.Code;
using System.Collections.Generic;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-06-30 22:01
    /// �� �����������Ϣ
    /// </summary>
    public class SubProjectEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// PrincipalId
        /// </summary>
        public string PrincipalId { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Levels
        /// </summary>
        /// <returns></returns>
        public int? Levels { get; set; }
        /// <summary>
        /// FullName
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
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
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}