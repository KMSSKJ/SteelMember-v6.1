using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 10:42
    /// �� �������Ϲ���
    /// </summary>
    public class RawMaterialLibraryMap : EntityTypeConfiguration<RawMaterialLibraryEntity>
    {
        public RawMaterialLibraryMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialLibrary");
            //����
            this.HasKey(t => t.RawMaterialId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
