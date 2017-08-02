using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 16:54
    /// 描 述：构件需求
    /// </summary>
    public interface MemberDemandIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<MemberDemandEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<MemberDemandEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        MemberDemandEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, MemberDemandEntity entity);
        #endregion

        #region 验证数据
        /// <summary>
        /// 字段不能重复（从全部数据里验证）
        /// </summary>
        /// <param name="query">要验证的字段</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool Exist(string query, string keyValue);

        /// <summary>
        /// 字段不能重复（从全部数据里按分类验证）
        /// </summary>
        /// <param name="query">要验证的字段</param>
        /// <param name="category">分类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool Exist(string query, string category, string keyValue);
        #endregion
    }
}
