using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System.Linq.Expressions;
using System;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-08-29 15:39
    /// 描 述：安全设备
    /// </summary>
    public class SafetyEquipmentService : RepositoryFactory<SafetyEquipmentEntity>, SafetyEquipmentIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<SafetyEquipmentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<SafetyEquipmentEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Name":              //设备名称
                        expression = expression.And(t => t.Name.Contains(keyword));
                        break;
                    case "Code":              //编号
                        expression = expression.And(t => t.Code.Contains(keyword));
                        break;
                    case "StandardModel":              //规格型号
                        expression = expression.And(t => t.StandardModel.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<SafetyEquipmentEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<SafetyEquipmentEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Name":              //设备名称
                        expression = expression.And(t => t.Name.Contains(keyword));
                        break;
                    case "Code":              //编号
                        expression = expression.And(t => t.Code.Contains(keyword));
                        break;
                    case "StandardModel":              //规格型号
                        expression = expression.And(t => t.StandardModel.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<SafetyEquipmentEntity> GetList(Expression<Func<SafetyEquipmentEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SafetyEquipmentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
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
        public void SaveForm(string keyValue, SafetyEquipmentEntity entity)
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
        #endregion
    }
}
