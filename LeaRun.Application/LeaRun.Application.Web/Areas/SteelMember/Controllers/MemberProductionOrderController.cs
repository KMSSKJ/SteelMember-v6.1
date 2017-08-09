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

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-11 10:12
    /// 描 述：构件生产订单
    /// </summary>
    public class MemberProductionOrderController : MvcControllerBase
    {
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
        private MemberDemandBLL memberdemandbll = new MemberDemandBLL();
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
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
        public ActionResult OrderForm()
        {
            return View();
        }
        /// <summary>
        /// 订单表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "GJDD" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
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
            var data = memberproductionorderbll.GetPageList(pagination, queryJson);
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
            var childData = memberproductionorderbll.GetDetails(keyValue);
            var jsonData = new
            {
                entity = data,
                childEntity = childData
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
            var data = memberproductionorderbll.GetDetails(keyValue);
            return ToJsonResult(data);
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
            //var data = new List<MemberDemandEntity>(); 
            var data = memberdemandbll.GetPageList1(f => f.SubProjectId == category&&f.IsReview==1,pagination).ToList();//.OrderByDescending(o => o.MemberNumbering)
          
            foreach (var item in data)
            {
                var member = memberlibrarybll.GetList(null).Find(f => f.MemberId == item.MemberId);
                item.MemberUnit = member.MemberUnit;
            }
            return ToJsonResult(data);
        }

        /// <summary>
        /// 控制订单构件的数量（新增）
        /// </summary>
        /// <returns></returns>
        public ContentResult AddMemberNumber(string KeyValue, string category)
        {
            var MemberDemand = memberdemandbll.GetList(null).Where(s=>s.SubProjectId== category&&s.MemberId== KeyValue).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;
            var Order = memberproductionorderbll.GetList(null).FindAll(f => f.Category == category).ToList();
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

            var MemberDemand = memberdemandbll.GetList(MemberId).Where(f=>f.Category == Order.Category).SingleOrDefault();
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
            var listmember = new List<ProjectDemandModel>();
            if (KeyValue != null)
            {
                string[] array = KeyValue.Split(',');
                if (array.Count() > 0)
                {
                    if (array != null)
                        foreach (var item in array)
                        {
                            var a = memberdemandbll.GetList(null).ToList().Find(f => f.SubProjectId == category && f.MemberId == item);
                            var member = memberlibrarybll.GetList(null).Find(f=>f.MemberId==a.MemberId);
                            ProjectDemandModel projectdemand = new ProjectDemandModel();
                            projectdemand.MemberId = a.MemberId;
                            projectdemand.MemberName = a.MemberName;
                            projectdemand.MemberModel = a.MemberModel;
                            projectdemand.MemberUnit = member.MemberUnit;
                            projectdemand.UnitPrice = a.UnitPrice;
                            projectdemand.MemberNumbering =a.MemberNumbering;
                            projectdemand.MemberNumber = a.MemberNumber;
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
        public ActionResult RemoveForm(string keyValue)
        {
            memberproductionorderbll.RemoveForm(keyValue);
            var meminfo = memberproductionorderinfobll.GetList(f => f.OrderId == keyValue);
            if (meminfo.Count()>0)
            {
                foreach (var item in meminfo)
                {
                    memberproductionorderinfobll.RemoveForm(item.InfoId);
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
            if (keyValue == ""|| keyValue==null)
            {
                entity.IsPassed = entity.IsSubmit = entity.ProductionStatus= 0;
            }
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
        /// <param name="keyValues">要审核的数据的主键些</param>
        /// <param name="type">1通过，2失败</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReviewOperation(string keyValues, int type)
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
                        model.IsPassed = type;
                        model.ReviewTime = System.DateTime.Now;
                        model.ReviewMan = "测试管理员";
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

        #endregion
    }
}
