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
    /// 版 本 6.1
    /// 日 期：2017-06-30 22:01
    /// 描 述：子工程信息
    /// </summary>
    public class SubProjectController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 子项目树 
        /// </summary>
        /// <param name="Levels">关键字</param>
        /// <returns>返回树形Json</returns>
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = subprojectbll.GetList(queryJson).ToList();
            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                var FullName = queryParam["keyword"].ToString();
                data = data.FindAll(t => t.FullName.Contains(FullName));
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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = subprojectbll.GetEntity(keyValue);
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
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            int Number = 0;
            var list = GetSonId(keyValue);
            var Entity = new SubProjectEntity()
            {
                Id = keyValue,
            };
            list.Add(Entity);

            foreach (var item in list)
            {
                var memberdemand = memberdemandbll.GetList(item.Id);
                var memberlibrary= memberlibrarybll.GetList(f =>f.EngineeringId == item.Id);
                var memberproductionorder=  memberproductionorderbll.GetList(f=>f.Category== item.Id);
                var rawmaterialanalysis =rawmaterialanalysisbll.GetList(f=>f.Id== item.Id);
                var rawmaterialorder= rawmaterialorderbll.GetList(f=>f.Category== item.Id);
                var memberwarehouse =memberwarehousebll.GetList(f=>f.EngineeringId== item.Id);
                Number = Number + memberdemand.Count() + memberlibrary.Count() + memberproductionorder.Count() + rawmaterialanalysis.Count() + rawmaterialorder.Count() + memberwarehouse.Count() + memberlibrary.Count();
            }
            if (Number > 0)
            {
                return Error("该节点下存在关联数据不允许删除！");
            }
            else
            {
                var data = subprojectbll.GetList(null).ToList();
                data = data.FindAll(t => t.ParentId == keyValue);
                if (data.Count() == 0)
                {
                    subprojectbll.RemoveForm(keyValue);
                }
                else
                {//删除存在的子数据
                    subprojectbll.RemoveForm(keyValue);
                    foreach (var item in data)
                    {
                        subprojectbll.RemoveForm(item.Id);
                    }
                }
                return Success("删除成功");

            }  
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, SubProjectEntity entity)
        {
            subprojectbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 分类型下名称不能重复
        /// </summary>
        /// <param name="FullName">型号</param>
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
        /// 验证是否存在子数据
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

        /// <summary>
        /// 获取树字节子节点(自循环)
        /// </summary>
        /// <param name="SubProjectId"></param>
        /// <returns></returns>
        public List<SubProjectEntity> GetSonId(string SubProjectId)
        {
            List<SubProjectEntity> list = subprojectbll.GetListWant(f => f.ParentId == SubProjectId);
            var sb = list.SelectMany(p => GetSonId(p.Id));
            return list.Concat(list.SelectMany(t => GetSonId(t.Id))).ToList();
        }
    }
}
