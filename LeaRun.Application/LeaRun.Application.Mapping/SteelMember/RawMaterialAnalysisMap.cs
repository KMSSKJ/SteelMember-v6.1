using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 22:03
    /// �� ������������
    /// </summary>
    public class RawMaterialAnalysisMap : EntityTypeConfiguration<RawMaterialAnalysisEntity>
    {
        public RawMaterialAnalysisMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMaterialAnalysis");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
