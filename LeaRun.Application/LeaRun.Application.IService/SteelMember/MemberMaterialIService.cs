using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-06 09:49
    /// 描 述：构件材料
    /// </summary>
    public interface MemberMaterialIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<MemberMaterialEntity> GetList(string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<MemberMaterialEntity> GetList(Expression<Func<MemberMaterialEntity, bool>> condition);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        MemberMaterialEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, MemberMaterialEntity entity);
        #endregion

        #region 验证数据
        /// <summary>
        /// 名称不能重复
        /// </summary>
        /// <param name="FullName">名称值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string FullName, string TreeName, string keyValue);
        #endregion
    }
}
