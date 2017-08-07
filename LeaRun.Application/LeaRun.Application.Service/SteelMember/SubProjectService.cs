using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System;
using LeaRun.Util;

using LeaRun.Util.Extension;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-06-30 22:01
    /// 描 述：子工程信息
    /// </summary>
    public class SubProjectService : RepositoryFactory<SubProjectEntity>, SubProjectIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="levels">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<SubProjectEntity> GetList(string levels)
        {
            var expression = LinqExtensions.True<SubProjectEntity>();
            //查询条件
            if (!string.IsNullOrEmpty(levels))
            {
                int level = Convert.ToInt32(levels);
                expression = expression.And(t => t.Levels<=level);
            }
            else
            {
                expression = expression.And(t => t.Levels > 0);
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SubProjectEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        public List<SubProjectEntity> GetListWant(Expression<Func<SubProjectEntity, bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string FullName, string keyValue)
        {
            var expression = LinqExtensions.True<SubProjectEntity>();
            expression = expression.And(t => t.FullName == FullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id == keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
        public void SaveForm(string keyValue, SubProjectEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                if (entity.ParentId == null)
                {
                    entity.ParentId = "0";
                    entity.Levels = 1;
                }
                else
                {
                    var level = BaseRepository().IQueryable(s => s.Id == entity.ParentId).ToList()[0].Levels;
                    entity.Levels = level + 1;
                }

                this.BaseRepository().Insert(entity);
            }
        }

        
        #endregion
    }
}
