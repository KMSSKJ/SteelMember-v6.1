using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 17:19
    /// �� �������ù���
    /// </summary>
    public class RawMterialCollarController : MvcControllerBase
    {
        private RawMterialCollarBLL rawmterialcollarbll = new RawMterialCollarBLL();
        private RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        /// ��ҳ��ѯ������Ϣ
        /// </summary>
        /// <returns></returns>
         public ActionResult OutInventoryDetailInfo(Pagination pagination, string category)
        {
            var degintime = Convert.ToDateTime(Request["begintime"]) == Convert.ToDateTime(null) ? Convert.ToDateTime(null) : Convert.ToDateTime(Request["begintime"]);
            var endtime = Convert.ToDateTime(Request["endtime"]) == Convert.ToDateTime(null) ? System.DateTime.Now : Convert.ToDateTime(Request["endtime"]);
            List<RawMaterialCollarModel> list = new List<RawMaterialCollarModel>();
            try
            {
                var data = rawmateriallibrarybll.GetPageListByLikeCategory(pagination, category);
                //List<RawMaterialCollarModel> list = new List<RawMaterialCollarModel>();
                foreach (var item in data)
                {
                    var linventory = rawmaterialinventorybll.GetEntityByRawMaterialId(item.RawMaterialId);
                    //var collarlist= rawmterialcollarbll.GetCallarList(p=>p.InventoryId== linventory.InventoryId&&p.CollarTime>= degintime&&p.CollarTime<= endtime);
                    var query = linventory.InventoryId + "," + degintime + "," + endtime;
                    var collarlist = rawmterialcollarbll.GetPageList(pagination, query);
                    for (var i = 0; i < collarlist.Count; i++)
                    {
                        RawMaterialCollarModel rawMaterialCollarModel = new RawMaterialCollarModel();
                        var subproject = subprojectbll.GetEntity(collarlist[i].CollarEngineering);

                        rawMaterialCollarModel.CollarId = collarlist[i].CollarId;
                        rawMaterialCollarModel.InventoryId = collarlist[i].InventoryId;
                        //rawMaterialCollarModel.CollarType = collarlist[i].CollarType;
                        //rawMaterialCollarModel.CollarEngineering = item.CollarEngineering;
                        rawMaterialCollarModel.CollarEngineering = subproject.FullName;//ȡ���ӹ����� ��������

                        rawMaterialCollarModel.CollarTime = collarlist[i].CollarTime;
                        rawMaterialCollarModel.CollarQuantity = collarlist[i].CollarQuantity;
                        rawMaterialCollarModel.CollarMan = collarlist[i].CollarMan;
                        rawMaterialCollarModel.Description = collarlist[i].Description;
                        rawMaterialCollarModel.CollarType = collarlist[i].CollarType == 1 ? "��������" : "��������";

                        //rawMaterialCollarModel.Category = item.Category;
                        rawMaterialCollarModel.Category = item.RawMaterialName;
                        rawMaterialCollarModel.RawMaterialModel = item.RawMaterialModel;
                        rawMaterialCollarModel.RawMaterialStandard = item.RawMaterialStandard;
                        rawMaterialCollarModel.Unit = item.Unit;

                        list.Add(rawMaterialCollarModel);

                    }
                }
            }
            catch (Exception e)
            {
                return ToJsonResult(e);
                //throw (e);
            }
            return ToJsonResult(list);
        }
        //public ActionResult OutInventoryDetailInfo(Pagination pagination, string queryJson)
        //{
        //    var data = rawmterialcollarbll.OutInventoryDetailInfo(pagination, queryJson);
        //    List<RawMaterialCollarModel> list = new List<RawMaterialCollarModel>();
        //    foreach (var item in data)
        //    {
        //        RawMaterialCollarModel rawMaterialCollarModel = new RawMaterialCollarModel();
        //        var inventorymodel = rawmaterialinventorybll.GetEntity(item.InventoryId);
        //        var librarymodel = rawmateriallibrarybll.GetEntity(inventorymodel.RawMaterialId);
        //        //ȡ������value
        //        var subproject = subprojectbll.GetEntity(item.CollarEngineering);

        //        rawMaterialCollarModel.CollarId = item.CollarId;
        //        rawMaterialCollarModel.InventoryId = item.InventoryId;
        //        rawMaterialCollarModel.CollarType = item.CollarType;
        //        //rawMaterialCollarModel.CollarEngineering = item.CollarEngineering;
        //        rawMaterialCollarModel.CollarEngineering = subproject.FullName;//ȡ���ӹ�����

        //        rawMaterialCollarModel.CollarTime = item.CollarTime;
        //        rawMaterialCollarModel.CollarQuantity = item.CollarQuantity;
        //        rawMaterialCollarModel.CollarMan = item.CollarMan;
        //        rawMaterialCollarModel.Description = item.Description;

        //        rawMaterialCollarModel.Category = librarymodel.Category;
        //        rawMaterialCollarModel.RawMaterialModel = librarymodel.RawMaterialModel;
        //        rawMaterialCollarModel.RawMaterialStandard = librarymodel.RawMaterialStandard;
        //        rawMaterialCollarModel.Unit = librarymodel.Unit;

        //        list.Add(rawMaterialCollarModel);

        //    }
        //    return ToJsonResult(list);
        //}

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = rawmterialcollarbll.GetList(queryJson);
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
            var data = rawmterialcollarbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="collarinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCollarinfo(string collarinfo) {
            var collarmodel = collarinfo.ToObject<RawMterialCollarEntity>();
            collarmodel.CollarTime = System.DateTime.Now;
            try {
                if (collarmodel.CollarQuantity > 0)
                {
                    //�ڿ�����м������������
                    var inventorymodel = rawmaterialinventorybll.GetEntity(collarmodel.InventoryId);
                    inventorymodel.Quantity = inventorymodel.Quantity - collarmodel.CollarQuantity;//���--
                    rawmaterialinventorybll.SaveForm(collarmodel.InventoryId, inventorymodel);

                    //��ӵ����ñ���  
                    string keyValue="";
                    rawmterialcollarbll.SaveForm(keyValue, collarmodel);
                }
            } catch (System.Exception e) {
                throw e;
            }
            return Success("���óɹ���");

        }
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
            rawmterialcollarbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, RawMterialCollarEntity entity)
        {
            rawmterialcollarbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
