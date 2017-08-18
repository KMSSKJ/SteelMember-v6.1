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
using LeaRun.Application.Busines.SystemManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-11 10:12
    /// �� ����������������
    /// </summary>
    public class MemberProductionOrderController : MvcControllerBase
    {
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
        private MemberDemandBLL memberdemandbll = new MemberDemandBLL();
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();

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
        /// ��������
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderForm()
        {
            return View();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "GJDD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                //ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;
            }
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
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="IsReceive"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, int IsReceive, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = memberproductionorderbll.GetPageList(pagination, IsReceive, queryJson);
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = memberproductionorderbll.GetEntity(keyValue);
            var childData = memberproductionorderbll.GetDetails(keyValue).ToList();
            var MemberList = new List<MemberDemandModel>();
            for (int i = 0; i < childData.Count(); i++)
            {
                var MemberLibrary = memberlibrarybll.GetEntity(childData[i].MemberId);
                var Member = new MemberDemandModel()
                {
                    InfoId= childData[i].InfoId,
                    MemberId = childData[i].MemberId,
                    MemberNumber = childData[i].ProductionQuantity,
                    ProductionedQuantity=Convert.ToInt32(childData[i].ProductionQuantity),
                    SelfDetectNumber = childData[i].SelfDetectNumber,
                    SelfDetectRemarks = childData[i].SelfDetectRemarks,
                    QualityInspectionNumber = childData[i].QualityInspectionNumber,
                    QualityInspectionRemarks= childData[i].QualityInspectionRemarks,
                    MemberName = MemberLibrary.MemberName,
                    MemberNumbering = MemberLibrary.MemberNumbering,
                    MemberUnit = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
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
        /// ��ȡ�ӱ���ϸ��Ϣ 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = memberproductionorderbll.GetDetails(keyValue).ToList();
            var MemberList = new List<MemberDemandModel>();
            for (int i = 0; i < data.Count(); i++)
            {
                var MemberLibrary = memberlibrarybll.GetEntity(data[i].MemberId);
                var Member = new MemberDemandModel()
                {
                    MemberId = data[i].MemberId,
                    MemberNumber = data[i].ProductionQuantity,
                    ProductionedQuantity = Convert.ToInt32(data[i].ProductionedQuantity),
                    SelfDetectNumber = data[i].SelfDetectNumber,
                    QualityInspectionNumber = data[i].QualityInspectionNumber,
                    MemberName = MemberLibrary.MemberName,
                    MemberNumbering = MemberLibrary.MemberNumbering,
                    MemberUnit = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
                };
                MemberList.Add(Member);
            }
            return ToJsonResult(MemberList);
        }
        /// <summary>
        /// �������ͨ���Ĺ�������
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GridListJsonDemand(Pagination pagination, string category)
        {
            var watch = CommonHelper.TimerStart();
            var data = new List<MemberDemandModel>();
            var memberdemand = memberdemandbll.GetPageList1(pagination, f => f.SubProjectId == category && f.IsReview == 1).ToList();//.OrderByDescending(o => o.MemberNumbering)

            foreach (var item in memberdemand)
            {
                MemberDemandModel MemberDemand = new MemberDemandModel();
                var member = memberlibrarybll.GetList(null).Find(f => f.MemberId == item.MemberId);
                MemberDemand.MemberId = member.MemberId;
                MemberDemand.Category = member.Category;
                MemberDemand.MemberNumbering = member.MemberNumbering;
                MemberDemand.MemberName = member.MemberName;
                MemberDemand.MemberUnit = dataitemdetailbll.GetEntity(member.UnitId).ItemName;
                MemberDemand.MemberNumber = item.MemberNumber;
                data.Add(MemberDemand);
            }
            var jsonData = new
            {
                rows = data.OrderBy(O => O.MemberNumbering),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// ���ƶ���������������������
        /// </summary>
        /// <returns></returns>
        public ContentResult AddMemberNumber(string KeyValue, string category)
        {
            var MemberDemand = memberdemandbll.GetList(null).Where(s => s.SubProjectId == category && s.MemberId == KeyValue).SingleOrDefault();
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
        /// ���ƶ�������������(�༭)
        /// </summary>
        /// <returns></returns>
        public ContentResult EditMemberNumber(string KeyValue, string MemberId)
        {

            var OrderList = new List<MemberProductionOrderEntity>();
            var Order = memberproductionorderbll.GetList(KeyValue).SingleOrDefault();
            OrderList = memberproductionorderbll.GetList(Order.Category).ToList();

            var MemberDemand = memberdemandbll.GetList(MemberId).Where(f => f.SubProjectId == Order.Category).SingleOrDefault();
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
        /// ������Ӻ�Ĺ���
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult ListMember(string KeyValue, string category)
        {
            var listmember = new List<MemberDemandModel>();
            if (KeyValue != null)
            {
                string[] array = KeyValue.Split(',');
                if (array.Count() > 0)
                {
                    if (array != null)
                        foreach (var item in array)
                        {
                            var a = memberdemandbll.GetList(null).ToList().Find(f => f.SubProjectId == category && f.MemberId == item);
                            var member = memberlibrarybll.GetList(null).Find(f => f.MemberId == a.MemberId);
                            MemberDemandModel projectdemand = new MemberDemandModel()
                            {
                                MemberId = a.MemberId,
                                MemberName = member.MemberName,
                                UnitPrice = member.UnitPrice,
                                MemberUnit = dataitemdetailbll.GetEntity(member.UnitId).ItemName,
                                MemberNumbering = member.MemberNumbering,
                                MemberNumber = a.MemberNumber,
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
        public ActionResult RemoveForm(string keyValue)
        {
            memberproductionorderbll.RemoveForm(keyValue);
            var meminfo = memberproductionorderinfobll.GetList(f => f.OrderId == keyValue);
            if (meminfo.Count() > 0)
            {
                foreach (var item in meminfo)
                {
                    memberproductionorderinfobll.RemoveForm(item.InfoId);
                }
            }
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
            var entity = strEntity.ToObject<MemberProductionOrderEntity>();
            if (keyValue == "" || keyValue == null)
            {
                entity.IsPassed = 0;
                entity.IsSubmit = 0;
                entity.ProductionStatus = 0;
                entity.IsReceive = 0;
                entity.IsPackage = 0;
                entity.OrderWarehousingStatus = 0;
                entity.ProductionStatus = 0;
                entity.QualityInspectionStatus = 0;
                entity.SelfDetectStatus = 0;
                entity.IsReceiveRawMaterial = 0;
            }
            List<MemberProductionOrderInfoEntity> childEntitys = strChildEntitys.ToList<MemberProductionOrderInfoEntity>();
            memberproductionorderbll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ύ���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ύ��1�ύ</param>
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
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ��˴���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ</param>
        /// <param name="type">1ͨ����2ʧ��</param>
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
                        model.ReviewMan = OperatorProvider.Provider.Current().UserName;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    memberproductionorderbll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }

        #endregion
    }
}
