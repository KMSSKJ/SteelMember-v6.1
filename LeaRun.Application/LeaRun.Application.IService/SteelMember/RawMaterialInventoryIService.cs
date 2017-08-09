using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-19 10:03
    /// 描 述：原材料库存
    /// </summary>
    public interface RawMaterialInventoryIService : IBaseService<RawMaterialInventoryEntity>
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        List<RawMaterialInventoryEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<RawMaterialInventoryEntity> GetPageList1(Pagination pagination, Expression<Func<RawMaterialInventoryEntity, bool>> condition);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMaterialInventoryEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RawMaterialInventoryEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="rawMaterialId">外键值</param>
        /// <returns></returns>
        RawMaterialInventoryEntity GetEntityByRawMaterialId(string rawMaterialId);
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
        void SaveForm(string keyValue, RawMaterialInventoryEntity entity);
        #endregion
    }
}
