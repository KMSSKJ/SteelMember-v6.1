using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-28 11:34
    /// 描 述：构件库存
    /// </summary>
    public class MemberWarehouseMap : EntityTypeConfiguration<MemberWarehouseEntity>
    {
        public MemberWarehouseMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberWarehouse");
            //主键
            this.HasKey(t => t.MemberWarehouseId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
