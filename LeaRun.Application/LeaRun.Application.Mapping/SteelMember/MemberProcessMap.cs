using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 09:51
    /// �� ���������Ƴ�
    /// </summary>
    public class MemberProcessMap : EntityTypeConfiguration<MemberProcessEntity>
    {
        public MemberProcessMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberProcess");
            //����
            this.HasKey(t => t.MemberProcessId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
