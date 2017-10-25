using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-09-27 09:17
    /// 描 述：生产设备
    /// </summary>
    public class EquipmentMaintenanceRecordsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        [Column("INFOID")]
        public string InfoId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 维护项目
        /// </summary>
        /// <returns></returns>
        [Column("MAINTENANCECONTENT")]
        public string MaintenanceContent { get; set; }
        /// <summary>
        /// 故障原因
        /// </summary>
        /// <returns></returns>
        [Column("MALFUNCTIONREASON")]
        public string MalfunctionReason { get; set; }
        /// <summary>
        /// 维护时间
        /// </summary>
        /// <returns></returns>
        [Column("MAINTENANCEDATE")]
        public DateTime? MaintenanceDate { get; set; }
        /// <summary>
        /// 解决方法
        /// </summary>
        /// <returns></returns>
        [Column("SOLVEMETHOD")]
        public string SolveMethod { get; set; }
        /// <summary>
        /// 维护人
        /// </summary>
        /// <returns></returns>
        [Column("MAINTENANCEPEOPLE")]
        public string MaintenancePeople { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.InfoId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.InfoId = keyValue;
                                            }
        #endregion
    }
}