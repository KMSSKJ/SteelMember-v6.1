using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-08-29 15:00
    /// 描 述：生产设备
    /// </summary>
    public class EquipmentMaintenanceRecordsController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = equipmentmaintenancerecordsbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = equipmentmaintenancerecordsbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Id">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJsonFunc(string Id)
        {
            var data = equipmentmaintenancerecordsbll.GetList(f => f.Id == Id);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = equipmentmaintenancerecordsbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
            }
            foreach (var item in ids)
            {
                equipmentmaintenancerecordsbll.RemoveForm(item);
            }

            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, EquipmentMaintenanceRecordsEntity entity, DateTime warrantyDate, int type)
        {
            entity.MaintenanceDate = DateTime.Now;
            entity.MaintenancePeople = OperatorProvider.Provider.Current().UserName;
            equipmentmaintenancerecordsbll.SaveForm(keyValue, entity);

            if (type == 1)
            {
                var model = produceequipmentbll.GetEntity(entity.Id);
                model.Status = 1;
                model.MaintenanceDate = entity.MaintenanceDate;
                model.WarrantyDate = warrantyDate;
                produceequipmentbll.SaveForm(entity.Id, model);
            }
            else
            {
                var model = safetyequipmentbll.GetEntity(entity.Id);
                model.Status = 1;
                model.MaintenanceDate = entity.MaintenanceDate;
                model.WarrantyDate = warrantyDate;
                safetyequipmentbll.SaveForm(entity.Id, model);
            }

            return Success("操作成功。");
        }
        #endregion
    }
}
