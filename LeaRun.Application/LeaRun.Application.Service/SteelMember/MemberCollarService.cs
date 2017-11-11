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
    /// 日 期：2017-09-13 22:58
    /// 描 述：构件领用
    /// </summary>
    public class MemberCollarService : RepositoryFactory, MemberCollarIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MemberCollarEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MemberCollarEntity>();
            var queryParam = queryJson.ToJObject();

            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date >= BeginTime);
                expression = expression.And(t => t.Date <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date <= EndTime);
            }

            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "CollarNumbering":
                        expression = expression.And(t => t.CollarNumbering.Contains(keyword));
                        break;
                    //case "CollarEngineering":
                    //    expression = expression.And(t => t.CollarEngineering.Contains(keyword));
                    //    break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<MemberCollarEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberCollarEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "CollarNumbering":              //CollarNumbering
                        expression = expression.And(t => t.CollarNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<MemberCollarEntity> GetList(Expression<Func<MemberCollarEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MemberCollarEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<MemberCollarEntity>(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">主键值</param>
        /// <returns></returns>
        public MemberCollarEntity GetEntity(Expression<Func<MemberCollarEntity,bool>> condition)
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
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MemberCollarEntity entity)
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
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="entryList"></param>
        public void SaveForm(string keyValue, MemberCollarEntity entity, List<MemberCollarInfoEntity> entryList)
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
                    db.Delete<MemberCollarInfoEntity>(t => t.CollarId.Equals(keyValue));
                    foreach (MemberCollarInfoEntity item in entryList)
                    {
                        item.Create();
                        item.CollarId = entity.CollarId;
                        db.Insert(item);
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                    //明细
                    foreach (MemberCollarInfoEntity item in entryList)
                    {
                        item.Create();
                        item.CollarId = entity.CollarId;
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
        /// <param name="list"></param>
        public void UpdataList(List<MemberCollarEntity> list)
        {
            this.BaseRepository().Update(list);
        }
        #endregion
    }
}
