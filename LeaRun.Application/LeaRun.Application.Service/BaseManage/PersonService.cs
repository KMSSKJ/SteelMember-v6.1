using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-11-20 21:59
    /// �� ������Ա����
    /// </summary>
    public class PersonService : RepositoryFactory<PersonEntity>, PersonIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<PersonEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<PersonEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "RealName":              //����
                        expression = expression.And(t => t.RealName.Contains(keyword));
                        break;
                    case "DepartmentId":              //��������
                        expression = expression.And(t => t.DepartmentId.Contains(keyword));
                        break;
                    case "Mobile":              //����
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                string OrganizeId = queryParam["OrganizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == OrganizeId);
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<PersonEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<PersonEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "RealName":              //����
                        expression = expression.And(t => t.RealName.Contains(keyword));
                        break;
                    case "DepartmentId":              //��������
                        expression = expression.And(t => t.DepartmentId.Contains(keyword));
                        break;
                    case "Mobile":              //����
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                var OrganizeId = queryParam["OrganizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == OrganizeId);
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(o => o.SortCode).ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public PersonEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, PersonEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
