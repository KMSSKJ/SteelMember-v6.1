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

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNumberingList()
        {
            //List<Text> list = new List<Text>();
            var MemberCollar = membercollarbll.GetList("");

            return ToJsonResult(MemberCollar);
        }

        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="Numbering">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult NumberingToGetFormJson(string Numbering)
        {
            var list = new List<MemberCollarInfoModel>();
            var data = membercollarbll.GetEntity(f => f.Numbering == Numbering.Trim());
            if (data != null)
            {
                data.DepartmentId = departmentbll.GetEntity(data.DepartmentId).FullName + "(" + organizebll.GetEntity(departmentbll.GetEntity(data.DepartmentId).OrganizeId).FullName + ")";
                data.CollarEngineering = subprojectbll.GetEntity(data.CollarEngineering).FullName;

                var childData = membercollarinfobll.GetList(f => f.CollarId == data.CollarId);

                foreach (var item in childData)
                {
                    var memberwarehouse = memberwarehousebll.GetEntity(f => f.MemberId== item.MemberId);
                    var MemberLibrary = memberlibrarybll.GetEntity(memberwarehouse.MemberId);
                    var MemberCollarInfomodel = new MemberCollarInfoModel()
                    {
                        InfoId = item.InfoId,
                        MemberWarehouseId = memberwarehouse.MemberWarehouseId,
                        MemberName = MemberLibrary.MemberName,
                        MemberNumbering= MemberLibrary.MemberNumbering,
                        CollarQuantity = item.CollarQuantity,
                        CollaredQuantity = item.CollaredQuantity,
                        Quantity = item.CollarQuantity,
                        UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
                        Description = item.Description,
                    };
                    list.Add(MemberCollarInfomodel);
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
            var entity = membercollarbll.GetEntity(f => f.Numbering == Numbering);
            entity.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            entity.CollarNumbering = CollarNumbering;
            entity.ReviewMan = OperatorProvider.Provider.Current().UserName;

            List<MemberCollarInfoEntity> childEntitys = strChildEntitys.ToList<MemberCollarInfoEntity>();

            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    //�ڿ�����м������������
                    var model = memberwarehousebll.GetEntity(item.MemberWarehouseId);
                    model.InStock = model.InStock.ToDecimal() - item.CollarQuantity.ToDecimal();//���--
                    memberwarehousebll.SaveForm(item.MemberWarehouseId, model);
                    //end

                    //�޸ĳ�����Ϣ
                    var entitys = membercollarinfobll.GetEntity(item.InfoId);
                    entitys.CollarQuantity = item.CollarQuantity;
                    entitys.CollaredQuantity = entitys.CollaredQuantity.ToDecimal() + item.CollarQuantity;
                    entitys.MemberWarehouseId = item.MemberWarehouseId;
                    entitys.Description = item.Description;
                    membercollarinfobll.SaveForm(item.InfoId, entitys);
                    //end

                    //�޸���������ʹ����
                    var DemandEntity = memberdemandbll.GetEntity(entitys.MemberDemandId);
                    DemandEntity.CollaredNumber = DemandEntity.CollaredNumber.ToDecimal() + item.CollarQuantity;
                    //end
                }
            }
            membercollarbll.SaveForm(entity.CollarId, entity);

            return Success("�����ɹ���");
        }
        #endregion
    }
}
