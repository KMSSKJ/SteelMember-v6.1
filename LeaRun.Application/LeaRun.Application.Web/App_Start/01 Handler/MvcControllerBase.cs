using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Util;
using LeaRun.Util.Log;
using LeaRun.Util.WebControl;
using System.Web.Mvc;

namespace LeaRun.Application.Web
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.11.9 10:45
    /// 描 述：控制器基类
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public abstract class MvcControllerBase : Controller
    {
        public MemberCollarBLL membercollarbll = new MemberCollarBLL();
        public MemberCollarInfoBLL membercollarinfobll = new MemberCollarInfoBLL();
        public MemberWarehouseBLL memberwarehousebll = new MemberWarehouseBLL();
        public MemberDemandBLL memberdemandbll = new MemberDemandBLL();
        public MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();

        public OrganizeBLL organizebll = new OrganizeBLL();
        public UserBLL userbll = new UserBLL();
        public DepartmentBLL departmentbll = new DepartmentBLL();
        public SubProjectBLL subprojectbll = new SubProjectBLL();
        public DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();
        public MemberMaterialBLL membermaterialbll = new MemberMaterialBLL();
        public MemberProcessBLL memberprocessbll = new MemberProcessBLL();
        public ProjectInfoBLL projectinfobll = new ProjectInfoBLL();
        public SystemConfigurationBLL systemconfigurationbll = new SystemConfigurationBLL();
        public RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        public DataItemBLL dataitembll = new DataItemBLL();
        public DataItemCache dataItemCache = new DataItemCache();
        public MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        public MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
        public MemberWarehouseRecordingBLL memberwarehouserecordingbll = new MemberWarehouseRecordingBLL();
        public ProduceEquipmentBLL produceequipmentbll = new ProduceEquipmentBLL();
        public EquipmentMaintenanceRecordsBLL equipmentmaintenancerecordsbll = new EquipmentMaintenanceRecordsBLL();
        public RawMaterialOrderInfoBLL rawmaterialorderinfobll = new RawMaterialOrderInfoBLL();
        public RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();
        public RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        public RawMaterialPurchaseBLL rawmaterialpurchasebll = new RawMaterialPurchaseBLL();
        public RawMaterialWarehouseBLL rawmaterialwarehousebll = new RawMaterialWarehouseBLL();
        public RawMaterialOrderBLL rawmaterialorderbll = new RawMaterialOrderBLL();
        public RawMterialCollarBLL rawmterialcollarbll = new RawMterialCollarBLL();
        public RawMterialCollarInfoBLL rawmterialcollarinfobll = new RawMterialCollarInfoBLL();
        public SafetyEquipmentBLL safetyequipmentbll = new SafetyEquipmentBLL();
        public NoticeBLL noticebll = new NoticeBLL();

        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult ToJsonResult(object data)
        {
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message, resultdata = data }.ToJson());
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { type = ResultType.error, message = message }.ToJson());
        }

        //public int DeleteVerification(string keyValue )
        //{

        //}
    }
}
