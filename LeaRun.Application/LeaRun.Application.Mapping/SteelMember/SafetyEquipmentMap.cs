using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-08-29 15:39
    /// �� ������ȫ�豸
    /// </summary>
    public class SafetyEquipmentMap : EntityTypeConfiguration<SafetyEquipmentEntity>
    {
        public SafetyEquipmentMap()
        {
            #region ������
            //��
            this.ToTable("RMC_SafetyEquipment");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
