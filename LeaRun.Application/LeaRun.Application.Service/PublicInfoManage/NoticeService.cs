using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.IService.PublicInfoManage;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LeaRun.Application.Service.PublicInfoManage
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子公告
    /// </summary>
    public class NoticeService : RepositoryFactory<NewsEntity>, INoticeService
    {
        #region 获取数据
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<NewsEntity>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["FullHead"].IsEmpty())
                {
                    string FullHead = queryParam["FullHead"].ToString();
                    expression = expression.And(t => t.FullHead.Contains(FullHead));
                }
                if (!queryParam["Category"].IsEmpty())
                {
                    string Category = queryParam["Category"].ToString();
                    expression = expression.And(t => t.Category == Category);
                }
            expression = expression.And(t => t.TypeId == 2);
            return this.BaseRepository().FindList(expression, pagination);
        }

#pragma warning disable CS1572 // XML 注释中有“”的 param 标记，但是没有该名称的参数
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="">查询参数</param>
        /// <returns></returns>
        public DataTable GetList()
#pragma warning restore CS1572 // XML 注释中有“”的 param 标记，但是没有该名称的参数
        {
            return this.BaseRepository().FindTable("select top 6 * from Base_News where TypeId = 2 order by ReleaseTime desc");
        }

        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetList(Expression<Func<NewsEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NewsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">公告实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NewsEntity newsEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                newsEntity.Modify(keyValue);
                newsEntity.TypeId = 2;
                this.BaseRepository().Update(newsEntity);
            }
            else
            {
                newsEntity.Create();
                newsEntity.TypeId = 2;
                this.BaseRepository().Insert(newsEntity);
            }
        }
        #endregion
    }
}
