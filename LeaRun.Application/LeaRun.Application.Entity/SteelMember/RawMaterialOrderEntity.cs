using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-08-07 17:28
    /// �� �������϶���
    /// </summary>
    public class RawMaterialOrderEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// OrderNumbering
        /// </summary>
        /// <returns></returns>
        public string OrderNumbering { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>
        /// <returns></returns>
        public string CreateMan { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// IsSubmit
        /// </summary>
        /// <returns></returns>
        public int? IsSubmit { get; set; }
        /// <summary>
        /// IsPassed
        /// </summary>
        /// <returns></returns>
        public int? IsPassed { get; set; }

        /// <summary>
        /// IsReceived
        /// </summary>
        /// <returns></returns>
        public int? IsReceived { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan1 { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan2 { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan3 { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan4 { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan5 { get; set; }

        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// ContactPerson
        /// </summary>
        /// <returns></returns>
        public string ContactPerson { get; set; }
        /// <summary>
        /// ShippingAddress
        /// </summary>
        /// <returns></returns>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// ContactPersonTel
        /// </summary>
        /// <returns></returns>
        public string ContactPersonTel { get; set; }

        /// <summary>
        /// ReviewDescription
        /// </summary>
        /// <returns></returns>
        public string ReviewDescription { get; set; }

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
            this.OrderId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
        }
        #endregion
    }
}