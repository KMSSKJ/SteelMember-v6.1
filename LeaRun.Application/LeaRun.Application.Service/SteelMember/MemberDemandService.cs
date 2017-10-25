using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Linq.Expressions;
using System;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 16:54
    /// �� ������������
    /// </summary>
    public class MemberDemandService : RepositoryFactory<MemberDemandEntity>, MemberDemandIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<MemberDemandEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MemberDemandEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime >= BeginTime);
                expression = expression.And(t => t.CreateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime <= EndTime);
            }


            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    //case "Category":              //��������
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    //case "MemberName":              //��������
                    //    expression = expression.And(t => t.MemberName.Contains(keyword));
                    //    break;
                    //case "MemberNumbering":              //���
                    //    expression = expression.And(t => t.MemberNumbering.Contains(keyword));
                    //    break;
                    default:
                        break;
                }
            }
            if (!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.SubProjectId == SubProjectId);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<MemberDemandEntity> GetPageList1(Pagination pagination, Expression<Func<MemberDemandEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<MemberDemandEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<MemberDemandEntity> GetList(Expression<Func<MemberDemandEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public MemberDemandEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="condition">����</param>
        /// <returns></returns>
        public MemberDemandEntity GetEntity(Expression<Func<MemberDemandEntity,bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
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
        /// ����������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MemberDemandEntity entity)
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

        #region ��֤����
        /// <summary>
        /// ���Ʋ����ظ�
        /// </summary>
        /// <param name="query">����</param>
        /// <param name="category"></param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool Exist(string query, string category, string keyValue)
        {
            var expression = LinqExtensions.True<MemberDemandEntity>();
            expression = expression.And(t => t.MemberDemandId == query);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.MemberDemandId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// ���Ʋ����ظ�
        /// </summary>
        /// <param name="query"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string keyValue)
        {
            var expression = LinqExtensions.True<MemberDemandEntity>();
            expression = expression.And(t => t.SubProjectId == keyValue);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.MemberId == query);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}