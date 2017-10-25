using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-09-12 09:47
    /// 描 述：材料领用详情
    /// </summary>
    public class RawMterialCollarInfoMap : EntityTypeConfiguration<RawMterialCollarInfoEntity>
    {
        public RawMterialCollarInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMterialCollarInfo");
            //主键
            this.HasKey(t => t.InfoId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
