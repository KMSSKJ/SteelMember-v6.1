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
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-08-30 11:16
    /// �� ������Ŀ��Ϣ
    /// </summary>
    public class SystemConfigurationController : MvcControllerBase
    {
        #region ��ͼ����
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SystemConfigurationForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = systemconfigurationbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = systemconfigurationbll.GetList(null).SingleOrDefault();
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            systemconfigurationbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(SystemConfigurationEntity entity)
        {
            entity.EngineeringImg = entity.EngineeringImg.Substring(0, entity.EngineeringImg.Length - 1);
            systemconfigurationbll.SaveForm(entity.SystemConfigurationId, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ϴ�/�޸�ϵͳlogo
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            var files = Request.Files;
            //HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //û���ļ��ϴ���ֱ�ӷ���
            if (files.Count == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return Content("1");
            }

            Guid code = GuidComb.GenerateComb();
            string FileEextension = Path.GetExtension(files[0].FileName);
            string virtualPath = string.Format("/Resource/LogoFile/{0}{1}", code, FileEextension);

            string fullFileName = Server.MapPath(virtualPath);
            //�����ļ��У������ļ�
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);

            //UserEntity userEntity = new UserEntity();
            //userEntity.UserId = OperatorProvider.Provider.Current().UserId;
            //userEntity.HeadIcon = virtualPath;
            //userBLL.SaveForm(userEntity.UserId, userEntity);
            return Content(virtualPath);
        }

        public ActionResult abc()
        {
            return View();
        }


        /// <summary>
        /// ͼƬ�ϴ�  [FromBody]string type
        /// ����ͼƬ���֧��200KB
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImgUpload()
        {
            //var result = new List<ImgUploadResult>();
            var result = "";

            // ���������ϴ����ļ���չ�� 
            const string fileTypes = "gif,jpg,jpeg,png,bmp";
            // ����ļ���С(2MB)
            const int maxSize = 1024000*5;
            // ��ȡ����POST����ֵ
            var type = Request["type"];

            for (var fileId = 0; fileId < Request.Files.Count; fileId++)
            {
                var curFile = Request.Files[fileId];
                if (curFile.ContentLength > maxSize)
                {
                    return Content("1");
                }
                var fileExt = Path.GetExtension(curFile.FileName);
                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    return Content("2");
                }
                else
                {
                    Guid code = GuidComb.GenerateComb();
                    // �洢�ļ���
                    string fileName = code + fileExt;

                    // �洢·��������·����
                    string virtualPath = string.Format("/Resource/EngineeringImg/{0}", fileName);
                    string fullFileName = Server.MapPath(virtualPath);
                    try
                    {
                        string path = Path.GetDirectoryName(fullFileName);
                        Directory.CreateDirectory(path);
                        curFile.SaveAs(fullFileName);
                        
                        result= virtualPath;
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

    public class ImgUploadResult
    {
        public string FullFileName { get; set; }
        public string ImgUrl { get; set; }
    }
}
