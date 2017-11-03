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
    /// 日 期：2017-07-26 17:17
    /// 描 述：入库管理 RawMaterialWarehouseEntity
    /// </summary>
    public class RawMaterialWarehouseService : RepositoryFactory<RawMaterialWarehouseEntity>, RawMaterialWarehouseIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMaterialWarehouseEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialWarehouseEntity>();
         
            return this.BaseRepository().IQueryable(expression);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMaterialWarehouseEntity> GetList(Expression<Func<RawMaterialWarehouseEntity, bool>> condition)
        {
          
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialWarehouseEntity GetEntity(string keyValue)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<RawMaterialWarehouseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            //string[] arrayqueryJson = queryJson.Split(',');
            var expression = LinqExtensions.True<RawMaterialWarehouseEntity>();
            expression = expression.And(p => p.WarehouseId != "");
            return this.BaseRepository().FindList(expression, pagination).ToList();
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="list"></param>
        public void RemoveList(List<RawMaterialWarehouseEntity> list)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialWarehouseEntity> list)
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
        /// <param name="condition"></param>
        /// <returns></returns>
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
