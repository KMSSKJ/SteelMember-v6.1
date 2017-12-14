using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-12-12 09:00
    /// 描 述：设备易损件
    /// </summary>
    public class EquipmentPreWearPartsMap : EntityTypeConfiguration<EquipmentPreWearPartsEntity>
    {
        public EquipmentPreWearPartsMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_EquipmentPreWearParts");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
