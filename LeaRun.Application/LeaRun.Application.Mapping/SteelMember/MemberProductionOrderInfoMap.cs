using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-11 10:12
    /// �� ����������������
    /// </summary>
    public class MemberProductionOrderInfoMap : EntityTypeConfiguration<MemberProductionOrderInfoEntity>
    {
        public MemberProductionOrderInfoMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberProductionOrderInfo");
            //����
            this.HasKey(t => t.InfoId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
