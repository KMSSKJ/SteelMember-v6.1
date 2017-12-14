using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Util.Extension;
using System.Linq;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 17:17
    /// 描 述：入库管理
    /// </summary>
    public class RawMaterialWarehouseController : MvcControllerBase
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
        public ActionResult IntoInventoryDetailInfo(Pagination pagination, string queryJson, string category)
        {
            if (pagination.sidx == "RawMaterialName")
            {
                pagination.sidx = "RawMaterialId";
            }

            List<RawMaterialWarehouseModel> list = new List<RawMaterialWarehouseModel>();
            // var data = rawmateriallibrarybll.GetPageListByLikeCategory(pagination, category);
            var data = rawmaterialwarehousebll.GetPageList(pagination, category);
            foreach (var item in data)
            {
                //var warehoused = rawmaterialwarehousebll.GetPageList(pagination, item.RawMaterialId);
                var warehoused = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                if (warehoused != null)
                {
                    RawMaterialWarehouseModel RawmaterialWarehouseModel = new RawMaterialWarehouseModel()
                    {
                        WarehouseId = item.WarehouseId,
                        WarehouseQuantity = item.WarehouseQuantity,
                        WarehouseTime = item.WarehouseTime,
                        Description = item.Description,
                        RawMaterialModel = warehoused.RawMaterialModel,
                        RawMaterialName = warehoused.RawMaterialName,
                        //RawMaterialSupplier = organizebll.GetEntity(item.RawMaterialSupplier).FullName,
                        Unit = dataitemdetailbll.GetEntity(warehoused.Unit).ItemName,
                    };
                    list.Add(RawmaterialWarehouseModel);
                }
            }

            //
            var queryParam = queryJson.ToJObject();
            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.WarehouseTime >= BeginTime);
                list = list.FindAll(t => t.WarehouseTime <= EndTime);
            }
            if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.WarehouseTime >= BeginTime);
            }
            if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.WarehouseTime <= EndTime);
            }
            if (!queryParam["RawMaterialName"].IsEmpty())
            {
                string RawMaterialName = queryParam["RawMaterialName"].ToString().Trim();
                list = list.FindAll(t => t.RawMaterialName.Contains(RawMaterialName));
            }
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString().Trim();
                list = list.FindAll(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }

            //if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            //{
            //    string condition = queryParam["condition"].ToString();
            //    string keyword = queryParam["keyword"].ToString();
            //    switch (condition)
            //    {

            //        //case "Category":              //构件类型
            //        //    expression = expression.And(t => t.Category.Contains(keyword));
            //        //    break;
            //        case "RawMaterialName":              //构件名称
            //            list = list.FindAll(t => t.RawMaterialName.Contains(keyword));
            //            break;
            //        case "RawMaterialModel":              //牌号/规格
            //            list = list.FindAll(t => t.RawMaterialModel.Contains(keyword));
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //

            return ToJsonResult(list);

        }

        /// <summary>
        /// 入库详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoQuantitySummaryList(/*Pagination pagination,*/ string queryJson/*, string category*/)
        {
            var queryParam = queryJson.ToJObject();
            var RawMaterialSupplier = queryParam["RawMaterialSupplier"].ToString();
            List<RawMaterialWarehouseModel> list = new List<RawMaterialWarehouseModel>();
            var data = rawmaterialwarehousebll.GetList(f => f.RawMaterialSupplier == RawMaterialSupplier);
            foreach (var item in data)
            {
                var warehoused = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                if (warehoused != null)
                {
                    RawMaterialWarehouseModel RawmaterialWarehouseModel = new RawMaterialWarehouseModel()
                    {
                        WarehouseId = item.WarehouseId,
                        WarehouseQuantity = item.WarehouseQuantity,
                        WarehouseTime = item.WarehouseTime,
                        Description = item.Description,
                        RawMaterialId = warehoused.RawMaterialId,
                        RawMaterialModel = warehoused.RawMaterialModel,
                        RawMaterialName = warehoused.RawMaterialName,
                        // RawMaterialManufacturer = item.RawMaterialManufacturer,
                        RawMaterialSupplier = organizebll.GetEntity(item.RawMaterialSupplier).FullName,
                        Unit = dataitemdetailbll.GetEntity(warehoused.Unit).ItemName,
                    };
                    if (list.Count() < 0)
                    {
                        list.Add(RawmaterialWarehouseModel);
                    }
                    else if (list.Find(f => f.RawMaterialId == RawmaterialWarehouseModel.RawMaterialId).IsEmpty())
                    {
                        list.Add(RawmaterialWarehouseModel);
                    }
                    else
                    {
                        var a = list.Find(f => f.RawMaterialId == RawmaterialWarehouseModel.RawMaterialId);
                        a.WarehouseQuantity += RawmaterialWarehouseModel.WarehouseQuantity;
                    }
                }
            }

            //

            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.WarehouseTime >= BeginTime);
                list = list.FindAll(t => t.WarehouseTime <= EndTime);
            }
            if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.WarehouseTime >= BeginTime);
            }
            if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.WarehouseTime <= EndTime);
            }
            if (!queryParam["RawMaterialName"].IsEmpty())
            {
                string RawMaterialName = queryParam["RawMaterialName"].ToString().Trim();
                list = list.FindAll(t => t.RawMaterialName.Contains(RawMaterialName));
            }
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString().Trim();
                list = list.FindAll(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }

            var jsonData = new
            {
                // entity = data,
                Date = queryParam["BeginTime"].ToString().Replace("-", "/") + "--" + queryParam["EndTime"].ToString().Replace("-", "/"),
                RawMaterialSupplier = organizebll.GetEntity(RawMaterialSupplier).FullName,
                Count = list.Count(),
                childEntity = list
            };
            return ToJsonResult(jsonData);
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
            string[] idsArr = keyValue.Split(',');
            foreach (var item in idsArr)
            {
                var rawmaterialwarehouse = rawmaterialwarehousebll.GetEntity(item);
                rawmaterialwarehousebll.RemoveForm(item);
                var rawmaterialinventory = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == rawmaterialwarehouse.RawMaterialId);
                rawmaterialinventory.Quantity = rawmaterialinventory.Quantity - rawmaterialwarehouse.WarehouseQuantity;
                rawmaterialinventorybll.SaveForm(rawmaterialinventory.InventoryId, rawmaterialinventory);//修改库存
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
        public ActionResult SaveForm(string keyValue, RawMaterialWarehouseEntity entity)
        {
            rawmaterialwarehousebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
