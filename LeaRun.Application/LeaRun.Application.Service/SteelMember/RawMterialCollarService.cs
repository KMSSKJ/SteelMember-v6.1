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
    /// 日 期：2017-07-26 17:19 
    /// 描 述：领用管理 RawMterialCollarEntity
    ///</summary>

    public class RawMterialCollarService : RepositoryFactory,RawMterialCollarIService
    {
        //RepositoryFactory<RawMterialCollarEntity>, RawMterialCollarIService
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMterialCollarEntity> GetList(string queryJson)
        {
            //return this.BaseRepository().IQueryable().ToList();
            return this.BaseRepository().FindList<RawMterialCollarEntity>(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(string keyValue)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().FindEntity<RawMterialCollarEntity>(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(Expression<Func<RawMterialCollarEntity,bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().FindEntity<RawMterialCollarEntity>(condition);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// <param name="keyValue">主键</param>
        /// </summary>


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
        public void SaveForm(string keyValue, RawMterialCollarEntity entity)
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
        public void SaveForm(string keyValue, RawMterialCollarEntity entity, List<RawMterialCollarInfoEntity> entryList)
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
                    db.Delete<RawMterialCollarInfoEntity>(t => t.CollarId.Equals(keyValue));
                    foreach (RawMterialCollarInfoEntity item in entryList)
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
                    foreach (RawMterialCollarInfoEntity item in entryList)
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
        public void UpdataList(List<RawMterialCollarEntity> list)
        {
            this.BaseRepository().Update(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson)
        {
            throw new NotImplementedException();
            //if (queryJson != null)
            //{

            //    return this.BaseRepository().FindList<RawMterialCollarEntity>(p => p.CollarId == queryJson, pagination);
            //}
            //return this.BaseRepository().FindList<RawMterialCollarEntity>(pagination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<RawMterialCollarEntity> GetCallarList(Expression<Func<RawMterialCollarEntity, bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
           // return this.BaseRepository().FindList<RawMterialCollarEntity>(condition);
        }
        /// <summary>
        /// 分页查询出库信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public List<RawMterialCollarEntity> GetPageList(Pagination pagination, string queryJson)
        {
           
            var expression = LinqExtensions.True<RawMterialCollarEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date>= BeginTime);
                expression = expression.And(t => t.Date<= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date>= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date<= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    //case "Category":              //构件类型
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    //case "CollarEngineering":             
                    //    expression = expression.And(t => t.CollarEngineering.Contains(keyword));
                    //    break;
                    case "CollarNumbering":              //型号
                        expression = expression.And(t => t.CollarNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.CollarEngineering == SubProjectId);
            }
            expression = expression.And(t => t.CollarNumbering !=null&& t.CollarNumbering != "");
            return this.BaseRepository().FindList(expression, pagination).ToList();
        }

        #endregion
    }
}
