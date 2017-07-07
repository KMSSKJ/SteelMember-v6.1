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
    /// 版 本 6.1
    /// 日 期：2017-07-06 10:42
    /// 描 述：原材料管理
    /// </summary>
    public class RawMaterialLibraryService : RepositoryFactory<RawMaterialLibraryEntity>, RawMaterialLibraryIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public List<RawMaterialLibraryEntity> GetList(Expression<Func<RawMaterialLibraryEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialLibraryEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
            }
        }
        #endregion 

        #region 验证数据
        /// <summary>
        /// 名称不能重复
        /// </summary>
        /// <param name="query">名称</param>
        /// <param name="category"></param>
        /// <param name="keyValue">主键</param>
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
        /// 名称不能重复
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
        #endregion

    }
}
