using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Util.Extension;
using System.Linq;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 17:17
    /// �� ����������
    /// </summary>
    public class RawMaterialWarehouseController : MvcControllerBase
    {
        private RawMaterialWarehouseBLL rawmaterialwarehousebll = new RawMaterialWarehouseBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
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
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = rawmaterialwarehousebll.GetList(queryJson);
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
            var data = rawmaterialwarehousebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// �����ϸ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ActionResult IntoInventoryDetailInfo(Pagination pagination,string queryJson, string category)
        {
            if (pagination.sidx == "RawMaterialName")
            {
                pagination.sidx = "RawMaterialId";
            }

            List<RawMaterialWarehouseModel> list = new List<RawMaterialWarehouseModel>();
            // var data = rawmateriallibrarybll.GetPageListByLikeCategory(pagination, category);
            var data= rawmaterialwarehousebll.GetPageList(pagination, category);
            foreach (var item in data)
            {
                //var warehoused = rawmaterialwarehousebll.GetPageList(pagination, item.RawMaterialId);
                var warehoused = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                if (warehoused!=null)
                {
                    RawMaterialWarehouseModel RawmaterialWarehouseModel = new RawMaterialWarehouseModel() { 
                    WarehouseId = item.WarehouseId,
                    WarehouseQuantity = item.WarehouseQuantity,
                    WarehouseTime = item.WarehouseTime,
                    Description = item.Description,
                    RawMaterialModel = warehoused.RawMaterialModel,
                    RawMaterialName = warehoused.RawMaterialName,
                    Unit = dataitemdetailbll.GetEntity(warehoused.Unit).ItemName,
                };
                list.Add(RawmaterialWarehouseModel);
                }   
            }

            //
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.UpdateTime >= BeginTime);
                list = list.FindAll(t => t.UpdateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.UpdateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.UpdateTime <= EndTime);
            }


            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    //case "Category":              //��������
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    case "RawMaterialName":              //��������
                        list = list.FindAll(t => t.RawMaterialName.Contains(keyword));
                        break;
                    case "RawMaterialModel":              //�ͺ�
                        list = list.FindAll(t => t.RawMaterialModel.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            //

            return ToJsonResult(list);

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
            rawmaterialwarehousebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, RawMaterialWarehouseEntity entity)
        {
            rawmaterialwarehousebll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
