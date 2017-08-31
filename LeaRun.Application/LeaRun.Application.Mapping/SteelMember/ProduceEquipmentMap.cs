using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-29 15:00
    /// 描 述：生产设备
    /// </summary>
    public class ProduceEquipmentMap : EntityTypeConfiguration<ProduceEquipmentEntity>
    {
        public ProduceEquipmentMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_ProduceEquipment");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
