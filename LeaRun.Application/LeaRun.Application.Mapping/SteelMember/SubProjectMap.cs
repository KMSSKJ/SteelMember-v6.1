using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-06-30 22:01
    /// �� �����ӹ�����Ϣ
    /// </summary>
    public class SubProjectMap : EntityTypeConfiguration<SubProjectEntity>
    {
        public SubProjectMap()
        {
            #region ������
            //��
            this.ToTable("RMC_SubProject");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
