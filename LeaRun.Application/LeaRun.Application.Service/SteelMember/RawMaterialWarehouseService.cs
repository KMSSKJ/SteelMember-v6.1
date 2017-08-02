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
    /// �� �� 6.1 RepositoryFactory<RawMaterialWarehouseEntity>
    /// �� �ڣ�2017-07-26 17:17RepositoryFactory
    /// �� ����������
    /// </summary>
    public class RawMaterialWarehouseService : RepositoryFactory, RawMaterialWarehouseIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialWarehouseEntity> GetList(string queryJson)
        {
            throw new NotImplementedException();
            //return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialWarehouseEntity GetEntity(string keyValue)
        {
            throw new NotImplementedException();
            //return this.BaseRepository().FindEntity(keyValue);
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
        public void SaveForm(string keyValue, RawMaterialWarehouseEntity entity)
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

        public IEnumerable<RawMaterialWarehouseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            //throw new NotImplementedException();
            if (queryJson != null)
            {
                return this.BaseRepository().FindList<RawMaterialWarehouseEntity>(p => p.WarehouseId == queryJson, pagination);
            }
            return this.BaseRepository().FindList<RawMaterialWarehouseEntity>(pagination);
        }
      
        public void RemoveList(List<RawMaterialWarehouseEntity> list)
        {
            throw new NotImplementedException();
        }

        public void UpdataList(List<RawMaterialWarehouseEntity> list)
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

        public List<RawMaterialWarehouseEntity> GetpurchaseList(Expression<Func<RawMaterialWarehouseEntity, bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        //List<RawMaterialWarehouseEntity> RawMaterialWarehouseIService.GetPageList(Pagination pagination, string queryJson)
        //{
        //    //throw new NotImplementedException();
        //    if (queryJson != null)
        //    {
        //        return this.BaseRepository().FindList<RawMaterialWarehouseEntity>(p => p.WarehouseId == queryJson, pagination);
        //    }
        //    return this.BaseRepository().FindList<RawMaterialWarehouseEntity>(pagination);
        //}
        #endregion
    }
    }
