using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 11:18
    /// 描 述：项目信息
    /// </summary>
    public class ProjectInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ProjectInfoId
        /// </summary>
        /// <returns></returns>
        public string ProjectInfoId { get; set; }
        ///// <summary>
        ///// ProjectId
        ///// </summary>
        ///// <returns></returns>
        //public string ProjectId { get; set; }
        /// <summary>
        /// ProjectName
        /// </summary>
        /// <returns></returns>
        public string ProjectName { get; set; }
   
        /// <summary>
        /// ProjectBackground
        /// </summary>
        /// <returns></returns>
        public string ProjectBackground { get; set; }
        /// <summary>
        /// 项目地址
        /// </summary>
        /// <returns></returns>
        public string ProjectAddress { get; set; }

        /// <summary>
        /// 业主单位
        /// </summary>
        /// <returns></returns>
        public string OwnerUnit { get; set; }
       
        /// <summary>
        /// 施工单位
        /// </summary>
        /// <returns></returns>
        public string ConstructionUnit { get; set; }
      
        /// <summary>
        /// 设计单位
        /// </summary>
        /// <returns></returns>
        public string DesignUnit { get; set; }
      
        /// <summary>
        /// 监理单位
        /// </summary>
        /// <returns></returns>
        public string SupervisionUnit { get; set; }

        /// <summary>
        /// 工程概况
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }

        /// <summary>
        /// 开工时间
        /// </summary>
        /// <returns></returns>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// ModifiedTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CompletedTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ProjectInfoId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ProjectInfoId = keyValue;
        }
        #endregion
    }
}