using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Linq;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-08 11:58
    /// 描 述：原材料采购管理
    /// </summary>
    public class RawMaterialPurchaseController : MvcControllerBase
    {
        private RawMaterialPurchaseBLL rawmaterialpurchasebll = new RawMaterialPurchaseBLL();
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();

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
        public ActionResult ItemList()
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

        [HttpGet]
        public ActionResult DetailForm()
        {
            return View();
        }
        public ActionResult EditForm() {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailRawMaterialPurchase(string rawMaterialPurchaseId)
        {
            var entityrawMaterialPurchaseId = rawmaterialpurchasebll.GetEntity(rawMaterialPurchaseId);
            //entityrawMaterialPurchaseId.CreateTime; 
            return ToJsonResult(entityrawMaterialPurchaseId);
        }
        /// <summary>
        /// 获取订单下children详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailRawMaterialPurchaseInfo(string rawMaterialPurchaseId)
        {
            List<Text> list = new List<Text>();
            var listrawmaterialpurchaseInfo = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == rawMaterialPurchaseId);
            if (listrawmaterialpurchaseInfo.Count > 0)
            {
                for (int i = 0; i < listrawmaterialpurchaseInfo.Count; i++)
                {
                    var SuggestQuantity = listrawmaterialpurchaseInfo[i].PurchaseQuantity;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(listrawmaterialpurchaseInfo[i].RawMaterialAnalysisId);
                    var Entitymateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    Text projectdemand = new Text();
                    projectdemand.PurchaseQuantity = entityrawmaterialanalysis.RawMaterialDosage;//分析数量

                    projectdemand.SuggestQuantity = SuggestQuantity;
                    projectdemand.RawMaterialAnalysisId = entityrawmaterialanalysis.Id;   //分析ID                                                                      // // projectdemand.RawMaterialAnalysisId = arrayAnalysisId[i];
                    projectdemand.RawMaterialPurchaseId = rawMaterialPurchaseId;
                    projectdemand.RawMaterialName = Entitymateriallibrary.Category;
                    projectdemand.RawMaterialModel = Entitymateriallibrary.RawMaterialModel;
                    projectdemand.RawMaterialStandard = Entitymateriallibrary.RawMaterialStandard;
                    projectdemand.UnitName = Entitymateriallibrary.Unit;
                    projectdemand.Description = entityrawmaterialanalysis.Description;
                    list.Add(projectdemand);
                }
                return ToJsonResult(list);

            }
            return ToJsonResult(list);
        }
        /// <summary>
        /// 获取分析表中审核通过的材料  IsPassed=1表示通过
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult GetListByIsPassed(string category)
        {
            var list = rawmaterialanalysisbll.GetList(p => p.IsPassed == 1 && p.Category == category);
            var data = new List<RawMaterialLibraryModel>();

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var _data = new RawMaterialLibraryEntityBill();
                    var rawMaterialDosage = item.RawMaterialDosage;
                    var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    _data.RawMaterialId = model.RawMaterialId;
                    _data.RawMaterialDosage = rawMaterialDosage;
                    _data.AnalysisId = item.Id;
                    _data.RawMaterialName = model.Category;
                    _data.RawMaterialModel = model.RawMaterialModel;
                    _data.RawMaterialStandard = model.RawMaterialStandard;
                    _data.UnitName = model.Unit;
                    _data.Description = model.Description;
                    data.Add(_data);
                }
            }
            return ToJsonResult(data);
        }
        public ActionResult ListMember(string AnalysisId, string RawMaterialId, string RawMaterialName,
            string RawMaterialModel, string RawMaterialStandard, string RawMaterialDosage, string UnitName, string Description)
        {
            var inventory = 0; //库存量
            var listmember = new List<Text>();
            if (AnalysisId != null && AnalysisId != "")
            {
                string[] arrayAnalysisId = AnalysisId.Split(',');
                string[] arrayRawMaterialId = RawMaterialId.Split(',');
                string[] arrayRawMaterialName = RawMaterialName.Split(',');
                string[] arrayRawMaterialModel = RawMaterialModel.Split(',');
                string[] arrayRawMaterialStandard = RawMaterialStandard.Split(',');
                string[] arrayRawMaterialDosage = RawMaterialDosage.Split(',');
                string[] arrayUnitName = UnitName.Split(',');
                string[] arrayDescription = Description.Split(',');
                if (arrayAnalysisId.Count() > 0)
                {
                    if (arrayAnalysisId != null)
                        for (int i = 0; i < arrayAnalysisId.Length; i++)
                        {
                            Text projectdemand = new Text();
                            projectdemand.PurchaseQuantity = arrayRawMaterialDosage[i];
                            projectdemand.Inventory = inventory;
                            projectdemand.SuggestQuantity = int.Parse(arrayRawMaterialDosage[i]) - inventory;//建议采购量=需求量-库存量
                            projectdemand.RawMaterialAnalysisId = arrayAnalysisId[i];
                            projectdemand.RawMaterialPurchaseId = arrayRawMaterialId[i];
                            projectdemand.RawMaterialName = arrayRawMaterialName[i];
                            projectdemand.RawMaterialModel = arrayRawMaterialModel[i];
                            projectdemand.RawMaterialStandard = arrayRawMaterialStandard[i];
                            projectdemand.UnitName = arrayUnitName[i];
                            projectdemand.Description = arrayDescription[i];
                            listmember.Add(projectdemand);
                        }
                }
            }
            else
            {
                return Json(listmember);
            }
            return Json(listmember);
        }
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
            var data = rawmaterialpurchasebll.GetPageList(pagination, queryJson);
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
            var data = rawmaterialpurchasebll.GetEntity(keyValue);
            var childData = rawmaterialpurchasebll.GetDetails(keyValue);
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
            //var data = rawmaterialpurchasebll.GetDetails(keyValue);  
            //return ToJsonResult(data);
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var data = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == keyValue);
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModelEntity = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//实际购买量
                    var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModelEntity.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModelEntity.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModelEntity.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModelEntity.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModelEntity.RawMaterialStandard = entityrawmateriallibrary.RawMaterialStandard;
                    rawMaterialPurchaseModelEntity.UnitName = entityrawmateriallibrary.Unit;
                    rawMaterialPurchaseModelEntity.Description = entityrawmaterialanalysis.Description;
                    rawMaterialPurchaseModelEntity.RawMaterialName = entityrawmateriallibrary.Category;

                    list.Add(rawMaterialPurchaseModelEntity);
                }
            }
            
            return ToJsonResult(list);

        }

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
            rawmaterialpurchasebll.RemoveForm(keyValue);
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
            //Session
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
            entity.CreateTime = System.DateTime.Now;
            entity.IsSubmit = entity.IsSubmit == null ? 0 : entity.IsSubmit;
            entity.IsPassed = entity.IsPassed == null ? 0 : entity.IsPassed;
            entity.IsPurchase = entity.IsPurchase == null ? 0 : entity.IsPurchase;
            entity.CreateMan = entity.CreateMan == null ? "测试管理员" : entity.CreateMan;
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
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
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmit = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
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
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
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
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 提交采购
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未采购；1已采购</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitIsPurchase(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsPurchase = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }
        #endregion
    }
}
    #endregion