using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-13 23:45
    /// �� �������⹹����Ϣ
    /// </summary>
    public class MemberCollarInfoMap : EntityTypeConfiguration<MemberCollarInfoEntity>
    {
        public MemberCollarInfoMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberCollarInfo");
            //����
            this.HasKey(t => t.InfoId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
