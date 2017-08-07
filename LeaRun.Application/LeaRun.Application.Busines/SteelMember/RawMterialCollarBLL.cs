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
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public class RawMterialCollarBLL
    {
        private RawMterialCollarIService service = new RawMterialCollarService();

        #region 获取数据  
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMterialCollarEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 分页查询出库信息
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson)
        {
            //return service.GetList(queryJson);
            return service.OutInventoryDetailInfo(pagination, queryJson);
        }
        /// <summary>
        /// 分页查询出库信息
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public List<RawMterialCollarEntity> GetPageList(Pagination pagination, string queryJson)
        {
            //return service.GetList(queryJson);
            return service.GetPageList(pagination, queryJson);
        }
        public List<RawMterialCollarEntity> GetCallarList(Expression<Func<RawMterialCollarEntity, bool>> condition)
        {
            return service.GetCallarList(condition);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        public void SaveForm(string keyValue, RawMterialCollarEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
