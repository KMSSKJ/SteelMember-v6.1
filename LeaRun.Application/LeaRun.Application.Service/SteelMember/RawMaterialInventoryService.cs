using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-19 10:03
    /// �� ����ԭ���Ͽ��
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
        public IEnumerable<RawMaterialInventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
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

        public void RemoveList(List<RawMaterialInventoryEntity> list)
        {
            this.BaseRepository().Delete(list);
        }

        public void UpdataList(List<RawMaterialInventoryEntity> list)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string query, string keyValue)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string query, string category, string keyValue)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
