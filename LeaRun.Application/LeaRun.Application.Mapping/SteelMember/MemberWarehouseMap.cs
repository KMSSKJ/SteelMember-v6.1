using LeaRun.Application.Entity.SteelMember;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-28 11:34
    /// �� �����������
    /// </summary>
    public class MemberWarehouseMap : EntityTypeConfiguration<MemberWarehouseEntity>
    {
        public MemberWarehouseMap()
        {
            #region ������
            //��
            this.ToTable("RMC_MemberWarehouse");
            //����
            this.HasKey(t => t.MemberWarehouseId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
