using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-13 22:58
    /// �� ������������
    /// </summary>
    public class MemberCollarEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }
        /// <summary>
        /// CollarEngineering
        /// </summary>
        /// <returns></returns>
        public string CollarEngineering { get; set; }
        /// <summary>
        /// ShippingAddress
        /// </summary>
        /// <returns></returns>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        /// <returns></returns>
        public DateTime Date { get; set; }
        /// <summary>
        /// CollarNumbering
        /// </summary>
        /// <returns></returns>
        public string CollarNumbering { get; set; }

        /// <summary>
        /// Numbering
        /// </summary>
        /// <returns></returns>
        public string Numbering { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// DepartmentId
        /// </summary>
        /// <returns></returns>
        public string DepartmentId { get; set; }
        /// <summary>
        /// ContactPerson
        /// </summary>
        /// <returns></returns>
        public string ContactPerson { get; set; }
        /// <summary>
        /// ContactPersonTel
        /// </summary>
        /// <returns></returns>
        public string ContactPersonTel { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>
        /// <returns></returns>
        public string CreateMan { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        /// <returns></returns>
        public string ReviewMan { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CollarId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CollarId = keyValue;
                                            }
        #endregion
    }
}