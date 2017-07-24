using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-19 10:03
    /// �� ����ԭ���Ͽ��
    /// </summary>
    public class RawMaterialInventoryMap : EntityTypeConfiguration<RawMaterialInventoryEntity>
    {
        public RawMaterialInventoryMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialInventory");
            //����
            this.HasKey(t => t.InventoryId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
