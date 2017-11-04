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
    /// 日 期：2017-07-06 22:03
    /// 描 述：材料需求
    /// </summary>
    public class RawMaterialAnalysisService : RepositoryFactory<RawMaterialAnalysisEntity>, RawMaterialAnalysisIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public List<RawMaterialAnalysisEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialAnalysisEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
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
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public List<RawMaterialAnalysisEntity> GetList(Expression<Func<RawMaterialAnalysisEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialAnalysisEntity GetEntity(string keyValue)
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
        /// 删除数据（批量）
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<RawMaterialAnalysisEntity> list)
        {
            this.BaseRepository().Delete(list);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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
        /// 批量修改
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialAnalysisEntity> list)
        {
            this.BaseRepository().Update(list);
        }
        #endregion

        #region 验证数据
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
