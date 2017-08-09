using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 17:17
    /// 描 述：入库管理
    /// </summary>
    public class RawMaterialWarehouseController : MvcControllerBase
    {
        private RawMaterialWarehouseBLL rawmaterialwarehousebll = new RawMaterialWarehouseBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = rawmaterialwarehousebll.GetList(queryJson);
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
            var data = rawmaterialwarehousebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 入库详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult IntoInventoryDetailInfo(Pagination pagination, string category)
        {
            //var SB=Request["endtime"];
            //var notime = "0001/1/1/1 0:00:00";
            var degintime = Convert.ToDateTime(Request["begintime"])== Convert.ToDateTime(null) ? Convert.ToDateTime(null) : Convert.ToDateTime(Request["begintime"]);
            var endtime = Convert.ToDateTime(Request["endtime"]) == Convert.ToDateTime(null) ? System.DateTime.Now : Convert.ToDateTime(Request["endtime"]);
            List<RawmaterialWarehouseModel> list = new List<RawmaterialWarehouseModel>();
            try {
                var data = rawmateriallibrarybll.GetPageListByLikeCategory(pagination, category);
                foreach (var item in data)
                {
                    //var warehoused = rawmaterialwarehousebll.GetpurchaseList(p => p.RawMaterialId == item.RawMaterialId&&p.WarehouseTime>= degintime&&p.WarehouseTime<= endtime);
                    var query = item.RawMaterialId + "," + degintime + "," + endtime;
                    var warehoused = rawmaterialwarehousebll.GetPageList(pagination, query);
                    for (int i = 0; i < warehoused.Count; i++)
                    {
                        RawmaterialWarehouseModel RawmaterialWarehouseModel = new RawmaterialWarehouseModel();
                        RawmaterialWarehouseModel.WarehouseId = warehoused[i].WarehouseId;
                        RawmaterialWarehouseModel.WarehouseQuantity = warehoused[i].WarehouseQuantity;
                        RawmaterialWarehouseModel.WarehouseTime = warehoused[i].WarehouseTime;
                        RawmaterialWarehouseModel.Description = warehoused[i].Description;

                        RawmaterialWarehouseModel.RawMaterialModel = item.RawMaterialModel;
                        RawmaterialWarehouseModel.RawMaterialStandard = item.RawMaterialStandard;
                        RawmaterialWarehouseModel.Category = item.Category;
                        RawmaterialWarehouseModel.Unit = item.Unit;

                        list.Add(RawmaterialWarehouseModel);
                    }

                }
            } catch (Exception e) {
                return ToJsonResult(e);
            }
            
            
           
            return ToJsonResult(list);

        }
            //public ActionResult IntoInventoryDetailInfo(Pagination pagination, string queryJson)
            //{
            //    var data = rawmaterialwarehousebll.GetPageList(pagination, queryJson);
            //    List<RawmaterialWarehouseModel> list = new List<RawmaterialWarehouseModel>();
            //    foreach (var item in data)
            //    {
            //        RawmaterialWarehouseModel RawmaterialWarehouseModel = new RawmaterialWarehouseModel();
            //        var modellib = rawmateriallibrarybll.GetEntity(item.RawMaterialId);

            //        RawmaterialWarehouseModel.WarehouseId = item.WarehouseId;
            //        RawmaterialWarehouseModel.WarehouseQuantity = item.WarehouseQuantity;
            //        RawmaterialWarehouseModel.WarehouseTime = item.WarehouseTime;
            //        RawmaterialWarehouseModel.Description = item.Description;
            //        RawmaterialWarehouseModel.RawMaterialModel = modellib.RawMaterialModel;
            //        RawmaterialWarehouseModel.RawMaterialStandard = modellib.RawMaterialStandard;
            //        RawmaterialWarehouseModel.Category = modellib.Category;
            //        RawmaterialWarehouseModel.Unit = modellib.Unit;

            //        list.Add(RawmaterialWarehouseModel);
            //    }
            //    //}
            //    return ToJsonResult(list);
            //}

           // return ToJsonResult(data);
   // }
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
        public ActionResult RemoveForm(string keyValue)
        {
            rawmaterialwarehousebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, RawMaterialWarehouseEntity entity)
        {
            rawmaterialwarehousebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
