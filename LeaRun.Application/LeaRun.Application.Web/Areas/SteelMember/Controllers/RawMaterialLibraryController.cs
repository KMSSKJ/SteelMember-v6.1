using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Util.WebControl;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class RawMaterialLibraryController : MvcControllerBase
    {
        [Inject]
        public TreeIBLL treeBll { get; set; }
        [Inject]
        public RawMaterialIBLL rawMaterialBll { get; set; }
        //
        // GET: /SteelMember/RawMaterialLibrary/

        public ActionResult Index()
        {
            Session["moduleId"] = Request.QueryString["username"];
            return View();
        }

        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            string moduleId = Session["moduleId"].ToString();
            var data = treeBll.Find(t=>t.ModuleId==moduleId).ToList();
            var treeList = new List<TreeEntity>();
            foreach (RMC_Tree item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentID == item.TreeID) == 0 ? false : true;
                tree.id = item.TreeID;
                tree.text = item.TreeName;
                tree.value = item.TreeID;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentID;
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
    }
}
