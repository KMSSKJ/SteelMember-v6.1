using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-11 11:05
    /// �� ������������
    /// </summary>
    public class ProjectDemandEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ProjectDemandId
        /// </summary>
        /// <returns></returns>
        public int? ProjectDemandId { get; set; }
        /// <summary>
        /// TreeId
        /// </summary>
        /// <returns></returns>
        public string TreeId { get; set; }
        /// <summary>
        /// TreeName
        /// </summary>
        /// <returns></returns>
        public string TreeName { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// MemberNumbering
        /// </summary>
        /// <returns></returns>
        public string MemberNumbering { get; set; }
        /// <summary>
        /// MemberModel
        /// </summary>
        /// <returns></returns>
        public string MemberModel { get; set; }
        /// <summary>
        /// MemberCompanyId
        /// </summary>
        /// <returns></returns>
        public int? MemberCompanyId { get; set; }
        /// <summary>
        /// MemberClassId
        /// </summary>
        /// <returns></returns>
        public int? MemberClassId { get; set; }
        /// <summary>
        /// UnitId
        /// </summary>
        /// <returns></returns>
        public int? UnitId { get; set; }
        /// <summary>
        /// MemberWeight
        /// </summary>
        /// <returns></returns>
        public string MemberWeight { get; set; }
        /// <summary>
        /// UnitPrice
        /// </summary>
        /// <returns></returns>
        public string UnitPrice { get; set; }
        /// <summary>
        /// CostBudget
        /// </summary>
        /// <returns></returns>
        public decimal? CostBudget { get; set; }
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
        /// IsSubmit
        /// </summary>
        /// <returns></returns>
        public int? IsSubmit { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// DeleteFlag
        /// </summary>
        /// <returns></returns>
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// MemberNumber
        /// </summary>
        /// <returns></returns>
        public int? MemberNumber { get; set; }
        /// <summary>
        /// OrderQuantityed
        /// </summary>
        /// <returns></returns>
        public int? OrderQuantityed { get; set; }
        /// <summary>
        /// Productioned
        /// </summary>
        /// <returns></returns>
        public int? Productioned { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ModifiedTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifiedTime { get; set; }
        /// <summary>
        /// CollarNumbered
        /// </summary>
        /// <returns></returns>
        public int? CollarNumbered { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>
        /// <returns></returns>
        public string CreateMan { get; set; }
        /// <summary>
        /// ProductionNumber
        /// </summary>
        /// <returns></returns>
        public int? ProductionNumber { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ProjectDemandId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ProjectDemandId = keyValue;
                                            }
        #endregion
    }
}