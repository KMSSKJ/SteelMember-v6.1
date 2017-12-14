using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Application.Service.SteelMember;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
namespace LeaRun.Application.Busines.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-08 11:58
    /// 描 述：材料申请管理
    /// </summary>
    public class RawMaterialPurchaseBLL
    {
        private RawMaterialPurchaseIService service = new RawMaterialPurchaseService();

        #region 获取数据
        /// <summary>
        /// 获取列表(分页)
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetList(Expression<Func<RawMaterialPurchaseEntity,bool>>condition)
        {
            return service.GetList(condition);
        }

        /// <summary>
        /// 获取列表(已申请)
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RawMaterialPurchaseEntity> GetPageListByIsWarehousing(Pagination pagination, int IsWarehousing)
        {
            return service.GetPageListByIsWarehousing(pagination, IsWarehousing);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialPurchaseEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public List<RawMaterialPurchaseEntity> GetpurchaseList(Expression<Func<RawMaterialPurchaseEntity, bool>> condition)
        {
            return service.GetpurchaseList(condition);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<RawMaterialPurchaseInfoEntity> GetDetails(string keyValue)
        {
            return service.GetDetails(keyValue);
        }

        public RawMaterialPurchaseInfoEntity GetEntity(Expression<Func<RawMaterialPurchaseInfoEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public List<RawMaterialPurchaseInfoEntity> GetInfoList(Expression<Func<RawMaterialPurchaseInfoEntity, bool>> condition)
        {
            return service.GetInfoList(condition);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialPurchaseEntity entity,List<RawMaterialPurchaseInfoEntity> entryList)
        {
            try
            {
                service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SavePurchaseForm(string keyValue, RawMaterialPurchaseEntity entity)
        {
            try
            {
                service.SavePurchaseForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialPurchaseEntity> list)
        {
            try
            {

                service.UpdataList(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
