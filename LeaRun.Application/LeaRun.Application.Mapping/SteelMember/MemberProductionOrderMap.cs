using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-11 10:12
    /// 描 述：构件生产订单
    /// </summary>
    public class MemberProductionOrderMap : EntityTypeConfiguration<MemberProductionOrderEntity>
    {
        public MemberProductionOrderMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberProductionOrder");
            //主键
            this.HasKey(t => t.OrderId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
