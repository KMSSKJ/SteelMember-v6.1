using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-11 10:12
    /// �� ����������������
    /// </summary>
    public class MemberProductionOrderMap : EntityTypeConfiguration<MemberProductionOrderEntity>
    {
        public MemberProductionOrderMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberProductionOrder");
            //����
            this.HasKey(t => t.OrderId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
