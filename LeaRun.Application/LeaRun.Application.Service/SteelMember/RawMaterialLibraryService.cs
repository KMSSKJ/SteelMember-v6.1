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
    /// �� �ڣ�2017-07-06 10:42  RepositoryFactory<RawMaterialLibraryEntity>
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public class RawMaterialLibraryService : RepositoryFactory, RawMaterialLibraryIService
    {
        private RawMaterialInventoryIService service = new RawMaterialInventoryService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        public List<RawMaterialLibraryEntity> GetList(Expression<Func<RawMaterialLibraryEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialLibraryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString();
                expression = expression.And(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                expression = expression.And(t => t.Category == Category);
            }
            //expression = expression.And(t => t.TypeId == 1);
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialLibraryEntity GetEntity(string keyValue)
        {
            // return this.BaseRepository().FindEntity(keyValue);
            return this.BaseRepository().FindEntity<RawMaterialLibraryEntity>(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            RawMaterialLibraryEntity entity = new RawMaterialLibraryEntity();
            entity.RawMaterialId = keyValue;
            this.BaseRepository().Delete(entity);

            var inventory = service.GetList("").Where(i => i.RawMaterialId.Trim() == keyValue.Trim()).SingleOrDefault();
            if (!inventory.IsEmpty())
            {
                service.RemoveForm(inventory.InventoryId);
            }

        }
        /// <summary>
        /// ɾ�����ݣ�������
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<RawMaterialLibraryEntity> list)
        {
            var inventoryList = new List<RawMaterialInventoryEntity>();
            if (list.Count()>0)
            {
                foreach (var item in list)
                {
                    var inventoryModel = service.GetEntityByRawMaterialId(item.RawMaterialId);
                    if (!inventoryModel.IsEmpty())
                    {
                        inventoryList.Add(inventoryModel);
                    }
                }
            }
            service.RemoveList(inventoryList);
            this.BaseRepository().Delete(list);
        }
        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialLibraryEntity> list)
        {
            this.BaseRepository().Update(list);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialLibraryEntity entity)
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

                var model = new RawMaterialInventoryEntity();
                model.Create();
                model.RawMaterialId = entity.RawMaterialId;
                model.Category = entity.Category;
                model.Quantity = 0;
                service.SaveForm(null, model);


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
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            expression = expression.And(t => t.RawMaterialModel == query);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RawMaterialId != keyValue);
            }
            if (!string.IsNullOrEmpty(category))
            {
                expression = expression.And(t => t.Category == category);
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
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            expression = expression.And(t => t.RawMaterialModel == query);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RawMaterialId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }

        /// <summary>
        /// ģ����ѯ(Category)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="category">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialLibraryEntity> GetPageListByLikeCategory(Pagination pagination, string category)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().FindList<RawMaterialLibraryEntity>("select * from RMC_RawMaterialLibrary where Category like'%"+category+"%'");
        }
        #endregion

    }
}
