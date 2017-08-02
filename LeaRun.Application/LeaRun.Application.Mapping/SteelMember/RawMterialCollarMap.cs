using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public class RawMterialCollarMap : EntityTypeConfiguration<RawMterialCollarEntity>
    {
        public RawMterialCollarMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMterialCollar");
            //主键
            this.HasKey(t => t.CollarId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
