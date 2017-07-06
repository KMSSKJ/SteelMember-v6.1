using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 09:51
    /// 描 述：构件制程
    /// </summary>
    public class MemberProcessMap : EntityTypeConfiguration<MemberProcessEntity>
    {
        public MemberProcessMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberProcess");
            //主键
            this.HasKey(t => t.MemberProcessId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
