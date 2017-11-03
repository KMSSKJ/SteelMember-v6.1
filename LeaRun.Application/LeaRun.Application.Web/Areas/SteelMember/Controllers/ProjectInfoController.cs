using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Web.WebPages;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-08-30 11:16
    /// 描 述：项目信息
    /// </summary>
    public class ProjectInfoController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ProjectIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ProjectForm()
        {
            return View();
        }

        /// <summary>
        /// 部门列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DepartmentIndex()
        {
            return View();
        }
        /// <summary>
        /// 用户列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ManIndex()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = projectinfobll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            // var data = projectinfobll.GetEntity(keyValue
            var data = projectinfobll.GetList(null).SingleOrDefault();
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            projectinfobll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string ProjectId, ProjectInfoEntity entity)
        {
            //entity.ProjectId = ProjectId;
            //entity.StartTime =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            projectinfobll.SaveForm(entity.ProjectInfoId, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public string GetProjectInfoId(string ProjectId, ProjectInfoEntity entity)
        {
            //entity.ProjectId = ProjectId;
            //entity.StartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            return  projectinfobll.GetProjectInfoId(entity.ProjectInfoId, entity);
        }
        #endregion
    }
}
