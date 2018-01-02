using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using LeaRun.Application.Code;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Collections.Generic;
using System;
using LeaRun.Util.Extension;
using LeaRun.Application.Busines.SystemManage;
using System.Data.OleDb;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using LeaRun.Util.Offices;
using LeaRun.Application.Busines.BaseManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-08-07 17:28
    /// 描 述：材料订单
    /// </summary>
    public class RawMaterialOrderController : MvcControllerBase
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
        public ActionResult Form(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "CLSQD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;
                ViewData["CreateTime"] = DateTime.Now;
            }
            return View();
        }
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormDetail()
        {
            return View();
        }
        /// <summary>
        ///材料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ItemList()
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = rawmaterialorderbll.GetPageList(pagination, queryJson);
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
            var data = rawmaterialorderbll.GetList(queryJson);
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
            var data = rawmaterialorderbll.GetEntity(keyValue);
            var childData = rawmaterialorderinfobll.GetList(f => f.OrderId == keyValue).ToList();
            var MemberList = new List<MemberMaterialModel>();
            for (int i = 0; i < childData.Count(); i++)
            {
                var rawmaterial = rawmateriallibrarybll.GetEntity(childData[i].RawMaterialId);
                var Member = new MemberMaterialModel()
                {
                    InfoId = childData[i].InfoId,
                    RawMaterialId = childData[i].RawMaterialId,
                    RawMaterialAnalysisId = childData[i].RawMaterialAnalysisId,
                    RawMaterialNumber = childData[i].Quantity,
                    RawMaterialName = rawmaterial.RawMaterialName,
                    RawMaterialModel = rawmaterial.RawMaterialModel,
                    UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
                };
                MemberList.Add(Member);
            }
            var jsonData = new
            {
                entity = data,
                childEntity = MemberList
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
            var data = rawmaterialorderbll.GetEntity(keyValue);
            var MemberList = new List<MemberMaterialModel>();
            if (data != null)
            {
                data.Category = subprojectbll.GetEntity(data.Category).FullName;
                data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;

                var childData = rawmaterialorderinfobll.GetList(f => f.OrderId == keyValue).ToList();
                for (int i = 0; i < childData.Count(); i++)
                {
                    var rawmaterial = rawmateriallibrarybll.GetEntity(childData[i].RawMaterialId);
                    var Member = new MemberMaterialModel()
                    {
                        InfoId = childData[i].InfoId,
                        RawMaterialId = childData[i].RawMaterialId,
                        RawMaterialAnalysisId = childData[i].RawMaterialAnalysisId,
                        RawMaterialNumber = childData[i].Quantity,
                        RawMaterialName = rawmaterial.RawMaterialName,
                        RawMaterialModel = rawmaterial.RawMaterialModel,
                        UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
                    };
                    MemberList.Add(Member);
                }
            }



            var jsonData = new
            {
                entity = data,
                childEntity = MemberList
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        ///加载材料
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="category"></param>
        /// <returns>返回对象Json</returns>
        ///  [HttpGet]
        public ActionResult GridListJsonRawAnalysis(Pagination pagination, string category)
        {
            var data = new List<RawMaterialLibraryModel>();
            var RawMaterialAnalysis = rawmaterialanalysisbll.GetPageList1(f => f.Category == category && f.IsPassed == 1, pagination).ToList();//.OrderByDescending(o => o.MemberNumbering)
            var RawMaterialOrder = rawmaterialorderbll.GetList(f => f.Category == category);
            foreach (var item in RawMaterialAnalysis)
            {
                decimal Number = 0;
                foreach (var item1 in RawMaterialOrder)
                {
                    var RawMaterialOrderinfo = rawmaterialorderinfobll.GetList(f => f.RawMaterialId == item.RawMaterialId && f.OrderId == item1.OrderId).SingleOrDefault();
                    if (RawMaterialOrderinfo == null)
                    {
                        var a = 0;
                        Number += a;
                    }
                    else if (RawMaterialOrderinfo.RawMaterialId == item.RawMaterialId)
                    {
                        Number += RawMaterialOrderinfo.Quantity.ToDecimal();
                    }
                }

                var RawMaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == item.RawMaterialId).SingleOrDefault();
                RawMaterialLibraryModel RawMaterialLibrary = new RawMaterialLibraryModel()
                {
                    RawMaterialId = RawMaterial.RawMaterialId,
                    Category = dataitemdetailbll.GetEntity(RawMaterial.Category).ItemName,
                    CategoryId = RawMaterial.Category,
                    RawMaterialAnalysisId = item.Id,
                    RawMaterialModel = RawMaterial.RawMaterialModel,
                    RawMaterialName = RawMaterial.RawMaterialName,
                    UnitId = dataitemdetailbll.GetEntity(RawMaterial.Unit).ItemName,
                    Qty = item.RawMaterialDosage+item.ChangeQuantity.ToDecimal() - Number,//可申请量                                      //PurchasedQuantity = item.PurchasedQuantity
                };
                if (RawMaterialLibrary.Qty > 0)
                {
                    data.Add(RawMaterialLibrary);//可申请量大于0就加入
                }
            }
            return ToJsonResult(data);
        }

        /// <summary>
        /// 控制订单的数量（新增）
        /// </summary>
        /// <returns></returns>
        public ContentResult AddRawMaterialNumber(string KeyValue, string category)
        {
            var MemberDemand = rawmaterialanalysisbll.GetList(s => s.Category == category && s.RawMaterialId == KeyValue).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;
            var Order = rawmaterialorderbll.GetList(f => f.Category == category);
            foreach (var item in Order)
            {
                var MemberOrder = rawmaterialorderinfobll.GetList(f => f.OrderId == item.OrderId && f.RawMaterialId == KeyValue).SingleOrDefault();
                if (MemberOrder != null)
                {
                    Number += Convert.ToInt32(MemberOrder.Quantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.RawMaterialDosage) - Number;

            return Content(MemberDemandNumber.ToString());
        }

        /// <summary>
        /// 控制订单构件的数量(编辑)
        /// </summary>
        /// <returns></returns>
        public ContentResult EditRawMaterialNumber(string KeyValue, string MemberId)
        {

            var OrderList = new List<RawMaterialOrderEntity>();
            var Order = rawmaterialorderbll.GetList(KeyValue).SingleOrDefault();
            OrderList = rawmaterialorderbll.GetList(Order.Category).ToList();

            var MemberDemand = rawmaterialanalysisbll.GetList(f => f.Category == Order.Category && f.RawMaterialId == MemberId).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;

            foreach (var item in OrderList)
            {
                var OrderMember = rawmaterialorderinfobll.GetList(f => f.OrderId == item.OrderId && f.RawMaterialId == MemberId).SingleOrDefault();
                if (OrderMember != null)
                {
                    Number += Convert.ToInt32(OrderMember.Quantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.RawMaterialDosage) - Number;

            return Content(MemberDemandNumber.ToString());
        }
        
#pragma warning disable CS1572 // XML 注释中有“category”的 param 标记，但是没有该名称的参数
#pragma warning disable CS1572 // XML 注释中有“KeyValue”的 param 标记，但是没有该名称的参数
/// <summary>
        /// 载入添加后的构件
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        //public ActionResult ListMember(string KeyValue, string category)
        //{
        //    var listmember = new List<RawMaterialLibraryModel>();
        //    if (KeyValue != null)
        //    {
        //        string[] array = KeyValue.Split(',');
        //        if (array.Count() > 0)
        //        {
        //            if (array != null)
        //                foreach (var item in array)
        //                {
        //                    var a = rawmaterialanalysisbll.GetList(f => f.Category == category && f.RawMaterialId == item).SingleOrDefault();
        //                    var rawmaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == a.RawMaterialId).SingleOrDefault();
        //                    var rawmaterialInventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == a.RawMaterialId);
        //                    RawMaterialLibraryModel projectdemand = new RawMaterialLibraryModel()
        //                    {
        //                        RawMaterialId = rawmaterial.RawMaterialId,
        //                        RawMaterialAnalysisId = a.Id,
        //                        RawMaterialModel = rawmaterial.RawMaterialModel,
        //                        RawMaterialName = rawmaterial.RawMaterialName,
        //                        UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
        //                        Qty = a.RawMaterialDosage,
        //                        InventoryQty = Convert.ToDecimal(rawmaterialInventory.Quantity),
        //                        InventoryId = rawmaterialInventory.InventoryId,
        //                    };
        //                    listmember.Add(projectdemand);
        //                }
        //        }
        //    }
        //    else
        //    {
        //        listmember = null;
        //    }
        //    return Json(listmember);
        //}

#pragma warning disable CS1573 // 参数“RawMaterialId”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1573 // 参数“RawMaterialName”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1573 // 参数“RawMaterialModel”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1573 // 参数“RawMaterialAnalysisId”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1573 // 参数“UnitId”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1573 // 参数“Qty”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1573 // 参数“RawMaterialCategory”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        public ActionResult ListMember(string RawMaterialAnalysisId, string RawMaterialId, string RawMaterialCategory, string RawMaterialName, string RawMaterialModel, string UnitId, string Qty)
#pragma warning restore CS1573 // 参数“RawMaterialCategory”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1573 // 参数“Qty”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1573 // 参数“UnitId”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1573 // 参数“RawMaterialAnalysisId”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1572 // XML 注释中有“KeyValue”的 param 标记，但是没有该名称的参数
#pragma warning restore CS1573 // 参数“RawMaterialModel”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1572 // XML 注释中有“category”的 param 标记，但是没有该名称的参数
#pragma warning restore CS1573 // 参数“RawMaterialName”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1573 // 参数“RawMaterialId”在“RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        {
            // var inventory = 0; //库存量
            var listmember = new List<Text>();
            if (RawMaterialId != null && RawMaterialId != "")
            {
                string[] arrayRawMaterialAnalysisId = RawMaterialAnalysisId.Split(',');
                string[] arrayRawMaterialId = RawMaterialId.Split(',');
                string[] arrayRawMaterialCategory = RawMaterialCategory.Split(',');
                string[] arrayRawMaterialName = RawMaterialName.Split(',');
                string[] arrayRawMaterialModel = RawMaterialModel.Split(',');
                string[] arrayUnitId = UnitId.Split(',');
                string[] arrayRawMaterialQty = Qty.Split(',');
               
                if (arrayRawMaterialId.Count() > 0)
                {
                    if (arrayRawMaterialAnalysisId != null)
                        for (int i = 0; i < arrayRawMaterialAnalysisId.Length; i++)
                        {
                            // var sb = arrayRawMaterialId[i];
                            //var rawmaterialinventory = rawmaterialinventorybll.GetEntityByRawMaterialId(arrayRawMaterialId[i].ToString());
                            //var inventory = rawmaterialinventory.Quantity.ToDecimal();//需申请量
                            Text projectdemand = new Text()
                            {
                                RawMaterialAnalysisId= arrayRawMaterialAnalysisId[i],
                                RawMaterialId= arrayRawMaterialId[i],
                                RawMaterialCategory= arrayRawMaterialCategory[i],
                                RawMaterialName= arrayRawMaterialName[i],
                                RawMaterialModel= arrayRawMaterialModel[i],
                                UnitId= arrayUnitId[i],
                                Qty= arrayRawMaterialQty[i].ToDecimal(),
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
            string[] idsArr = keyValue.Split(',');
            int number = 0;
            foreach (var item1 in idsArr)
            {
                var meminfo = rawmaterialorderinfobll.GetList(f => f.OrderId == item1);
                if (meminfo.Count() > 0)
                {
                    foreach (var item in meminfo)
                    {
                        var rawmaterialPurchaseinfo = rawmaterialpurchasebll.GetInfoList(f => f.InfoId == item.InfoId);
                        number = rawmaterialPurchaseinfo.Count();
                        if (number == 0)
                        {
                            rawmaterialorderinfobll.RemoveForm(item.InfoId);
                        }
                        else
                        {
                            return Error("数据中存在关联数据");
                        }
                    }
                }
                if (number == 0)
                {
                    rawmaterialorderbll.RemoveForm(keyValue);
                }
                else
                {
                    return Error("数据中存在关联数据");
                }

            }

            return Success("删除成功。");
        }
        ///// <summary>
        ///// 保存表单（新增、修改）
        ///// </summary>
        ///// <param name="keyValue">主键值</param>
        ///// <param name="entity">实体对象</param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AjaxOnly]
        //public ActionResult SaveForm(string keyValue, RawMaterialOrderEntity entity)
        //{
        //    rawmaterialorderbll.SaveForm(keyValue, entity);
        //    return Success("操作成功。");
        //}

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
            var entity = strEntity.ToObject<RawMaterialOrderEntity>();

            entity.IsPassed = entity.IsSubmit = 0;
            entity.ReviewMan1 = entity.ReviewMan2 = entity.ReviewMan3 = entity.ReviewMan4 = entity.ReviewMan5 = "0";
            List<RawMaterialOrderInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialOrderInfoEntity>();
            rawmaterialorderbll.SaveForm(keyValue, entity, childEntitys);

            return Success("操作成功。");
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未提交；1提交</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SubmitReview(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialOrderEntity> list = new List<RawMaterialOrderEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmit = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialorderbll.UpdataList(list);
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
        public ActionResult ReviewOperation(string keyValue, RawMaterialOrderEntity entity,int operat, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialOrderEntity> list = new List<RawMaterialOrderEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        if (type == 1)
                        {
                            if (operat == 1)
                            {
                                model.ReviewMan1 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "构件厂长审核通过";
                            }
                            else if (operat == 2)
                            {
                                model.ReviewMan2 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "工程部长审核通过";
                            }
                            else if (operat == 3)
                            {
                                model.ReviewMan3 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "物质部长审核通过";
                            }
                            else if (operat == 4)
                            {
                                model.ReviewMan4 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "副总工审核通过";
                            }
                            else
                            {
                                model.ReviewMan5 = OperatorProvider.Provider.Current().UserName;
                                model.IsPassed = 1;
                                model.ReviewDescription = "总工审核通过";
                            }
                        }
                        else
                        {
                            if (operat == 1)
                            {
                                model.ReviewMan1 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "构件厂长审核失败";
                            }
                            else if (operat == 2)
                            {
                                model.ReviewMan2 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "工程部长审核失败";
                            }
                            else if (operat == 3)
                            {
                                model.ReviewMan3 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "物质部长审核失败";
                            }
                            else if(operat == 4)
                            {
                                model.ReviewMan4 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "副总工审核失败";
                            }
                            else
                            {
                                model.ReviewMan5 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "总工审核失败";
                            }
                        }
                        model.Description = entity.Description;
                        list.Add(model);
                    }
                    if (type == 1)
                    {
                        //数据添加至出库信息
                        var childEntitys1 = new List<RawMterialCollarInfoEntity>();
                        var entity1 = new RawMterialCollarEntity()
                        {
                            Numbering = model.OrderNumbering,
                            CollarEngineering = model.Category,
                            CollarType = 0,
                            OrganizeId = model.OrganizeId,
                            Date = model.CreateTime,
                            CreateMan = model.CreateMan,
                            ShippingAddress = model.ShippingAddress,
                            ContactPerson = model.ContactPerson,
                            ContactPersonTel = model.ContactPersonTel,
                        };
                        var ListEntity = rawmaterialorderinfobll.GetList(f => f.OrderId == item.Trim());
                        if (ListEntity.Count() > 0)
                        {
                            foreach (var item1 in ListEntity)
                            {
                                var Entity = new RawMterialCollarInfoEntity()
                                {
                                    Quantity = item1.Quantity,
                                    RawMaterialId = item1.RawMaterialId,
                                    RawMaterialAnalysisId = item1.RawMaterialAnalysisId,
                                };
                                childEntitys1.Add(Entity);
                                //更改需求中已成单量
                                var RawMaterialAnalysisEntiity = rawmaterialanalysisbll.GetEntity(item1.RawMaterialAnalysisId);
                                RawMaterialAnalysisEntiity.ApplicationPurchasedQuantity = RawMaterialAnalysisEntiity.ApplicationPurchasedQuantity.ToDecimal() + item1.Quantity;
                                rawmaterialanalysisbll.SaveForm(item1.RawMaterialAnalysisId, RawMaterialAnalysisEntiity);
                                //
                            }
                        }
                        rawmterialcollarbll.SaveForm("", entity1, childEntitys1);
                        //end
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialorderbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        /// <summary>
        ///收货操作
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未采购；1已采购</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitIsReceived(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialOrderEntity> list = new List<RawMaterialOrderEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsReceived = 1;
                        list.Add(model);

                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialorderbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        #endregion

        /// <summary>
        /// 导出订单明细（Excel模板导出）
        /// </summary>
        /// <param name="keyValue">订单Id</param>
        /// <returns></returns>
        //[ValidateInput(false)]
        public void OutExcel(string keyValue)

        {
            var data = rawmaterialorderbll.GetEntity(keyValue);
            var childData = rawmaterialorderinfobll.GetList(keyValue).ToList();

            var list = new List<TemplateMode>();
            //设置主表信息
            list.Add(new TemplateMode() { row = 1, cell = 1, value = data.OrderNumbering });
            list.Add(new TemplateMode() { row = 1, cell = 3, value = data.CreateMan });
            list.Add(new TemplateMode() { row = 1, cell = 5, value = data.CreateTime.ToDate().ToString("yyyy-MM-dd") });
            list.Add(new TemplateMode() { row = 2, cell = 1, value = subprojectbll.GetEntity(data.Category).FullName });

            //设置明细信息
            int rowIndex = 5;
            foreach (RawMaterialOrderInfoEntity item in childData)
            {
                var rawmaterial = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                list.Add(new TemplateMode() { row = rowIndex, cell = 0, value = rawmaterial.RawMaterialModel });
                list.Add(new TemplateMode() { row = rowIndex, cell = 1, value = rawmaterial.RawMaterialName });
                list.Add(new TemplateMode() { row = rowIndex, cell = 2, value = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName });
                list.Add(new TemplateMode() { row = rowIndex, cell = 3, value = item.Quantity.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 4, value = rawmaterial.Description });
                rowIndex++;
            }
            ////设置明细合计
            //list.Add(new TemplateMode() { row = 16, cell = 5, value = orderEntry.Sum(t => t.Qty).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 6, value = orderEntry.Sum(t => t.Price).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 7, value = orderEntry.Sum(t => t.Amount).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 9, value = orderEntry.Sum(t => t.Taxprice).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 10, value = orderEntry.Sum(t => t.Tax).ToString() });

            ExcelHelper.ExcelDownload(list, "材料订单模板.xlsx", "材料订单-" + data.OrderNumbering + ".xlsx");
        }
    }
}
