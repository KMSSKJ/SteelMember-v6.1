using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 11:18
    /// 描 述：项目信息
    /// </summary>
    public class ProjectInfoMap : EntityTypeConfiguration<ProjectInfoEntity>
    {
        public ProjectInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_ProjectInfo");
            //主键
            this.HasKey(t => t.ProjectId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
