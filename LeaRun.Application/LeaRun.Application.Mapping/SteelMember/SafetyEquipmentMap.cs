using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-29 15:39
    /// 描 述：安全设备
    /// </summary>
    public class SafetyEquipmentMap : EntityTypeConfiguration<SafetyEquipmentEntity>
    {
        public SafetyEquipmentMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_SafetyEquipment");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
