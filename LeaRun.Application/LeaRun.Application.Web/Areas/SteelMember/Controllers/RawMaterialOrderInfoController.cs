using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-08-07 17:35
    /// �� ������������
    /// </summary>
    public class RawMaterialOrderInfoController : MvcControllerBase
    {
        private RawMaterialOrderInfoBLL rawmaterialorderinfobll = new RawMaterialOrderInfoBLL();
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
        /// ����ҳ��
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
        /// <param name="keyValue">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string keyValue)
        {
            var data = rawmaterialorderinfobll.GetList(f=>f.OrderId== keyValue).ToList();
            var RawMaterialList = new List<MemberMaterialModel>();
            for (int i = 0; i < data.Count(); i++)
            {
                var rawmaterial = rawmateriallibrarybll.GetEntity(data[i].RawMaterialId);
                var Member = new MemberMaterialModel()
                {
                    InfoId = data[i].InfoId,
                    RawMaterialId = data[i].RawMaterialId,
                    RawMaterialNumber = data[i].Quantity,
                    Category = dataitemdetailbll.GetEntity(rawmaterial.Category).ItemName,
                    RawMaterialName = rawmaterial.RawMaterialName,
                    RawMaterialModel = rawmaterial.RawMaterialModel,
                    UnitId =dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName,
                };
                RawMaterialList.Add(Member);
            }
            return ToJsonResult(RawMaterialList);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmaterialorderinfobll.GetEntity(keyValue);
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
            rawmaterialorderinfobll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ����������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, RawMaterialOrderInfoEntity entity)
        {
            rawmaterialorderinfobll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}