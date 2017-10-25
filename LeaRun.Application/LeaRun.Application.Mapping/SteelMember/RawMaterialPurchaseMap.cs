using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-08 11:58
    /// �� �������ϲɹ�����
    /// </summary>
    public class RawMaterialPurchaseMap : EntityTypeConfiguration<RawMaterialPurchaseEntity>
    {
        public RawMaterialPurchaseMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialPurchase");
            //����
            this.HasKey(t => t.RawMaterialPurchaseId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
