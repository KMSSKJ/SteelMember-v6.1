﻿using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class RawMaterialLibraryController : MvcControllerBase
    {
        [Inject]
        public TreeIBLL treeBll { get; set; }
        [Inject]
        public MemberUnitIBLL unitBll { get; set; }
        [Inject]
        public RawMaterialIBLL rawMaterialBll { get; set; }
        //
        // GET: /SteelMember/RawMaterialLibrary/

        public ActionResult Index()
        {
            Session["moduleId"] = Request["moduleId"];

            return View();
        }
        /// <summary>
        /// 原材料表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 原材料列表 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            string moduleId = Session["moduleId"].ToString();
            var data = treeBll.Find(t => t.ModuleId == "b59c689d-dcc7-40ca-b3b8-5b81d3b080b4").ToList();
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

        /// <summary>
        /// 原材料列表
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination">分页参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(string parentid, /*string condition, */string keyword, Pagination pagination)
        {
            Expression<Func<RMC_RawMaterialLibrary, bool>> func = f => f.RawMaterialId != 0;
            if (parentid != "0" && parentid != "")
            {
                func = f => f.TreeId == parentid;
            }

            var watch = CommonHelper.TimerStart();
            int total = 0;
            var data = rawMaterialBll.FindPage(pagination.page
                                             , pagination.rows
                                             , func
                                             , false
                                             , f => f.Sort.ToString()
                                             , out total
                                             ).ToList();
            if (data.Count() > 0 && keyword != "" && keyword != null)
            {
                data = data.FindAll(t => t.RawMaterialModel.Contains(keyword));
            }
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }

        /// <summary>
        /// 原材料实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawMaterialBll.Find(r => r.RawMaterialId.ToString() == keyValue).Single();
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存原材料表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="rawMaterialLibraryEntity">原材料实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, RMC_RawMaterialLibrary rawMaterialLibraryEntity)
        {

            if (keyValue == "" || keyValue == null)
            {
                rawMaterialBll.Add(rawMaterialLibraryEntity);
            }
            else
            {
                var data = rawMaterialBll.Find(r => r.RawMaterialId.ToString() == keyValue).Single();
                data.RawMaterialModel = rawMaterialLibraryEntity.RawMaterialModel;
                data.RawMaterialStandard = rawMaterialLibraryEntity.RawMaterialStandard;
                data.TreeId = rawMaterialLibraryEntity.TreeId;
                data.UnitId = rawMaterialLibraryEntity.UnitId;
                data.Description = rawMaterialLibraryEntity.Description;

                rawMaterialBll.Modified(data);

            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除原材料
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            List<int> ids = new List<int>();
            if (keyValue != "" || keyValue != null)
            {
                ids.Add(Convert.ToInt32(keyValue));
            }
            rawMaterialBll.Remove(ids);
            return Success("删除成功。");
        }

        /// <summary>
        /// 型号不能重复
        /// </summary>
        /// <param name="RawMaterialModel">型号</param>
        /// <param name="keyValue"></param>
        /// <param name="treeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string RawMaterialModel, string keyValue,string treeId)
        {
            bool IsOk = false;

            var expression = LinqExtensions.True<RMC_RawMaterialLibrary>();
            expression = expression.And(t => t.RawMaterialModel.Trim() == RawMaterialModel.Trim()&&t.TreeId.Trim()==treeId.Trim());
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RawMaterialId.ToString() != keyValue);
            }

            IsOk = rawMaterialBll.Find(expression).Count() == 0 ? true : false;
            return Content(IsOk.ToString());
        }

        /// <summary>
        /// 单位列表 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetUnitJson()
        {
            var data = unitBll.Find(u => u.UnitId > 0).ToList();
            return Content(data.ToJson());
        }
    }
}
