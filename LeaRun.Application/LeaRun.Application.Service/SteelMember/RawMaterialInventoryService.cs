using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-19 10:03
    /// �� �������Ͽ��
    /// </summary>
    public class RawMaterialInventoryService : RepositoryFactory<RawMaterialInventoryEntity>, RawMaterialInventoryIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public List<RawMaterialInventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialInventoryEntity>();
            var queryParam = queryJson.ToJObject();

            if (!queryParam["CategoryId"].IsEmpty())
            {
                string CategoryId = queryParam["CategoryId"].ToString();
                expression = expression.And(t => t.Category == CategoryId);
            }

            if (!queryParam["Quantity"].IsEmpty())
            {
                int Quantity =Convert.ToInt32(queryParam["Quantity"]);
                expression = expression.And(t => t.Quantity.ToDecimal()> Quantity);
            }
            expression = expression.And(t => t.Quantity>0);
            return this.BaseRepository().FindList(expression, pagination).ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialInventoryEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntity(Expression<Func<RawMaterialInventoryEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="rawMaterialId">���ֵ</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntityByRawMaterialId(string rawMaterialId)
        {
            var expression = LinqExtensions.True<RawMaterialInventoryEntity>();
            expression = expression.And(t => t.RawMaterialId == rawMaterialId);
            return this.BaseRepository().FindEntity(expression);
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
        public void SaveForm(string keyValue, RawMaterialInventoryEntity entity)
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
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<RawMaterialInventoryEntity> list)
        {
            this.BaseRepository().Delete(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialInventoryEntity> list)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string keyValue)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="category"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string category, string keyValue)
        {
            throw new NotImplementedException();
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
