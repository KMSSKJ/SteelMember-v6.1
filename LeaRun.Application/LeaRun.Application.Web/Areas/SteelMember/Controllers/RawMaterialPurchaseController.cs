using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Linq;
using LeaRun.Util.Extension;
using System;
using LeaRun.Application.Code;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Busines.BaseManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-08 11:58
    /// 描 述：材料采购管理
    /// </summary>
    public class RawMaterialPurchaseController : MvcControllerBase
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
        public ActionResult ItemList()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form(string keyValue)
        {
            if (keyValue == "" || keyValue == null)
            {

                ViewData["PurchaseNumbering"] = "CGDD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;

            }
            return View();
        }

        //[HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Purchase()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotReviewForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailRawMaterialPurchase(string rawMaterialPurchaseId)
        {
            var entityrawMaterialPurchaseId = rawmaterialpurchasebll.GetEntity(rawMaterialPurchaseId);
            //entityrawMaterialPurchaseId.CreateTime; 
            var data = ToJsonResult(entityrawMaterialPurchaseId);
            return data;
        }

        /// <summary>
        /// 获取申请中审核通过的材料  IsPassed=1表示通过
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetListByIsPassed(Pagination pagination, string queryJson, string keyValue)
        {
            var data = new List<RawMaterialLibraryModel>();
            //var list = rawmaterialorderbll.GetPageList(pagination, queryJson);
            //if (list.Count() > 0)
            //{
            //    foreach (var item in list)
            //    {
            //        var rawmaterialorderinfolist = rawmaterialorderinfobll.GetList(f => f.OrderId == item.OrderId);
            //        if (rawmaterialorderinfolist.Count() > 0)
            //        {
            //            foreach (var item1 in rawmaterialorderinfolist)
            //            {
            //                if (data.Count() > 0)
            //                {
            //                    var RawMaterial = data.Where(w => w.RawMaterialId == item1.RawMaterialId).SingleOrDefault();
            //                    if (RawMaterial != null)
            //                    {
            //                        RawMaterial.Qty += item1.Quantity.ToDecimal();
            //                    }
            //                    else
            //                    {
            //                        var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
            //                        var _data = new RawMaterialLibraryModel()
            //                        {
            //                            RawMaterialId = model.RawMaterialId,
            //                            Qty = item1.Quantity,
            //                            InfoId = item1.InfoId,
            //                            RawMaterialName = model.RawMaterialName,
            //                            RawMaterialModel = model.RawMaterialModel,
            //                            Category = model.Category,
            //                            UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
            //                            Quantity = 0,
            //                            Date = item1.Time.ToDate(),
            //                            //RawMaterialSupplier = item1.RawMaterialSupplier,
            //                            Description = model.Description,
            //                        };
            //                        data.Add(_data);
            //                    }
            //                }
            //                else
            //                {
            //                    var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
            //                    var _data = new RawMaterialLibraryModel()
            //                    {
            //                        RawMaterialId = model.RawMaterialId,
            //                        Qty = item1.Quantity,
            //                        InfoId = item1.InfoId,
            //                        Quantity = 0,
            //                        Date = item1.Time.ToDate(),
            //                        RawMaterialName = model.RawMaterialName,
            //                        RawMaterialModel = model.RawMaterialModel,
            //                        Category = model.Category,
            //                        UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
            //                        Description = model.Description,
            //                    };
            //                    data.Add(_data);
            //                }
            //            }
            //        }
            //    }
            //}
            var list = rawmaterialanalysisbll.GetPageList1(f => f.IsPassed==1, pagination);
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    var _data = new RawMaterialLibraryModel()
                    {
                        InfoId = item.Id,
                        RawMaterialId = model.RawMaterialId,
                        Qty = item.RawMaterialDosage,
                        RawMaterialName = model.RawMaterialName,
                        RawMaterialModel = model.RawMaterialModel,
                        Category = model.Category,
                        UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                        Description = model.Description,
                    };
                    data.Add(_data);
                }
            }

            var datapurchase = new List<RawMaterialLibraryModel>();
            var listpurchase = rawmaterialpurchasebll.GetList("").ToList(); //f => f.IsPurchase == 1
            if (listpurchase.Count() > 0)
            {
                foreach (var item in listpurchase)
                {
                    var rawmaterialorderpurchaselist = rawmaterialpurchasebll.GetInfoList(f => f.RawMaterialPurchaseId == item.RawMaterialPurchaseId);
                    if (rawmaterialorderpurchaselist.Count() > 0)
                    {
                        foreach (var item1 in rawmaterialorderpurchaselist)
                        {
                            if (datapurchase.Count() > 0)
                            {
                                var RawMaterial = datapurchase.Where(w => w.RawMaterialId == item1.RawMaterialId).SingleOrDefault();
                                if (RawMaterial != null)
                                {
                                    RawMaterial.Qty += item1.PurchaseQuantity.ToDecimal();
                                }
                                else
                                {
                                    var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                                    var _data = new RawMaterialLibraryModel()
                                    {
                                        RawMaterialId = model.RawMaterialId,
                                        Qty = item1.PurchaseQuantity,
                                        InfoId = item1.InfoId,
                                        RawMaterialName = model.RawMaterialName,
                                        RawMaterialModel = model.RawMaterialModel,
                                        Category = model.Category,
                                        UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                                        Quantity = 0,
                                        RawMaterialSupplier = item1.RawMaterialSupplier,
                                        Description = model.Description,
                                    };
                                    datapurchase.Add(_data);
                                }
                            }
                            else
                            {
                                var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                                var _data = new RawMaterialLibraryModel()
                                {
                                    RawMaterialId = model.RawMaterialId,
                                    Qty = item1.PurchaseQuantity,
                                    Quantity = 0,
                                    InfoId = item1.InfoId,
                                    RawMaterialName = model.RawMaterialName,
                                    RawMaterialModel = model.RawMaterialModel,
                                    Category = model.Category,
                                    UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                                    //Date = item1.Time.ToDate(),
                                    Description = model.Description,
                                };
                                datapurchase.Add(_data);
                            }
                        }
                    }
                }
            }

            if (data.Count() > 0)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    var datapurchaseEntity = datapurchase.Where(w => w.RawMaterialId == data[i].RawMaterialId).SingleOrDefault();

                    if (datapurchaseEntity != null)
                    {
                        if (!keyValue.IsEmpty())
                        {
                            var RawMaterialId = data[i].RawMaterialId;
                            var rawmaterialorderpurchaseEntity = rawmaterialpurchasebll.GetInfoList(f => f.RawMaterialId == RawMaterialId && f.RawMaterialPurchaseId == keyValue).SingleOrDefault();
                            data[i].PurchasedQuantity = datapurchaseEntity.Qty + rawmaterialorderpurchaseEntity.PurchaseQuantity;
                            
                        }
                        else
                        {
                            data[i].PurchasedQuantity = datapurchaseEntity.Qty;
                            // data[i].Qty = data[i].Qty - datapurchaseEntity.Qty;
                        }
                    }
                    if (data[i].PurchasedQuantity == null)
                    {
                        data[i].PurchasedQuantity = 0;
                    }
                }
                data = data.Where(f => f.Qty != f.PurchasedQuantity).ToList();//把已采购的移除
            }
            var queryParam = queryJson.ToJObject();
            if (!queryParam["MaterialCategory"].IsEmpty())
            {
                string MaterialCategory = queryParam["MaterialCategory"].ToString();
                data = data.FindAll(t => t.Category == MaterialCategory);
                foreach (var item in data)
                {
                    //item.Category = dataitemdetailbll.GetEntity(item.Category).ItemName;
                    item.Quantity = item.Qty.ToDecimal() - item.PurchasedQuantity.ToDecimal();
                }
            }

            return ToJsonResult(data);
        }
        public ActionResult ListMember(string InfoId, string RawMaterialId, string RawMaterialOrderInfoId, string RawMaterialName,
            string RawMaterialModel, string Price, string Quantity, string PurchasedQuantity, string UnitId, string Description, string RawMaterialSupplier, string RawMaterialManufacturer)
        {
            // var inventory = 0; //库存量
            var listmember = new List<Text>();
            if (RawMaterialId != null && RawMaterialId != "")
            {
                string[] arrayAnalysisId = InfoId.Split(',');
                string[] arrayRawMaterialId = RawMaterialId.Split(',');
                //string[] arrayRawMaterialOrderInfoId = RawMaterialOrderInfoId.Split(',');
                string[] arrayRawMaterialName = RawMaterialName.Split(',');
                string[] arrayRawMaterialModel = RawMaterialModel.Split(',');
                //string[] arrayRawMaterialPrice = Price.Split(',');
                string[] arrayRawMaterialQuantity = Quantity.Split(',');
                string[] arrayRawMaterialPurchasedQuantity = PurchasedQuantity.Split(',');
                string[] arrayUnitId = UnitId.Split(',');
                string[] arrayDescription = Description.Split(',');
                string[] arrayRawMaterialSupplier = RawMaterialSupplier.Split(',');
                //string[] arrayRawMaterialManufacturer = RawMaterialManufacturer.Split(',');
                if (arrayAnalysisId.Count() > 0)
                {
                    if (arrayAnalysisId != null)
                        for (int i = 0; i < arrayAnalysisId.Length; i++)
                        {
                            // var sb = arrayRawMaterialId[i];
                            //var rawmaterialinventory = rawmaterialinventorybll.GetEntityByRawMaterialId(arrayRawMaterialId[i].ToString());
                            //var inventory = rawmaterialinventory.Quantity.ToDecimal();//需申请量
                            Text projectdemand = new Text()
                            {
                                PurchasedQuantity = arrayRawMaterialPurchasedQuantity[i],
                                Quantity = arrayRawMaterialQuantity[i].ToDecimal(),
                                RawMaterialId = arrayRawMaterialId[i].ToString(),
                                InfoId = arrayAnalysisId[i],
                                //RawMaterialPurchaseId = arrayRawMaterialId[i],
                                RawMaterialName = arrayRawMaterialName[i],
                                RawMaterialModel = arrayRawMaterialModel[i],
                                UnitId = arrayUnitId[i],
                                //Price = arrayRawMaterialPrice[i].ToDecimal(),
                                Description = arrayDescription[i],
                                //RawMaterialSupplier = arrayRawMaterialSupplier[i],
                                //RawMaterialManufacturer = arrayRawMaterialManufacturer[i],
                            };
                            listmember.Add(projectdemand);
                        }
                }
            }
            else
            {
                return Json(listmember);
            }
            return Json(listmember);
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
            var data = rawmaterialpurchasebll.GetPageList(pagination, queryJson);
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
        /// 编辑获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmaterialpurchasebll.GetEntity(keyValue);
            var childData = rawmaterialpurchasebll.GetDetails(keyValue).ToList();
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            if (childData.Count > 0)
            {
                foreach (var item in childData)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//实际购买量
                    //var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    //var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == item.RawMaterialId);
                    //rawMaterialPurchaseModel.Quantity = rawmaterialpurchasebll.GetEntity(f => f.RawMaterialAnalysisId == item.RawMaterialAnalysisId).PurchaseQuantity;
                    //rawMaterialPurchaseModel.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialId = entityrawmateriallibrary.RawMaterialId;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Description = item.Description;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Quantity = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.PurchasedQuantity = item..ToString();
                    list.Add(rawMaterialPurchaseModel);

                }
            }

            var jsonData = new
            {
                entity = data,
                childEntity = list
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson1(string keyValue)
        {
            var data = rawmaterialpurchasebll.GetEntity(keyValue);

            var childData = rawmaterialpurchasebll.GetDetails(keyValue).ToList();
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            //var rawmaterialorder = new RawMaterialOrderEntity();
            if (childData.Count > 0)
            {

                foreach (var item in childData)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//实际购买量
                    //var rawMaterialAnalysisId = item.RawMaterialOrderInfoId;

                   // var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    var entityrawmaterialinventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == item.RawMaterialId);

                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                   // rawMaterialPurchaseModel.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialId = entityrawmateriallibrary.RawMaterialId;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Description = item.Description;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialSupplier =organizebll.GetEntity(item.RawMaterialSupplier).FullName;

                    //rawMaterialPurchaseModel.Quantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }
            var jsonData = new
            {
                entity = data,
                childEntity = list
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
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//实际购买量
                    //var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    //var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Category = entityrawmateriallibrary.Category;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Inventory = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.SuggestQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }

            return ToJsonResult(list);

        }

        /// <summary>
        /// 获取子表详细信息(材料入库时)
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson1(string keyValue)
        {
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var rawmaterialpurchase = rawmaterialpurchasebll.GetEntity(keyValue);
            var data = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == rawmaterialpurchase.RawMaterialPurchaseId);
            var CategoryName = "";
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//实际购买量
                    //var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    //var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    CategoryName = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Category).ItemName;
                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    //rawMaterialPurchaseModel.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Category = entityrawmateriallibrary.Category;
                    rawMaterialPurchaseModel.RawMaterialId = entityrawmateriallibrary.RawMaterialId;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Inventory = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.SuggestQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }

            var jsonData = new
            {
                CategoryName = CategoryName,
                childEntity = data
            };

            return ToJsonResult(jsonData);

        }

        /// <summary>
        /// 获取未入库
        /// </summary>

        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetPurchaseList()
        {
            var data = rawmaterialpurchasebll.GetList("").ToList().FindAll(f => f.IsWarehousing != 1);
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
        // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            rawmaterialpurchasebll.RemoveForm(keyValue);
            return Success("删除成功。");

        }


        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity">实体对象</param>
        /// <param name="strChildEntitys">子表对象集</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            //return Success("操作成功。");
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
            entity.IsSubmit = entity.IsSubmit.IsEmpty() ? 0 : entity.IsSubmit;
            entity.IsWarehousing = entity.IsWarehousing.IsEmpty() ? 0 : entity.IsWarehousing;
            entity.CreateMan = entity.CreateMan.IsEmpty() ? "测试管理员" : entity.CreateMan;
            entity.IsPassed = 0;
            entity.ReviewMan1 = entity.ReviewMan2 = entity.ReviewMan3 = entity.ReviewMan4 = "0";
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            //foreach (var item in childEntitys)
            //{
            //    item.RawMaterialSupplier = dataitemdetailbll.GetDataItemList().Where(s => s.ItemName.Trim() == item.RawMaterialSupplier.Trim()).SingleOrDefault()?.ItemDetailId;
            //}

            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存已采购
        /// </summary>  获取分析表中审核通过的材料  IsPassed=1表示通过
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity">实体对象</param>
        /// <param name="strChildEntitys">子表对象集</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult PurchaseSaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            //Session
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
           // entity.IsPurchase = 1;
            entity.IsPassed = 1;
            entity.IsSubmit = 1;
            entity.IsWarehousing = entity.IsWarehousing.IsEmpty() ? 0 : entity.IsWarehousing;
            //entity.IsWarehousing = 1;
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未提交；1提交</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SubmitReview(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmit = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }
        /// <summary>
        /// 审核处理
        /// </summary>
        /// <param name="keyValue">要审核的数据的主键些</param>
        /// <param name="entity"></param>
        /// <param name="operat"></param>
        /// <param name="type">1通过，2失败</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReviewOperation(string keyValue, RawMaterialPurchaseEntity entity,int operat, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        if (type==1)
                        {
                        if (operat==1)
                        {
                            model.ReviewMan1 = OperatorProvider.Provider.Current().UserName;
                            model.ReviewDescription = "工程部长审核通过";
                        }
                        else if (operat == 2)
                        {
                            model.ReviewMan2 = OperatorProvider.Provider.Current().UserName;
                            model.ReviewDescription = "物质部长审核通过";
                        }
                        else if (operat ==3)
                        {
                            model.ReviewMan3 = OperatorProvider.Provider.Current().UserName;
                            model.ReviewDescription = "副总工审核通过";
                        }
                        else 
                        {
                            model.ReviewMan4 = OperatorProvider.Provider.Current().UserName;
                            model.IsPassed = 1;
                            model.ReviewDescription = "总工审核通过";
                        }
                        }
                        else
                        {
                            if (operat == 1)
                            {
                                model.ReviewMan1 ="2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "工程部长审核失败";
                            }
                            else if (operat == 2)
                            {
                                model.ReviewMan2 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "物质部长审核失败";
                            }
                            else if (operat == 3)
                            {
                                model.ReviewMan3 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "副总工审核失败";
                            }
                            else
                            {
                                model.ReviewMan4 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "总工审核失败";
                            }
                        }
                        model.Description = entity.Description;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 提交采购
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未采购；1已采购</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitIsPurchase(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsWarehousing = 1;
                        list.Add(model);

                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                    ////将已采购的加入材料库
                    //for (var i = 0; i < ids.Length; i++)
                    //{
                    //    try
                    //    {
                    //        var model = rawmaterialpurchasebll.GetEntity(ids[i]);
                    //        //先取到采购单据
                    //        if (model.RawMaterialPurchaseId != null)
                    //        {
                    //            //再拿去订单详情取到每一条
                    //            var listInfo = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == model.RawMaterialPurchaseId && model.IsWarehousing == 1);
                    //            //更改需求量
                    //            for (var j = 0; j < listInfo.Count; j++)
                    //            {
                    //                var RawMaterialAnalysisId = listInfo[j].RawMaterialAnalysisId;
                    //                //var Category= listInfo[j].Category;
                    //                var rawmaterialanalysis = rawmaterialanalysisbll.GetList(f=>f.Id == RawMaterialAnalysisId).SingleOrDefault();//通过分析ID取得分析数据
                    //                rawmaterialanalysis.PurchasedQuantity = rawmaterialanalysis.PurchasedQuantity.ToDecimal() + listInfo[j].PurchaseQuantity;
                    //                rawmaterialanalysisbll.SaveForm(listInfo[j].RawMaterialAnalysisId, rawmaterialanalysis);
                    //            }
                    //        }
                    //    }
                    //    catch (System.Exception e)
                    //    {
                    //        throw (e);
                    //    }
                    //}

                }
            }
            return Success("操作成功。");
        }
        #endregion
    }
}