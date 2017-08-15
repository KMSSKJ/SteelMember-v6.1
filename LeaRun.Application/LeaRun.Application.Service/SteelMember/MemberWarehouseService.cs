using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-28 11:34
    /// 描 述：构件库存
    /// </summary>
    public class MemberWarehouseService : RepositoryFactory<MemberWarehouseEntity>, MemberWarehouseIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MemberWarehouseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MemberWarehouseEntity>();
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
            else if(queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UpdateTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    case "Category":              //构件类型
                        expression = expression.And(t => t.Category.Contains(keyword));
                        break;
                    case "MemberModel":              //构件型号
                        expression = expression.And(t => t.MemberModel.Contains(keyword));
                        break;
                    case "EngineeringId":              //编号
                        expression = expression.And(t => t.EngineeringId.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["EngineeringId"].IsEmpty())
            {
                var SubProjectId = queryParam["EngineeringId"].ToString();
                expression = expression.And(t => t.EngineeringId == SubProjectId);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public List<MemberWarehouseEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberWarehouseEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Category":              //构件类型
                        expression = expression.And(t => t.Category.Contains(keyword));
                        break;
                    case "MemberModel":              //构件型号
                        expression = expression.And(t => t.MemberModel.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MemberWarehouseEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, MemberWarehouseEntity entity)
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
    }
}
