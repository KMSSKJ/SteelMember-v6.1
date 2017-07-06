using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.IService
{
   public interface IBaseService<T> where T : class
    {
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="query">用户Id</param>
        /// <returns></returns>
        IEnumerable<T> GetList(string query);
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="query">主键值</param>
        /// <returns></returns>
        T GetEntity(string query);

        #endregion

        #region 验证数据
        /// <summary>
        /// 字段不能重复（从全部数据里验证）
        /// </summary>
        /// <param name="queryJson">要验证的字段</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool Exist(string queryJson, string keyValue);

        /// <summary>
        /// 字段不能重复（从全部数据里按分类验证）
        /// </summary>
        /// <param name="query">要验证的字段</param>
        /// <param name="category">分类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool Exist(string query,string category, string keyValue);
        #endregion
    }
}
