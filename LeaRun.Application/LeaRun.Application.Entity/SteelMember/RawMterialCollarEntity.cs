using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-26 17:19
    /// �� �������ù���
    /// </summary>
    public class RawMterialCollarEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string CollarEngineering { get; set; }
        /// <summary>
        /// �ջ���ַ
        /// </summary>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// ���õ���
        /// </summary>
        public string CollarNumbering { get; set; }
        /// <summary>
        /// ���뵥��
        /// </summary>
        public string Numbering { get; set; }
        /// <summary>
        /// ��֯
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int? CollarType { get; set; }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        public string ContactPersonTel { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string CreateMan { get; set; }
        /// <summary>
        /// ������
        /// </summary>
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