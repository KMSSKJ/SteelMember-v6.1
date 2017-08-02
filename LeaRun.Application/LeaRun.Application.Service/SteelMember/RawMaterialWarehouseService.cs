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
    /// 版 本 6.1 RepositoryFactory<RawMaterialWarehouseEntity>
    /// 日 期：2017-07-26 17:17RepositoryFactory
    /// 描 述：入库管理
    /// </summary>
    public class RawMaterialWarehouseService : RepositoryFactory, RawMaterialWarehouseIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMaterialWarehouseEntity> GetList(string queryJson)
        {
            throw new NotImplementedException();
            //return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialWarehouseEntity GetEntity(string keyValue)
        {
            throw new NotImplementedException();
            //return this.BaseRepository().FindEntity(keyValue);
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
