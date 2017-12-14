using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-11-20 21:59
    /// 描 述：人员管理
    /// </summary>
    public class PersonMap : EntityTypeConfiguration<PersonEntity>
    {
        public PersonMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Person");
            //主键
            this.HasKey(t => t.PersonId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
