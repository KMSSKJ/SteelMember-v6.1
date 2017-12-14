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
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public class RawMterialCollarController : MvcControllerBase
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

        ///// <summary>
        ///// 控制出库的数量（新增）
        ///// </summary>
        ///// <returns></returns>
        //public ContentResult AddRawMaterialNumber(string KeyValue, string category)
        //{
        //    var MemberDemand = rawmaterialanalysisbll.GetList(s => s.Category == category && s.RawMaterialId == KeyValue).SingleOrDefault();
        //    int MemberDemandNumber = 0;
        //    int Number = 0;
        //    var Order = rawmaterialorderbll.GetList(null).ToList().FindAll(f => f.Category == category);
        //    foreach (var item in Order)
        //    {
        //        var MemberOrder = rawmaterialorderinfobll.GetList(null).ToList().Find(f => f.OrderId == item.OrderId && f.RawMaterialId == KeyValue);
        //        if (MemberOrder != null)
        //        {
        //            Number += Convert.ToInt32(MemberOrder.ProductionQuantity);
        //        }
        //    }
        //    MemberDemandNumber = Convert.ToInt32(MemberDemand.RawMaterialDosage) - Number;

        //    return Content(MemberDemandNumber.ToString());
        //}

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNumberingList()
        {
            var RawMaterialCollar = rawmterialcollarbll.GetCallarList(f => f.CollarId != "" && f.CollarNumbering == null);
            if (RawMaterialCollar.Count() > 0)
            {
                for (int i = 0; i < RawMaterialCollar.Count(); i++)
                {
                    var Numbering = RawMaterialCollar[i].Numbering;
                    var RawMaterialOrder = rawmaterialorderbll.GetEntity(f=>f.OrderNumbering== Numbering);
                    if (RawMaterialOrder.IsPassed == 1)
                    {
                    }
                    else
                    {
                      RawMaterialCollar.Remove(RawMaterialCollar[i]);
                    }
                }
            }
            return ToJsonResult(RawMaterialCollar);
        }

        /// <summary>
        /// 分页查询出库信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OutInventory(Pagination pagination, string queryJson)
        {
            var list = rawmterialcollarbll.GetPageList(pagination, queryJson);
            for (var i = 0; i < list.Count(); i++)
            {
                list[i].CollarEngineering = subprojectbll.GetEntity(list[i].CollarEngineering).FullName;
                list[i].OrganizeId = organizebll.GetEntity(list[i].OrganizeId) == null ? "" : organizebll.GetEntity(list[i].OrganizeId).FullName;

            }

            //
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["CollarEngineering"].IsEmpty())
            {
                var CollarEngineering = queryParam["CollarEngineering"].ToString();
                list = list.FindAll(t => t.CollarEngineering.Contains(CollarEngineering));
            }
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                var OrganizeId = queryParam["OrganizeId"].ToString();
                list = list.FindAll(t => t.OrganizeId.Contains(OrganizeId));
            }
            if (!queryParam["ShippingAddress"].IsEmpty())
            {
                var ShippingAddress = queryParam["ShippingAddress"].ToString();
                list = list.FindAll(t => t.ShippingAddress.Contains(ShippingAddress));
            }
            //

            return ToJsonResult(list);
        }

        public ActionResult OutInventoryInfo(string keyValue)
        {
            var data = rawmterialcollarinfobll.GetList(f => f.CollarId == keyValue);
            var list = new List<RawMaterialLibraryModel>();
            foreach (var item in data)
            {
                var rawmaterialinventory = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == item.RawMaterialId);
                var RawMaterialLibrary = rawmateriallibrarybll.GetEntity(rawmaterialinventory.RawMaterialId);
                var rawmaterial = new RawMaterialLibraryModel()
                {
                    InventoryId = rawmaterialinventory.InventoryId,
                    RawMaterialName = RawMaterialLibrary.RawMaterialName,
                    RawMaterialModel = RawMaterialLibrary.RawMaterialModel,
                    CollarQuantity = item.CollarQuantity.ToDecimal(),
                    UnitId = dataitemdetailbll.GetEntity(RawMaterialLibrary.Unit).ItemName,
                    Description = item.Description,
                };
                list.Add(rawmaterial);
            }
            return ToJsonResult(list);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = rawmterialcollarbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="OrderNumbering"></param>

        /// <returns>返回对象Json</returns>
        [HttpPost]
        public ActionResult GetFormJson1(string OrderNumbering)
        {
            var data = rawmterialcollarbll.GetEntity(f => f.Numbering == OrderNumbering);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="Numbering">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult NumberingToGetFormJson(string Numbering)
        {
            var list = new List<RawMaterialLibraryModel>();
            var data = rawmterialcollarbll.GetEntity(f => f.Numbering == Numbering.Trim());
            if (data != null)
            {
                data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;
                data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;
                data.ReviewMan = OperatorProvider.Provider.Current().UserName;

                var childData = rawmterialcollarinfobll.GetList(f => f.CollarId == data.CollarId).ToList();

                foreach (var item in childData)
                {
                    var RawMaterialOrderInfo = new RawMaterialOrderInfoEntity();
                    var rawmaterialinventory = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == item.RawMaterialId);
                    var RawMaterialLibrary = rawmateriallibrarybll.GetEntity(rawmaterialinventory.RawMaterialId);
                    if (!item.RawMaterialAnalysisId.IsEmpty())
                    {
                        RawMaterialOrderInfo = rawmaterialorderinfobll.GetEntity(f => f.RawMaterialAnalysisId == item.RawMaterialAnalysisId && f.RawMaterialId == item.RawMaterialId);
                    }
                    else
                    {
                        RawMaterialOrderInfo = rawmaterialorderinfobll.GetEntity(f => f.RawMaterialId == item.RawMaterialId);
                    }

                    var rawmaterial = new RawMaterialLibraryModel()
                    {
                        InfoId = item.InfoId,
                        InventoryId = rawmaterialinventory.InventoryId,
                        InventoryQuantity = rawmaterialinventory.Quantity,
                        RawMaterialName = RawMaterialLibrary.RawMaterialName,
                        RawMaterialModel = RawMaterialLibrary.RawMaterialModel,
                        CollarQuantity = item.CollarQuantity,
                        CollaredQuantity = item.CollaredQuantity,
                        Quantity = item.Quantity,
                        UnitId = dataitemdetailbll.GetEntity(RawMaterialLibrary.Unit).ItemName,
                        Description = item.Description,
                    };
                    list.Add(rawmaterial);
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
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmterialcollarbll.GetEntity(keyValue);
            if (data != null)
            {
                data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;
                data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;
            }

            var childData = rawmterialcollarinfobll.GetList(f => f.CollarId == keyValue);
            var list = new List<RawMaterialLibraryModel>();
            foreach (var item in childData)
            {
                var rawmaterialinventory = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == item.RawMaterialId);
                var RawMaterialLibrary = rawmateriallibrarybll.GetEntity(rawmaterialinventory.RawMaterialId);
                var rawmaterial = new RawMaterialLibraryModel()
                {
                    InventoryId = rawmaterialinventory.InventoryId,
                    RawMaterialName = RawMaterialLibrary.RawMaterialName,
                    RawMaterialModel = RawMaterialLibrary.RawMaterialModel,
                    Qty = item.CollarQuantity.ToDecimal(),
                    UnitId = dataitemdetailbll.GetEntity(RawMaterialLibrary.Unit).ItemName,
                    Description = item.Description,
                };
                list.Add(rawmaterial);
            }
            var jsonData = new
            {
                entity = data,
                childEntity = list
            };
            return ToJsonResult(jsonData);
        }


        /// <summary>
        /// 获取条件汇总 
        /// </summary>
        /// <param name="CollarEntityJson"></param>
        /// <param name="CollarJson"></param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult QuantitySummaryInfo(string CollarEntityJson, string CollarJson)
        {
            var data = CollarEntityJson.ToObject<CollarEntityModel>();

            var list = new List<RawMaterialLibraryModel>();
            var CollarJsonList = CollarJson.ToList<CollarJsonMoadel>();
            foreach (var item1 in CollarJsonList)
            {
                var childData = rawmterialcollarinfobll.GetList(f => f.CollarId == item1.CollarId);
                foreach (var item in childData)
                {
                    var rawmaterialinventory = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == item.RawMaterialId);
                    var RawMaterialLibrary = rawmateriallibrarybll.GetEntity(rawmaterialinventory.RawMaterialId);
                    var rawmaterial = new RawMaterialLibraryModel()
                    {
                        InventoryId = rawmaterialinventory.InventoryId,
                        RawMaterialId = RawMaterialLibrary.RawMaterialId,
                        RawMaterialName = RawMaterialLibrary.RawMaterialName,
                        RawMaterialModel = RawMaterialLibrary.RawMaterialModel,
                        Qty = item.CollarQuantity.ToDecimal(),
                        UnitId = dataitemdetailbll.GetEntity(RawMaterialLibrary.Unit).ItemName,
                        Description = item.Description,
                        Date = item1.Date.ToDate(),
                    };
                    if (list.Count() == 0)
                    {
                        list.Add(rawmaterial);
                    }
                    else
                    {
                        if (list.Find(f => f.RawMaterialId == rawmaterial.RawMaterialId) == null)
                        {
                            list.Add(rawmaterial);
                        }
                        else
                        {
                            var RawMaterialEntity = list.Find(f => f.RawMaterialId == rawmaterial.RawMaterialId);
                            RawMaterialEntity.Qty = RawMaterialEntity.Qty.ToDecimal() + item.CollarQuantity.ToDecimal();
                        }
                    }

                }
            }
            var jsonData = new
            {
                entity = data,
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
            rawmterialcollarbll.RemoveForm(keyValue);

            var data = rawmterialcollarinfobll.GetList(f => f.CollarId == keyValue);
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    rawmterialcollarinfobll.RemoveForm(item.InfoId);
                }
            }
            return Success("删除成功。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveFormEdit(string keyValue)
        {
            var rawmterialcollar = rawmterialcollarbll.GetEntity(keyValue);
            rawmterialcollar.CollarNumbering = "";
            rawmterialcollarbll.SaveForm(keyValue, rawmterialcollar);
            var data = rawmterialcollarinfobll.GetList(f => f.CollarId == keyValue).ToList();
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var rawmterialcollarinfo = data.Find(f => f.InfoId == item.InfoId);

                    rawmterialcollarinfo.InventoryId = "";
                    rawmterialcollarinfo.CollarQuantity = 0;
                    rawmterialcollarinfo.CollaredQuantity = 0;
                    rawmterialcollarinfobll.SaveForm(item.InfoId, rawmterialcollarinfo);
                }
            }
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity"></param>
        /// <param name="strChildEntitys"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]

        public ActionResult SaveForm_Memebr(string keyValue, string strEntity, string strChildEntitys)
        {
            RawMterialCollarEntity entity = strEntity.ToObject<RawMterialCollarEntity>();
            entity.CreateMan = OperatorProvider.Provider.Current().UserName;
            List<RawMterialCollarInfoEntity> childEntitys = strChildEntitys.ToList<RawMterialCollarInfoEntity>();
            rawmterialcollarbll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="Numbering">主键值</param>
        /// <param name="CollarNumbering">实体对象</param>
        /// <param name="strChildEntitys">子表对象集</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string Numbering, string CollarNumbering, string strChildEntitys)
        {
            var entity = rawmterialcollarbll.GetEntity(f => f.Numbering == Numbering);
            entity.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            entity.CollarNumbering = CollarNumbering;

            List<RawMterialCollarInfoEntity> childEntitys = strChildEntitys.ToList<RawMterialCollarInfoEntity>();

            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    //判断库存量是否满足出库
                    var model = rawmaterialinventorybll.GetEntity(item.InventoryId);
                    model.Quantity = model.Quantity.ToDecimal() - item.CollarQuantity.ToDecimal();//库存--
                    if (model.Quantity < 0)
                    {
                        //var member = memberlibrarybll.GetEntity(item.MemberId);
                        return Error("存在材料库存不足构件，无法出库");
                    }
                }


                foreach (var item in childEntitys)
                {
                    //在库存量中减掉领出的数量
                    var inventorymodel = rawmaterialinventorybll.GetEntity(item.InventoryId);
                    inventorymodel.Quantity = Convert.ToDecimal(inventorymodel.Quantity) - Convert.ToDecimal(item.CollarQuantity);//库存--
                    rawmaterialinventorybll.SaveForm(item.InventoryId, inventorymodel);
                    //end

                    ////修改出库信息
                    var entitys = rawmterialcollarinfobll.GetEntity(f => f.InfoId == item.InfoId);
                    entitys.CollarQuantity = item.CollarQuantity;
                    entitys.CollaredQuantity = item.Quantity;    //entitys.CollaredQuantity.ToDecimal() + item.Quantity;
                    entitys.InventoryId = item.InventoryId;
                    entitys.Description = item.Description;
                    rawmterialcollarinfobll.SaveForm(item.InfoId, entitys);
                    //end

                    //修改需求中已使用量
                    var rawmaterialanalysisEntity = rawmaterialanalysisbll.GetEntity(entitys.RawMaterialAnalysisId);
                    if (rawmaterialanalysisEntity != null)
                    {
                        rawmaterialanalysisEntity.WarehousedQuantity = rawmaterialanalysisEntity.WarehousedQuantity.ToDecimal() + item.CollarQuantity.ToDecimal();
                    }
                    rawmaterialanalysisbll.SaveForm(entitys.RawMaterialAnalysisId, rawmaterialanalysisEntity);
                    //end
                }
            }
            rawmterialcollarbll.SaveForm(entity.CollarId, entity);

            return Success("操作成功。");
        }
        #endregion
    }
}
