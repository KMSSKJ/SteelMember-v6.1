using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class DirectoryController : MvcControllerBase
    {
        [Inject]
        public TreeIBLL TreeCurrent { get; set; }
        //
        // GET: /SteelMember/Directory/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 按钮表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string moduleId)
        {
            var data = TreeCurrent.Find(t => t.ModuleId == moduleId).ToList();
            return Content(data.ToJson());
        }

        /// <summary>
        /// 按钮列表 
        /// </summary>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string moduleId)
        {
            var data = TreeCurrent.Find(t => t.ModuleId == moduleId).ToList();
            if (data != null)
            {
                var TreeList = new List<TreeGridEntity>();
                foreach (RMC_Tree item in data)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    bool hasChildren = data.Count(t => t.ParentID == item.TreeID) == 0 ? false : true;
                    tree.id = item.TreeID.ToString();
                    tree.parentId = item.ParentID.ToString();
                    tree.expanded = true;
                    tree.hasChildren = hasChildren;
                    tree.entityJson = item.ToJson();
                    TreeList.Add(tree);
                }
                return Content(TreeList.TreeJson());
            }
            return null;
        }

        /// <summary>
        /// 按钮列表Json转换按钮树形Json 
        /// </summary>
        /// <param name="moduleButtonJson">按钮列表</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        public ActionResult ListToListTreeJson(string moduleButtonJson)
        {
            var data = moduleButtonJson.ToList<RMC_Tree>();
            var TreeList = new List<TreeGridEntity>();
            foreach (RMC_Tree item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentID == item.TreeID) == 0 ? false : true;
                tree.id = item.TreeID;
                tree.parentId = item.ParentID;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }
        /// <summary>
        /// 按钮列表Json转换按钮树形Json 
        /// </summary>
        /// <param name="moduleButtonJson">按钮列表</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        public ActionResult ListToTreeJson(string moduleButtonJson)
        {
            var data = moduleButtonJson.ToList<RMC_Tree>();
            var treeList = new List<TreeEntity>();
            foreach (RMC_Tree item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentID == item.TreeID) == 0 ? false : true;
                tree.id = item.TreeID;
                tree.text = item.TreeName;
                tree.value = item.ModuleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentID.ToString();
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
    }
}
