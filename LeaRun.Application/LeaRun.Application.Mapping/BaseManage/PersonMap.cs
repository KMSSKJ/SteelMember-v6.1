using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-11-20 21:59
    /// �� ������Ա����
    /// </summary>
    public class PersonMap : EntityTypeConfiguration<PersonEntity>
    {
        public PersonMap()
        {
            #region ������
            //��
            this.ToTable("Base_Person");
            //����
            this.HasKey(t => t.PersonId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
