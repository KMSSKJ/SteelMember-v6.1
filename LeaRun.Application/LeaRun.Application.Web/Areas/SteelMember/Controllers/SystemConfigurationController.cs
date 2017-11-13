using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using System.Web.Mvc;
using System.Linq;
using System;
using LeaRun.Application.Code;
using System.Web;
using System.IO;
using System.Collections.Generic;
using LeaRun.Application.Web.Areas.SteelMember.Models;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class SystemConfigurationController : MvcControllerBase
    {
        //
        // GET: /SteelMember/SystemConfiguration/

        #region 视图功能
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Img()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = systemconfigurationbll.GetList(queryJson);
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
            var data = systemconfigurationbll.GetList(null).SingleOrDefault();
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
            systemconfigurationbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(SystemConfigurationEntity entity)
        {
            if (entity.EngineeringImg != null && entity.EngineeringImg != "")
            {
                entity.EngineeringImg = entity.EngineeringImg.Substring(0, entity.EngineeringImg.Length - 1);
            }
            else
            {
                entity.EngineeringImg = "";
            }
            if (entity.EngineeringOverview == null)
            {
                entity.EngineeringOverview = "";
            }
            try
            {
                var a = systemconfigurationbll.GetList(null).ToList();
                if (a.Count()>0)
                {
                    foreach (var item in a)
                    {
                        systemconfigurationbll.RemoveForm(entity.SystemConfigurationId);
                    }
                }
                systemconfigurationbll.SaveForm(null, entity);

                var model = subprojectbll.GetListWant(s => s.ParentId == "0").SingleOrDefault();
                if (model == null)
                {
                    var _entity = new SubProjectEntity();
                    _entity.ParentId = "0";
                    _entity.FullName = entity.EngineeringName;
                    _entity.Levels = 1;
                    subprojectbll.SaveForm("", _entity);
                }
                else
                {
                    model.FullName = entity.EngineeringName;
                    subprojectbll.SaveForm(model.Id, model);
                }
                return Success("操作成功!");
            }
            catch (Exception)
            {
                return Error("操作失败!");
            }
        }

        /// <summary>
        /// 上传/修改系统logo
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            var files = Request.Files;
            //HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files.Count == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return Content("1");
            }
            if (files[0].ContentLength > 200000)
            {
                return Content("2");
            }
            Guid code = GuidComb.GenerateComb();
            string FileEextension = Path.GetExtension(files[0].FileName);
            string virtualPath = string.Format("/Resource/LogoFile/{0}{1}", code, FileEextension);

            string fullFileName = Server.MapPath(virtualPath);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);

            //UserEntity userEntity = new UserEntity();
            //userEntity.UserId = OperatorProvider.Provider.Current().UserId;
            //userEntity.HeadIcon = virtualPath;
            //userBLL.SaveForm(userEntity.UserId, userEntity);
            return Content(virtualPath);
        }

        /// <summary>
        /// 图片上传  [FromBody]string type
        /// 单个图片最大支持200KB
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImgUpload()
        {
            //var result = new List<ImgUploadResult>();
            var result = "";

            // 定义允许上传的文件扩展名 
            const string fileTypes = "gif,jpg,jpeg,png,bmp";
            // 最大文件大小(200KB)
            const int maxSize = 1024000 / 5;
            // 获取附带POST参数值
            var type = Request["type"];

            for (var fileId = 0; fileId < Request.Files.Count; fileId++)
            {
                var curFile = Request.Files[fileId];

                var fileExt = Path.GetExtension(curFile.FileName);
                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    return Content("2");
                }
                else
                {
                    if (curFile.ContentLength > maxSize)
                    {
                        return Content("1");
                    }
                    Guid code = GuidComb.GenerateComb();
                    // 存储文件名
                    string fileName = code + fileExt;

                    // 存储路径（绝对路径）
                    string virtualPath = string.Format("/Resource/EngineeringImg/{0}", fileName);
                    string fullFileName = Server.MapPath(virtualPath);
                    try
                    {
                        string path = Path.GetDirectoryName(fullFileName);
                        Directory.CreateDirectory(path);
                        curFile.SaveAs(fullFileName);

                        result = virtualPath;
                        //result.Add(new ImgUploadResult()
                        //{
                        //    FullFileName = fileName,
                        //    ImgUrl = virtualPath
                        //});
                    }
                    catch (Exception)
                    {
                        return Content("3");
                    }
                }
            }
            return Content(result);
        }

        #endregion
    }
}
