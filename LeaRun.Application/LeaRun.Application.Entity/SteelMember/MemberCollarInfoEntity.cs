using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-13 23:45
    /// �� �������⹹����Ϣ
    /// </summary>
    public class MemberCollarInfoEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        public string InfoId { get; set; }
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }

        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// MemberWarehouseId
        /// </summary>
        /// <returns></returns>
        public string MemberWarehouseId { get; set; }
        /// <summary>
        /// MemberDemandId
        /// </summary>
        /// <returns></returns>
        public string MemberOrderInfoId { get; set; }
        /// <summary>
        /// CollarQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? CollarQuantity { get; set; }

        ///// <summary>
        ///// CollaredQuantity
        ///// </summary>
        ///// <returns></returns>
        //public decimal? CollaredQuantity { get; set; }
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
            this.InfoId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.InfoId = keyValue;
                                            }
        #endregion
    }
}