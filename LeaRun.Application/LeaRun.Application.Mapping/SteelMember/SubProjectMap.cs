using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-06-30 22:01
    /// 描 述：子工程信息
    /// </summary>
    public class SubProjectMap : EntityTypeConfiguration<SubProjectEntity>
    {
        public SubProjectMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_SubProject");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
