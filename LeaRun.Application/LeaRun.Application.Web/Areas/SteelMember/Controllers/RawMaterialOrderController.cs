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
                ViewBag.OrderNumbering = "CLSQD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
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
            var data = rawmaterialorderbll.GetPageList(pagination, queryJson);
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
            var RawMaterialAnalysis = rawmaterialanalysisbll.GetPageList1(f => f.Category == category && f.IsPassed == 1, pagination).ToList();//.OrderByDescending(o => o.MemberNumbering)
            var RawMaterialOrder = rawmaterialorderbll.GetList(f => f.Category == category);
            foreach (var item in RawMaterialAnalysis)
            {
                decimal Number = 0;
                foreach (var item1 in RawMaterialOrder)
                {
                    var RawMaterialOrderinfo = rawmaterialorderinfobll.GetList(f => f.RawMaterialId == item.RawMaterialId && f.OrderId == item1.OrderId).SingleOrDefault();
                    if (RawMaterialOrderinfo == null)
                    {
                        var a = 0;
                        Number += a;
                    }
                    else if (RawMaterialOrderinfo.RawMaterialId == item.RawMaterialId)
                    {
                        Number += RawMaterialOrderinfo.Quantity.ToDecimal();
                    }
                }

                var RawMaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == item.RawMaterialId).SingleOrDefault();
                RawMaterialLibraryModel RawMaterialLibrary = new RawMaterialLibraryModel()
                {
                    RawMaterialId = RawMaterial.RawMaterialId,
                    Category = dataitemdetailbll.GetEntity(RawMaterial.Category).ItemName,
                    CategoryId = RawMaterial.Category,
                    RawMaterialAnalysisId = item.Id,
                    RawMaterialModel = RawMaterial.RawMaterialModel,
                    RawMaterialName = RawMaterial.RawMaterialName,
                    UnitId = dataitemdetailbll.GetEntity(RawMaterial.Unit).ItemName,
                    Qty = item.RawMaterialDosage+item.ChangeQuantity.ToDecimal() - Number,//��������                                      //PurchasedQuantity = item.PurchasedQuantity
                };
                if (RawMaterialLibrary.Qty > 0)
                {
                    data.Add(RawMaterialLibrary);//������������0�ͼ���
                }
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
        
#pragma warning disable CS1572 // XML ע�����С�category���� param ��ǣ�����û�и����ƵĲ���
#pragma warning disable CS1572 // XML ע�����С�KeyValue���� param ��ǣ�����û�и����ƵĲ���
/// <summary>
        /// ������Ӻ�Ĺ���
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        //public ActionResult ListMember(string KeyValue, string category)
        //{
        //    var listmember = new List<RawMaterialLibraryModel>();
        //    if (KeyValue != null)
        //    {
        //        string[] array = KeyValue.Split(',');
        //        if (array.Count() > 0)
        //        {
        //            if (array != null)
        //                foreach (var item in array)
        //                {
        //                    var a = rawmaterialanalysisbll.GetList(f => f.Category == category && f.RawMaterialId == item).SingleOrDefault();
        //                    var rawmaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == a.RawMaterialId).SingleOrDefault();
        //                    var rawmaterialInventory = rawmaterialinventorybll.GetList(null).ToList().Find(f => f.RawMaterialId == a.RawMaterialId);
        //                    RawMaterialLibraryModel projectdemand = new RawMaterialLibraryModel()
        //                    {
        //                        RawMaterialId = rawmaterial.RawMaterialId,
        //                        RawMaterialAnalysisId = a.Id,
        //                        RawMaterialModel = rawmaterial.RawMaterialModel,
        //                        RawMaterialName = rawmaterial.RawMaterialName,
        //                        UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
        //                        Qty = a.RawMaterialDosage,
        //                        InventoryQty = Convert.ToDecimal(rawmaterialInventory.Quantity),
        //                        InventoryId = rawmaterialInventory.InventoryId,
        //                    };
        //                    listmember.Add(projectdemand);
        //                }
        //        }
        //    }
        //    else
        //    {
        //        listmember = null;
        //    }
        //    return Json(listmember);
        //}

#pragma warning disable CS1573 // ������RawMaterialId���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning disable CS1573 // ������RawMaterialName���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning disable CS1573 // ������RawMaterialModel���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning disable CS1573 // ������RawMaterialAnalysisId���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning disable CS1573 // ������UnitId���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning disable CS1573 // ������Qty���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning disable CS1573 // ������RawMaterialCategory���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
        public ActionResult ListMember(string RawMaterialAnalysisId, string RawMaterialId, string RawMaterialCategory, string RawMaterialName, string RawMaterialModel, string UnitId, string Qty)
