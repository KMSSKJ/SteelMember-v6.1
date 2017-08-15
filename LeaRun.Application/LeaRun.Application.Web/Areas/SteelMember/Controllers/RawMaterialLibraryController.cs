using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Entity.SystemManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 10:42
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public class RawMaterialLibraryController : MvcControllerBase
    {
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();

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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart(); 
            var data = rawmateriallibrarybll.GetList(pagination, queryJson);
            List<Models.RMLibraryModel> list = new List<Models.RMLibraryModel>();
            try {
                if (data!=null) {
                    foreach (var item in data) {
                        var dataItem = dataItemDetailBLL.GetEntity(item.Category);
                        Models.RMLibraryModel rmlibrarymodel = new Models.RMLibraryModel();
                        rmlibrarymodel.Category = dataItem.ItemName;
                        rmlibrarymodel.RawMaterialId = item.RawMaterialId;
                        rmlibrarymodel.RawMaterialName = item.RawMaterialName;
                        rmlibrarymodel.RawMaterialModel = item.RawMaterialModel;
                        rmlibrarymodel.Unit = item.Unit;
                        rmlibrarymodel.Description = item.Description;

                        list.Add(rmlibrarymodel);
                    }
                }
            } catch (System.Exception e) {
                throw;
            }
            var JsonData = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        //��ȡ���ֽ��ӽڵ�(��ѭ��)
        public List<DataItemDetailEntity> GetSonId(string ItemDetailId)
        {
            List<DataItemDetailEntity> list = dataItemDetailBLL.GetByParentToItemIdIdList(ItemDetailId);
            return list.Concat(list.SelectMany(t => GetSonId(t.ParentId))).ToList();
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmateriallibrarybll.GetEntity(keyValue);
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
            rawmateriallibrarybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, RawMaterialLibraryEntity entity)
        {
            rawmateriallibrarybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion

        #region ��֤����

        /// <summary>
        /// ԭ�������ͺŲ����ظ�
        /// </summary>
        /// <param name="RawMaterialModel">�ͺ�</param>
        /// <param name="keyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ExistFullName(string RawMaterialModel,string category, string keyValue)
        {
            bool IsOk = rawmateriallibrarybll.ExistFullName(RawMaterialModel, category, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
