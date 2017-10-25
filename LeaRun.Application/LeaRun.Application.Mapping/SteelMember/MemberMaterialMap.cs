using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-06 09:49
    /// 描 述：构件材料
    /// </summary>
    public class MemberMaterialMap : EntityTypeConfiguration<MemberMaterialEntity>
    {
        public MemberMaterialMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberMaterial");
            //主键
            this.HasKey(t => t.MemberMaterialId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
