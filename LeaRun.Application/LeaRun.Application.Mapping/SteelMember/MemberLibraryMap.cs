using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-05 17:15
    /// �� �������������
    /// </summary>
    public class MemberLibraryMap : EntityTypeConfiguration<MemberLibraryEntity>
    {
        public MemberLibraryMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberLibrary");
            //����
            this.HasKey(t => t.MemberId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
