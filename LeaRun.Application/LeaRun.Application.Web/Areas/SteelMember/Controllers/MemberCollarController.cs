using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Util.Extension;
using LeaRun.Application.Code;
using System.Linq;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-09-13 22:58
    /// �� ������������
    /// </summary>
    public class MemberCollarController : MvcControllerBase
    {
        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RMC_MemberCollarIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberCollarForm()
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
            var data = membercollarbll.GetPageList(pagination, queryJson);
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
            var data = membercollarbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        ///// <summary>
        ///// ��ȡ����
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult GetNumberingList()
        //{
        //    var MemberCollarList = new List<MemberCollarEntity>();
        //    var memberproductionorder = memberproductionorderbll.GetList(f=>f.IsReceiveRawMaterial==1&&f.QualityInspectionStatus!=0);
        //    if (memberproductionorder.Count() > 0)
        //    {
        //        foreach (var item in memberproductionorder)
        //        {
        //           var MemberCollar = membercollarbll.GetList(f => f.CollarId != "" && f.CollarNumbering == null&&f.Numbering==item.OrderNumbering).SingleOrDefault();
        //            MemberCollarList.Add(MemberCollar);
        //        }
        //    }
        //    return ToJsonResult(MemberCollarList);
        //}

        ///// <summary>
        ///// ��ȡʵ�� 
        ///// </summary>
        ///// <param name="Numbering">����ֵ</param>
        ///// <returns>���ض���Json</returns>
        //[HttpGet]
        //public ActionResult NumberingToGetFormJson(string Numbering)
        //{
        //    var list = new List<MemberCollarInfoModel>();
        //    var data = membercollarbll.GetEntity(f => f.Numbering == Numbering.Trim());
        //    if (data != null)
        //    {
        //        data.OrganizeId = organizebll.GetEntity(data.OrganizeId).FullName;
        //        data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;

        //        var childData = membercollarinfobll.GetList(f => f.CollarId == data.CollarId);

        //        foreach (var item in childData)
        //        {
        //            var memberwarehouse = memberwarehousebll.GetEntity(f => f.MemberId == item.MemberId);
        //            var MemberLibrary = memberlibrarybll.GetEntity(memberwarehouse.MemberId);
        //            var MemberCollarInfomodel = new MemberCollarInfoModel()
        //            {
        //                InfoId = item.InfoId,
        //                MemberWarehouseId = memberwarehouse.MemberWarehouseId,
        //                MemberName = MemberLibrary.MemberName,
        //                MemberNumbering = MemberLibrary.MemberNumbering,
        //                InStock = memberwarehouse.InStock,
        //                CollaredQuantity = item.CollaredQuantity,
        //                Quantity = item.CollarQuantity,
        //                UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
        //                Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
        //                Description = item.Description,
        //            };
        //            list.Add(MemberCollarInfomodel);
        //        }
        //    }
        //    var jsonData = new
        //    {
        //        entity = data,
        //        childEntity = list
        //    };
        //    return ToJsonResult(jsonData);
        //}

        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = membercollarbll.GetEntity(keyValue);
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
        public ActionResult RemoveForm(string keyValue)
        {
            membercollarbll.RemoveForm(keyValue);
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
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveFormEdit(string keyValue)
        {
            var membercollar = membercollarbll.GetEntity(keyValue);
            membercollar.CollarNumbering = "";
            membercollarbll.SaveForm(keyValue, membercollar);
            var data = membercollarinfobll.GetList(f => f.CollarId == keyValue).ToList();
            if (data.Count()> 0)
            {
                foreach (var item in data)
                {
                    var membercollarinfo = data.Find(f => f.InfoId == item.InfoId);

                    membercollarinfo.MemberWarehouseId = "";
                    membercollarinfo.CollarQuantity = 0;
                    membercollarinfobll.SaveForm(item.InfoId, membercollarinfo);
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
        //public ActionResult SaveForm(string keyValue, MemberCollarEntity entity)
        //{
        //    membercollarbll.SaveForm(keyValue, entity);
        //    return Success("�����ɹ���");
        //}
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="Numbering">����ֵ</param>
        /// <param name="CollarNumbering">ʵ�����</param>
        /// <param name="strChildEntitys">�ӱ����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string Numbering, string CollarNumbering, string strChildEntitys)
        {
            var entity = memberproductionorderbll.GetEntity(f => f.OrderNumbering == Numbering);
            List<MemberCollarInfoEntity> childEntitys = strChildEntitys.ToList<MemberCollarInfoEntity>();

            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    //�жϿ�����Ƿ��������
                    var model = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    model.InStock = model.InStock.ToDecimal() - item.CollarQuantity.ToDecimal();//���--
                    if (model.InStock < 0)
                    {
                        //var member = memberlibrarybll.GetEntity(item.MemberId);
                        return Error("���ڹ�����治�㹹�����޷�����");
                    }
                }

                foreach (var item in childEntitys)
                {
                    //�ڿ�����м������������
                    var model = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    model.InStock = model.InStock.ToDecimal() - item.CollarQuantity.ToDecimal();//���--
                    memberwarehousebll.SaveForm(item.MemberWarehouseId, model);
                    //end

                    //�޸��������ѳ�����
                    var MemberProductionOrderInfo = memberproductionorderinfobll.GetEntity(item.InfoId);
                    MemberProductionOrderInfo.CollaredQuantity = MemberProductionOrderInfo.CollaredQuantity.ToDecimal() + item.CollarQuantity.ToDecimal();
                    memberproductionorderinfobll.SaveForm(MemberProductionOrderInfo.InfoId, MemberProductionOrderInfo);
                    //end

                    ////�޸���������ʹ����
                    //var DemandEntity = memberdemandbll.GetEntity(entitys.MemberDemandId);
                    //DemandEntity.CollaredNumber = DemandEntity.CollaredNumber.ToDecimal() + item.CollarQuantity.ToDecimal();
                    //memberdemandbll.SaveForm(entitys.MemberDemandId,DemandEntity);
                    ////end
                }
                //��������������¼
                var childEntitys1 = new List<MemberCollarInfoEntity>();
                var entity1 = new MemberCollarEntity()
                {
                    CollarNumbering = CollarNumbering,
                    Numbering = entity.OrderNumbering,
                    CollarEngineering = entity.Category,
                    OrganizeId = entity.OrganizeId,
                    Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    CreateMan = entity.CreateMan,
                    ShippingAddress = entity.ShippingAddress,
                    ContactPerson = entity.ContactPerson,
                    ContactPersonTel = entity.ContactPersonTel,
                };
                var ListEntity = memberproductionorderinfobll.GetList(f => f.OrderId == entity.OrderId);
                if (ListEntity.Count() > 0)
                {
                    foreach (var item1 in ListEntity)
                    {
                        var data = childEntitys.Find(f=>f.MemberId== item1.MemberId);
                        var Entity = new MemberCollarInfoEntity()
                        {
                            CollarQuantity = data.CollarQuantity,
                            MemberId = item1.MemberId,
                            MemberOrderInfoId = item1.InfoId,
                            MemberWarehouseId = data.MemberWarehouseId,
                            Description = data.Description
                        };
                        childEntitys1.Add(Entity);
                    }
                }
                membercollarbll.SaveForm("", entity1, childEntitys1);
                //end
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
                List<MemberCollarEntity> list = new List<MemberCollarEntity>();
                foreach (var item in ids)
                {
                    var model = membercollarbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsReceived = 1;
                        list.Add(model);

                    }
                }
                if (list.Count > 0)
                {
                    membercollarbll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }
        #endregion
    }
}
