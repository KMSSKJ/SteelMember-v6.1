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
    /// 日 期：2017-07-05 17:15
    /// 描 述：构件库管理
    /// </summary>
    public class MemberLibraryService : RepositoryFactory<MemberLibraryEntity>, MemberLibraryIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MemberLibraryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MemberLibraryEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() || !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UploadTime >= BeginTime);
                expression = expression.And(t => t.UploadTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() || queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.UploadTime >= BeginTime);
            }
            else
            {
                expression = expression.And(t => t.UploadTime <= EndTime);
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
                    case "MemberName":              //构件名称
                        expression = expression.And(t => t.MemberName.Contains(keyword));
                        break;
                    case "MemberNumbering":              //编号
                        expression = expression.And(t => t.MemberNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if(!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.EngineeringId == SubProjectId);
            }
            return this.BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public List<MemberLibraryEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberLibraryEntity>();
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
                    case "MemberName":              //构件名称
                        expression = expression.And(t => t.MemberName.Contains(keyword));
                        break;
                    case "MemberNumbering":              //编号
                        expression = expression.And(t => t.MemberNumbering.Contains(keyword));
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
        public MemberLibraryEntity GetEntity(string keyValue)
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
        ///// <summary>
        ///// 保存表单（新增、修改）
        ///// </summary>
        ///// <param name="keyValue">主键值</param>
        ///// <param name="entity">实体对象</param>
        ///// <returns></returns>
        //public void SaveForm(string keyValue, MemberLibraryEntity entity)
        //{
        //    if (!string.IsNullOrEmpty(keyValue))
        //    {
        //        entity.Modify(keyValue);
        //        this.BaseRepository().Update(entity);
        //    }
        //    else
        //    {
        //        entity.Create();
        //        this.BaseRepository().Insert(entity);
        //    }
        //}

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, MemberLibraryEntity entity)
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
           return entity.MemberId;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="Category"></param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string FullName,string Category, string keyValue)
        {
            var expression = LinqExtensions.True<MemberLibraryEntity>();
            expression = expression.And(t => t.MemberName.Trim() == FullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.EngineeringId.Trim() == keyValue);
            }
            if (!string.IsNullOrEmpty(Category)) { 
                expression = expression.And(t => t.Category == Category);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}
