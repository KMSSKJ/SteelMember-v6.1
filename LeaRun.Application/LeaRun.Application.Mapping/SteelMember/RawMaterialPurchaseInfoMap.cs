using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-07-08 11:58
    /// 描 述：原材料采购管理
    /// </summary>
    public class RawMaterialPurchaseInfoMap : EntityTypeConfiguration<RawMaterialPurchaseInfoEntity>
    {
        public RawMaterialPurchaseInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialPurchaseInfo");
            //主键
            this.HasKey(t => t.InfoId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
