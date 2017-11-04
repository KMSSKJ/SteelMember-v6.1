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
    /// �� �ڣ�2017-07-06 22:03
    /// �� ������������
    /// </summary>
    public class RawMaterialAnalysisService : RepositoryFactory<RawMaterialAnalysisEntity>, RawMaterialAnalysisIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public List<RawMaterialAnalysisEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialAnalysisEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UpdateTime >= BeginTime);
                expression = expression.And(t => t.UpdateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UpdateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UpdateTime <= EndTime);
            }

            if (!queryParam["SubProjectId"].IsEmpty())
            {
                string SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.Category == SubProjectId);
            }
   
            return this.BaseRepository().FindList(expression, pagination).ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public List<RawMaterialAnalysisEntity> GetPageList1(Expression<Func<RawMaterialAnalysisEntity, bool>> queryJson,Pagination pagination)
        {
            //var expression = LinqExtensions.True<RawMaterialAnalysisEntity>();
            //var queryParam = queryJson.ToJObject();

            //if (!queryParam["rawMaterialModel"].IsEmpty())
            //{
            //    string RawMaterialModel = queryParam["rawMaterialModel"].ToString();
            //    expression = expression.And(t => t.RawMaterialEntitys.RawMaterialModel.Contains(RawMaterialModel));
            //}
            //if (!queryParam["category"].IsEmpty())
            //{
            //    string Category = queryParam["category"].ToString();
            //    expression = expression.And(t => t.Category == Category);
            //}

            return this.BaseRepository().FindList(queryJson, pagination).ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        public List<RawMaterialAnalysisEntity> GetList(Expression<Func<RawMaterialAnalysisEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialAnalysisEntity GetEntity(string keyValue)
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
        /// ɾ�����ݣ�������
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<RawMaterialAnalysisEntity> list)
        {
            this.BaseRepository().Delete(list);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialAnalysisEntity entity)
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
        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialAnalysisEntity> list)
        {
            this.BaseRepository().Update(list);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string keyValue)
        {
            var expression = LinqExtensions.True<RawMaterialAnalysisEntity>();
            expression = expression.And(t => t.RawMaterialId == query);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// Exist
        /// </summary>
        /// <param name="query"></param>
        /// <param name="category"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string category, string keyValue)
        {
            var expression = LinqExtensions.True<RawMaterialAnalysisEntity>();
            expression = expression.And(t => t.RawMaterialId == query);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            if (!string.IsNullOrEmpty(category))
            {
                expression = expression.And(t => t.Category == category);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="RawMaterialName"></param>
        /// <param name="category"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string RawMaterialName, string category, string keyValue)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
