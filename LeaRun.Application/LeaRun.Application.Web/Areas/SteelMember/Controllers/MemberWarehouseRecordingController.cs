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
    public class MemberWarehouseRecordingController : MvcControllerBase
    {
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private MemberWarehouseBLL memberwarehousebll = new MemberWarehouseBLL();
        private MemberWarehouseRecordingBLL memberwarehouserecordingbll = new MemberWarehouseRecordingBLL();
        private MemberProductionOrderBLL memberproductionorderbll = new MemberProductionOrderBLL();
        private MemberProductionOrderInfoBLL memberproductionorderinfobll= new MemberProductionOrderInfoBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();
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
        public ActionResult IntoInventoryDetail()
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
        /// <param name="Type"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination,string Type, string queryJson)
        {
            if (pagination.sidx == "RawMaterialName" || pagination.sidx == "RawMaterialCategory")
            {
                pagination.sidx = "RawMaterialId";
            }
            var watch = CommonHelper.TimerStart();
            var List = new List<MemberWarehouseRecordingModel>();
            var data = memberwarehouserecordingbll.GetPageList(pagination,Type, queryJson);

            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    var EngineeringId = "";
                    if(!item.SubProject.IsEmpty())
                    {
                        EngineeringId = item.SubProject;
                    }
                    else {
                        EngineeringId = memberwarehousebll.GetEntity(item.MemberWarehouseId).EngineeringId;
                    }
                    
                    var MemberLibrar = memberlibrarybll.GetEntity(item.MemberId);
                    var MemberWarehouseRecording = new MemberWarehouseRecordingModel()
                    {
                        MemberNumbering = MemberLibrar.MemberNumbering,
                        MemberName = MemberLibrar.MemberName,
                        Category = dataitemdetailbll.GetEntity(MemberLibrar.Category).ItemName,
                        UnitId = dataitemdetailbll.GetEntity(MemberLibrar.UnitId).ItemName,
                        CollarEngineering = subprojectbll.GetEntity(EngineeringId).FullName,
                        RecordingId = item.RecordingId,
                        InStock = item.InStock,
                        UpdateTime = item.UpdateTime,
                        //ToReportPeople = item.ToReportPeople,
                        //CollarDepartment= item.CollarDepartment,
                        //Receiver= item.Receiver,
                        //ReceiverTel = item.ReceiverTel,
                        Librarian = item.Librarian,
                        Description = MemberLibrar.Description
                    };
                    List.Add(MemberWarehouseRecording);
                }
            }

            //
            var queryParam = queryJson.ToJObject();

            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                //string Category = queryParam["Category"].ToString();
                List = List.FindAll(f => f.Category == Category);
            }
            if (!queryParam["MemberName"].IsEmpty())
            {
                var MemberName = queryParam["MemberName"].ToString();
                List = List.FindAll(t => t.MemberName.Contains(MemberName));
            }
            if (!queryParam["Numbering"].IsEmpty())
            {
                var Numbering = queryParam["Numbering"].ToString();
                List = List.FindAll(t => t.MemberNumbering.Contains(Numbering));
            }
            //
            var jsonData = new
            {
                rows = List,
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
            var data = memberwarehouserecordingbll.GetList(queryJson);
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
            var data = memberwarehouserecordingbll.GetEntity(keyValue);
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
            memberwarehouserecordingbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, MemberWarehouseRecordingEntity entity)
        {
            memberwarehouserecordingbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
