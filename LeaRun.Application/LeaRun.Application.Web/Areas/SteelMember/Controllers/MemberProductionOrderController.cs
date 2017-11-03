using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using LeaRun.Application.Code;
using System.Linq;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using LeaRun.Util.Extension;
using LeaRun.Application.Busines.SystemManage;
using System.Data.OleDb;
using System.Data;
using LeaRun.Application.Busines.BaseManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-11 10:12
    /// 描 述：构件生产订单
    /// </summary>
    public class MemberProductionOrderController : MvcControllerBase
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
        /// 订单表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "GJSQD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                //ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;
            }
            return View();
        }

        /// <summary>
        ///构件列表
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
            var data = memberproductionorderbll.GetPageList(pagination, queryJson).OrderByDescending(O => O.Priority).ToList();
            foreach (var item in data)
            {
                item.Category = subprojectbll.GetEntity(item.Category).FullName;
            }

            var queryParam = queryJson.ToJObject();
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                data = data.FindAll(t => t.Category == Category);
            }

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
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJsonProcess(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = memberproductionorderbll.GetPageList(pagination, queryJson).ToList().FindAll(f=>f.IsPassed==1).OrderByDescending(O => O.Priority).ToList();
            foreach (var item in data)
            {
                item.Category = subprojectbll.GetEntity(item.Category).FullName;
            }

            var queryParam = queryJson.ToJObject();
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                data = data.FindAll(t => t.Category == Category);
            }

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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = memberproductionorderbll.GetEntity(keyValue);
            var childData = memberproductionorderbll.GetDetails(keyValue).ToList();
            var MemberList = new List<MemberDemandModel>();
            for (int i = 0; i < childData.Count(); i++)
            {
                var MemberLibrary = memberlibrarybll.GetEntity(childData[i].MemberId);
                var Member = new MemberDemandModel()
                {
                    InfoId = childData[i].InfoId,
                    MemberId = childData[i].MemberId,
                    MemberDemandId= childData[i].MemberDemandId,
                    MemberNumber = childData[i].ProductionQuantity,
                    ProductionedQuantity = Convert.ToInt32(childData[i].ProductionQuantity),
                    SelfDetectNumber = childData[i].SelfDetectNumber,
                    SelfDetectRemarks = childData[i].SelfDetectRemarks,
                    QualityInspectionNumber = childData[i].QualityInspectionNumber,
                    QualityInspectionRemarks = childData[i].QualityInspectionRemarks,
                    MemberName = MemberLibrary.MemberName,
                    MemberNumbering = MemberLibrary.MemberNumbering,
                    UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
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
            var data = memberproductionorderbll.GetEntity(keyValue);
            var MemberList = new List<MemberDemandModel>();
            if (data!=null)
            {
                data.Category = subprojectbll.GetEntity(data.Category).FullName;
                data.DepartmentId = organizebll.GetEntity(data.OrganizeId).FullName + "—" + departmentbll.GetEntity(data.DepartmentId).FullName;

                var childData = memberproductionorderbll.GetDetails(keyValue).ToList();
                for (int i = 0; i < childData.Count(); i++)
                {
                    var MemberLibrary = memberlibrarybll.GetEntity(childData[i].MemberId);
                    var Member = new MemberDemandModel()
                    {
                        InfoId = childData[i].InfoId,
                        MemberId = childData[i].MemberId,
                        MemberDemandId = childData[i].MemberDemandId,
                        MemberNumber = childData[i].ProductionQuantity,
                        ProductionedQuantity = Convert.ToInt32(childData[i].ProductionQuantity),
                        SelfDetectNumber = childData[i].SelfDetectNumber,
                        SelfDetectRemarks = childData[i].SelfDetectRemarks,
                        QualityInspectionNumber = childData[i].QualityInspectionNumber,
                        QualityInspectionRemarks = childData[i].QualityInspectionRemarks,
                        MemberName = MemberLibrary.MemberName,
                        MemberNumbering = MemberLibrary.MemberNumbering,
                        UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
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
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = memberproductionorderbll.GetDetails(keyValue).ToList();
            var MemberList = new List<MemberDemandModel>();
            for (int i = 0; i < data.Count(); i++)
            {
                var MemberLibrary = memberlibrarybll.GetEntity(data[i].MemberId);
                var memberproductionorderinfo = memberproductionorderinfobll.GetEntity(data[i].InfoId);
                var Member = new MemberDemandModel()
                {
                    MemberId = data[i].MemberId,
                    MemberNumber = data[i].ProductionQuantity,
                    ProductionedQuantity = Convert.ToInt32(data[i].ProductionedQuantity),
                    SelfDetectNumber = data[i].SelfDetectNumber,
                    QualityInspectionNumber = data[i].QualityInspectionNumber,
                    QualifiedQuantity = memberproductionorderinfo.QualifiedQuantity,
                    MemberName = MemberLibrary.MemberName,
                    Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
                    MemberNumbering = MemberLibrary.MemberNumbering,
                    UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
                };
                MemberList.Add(Member);
            }
            return ToJsonResult(MemberList);
        }
        /// <summary>
        /// 加载审核通过的构件需求
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GridListJsonDemand(Pagination pagination, string category)
        {
            var watch = CommonHelper.TimerStart();
            var data = new List<MemberDemandModel>();
            var memberdemand = memberdemandbll.GetPageList1(pagination, f => f.SubProjectId == category && f.IsReview == 1).ToList();//.OrderByDescending(o => o.MemberNumbering)

            foreach (var item in memberdemand)
            {
                MemberDemandModel MemberDemand = new MemberDemandModel();
                var member = memberlibrarybll.GetEntity(f => f.MemberId == item.MemberId);
                MemberDemand.MemberId = member.MemberId;
                MemberDemand.MemberDemandId = item.MemberDemandId;
                MemberDemand.Category = member.Category;
                MemberDemand.MemberNumbering = member.MemberNumbering;
                MemberDemand.MemberName = member.MemberName;
                MemberDemand.UnitId = dataitemdetailbll.GetEntity(member.UnitId).ItemName;
                MemberDemand.MemberNumber = item.MemberNumber;
                data.Add(MemberDemand);
            }
            var jsonData = new
            {
                rows = data.OrderBy(O => O.MemberNumbering),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 控制订单构件的数量（新增）
        /// </summary>
        /// <returns></returns>
        public ContentResult AddMemberNumber(string KeyValue, string category)
        {
            var MemberDemand = memberdemandbll.GetEntity(s => s.SubProjectId == category && s.MemberId == KeyValue);
            int MemberDemandNumber = 0;
            int Number = 0;
            var Order = memberproductionorderbll.GetList(f => f.Category == category);
            foreach (var item in Order)
            {
                var MemberOrder = memberproductionorderinfobll.GetList(f => f.OrderId == item.OrderId && f.MemberId == KeyValue).SingleOrDefault();
                if (MemberOrder != null)
                {
                    Number += Convert.ToInt32(MemberOrder.ProductionQuantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.MemberNumber) - Number;

            return Content(MemberDemandNumber.ToString());
        }

        /// <summary>
        /// 控制订单构件的数量(编辑)
        /// </summary>
        /// <returns></returns>
        public ContentResult EditMemberNumber(string KeyValue, string MemberId)
        {

            var OrderList = new List<MemberProductionOrderEntity>();
            var Order = memberproductionorderbll.GetList(KeyValue).SingleOrDefault();
            OrderList = memberproductionorderbll.GetList(Order.Category).ToList();

            var MemberDemand = memberdemandbll.GetList(MemberId).Where(f => f.SubProjectId == Order.Category).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;

            foreach (var item in OrderList)
            {
                var OrderMember = memberproductionorderinfobll.GetList(f => f.OrderId == item.OrderId && f.MemberId == MemberId).SingleOrDefault();
                if (OrderMember != null)
                {
                    Number += Convert.ToInt32(OrderMember.ProductionQuantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.MemberNumber) - Number;

            return Content(MemberDemandNumber.ToString());
        }
        /// <summary>
        /// 载入添加后的构件
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult ListMember(string KeyValue, string category)
        {
            var listmember = new List<MemberDemandModel>();
            if (KeyValue != null)
            {
                string[] array = KeyValue.Split(',');
                if (array.Count() > 0)
                {
                    foreach (var item in array)
                    {
                        var a = memberdemandbll.GetEntity(f => f.SubProjectId == category && f.MemberId == item);
                        var member = memberlibrarybll.GetEntity(f => f.MemberId == a.MemberId);
                        MemberDemandModel projectdemand = new MemberDemandModel()
                        {
                            MemberId = a.MemberId,
                            MemberDemandId=a.MemberDemandId,
                            MemberName = member.MemberName,
                            UnitPrice = member.UnitPrice,
                            UnitId = dataitemdetailbll.GetEntity(member.UnitId).ItemName,
                            MemberNumbering = member.MemberNumbering,
                            MemberNumber = a.MemberNumber,
                        };

                        listmember.Add(projectdemand);
                    }
                }
            }
            else
            {
                listmember = null;
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
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
            }
            if (!ids.IsEmpty())
            {
                foreach (var item1 in ids)
                {
                    memberproductionorderbll.RemoveForm(item1);
                    var meminfo = memberproductionorderinfobll.GetList(f => f.OrderId == item1);
                    if (meminfo.Count() > 0)
                    {
                        foreach (var item in meminfo)
                        {
                            memberproductionorderinfobll.RemoveForm(item.InfoId);
                        }
                    }
                }
            }
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
            var entity = strEntity.ToObject<MemberProductionOrderEntity>();
            if (keyValue == "" || keyValue == null)
            {
                entity.ProductionStatus = 0;
                entity.IsConfirm = 0;
                entity.IsPackage = 0;
                entity.OrderWarehousingStatus = 0;
                entity.ProductionStatus = 0;
                entity.QualityInspectionStatus = 0;
                entity.SelfDetectStatus = 0;
                entity.IsReceiveRawMaterial = 0;
                entity.CreateMan= OperatorProvider.Provider.Current().UserName;
            }

            entity.IsPassed = 0;
            entity.IsSubmit = 0;

            List<MemberProductionOrderInfoEntity> childEntitys = strChildEntitys.ToList<MemberProductionOrderInfoEntity>();
            memberproductionorderbll.SaveForm(keyValue, entity, childEntitys);
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
                List<MemberProductionOrderEntity> list = new List<MemberProductionOrderEntity>();
                foreach (var item in ids)
                {
                    var model = memberproductionorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmit = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    memberproductionorderbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 审核处理
        /// </summary>
        /// <param name="keyValue">要审核的数据的主键些</param>
        /// <param name="entity"></param>
        /// <param name="type">1通过，2失败</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
       // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReviewOperation(string keyValue,MemberProductionOrderEntity entity, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<MemberProductionOrderEntity> list = new List<MemberProductionOrderEntity>();
                foreach (var item in ids)
                {
                    var model = memberproductionorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsPassed = type;
                        model.IsConfirm = 0;
                        model.ReviewTime = System.DateTime.Now;
                        model.ReviewMan = OperatorProvider.Provider.Current().UserName;
                        model.Description = entity.Description;
                        list.Add(model);
                    }
                    if (type==1)
                    {
                        //数据添加至出库信息
                        var childEntitys1 = new List<MemberCollarInfoEntity>();
                        var entity1 = new MemberCollarEntity()
                        {
                            Numbering = model.OrderNumbering,
                            CollarEngineering = model.Category,
                            OrganizeId = model.OrganizeId,
                            DepartmentId = model.DepartmentId,
                            Date = model.CreateTime,
                            CreateMan = model.CreateMan,
                            ShippingAddress = model.ShippingAddress,
                            ContactPerson = model.ContactPerson,
                            ContactPersonTel = model.ContactPersonTel,
                        };
                        var ListEntity = memberproductionorderinfobll.GetList(f => f.OrderId == item.Trim());
                        if (ListEntity.Count() > 0)
                        {
                            foreach (var item1 in ListEntity)
                            {
                                var Entity = new MemberCollarInfoEntity()
                                {
                                    CollarQuantity = item1.ProductionQuantity,
                                    MemberId = item1.MemberId,
                                    MemberDemandId = item1.MemberDemandId,
                                };
                                childEntitys1.Add(Entity);
                                //更改需求中已成单量
                                var MemberDemandEntiity = memberdemandbll.GetEntity(item1.MemberDemandId);
                                MemberDemandEntiity.OrderedQuantity = MemberDemandEntiity.OrderedQuantity.ToDecimal() + item1.ProductionQuantity;
                                memberdemandbll.SaveForm(item1.MemberDemandId, MemberDemandEntiity);
                                //
                            }
                        }
                        membercollarbll.SaveForm("", entity1, childEntitys1);
                        //end
                    }

                }
                if (list.Count > 0)
                {
                    memberproductionorderbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        #endregion
    }
}
