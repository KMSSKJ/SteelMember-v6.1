using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-28 11:34
    /// �� �����������
    /// </summary>
    public class MemberWarehouseRecordingMap : EntityTypeConfiguration<MemberWarehouseRecordingEntity>
    {
        public MemberWarehouseRecordingMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberWarehouseRecording");
            //����
            this.HasKey(t => t.RecordingId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
