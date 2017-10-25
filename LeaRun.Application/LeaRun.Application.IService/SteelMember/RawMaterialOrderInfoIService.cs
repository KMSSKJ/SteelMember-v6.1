using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-08-07 17:35
    /// 描 述：订单详情
    /// </summary>
    public interface RawMaterialOrderInfoIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMaterialOrderInfoEntity> GetList(string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMaterialOrderInfoEntity> GetList(Expression<Func<RawMaterialOrderInfoEntity,bool>> condition);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RawMaterialOrderInfoEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RawMaterialOrderInfoEntity GetEntity(Expression<Func<RawMaterialOrderInfoEntity,bool>>condition);
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
        void SaveForm(string keyValue, RawMaterialOrderInfoEntity entity);
        #endregion
    }
}
