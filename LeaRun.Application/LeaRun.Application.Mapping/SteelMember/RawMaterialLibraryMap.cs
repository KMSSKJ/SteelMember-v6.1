using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 10:42
    /// 描 述：材料管理
    /// </summary>
    public class RawMaterialLibraryMap : EntityTypeConfiguration<RawMaterialLibraryEntity>
    {
        public RawMaterialLibraryMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialLibrary");
            //主键
            this.HasKey(t => t.RawMaterialId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
