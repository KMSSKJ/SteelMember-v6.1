using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public class RawMterialCollarEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }

        /// <summary>
        /// 工程
        /// </summary>
        public string CollarEngineering { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// 领用单号
        /// </summary>
        public string CollarNumbering { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public string Numbering { get; set; }
        /// <summary>
        /// 组织
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 领用类型
        /// </summary>
        /// <returns></returns>
        public int? CollarType { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPersonTel { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateMan { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string ReviewMan { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CollarId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CollarId = keyValue;
                                            }
        #endregion
    }
}