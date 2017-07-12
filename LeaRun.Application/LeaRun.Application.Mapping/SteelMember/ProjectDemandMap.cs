using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-11 11:05
    /// 描 述：构件需求
    /// </summary>
    public class ProjectDemandMap : EntityTypeConfiguration<ProjectDemandEntity>
    {
        public ProjectDemandMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_ProjectDemand");
            //主键
            this.HasKey(t => t.ProjectDemandId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
