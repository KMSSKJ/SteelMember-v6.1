using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 11:18
    /// �� ������Ŀ��Ϣ
    /// </summary>
    public class ProjectInfoMap : EntityTypeConfiguration<ProjectInfoEntity>
    {
        public ProjectInfoMap()
        {
            #region ������
            //��
            this.ToTable("RMC_ProjectInfo");
            //����
            this.HasKey(t => t.ProjectId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
