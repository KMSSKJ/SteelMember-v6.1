using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-26 16:54
    /// 描 述：构件需求
    /// </summary>
    public class MemberDemandController : MvcControllerBase
    {
        private MemberDemandBLL memberdemandbll = new MemberDemandBLL();
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();

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
        [HandlerAuthorize(PermissionMode.Enforce)]
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
            var data = new List<MemberDemandEntity>();
            var watch = CommonHelper.TimerStart();
            var HavesChildren = "";
            var SubProjectId = "";
            var queryParam = queryJson.ToJObject();
            if (queryJson != null)
            {
                HavesChildren = queryParam["HavesChildren"].ToString();
                SubProjectId = queryParam["category"].ToString();
            }
            if (HavesChildren == "True")
            {
                var list = GetSonId(SubProjectId);

                foreach (var item1 in list)
                {
                    data = memberdemandbll.GetPageList1(pagination, f => f.SubProjectId == item1.Id).ToList();
                    //if (E.Count()> 0)
                    //{
                    //    foreach (var item in E)
                    //    {
                    //        var _model = new RawMaterialAnalysisModel();
                    //        var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    //        _model.RawMaterialId = model.RawMaterialId;
                    //        _model.Id = item.Id;
                    //        _model.RawMaterialCategory = model.Category;
                    //        _model.RawMaterialStandard = model.RawMaterialModel;
                    //        _model.RawMaterialUnit = model.Unit;
                    //        _model.Description = item.Description;
                    //        _model.IsSubmitReview = item.IsSubmitReview;
                    //        _model.IsPassed = item.IsPassed;
                    //        _model.RawMaterialDosage = item.RawMaterialDosage;
                    //        data.Add(_model);
                    //    }
                    //}
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
            data = memberdemandbll.GetPageList(pagination, queryJson).OrderBy(o => o.MemberNumbering).ToList();
            foreach (var item in data)
            {
                var data1 = memberlibrarybll.GetList(null).ToList().Find(f => f.MemberId == item.MemberId);
                item.MemberModel = data1.MemberModel;
                item.MemberUnit = data1.MemberUnit;
                item.Icon = data1.Icon;
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
            var data = memberlibrarybll.GetList(null).ToList().FindAll(f=>f.EngineeringId == EngineeringId);
            var JsonData = data.Select(p => new
            {
                MemberId = p.MemberId,
                MemberNumbering = p.MemberNumbering + ">" + p.Category + ">" + p.MemberName + ">" + p.MemberModel,
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
            string[] Arry = keyValue.Split(',');//字符串转数组
            foreach (var item in Arry)
            {
                memberdemandbll.RemoveForm(item);
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
            string[] Member = entity.MemberNumbering.Split('>');
            for (int i = 0; i < 1; i++)
            {
                entity.MemberNumbering = Member[i];
                entity.Category = Member[i+1];
                entity.MemberName= Member[i+2];
                entity.MemberModel = Member[i + 3];
            }
            entity.IsReview = 0;
            entity.CreateMan = OperatorProvider.Provider.Current().UserName;
            entity.CreateTime = DateTime.Now;
            memberdemandbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #region 审核需求

        /// <summary>
        /// 审核需求
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult ReviewMemberDemand(string keyValue, int type)
        {
            try
            {
                string[] Arry = keyValue.Split(',');//字符串转数组
                string Message = type == 2 ? "驳回成功。" : "审核成功。";
                foreach (var item in Arry)
                {
                    var file = memberdemandbll.GetList(null).ToList().Find(f => f.MemberDemandId == item);
                    file.ModifiedTime = DateTime.Now;
                    file.ReviewMan = OperatorProvider.Provider.Current().UserName;
                    file.IsReview = type;
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
        /// <param name="SubProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMember(string Member, string SubProjectId)
        { string[] arry = Member.Split('>');//字符串转数组
            var MemberNumbering = "";
            for (int i = 0; i < arry.Length; i++)
            {
                MemberNumbering = arry[0];
            }
                bool IsOk = memberdemandbll.ExistFullName(MemberNumbering.Trim(), SubProjectId);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
