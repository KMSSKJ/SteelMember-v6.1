using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-11 11:05
    /// �� ������������
    /// </summary>
    public class ProjectDemandMap : EntityTypeConfiguration<ProjectDemandEntity>
    {
        public ProjectDemandMap()
        {
            #region ������
            //��
            this.ToTable("RMC_ProjectDemand");
            //����
            this.HasKey(t => t.ProjectDemandId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
