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
    /// 版 本 6.1
    /// 日 期：2017-07-19 10:03
    /// 描 述：原材料库存
    /// </summary>
    public class RawMaterialInventoryService : RepositoryFactory<RawMaterialInventoryEntity>, RawMaterialInventoryIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public List<RawMaterialInventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialInventoryEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString();
                expression = expression.And(t => t.Category.Contains(RawMaterialModel));
            }
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                expression = expression.And(t => t.Category == Category);
            }
            //return this.BaseRepository().FindList(pagination);
            return this.BaseRepository().FindList(expression, pagination).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<RawMaterialInventoryEntity> GetPageList1(Pagination pagination, Expression<Func<RawMaterialInventoryEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition,pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMaterialInventoryEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="rawMaterialId">外键值</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntityByRawMaterialId(string rawMaterialId)
        {
            var expression = LinqExtensions.True<RawMaterialInventoryEntity>();
            expression = expression.And(t => t.RawMaterialId == rawMaterialId);
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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

        #endregion
    }
}
