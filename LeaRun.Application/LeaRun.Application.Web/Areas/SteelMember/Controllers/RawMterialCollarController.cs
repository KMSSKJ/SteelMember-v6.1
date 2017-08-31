using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 17:19
    /// 描 述：领用管理
    /// </summary>
    public class RawMterialCollarController : MvcControllerBase
    {
        private RawMterialCollarBLL rawmterialcollarbll = new RawMterialCollarBLL();
        private RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        /// 分页查询出库信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OutInventoryDetailInfo(Pagination pagination,string queryJson, string category)
        {
            List<RawMaterialCollarModel> list = new List<RawMaterialCollarModel>();

            var data = rawmateriallibrarybll.GetPageListByLikeCategory(pagination, category);
            foreach (var item in data)
            {
                var linventory = rawmaterialinventorybll.GetEntityByRawMaterialId(item.RawMaterialId);
                var collarlist = rawmterialcollarbll.GetPageList(pagination, linventory.InventoryId);
                for (var i = 0; i < collarlist.Count; i++)
                {
                    RawMaterialCollarModel rawMaterialCollarModel = new RawMaterialCollarModel();
                    var subproject = subprojectbll.GetEntity(collarlist[i].CollarEngineering);

                    rawMaterialCollarModel.CollarId = collarlist[i].CollarId;
                    rawMaterialCollarModel.InventoryId = collarlist[i].InventoryId;
                    rawMaterialCollarModel.CollarEngineering = subproject.FullName;//取到子工程名 生产领用

                    rawMaterialCollarModel.CollarTime = collarlist[i].CollarTime;
                    rawMaterialCollarModel.CollarQuantity = collarlist[i].CollarQuantity;
                    rawMaterialCollarModel.CollarMan = collarlist[i].CollarMan;
                    rawMaterialCollarModel.Description = collarlist[i].Description;
                    rawMaterialCollarModel.CollarType = collarlist[i].CollarType == 1 ? "生产领用" : "工程领用";

                    //rawMaterialCollarModel.Category = item.Category;
                    rawMaterialCollarModel.RawMaterialName = item.RawMaterialName;
                    rawMaterialCollarModel.RawMaterialModel = item.RawMaterialModel;
                    rawMaterialCollarModel.Unit = item.Unit;

                    list.Add(rawMaterialCollarModel);

                }
            }

            //
            var queryParam = queryJson.ToJObject();
            //查询条件
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.UpdateTime >= BeginTime);
                list = list.FindAll(t => t.UpdateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.UpdateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                list = list.FindAll(t => t.UpdateTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    //case "Category":              //构件类型
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    case "RawMaterialName":              //构件名称
                        list = list.FindAll(t => t.RawMaterialName.Contains(keyword));
                        break;
                    case "RawMaterialModel":              //型号
                        list = list.FindAll(t => t.RawMaterialModel.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            //
            return ToJsonResult(list);
        }
        //public ActionResult OutInventoryDetailInfo(Pagination pagination, string queryJson)
        //{
        //    var data = rawmterialcollarbll.OutInventoryDetailInfo(pagination, queryJson);
        //    List<RawMaterialCollarModel> list = new List<RawMaterialCollarModel>();
        //    foreach (var item in data)
        //    {
        //        RawMaterialCollarModel rawMaterialCollarModel = new RawMaterialCollarModel();
        //        var inventorymodel = rawmaterialinventorybll.GetEntity(item.InventoryId);
        //        var librarymodel = rawmateriallibrarybll.GetEntity(inventorymodel.RawMaterialId);
        //        //取出树的value
        //        var subproject = subprojectbll.GetEntity(item.CollarEngineering);

        //        rawMaterialCollarModel.CollarId = item.CollarId;
        //        rawMaterialCollarModel.InventoryId = item.InventoryId;
        //        rawMaterialCollarModel.CollarType = item.CollarType;
        //        //rawMaterialCollarModel.CollarEngineering = item.CollarEngineering;
        //        rawMaterialCollarModel.CollarEngineering = subproject.FullName;//取到子工程名

        //        rawMaterialCollarModel.CollarTime = item.CollarTime;
        //        rawMaterialCollarModel.CollarQuantity = item.CollarQuantity;
        //        rawMaterialCollarModel.CollarMan = item.CollarMan;
        //        rawMaterialCollarModel.Description = item.Description;

        //        rawMaterialCollarModel.Category = librarymodel.Category;
        //        rawMaterialCollarModel.RawMaterialModel = librarymodel.RawMaterialModel;
        //        rawMaterialCollarModel.RawMaterialStandard = librarymodel.RawMaterialStandard;
        //        rawMaterialCollarModel.Unit = librarymodel.Unit;

        //        list.Add(rawMaterialCollarModel);

        //    }
        //    return ToJsonResult(list);
        //}

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = rawmterialcollarbll.GetList(queryJson);
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
            var data = rawmterialcollarbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 领用入库
        /// </summary>
        /// <param name="collarinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCollarinfo(string collarinfo)
        {
            var collarmodel = collarinfo.ToObject<RawMterialCollarEntity>();
            collarmodel.CollarTime = System.DateTime.Now;
            try
            {
                if (collarmodel.CollarQuantity > 0)
                {
                    //在库存量中减掉领出的数量
                    var inventorymodel = rawmaterialinventorybll.GetEntity(collarmodel.InventoryId);
                    inventorymodel.Quantity = inventorymodel.Quantity - collarmodel.CollarQuantity;//库存--
                    rawmaterialinventorybll.SaveForm(collarmodel.InventoryId, inventorymodel);

                    //添加到领用表中  
                    string keyValue = "";
                    rawmterialcollarbll.SaveForm(keyValue, collarmodel);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return Success("领用成功。");

        }
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
            rawmterialcollarbll.RemoveForm(keyValue);
            return Success("删除成功。");
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
        public ActionResult SaveForm(string keyValue, RawMterialCollarEntity entity)
        {
            rawmterialcollarbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
