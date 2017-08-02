using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-26 17:17
    /// �� ����������
    /// </summary>
    public class RawMaterialWarehouseMap : EntityTypeConfiguration<RawMaterialWarehouseEntity>
    {
        public RawMaterialWarehouseMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialWarehouse");
            //����
            this.HasKey(t => t.WarehouseId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
