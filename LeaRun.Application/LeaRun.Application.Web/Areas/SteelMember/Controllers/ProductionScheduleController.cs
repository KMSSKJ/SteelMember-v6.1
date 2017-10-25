using LeaRun.Application.Busines.SteelMember;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class ProductionScheduleController : MvcControllerBase
    {
        // private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();

        //
        // GET: /SteelMember/ProductionSchedule/
        #region 视图功能
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Schedule()
        {
            return View();
        }

        #endregion

        #region 获取数据
        public ActionResult JsonData(string keyValue)
        {
            //var data = memberproductionorderbll.GetEntity(keyValue);Array
            string[] X_Array = new string[] { };
            int[] Y_MemberNumberArray = new int[] { };
            int[] Y_ProductionedQuantityArray = new int[] { };

            List<string> X_List = new List<string>();
            List<int> Y_MemberNumberList = new List<int>();
            List<int> Y_ProductionedQuantityList = new List<int>();

            var childData = memberproductionorderinfobll.GetList(g => g.OrderId == keyValue);
            for (int i = 0; i < childData.Count(); i++)
            {
                var MemberLibrary = memberlibrarybll.GetEntity(childData[i].MemberId);

                X_List.Add(dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName + MemberLibrary.MemberName);
                Y_MemberNumberList.Add(Convert.ToInt32(childData[i].ProductionQuantity));
                Y_ProductionedQuantityList.Add(Convert.ToInt32(childData[i].ProductionedQuantity));
            }
            var jsonData = new
            {
                X_Array = X_List.ToArray(),
                Y_MemberNumberArray = Y_MemberNumberList.ToArray(),
                Y_ProductionedQuantityArray = Y_ProductionedQuantityList.ToArray()
            };
            return ToJsonResult(jsonData);
        }

        #endregion
    }
}
