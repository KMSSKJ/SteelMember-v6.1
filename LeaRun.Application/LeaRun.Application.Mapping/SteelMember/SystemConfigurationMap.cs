using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 11:18
    /// �� ������Ŀ��Ϣ
    /// </summary>
    public class SystemConfigurationMap : EntityTypeConfiguration<SystemConfigurationEntity>
    {
        public SystemConfigurationMap()
        {
            #region ������
            //��
            this.ToTable("RMC_SystemConfiguration");
            //����
            this.HasKey(t => t.SystemConfigurationId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
