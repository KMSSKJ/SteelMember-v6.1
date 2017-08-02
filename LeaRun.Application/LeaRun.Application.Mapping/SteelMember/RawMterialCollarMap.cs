using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-26 17:19
    /// �� �������ù���
    /// </summary>
    public class RawMterialCollarMap : EntityTypeConfiguration<RawMterialCollarEntity>
    {
        public RawMterialCollarMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMterialCollar");
            //����
            this.HasKey(t => t.CollarId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
