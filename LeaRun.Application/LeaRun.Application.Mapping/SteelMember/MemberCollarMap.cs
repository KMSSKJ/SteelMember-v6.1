using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-09-13 22:58
    /// 描 述：构件领用
    /// </summary>
    public class MemberCollarMap : EntityTypeConfiguration<MemberCollarEntity>
    {
        public MemberCollarMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberCollar");
            //主键
            this.HasKey(t => t.CollarId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
