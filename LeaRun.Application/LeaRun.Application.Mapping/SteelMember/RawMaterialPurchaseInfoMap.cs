using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-08 11:58
    /// �� ����ԭ���ϲɹ�����
    /// </summary>
    public class RawMaterialPurchaseInfoMap : EntityTypeConfiguration<RawMaterialPurchaseInfoEntity>
    {
        public RawMaterialPurchaseInfoMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialPurchaseInfo");
            //����
            this.HasKey(t => t.InfoId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
