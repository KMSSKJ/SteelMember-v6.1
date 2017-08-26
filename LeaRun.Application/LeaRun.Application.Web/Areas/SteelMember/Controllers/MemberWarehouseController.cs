using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Code;
using System;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Linq;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-28 11:34
    /// 描 述：构件库存
    /// </summary>
    public class MemberWarehouseController : MvcControllerBase
    {
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private MemberWarehouseBLL memberwarehousebll = new MemberWarehouseBLL();
        private MemberWarehouseRecordingBLL memberwarehouserecordingbll = new MemberWarehouseRecordingBLL();
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 入库列表
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryInfo()
        {
            return View();
        }
        /// <summary>
        /// 入库报表
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryDetail()
        {
            return View();
        }
        /// <summary>
        /// 出库表
        /// </summary>
        /// <returns></returns>
        public ActionResult Collar()
        {
            return View();
        }
        /// <summary>
        /// 出库报表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutInventoryDetail()
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
            var datatabel = new List<MemberWarehouseModel>();
            var data = memberwarehousebll.GetPageList(pagination, queryJson);
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var MemberLibrar = memberlibrarybll.GetEntity(item.MemberId);

                    var MemberWarehouse = new MemberWarehouseModel()
                    {
                        MemberNumbering = MemberLibrar.MemberNumbering,
                        MemberName = MemberLibrar.MemberName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrar.Category).ItemName,
                        MemberUnit = dataitemdetailbll.GetEntity(MemberLibrar.UnitId).ItemName,
                        InStock = item.InStock,
                        Librarian = item.Librarian,
                        UpdateTime = item.UpdateTime,
                        Description = MemberLibrar.Description
                    };
                    datatabel.Add(MemberWarehouse);
                }
            }
           
            var queryParam = queryJson.ToJObject();
            if (!queryParam["Category"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f=>f.MemberNumbering.Contains(keyword)&& f.Category== Category);
            }
            if (!queryParam["Category"].IsEmpty() && queryParam["keyword"].IsEmpty())
            {
                //string keyword = queryParam["keyword"].ToString();
                string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f =>f.Category == Category);
            }
            if (queryParam["Category"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f => f.MemberNumbering.Contains(keyword));
            }

            var jsonData = new
            {
                rows = datatabel.OrderBy(O => O.MemberNumbering),
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
            var data = memberwarehousebll.GetList(queryJson);
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
            var data = memberwarehousebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取还没入库的 ProductionStatus值为0时是没生产的，值为1是正在生产的 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNoIswarehousing(Pagination pagination)
        {

            var watch = CommonHelper.TimerStart();
            var ProductionStatus = 2;//生成完成的
            var data = memberproductionorderbll.GetPageListByProductionStatus(pagination, ProductionStatus);
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
        /// <param name="Entity"></param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue, MemberProductionOrderInfoEntity Entity)
        {
            List<MemberWarehouseModel> MemberWarehouseModelList = new List<MemberWarehouseModel>();
            var data = memberproductionorderinfobll.GetList(f => f.OrderId == keyValue);
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    MemberWarehouseModel MemberWarehouse = new MemberWarehouseModel();
                    var data1 = memberlibrarybll.GetList(null).Find(f => f.MemberId == item.MemberId);
                    MemberWarehouse.MemberId = item.MemberId;
                    MemberWarehouse.ProductionQuantity = Convert.ToInt32(item.ProductionQuantity);
                    MemberWarehouse.Category = data1.Category;
                    MemberWarehouse.MemberName = data1.MemberName;
                    MemberWarehouse.MemberNumbering = data1.MemberNumbering;
                    //MemberWarehouse.MemberUnit = data1.Unit.ItemName;
                    MemberWarehouseModelList.Add(MemberWarehouse);
                }
            }
            return ToJsonResult(MemberWarehouseModelList);
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
        public ActionResult RemoveForm(string keyValue)
        {
            memberwarehousebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, MemberWarehouseEntity entity)
        {
            memberwarehousebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public ActionResult Inventory(string OrderId)
        {

            string[] OrderIds = OrderId.Split(',');
            try
            {
                if (OrderIds.Length > 0)
                {
                    for (var i = 0; i < OrderIds.Length; i++)
                    {
                        //获取到订单下的所有构件
                        var _OrderId = OrderIds[i];
                        var data = memberproductionorderinfobll.GetList(f => f.OrderId == _OrderId);
                        for (int i0 = 0; i0 < data.Count; i0++)
                        {
                            var MemberId = data[i].MemberId;
                            var memberinfo = memberlibrarybll.GetList(null).Find(f => f.MemberId == MemberId);
                            var orderinfo = memberproductionorderbll.GetList(null).Find(f => f.OrderId == _OrderId);

                            //先加到入库管理中
                            MemberWarehouseRecordingEntity warehouseRecording = new MemberWarehouseRecordingEntity();
                            string keyValue1 = null;
                            warehouseRecording.Librarian = OperatorProvider.Provider.Current().UserName;
                            warehouseRecording.MemberId = memberinfo.MemberId;
                            warehouseRecording.UpdateTime = System.DateTime.Now;
                            warehouseRecording.ToReportPeople = orderinfo.CreateMan;
                            warehouseRecording.Receiver = "1111";
                            warehouseRecording.ReceiverTel = "11111111111111";
                            warehouseRecording.Class = "入库";
                            memberwarehouserecordingbll.SaveForm(keyValue1, warehouseRecording);

                            //更改库存量
                            MemberWarehouseEntity MemberWarehouse = new MemberWarehouseEntity();
                            var MemberWarehouses = memberwarehousebll.GetList(null).Find(f => f.MemberId == MemberId);
                            if (MemberWarehouses != null)
                            {
                                MemberWarehouse.MemberWarehouseId = MemberWarehouses.MemberWarehouseId;
                                MemberWarehouse.InStock = Convert.ToInt32(MemberWarehouses.InStock) + data[i0].ProductionQuantity;//库存量++                                                                                              //inventorymodel.
                                memberwarehousebll.SaveForm(MemberWarehouse.MemberWarehouseId, MemberWarehouse);
                            }

                            //修改生产订单中入库状态

                            var MembeOrder = memberproductionorderbll.GetEntity(_OrderId);
                            if (MembeOrder != null)
                            {
                                MembeOrder.OrderWarehousingStatus = 1;
                                memberproductionorderbll.SaveForm(_OrderId, MembeOrder);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Success("入库成功。");
            //return View();

        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="collarinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCollarinfo(string collarinfo)
        {
            var collarmodel = collarinfo.ToObject<MemberWarehouseRecordingEntity>();
            collarmodel.UpdateTime = System.DateTime.Now;
            try
            {
                if (collarmodel.InStock > 0)
                {
                    //在库存量中减掉领出的数量
                    MemberWarehouseEntity MemberWarehouse = new MemberWarehouseEntity();
                    var MemberWarehouses = memberwarehousebll.GetList(null).Find(f => f.MemberWarehouseId == collarmodel.MemberWarehouseId);
                    if (MemberWarehouses != null)
                    {
                        MemberWarehouse.MemberWarehouseId = MemberWarehouses.MemberWarehouseId;
                        MemberWarehouse.InStock = Convert.ToInt32(MemberWarehouse.InStock) - collarmodel.InStock;//库存量++
                        memberwarehousebll.SaveForm(MemberWarehouse.MemberWarehouseId, MemberWarehouse);
                    }

                    //添加到出库表中  
                    string keyValue = "";
                    var MemberLibrary = memberlibrarybll.GetList(null).Find(f => f.MemberId == MemberWarehouses.MemberId);
                    collarmodel.Class = "出库";
                    collarmodel.Librarian = OperatorProvider.Provider.Current().UserName;
                    collarmodel.UpdateTime = DateTime.Now;
                    memberwarehouserecordingbll.SaveForm(keyValue, collarmodel);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return Success("出库成功");

        }
        #endregion
    }
}
