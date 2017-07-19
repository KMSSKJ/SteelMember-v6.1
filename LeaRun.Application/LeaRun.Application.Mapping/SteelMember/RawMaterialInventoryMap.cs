using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-19 10:03
    /// 描 述：原材料库存
    /// </summary>
    public class RawMaterialInventoryMap : EntityTypeConfiguration<RawMaterialInventoryEntity>
    {
        public RawMaterialInventoryMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialInventory");
            //主键
            this.HasKey(t => t.InventoryId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
