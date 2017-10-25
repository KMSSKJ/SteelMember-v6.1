using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-08 11:58
    /// 描 述：材料采购管理
    /// </summary>
    public interface RawMaterialPurchaseIService: IService.IBaseService<RawMaterialPurchaseEntity>
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<RawMaterialPurchaseEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMaterialPurchaseEntity> GetList(string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RawMaterialPurchaseEntity> GetList(Expression<Func<RawMaterialPurchaseEntity,bool>>condition);

        /// <summary>
        /// 获取列表(已采购)
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<RawMaterialPurchaseEntity> GetPageListByIsWarehousing(Pagination pagination, int IsWarehousing);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RawMaterialPurchaseEntity GetEntity(string keyValue);
        List<RawMaterialPurchaseEntity> GetpurchaseList(Expression<System.Func<RawMaterialPurchaseEntity, bool>> condition);
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        IEnumerable<RawMaterialPurchaseInfoEntity> GetDetails(string keyValue);

        RawMaterialPurchaseInfoEntity GetEntity(Expression<Func<RawMaterialPurchaseInfoEntity, bool>> condition);

        List<RawMaterialPurchaseInfoEntity> GetInfoList(Expression<System.Func<RawMaterialPurchaseInfoEntity, bool>> condition);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        

        void SaveForm(string keyValue, RawMaterialPurchaseEntity entity,List<RawMaterialPurchaseInfoEntity> entryList);
        void SavePurchaseForm(string keyValue, RawMaterialPurchaseEntity entity);
        #endregion
    }
}
