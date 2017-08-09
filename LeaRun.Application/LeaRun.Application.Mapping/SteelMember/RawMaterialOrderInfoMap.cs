using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-08-07 17:35
    /// �� ������������
    /// </summary>
    public class RawMaterialOrderInfoMap : EntityTypeConfiguration<RawMaterialOrderInfoEntity>
    {
        public RawMaterialOrderInfoMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialOrderInfo");
            //����
            this.HasKey(t => t.InfoId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
