using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Collections.Generic;
using LeaRun.Util.Extension;
using System.Linq;
using System;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-06 22:03
    /// 描 述：原材料分析
    /// </summary>
    public class RawMaterialAnalysisController : MvcControllerBase
    {
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var data = new List<RawMaterialAnalysisModel>();
            var watch = CommonHelper.TimerStart();
            var HavesChildren = "";
            var SubProjectId = "";
            var queryParam = queryJson.ToJObject();
            if (queryJson != null) {
             HavesChildren = queryParam["HavesChildren"].ToString();
             SubProjectId = queryParam["category"].ToString();
            }
            if (HavesChildren=="True")
            {
                //List<string> SubProjectIds = new List<string>();
                var list = GetSonId(SubProjectId);
                
                foreach (var item1 in list)
                {
                    //var _model = new RawMaterialAnalysisModel();
                    var E= rawmaterialanalysisbll.GetPageList1(f=>f.Category==item1.Id, pagination);
                    if (E.Count > 0)
                    {
                        foreach (var item in E)
                        {
                            var _model = new RawMaterialAnalysisModel();
                            var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                            _model.RawMaterialId = model.RawMaterialId;
                            _model.Id = item.Id;

                            //_model.RawMaterialCategory = model.Category; 
                            _model.RawMaterialCategory = model.RawMaterialName;
                            _model.RawMaterialStandard = model.RawMaterialModel;
                            _model.RawMaterialUnit = model.Unit;
                            _model.Description = item.Description;
                            _model.IsSubmitReview = item.IsSubmitReview;
                            _model.IsPassed = item.IsPassed;
                            _model.RawMaterialDosage = item.RawMaterialDosage;
                            data.Add(_model);
                            //var a = data.Where(w => w.RawMaterialId == item.RawMaterialId).ToList();
                            //if (a.Count()==0)
                            //{
                            //    data.Add(_model);
                            //}
                            //else
                            //{
                            //    // _model.RawMaterialDosage =_model.RawMaterialDosage+item.RawMaterialDosage;
                            //    _model.RawMaterialDosage = data.RawMaterialDosage + item.RawMaterialDosage;
                            //}

                        }
                    }
                }

                if (data!=null)
                {
                    for (var i=0;i<data.Count;i++) {
                        for (var j=i+1;j<data.Count;j++)
                        {
                            if (data[i].RawMaterialId == data[j].RawMaterialId){

                                data[i].RawMaterialDosage = data[i].RawMaterialDosage + data[j].RawMaterialDosage;
                                data.Remove(data[j]);
                            }
                        }
                    }
                    var Data = new
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };

                    return ToJsonResult(Data);
                }

            }
            else {
            var list = rawmaterialanalysisbll.GetPageList(pagination, queryJson);
            //var data = new List<RawMaterialAnalysisModel>();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    var _model = new RawMaterialAnalysisModel();
                    _model.Id = item.Id;
                     //_model.RawMaterialCategory = model.Category;
                    _model.RawMaterialCategory = model.RawMaterialName;
                    _model.RawMaterialStandard = model.RawMaterialModel;
                    _model.RawMaterialDosage = item.RawMaterialDosage;
                    _model.RawMaterialUnit = model.Unit;
                    _model.Description = item.Description;
                    _model.IsSubmitReview = item.IsSubmitReview;
                    _model.IsPassed = item.IsPassed;
                    data.Add(_model);


                }
            }
            }
            var jsonData = new
        {
            rows = data,
            total = pagination.total,
            page = pagination.page,
            records = pagination.records,
            costtime = CommonHelper.TimerEnd(watch)
        };
          
            return ToJsonResult(jsonData);
        }

        //获取树字节子节点(自循环)
        public List<SubProjectEntity> GetSonId(string SubProjectId)
        {
            List<SubProjectEntity> list = subprojectbll.GetListWant(f=>f.ParentId== SubProjectId);
            var sb = list.SelectMany(p => GetSonId(p.Id));
            return list.Concat(list.SelectMany(t => GetSonId(t.Id))).ToList();
        }
        
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="type">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult RawMateriaType(string type)
        {
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            if (!string.IsNullOrEmpty(type))
            {
                expression = expression.And(r => r.Category.Trim() == type.Trim());
            }
            var data = rawmateriallibrarybll.GetList(expression);
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
            var data = rawmaterialanalysisbll.GetEntity(keyValue);
            var model = rawmateriallibrarybll.GetEntity(data.RawMaterialId);

            var _model = new RawMaterialAnalysisModel();
            _model.Category = data.Category;
            _model.RawMaterialCategory = model.Category;
            _model.RawMaterialStandard = model.RawMaterialModel;
            _model.RawMaterialDosage = data.RawMaterialDosage;
            _model.RawMaterialUnit = model.Unit;
            _model.Description = data.Description;

            return Content(_model.ToJson());
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
            string[] idsArr = keyValue.Split(',');
            List<RawMaterialAnalysisEntity> List = new List<RawMaterialAnalysisEntity>();
            foreach (var item in idsArr)
            {
                var model = rawmaterialanalysisbll.GetEntity(item);
                List.Add(model);
            }
            rawmaterialanalysisbll.RemoveList(List);
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
        public ActionResult SaveForm(string keyValue, RawMaterialAnalysisModel entity)
        {
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            if (!string.IsNullOrEmpty(entity.RawMaterialCategory))
            {
                expression = expression.And(r => r.Category.Trim() == entity.RawMaterialCategory.Trim());
            }
            if (!string.IsNullOrEmpty(entity.RawMaterialStandard))
            {
                expression = expression.And(r => r.RawMaterialModel.Trim() == entity.RawMaterialStandard.Trim());
            }
            var data = rawmateriallibrarybll.GetList(expression)[0];
            var model = new RawMaterialAnalysisEntity();
            model.RawMaterialId = data.RawMaterialId;
            model.RawMaterialDosage = entity.RawMaterialDosage;
            model.Category = entity.Category;
            model.Description = entity.Description;
            if (keyValue == "" || keyValue == null)
            {
                model.IsSubmitReview = 0;
                model.IsPassed = 0;
            }
            rawmaterialanalysisbll.SaveForm(keyValue, model);
            return Success("操作成功。");
        }
        
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitReview(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialAnalysisEntity> list = new List<RawMaterialAnalysisEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialanalysisbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmitReview = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialanalysisbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 审核处理
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些</param>
        /// <param name="type">1通过，2失败</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReviewOperation(string keyValues,int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialAnalysisEntity> list = new List<RawMaterialAnalysisEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialanalysisbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsPassed = type;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialanalysisbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }
        #endregion
    }
}