#pragma warning restore CS1573 // ������RawMaterialCategory���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning restore CS1573 // ������Qty���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning restore CS1573 // ������UnitId���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning restore CS1573 // ������RawMaterialAnalysisId���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning restore CS1572 // XML ע�����С�KeyValue���� param ��ǣ�����û�и����ƵĲ���
#pragma warning restore CS1573 // ������RawMaterialModel���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning restore CS1572 // XML ע�����С�category���� param ��ǣ�����û�и����ƵĲ���
#pragma warning restore CS1573 // ������RawMaterialName���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
#pragma warning restore CS1573 // ������RawMaterialId���ڡ�RawMaterialOrderController.ListMember(string, string, string, string, string, string, string)���� XML ע����û��ƥ��� param ���(������������)
        {
            // var inventory = 0; //�����
            var listmember = new List<Text>();
            if (RawMaterialId != null && RawMaterialId != "")
            {
                string[] arrayRawMaterialAnalysisId = RawMaterialAnalysisId.Split(',');
                string[] arrayRawMaterialId = RawMaterialId.Split(',');
                string[] arrayRawMaterialCategory = RawMaterialCategory.Split(',');
                string[] arrayRawMaterialName = RawMaterialName.Split(',');
                string[] arrayRawMaterialModel = RawMaterialModel.Split(',');
                string[] arrayUnitId = UnitId.Split(',');
                string[] arrayRawMaterialQty = Qty.Split(',');
               
                if (arrayRawMaterialId.Count() > 0)
                {
                    if (arrayRawMaterialAnalysisId != null)
                        for (int i = 0; i < arrayRawMaterialAnalysisId.Length; i++)
                        {
                            // var sb = arrayRawMaterialId[i];
                            //var rawmaterialinventory = rawmaterialinventorybll.GetEntityByRawMaterialId(arrayRawMaterialId[i].ToString());
                            //var inventory = rawmaterialinventory.Quantity.ToDecimal();//��������
                            Text projectdemand = new Text()
                            {
                                RawMaterialAnalysisId= arrayRawMaterialAnalysisId[i],
                                RawMaterialId= arrayRawMaterialId[i],
                                RawMaterialCategory= arrayRawMaterialCategory[i],
                                RawMaterialName= arrayRawMaterialName[i],
                                RawMaterialModel= arrayRawMaterialModel[i],
                                UnitId= arrayUnitId[i],
                                Qty= arrayRawMaterialQty[i].ToDecimal(),
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
                        if (number == 0)
                        {
                            rawmaterialorderinfobll.RemoveForm(item.InfoId);
                        }
                        else
                        {
                            return Error("�����д��ڹ�������");
                        }
                    }
                }
                if (number == 0)
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
            entity.ReviewMan1 = entity.ReviewMan2 = entity.ReviewMan3 = entity.ReviewMan4 = entity.ReviewMan5 = "0";
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
        /// <param name="operat"></param>
        /// <param name="type">1ͨ����2ʧ��</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReviewOperation(string keyValue, RawMaterialOrderEntity entity,int operat, int type)
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
                        if (type == 1)
                        {
                            if (operat == 1)
                            {
                                model.ReviewMan1 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "�����������ͨ��";
                            }
                            else if (operat == 2)
                            {
                                model.ReviewMan2 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "���̲������ͨ��";
                            }
                            else if (operat == 3)
                            {
                                model.ReviewMan3 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "���ʲ������ͨ��";
                            }
                            else if (operat == 4)
                            {
                                model.ReviewMan4 = OperatorProvider.Provider.Current().UserName;
                                model.ReviewDescription = "���ܹ����ͨ��";
                            }
                            else
                            {
                                model.ReviewMan5 = OperatorProvider.Provider.Current().UserName;
                                model.IsPassed = 1;
                                model.ReviewDescription = "�ܹ����ͨ��";
                            }
                        }
                        else
                        {
                            if (operat == 1)
                            {
                                model.ReviewMan1 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "�����������ʧ��";
                            }
                            else if (operat == 2)
                            {
                                model.ReviewMan2 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "���̲������ʧ��";
                            }
                            else if (operat == 3)
                            {
                                model.ReviewMan3 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "���ʲ������ʧ��";
                            }
                            else if(operat == 4)
                            {
                                model.ReviewMan4 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "���ܹ����ʧ��";
                            }
                            else
                            {
                                model.ReviewMan5 = "2";
                                model.IsPassed = 2;
                                model.ReviewDescription = "�ܹ����ʧ��";
                            }
                        }
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

        /// <summary>
        ///�ջ�����
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ɹ���1�Ѳɹ�</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitIsReceived(string keyValues)
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
                        model.IsReceived = 1;
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
