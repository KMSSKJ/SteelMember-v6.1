using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-26 16:54
    /// �� ������������
    /// </summary>
    public class MemberDemandEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// MemberDemandId
        /// </summary>
        /// <returns></returns>
        public string MemberDemandId { get; set; }
        /// <summary>
        /// SubProjectId
        /// </summary>
        /// <returns></returns>
        public string SubProjectId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
       
        /// <summary>
        /// IsReview
        /// </summary>
        /// <returns></returns>
        public int? IsReview { get; set; }
        /// <summary>
        /// ReviewMan
        /// </summary>
        /// <returns></returns>
        public string ReviewMan { get; set; }
        /// <summary>
        /// IsDemandSubmit
        /// </summary>
        /// <returns></returns>
        public int? IsDemandSubmit { get; set; }
        /// <summary>
        /// IsReview
        /// </summary>
        /// <returns></returns>
        public int? IsSubmit { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// MemberNumber
        /// </summary>
        /// <returns></returns>
        public decimal? MemberNumber { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ModifiedTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ReviewTime { get; set; }
        /// <summary>
        /// CollarNumbered
        /// </summary>
        /// <returns></returns>
        public decimal? CollarNumber { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>
        /// <returns></returns>
        public string CreateMan { get; set; }
        /// <summary>
        /// ProductionNumber
        /// </summary>
        /// <returns></returns>
        public decimal? ProductionNumber { get; set; }
        /// <summary>
        /// OrderedQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? OrderedQuantity { get; set; }
        /// <summary>
        /// CollaredNumber
        /// </summary>
        /// <returns></returns>
        public decimal? CollaredNumber { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.MemberDemandId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberDemandId = keyValue;
                                            }
        #endregion
    }
}