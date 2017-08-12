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

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-08-07 17:28
    /// �� �������϶���
    /// </summary>
    public class RawMaterialOrderController : MvcControllerBase
    {
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private RawMaterialOrderBLL rawmaterialorderbll = new RawMaterialOrderBLL();
        private RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialOrderInfoBLL rawmaterialorderinfobll = new RawMaterialOrderInfoBLL();
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "CLDD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                //ViewData["CreateMan"] = OperatorProvider.Provider.Current().UserName;
            }
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
            var childData = rawmaterialorderinfobll.GetList(keyValue);
            var jsonData = new
            {
                entity = data,
                childEntity = childData
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
            var data1 = rawmaterialanalysisbll.GetPageList1(f => f.Category == category && f.IsPassed==1,pagination).ToList();//.OrderByDescending(o => o.MemberNumbering)

            foreach (var item in data1)
            {
                var RawMaterial = rawmateriallibrarybll.GetList(f => f.RawMaterialId == item.RawMaterialId).SingleOrDefault();

                RawMaterialLibraryModel RawMaterialLibrary = new RawMaterialLibraryModel();

                RawMaterialLibrary.RawMaterialId = RawMaterial.RawMaterialId;
                RawMaterialLibrary.RawMaterialModel = RawMaterial.RawMaterialModel;
                RawMaterialLibrary.RawMaterialName = RawMaterial.Category;
                RawMaterialLibrary.UnitName = RawMaterial.Unit;
                RawMaterialLibrary.Qty = item.RawMaterialDosage.ToString();
                data.Add(RawMaterialLibrary);
            }
            return ToJsonResult(data);
        }

        /// <summary>
        /// ���ƶ���������������������
        /// </summary>
        /// <returns></returns>
        public ContentResult AddMemberNumber(string KeyValue, string category)
        {
            var MemberDemand = rawmaterialanalysisbll.GetList(s => s.Category == category && s.RawMaterialId == KeyValue).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;
            var Order = rawmaterialorderbll.GetList(null).ToList().FindAll(f => f.Category == category);
            foreach (var item in Order)
            {
                var MemberOrder = rawmaterialorderinfobll.GetList(null).ToList().Find(f => f.OrderId == item.OrderId && f.RawMaterialId == KeyValue);
                if (MemberOrder != null)
                {
                    Number += Convert.ToInt32(MemberOrder.ProductionQuantity);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.RawMaterialDosage) - Number;

            return Content(MemberDemandNumber.ToString());
        }

        /// <summary>
        /// ���ƶ�������������(�༭)
        /// </summary>
        /// <returns></returns>
        public ContentResult EditMemberNumber(string KeyValue, string MemberId)
        {

            var OrderList = new List<RawMaterialOrderEntity>();
            var Order = rawmaterialorderbll.GetList(KeyValue).SingleOrDefault();
            OrderList = rawmaterialorderbll.GetList(Order.Category).ToList();

            var MemberDemand = rawmaterialanalysisbll.GetList(f => f.Category == Order.Category&&f.RawMaterialId== MemberId).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;

            foreach (var item in OrderList)
            {
                var OrderMember = rawmaterialorderinfobll.GetList(null).ToList().Find(f => f.OrderId == item.OrderId && f.RawMaterialId == MemberId);
                if (OrderMember != null)
                {
                    Number += Convert.ToInt32(OrderMember.ProductionQuantity);
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
                            RawMaterialLibraryModel projectdemand = new RawMaterialLibraryModel();
                            projectdemand.RawMaterialId = rawmaterial.RawMaterialId;
                            projectdemand.RawMaterialModel = rawmaterial.RawMaterialModel;
                            projectdemand.RawMaterialName = rawmaterial.Category;
                            projectdemand.UnitName = rawmaterial.Unit;
                            projectdemand.Qty = a.RawMaterialDosage.ToString();
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
            rawmaterialorderbll.RemoveForm(keyValue);
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
            if (keyValue == "" || keyValue == null)
            {
                entity.IsPassed = entity.IsSubmit = 0;
            }
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
                List<RawMaterialOrderEntity> list = new List<RawMaterialOrderEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialorderbll.GetEntity(item.Trim());
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
                    rawmaterialorderbll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }

        #endregion
    }
}
