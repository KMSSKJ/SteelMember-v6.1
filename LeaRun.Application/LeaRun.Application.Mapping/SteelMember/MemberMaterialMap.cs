using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 09:49
    /// �� ������������
    /// </summary>
    public class MemberMaterialMap : EntityTypeConfiguration<MemberMaterialEntity>
    {
        public MemberMaterialMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberMaterial");
            //����
            this.HasKey(t => t.MemberMaterialId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
