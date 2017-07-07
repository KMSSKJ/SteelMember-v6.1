using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-06 09:49
    /// 描 述：构件原材料
    /// </summary>
    public class MemberMaterialService : RepositoryFactory<MemberMaterialEntity>, MemberMaterialIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<MemberMaterialEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MemberMaterialEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, MemberMaterialEntity entity)
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

        #region 验证数据
        /// <summary>
        /// 名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="TreeName"></param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string FullName, string TreeName, string keyValue)
        {
            var expression = LinqExtensions.True<MemberMaterialEntity>();
            expression = expression.And(t => t.RawMaterialModel == FullName);
            if (!string.IsNullOrEmpty(TreeName))
            {
                expression = expression.And(t => t.TreeName == TreeName);
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.MemberId == keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}
