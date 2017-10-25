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
    /// 日 期：2017-07-06 22:03
    /// 描 述：材料分析
    /// </summary>
    public class RawMaterialAnalysisBLL
    {
        private RawMaterialAnalysisIService service = new RawMaterialAnalysisService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public List<RawMaterialAnalysisEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public List<RawMaterialAnalysisEntity> GetPageList1(Expression<Func<RawMaterialAnalysisEntity, bool>> condition,Pagination pagination)
        {
            return service.GetPageList1(condition, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public List<RawMaterialAnalysisEntity> GetList(Expression<Func<RawMaterialAnalysisEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMaterialAnalysisEntity GetEntity(string keyValue)
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
        public void RemoveList(List<RawMaterialAnalysisEntity> list)
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
        public void SaveForm(string keyValue, RawMaterialAnalysisEntity entity)
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
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialAnalysisEntity> list)
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
