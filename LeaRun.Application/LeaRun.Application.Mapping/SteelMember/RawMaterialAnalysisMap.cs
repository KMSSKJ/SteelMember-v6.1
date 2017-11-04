using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 22:03
    /// 描 述：材料需求
    /// </summary>
    public class RawMaterialAnalysisMap : EntityTypeConfiguration<RawMaterialAnalysisEntity>
    {
        public RawMaterialAnalysisMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialAnalysis");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
