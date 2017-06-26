
using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Ninject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class ProjectInfoController : MvcControllerBase
    {
        //
        // GET: /ProjectInfo/

        [Inject]
        public ProjectInfoIBLL ProjectInfoCurrent { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }
    
        public ActionResult DepartmentIndex()
        {
            return View();
        }
        public ActionResult ManIndex()
        {
            return View();
        }

        #region 获取项目信息
        [HttpGet]
    public ActionResult GetItemInfo(string KeyValue)
        {
            RMC_ProjectInfo entity = ProjectInfoCurrent.Find(f => f.TreeID > 0).SingleOrDefault();
            return Content(entity.ToJson());
        }    
        #endregion
    }
}
