using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 17:19  RepositoryFactory<RawMterialCollarEntity>
    /// 描 述：领用管理
    ///</summary>
    
    public class RawMterialCollarService : RepositoryFactory, RawMterialCollarIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RawMterialCollarEntity> GetList(string queryJson)
        {
            //return this.BaseRepository().IQueryable().ToList();
            //return this.BaseRepository().FindEntity<RawMterialCollarEntity>(queryJson);
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<RawMterialCollarEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// <param name="keyValue">主键</param>
        /// </summary>


        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMterialCollarEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson)
        {
            //throw new NotImplementedException();
            if (queryJson != null)
            {
                return this.BaseRepository().FindList<RawMterialCollarEntity>(p => p.CollarId == queryJson, pagination);
            }
            return this.BaseRepository().FindList<RawMterialCollarEntity>(pagination);
        }
        #endregion
    }
}
