using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-13 22:58
    /// �� ������������
    /// </summary>
    public class MemberCollarMap : EntityTypeConfiguration<MemberCollarEntity>
    {
        public MemberCollarMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberCollar");
            //����
            this.HasKey(t => t.CollarId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
