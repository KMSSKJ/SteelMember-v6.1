using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using LeaRun.Application.Code;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Collections.Generic;
using System;
using LeaRun.Util.Extension;
using LeaRun.Application.Busines.SystemManage;
using System.Data.OleDb;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using LeaRun.Util.Offices;
using LeaRun.Application.Busines.BaseManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-08-07 17:28
    /// �� �������϶���
    /// </summary>
    public class RawMaterialOrderController : MvcControllerBase
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
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "CLSGD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;
                ViewData["CreateTime"] = DateTime.Now;
            }
            return View();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormDetail()
        {
            return View();
        }
        /// <summary>
        ///�����б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ItemList()
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = rawmaterialorderbll.GetPageList(pagination, queryJson).OrderBy(o => o.OrderNumbering);
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = rawmaterialorderbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmaterialorderbll.GetEntity(keyValue);
            var childData = rawmaterialorderinfobll.GetList(f => f.OrderId == keyValue).ToList();
            var MemberList = new List<MemberMaterialModel>();
            for (int i = 0; i < childData.Count(); i++)
            {
                var rawmaterial = rawmateriallibrarybll.GetEntity(childData[i].RawMaterialId);
                var Member = new MemberMaterialModel()
                {
                    InfoId = childData[i].InfoId,
                    RawMaterialId = childData[i].RawMaterialId,
                    RawMaterialAnalysisId = childData[i].RawMaterialAnalysisId,
                    RawMaterialNumber = childData[i].Quantity,
                    RawMaterialName = rawmaterial.RawMaterialName,
                    RawMaterialModel = rawmaterial.RawMaterialModel,
                    UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
                    Price = childData[i].Price,
                    RawMaterialManufacturer = childData[i].RawMaterialManufacturer,
                    RawMaterialSupplier = childData[i].RawMaterialSupplier,
                };
                MemberList.Add(Member);
            }
            var jsonData = new
            {
                entity = data,
                childEntity = MemberList
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
            var data = rawmaterialorderbll.GetEntity(keyValue);
            var MemberList = new List<MemberMaterialModel>();
            if (data != null)
            {
                data.Category = subprojectbll.GetEntity(data.Category).FullName;
                data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;

                var childData = rawmaterialorderinfobll.GetList(f => f.OrderId == keyValue).ToList();
                for (int i = 0; i < childData.Count(); i++)
                {
                    var rawmaterial = rawmateriallibrarybll.GetEntity(childData[i].RawMaterialId);
                    var Member = new MemberMaterialModel()
                    {
                        InfoId = childData[i].InfoId,
                        RawMaterialId = childData[i].RawMaterialId,
                        RawMaterialAnalysisId = childData[i].RawMaterialAnalysisId,
                        RawMaterialNumber = childData[i].Quantity,
                        RawMaterialName = rawmaterial.RawMaterialName,
                        RawMaterialModel = rawmaterial.RawMaterialModel,
                        UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
                        Price = childData[i].Price,
                        RawMaterialManufacturer = childData[i].RawMaterialManufacturer,
                        RawMaterialSupplier = childData[i].RawMaterialSupplier,
                    };
                    MemberList.Add(Member);
                }
            }



            var jsonData = new
            {
                entity = data,
                childEntity = MemberList
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        ///���ز���
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="category"></param>
        /// <returns>���ض���Json</returns>
        ///  [HttpGet]
        public ActionResult GridListJsonRawAnalysis(Pagination pagination, string category)
        {
            var data = new List<RawMaterialLibraryModel>();
            var data1 = rawmaterialanalysisbll.GetPageList1(f => f.Category == category && f.IsPassed == 1, pagination).ToList();//.OrderByDescending(o => o.MemberNumbering)

            foreach (var item in data1)
            {
                var RawMaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == item.RawMaterialId).SingleOrDefault();

                RawMaterialLibraryModel RawMaterialLibrary = new RawMaterialLibraryModel()
                {
                    RawMaterialId = RawMaterial.RawMaterialId,
                    RawMaterialAnalysisId = item.Id,
                    RawMaterialModel = RawMaterial.RawMaterialModel,
                    RawMaterialName = RawMaterial.RawMaterialName,
                    UnitId = dataitemdetailbll.GetEntity(RawMaterial.Unit).ItemName,
                    Qty = item.RawMaterialDosage,
                    PurchasedQuantity = item.PurchasedQuantity
                };
                data.Add(RawMaterialLibrary);
            }
            return ToJsonResult(data);
        }

        /// <summary>
        /// ���ƶ�����������������
        /// </summary>
        /// <returns></returns>
        public ContentResult AddRawMaterialNumber(string KeyValue, string category)
        {
            var MemberDemand = rawmaterialanalysisbll.GetList(s => s.Category == category && s.RawMaterialId == KeyValue).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;
            var Order = rawmaterialorderbll.GetList(f => f.Category == category);
            foreach (var item in Order)
            {
                var MemberOrder = rawmaterialorderinfobll.GetList(f => f.OrderId == item.OrderId && f.RawMaterialId == KeyValue).SingleOrDefault();
                if (MemberOrder != null)
                {
                    Number += Convert.ToInt32(MemberOrder.Quantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.RawMaterialDosage) - Number;

            return Content(MemberDemandNumber.ToString());
        }

        /// <summary>
        /// ���ƶ�������������(�༭)
        /// </summary>
        /// <returns></returns>
        public ContentResult EditRawMaterialNumber(string KeyValue, string MemberId)
        {

            var OrderList = new List<RawMaterialOrderEntity>();
            var Order = rawmaterialorderbll.GetList(KeyValue).SingleOrDefault();
            OrderList = rawmaterialorderbll.GetList(Order.Category).ToList();

            var MemberDemand = rawmaterialanalysisbll.GetList(f => f.Category == Order.Category && f.RawMaterialId == MemberId).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;

            foreach (var item in OrderList)
            {
                var OrderMember = rawmaterialorderinfobll.GetList(f => f.OrderId == item.OrderId && f.RawMaterialId == MemberId).SingleOrDefault();
                if (OrderMember != null)
                {
                    Number += Convert.ToInt32(OrderMember.Quantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.RawMaterialDosage) - Number;

            return Content(MemberDemandNumber.ToString());
        }
        /// <summary>
        /// ������Ӻ�Ĺ���
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult ListMember(string KeyValue, string category)
        {
            var listmember = new List<RawMaterialLibraryModel>();
            if (KeyValue != null)
            {
                string[] array = KeyValue.Split(',');
                if (array.Count() > 0)
                {
                    if (array != null)
                        foreach (var item in array)
                        {
                            var a = rawmaterialanalysisbll.GetList(f => f.Category == category && f.RawMaterialId == item).SingleOrDefault();
                            var rawmaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == a.RawMaterialId).SingleOrDefault();
                            var rawmaterialInventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == a.RawMaterialId);
                            RawMaterialLibraryModel projectdemand = new RawMaterialLibraryModel()
                            {
                                RawMaterialId = rawmaterial.RawMaterialId,
                                RawMaterialAnalysisId = a.Id,
                                RawMaterialModel = rawmaterial.RawMaterialModel,
                                RawMaterialName = rawmaterial.RawMaterialName,
                                UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
                                Qty = a.RawMaterialDosage,
                                InventoryQty = Convert.ToDecimal(rawmaterialInventory.Quantity),
                                InventoryId = rawmaterialInventory.InventoryId,
                            };
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
            string[] idsArr = keyValue.Split(',');
            int number = 0;
            foreach (var item1 in idsArr)
            {
                var meminfo = rawmaterialorderinfobll.GetList(f => f.OrderId == item1);
                if (meminfo.Count() > 0)
                {
                    foreach (var item in meminfo)
                    {
                        var rawmaterialPurchaseinfo = rawmaterialpurchasebll.GetInfoList(f => f.InfoId == item.InfoId);
                        number = rawmaterialPurchaseinfo.Count();
                        if (number==0)
                        {
                            rawmaterialorderinfobll.RemoveForm(item.InfoId);
                        }
                        else
                        {
                            return Error("�����д��ڹ�������");
                        }
                    }
                }
                if (number==0)
                {
                    rawmaterialorderbll.RemoveForm(keyValue);
                }
                else
                {
                    return Error("�����д��ڹ�������");
                }
                
            }
           
            return Success("ɾ���ɹ���");
        }
        ///// <summary>
        ///// ��������������޸ģ�
        ///// </summary>
        ///// <param name="keyValue">����ֵ</param>
        ///// <param name="entity">ʵ�����</param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AjaxOnly]
        //public ActionResult SaveForm(string keyValue, RawMaterialOrderEntity entity)
        //{
        //    rawmaterialorderbll.SaveForm(keyValue, entity);
        //    return Success("�����ɹ���");
        //}

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
            var entity = strEntity.ToObject<RawMaterialOrderEntity>();

            entity.IsPassed = entity.IsSubmit = 0;
            List<RawMaterialOrderInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialOrderInfoEntity>();
            rawmaterialorderbll.SaveForm(keyValue, entity, childEntitys);

            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ύ���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ύ��1�ύ</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
       // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SubmitReview(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialOrderEntity> list = new List<RawMaterialOrderEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmit = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialorderbll.UpdataList(list);
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
        public ActionResult ReviewOperation(string keyValue, RawMaterialOrderEntity entity, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialOrderEntity> list = new List<RawMaterialOrderEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsPassed = type;
                        model.ReviewTime = System.DateTime.Now;
                        model.ReviewMan = OperatorProvider.Provider.Current().UserName;
                        model.Description = entity.Description;
                        list.Add(model);
                    }
                    if (type == 1)
                    {
                        //���������������Ϣ
                        var childEntitys1 = new List<RawMterialCollarInfoEntity>();
                        var entity1 = new RawMterialCollarEntity()
                        {
                            Numbering = model.OrderNumbering,
                            CollarEngineering = model.Category,
                            CollarType = 0,
                            OrganizeId = model.OrganizeId,
                            DepartmentId = model.DepartmentId,
                            Date = model.CreateTime,
                            CreateMan = model.CreateMan,
                            ShippingAddress = model.ShippingAddress,
                            ContactPerson = model.ContactPerson,
                            ContactPersonTel = model.ContactPersonTel,
                        };
                        var ListEntity = rawmaterialorderinfobll.GetList(f => f.OrderId == item.Trim());
                        if (ListEntity.Count() > 0)
                        {
                            foreach (var item1 in ListEntity)
                            {
                                var Entity = new RawMterialCollarInfoEntity()
                                {
                                    Quantity = item1.Quantity,
                                    RawMaterialId = item1.RawMaterialId,
                                    RawMaterialAnalysisId = item1.RawMaterialAnalysisId,
                                };
                                childEntitys1.Add(Entity);
                                //�����������ѳɵ���
                                var RawMaterialAnalysisEntiity = rawmaterialanalysisbll.GetEntity(item1.RawMaterialAnalysisId);
                                RawMaterialAnalysisEntiity.ApplicationPurchasedQuantity = RawMaterialAnalysisEntiity.ApplicationPurchasedQuantity.ToDecimal() + item1.Quantity;
                                rawmaterialanalysisbll.SaveForm(item1.RawMaterialAnalysisId, RawMaterialAnalysisEntiity);
                                //
                            }
                        }
                        rawmterialcollarbll.SaveForm("", entity1, childEntitys1);
                        //end
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialorderbll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }

        #endregion

        /// <summary>
        /// ����������ϸ��Excelģ�嵼����
        /// </summary>
        /// <param name="keyValue">����Id</param>
        /// <returns></returns>
        //[ValidateInput(false)]
        public void OutExcel(string keyValue)

        {
            var data = rawmaterialorderbll.GetEntity(keyValue);
            var childData = rawmaterialorderinfobll.GetList(keyValue).ToList();

            var list = new List<TemplateMode>();
            //����������Ϣ
            list.Add(new TemplateMode() { row = 1, cell = 1, value = data.OrderNumbering });
            list.Add(new TemplateMode() { row = 1, cell = 3, value = data.CreateMan });
            list.Add(new TemplateMode() { row = 1, cell = 5, value = data.CreateTime.ToDate().ToString("yyyy-MM-dd") });
            list.Add(new TemplateMode() { row = 2, cell = 1, value = subprojectbll.GetEntity(data.Category).FullName });

            //������ϸ��Ϣ
            int rowIndex = 5;
            foreach (RawMaterialOrderInfoEntity item in childData)
            {
                var rawmaterial = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                list.Add(new TemplateMode() { row = rowIndex, cell = 0, value = rawmaterial.RawMaterialModel });
                list.Add(new TemplateMode() { row = rowIndex, cell = 1, value = rawmaterial.RawMaterialName });
                list.Add(new TemplateMode() { row = rowIndex, cell = 2, value = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName });
                list.Add(new TemplateMode() { row = rowIndex, cell = 3, value = item.Quantity.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 4, value = rawmaterial.Description });
                rowIndex++;
            }
            ////������ϸ�ϼ�
            //list.Add(new TemplateMode() { row = 16, cell = 5, value = orderEntry.Sum(t => t.Qty).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 6, value = orderEntry.Sum(t => t.Price).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 7, value = orderEntry.Sum(t => t.Amount).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 9, value = orderEntry.Sum(t => t.Taxprice).ToString() });
            //list.Add(new TemplateMode() { row = 16, cell = 10, value = orderEntry.Sum(t => t.Tax).ToString() });

            ExcelHelper.ExcelDownload(list, "���϶���ģ��.xlsx", "���϶���-" + data.OrderNumbering + ".xlsx");
        }
    }
}
