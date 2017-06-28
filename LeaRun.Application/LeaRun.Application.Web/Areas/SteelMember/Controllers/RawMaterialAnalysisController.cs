﻿using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Util;
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
    public class RawMaterialAnalysisController : MvcControllerBase
    {
        [Inject]
        public TreeIBLL treeBll { get; set; }
        [Inject]
        public MemberUnitIBLL unitBll { get; set; }
        [Inject]
        public RawMaterialIBLL rawMaterialBll { get; set; }
        [Inject]
        public RawMaterialAnalysisIBLL rawMaterialAnalysisBll { get; set; }
        //
        // GET: /SteelMember/RawMaterialAnalysis/

        public ActionResult Index()
        {
            Session["moduleId"] = Request["moduleId"];
            return View();
        }

        /// <summary>
        /// 原材料分析表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 原材料分析列表 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            string moduleId = Session["moduleId"].ToString();
            var data = treeBll.Find(t => t.ModuleId == moduleId).ToList();
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
        /// 原材料分析列表
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination">分页参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(string parentid, string keyword, Pagination pagination)
        {
            Expression<Func<RMC_RawMaterialAnalysis, bool>> func = f => f.Id != 0;
            if (parentid != "0" && parentid != "")
            {
                func = f => f.TreeId == parentid;
            }

            var watch = CommonHelper.TimerStart();
            int total = 0;
            var data = rawMaterialAnalysisBll.FindPage(pagination.page
                                             , pagination.rows
                                             , func
                                             , false
                                             , f => f.Id.ToString()
                                             , out total
                                             ).ToList();
            if (data.Count() > 0 && keyword != "" && keyword != null)
            {
                data = data.FindAll(t => t.RawMaterialId.ToString().Contains(keyword));
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
        /// 原材料分析实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawMaterialAnalysisBll.Find(r => r.Id.ToString() == keyValue).Single();
            return Content(data.ToJson());
        }

    }
}