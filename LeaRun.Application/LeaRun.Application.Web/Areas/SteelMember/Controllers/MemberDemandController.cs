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
            var watch = CommonHelper.TimerStart();
            //var data = memberdemandbll.GetList(queryJson);
            //var data1 = memberlibrarybll.GetPageList(pagination, queryJson).OrderBy(o=>o.MemberNumbering);
           
            var data = memberdemandbll.GetPageList(pagination, queryJson).OrderBy(o => o.MemberNumbering);
            foreach (var item in data)
            {
                var data1 = memberlibrarybll.GetList(null).ToList().Find(f => f.MemberId == item.MemberId);
                item.MemberModel = data1.MemberModel;
                item.MemberUnit = data1.MemberUnit;
                item.Icon = data1.Icon;
            }

            var jsonData = new
            {
                //rows = data,
                rows = data,
                //.Select(p => new
                //{
                //    Icon = p.Icon,
                //    FileName = p.FileName,
                //    FullPath = p.FullPath,
                //    FileType = p.FileType,
                //    FileSize = p.FileSize,
                //    UploadTime = p.UploadTime,
                //    ModifiedTime = p.ModifiedTime,
                //    OverdueTime = p.OverdueTime,
                //    Status = p.Status,
                //    //temp.Where(r => r.FileID == p.FileID).Count() == 0 ? "" :
                //})
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
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
        /// <param name="SubProjectId">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJsonMemberlibrary(string SubProjectId)
        {
            var data = memberlibrarybll.GetList(null).ToList().FindAll(f=>f.SubProjectId==SubProjectId);
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
    }
}
