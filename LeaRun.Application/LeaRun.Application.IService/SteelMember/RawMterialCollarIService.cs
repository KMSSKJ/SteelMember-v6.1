using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public interface RawMterialCollarIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMterialCollarEntity> GetList(string queryJson);

        List<RawMterialCollarEntity> GetCallarList(Expression<System.Func<RawMterialCollarEntity, bool>> condition);
        /// <summary>
        /// 分页查询出库信息
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson);

        /// <summary>
        /// 分页查询出库信息
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        List<RawMterialCollarEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RawMterialCollarEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        RawMterialCollarEntity GetEntity(Expression<Func<RawMterialCollarEntity,bool>>condition);
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
        void SaveForm(string keyValue, RawMterialCollarEntity entity);

        void SaveForm(string keyValue, RawMterialCollarEntity entity, List<RawMterialCollarInfoEntity> entryList);
        void UpdataList(List<RawMterialCollarEntity> list);
        #endregion
    }
}
