using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-12-12 09:00
    /// �� �����豸�����
    /// </summary>
    public class EquipmentPreWearPartsMap : EntityTypeConfiguration<EquipmentPreWearPartsEntity>
    {
        public EquipmentPreWearPartsMap()
        {
            #region ������
            //��
            this.ToTable("RMC_EquipmentPreWearParts");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
