using LeaRun.Application.Busines;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity;
using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using LeaRun.Util;
using LeaRun.Util.Attributes;
using LeaRun.Util.Extension;
using LeaRun.Util.Log;
using LeaRun.Util.Offices;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : MvcControllerBase
    {
        UserBLL user = new UserBLL();
        DepartmentBLL department = new DepartmentBLL();
#pragma warning disable CS0108 // “HomeController.noticebll”隐藏继承的成员“MvcControllerBase.noticebll”。如果是有意隐藏，请使用关键字 new。
        private NoticeBLL noticebll = new NoticeBLL();
#pragma warning restore CS0108 // “HomeController.noticebll”隐藏继承的成员“MvcControllerBase.noticebll”。如果是有意隐藏，请使用关键字 new。

        #region 视图功能
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {
            var data = systemconfigurationbll.GetList(null).SingleOrDefault();
            string prjName = "";
            var virtualPath1 = "";
            if (data != null)
            {
                virtualPath1 = data.SystemLogo;
                prjName = data.SystemName;
            }
            ViewData["prjLogo"] = virtualPath1;
            ViewData["prjName"] = prjName + "钢构件生产管理系统";
            return View();
        }
        public ActionResult AdminLTE()
        {
            var data = systemconfigurationbll.GetList(null).SingleOrDefault();
            string prjName = "";
            var virtualPath1 = "";
            if (data != null)
            {
                virtualPath1 = data.SystemLogo;
                prjName = data.SystemName;
            }
            ViewData["prjLogo"] = virtualPath1;
            ViewData["prjName"] = prjName + "钢构件生产管理系统";
            return View();
        }
        public ActionResult AdminWindos()
        {
            var data = systemconfigurationbll.GetList(null).SingleOrDefault();
            string prjName = "";
            var virtualPath1 = "";
            if (data != null)
            {

                virtualPath1 = data.SystemLogo;
                prjName = data.SystemName;
            }
            ViewData["prjLogo"] = virtualPath1;
            ViewData["prjName"] = prjName + "钢构件生产管理系统";
            return View();
        }
        public ActionResult AdminPretty()
        {
            var data = systemconfigurationbll.GetList(null).SingleOrDefault();
            string prjName = "";
            var virtualPath1 = "";
            if (data != null)
            {

                virtualPath1 = data.SystemLogo;
                prjName = data.SystemName;
            }
            ViewData["prjLogo"] = virtualPath1;
            ViewData["prjName"] = prjName + "钢构件生产管理系统";
            return View();
        }
        public ActionResult AdminDefaultDesktop()
        {
            return View();
        }
        public ActionResult AdminLTEDesktop()
        {
            return View();
        }
        public ActionResult AdminWindosDesktop()
        {
            return View();
        }
        public ActionResult AdminPrettyDesktop()
        {
            return View();
        }

        public ActionResult ToBeDoneMoreIndex()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 2;
            logEntity.OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.OperateAccount = OperatorProvider.Provider.Current().Account;
            logEntity.OperateUserId = OperatorProvider.Provider.Current().UserId;
            logEntity.ModuleId = moduleId;
            logEntity.Module = moduleName;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Content(moduleId);
        }
        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }
        #endregion
        #region 获取数据
        [HttpGet]
        public ActionResult GetMemberSumProportion()
        {
            List<MemberDemandEntity> MemberDemandList = new List<MemberDemandEntity>();
            var Sub = subprojectbll.GetList(null);

            foreach (var item in Sub)
            {

                var MemberDemand = memberdemandbll.GetList(f => f.SubProjectId == item.Id && f.IsReview == 1);
                if (MemberDemand.Count() > 0)
                {
                    foreach (var item1 in MemberDemand)
                    {
                        if (MemberDemandList.Count() > 0)
                        {
                            var MemberDemand1 = MemberDemandList.Find(f => f.SubProjectId == subprojectbll.GetEntity(item1.SubProjectId).FullName);
                            if (MemberDemand1 != null)
                            {
                                MemberDemand1.MemberNumber += item1.MemberNumber;
                            }
                            else
                            {
                                var _MemberDemandEntity = new MemberDemandEntity()
                                {
                                    SubProjectId = subprojectbll.GetEntity(item1.SubProjectId).FullName,
                                    MemberNumber = item1.MemberNumber
                                };
                                MemberDemandList.Add(_MemberDemandEntity);
                            }
                        }
                        else
                        {
                            var _MemberDemandEntity = new MemberDemandEntity()
                            {
                                SubProjectId = subprojectbll.GetEntity(item1.SubProjectId).FullName,
                                MemberNumber = item1.MemberNumber
                            };
                            MemberDemandList.Add(_MemberDemandEntity);
                        }
                    }
                }

            }
            return ToJsonResult(MemberDemandList);
        }

        [HttpGet]
        public ActionResult GetOrderSumProportion()
        {
            List<MemberProductionOrderEntity> MemberProductionOrderList = new List<MemberProductionOrderEntity>();
            var Sub = subprojectbll.GetList(null);

            foreach (var item in Sub)
            {
                var MemberProductionOrder = memberproductionorderbll.GetList(f => f.Category == item.Id && f.IsPassed == 1);
                if (MemberProductionOrder.Count() > 0)
                {
                    var _MemberProductionOrderEntity = new MemberProductionOrderEntity()
                    {
                        Category = subprojectbll.GetEntity(item.Id).FullName,
                        Priority = MemberProductionOrder.Count()
                    };
                    MemberProductionOrderList.Add(_MemberProductionOrderEntity);
                }

            }
            return ToJsonResult(MemberProductionOrderList);

        }

        [HttpGet]
        public ActionResult GetMemberProduceSchedule()
        {
            List<MemberProductionOrderInfoEntity> MemberDemandList = new List<MemberProductionOrderInfoEntity>();
            var Sub = subprojectbll.GetList(null);

            foreach (var item in Sub)
            {
                var MemberProductionOrder = memberproductionorderbll.GetList(f => f.Category == item.Id && f.IsPassed == 1);
                if (MemberProductionOrder.Count() > 0)
                {
                    foreach (var item1 in MemberProductionOrder)
                    {
                        var MemberProductionOrderInfo = memberproductionorderinfobll.GetList(f => f.OrderId == item1.OrderId);
                        if (MemberProductionOrderInfo.Count() > 0)
                        {
                            foreach (var item2 in MemberProductionOrderInfo)
                            {
                                if (MemberProductionOrderInfo.Count() > 0)
                                {
                                    var MemberProductionOrder1 = MemberDemandList.Find(f => f.InfoId == subprojectbll.GetEntity(item.Id).FullName);
                                    if (MemberProductionOrder1 != null)
                                    {
                                        MemberProductionOrder1.ProductionQuantity += Convert.ToInt32(item2.ProductionQuantity);//计划量
                                        MemberProductionOrder1.QualityInspectionNumber += Convert.ToInt32(item2.QualityInspectionNumber);
                                    }
                                    else
                                    {
                                        var _MemberDemandEntity = new MemberProductionOrderInfoEntity()
                                        {
                                            InfoId = subprojectbll.GetEntity(item.Id).FullName,
                                            ProductionQuantity = item2.ProductionQuantity,//计划量
                                            QualityInspectionNumber = Convert.ToInt32(item2.QualityInspectionNumber)
                                        };
                                        MemberDemandList.Add(_MemberDemandEntity);
                                    }
                                }
                                else
                                {
                                    var _MemberDemandEntity = new MemberProductionOrderInfoEntity()
                                    {
                                        InfoId = subprojectbll.GetEntity(item.Id).FullName,
                                        ProductionQuantity = item2.ProductionQuantity,
                                        QualityInspectionNumber = Convert.ToInt32(item2.QualityInspectionNumber)
                                    };
                                    MemberDemandList.Add(_MemberDemandEntity);
                                }
                            }
                        }
                    }
                }

            }
            return ToJsonResult(MemberDemandList);
        }

        /// <summary>
        /// 获取公告最新6条
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNotice()
        {
            var NoticeList = noticebll.GetList();
            var list = DataTableToList<NewsEntity>.ConvertToModel(NoticeList);
            return ToJsonResult(list);
        }

        /// <summary>
        /// 更多待办
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetToBeDoneMore(Pagination pagination, string queryJson)
        {
            var list = new List<ToBeDoneModel>();
            //材料需求
            var RawMaterialAnalysisList = rawmaterialanalysisbll.GetList(f => f.IsSubmitReview == 1 && f.IsPassed == 0);
            if (RawMaterialAnalysisList.Count() > 0)
            {
                foreach (var item in RawMaterialAnalysisList)
                {
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.Id,
                        Type = "RawMaterialAnalysis",
                        Category = "审核",
                        FullHead = item.CreateMan + "的材料需求",
                        FullHeadColor = "red",
                        ReleaseTime = item.ReviewTime,
                    };
                    list.Add(ToBeDone);
                }
            }

            //材料申请
            var RawMaterialOrderList = rawmaterialorderbll.GetList(f => f.IsSubmit == 1 && f.IsPassed == 0);
            if (RawMaterialOrderList.Count() > 0)
            {
                foreach (var item in RawMaterialOrderList)
                {
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.OrderId,
                        Type = "RawMaterialOrder",
                        Category = "审核",
                        FullHead = item.CreateMan + "的材料申请",
                        FullHeadColor = "red",
                    };
                    list.Add(ToBeDone);
                }
            }

            //材料申请
            var RawMaterialPurchaseList = rawmaterialpurchasebll.GetList(f => f.IsSubmit == 1 && f.IsPassed == 0);
            if (RawMaterialPurchaseList.Count() > 0)
            {
                foreach (var item in RawMaterialPurchaseList)
                {
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.RawMaterialPurchaseId,
                        Type = "RawMaterialPurchase",
                        Category = "审核",
                        FullHead = item.CreateMan + "的材料申请",
                        FullHeadColor = "red",
                    };
                    list.Add(ToBeDone);
                }
            }

            //构件需求
            var MemberDemandList = memberdemandbll.GetList(f => f.IsSubmit == 1 && f.IsReview == 0);
            if (MemberDemandList.Count() > 0)
            {
                foreach (var item in MemberDemandList)
                {
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.MemberDemandId,
                        Type = "MemberDemand",
                        Category = "审核",
                        FullHead = item.CreateMan + "的构件需求",
                        FullHeadColor = "red",
                        ReleaseTime = item.ReviewTime,
                    };
                    list.Add(ToBeDone);
                }
            }

            //构件申请
            var MemberOrderList = memberproductionorderbll.GetList(f => f.IsSubmit == 1 && f.IsPassed == 0);
            if (MemberOrderList.Count() > 0)
            {
                foreach (var item in MemberOrderList)
                {
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.OrderId,
                        Type = "MemberOrder",
                        Category = "审核",
                        FullHead = item.CreateMan + "的构件申请",
                        FullHeadColor = "red",
                    };
                    list.Add(ToBeDone);
                }
            }

            //构件生产确认
            var MemberProcessList = memberproductionorderbll.GetList(f => f.IsPassed == 1 && f.IsConfirm == 1);
            if (MemberProcessList.Count() > 0)
            {
                foreach (var item in MemberProcessList)
                {
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.OrderId,
                        Type = "MemberProcess",
                        Category = "审核",
                        FullHead = item.CreateMan + "的构件生产",
                        FullHeadColor = "red",
                    };
                    list.Add(ToBeDone);
                }
            }

            //生产设备维护
            var ProduceEquipmentList = produceequipmentbll.GetList(f => f.Status == 0 && f.Status == 3);
            if (ProduceEquipmentList.Count() > 0)
            {
                foreach (var item in ProduceEquipmentList)
                {
                    var text = item.Status == 0 ? "需保养" : "故障";
                    var Color = item.Status == 0 ? "brown" : "yellow";
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.Id,
                        Type = "ProduceEquipment",
                        Category = "工作",
                        FullHead = item.Name + text,
                        FullHeadColor = Color,
                        ReleaseTime = item.UpdateTime,
                    };
                    list.Add(ToBeDone);
                }
            }

            //安全设备维护
            var SafetyEquipmentList = safetyequipmentbll.GetList(f => f.Status == 0 && f.Status == 3);
            if (SafetyEquipmentList.Count() > 0)
            {
                foreach (var item in SafetyEquipmentList)
                {
                    var text = item.Status == 0 ? "需保养" : "故障";
                    var Color = item.Status == 0 ? "brown" : "yellow";
                    var ToBeDone = new ToBeDoneModel()
                    {
                        Id = item.Id,
                        Type = "ProduceEquipment",
                        Category = "工作",
                        FullHead = item.Name + text,
                        FullHeadColor = Color,
                        ReleaseTime = item.UpdateTime,
                    };
                    list.Add(ToBeDone);
                }
            }

            var queryParam = queryJson.ToJObject();
            if (!queryParam["FullHead"].IsEmpty())
            {
                string FullHead = queryParam["FullHead"].ToString();
                list = list.FindAll(t => t.FullHead.Contains(FullHead));
            }

            list = list.OrderByDescending(f => f.ReleaseTime).ToList();

            var JsonData = new
            {
                rows = list.Skip((pagination.page - 1) * pagination.rows).Take(pagination.rows),
                total = pagination.total + 1,//总页数
                page = pagination.page,//当前页
                records = list.Count(),//总记录数
            };
            return ToJsonResult(JsonData);
        }

        #endregion
    }
}
