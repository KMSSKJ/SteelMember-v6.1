using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-26 16:54
    /// 描 述：构件需求
    /// </summary>
    public class MemberDemandMap : EntityTypeConfiguration<MemberDemandEntity>
    {
        public MemberDemandMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberDemand");
            //主键
            this.HasKey(t => t.MemberDemandId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
