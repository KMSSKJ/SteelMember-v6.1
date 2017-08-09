using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-07 17:35
    /// 描 述：订单详情
    /// </summary>
    public class RawMaterialOrderInfoMap : EntityTypeConfiguration<RawMaterialOrderInfoEntity>
    {
        public RawMaterialOrderInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("RMC_RawMaterialOrderInfo");
            //主键
            this.HasKey(t => t.InfoId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
