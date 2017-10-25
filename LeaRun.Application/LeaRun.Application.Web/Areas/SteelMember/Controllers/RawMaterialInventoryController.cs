using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using System.Linq;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-19 10:03
    /// �� �������Ͽ��
    /// </summary>
    public class RawMaterialInventoryController : MvcControllerBase
    {
        private RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private RawMaterialPurchaseBLL rawmaterialpurchasebll = new RawMaterialPurchaseBLL();
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialWarehouseBLL rawmaterialwarehousebll = new RawMaterialWarehouseBLL();
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
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryInfo()
        {
            return View();
        }
        /// <summary>
        ///����ҳ��
        /// </summary>
        /// <returns></returns>
        public ActionResult CollarDetail()
        {
            return View();
        }
        /// <summary>
        /// ����ѡ���
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemList()
        {
            return View();
        }
        /// <summary>
        ///  ����
        /// </summary>
        /// <returns></returns>
        public ActionResult Collar()
        {
            //ViewBag.CollarNumbering = "CLLYD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
            //ViewBag.CreateMan = OperatorProvider.Provider.Current().UserName;
            return View();
        }
        /// <summary>
        /// �����ϸ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ActionResult IntoInventoryDetail()
        {
            return View();
        }
        /// <summary>
        /// ������ϸ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ActionResult OutInventoryDetail()
        {
            return View();
        }

        /// <summary>
        ///��������
        /// </summary>
        /// <returns></returns>
        public ActionResult QuantitySummary()
        {
            return View();
        }

        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ��û���� IswarehousingֵΪ0ʱ��û���ģ�ֵΪ1���Ѿ����� IsWarehousing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNoIswarehousing(Pagination pagination)
        {

            var watch = CommonHelper.TimerStart();
            var IsWarehousing = 0;
            var IsPurchase = 1;
            var data = rawmaterialpurchasebll.GetPageListByIsWarehousing(pagination, IsWarehousing).ToList();
            data = data.FindAll(f => f.IsPurchase == IsPurchase);
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
                    RawMaterialPurchaseModel rawMaterialPurchaseModelEntity = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModelEntity.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModelEntity.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModelEntity.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModelEntity.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModelEntity.UnitId = entityrawmateriallibrary.Unit;
                    rawMaterialPurchaseModelEntity.Description = entityrawmaterialanalysis.Description;
                    rawMaterialPurchaseModelEntity.RawMaterialName = entityrawmateriallibrary.Category;
                    rawMaterialPurchaseModelEntity.RawMaterialPurchaseModelPrice = item.Price;
                    list.Add(rawMaterialPurchaseModelEntity);
                }
            }
            return ToJsonResult(list);
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public ActionResult Inventory(RawMaterialInventoryEntity Entity, string PurchaseId)
        {

            //string[] RawMaterialPurchaseIds = RawMaterialPurchaseId.Split(',');
            //try
            //{
            //if (RawMaterialPurchaseIds.Length > 0)
            //{
            //    for (var i = 0; i < RawMaterialPurchaseIds.Length; i++)
            //    {
            //        ��ȡ�������µ�����������
            //        var data = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == keyValue);
            //        var keyValue = RawMaterialPurchaseIds[i];
            var purchaseinfo = rawmaterialpurchasebll.GetInfoList(p => p.RawMaterialPurchaseId == PurchaseId);
            var a = 0;
            foreach (var item in purchaseinfo)
            {
                var analysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                // var library = rawmateriallibrarybll.GetEntity(analysis.RawMaterialId);
                if (analysis.RawMaterialId == Entity.RawMaterialId)
                {
                    //�ȼӵ���������
                    RawMaterialWarehouseEntity warehouseModel = new RawMaterialWarehouseEntity();
                    string keyValue1 = null;
                    warehouseModel.RawMaterialId = Entity.RawMaterialId;
                    warehouseModel.WarehouseQuantity = Entity.Quantity.ToDecimal();
                    warehouseModel.Description = Entity.Description;
                    warehouseModel.WarehouseTime = System.DateTime.Now;
                    rawmaterialwarehousebll.SaveForm(keyValue1, warehouseModel);

                    //���Ŀ����
                    // RawMaterialInventoryModel RawMaterialInventoryModel = new RawMaterialInventoryModel();
                    var inventorymodel = rawmaterialinventorybll.GetEntityByRawMaterialId(Entity.RawMaterialId);
                    if (inventorymodel != null)
                    {
                        inventorymodel.Quantity = inventorymodel.Quantity.ToDecimal() + Entity.Quantity.ToDecimal();//�����++                                                                                              //inventorymodel.
                        inventorymodel.UnitPrice = Entity.UnitPrice;
                        inventorymodel.RawMaterialManufacturer = Entity.RawMaterialManufacturer;
                        inventorymodel.RawMaterialSupplier = Entity.RawMaterialSupplier;
                        rawmaterialinventorybll.SaveForm(inventorymodel.InventoryId, inventorymodel);
                    }

                    ////�޸Ĳɹ����������״̬
                    //var purchasemodel = rawmaterialpurchasebll.GetEntity(RawMaterialPurchaseIds[i]);
                    //if (purchasemodel != null)
                    //{
                    //    purchasemodel.IsWarehousing = 1;
                    //    rawmaterialpurchasebll.SavePurchaseForm(purchasemodel.RawMaterialPurchaseId, purchasemodel);
                    //}

                    //�޸������������
                    analysis.WarehousedQuantity = analysis.WarehousedQuantity.ToDecimal() + Entity.Quantity.ToDecimal();
                    rawmaterialanalysisbll.SaveForm(analysis.Id, analysis);
                    //
                    a++;
                }
            }
            var str = "";
            if (a > 0)
            {
                str = "���ɹ�";
                return Success(str);
            }
            else
            {
                str = "�òɹ������޴˲��ϣ����鵥������";//�򵽲��Ϸ����������Ӵ˲���
                return Error(str);
            }
        }
            /// <summary>
            /// ���ز���
            /// </summary>
            /// <param name="InventoryId"></param>
            /// <returns></returns>
            public ActionResult AddRawMaterial(string InventoryId)
            {
                RawMaterialInventoryModel rawMaterialInventoryModel = new RawMaterialInventoryModel();
                var modelinventory = rawmaterialinventorybll.GetEntity(InventoryId);
                var modellibrary = rawmateriallibrarybll.GetEntity(modelinventory.RawMaterialId);

                rawMaterialInventoryModel.Quantity = Convert.ToDecimal(modelinventory.Quantity);
                rawMaterialInventoryModel.RawMaterialModel = modellibrary.RawMaterialModel;
                rawMaterialInventoryModel.Unit = modellibrary.Unit;
                //rawMaterialInventoryModel.Category = modellibrary.Category;
                rawMaterialInventoryModel.Category = modellibrary.RawMaterialName;


                return ToJsonResult(rawMaterialInventoryModel); ;
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
            var data = rawmaterialinventorybll.GetPageList(pagination, queryJson);//.FindAll(f => f.Quantity > 0);
            //rawmateriallibrarybll.GetEntity(data)
            List<RawMaterialInventoryModel> list = new List<RawMaterialInventoryModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialInventoryModel rawMaterialInventoryModel = new RawMaterialInventoryModel();
                    var rawmateriallibrary = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    if (rawmateriallibrary != null)
                    {
                        rawMaterialInventoryModel.InventoryId = item.InventoryId;
                        rawMaterialInventoryModel.Quantity = Convert.ToDecimal(item.Quantity);
                        rawMaterialInventoryModel.RawMaterialId = item.RawMaterialId;
                        rawMaterialInventoryModel.Category = rawmateriallibrary.RawMaterialName;
                        rawMaterialInventoryModel.RawMaterialName = rawmateriallibrary.RawMaterialName;
                        rawMaterialInventoryModel.RawMaterialModel = rawmateriallibrary.RawMaterialModel;
                        rawMaterialInventoryModel.Unit = dataitemdetailbll.GetEntity(rawmateriallibrary.Unit).ItemName;

                        list.Add(rawMaterialInventoryModel);
                    }

                }
            }

            var queryParam = queryJson.ToJObject();
          
            if (!queryParam["RawMaterialName"].IsEmpty())
            {
                string RawMaterialName = queryParam["RawMaterialName"].ToString();
                list = list.FindAll(t => t.RawMaterialName.Contains(RawMaterialName));
            }
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString();
                list = list.FindAll(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }


            var jsonData = new
            {
                rows = list,
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
            var data = rawmaterialinventorybll.GetList(queryJson);
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
            var data = rawmaterialinventorybll.GetEntity(keyValue);
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
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            rawmaterialinventorybll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, RawMaterialInventoryEntity entity)
        {
            rawmaterialinventorybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
