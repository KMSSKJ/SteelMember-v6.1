using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Application.Code;
using System;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 16:54
    /// 描 述：构件需求
    /// </summary>
    public class MemberDemandController : MvcControllerBase
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

        [HttpGet]
        public ActionResult Index1()
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


        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotReviewForm()
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
            if (pagination.sidx == "MemberNumbering" || pagination.sidx == "Category")
            {
                pagination.sidx = "MemberId";
            }


            var data = new List<MemberDemandModel>();
            var watch = CommonHelper.TimerStart();
            var HavesChildren = "";
            var SubProjectId = "";
            var queryParam = queryJson.ToJObject();
            if (queryJson != null|| queryJson == "\"MemberName\":\"\",\"Numbering\":\"\",\"SubProjectId\":\"\",\"HavesChildren\":\"\",\"BeginTime\":\"\",\"EndTime\":\"\"")
            {
                if (!queryParam["HavesChildren"].IsEmpty()) { 
                HavesChildren = queryParam["HavesChildren"].ToString();
                SubProjectId = queryParam["SubProjectId"].ToString();
                }
               
            }
            if (HavesChildren == "True")
            {
                var list = GetSonId(SubProjectId);

                foreach (var item1 in list)
                {
                   var E = memberdemandbll.GetPageList1(pagination, f => f.SubProjectId == item1.Id).ToList();
                    if (E.Count() > 0)
                    {
                        foreach (var item in E)
                        {
                            var _model = new MemberDemandModel();

                                var data1 = memberlibrarybll.GetEntity(f => f.MemberId == item.MemberId);

                            _model.Icon = data1.Icon;
                            _model.MemberNumbering = data1.MemberNumbering;
                            _model.Category = dataitemdetailbll.GetEntity(data1.Category).ItemName;
                            _model.UnitId = dataitemdetailbll.GetEntity(data1.UnitId).ItemName;
                            _model.MemberId = data1.MemberId;
                            _model.CollarNumber = item.CollarNumber;
                            _model.CreateMan = item.CreateMan;
                            _model.CreateTime = item.CreateTime;
                            _model.Description = item.Description;
                            _model.SubProjectId = item.SubProjectId;
                            _model.IsSubmit = item.IsSubmit;
                            _model.IsReview = item.IsReview;
                            _model.MemberDemandId= item.MemberDemandId;
                            _model.MemberName= data1.MemberName;
                            _model.MemberNumber = item.MemberNumber;
                            _model.MemberNumbering = data1.MemberNumbering;
                            _model.MemberWeight= data1.MemberWeight;
                            _model.ProductionNumber = item.ProductionNumber;
                            _model.ReviewMan = item.ReviewMan;
                            _model.UnitPrice = data1.UnitPrice;
                         
                            data.Add(_model);
                        }
                    }
                }

                if (data != null)
                {
                    for (var i = 0; i < data.Count; i++)
                    {
                        for (var j = i + 1; j < data.Count; j++)
                        {
                            if (data[i].MemberId == data[j].MemberId)
                            {

                                data[i].MemberNumber = data[i].MemberNumber + data[j].MemberNumber;
                                data.Remove(data[j]);
                            }
                        }
                    }
                }

            }
            else { 
           var memberdemand = memberdemandbll.GetPageList(pagination, queryJson).ToList();
            foreach (var item in memberdemand)
            {
                    var _model = new MemberDemandModel();
                    var data1 = memberlibrarybll.GetEntity(f => f.MemberId == item.MemberId);
                    var SubProject = subprojectbll.GetEntity(item.SubProjectId);
                    _model.Icon = data1.Icon;
                    _model.Category = dataitemdetailbll.GetEntity(data1.Category).ItemName;
                    _model.UnitId = dataitemdetailbll.GetEntity(data1.UnitId).ItemName;
                    _model.MemberId = data1.MemberId;
                    _model.CollarNumber = item.CollarNumber;
                    _model.CreateMan = item.CreateMan;
                    _model.CreateTime = item.CreateTime;
                    _model.Description = item.Description;
                    _model.SubProject = SubProject.FullName;
                    _model.IsSubmit = item.IsSubmit;
                    _model.IsReview = item.IsReview;
                    _model.MemberDemandId = item.MemberDemandId;
                    _model.MemberName = data1.MemberName;
                    _model.MemberNumber = item.MemberNumber;
                    _model.MemberNumbering = data1.MemberNumbering;
                    _model.MemberWeight = data1.MemberWeight;
                    _model.ProductionNumber = item.ProductionNumber;
                    _model.ReviewMan = item.ReviewMan;
                    data.Add(_model);
                }
            }
            //
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                data = data.FindAll(t => t.Category== Category);
            }
           
            if (!queryParam["MemberName"].IsEmpty())
            {
                var MemberName = queryParam["MemberName"].ToString();
                data = data.FindAll(t => t.MemberName.Contains(MemberName));
            }
            if (!queryParam["Numbering"].IsEmpty())
            {
                var Numbering = queryParam["Numbering"].ToString();
                data = data.FindAll(t => t.MemberNumbering.Contains(Numbering));
            }
            //
            var jsonData = new
            {
                rows = data.OrderBy(O=>O.MemberNumbering),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
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

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = memberdemandbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取构件库列表
        /// </summary>
        /// <param name="EngineeringId">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJsonMemberlibrary(string EngineeringId)
        {
            var data = memberlibrarybll.GetList(f=>f.EngineeringId == EngineeringId).OrderBy(o=>o.MemberNumbering);
            var JsonData = data.Select(p => new
            {
                MemberId = p.MemberId,
                MemberNumbering = p.MemberNumbering + "➩" +dataitemdetailbll.GetEntity(p.Category).ItemName + "➩" + p.MemberName,
            });
            return ToJsonResult(JsonData);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = memberdemandbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取构件实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJsonMemberLibrary(string keyValue)
        {
            var data = memberlibrarybll.GetEntity(keyValue);
            data.Category = dataitemdetailbll.GetEntity(data.Category).ItemName;
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
            string[] Arry = keyValue.Split(',');//字符串转数组
            int number = 0;
            foreach (var item in Arry)
            {
                var member = memberdemandbll.GetEntity(f=>f.MemberDemandId== item);
                var memberproductionorderinfo = memberproductionorderinfobll.GetList(f=>f.MemberDemandId== item);
                var memberwarehouserecording = memberwarehouserecordingbll.GetList(f=>f.MemberId== member.MemberId);
                var membercollarinfo = membercollarinfobll.GetList(f=>f.MemberDemandId== item);
                number = memberproductionorderinfo.Count() + memberwarehouserecording.Count() + membercollarinfo.Count();
                if (number== 0)
                {
                    memberdemandbll.RemoveForm(item);
                }
                else {
                    return Error("数据中存在关联数据");
                }
            }
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
        public ActionResult SaveForm(string keyValue, MemberDemandEntity entity)
        {
            entity.IsReview = 0;
            entity.IsSubmit = 0;
            entity.CreateMan = OperatorProvider.Provider.Current().UserName;
            entity.CreateTime = DateTime.Now;
            memberdemandbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 提交需求
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SubmitReview(string keyValues)
        {
            string[] Arry = keyValues.Split(',');//字符串转数组
            foreach (var item in Arry)
            {
                var file = memberdemandbll.GetEntity(f => f.MemberDemandId == item);
                //file.ReviewMan = OperatorProvider.Provider.Current().UserName;
                file.IsSubmit = 1;
                memberdemandbll.SaveForm(item, file);
            }
            return Success("操作成功");
        }

        #region 审核需求
        /// <summary>
        /// 审核需求
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="Entity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReviewMemberDemand(string keyValue, MemberDemandEntity Entity, int type)
        {
            try
            {
                string[] Arry = keyValue.Split(',');//字符串转数组
                string Message = type == 2 ? "驳回成功。" : "审核成功。";
                foreach (var item in Arry)
                {
                    var file = memberdemandbll.GetEntity(f => f.MemberDemandId == item);
                    file.ReviewTime = DateTime.Now;
                    file.ReviewMan = OperatorProvider.Provider.Current().UserName;
                    file.IsReview = type;
                    file.Description = Entity.Description;
                    memberdemandbll.SaveForm(item, file);
                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #endregion

        #region 验证数据
        /// <summary>
        ///构件不能重复
        /// </summary>
        /// <param name="Member"></param>
        /// <param name="SubProject"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMember(string Member, string SubProject)
        { 
                bool IsOk = memberdemandbll.ExistFullName(Member, SubProject);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
