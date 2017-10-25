using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-09-13 23:45
    /// 描 述：出库构件信息
    /// </summary>
    public class MemberCollarInfoMap : EntityTypeConfiguration<MemberCollarInfoEntity>
    {
        public MemberCollarInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_MemberCollarInfo");
            //主键
            this.HasKey(t => t.InfoId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
