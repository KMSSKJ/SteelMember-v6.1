using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 09:49
    /// �� ��������ԭ����
    /// </summary>
    public class MemberMaterialEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// MemberMaterialId
        /// </summary>
        /// <returns></returns>
        public int? MemberMaterialId { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public int? RawMaterialId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// TreeId
        /// </summary>
        /// <returns></returns>
        public string TreeId { get; set; }
        /// <summary>
        /// RawMaterialModel
        /// </summary>
        /// <returns></returns>
        public string RawMaterialModel { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// RawMaterialNumber
        /// </summary>
        /// <returns></returns>
        public int? RawMaterialNumber { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        //public override void Create()
        //{
        //    this.MemberMaterialId = Guid.NewGuid().ToString();
        //                                    }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        //public override void Modify(string keyValue)
        //{
        //    this.MemberMaterialId = keyValue;
        //                                    }
        #endregion
    }
}