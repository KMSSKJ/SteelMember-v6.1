using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-07 17:28
    /// 描 述：材料订单
    /// </summary>
    public class RawMaterialOrderMap : EntityTypeConfiguration<RawMaterialOrderEntity>
    {
        public RawMaterialOrderMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialOrder");
            //主键
            this.HasKey(t => t.OrderId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
