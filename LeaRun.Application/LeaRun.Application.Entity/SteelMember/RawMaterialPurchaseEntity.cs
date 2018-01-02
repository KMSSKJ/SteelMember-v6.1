using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-08 11:58
    /// 描 述：材料申请管理
    /// </summary>
    public class RawMaterialPurchaseEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 申请单据
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALPURCHASEID")]
        public string RawMaterialPurchaseId { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        /// <returns></returns>
        [Column("PURCHASENUMBERING")]
        public string PurchaseNumbering { get; set; }
       
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        [Column("ISSUBMIT")]
        public int? IsSubmit { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        [Column("ISPASSED")]
        public int? IsPassed { get; set; }
       
        /// <summary>
        /// 入库
        /// </summary>
        [Column("ISWAREHOUSING")]
        public int? IsWarehousing { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        /// <returns></returns>
        [Column("CREATEMAN")]
        public string CreateMan { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN1")]
        public string ReviewMan1 { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN2")]
        public string ReviewMan2 { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN3")]
        public string ReviewMan3 { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN4")]
        public string ReviewMan4 { get; set; }


        /// <summary>
        /// ReviewDescription
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWDESCRIPTION")]
        public string ReviewDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        ///// <summary>
        /////组织ID
        ///// </summary>
        ///// <returns></returns>
        //[Column("ORGANIZEID")]
        //public string OrganizeId { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.RawMaterialPurchaseId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.RawMaterialPurchaseId = keyValue;
                                            }
        #endregion
    }
}