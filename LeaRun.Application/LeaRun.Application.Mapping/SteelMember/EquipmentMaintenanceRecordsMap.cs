using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-27 09:17
    /// �� ���������豸
    /// </summary>
    public class EquipmentMaintenanceRecordsMap : EntityTypeConfiguration<EquipmentMaintenanceRecordsEntity>
    {
        public EquipmentMaintenanceRecordsMap()
        {
            #region ������
            //��
            this.ToTable("RMC_EquipmentMaintenanceRecords");
            //����
            this.HasKey(t => t.InfoId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
