using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-12 09:47
    /// �� ����������������
    /// </summary>
    public class RawMterialCollarInfoMap : EntityTypeConfiguration<RawMterialCollarInfoEntity>
    {
        public RawMterialCollarInfoMap()
        {
            #region ������
            //��
            this.ToTable("RMC_RawMterialCollarInfo");
            //����
            this.HasKey(t => t.InfoId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
