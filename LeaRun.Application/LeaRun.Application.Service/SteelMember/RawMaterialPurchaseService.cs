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
    /// 版 本 6.1
    /// 日 期：2017-07-08 11:58
    /// 描 述：材料申请管理
    /// </summary> :
    public class RawMaterialPurchaseService : RepositoryFactory, RawMaterialPurchaseIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialPurchaseEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime >= BeginTime);
                expression = expression.And(t => t.CreateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "PurchaseNumbering":              //牌号/规格
                        expression = expression.And(t => t.PurchaseNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["IsWarehousing"].IsEmpty())
            {
                var IsWarehousing = queryParam["IsWarehousing"].ToInt();
                expression = expression.And(t => t.IsWarehousing == IsWarehousing);
            }
            return this.BaseRepository().FindList(expression, pagination);
            //if (queryJson!=null) {
            //    return this.BaseRepository().FindList<RawMaterialPurchaseEntity>(p=>p.Category==queryJson, pagination);
            //}
            //return this.BaseRepository().FindList<RawMaterialPurchaseEntity>(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialPurchaseEntity>();
            //var queryParam = queryJson.ToJObject();
            ////查询条件
            //if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            //{
            //    string condition = queryParam["condition"].ToString();
            //    string keyword = queryParam["keyword"].ToString();
            //    switch (condition)
            //    {
            //        case "Category":              //编号
            //            expression = expression.And(t => t.Category.Contains(keyword));
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //if (queryJson != "")
            //{
            //    expression = expression.And(t => t.Category == queryJson);
            //}
            return this.BaseRepository().IQueryable(expression);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetList(Expression<Func<RawMaterialPurchaseEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialPurchaseEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<RawMaterialPurchaseEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<RawMaterialPurchaseInfoEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<RawMaterialPurchaseInfoEntity>("select * from RMC_RawMaterialPurchaseInfo where RawMaterialPurchaseId='" + keyValue + "'");        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">主键值</param>
        /// <returns></returns>
        public RawMaterialPurchaseInfoEntity GetEntity(Expression<Func<RawMaterialPurchaseInfoEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList"></param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialPurchaseEntity entity,List<RawMaterialPurchaseInfoEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //主表
                entity.Modify(keyValue);
                db.Update(entity);
                //明细
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
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<RawMaterialPurchaseInfoEntity> GetInfoList(Expression<Func<RawMaterialPurchaseInfoEntity, bool>> condition)
        {
            // throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<RawMaterialPurchaseEntity> list)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialPurchaseEntity> list)
        {
            this.BaseRepository().Update(list);
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
        public List<RawMaterialPurchaseEntity> GetpurchaseList(Expression<Func<RawMaterialPurchaseEntity, bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SavePurchaseForm(string keyValue, RawMaterialPurchaseEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                   
                    entity.Modify(keyValue);
                    db.Update(entity);
                  
                }
                else
                {
                   
                    entity.Create();
                    db.Insert(entity);
                    

                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="IsWarehousing"></param>
        /// <returns></returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetPageListByIsWarehousing(Pagination pagination, int IsWarehousing)
        {
            try {
                if (IsWarehousing == 0)
                {
                    return this.BaseRepository().FindList<RawMaterialPurchaseEntity>(p => p.IsWarehousing == IsWarehousing, pagination);
                }
            }
            catch(Exception) {
                throw;
            }

            // return this.BaseRepository().FindList<RawMaterialPurchaseEntity>(pagination);
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
