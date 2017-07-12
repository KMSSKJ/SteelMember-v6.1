using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;

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

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 订单表单
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderForm(string KeyValue)
        {
            //if (KeyValue == "" || KeyValue == null)
            //{
            //    ViewBag.OrderNumbering = "DD" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            //    ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;
            //}
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

        class DemandList
        {
            public string MemberNumbering { get; set; }
            public string MemberName { get; set; }
            public string MemberModel { get; set; }
            public int MemberNumber { get; set; }
            public string MemberUnit { get; set; }
        }

        [HttpGet]
        public ActionResult GridListJsonDemand(string category)
        {
            var data = new List<DemandList>();
            for (int i = 0; i < 50; i++)
            {
                var model = new DemandList();
                model.MemberNumbering = "1000" + (i+1);
                model.MemberName = "构件名称" + (i + 1);
                model.MemberModel = "规格" + (i + 1);
                model.MemberNumber = (i + 1);
                model.MemberUnit = "个";
                data.Add(model);

            }
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
            memberproductionorderbll.RemoveForm(keyValue);
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
            List<MemberProductionOrderInfoEntity> childEntitys = strChildEntitys.ToList<MemberProductionOrderInfoEntity>();
            memberproductionorderbll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }
        #endregion
    }
}
