using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-06-30 21:02
    /// �� ����ԭ���Ͽ����
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
