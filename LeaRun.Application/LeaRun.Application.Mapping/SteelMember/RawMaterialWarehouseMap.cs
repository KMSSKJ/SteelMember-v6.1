using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-26 17:17
    /// 描 述：入库管理
    /// </summary>
    public class RawMaterialWarehouseMap : EntityTypeConfiguration<RawMaterialWarehouseEntity>
    {
        public RawMaterialWarehouseMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialWarehouse");
            //主键
            this.HasKey(t => t.WarehouseId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
