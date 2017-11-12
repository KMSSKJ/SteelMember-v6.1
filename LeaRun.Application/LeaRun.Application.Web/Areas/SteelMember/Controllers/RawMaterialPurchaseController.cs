using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Linq;
using LeaRun.Util.Extension;
using System;
using LeaRun.Application.Code;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Busines.BaseManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-08 11:58
    /// �� �������ϲɹ�����
    /// </summary>
    public class RawMaterialPurchaseController : MvcControllerBase
    {
        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ItemList()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form(string keyValue)
        {
            if (keyValue == "" || keyValue == null)
            {

                ViewData["PurchaseNumbering"] = "CGDD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;

            }
            return View();
        }

        //[HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Purchase()
        {
            return View();
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotReviewForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailRawMaterialPurchase(string rawMaterialPurchaseId)
        {
            var entityrawMaterialPurchaseId = rawmaterialpurchasebll.GetEntity(rawMaterialPurchaseId);
            //entityrawMaterialPurchaseId.CreateTime; 
            var data = ToJsonResult(entityrawMaterialPurchaseId);
            return data;
        }
        ///// <summary>
        ///// ��ȡ������children����
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult DetailRawMaterialPurchaseInfo(string rawMaterialPurchaseId)
        //{
        //    List<Text> list = new List<Text>();
        //    var listrawmaterialpurchaseInfo = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == rawMaterialPurchaseId);
        //    if (listrawmaterialpurchaseInfo.Count > 0)
        //    {
        //        for (int i = 0; i < listrawmaterialpurchaseInfo.Count; i++)
        //        {
        //            var SuggestQuantity = listrawmaterialpurchaseInfo[i].PurchaseQuantity;
        //            var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(listrawmaterialpurchaseInfo[i].RawMaterialOrderInfoId);
        //            var Entitymateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
        //            Text projectdemand = new Text()
        //            {
        //                PurchaseQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString(),//��ɹ�����

        //                InfoId = listrawmaterialpurchaseInfo[i].InfoId,
        //                RawMaterialAnalysisId = entityrawmaterialanalysis.Id,   //����ID                                                                      // // projectdemand.RawMaterialAnalysisId = arrayAnalysisId[i];
        //                RawMaterialPurchaseId = rawMaterialPurchaseId,
        //                RawMaterialName = Entitymateriallibrary.Category,
        //                RawMaterialModel = Entitymateriallibrary.RawMaterialModel,
        //                UnitId = Entitymateriallibrary.Unit,
        //                Description = entityrawmaterialanalysis.Description,
        //                Price = listrawmaterialpurchaseInfo[i].Price.IsEmpty() ? 0 : listrawmaterialpurchaseInfo[i].Price,
        //                RawMaterialSupplier = listrawmaterialpurchaseInfo[i].RawMaterialSupplier,
        //            };
        //            list.Add(projectdemand);
        //        }
        //        return ToJsonResult(list);

        //    }
        //    return ToJsonResult(list);
        //}

        /// <summary>
        /// ��ȡ���������ͨ���Ĳ���  IsPassed=1��ʾͨ��
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetListByIsPassed(Pagination pagination, string queryJson, string keyValue)
        {
            var data = new List<RawMaterialLibraryModel>();
            var list = rawmaterialorderbll.GetPageList(pagination, queryJson);
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var rawmaterialorderinfolist = rawmaterialorderinfobll.GetList(f => f.OrderId == item.OrderId);
                    if (rawmaterialorderinfolist.Count() > 0)
                    {
                        foreach (var item1 in rawmaterialorderinfolist)
                        {
                            if (data.Count() > 0)
                            {
                                var RawMaterial = data.Where(w => w.RawMaterialId == item1.RawMaterialId).SingleOrDefault();
                                if (RawMaterial != null)
                                {
                                    RawMaterial.Qty += item1.Quantity.ToDecimal();
                                }
                                else
                                {
                                    var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                                    var _data = new RawMaterialLibraryModel()
                                    {
                                        RawMaterialId = model.RawMaterialId,
                                        Qty = item1.Quantity,
                                        InfoId = item1.InfoId,
                                        RawMaterialName = model.RawMaterialName,
                                        RawMaterialModel = model.RawMaterialModel,
                                        Category = model.Category,
                                        UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                                        Price = item1.Price,
                                        Date = item1.Time.ToDate(),
                                        RawMaterialManufacturer = item1.RawMaterialManufacturer,
                                        RawMaterialSupplier = item1.RawMaterialSupplier,
                                        Description = model.Description,
                                    };
                                    data.Add(_data);
                                }
                            }
                            else
                            {
                                var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                                var _data = new RawMaterialLibraryModel()
                                {
                                    RawMaterialId = model.RawMaterialId,
                                    Qty = item1.Quantity,
                                    InfoId = item1.InfoId,
                                    Date = item1.Time.ToDate(),
                                    RawMaterialName = model.RawMaterialName,
                                    RawMaterialModel = model.RawMaterialModel,
                                    Category = model.Category,
                                    UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                                    Description = model.Description,
                                };
                                data.Add(_data);
                            }
                        }
                    }
                }
            }
            var datapurchase = new List<RawMaterialLibraryModel>();
            var listpurchase = rawmaterialpurchasebll.GetList(""); //f => f.IsPurchase == 1
            if (listpurchase.Count() > 0)
            {
                foreach (var item in listpurchase)
                {
                    var rawmaterialorderpurchaselist = rawmaterialpurchasebll.GetInfoList(f => f.RawMaterialPurchaseId == item.RawMaterialPurchaseId);
                    if (rawmaterialorderpurchaselist.Count() > 0)
                    {
                        foreach (var item1 in rawmaterialorderpurchaselist)
                        {
                            if (datapurchase.Count() > 0)
                            {
                                var RawMaterial = datapurchase.Where(w => w.RawMaterialId == item1.RawMaterialId).SingleOrDefault();
                                if (RawMaterial != null)
                                {
                                    RawMaterial.Qty += item1.PurchaseQuantity.ToDecimal();
                                }
                                else
                                {
                                    var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                                    var _data = new RawMaterialLibraryModel()
                                    {
                                        RawMaterialId = model.RawMaterialId,
                                        Qty = item1.PurchaseQuantity,
                                        InfoId = item1.InfoId,
                                        RawMaterialName = model.RawMaterialName,
                                        RawMaterialModel = model.RawMaterialModel,
                                        Category = model.Category,
                                        UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                                        Price = item1.Price,
                                        //Date = item1..ToDate(),
                                        RawMaterialManufacturer = item1.RawMaterialManufacturer,
                                        RawMaterialSupplier = item1.RawMaterialSupplier,
                                        Description = model.Description,
                                    };
                                    datapurchase.Add(_data);
                                }
                            }
                            else
                            {
                                var model = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);
                                var _data = new RawMaterialLibraryModel()
                                {
                                    RawMaterialId = model.RawMaterialId,
                                    Qty = item1.PurchaseQuantity,
                                    InfoId = item1.InfoId,
                                    RawMaterialName = model.RawMaterialName,
                                    RawMaterialModel = model.RawMaterialModel,
                                    Category = model.Category,
                                    UnitId = dataitemdetailbll.GetEntity(model.Unit).ItemName,
                                    //Date = item1.Time.ToDate(),
                                    Description = model.Description,
                                };
                                datapurchase.Add(_data);
                            }
                        }
                    }
                }
            }

            if (data.Count() > 0)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    var datapurchaseEntity = datapurchase.Where(w => w.RawMaterialId == data[i].RawMaterialId).SingleOrDefault();

                    if (datapurchaseEntity != null)
                    {
                        if (!keyValue.IsEmpty())
                        {
                            var rawmaterialorderpurchaseEntity = rawmaterialpurchasebll.GetInfoList(f => f.RawMaterialId == data[i].RawMaterialId && f.RawMaterialPurchaseId == keyValue).SingleOrDefault();
                            data[i].PurchasedQuantity = datapurchaseEntity.Qty + rawmaterialorderpurchaseEntity.PurchaseQuantity;
                        }
                        else
                        {
                            data[i].PurchasedQuantity = datapurchaseEntity.Qty;
                            // data[i].Qty = data[i].Qty - datapurchaseEntity.Qty;
                        }
                    }
                    if (data[i].PurchasedQuantity == null)
                    {
                        data[i].PurchasedQuantity = 0;
                    }
                }
                data = data.Where(f => f.Qty != f.PurchasedQuantity).ToList();//���Ѳɹ����Ƴ�
            }
            var queryParam = queryJson.ToJObject();
            if (!queryParam["MaterialCategory"].IsEmpty())
            {
                string MaterialCategory = queryParam["MaterialCategory"].ToString();
                data = data.FindAll(t => t.Category == MaterialCategory);
                foreach (var item in data)
                {
                    item.Category = dataitemdetailbll.GetEntity(item.Category).ItemName;
                }
            }

            return ToJsonResult(data);
        }
        public ActionResult ListMember(string InfoId, string RawMaterialId, string RawMaterialOrderInfoId, string RawMaterialName,
            string RawMaterialModel, string Price, string Quantity, string PurchasedQuantity, string UnitId, string Description, string RawMaterialSupplier, string RawMaterialManufacturer)
        {
            // var inventory = 0; //�����
            var listmember = new List<Text>();
            if (InfoId != null && InfoId != "")
            {
                string[] arrayAnalysisId = InfoId.Split(',');
                string[] arrayRawMaterialId = RawMaterialId.Split(',');
                string[] arrayRawMaterialOrderInfoId = RawMaterialOrderInfoId.Split(',');
                string[] arrayRawMaterialName = RawMaterialName.Split(',');
                string[] arrayRawMaterialModel = RawMaterialModel.Split(',');
                string[] arrayRawMaterialPrice = Price.Split(',');
                string[] arrayRawMaterialQuantity = Quantity.Split(',');
                string[] arrayRawMaterialPurchasedQuantity = PurchasedQuantity.Split(',');
                string[] arrayUnitId = UnitId.Split(',');
                string[] arrayDescription = Description.Split(',');
                string[] arrayRawMaterialSupplier = RawMaterialSupplier.Split(',');
                string[] arrayRawMaterialManufacturer = RawMaterialManufacturer.Split(',');
                if (arrayAnalysisId.Count() > 0)
                {
                    if (arrayAnalysisId != null)
                        for (int i = 0; i < arrayAnalysisId.Length; i++)
                        {
                            // var sb = arrayRawMaterialId[i];
                            //var rawmaterialinventory = rawmaterialinventorybll.GetEntityByRawMaterialId(arrayRawMaterialId[i].ToString());
                            //var inventory = rawmaterialinventory.Quantity.ToDecimal();//��ɹ���
                            Text projectdemand = new Text()
                            {
                                PurchaseQuantity = arrayRawMaterialPurchasedQuantity[i],
                                Quantity = arrayRawMaterialQuantity[i].ToDecimal(),
                                RawMaterialId = arrayRawMaterialId[i].ToString(),
                                RawMaterialOrderInfoId = arrayRawMaterialOrderInfoId[i],
                                RawMaterialPurchaseId = arrayRawMaterialId[i],
                                RawMaterialName = arrayRawMaterialName[i],
                                RawMaterialModel = arrayRawMaterialModel[i],
                                UnitId = arrayUnitId[i],
                                Price = arrayRawMaterialPrice[i].ToDecimal(),
                                Description = arrayDescription[i],
                                RawMaterialSupplier = arrayRawMaterialSupplier[i],
                                RawMaterialManufacturer = arrayRawMaterialManufacturer[i],
                            };
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// �༭��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmaterialpurchasebll.GetEntity(keyValue);
            var childData = rawmaterialpurchasebll.GetDetails(keyValue).ToList();
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            if (childData.Count > 0)
            {
                foreach (var item in childData)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    //var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    //var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == item.RawMaterialId);
                    rawMaterialPurchaseModel.Quantity = rawmaterialpurchasebll.GetEntity(f=>f.RawMaterialOrderInfoId== item.RawMaterialOrderInfoId).PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialOrderInfoId = item.RawMaterialOrderInfoId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialId = entityrawmateriallibrary.RawMaterialId;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Description = item.Description;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.Price = item.Price;
                    rawMaterialPurchaseModel.RawMaterialManufacturer = item.RawMaterialManufacturer;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Quantity = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.PurchasedQuantity = item..ToString();
                    list.Add(rawMaterialPurchaseModel);

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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson1(string keyValue)
        {
            var data = rawmaterialpurchasebll.GetEntity(keyValue);
            
            var childData = rawmaterialpurchasebll.GetDetails(keyValue).ToList();
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var rawmaterialorder = new RawMaterialOrderEntity();
            if (childData.Count > 0)
            {
              
                foreach (var item in childData)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    //var rawMaterialAnalysisId = item.RawMaterialOrderInfoId;
                    var OrderId = rawmaterialorderinfobll.GetEntity(item.RawMaterialOrderInfoId).OrderId;
                     rawmaterialorder = rawmaterialorderbll.GetEntity(OrderId);
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    var entityrawmaterialinventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialOrderInfoId = item.RawMaterialOrderInfoId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialId = entityrawmateriallibrary.RawMaterialId;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Description = entityrawmaterialanalysis.Description;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.Price = item.Price;
                    rawMaterialPurchaseModel.RawMaterialManufacturer = item.RawMaterialManufacturer;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                   
                    //rawMaterialPurchaseModel.Quantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }
            var DepartmentId = departmentbll.GetEntity(rawmaterialorder.DepartmentId).FullName + "(" + organizebll.GetEntity(departmentbll.GetEntity(rawmaterialorder.DepartmentId).OrganizeId).FullName + ")";
            var Category = subprojectbll.GetEntity(rawmaterialorder.Category).FullName;
            var jsonData = new
            {
                DepartmentId= DepartmentId,
                Category= Category,
                entity = data,
                childEntity = list
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var data = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == keyValue);
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    //var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    //var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialOrderInfoId = item.RawMaterialOrderInfoId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Category = entityrawmateriallibrary.Category;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialPurchaseModelPrice = item.Price;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Inventory = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.SuggestQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }

            return ToJsonResult(list);

        }

        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ(�������ʱ)
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson1(string keyValue)
        {
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var rawmaterialpurchase = rawmaterialpurchasebll.GetEntity(keyValue);
            var data = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == rawmaterialpurchase.RawMaterialPurchaseId);
            var CategoryName = "";
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    //var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    //var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    CategoryName = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Category).ItemName;
                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialOrderInfoId = item.RawMaterialOrderInfoId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitId = dataitemdetailbll.GetEntity(entityrawmateriallibrary.Unit).ItemName;
                    rawMaterialPurchaseModel.Category = entityrawmateriallibrary.Category;
                    rawMaterialPurchaseModel.RawMaterialId = entityrawmateriallibrary.RawMaterialId;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialPurchaseModelPrice = item.Price;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Inventory = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.SuggestQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }

            var jsonData = new
            {
                CategoryName = CategoryName,
                childEntity = data
            };

            return ToJsonResult(jsonData);

        }

        /// <summary>
        /// ��ȡδ���
        /// </summary>

        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetPurchaseList()
        {
            var data = rawmaterialpurchasebll.GetList("").ToList().FindAll(f => f.IsWarehousing != 1&&f.IsPurchase==1);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
       // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            rawmaterialpurchasebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");

        }
     

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
        /// <param name="strChildEntitys">�ӱ����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
            entity.IsSubmit = entity.IsSubmit.IsEmpty() ? 0 : entity.IsSubmit;
            entity.IsPassed = entity.IsPassed.IsEmpty() ? 0 : entity.IsPassed;
            entity.IsPurchase = entity.IsPurchase.IsEmpty() ? 0 : entity.IsPurchase;
            entity.IsWarehousing = entity.IsWarehousing.IsEmpty() ? 0 : entity.IsWarehousing;
            entity.CreateMan = entity.CreateMan.IsEmpty() ? "���Թ���Ա" : entity.CreateMan;
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            foreach (var item in childEntitys)
            {
                var RawMaterialorderInfo = rawmaterialorderinfobll.GetEntity(item.RawMaterialOrderInfoId);
                item.RawMaterialAnalysisId = RawMaterialorderInfo.RawMaterialAnalysisId;
            }
           
            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �����Ѳɹ�
        /// </summary>  ��ȡ�����������ͨ���Ĳ���  IsPassed=1��ʾͨ��
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
        /// <param name="strChildEntitys">�ӱ����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult PurchaseSaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            //Session
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
            entity.IsPurchase = 1;
            entity.IsPassed = 1;
            entity.IsSubmit = 1;
            entity.IsWarehousing = entity.IsWarehousing.IsEmpty() ? 0 : entity.IsWarehousing;
            //entity.IsWarehousing = 1;
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ύ���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ύ��1�ύ</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
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
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ��˴���
        /// </summary>
        /// <param name="keyValue">Ҫ��˵����ݵ�����Щ</param>
        /// <param name="entity"></param>
        /// <param name="type">1ͨ����2ʧ��</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReviewOperation(string keyValue, RawMaterialPurchaseEntity entity, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
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
                        model.ReviewMan = "���Թ���Ա";
                        model.Description = entity.Description;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ύ�ɹ�
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ɹ���1�Ѳɹ�</param>
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
                    //���Ѳɹ��ļ�����Ͽ�
                    for (var i = 0; i < ids.Length; i++)
                    {
                        try
                        {
                            var model = rawmaterialpurchasebll.GetEntity(ids[i]);
                            //��ȡ���ɹ�����
                            if (model.RawMaterialPurchaseId != null)
                            {
                                //����ȥ��������ȡ��ÿһ��
                                var listInfo = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == model.RawMaterialPurchaseId && model.IsPurchase == 1);
                                //����������
                                for (var j = 0; j < listInfo.Count; j++)
                                {
                                    var OrderId = rawmaterialorderinfobll.GetEntity(listInfo[i].RawMaterialOrderInfoId).OrderId;
                                    var Category = rawmaterialorderbll.GetEntity(OrderId).Category;
                                    var RawMaterialAnalysisId = listInfo[j].RawMaterialAnalysisId;
                                    var rawmaterialanalysis = rawmaterialanalysisbll.GetList(f=>f.Category==Category&& f.Id== RawMaterialAnalysisId).SingleOrDefault();//ͨ������IDȡ�÷�������
                                    rawmaterialanalysis.PurchasedQuantity = rawmaterialanalysis.PurchasedQuantity.ToDecimal() + listInfo[j].PurchaseQuantity;
                                    rawmaterialanalysisbll.SaveForm(listInfo[j].RawMaterialAnalysisId, rawmaterialanalysis);
                                }
                            }
                        }
                        catch (System.Exception e)
                        {
                            throw (e);
                        }
                    }

                }
            }
            return Success("�����ɹ���");
        }
        #endregion
    }
}