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

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNumberingList()
        {
            //List<Text> list = new List<Text>();
            var MemberCollar = membercollarbll.GetList("");

            return ToJsonResult(MemberCollar);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="Numbering">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult NumberingToGetFormJson(string Numbering)
        {
            var list = new List<MemberCollarInfoModel>();
            var data = membercollarbll.GetEntity(f => f.Numbering == Numbering.Trim());
            if (data != null)
            {
                data.DepartmentId = departmentbll.GetEntity(data.DepartmentId).FullName + "(" + organizebll.GetEntity(departmentbll.GetEntity(data.DepartmentId).OrganizeId).FullName + ")";
                data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;

                var childData = membercollarinfobll.GetList(f => f.CollarId == data.CollarId);

                foreach (var item in childData)
                {
                    var memberwarehouse = memberwarehousebll.GetEntity(f => f.MemberId== item.MemberId);
                    var MemberLibrary = memberlibrarybll.GetEntity(memberwarehouse.MemberId);
                    var MemberCollarInfomodel = new MemberCollarInfoModel()
                    {
                        InfoId = item.InfoId,
                        MemberWarehouseId = memberwarehouse.MemberWarehouseId,
                        MemberName = MemberLibrary.MemberName,
                        MemberNumbering= MemberLibrary.MemberNumbering,
                        CollarQuantity = item.CollarQuantity,
                        CollaredQuantity = item.CollaredQuantity,
                        Quantity = item.CollarQuantity,
                        UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
                        Description = item.Description,
                    };
                    list.Add(MemberCollarInfomodel);
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
            var entity = membercollarbll.GetEntity(f => f.Numbering == Numbering);
            entity.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            entity.CollarNumbering = CollarNumbering;
            entity.ReviewMan = OperatorProvider.Provider.Current().UserName;

            List<MemberCollarInfoEntity> childEntitys = strChildEntitys.ToList<MemberCollarInfoEntity>();

            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    //在库存量中减掉领出的数量
                    var model = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    model.InStock = model.InStock.ToDecimal() - item.CollarQuantity.ToDecimal();//库存--
                    memberwarehousebll.SaveForm(item.MemberWarehouseId, model);
                    //end

                    //修改出库信息
                    var entitys = membercollarinfobll.GetEntity(item.InfoId);
                    entitys.CollarQuantity = item.CollarQuantity;
                    entitys.CollaredQuantity = entitys.CollaredQuantity.ToDecimal() + item.CollarQuantity;
                    entitys.MemberWarehouseId = item.MemberWarehouseId;
                    entitys.Description = item.Description;
                    membercollarinfobll.SaveForm(item.InfoId, entitys);
                    //end

                    //修改需求中已使用量
                    var DemandEntity = memberdemandbll.GetEntity(entitys.MemberDemandId);
                    DemandEntity.CollaredNumber = DemandEntity.CollaredNumber.ToDecimal() + item.CollarQuantity;
                    //end
                }
            }
            membercollarbll.SaveForm(entity.CollarId, entity);

            return Success("操作成功。");
        }
        #endregion
    }
}
