using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeController : MvcControllerBase
    {
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        private OrganizeCache organizeCache = new OrganizeCache();

        #region 视图功能
        /// <summary>
        /// 机构管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 机构表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var data = organizeCache.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "OrganizeId");
            }
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                tree.id = item.OrganizeId;
                tree.text = item.FullName;
                tree.value = item.OrganizeId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]   
        public ActionResult GetTreeListJson(string queryJson, string keyword)
        {
            var data = organizeBLL.GetList().ToList();
            var queryParam = queryJson.ToJObject();
          
            if (!string.IsNullOrEmpty(queryParam.ToString()) && !string.IsNullOrEmpty(keyword))
            {
                var condition = queryParam["condition"].ToString();
                #region 多条件查询
                switch (condition)
                {
                    case "FullName":    //公司名称
                        //data = data.TreeWhere(t => t.FullName.Contains(keyword), "OrganizeId");
                        data = data.FindAll(t => t.FullName.Contains(keyword));
                        break;
                    case "EnCode":      //外文名称
                        data = data.FindAll(t => t.EnCode.Contains(keyword));
                        break;
                    case "ShortName":   //中文名称
                        data = data.FindAll(t => t.ShortName.Contains(keyword));
                        break;
                    case "Manager":     //负责人
                        data = data.FindAll(t => t.Manager.Contains(keyword));
                        break;
                    default:
                        break;
                }
                #endregion
            }
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword1 = queryParam["keyword"].ToString();
                data = data.FindAll(t => t.FullName.Contains(keyword1));
            }

            if (!queryParam["Nature"].IsEmpty())
            {
                string Nature = queryParam["Nature"].ToString();
                data = data.FindAll(t => t.Nature.Contains(Nature));
            }
                        
            //var treeList = new List<TreeGridEntity>();
            //foreach (OrganizeEntity item in data)
            //{
            //    TreeGridEntity tree = new TreeGridEntity();
            //    bool hasChildren = data.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
            //    tree.id = item.OrganizeId;
            //    tree.hasChildren = hasChildren;
            //    tree.parentId = item.ParentId;
            //    tree.expanded = true;
            //    tree.entityJson = item.ToJson();
            //    treeList.Add(tree);
            //}
            return Content(data.ToJson());
        }

        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="keyword"></param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson1(string queryJson, string keyword)//
        {
            var queryParam = queryJson.ToJObject();
            string Nature = "";
            if (!queryParam["Nature"].IsEmpty())
            {
                Nature = queryParam["Nature"].ToString();
                // data = data.FindAll(t => t.Nature.Contains(Nature));
            }
            var data = new List<OrganizeEntity>();
            var itemdata = dataitemdetailbll.GetList(f => f.ParentId == Nature);
            if (itemdata.Count() > 0)
            {
                foreach (var item in itemdata)
                {
                    var data1 = organizeBLL.GetList().ToList();
                    if (data1.Count() > 0)
                    {
                        foreach (var item1 in data1)
                        {
                            if (data.Find(f => f.OrganizeId == item1.OrganizeId) == null)
                            {
                                data.Add(item1);
                            }
                        }
                    }
                }
            }



            if (!string.IsNullOrEmpty(queryParam.ToString()) && !string.IsNullOrEmpty(keyword))
            {
                var condition = queryParam["condition"].ToString();
                #region 多条件查询
                switch (condition)
                {
                    case "FullName":    //公司名称
                        //data = data.TreeWhere(t => t.FullName.Contains(keyword), "OrganizeId");
                        data = data.FindAll(t => t.FullName.Contains(keyword));
                        break;
                    case "EnCode":      //外文名称
                        data = data.FindAll(t => t.EnCode.Contains(keyword));
                        break;
                    case "ShortName":   //中文名称
                        data = data.FindAll(t => t.ShortName.Contains(keyword));
                        break;
                    case "Manager":     //负责人
                        data = data.FindAll(t => t.Manager.Contains(keyword));
                        break;
                    default:
                        break;
                }
                #endregion
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="keyword"></param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson2(string queryJson, string keyword)//
        {
            var queryParam = queryJson.ToJObject();
            string Nature = "";
            if (!queryParam["Nature"].IsEmpty())
            {
                Nature = queryParam["Nature"].ToString();
            }
            //var data = new List<OrganizeEntity>();
            //var itemdata = dataitemdetailbll.GetList(f => f.ParentId == Nature);
            //if (itemdata.Count() > 0)
            //{
            //    foreach (var item in itemdata)
            //    {
            //        var data1 = organizeBLL.GetList().ToList();
            //        if (data1.Count() > 0)
            //        {
            //            foreach (var item1 in data1)
            //            {
            //                if (data.Find(f => f.OrganizeId == item1.OrganizeId) == null)
            //                {
            //                    data.Add(item1);
            //                }
            //            }
            //        }
            //    }
            //}
            var data = organizeBLL.GetList(f=>f.Nature == Nature).ToList();
            return Content(data.ToJson());
        }


        /// <summary>
        /// 机构实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = organizeBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="FullName">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = organizeBLL.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="EnCode">外文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string EnCode, string keyValue)
        {
            bool IsOk = organizeBLL.ExistEnCode(EnCode, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="ShortName">中文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistShortName(string ShortName, string keyValue)
        {
            bool IsOk = organizeBLL.ExistShortName(ShortName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            organizeBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存机构表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="organizeEntity">机构实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, OrganizeEntity organizeEntity)
        {
            organizeBLL.SaveForm(keyValue, organizeEntity);
            return Success("操作成功。");
        }
        #endregion
    }
}
