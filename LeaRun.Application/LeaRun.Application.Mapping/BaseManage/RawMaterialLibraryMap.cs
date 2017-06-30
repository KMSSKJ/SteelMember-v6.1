using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-06-30 21:02
    /// 描 述：原材料库管理
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
