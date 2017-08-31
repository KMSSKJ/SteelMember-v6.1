using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using System.Web.Mvc;
using System.Linq;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-08-30 11:16
    /// �� ������Ŀ��Ϣ
    /// </summary>
    public class ProjectInfoController : MvcControllerBase
    {
        private ProjectInfoBLL projectinfobll = new ProjectInfoBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProjectIndex()
        {
            return View();
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProjectForm()
        {
            return View();
        }

        /// <summary>
        /// �����б�ҳ��
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentIndex()
        {
            return View();
        }

        /// <summary>
        /// �û��б�ҳ��
        /// </summary>
        /// <returns></returns>
        public ActionResult ManIndex()
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
            var data = projectinfobll.GetList(queryJson);
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
            // var data = projectinfobll.GetEntity(keyValue
            var data = projectinfobll.GetList(null).ToList().Find(f=>f.ProjectId== keyValue);
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
            projectinfobll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string ProjectId, ProjectInfoEntity entity)
        {
            entity.ProjectId = ProjectId;
            projectinfobll.SaveForm(entity.ProjectInfoId, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
