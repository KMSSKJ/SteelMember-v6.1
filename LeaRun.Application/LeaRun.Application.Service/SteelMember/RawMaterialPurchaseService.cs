using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-08 11:58
    /// �� ����ԭ���ϲɹ�����
    /// </summary>
    public class RawMaterialPurchaseService : RepositoryFactory, RawMaterialPurchaseIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            if (queryJson!=null) {
                return this.BaseRepository().FindList<RawMaterialPurchaseEntity>(p=>p.Category==queryJson, pagination);
            }
            return this.BaseRepository().FindList<RawMaterialPurchaseEntity>(pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialPurchaseEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<RawMaterialPurchaseEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<RawMaterialPurchaseInfoEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<RawMaterialPurchaseInfoEntity>("select * from RMC_RawMaterialPurchaseInfo where RawMaterialPurchaseId='" + keyValue + "'");        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<RawMaterialPurchaseEntity>(keyValue);
                db.Delete<RawMaterialPurchaseInfoEntity>(t => t.RawMaterialPurchaseId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="entryList"></param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialPurchaseEntity entity,List<RawMaterialPurchaseInfoEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //����
                entity.Modify(keyValue);
                db.Update(entity);
                //��ϸ
                db.Delete<RawMaterialPurchaseInfoEntity>(t => t.RawMaterialPurchaseId.Equals(keyValue));
                foreach (RawMaterialPurchaseInfoEntity item in entryList)
                {
                    item.Create();
                    item.RawMaterialPurchaseId = entity.RawMaterialPurchaseId;
                    db.Insert(item);
                }
            }
            else
            {
                //����
                entity.Create();
                db.Insert(entity);
                //��ϸ
                foreach (RawMaterialPurchaseInfoEntity item in entryList)
                {
                    item.Create();
                    item.RawMaterialPurchaseId = entity.RawMaterialPurchaseId;
                    db.Insert(item);
                }
            }
            db.Commit();
        }
        catch (Exception)
        {
            db.Rollback();
            throw;
        }
        }

        public List<RawMaterialPurchaseInfoEntity> GetList(Expression<Func<RawMaterialPurchaseInfoEntity, bool>> condition)
        {
            // throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
        }

        public void RemoveList(List<RawMaterialPurchaseEntity> list)
        {
            throw new NotImplementedException();
        }

        public void UpdataList(List<RawMaterialPurchaseEntity> list)
        {
            this.BaseRepository().Update(list);
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
