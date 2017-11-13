using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-08-29 15:39
    /// �� ������ȫ�豸
    /// </summary>
    public class SafetyEquipmentController : MvcControllerBase
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
        [HttpGet]
        public ActionResult EquipmentDeail(string keyValue)
        {
            var model = safetyequipmentbll.GetEntity(keyValue);
            string filePath = System.IO.Path.GetFileNameWithoutExtension(model.Icon);
            model.Icon = "/Resource/Document/NetworkDisk/System/Member/" + filePath + "/" + model.Icon;
            return View(model);
        }
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index1()
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
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = safetyequipmentbll.GetPageList(pagination, queryJson);
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
            var data = safetyequipmentbll.GetList(queryJson);
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
            var data = safetyequipmentbll.GetEntity(keyValue);
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
            string[] idsArr = keyValue.Split(',');
            foreach (var item1 in idsArr)
            {
                safetyequipmentbll.RemoveForm(item1);
                var list = equipmentmaintenancerecordsbll.GetList(f => f.Id == item1);
                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        equipmentmaintenancerecordsbll.RemoveForm(item.InfoId);
                    }
                }
            }
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
        public ActionResult SaveForm(string keyValue, SafetyEquipmentEntity entity)
        {
            entity.Icon = System.IO.Path.GetFileName(entity.Icon);
            safetyequipmentbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }


        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult Warranty(string keyValue)
        {
            var ids = keyValue.Split(',');

            if (ids.Count() > 0)
            {
                foreach (var item in ids)
                {
                    var safetyequipment = safetyequipmentbll.GetEntity(item);
                    safetyequipment.Status = 3;
                    safetyequipmentbll.SaveForm(item, safetyequipment);
                }
            }
            return Success("�����ɹ���");
        }
        #endregion
    }
}
