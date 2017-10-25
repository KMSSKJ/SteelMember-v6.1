using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using System.Linq;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-19 10:03
    /// 描 述：材料库存
    /// </summary>
    public class RawMaterialInventoryController : MvcControllerBase
    {
        private RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private RawMaterialPurchaseBLL rawmaterialpurchasebll = new RawMaterialPurchaseBLL();
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialWarehouseBLL rawmaterialwarehousebll = new RawMaterialWarehouseBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();

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
        /// <summary>
        /// 入库界面
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryInfo()
        {
            return View();
        }
        /// <summary>
        ///详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CollarDetail()
        {
            return View();
        }
        /// <summary>
        /// 材料选择表
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemList()
        {
            return View();
        }
        /// <summary>
        ///  领用
        /// </summary>
        /// <returns></returns>
        public ActionResult Collar()
        {
            //ViewBag.CollarNumbering = "CLLYD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
            //ViewBag.CreateMan = OperatorProvider.Provider.Current().UserName;
            return View();
        }
        /// <summary>
        /// 入库详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult IntoInventoryDetail()
        {
            return View();
        }
        /// <summary>
        /// 出库详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OutInventoryDetail()
        {
            return View();
        }

        /// <summary>
        ///数量汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult QuantitySummary()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取还没入库的 Iswarehousing值为0时是没入库的，值为1是已经入库的 IsWarehousing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNoIswarehousing(Pagination pagination)
        {

            var watch = CommonHelper.TimerStart();
            var IsWarehousing = 0;
            var IsPurchase = 1;
            var data = rawmaterialpurchasebll.GetPageListByIsWarehousing(pagination, IsWarehousing).ToList();
            data = data.FindAll(f => f.IsPurchase == IsPurchase);
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
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {

            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var data = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == keyValue);
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModelEntity = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//实际购买量
                    var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModelEntity.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModelEntity.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModelEntity.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModelEntity.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModelEntity.UnitId = entityrawmateriallibrary.Unit;
                    rawMaterialPurchaseModelEntity.Description = entityrawmaterialanalysis.Description;
                    rawMaterialPurchaseModelEntity.RawMaterialName = entityrawmateriallibrary.Category;
                    rawMaterialPurchaseModelEntity.RawMaterialPurchaseModelPrice = item.Price;
                    list.Add(rawMaterialPurchaseModelEntity);
                }
            }
            return ToJsonResult(list);
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public ActionResult Inventory(RawMaterialInventoryEntity Entity, string PurchaseId)
        {

            //string[] RawMaterialPurchaseIds = RawMaterialPurchaseId.Split(',');
            //try
            //{
            //if (RawMaterialPurchaseIds.Length > 0)
            //{
            //    for (var i = 0; i < RawMaterialPurchaseIds.Length; i++)
            //    {
            //        获取到订单下的所有子数据
            //        var data = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == keyValue);
            //        var keyValue = RawMaterialPurchaseIds[i];
            var purchaseinfo = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == PurchaseId);
            var a = 0;
            foreach (var item in purchaseinfo)
            {
                var analysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                // var library = rawmateriallibrarybll.GetEntity(analysis.RawMaterialId);
                if (analysis.RawMaterialId == Entity.RawMaterialId)
                {
                    //先加到入库管理中
                    RawMaterialWarehouseEntity warehouseModel = new RawMaterialWarehouseEntity();
                    string keyValue1 = null;
                    warehouseModel.RawMaterialId = Entity.RawMaterialId;
                    warehouseModel.WarehouseQuantity = Entity.Quantity.ToDecimal();
                    warehouseModel.Description = Entity.Description;
                    warehouseModel.WarehouseTime = System.DateTime.Now;
                    rawmaterialwarehousebll.SaveForm(keyValue1, warehouseModel);

                    //更改库存量
                    // RawMaterialInventoryModel RawMaterialInventoryModel = new RawMaterialInventoryModel();
                    var inventorymodel = rawmaterialinventorybll.GetEntityByRawMaterialId(Entity.RawMaterialId);
                    if (inventorymodel != null)
                    {
                        inventorymodel.Quantity = inventorymodel.Quantity.ToDecimal() + Entity.Quantity.ToDecimal();//库存量++                                                                                              //inventorymodel.
                        inventorymodel.UnitPrice = Entity.UnitPrice;
                        inventorymodel.RawMaterialManufacturer = Entity.RawMaterialManufacturer;
                        inventorymodel.RawMaterialSupplier = Entity.RawMaterialSupplier;
                        rawmaterialinventorybll.SaveForm(inventorymodel.InventoryId, inventorymodel);
                    }

                    ////修改采购订单中入库状态
                    //var purchasemodel = rawmaterialpurchasebll.GetEntity(RawMaterialPurchaseIds[i]);
                    //if (purchasemodel != null)
                    //{
                    //    purchasemodel.IsWarehousing = 1;
                    //    rawmaterialpurchasebll.SavePurchaseForm(purchasemodel.RawMaterialPurchaseId, purchasemodel);
                    //}

                    //修改需求中入库量
                    analysis.WarehousedQuantity = analysis.WarehousedQuantity.ToDecimal() + Entity.Quantity.ToDecimal();
                    rawmaterialanalysisbll.SaveForm(analysis.Id, analysis);
                    //
                    a++;
                }
            }
            var str = "";
            if (a > 0)
            {
                str = "入库成功";
                return Success(str);
            }
            else
            {
                str = "该采购单下无此材料，请检查单号无吴";//或到材料分类管理中添加此材料
                return Error(str);
            }
        }
            /// <summary>
            /// 加载材料
            /// </summary>
            /// <param name="InventoryId"></param>
            /// <returns></returns>
            public ActionResult AddRawMaterial(string InventoryId)
            {
                RawMaterialInventoryModel rawMaterialInventoryModel = new RawMaterialInventoryModel();
                var modelinventory = rawmaterialinventorybll.GetEntity(InventoryId);
                var modellibrary = rawmateriallibrarybll.GetEntity(modelinventory.RawMaterialId);

                rawMaterialInventoryModel.Quantity = Convert.ToDecimal(modelinventory.Quantity);
                rawMaterialInventoryModel.RawMaterialModel = modellibrary.RawMaterialModel;
                rawMaterialInventoryModel.Unit = modellibrary.Unit;
                //rawMaterialInventoryModel.Category = modellibrary.Category;
                rawMaterialInventoryModel.Category = modellibrary.RawMaterialName;


                return ToJsonResult(rawMaterialInventoryModel); ;
            }
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
            var data = rawmaterialinventorybll.GetPageList(pagination, queryJson);//.FindAll(f => f.Quantity > 0);
            //rawmateriallibrarybll.GetEntity(data)
            List<RawMaterialInventoryModel> list = new List<RawMaterialInventoryModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialInventoryModel rawMaterialInventoryModel = new RawMaterialInventoryModel();
                    var rawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    if (rawmateriallibrary != null)
                    {
                        rawMaterialInventoryModel.InventoryId = item.InventoryId;
                        rawMaterialInventoryModel.Quantity = Convert.ToDecimal(item.Quantity);
                        rawMaterialInventoryModel.RawMaterialId = item.RawMaterialId;
                        rawMaterialInventoryModel.Category = rawmateriallibrary.RawMaterialName;
                        rawMaterialInventoryModel.RawMaterialName = rawmateriallibrary.RawMaterialName;
                        rawMaterialInventoryModel.RawMaterialModel = rawmateriallibrary.RawMaterialModel;
                        rawMaterialInventoryModel.Unit = dataitemdetailbll.GetEntity(rawmateriallibrary.Unit).ItemName;

                        list.Add(rawMaterialInventoryModel);
                    }

                }
            }

            var queryParam = queryJson.ToJObject();
          
            if (!queryParam["RawMaterialName"].IsEmpty())
            {
                string RawMaterialName = queryParam["RawMaterialName"].ToString();
                list = list.FindAll(t => t.RawMaterialName.Contains(RawMaterialName));
            }
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString();
                list = list.FindAll(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }


            var jsonData = new
            {
                rows = list,
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
            var data = rawmaterialinventorybll.GetList(queryJson);
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
            var data = rawmaterialinventorybll.GetEntity(keyValue);
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
            rawmaterialinventorybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, RawMaterialInventoryEntity entity)
        {
            rawmaterialinventorybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
