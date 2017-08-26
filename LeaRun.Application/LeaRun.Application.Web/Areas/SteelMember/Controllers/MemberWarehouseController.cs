using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Code;
using System;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Linq;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-28 11:34
    /// �� �����������
    /// </summary>
    public class MemberWarehouseController : MvcControllerBase
    {
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private MemberWarehouseBLL memberwarehousebll = new MemberWarehouseBLL();
        private MemberWarehouseRecordingBLL memberwarehouserecordingbll = new MemberWarehouseRecordingBLL();
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll = new MemberProductionOrderInfoBLL();
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// ����б�
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryInfo()
        {
            return View();
        }
        /// <summary>
        /// ��ⱨ��
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryDetail()
        {
            return View();
        }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        public ActionResult Collar()
        {
            return View();
        }
        /// <summary>
        /// ���ⱨ��
        /// </summary>
        /// <returns></returns>
        public ActionResult OutInventoryDetail()
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
            var datatabel = new List<MemberWarehouseModel>();
            var data = memberwarehousebll.GetPageList(pagination, queryJson);
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var MemberLibrar = memberlibrarybll.GetEntity(item.MemberId);

                    var MemberWarehouse = new MemberWarehouseModel()
                    {
                        MemberNumbering = MemberLibrar.MemberNumbering,
                        MemberName = MemberLibrar.MemberName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrar.Category).ItemName,
                        MemberUnit = dataitemdetailbll.GetEntity(MemberLibrar.UnitId).ItemName,
                        InStock = item.InStock,
                        Librarian = item.Librarian,
                        UpdateTime = item.UpdateTime,
                        Description = MemberLibrar.Description
                    };
                    datatabel.Add(MemberWarehouse);
                }
            }
           
            var queryParam = queryJson.ToJObject();
            if (!queryParam["Category"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f=>f.MemberNumbering.Contains(keyword)&& f.Category== Category);
            }
            if (!queryParam["Category"].IsEmpty() && queryParam["keyword"].IsEmpty())
            {
                //string keyword = queryParam["keyword"].ToString();
                string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f =>f.Category == Category);
            }
            if (queryParam["Category"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f => f.MemberNumbering.Contains(keyword));
            }

            var jsonData = new
            {
                rows = datatabel.OrderBy(O => O.MemberNumbering),
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
            var data = memberwarehousebll.GetList(queryJson);
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
            var data = memberwarehousebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡ��û���� ProductionStatusֵΪ0ʱ��û�����ģ�ֵΪ1������������ 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNoIswarehousing(Pagination pagination)
        {

            var watch = CommonHelper.TimerStart();
            var ProductionStatus = 2;//������ɵ�
            var data = memberproductionorderbll.GetPageListByProductionStatus(pagination, ProductionStatus);
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
        /// <param name="Entity"></param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue, MemberProductionOrderInfoEntity Entity)
        {
            List<MemberWarehouseModel> MemberWarehouseModelList = new List<MemberWarehouseModel>();
            var data = memberproductionorderinfobll.GetList(f => f.OrderId == keyValue);
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    MemberWarehouseModel MemberWarehouse = new MemberWarehouseModel();
                    var data1 = memberlibrarybll.GetList(null).Find(f => f.MemberId == item.MemberId);
                    MemberWarehouse.MemberId = item.MemberId;
                    MemberWarehouse.ProductionQuantity = Convert.ToInt32(item.ProductionQuantity);
                    MemberWarehouse.Category = data1.Category;
                    MemberWarehouse.MemberName = data1.MemberName;
                    MemberWarehouse.MemberNumbering = data1.MemberNumbering;
                    //MemberWarehouse.MemberUnit = data1.Unit.ItemName;
                    MemberWarehouseModelList.Add(MemberWarehouse);
                }
            }
            return ToJsonResult(MemberWarehouseModelList);
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
            memberwarehousebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, MemberWarehouseEntity entity)
        {
            memberwarehousebll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public ActionResult Inventory(string OrderId)
        {

            string[] OrderIds = OrderId.Split(',');
            try
            {
                if (OrderIds.Length > 0)
                {
                    for (var i = 0; i < OrderIds.Length; i++)
                    {
                        //��ȡ�������µ����й���
                        var _OrderId = OrderIds[i];
                        var data = memberproductionorderinfobll.GetList(f => f.OrderId == _OrderId);
                        for (int i0 = 0; i0 < data.Count; i0++)
                        {
                            var MemberId = data[i].MemberId;
                            var memberinfo = memberlibrarybll.GetList(null).Find(f => f.MemberId == MemberId);
                            var orderinfo = memberproductionorderbll.GetList(null).Find(f => f.OrderId == _OrderId);

                            //�ȼӵ���������
                            MemberWarehouseRecordingEntity warehouseRecording = new MemberWarehouseRecordingEntity();
                            string keyValue1 = null;
                            warehouseRecording.Librarian = OperatorProvider.Provider.Current().UserName;
                            warehouseRecording.MemberId = memberinfo.MemberId;
                            warehouseRecording.UpdateTime = System.DateTime.Now;
                            warehouseRecording.ToReportPeople = orderinfo.CreateMan;
                            warehouseRecording.Receiver = "1111";
                            warehouseRecording.ReceiverTel = "11111111111111";
                            warehouseRecording.Class = "���";
                            memberwarehouserecordingbll.SaveForm(keyValue1, warehouseRecording);

                            //���Ŀ����
                            MemberWarehouseEntity MemberWarehouse = new MemberWarehouseEntity();
                            var MemberWarehouses = memberwarehousebll.GetList(null).Find(f => f.MemberId == MemberId);
                            if (MemberWarehouses != null)
                            {
                                MemberWarehouse.MemberWarehouseId = MemberWarehouses.MemberWarehouseId;
                                MemberWarehouse.InStock = Convert.ToInt32(MemberWarehouses.InStock) + data[i0].ProductionQuantity;//�����++                                                                                              //inventorymodel.
                                memberwarehousebll.SaveForm(MemberWarehouse.MemberWarehouseId, MemberWarehouse);
                            }

                            //�޸��������������״̬

                            var MembeOrder = memberproductionorderbll.GetEntity(_OrderId);
                            if (MembeOrder != null)
                            {
                                MembeOrder.OrderWarehousingStatus = 1;
                                memberproductionorderbll.SaveForm(_OrderId, MembeOrder);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Success("���ɹ���");
            //return View();

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="collarinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCollarinfo(string collarinfo)
        {
            var collarmodel = collarinfo.ToObject<MemberWarehouseRecordingEntity>();
            collarmodel.UpdateTime = System.DateTime.Now;
            try
            {
                if (collarmodel.InStock > 0)
                {
                    //�ڿ�����м������������
                    MemberWarehouseEntity MemberWarehouse = new MemberWarehouseEntity();
                    var MemberWarehouses = memberwarehousebll.GetList(null).Find(f => f.MemberWarehouseId == collarmodel.MemberWarehouseId);
                    if (MemberWarehouses != null)
                    {
                        MemberWarehouse.MemberWarehouseId = MemberWarehouses.MemberWarehouseId;
                        MemberWarehouse.InStock = Convert.ToInt32(MemberWarehouse.InStock) - collarmodel.InStock;//�����++
                        memberwarehousebll.SaveForm(MemberWarehouse.MemberWarehouseId, MemberWarehouse);
                    }

                    //��ӵ��������  
                    string keyValue = "";
                    var MemberLibrary = memberlibrarybll.GetList(null).Find(f => f.MemberId == MemberWarehouses.MemberId);
                    collarmodel.Class = "����";
                    collarmodel.Librarian = OperatorProvider.Provider.Current().UserName;
                    collarmodel.UpdateTime = DateTime.Now;
                    memberwarehouserecordingbll.SaveForm(keyValue, collarmodel);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return Success("����ɹ�");

        }
        #endregion
    }
}
