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
    public class MemberWarehouseRecordingController : MvcControllerBase
    {
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private MemberWarehouseBLL memberwarehousebll = new MemberWarehouseBLL();
        private MemberWarehouseRecordingBLL memberwarehouserecordingbll = new MemberWarehouseRecordingBLL();
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll= new MemberProductionOrderInfoBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();
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
        public ActionResult IntoInventoryDetail()
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
        /// <param name="Type"></param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination,string Type, string queryJson)
        {
            if (pagination.sidx == "RawMaterialName" || pagination.sidx == "RawMaterialCategory")
            {
                pagination.sidx = "RawMaterialId";
            }
            var watch = CommonHelper.TimerStart();
            var List = new List<MemberWarehouseRecordingModel>();
            var data = memberwarehouserecordingbll.GetPageList(pagination,Type, queryJson);

            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var EngineeringId = "";
                    if(!item.SubProject.IsEmpty())
                    {
                        EngineeringId = item.SubProject;
                    }
                    else {
                        EngineeringId = memberwarehousebll.GetEntity(item.MemberWarehouseId).EngineeringId;
                    }
                    
                    var MemberLibrar = memberlibrarybll.GetEntity(item.MemberId);
                    var MemberWarehouseRecording = new MemberWarehouseRecordingModel()
                    {
                        MemberNumbering = MemberLibrar.MemberNumbering,
                        MemberName = MemberLibrar.MemberName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrar.Category).ItemName,
                        UnitId = dataitemdetailbll.GetEntity(MemberLibrar.UnitId).ItemName,
                        CollarEngineering = subprojectbll.GetEntity(EngineeringId).FullName,
                        RecordingId = item.RecordingId,
                        InStock = item.InStock,
                        UpdateTime = item.UpdateTime,
                        //ToReportPeople = item.ToReportPeople,
                        //CollarDepartment= item.CollarDepartment,
                        //Receiver= item.Receiver,
                        //ReceiverTel = item.ReceiverTel,
                        Librarian = item.Librarian,
                        Description = MemberLibrar.Description
                    };
                    List.Add(MemberWarehouseRecording);
                }
            }

            //
            var queryParam = queryJson.ToJObject();

            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                //string Category = queryParam["Category"].ToString();
                List = List.FindAll(f => f.Category == Category);
            }
            if (!queryParam["MemberName"].IsEmpty())
            {
                var MemberName = queryParam["MemberName"].ToString();
                List = List.FindAll(t => t.MemberName.Contains(MemberName));
            }
            if (!queryParam["Numbering"].IsEmpty())
            {
                var Numbering = queryParam["Numbering"].ToString();
                List = List.FindAll(t => t.MemberNumbering.Contains(Numbering));
            }
            //
            var jsonData = new
            {
                rows = List,
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
            var data = memberwarehouserecordingbll.GetList(queryJson);
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
            var data = memberwarehouserecordingbll.GetEntity(keyValue);
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
            memberwarehouserecordingbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, MemberWarehouseRecordingEntity entity)
        {
            memberwarehouserecordingbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
