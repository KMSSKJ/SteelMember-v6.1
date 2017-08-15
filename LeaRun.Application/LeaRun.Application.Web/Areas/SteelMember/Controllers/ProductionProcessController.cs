using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Code;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Application.Web.Areas.SteelMember.Models;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class ProductionProcessController : MvcControllerBase
    {
        //
        // GET: /SteelMember/ProductionProcess/
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
        private RawMaterialOrderInfoBLL rawmaterialorderinfobll = new RawMaterialOrderInfoBLL();
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private MemberMaterialBLL membermaterialbll = new MemberMaterialBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
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
        /// 领取订单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveOrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 领取材料页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveRawMaterialIndex()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = ""; //memberprocessbll.GetList(queryJson);
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
            var data = "";//memberprocessbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="OrderId">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GridListJsonRawMaterial(string OrderId)
        {
            var RawMaterial =new List<RawMaterialNumberModel>();

            var orderinfo = memberproductionorderinfobll.GetList(f=>f.OrderId==OrderId);
            if (orderinfo.Count>0)
            {
                foreach (var item in orderinfo)
                {
                    var membermaterial = membermaterialbll.GetList(item.MemberId);
                    if (membermaterial.Count()>0)
                    {
                        foreach (var item1 in membermaterial)
                        {
                            var rawmaterial = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                            if (rawmaterial!=null)
                            {
                                var RawMaterialNumber=new RawMaterialNumberModel() {
                                RawMaterialId = rawmaterial.RawMaterialId,
                                RawMaterialModel = rawmaterial.RawMaterialModel,
                                RawMaterialName = rawmaterial.RawMaterialName,
                                Description = rawmaterial.Description,
                                UnitName = rawmaterial.Unit
                            };
                            
                                if (RawMaterial.Count()>0)
                                {
                                    var a = RawMaterial.Where(w => w.RawMaterialId == RawMaterialNumber.RawMaterialId).SingleOrDefault();
                                    if (a!=null)
                                    {
                                        RawMaterial.Where(r=>r.RawMaterialId==a.RawMaterialId).Single().RawMaterialNumber+= item1.RawMaterialNumber * item.ProductionQuantity;
                                    }
                                    else
                                    {
                                        RawMaterialNumber.RawMaterialNumber = item1.RawMaterialNumber * item.ProductionQuantity;
                                        RawMaterial.Add(RawMaterialNumber);
                                    }

                                }
                                else
                                {
                                    RawMaterialNumber.RawMaterialNumber = item1.RawMaterialNumber * item.ProductionQuantity;
                                    RawMaterial.Add(RawMaterialNumber);
                                }
                            }
                        }
                    }
                }
            }
            return ToJsonResult(RawMaterial);
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
            //memberprocessbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, MemberProcessEntity entity)
        {
            //memberprocessbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 领取订单
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未提交；1提交</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReceiveOrder(string keyValues)
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
                        model.IsReceive = 1;
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
