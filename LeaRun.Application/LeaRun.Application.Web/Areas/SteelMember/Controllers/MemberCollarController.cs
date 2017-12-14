using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Util.Extension;
using LeaRun.Application.Code;
using System.Linq;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-09-13 22:58
    /// 描 述：构件领用
    /// </summary>
    public class MemberCollarController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RMC_MemberCollarIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberCollarForm()
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
            var data = membercollarbll.GetPageList(pagination, queryJson);
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
            var data = membercollarbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        ///// <summary>
        ///// 获取单号
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult GetNumberingList()
        //{
        //    var MemberCollarList = new List<MemberCollarEntity>();
        //    var memberproductionorder = memberproductionorderbll.GetList(f=>f.IsReceiveRawMaterial==1&&f.QualityInspectionStatus!=0);
        //    if (memberproductionorder.Count() > 0)
        //    {
        //        foreach (var item in memberproductionorder)
        //        {
        //           var MemberCollar = membercollarbll.GetList(f => f.CollarId != "" && f.CollarNumbering == null&&f.Numbering==item.OrderNumbering).SingleOrDefault();
        //            MemberCollarList.Add(MemberCollar);
        //        }
        //    }
        //    return ToJsonResult(MemberCollarList);
        //}

        ///// <summary>
        ///// 获取实体 
        ///// </summary>
        ///// <param name="Numbering">主键值</param>
        ///// <returns>返回对象Json</returns>
        //[HttpGet]
        //public ActionResult NumberingToGetFormJson(string Numbering)
        //{
        //    var list = new List<MemberCollarInfoModel>();
        //    var data = membercollarbll.GetEntity(f => f.Numbering == Numbering.Trim());
        //    if (data != null)
        //    {
        //        data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;
        //        data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;

        //        var childData = membercollarinfobll.GetList(f => f.CollarId == data.CollarId);

        //        foreach (var item in childData)
        //        {
        //            var memberwarehouse = memberwarehousebll.GetEntity(f => f.MemberId == item.MemberId);
        //            var MemberLibrary = memberlibrarybll.GetEntity(memberwarehouse.MemberId);
        //            var MemberCollarInfomodel = new MemberCollarInfoModel()
        //            {
        //                InfoId = item.InfoId,
        //                MemberWarehouseId = memberwarehouse.MemberWarehouseId,
        //                MemberName = MemberLibrary.MemberName,
        //                MemberNumbering = MemberLibrary.MemberNumbering,
        //                InStock = memberwarehouse.InStock,
        //                CollaredQuantity = item.CollaredQuantity,
        //                Quantity = item.CollarQuantity,
        //                UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
        //                Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
        //                Description = item.Description,
        //            };
        //            list.Add(MemberCollarInfomodel);
        //        }
        //    }
        //    var jsonData = new
        //    {
        //        entity = data,
        //        childEntity = list
        //    };
        //    return ToJsonResult(jsonData);
        //}

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = membercollarbll.GetEntity(keyValue);
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
        public ActionResult RemoveForm(string keyValue)
        {
            membercollarbll.RemoveForm(keyValue);
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
            var membercollar = membercollarbll.GetEntity(keyValue);
            membercollar.CollarNumbering = "";
            membercollarbll.SaveForm(keyValue, membercollar);
            var data = membercollarinfobll.GetList(f => f.CollarId == keyValue).ToList();
            if (data.Count()> 0)
            {
                foreach (var item in data)
                {
                    var membercollarinfo = data.Find(f => f.InfoId == item.InfoId);

                    membercollarinfo.MemberWarehouseId = "";
                    membercollarinfo.CollarQuantity = 0;
                    membercollarinfobll.SaveForm(item.InfoId, membercollarinfo);
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
        //public ActionResult SaveForm(string keyValue, MemberCollarEntity entity)
        //{
        //    membercollarbll.SaveForm(keyValue, entity);
        //    return Success("操作成功。");
        //}
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
            var entity = memberproductionorderbll.GetEntity(f => f.OrderNumbering == Numbering);
            List<MemberCollarInfoEntity> childEntitys = strChildEntitys.ToList<MemberCollarInfoEntity>();

            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    //判断库存量是否满足出库
                    var model = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    model.InStock = model.InStock.ToDecimal() - item.CollarQuantity.ToDecimal();//库存--
                    if (model.InStock < 0)
                    {
                        //var member = memberlibrarybll.GetEntity(item.MemberId);
                        return Error("存在构件库存不足构件，无法出库");
                    }
                }

                foreach (var item in childEntitys)
                {
                    //在库存量中减掉领出的数量
                    var model = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    model.InStock = model.InStock.ToDecimal() - item.CollarQuantity.ToDecimal();//库存--
                    memberwarehousebll.SaveForm(item.MemberWarehouseId, model);
                    //end

                    //修改申请中已出库量
                    var MemberProductionOrderInfo = memberproductionorderinfobll.GetEntity(item.InfoId);
                    MemberProductionOrderInfo.CollaredQuantity = MemberProductionOrderInfo.CollaredQuantity.ToDecimal() + item.CollarQuantity.ToDecimal();
                    memberproductionorderinfobll.SaveForm(MemberProductionOrderInfo.InfoId, MemberProductionOrderInfo);
                    //end

                    ////修改需求中已使用量
                    //var DemandEntity = memberdemandbll.GetEntity(entitys.MemberDemandId);
                    //DemandEntity.CollaredNumber = DemandEntity.CollaredNumber.ToDecimal() + item.CollarQuantity.ToDecimal();
                    //memberdemandbll.SaveForm(entitys.MemberDemandId,DemandEntity);
                    ////end
                }
                //数据添加至出库记录
                var childEntitys1 = new List<MemberCollarInfoEntity>();
                var entity1 = new MemberCollarEntity()
                {
                    CollarNumbering = CollarNumbering,
                    Numbering = entity.OrderNumbering,
                    CollarEngineering = entity.Category,
                    OrganizeId = entity.OrganizeId,
                    Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    CreateMan = entity.CreateMan,
                    ShippingAddress = entity.ShippingAddress,
                    ContactPerson = entity.ContactPerson,
                    ContactPersonTel = entity.ContactPersonTel,
                };
                var ListEntity = memberproductionorderinfobll.GetList(f => f.OrderId == entity.OrderId);
                if (ListEntity.Count() > 0)
                {
                    foreach (var item1 in ListEntity)
                    {
                        var data = childEntitys.Find(f=>f.MemberId== item1.MemberId);
                        var Entity = new MemberCollarInfoEntity()
                        {
                            CollarQuantity = data.CollarQuantity,
                            MemberId = item1.MemberId,
                            MemberOrderInfoId = item1.InfoId,
                            MemberWarehouseId = data.MemberWarehouseId,
                            Description = data.Description
                        };
                        childEntitys1.Add(Entity);
                    }
                }
                membercollarbll.SaveForm("", entity1, childEntitys1);
                //end
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
                List<MemberCollarEntity> list = new List<MemberCollarEntity>();
                foreach (var item in ids)
                {
                    var model = membercollarbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsReceived = 1;
                        list.Add(model);

                    }
                }
                if (list.Count > 0)
                {
                    membercollarbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }
        #endregion
    }
}
