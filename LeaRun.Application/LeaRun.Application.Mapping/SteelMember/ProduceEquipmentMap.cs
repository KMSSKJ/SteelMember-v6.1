using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-08-29 15:00
    /// �� ���������豸
    /// </summary>
    public class ProduceEquipmentMap : EntityTypeConfiguration<ProduceEquipmentEntity>
    {
        public ProduceEquipmentMap()
        {
            #region ������
            //��
            this.ToTable("RMC_ProduceEquipment");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
