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
using LeaRun.Application.Busines.BaseManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-28 11:34
    /// �� �����������
    /// </summary>
    public class MemberWarehouseController : MvcControllerBase
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
            ViewBag.CollarNumbering = "GJCKD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
            ViewBag.CreateMan = OperatorProvider.Provider.Current().UserName;
            ViewBag.Date = DateTime.Now;
            return View();
        }
        public ActionResult CollarDetail()
        {
            return View();
        }

        /// <summary>
        /// �����ڵ��б�
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemList()
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            if (queryJson==null)
            {
                queryJson = "{\"InStock\":\"0.00000001\"}";
            }
            else
            {
                queryJson = queryJson.Replace("}", ",\"InStock\":\"0.00000001\"}");
             }

            var watch = CommonHelper.TimerStart();
            var datatabel = new List<MemberWarehouseModel>();
            var data = memberwarehousebll.GetPageList(pagination, queryJson).ToList();//
           // data = data.FindAll(f => f.InStock > 0);
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var MemberLibrar = memberlibrarybll.GetEntity(item.MemberId);

                    var MemberWarehouse = new MemberWarehouseModel()
                    {
                        MemberWarehouseId = item.MemberWarehouseId,
                        MemberNumbering = MemberLibrar.MemberNumbering,
                        MemberName = MemberLibrar.MemberName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrar.Category).ItemName,
                        UnitId = dataitemdetailbll.GetEntity(MemberLibrar.UnitId).ItemName,
                        InStock = item.InStock,
                        Librarian = item.Librarian,
                        UpdateTime = item.UpdateTime,
                        Description = MemberLibrar.Description
                    };
                    datatabel.Add(MemberWarehouse);
                }
            }

            var queryParam = queryJson.ToJObject();

            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                //string Category = queryParam["Category"].ToString();
                datatabel = datatabel.FindAll(f => f.Category == Category);
            }
            if (!queryParam["MemberName"].IsEmpty())
            {
                var MemberName = queryParam["MemberName"].ToString();
                datatabel = datatabel.FindAll(t => t.MemberName.Contains(MemberName));
            }
            if (!queryParam["Numbering"].IsEmpty())
            {
                var Numbering = queryParam["Numbering"].ToString();
                datatabel = datatabel.FindAll(t => t.MemberNumbering.Contains(Numbering));
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

            var data1 = memberlibrarybll.GetEntity(data.MemberId);
            data1.Category = dataitemdetailbll.GetEntity(data1.Category).ItemName;
            return ToJsonResult(data1);
        }

        /// <summary>
        /// ��ȡ���ⵥ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberCollar(Pagination pagination, string queryJson)
        {

            var data = membercollarbll.GetPageList(pagination, queryJson).ToList().FindAll(f => f.CollarNumbering != null);
            if (data.Count() > 0)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    data[i].CollarEngineering = subprojectbll.GetEntity(data[i].CollarEngineering).FullName;
                    data[i].DepartmentId = organizebll.GetEntity(data[i].OrganizeId).FullName + "-" + departmentbll.GetEntity(data[i].DepartmentId).FullName;
                }

            }

            //
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["CollarEngineering"].IsEmpty())
            {
                var CollarEngineering = queryParam["CollarEngineering"].ToString();
                data = data.FindAll(t => t.CollarEngineering.Contains(CollarEngineering));
            }
            if (!queryParam["DepartmentId"].IsEmpty())
            {
                var DepartmentId = queryParam["DepartmentId"].ToString();
                data = data.FindAll(t => t.DepartmentId.Contains(DepartmentId));
            }
            if (!queryParam["ShippingAddress"].IsEmpty())
            {
                var ShippingAddress = queryParam["ShippingAddress"].ToString();
                data = data.FindAll(t => t.ShippingAddress.Contains(ShippingAddress));
            }
            //

            return ToJsonResult(data);
        }

        /// <summary>
        /// ���ⵥ����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberCollarInfo(string keyValue)
        {
            var childData = membercollarinfobll.GetList(f => f.CollarId == keyValue);
            var list = new List<MemberDemandModel>();
            foreach (var item in childData)
            {
                var rawmaterialinventory = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                var MemberLibrary = memberlibrarybll.GetEntity(rawmaterialinventory.MemberId);
                var rawmaterial = new MemberDemandModel()
                {
                    MemberWarehouseId = rawmaterialinventory.MemberWarehouseId,
                    MemberId = rawmaterialinventory.MemberId,
                    MemberNumbering = MemberLibrary.MemberNumbering,
                    MemberName = MemberLibrary.MemberName,
                    Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
                    MemberNumber = Convert.ToDecimal(item.CollarQuantity),
                    UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
                    Description = item.Description,
                };
                list.Add(rawmaterial);
            }
            return ToJsonResult(list);
        }

        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson1(string keyValue)
        {
            var data = membercollarbll.GetEntity(keyValue);
            if (data != null)
            {
                data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;
                data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;
            }

            var childData = membercollarinfobll.GetList(keyValue);
            var list = new List<MemberDemandModel>();
            foreach (var item in childData)
            {
                var rawmaterialinventory = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                var RawMaterialLibrary = memberlibrarybll.GetEntity(rawmaterialinventory.MemberId);
                var rawmaterial = new MemberDemandModel()
                {
                    MemberWarehouseId = rawmaterialinventory.MemberWarehouseId,
                    MemberId = rawmaterialinventory.MemberId,
                    MemberNumbering = RawMaterialLibrary.MemberNumbering,
                    MemberName = RawMaterialLibrary.MemberName,
                    Category = dataitemdetailbll.GetEntity(RawMaterialLibrary.Category).ItemName,
                    MemberNumber = Convert.ToDecimal(item.CollarQuantity),
                    UnitId = dataitemdetailbll.GetEntity(RawMaterialLibrary.UnitId).ItemName,
                    Description = item.Description,
                };
                list.Add(rawmaterial);
            }
            var jsonData = new
            {
                entity = data,
                childEntity = list
            };
            return ToJsonResult(jsonData);
        }



        /// <summary>
        /// ��ȡ��û���� ProductionStatusֵΪ0ʱ��û�����ģ�ֵΪ1������������ 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNoIswarehousing(Pagination pagination)
        {

            var watch = CommonHelper.TimerStart();
            //var ProductionStatus = 2;//������ɵ�
            var data = memberproductionorderbll.GetPageListByProductionStatus(pagination, f => f.ProductionStatus != 0);
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
            var data = memberproductionorderinfobll.GetList(f => f.QualifiedQuantity != f.WarehousedQuantity);//.FindAll(f => f.ProductionedQuantity != f.WarehousedQuantity)
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    MemberWarehouseModel MemberWarehouse = new MemberWarehouseModel();
                    var data1 = memberlibrarybll.GetEntity(f => f.MemberId == item.MemberId);
                    var order = memberproductionorderbll.GetList(f => f.OrderId == item.OrderId).SingleOrDefault();

                    MemberWarehouse.MemberId = item.MemberId;
                    MemberWarehouse.ProductionQuantity = Convert.ToInt32(item.ProductionQuantity);
                    MemberWarehouse.ProductionedQuantity = item.ProductionedQuantity;
                    MemberWarehouse.WarehousedQuantity = item.WarehousedQuantity;
                    MemberWarehouse.QualifiedQuantity = item.QualifiedQuantity;
                    MemberWarehouse.Category = dataitemdetailbll.GetEntity(data1.Category).ItemName;
                    MemberWarehouse.MemberName = data1.MemberName;
                    MemberWarehouse.MemberNumbering = data1.MemberNumbering;
                    MemberWarehouse.UnitId = dataitemdetailbll.GetEntity(data1.UnitId).ItemName;
                    MemberWarehouse.OrderId = order.OrderId;
                    MemberWarehouseModelList.Add(MemberWarehouse);
                }
            }
            return ToJsonResult(MemberWarehouseModelList);
        }

        /// <summary>
        /// ��ȡ������ϸ��Ϣ�����⣩
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpPost]
        public ActionResult ListMember(string keyValue)
        {
            var listmember = new List<MemberDemandModel>();
            if (keyValue != null)
            {
                string[] array = keyValue.Split(',');
                if (array.Count() > 0)
                {
                    if (array != null)
                        foreach (var item in array)
                        {
                            var a = memberwarehousebll.GetEntity(item);
                            var member = memberlibrarybll.GetEntity(a.MemberId);
                            MemberDemandModel projectdemand = new MemberDemandModel()
                            {
                                MemberWarehouseId = item,
                                MemberId = a.MemberId,
                                MemberNumbering = member.MemberNumbering,
                                MemberName = member.MemberName,
                                Category = dataitemdetailbll.GetEntity(member.Category).ItemName,
                                //UnitPrice = member.UnitPrice,
                                UnitId = dataitemdetailbll.GetEntity(member.UnitId).ItemName,
                                MemberNumber = a.InStock,
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

        /// <summary>
        /// ��ȡ�������� 
        /// </summary>
        /// <param name="CollarEntityJson"></param>
        /// <param name="CollarJson"></param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult QuantitySummaryInfo(string CollarEntityJson, string CollarJson)
        {
            var data = CollarEntityJson.ToObject<CollarEntityModel>();

            var list = new List<MemberDemandModel>();
            var CollarJsonList = CollarJson.ToList<CollarJsonMoadel>();
            foreach (var item1 in CollarJsonList)
            {
                var childData = membercollarinfobll.GetList(f => f.CollarId == item1.CollarId);
                foreach (var item in childData)
                {
                    var rawmaterialinventory = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    var memberlibrary = memberlibrarybll.GetEntity(rawmaterialinventory.MemberId);
                    var rawmaterial = new MemberDemandModel()
                    {
                        // MemberWarehouseId = rawmaterialinventory.MemberWarehouseId,
                        MemberName = memberlibrary.MemberName,
                        Category = dataitemdetailbll.GetEntity(memberlibrary.Category).ItemName,
                        MemberNumber = Convert.ToDecimal(item.CollarQuantity),
                        UnitId = dataitemdetailbll.GetEntity(memberlibrary.UnitId).ItemName,
                        Description = item.Description,
                        CreateTime = item1.Date.ToDate(),
                    };
                    list.Add(rawmaterial);
                }
            }
            var jsonData = new
            {
                entity = data,
                childEntity = list
            };
            return ToJsonResult(jsonData);

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
            memberwarehousebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveFormCollar(string keyValue)
        {
            membercollarbll.RemoveForm(keyValue);
            var data = membercollarinfobll.GetList(f => f.CollarId == keyValue);
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    membercollarinfobll.RemoveForm(item.InfoId);
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
        //public ActionResult SaveForm(string keyValue, MemberWarehouseEntity entity)
        //{
        //    memberwarehousebll.SaveForm(keyValue, entity);
        //    return Success("�����ɹ���");
        //}

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public ActionResult Inventory(string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            var OrderId = queryParam["OrderId"].ToString();
            var MemberId = queryParam["MemberId"].ToString();

            string[] OrderIds = OrderId.Split(',');
            string[] MemberIds = MemberId.Split(',');
            try
            {
                if (OrderIds.Length > 0)
                {
                    for (var i = 0; i < OrderIds.Length; i++)
                    {
                        for (var j = 0; j < MemberIds.Length; j++)
                        {
                            //��ȡ�������µ����й���
                            var _OrderId = OrderIds[i];
                            var _MemberId = MemberIds[i];
                            var MemberOrder = memberproductionorderbll.GetEntity(_OrderId);
                            var data = memberproductionorderinfobll.GetList(f => f.OrderId == _OrderId&&f.MemberId==_MemberId).SingleOrDefault();

                            ////�޸��������������״̬
                            //var MembeOrder = memberproductionorderbll.GetEntity(_OrderId);
                            //if (MembeOrder != null)
                            //{
                            //    MembeOrder.OrderWarehousingStatus = 1;
                            //    memberproductionorderbll.SaveForm(_OrderId, MembeOrder);
                            //}

                            //�޸����������й����������

                            data.WarehousedQuantity = data.WarehousedQuantity.ToDecimal() + data.QualifiedQuantity.ToDecimal();
                            memberproductionorderinfobll.SaveForm(data.InfoId, data);
                            //end

                            var memberinfo = memberlibrarybll.GetEntity(f => f.MemberId == _MemberId);
                            var orderinfo = memberproductionorderbll.GetList(f => f.OrderId == _OrderId);

                            //���Ŀ����
                            MemberWarehouseEntity MemberWarehouse = new MemberWarehouseEntity();
                            var MemberWarehouses = memberwarehousebll.GetEntity(f => f.MemberId == _MemberId);
                            if (MemberWarehouses != null)
                            {
                                MemberWarehouse.MemberWarehouseId = MemberWarehouses.MemberWarehouseId;
                                MemberWarehouse.Librarian = OperatorProvider.Provider.Current().UserName;
                                MemberWarehouse.InStock = MemberWarehouses.InStock.ToDecimal() + data.QualifiedQuantity.ToDecimal();//�����++                       
                                memberwarehousebll.SaveForm(MemberWarehouse.MemberWarehouseId, MemberWarehouse);
                            }//end

                            //�ӵ�������
                            MemberWarehouseRecordingEntity warehouseRecording = new MemberWarehouseRecordingEntity();
                            string keyValue1 = null;
                            warehouseRecording.Librarian = OperatorProvider.Provider.Current().UserName;
                            warehouseRecording.MemberId = memberinfo.MemberId;
                            warehouseRecording.UpdateTime = System.DateTime.Now;
                            warehouseRecording.SubProject = MemberOrder.Category;
                            warehouseRecording.InStock = data.QualifiedQuantity;
                            warehouseRecording.MemberWarehouseId = MemberWarehouses.MemberWarehouseId;
                            warehouseRecording.Type = "���";
                            memberwarehouserecordingbll.SaveForm(keyValue1, warehouseRecording);
                            //end
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
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
        /// <param name="strChildEntitys">�ӱ����</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<MemberCollarEntity>();
            //if (keyValue == "" || keyValue == null)
            //{
            //    entity.IsPassed = entity.IsSubmit = 0;
            //}
            entity.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            List<MemberCollarInfoEntity> childEntitys = strChildEntitys.ToList<MemberCollarInfoEntity>();

            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    //�ڿ�����м������������
                    var inventorymodel = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    inventorymodel.InStock = inventorymodel.InStock - Convert.ToDecimal(item.CollarQuantity);//���--
                    memberwarehousebll.SaveForm(item.MemberWarehouseId, inventorymodel);

                    //����������ѳ�����
                    var memberdemand = memberdemandbll.GetEntity(item.MemberDemandId);
                    memberdemand.CollaredNumber = memberdemand.CollaredNumber.ToDecimal() + item.CollarQuantity;
                    memberdemandbll.SaveForm(item.MemberDemandId, memberdemand);

                }
            }
            membercollarbll.SaveForm(keyValue, entity, childEntitys);

            return Success("�����ɹ���");
        }

        #endregion
    }
}
