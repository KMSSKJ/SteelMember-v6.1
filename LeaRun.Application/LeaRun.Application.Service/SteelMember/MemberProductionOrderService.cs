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
    /// 日 期：2017-07-11 10:12
    /// 描 述：构件生产订单
    /// </summary>
    public class MemberProductionOrderService : RepositoryFactory, MemberProductionOrderIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MemberProductionOrderEntity> GetPageList(Pagination pagination,string queryJson)
        {
           var expression = LinqExtensions.True<MemberProductionOrderEntity>();
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
            else if(queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    //case "CategoryName":              //工程名
                    //    expression = expression.And(t => t.CategoryName.Contains(keyword));
                    //    break;
                    case "CreateMan":              //构件名称
                        expression = expression.And(t => t.CreateMan.Contains(keyword));
                        break;
                    case "OrderNumbering":              //编号
                        expression = expression.And(t => t.OrderNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.Category == SubProjectId);
            }
            if (!queryParam["IsConfirm"].IsEmpty())
            {
                int IsConfirm = Convert.ToInt32(queryParam["IsConfirm"]);
                expression = expression.And(t => t.IsConfirm == IsConfirm);
            }
            if (!queryParam["ProductionStatus"].IsEmpty())
            {
                int ProductionStatus = Convert.ToInt32(queryParam["ProductionStatus"]);
                expression = expression.And(t => t.ProductionStatus == ProductionStatus);
            }
            if (!queryParam["IsPassed"].IsEmpty())
            {
                int IsPassed = Convert.ToInt32(queryParam["IsPassed"]);
                expression = expression.And(t =>t.IsPassed == IsPassed);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <param name="pagination">分页</param>
        ///// <param name="queryJson">查询参数</param>
        ///// <returns>返回分页列表</returns>
        //public IEnumerable<MemberProductionOrderEntity> GetPageList(Pagination pagination, string queryJson)
        //{
        //    return this.BaseRepository().FindList<MemberProductionOrderEntity>(pagination);
        //}

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回分页列表</returns>
        public List<MemberProductionOrderEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberProductionOrderEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    //case "Category":              //构件类型
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    //case "MemberName":              //构件名称
                    //    expression = expression.And(t => t..Contains(keyword));
                    //    break;
                    case "OrderNumbering":              //编号
                        expression = expression.And(t => t.OrderNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            //expression = expression.And(t => t.OrderId==);
            return this.BaseRepository().IQueryable(expression).ToList();
            //return this.BaseRepository().FindList<MemberProductionOrderEntity>(pagination);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回分页列表</returns>
        public List<MemberProductionOrderEntity> GetList(Expression<Func<MemberProductionOrderEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
            
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MemberProductionOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<MemberProductionOrderEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<MemberProductionOrderInfoEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<MemberProductionOrderInfoEntity>("select * from RMC_MemberProductionOrderInfo where OrderId='" + keyValue + "'");
        }

        /// <summary>
        /// 获取列表(已生产)
        /// <param name="pagination"></param>
        /// <param name="condition">查询参数</param>
        /// <returns></returns>
        /// </summary>
        public IEnumerable<MemberProductionOrderEntity> GetPageListByProductionOrderStatus(Pagination pagination, Expression<Func<MemberProductionOrderEntity,bool>> condition)
        {
            //var expression = LinqExtensions.True<MemberProductionOrderEntity>();
            //expression = expression.And(t => t.ProductionStatus == IsWarehousing);
            return this.BaseRepository().FindList(condition,pagination);
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
                db.Delete<MemberProductionOrderEntity>(keyValue);
                db.Delete<MemberProductionOrderInfoEntity>(t => t.InfoId.Equals(keyValue));
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
        public void SaveForm(string keyValue, MemberProductionOrderEntity entity, List<MemberProductionOrderInfoEntity> entryList)
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
                    db.Delete<MemberProductionOrderInfoEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (MemberProductionOrderInfoEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
                        db.Insert(item);
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                    //明细
                    foreach (MemberProductionOrderInfoEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
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
            /// <param name="keyValue"></param>
            /// <param name="entity"></param>
            public void SaveForm(string keyValue, MemberProductionOrderEntity entity)
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
                //return entity.OrderId;
            }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<MemberProductionOrderEntity> list)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<MemberProductionOrderEntity> list)
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

        #endregion
    }
}
   