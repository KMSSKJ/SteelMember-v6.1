using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util.Extension;
using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-06-30 22:01
    /// �� �����ӹ�����Ϣ
    /// </summary>
    public class SubProjectController : MvcControllerBase
    {
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ����Ŀ�� 
        /// </summary>
        /// <param name="Levels">�ؼ���</param>
        /// <returns>��������Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string Levels)
        {
            var data = subprojectbll.GetList(Levels);
            var treeList = new List<TreeEntity>();
            foreach (SubProjectEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.Id;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeJsonParentId(string SubProjectId)
        {
            var data = subprojectbll.GetEntity(SubProjectId);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = subprojectbll.GetList(queryJson).ToList();
            if (!string.IsNullOrEmpty(queryJson))
            {
                data = data.FindAll(t => t.FullName.Contains(queryJson));
            }
            var treeList = new List<TreeGridEntity>();
            foreach (SubProjectEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.entityJson = item.ToJson();
                treeList.Add(tree);
            }
            return Content(treeList.TreeJson());
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = subprojectbll.GetEntity(keyValue);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            var data = subprojectbll.GetList(null).ToList();
            data = data.FindAll(t => t.ParentId == keyValue);
            if (data.Count()== 0)
            {
                subprojectbll.RemoveForm(keyValue);
            }
            else
            {//ɾ�����ڵ�������
                subprojectbll.RemoveForm(keyValue);
                foreach (var item in data)
                {
                    subprojectbll.RemoveForm(item.Id);
                }
            }
            return Success("ɾ���ɹ�");
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
        public ActionResult SaveForm(string keyValue, SubProjectEntity entity)
        {
            subprojectbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �����������Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">�ͺ�</param>
        /// <param name="keyValue"></param>
        /// <param name="treeId"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue, string treeId)
        {
            bool IsOk = subprojectbll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }

        /// <summary>
        /// ��֤�Ƿ����������
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ExistSubData(string keyValue)
        {
            var data = subprojectbll.GetList(null).ToList();
            bool hasChildren = data.Count(t => t.ParentId == keyValue) == 0 ? false : true;
            if (hasChildren)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
