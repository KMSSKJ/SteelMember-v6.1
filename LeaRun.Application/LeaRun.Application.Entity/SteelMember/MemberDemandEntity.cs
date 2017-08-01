using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-26 16:54
    /// 描 述：构件需求
    /// </summary>
    public class MemberDemandEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// MemberDemandId
        /// </summary>
        /// <returns></returns>
        public string MemberDemandId { get; set; }
        /// <summary>
        /// TreeId
        /// </summary>
        /// <returns></returns>
        public string SubProjectId { get; set; }
        /// <summary>
        /// TreeName
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
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
        public string MemberName { get; set; }
        /// <summary>
        /// MemberClassId
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// UnitId
        /// </summary>
        /// <returns></returns>
        public string MemberUnit { get; set; }
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
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        /// <returns></returns>
        public string Icon { get; set; }
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

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.MemberDemandId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberDemandId = keyValue;
                                            }
        #endregion
    }
}