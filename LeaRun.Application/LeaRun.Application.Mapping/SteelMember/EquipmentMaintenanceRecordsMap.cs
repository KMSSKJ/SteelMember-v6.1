using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-09-27 09:17
    /// 描 述：生产设备
    /// </summary>
    public class EquipmentMaintenanceRecordsMap : EntityTypeConfiguration<EquipmentMaintenanceRecordsEntity>
    {
        public EquipmentMaintenanceRecordsMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_EquipmentMaintenanceRecords");
            //主键
            this.HasKey(t => t.InfoId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
