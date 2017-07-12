using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-11 10:12
    /// 描 述：构件生产订单
    /// </summary>
    public class MemberProductionOrderInfoMap : EntityTypeConfiguration<MemberProductionOrderInfoEntity>
    {
        public MemberProductionOrderInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberProductionOrderInfo");
            //主键
            this.HasKey(t => t.InfoId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
