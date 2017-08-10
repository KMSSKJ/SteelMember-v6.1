using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-05 17:15
    /// �� �������������
    /// </summary>
    public class MemberLibraryService : RepositoryFactory<MemberLibraryEntity>, MemberLibraryIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<MemberLibraryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MemberLibraryEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() || !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UploadTime >= BeginTime);
                expression = expression.And(t => t.UploadTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() || queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UploadTime >= BeginTime);
            }
            else
            {
                expression = expression.And(t => t.UploadTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                  
                    case "Category":              //��������
                        expression = expression.And(t => t.Category.Contains(keyword));
                        break;
                    case "MemberName":              //��������
                        expression = expression.And(t => t.MemberName.Contains(keyword));
                        break;
                    case "MemberNumbering":              //���
                        expression = expression.And(t => t.MemberNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if(!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.EngineeringId == SubProjectId);
            }
            return this.BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public List<MemberLibraryEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberLibraryEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Category":              //��������
                        expression = expression.And(t => t.Category.Contains(keyword));
                        break;
                    case "MemberName":              //��������
                        expression = expression.And(t => t.MemberName.Contains(keyword));
                        break;
                    case "MemberNumbering":              //���
                        expression = expression.And(t => t.MemberNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public MemberLibraryEntity GetEntity(string keyValue)
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
        ///// <summary>
        ///// ��������������޸ģ�
        ///// </summary>
        ///// <param name="keyValue">����ֵ</param>
        ///// <param name="entity">ʵ�����</param>
        ///// <returns></returns>
        //public void SaveForm(string keyValue, MemberLibraryEntity entity)
        //{
        //    if (!string.IsNullOrEmpty(keyValue))
        //    {
        //        entity.Modify(keyValue);
        //        this.BaseRepository().Update(entity);
        //    }
        //    else
        //    {
        //        entity.Create();
        //        this.BaseRepository().Insert(entity);
        //    }
        //}

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, MemberLibraryEntity entity)
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
           return entity.MemberId;
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// ���Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����</param>
        /// <param name="Category"></param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string FullName,string Category, string keyValue)
        {
            var expression = LinqExtensions.True<MemberLibraryEntity>();
            expression = expression.And(t => t.MemberName.Trim() == FullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.EngineeringId.Trim() == keyValue);
            }
            if (!string.IsNullOrEmpty(Category)) { 
                expression = expression.And(t => t.Category == Category);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}
