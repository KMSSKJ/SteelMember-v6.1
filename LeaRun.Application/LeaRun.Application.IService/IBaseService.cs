using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.IService
{
   public interface IBaseService<T> where T : class
    {
        #region 提交数据(批量)
        /// <summary>
        /// 删除数据
        /// </summary>
        void RemoveList(List<T> list);

        /// <summary>
        /// 修改数据
        /// </summary>
        void UpdataList(List<T> list);

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
        bool Exist(string query,string category, string keyValue);
        #endregion
    }
}
