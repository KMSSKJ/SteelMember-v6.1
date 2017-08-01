using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-28 11:34
    /// �� �����������
    /// </summary>
    public class MemberWarehouseEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// MemberWarehouseId
        /// </summary>
        /// <returns></returns>
        public string MemberWarehouseId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// DeleteFlag
        /// </summary>
        /// <returns></returns>
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// SubProjectId
        /// </summary>
        /// <returns></returns>
        public string SubProjectId { get; set; }
        /// <summary>
        /// InStock
        /// </summary>
        /// <returns></returns>
        public int? InStock { get; set; }
        /// <summary>
        /// Damage
        /// </summary>
        /// <returns></returns>
        public int? Damage { get; set; }
        /// <summary>
        /// Class
        /// </summary>
        /// <returns></returns>
        public string Class { get; set; }
        /// <summary>
        /// IsShiped
        /// </summary>
        /// <returns></returns>
        public int? IsShiped { get; set; }
        /// <summary>
        /// ModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Librarian
        /// </summary>
        /// <returns></returns>
        public string Librarian { get; set; }
        /// <summary>
        /// Leader
        /// </summary>
        /// <returns></returns>
        public string Leader { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// MemberTreeId
        /// </summary>
        /// <returns></returns>
        public string MemberTreeId { get; set; }
        /// <summary>
        /// ProjectDemandId
        /// </summary>
        /// <returns></returns>
        public int? ProjectDemandId { get; set; }
        /// <summary>
        /// MemberModel
        /// </summary>
        /// <returns></returns>
        public string MemberModel { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.MemberWarehouseId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberWarehouseId = keyValue;
                                            }
        #endregion
    }
}