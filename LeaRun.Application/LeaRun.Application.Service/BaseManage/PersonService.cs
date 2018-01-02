using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-11-20 21:59
    /// 描 述：人员管理
    /// </summary>
    public class PersonService : RepositoryFactory<PersonEntity>, PersonIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PersonEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<PersonEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "RealName":              //姓名
                        expression = expression.And(t => t.RealName.Contains(keyword));
                        break;
                    case "DepartmentId":              //所属部门
                        expression = expression.And(t => t.DepartmentId.Contains(keyword));
                        break;
                    case "Mobile":              //姓名
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                string OrganizeId = queryParam["OrganizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == OrganizeId);
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PersonEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<PersonEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "RealName":              //姓名
                        expression = expression.And(t => t.RealName.Contains(keyword));
                        break;
                    case "DepartmentId":              //所属部门
                        expression = expression.And(t => t.DepartmentId.Contains(keyword));
                        break;
                    case "Mobile":              //姓名
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                var OrganizeId = queryParam["OrganizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == OrganizeId);
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(o => o.SortCode).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PersonEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, PersonEntity entity)
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
