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
    /// 日 期：2017-07-06 10:42
    /// 描 述：原材料管理
    /// </summary>
    public class RawMaterialLibraryBLL
    {
        private RawMaterialLibraryIService service = new RawMaterialLibraryService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public List<RawMaterialLibraryEntity> GetList(Expression<Func<RawMaterialLibraryEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMaterialLibraryEntity> GetList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialLibraryEntity GetEntity(string keyValue)
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
        /// 删除数据(批量)
        /// </summary>
        public void RemoveList(List<RawMaterialLibraryEntity>list)
        {
            try
            {
                service.RemoveList(list);
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
        public void SaveForm(string keyValue, RawMaterialLibraryEntity entity)
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

        #region 验证数据
        /// <summary>
        /// 名称不能重复
        /// </summary>
        public bool ExistFullName(string FullName, string category, string keyValue = "")
        {
            return service.Exist(FullName, category, keyValue);
        }
        #endregion
    }
}
