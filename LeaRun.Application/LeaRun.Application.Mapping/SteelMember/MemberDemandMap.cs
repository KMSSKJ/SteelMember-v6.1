using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-26 16:54
    /// �� ������������
    /// </summary>
    public class MemberDemandMap : EntityTypeConfiguration<MemberDemandEntity>
    {
        public MemberDemandMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberDemand");
            //����
            this.HasKey(t => t.MemberDemandId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
