using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Collections.Generic;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 22:03
    /// �� ����ԭ���Ϸ���
    /// </summary>
    public class RawMaterialAnalysisController : MvcControllerBase
    {
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var list = rawmaterialanalysisbll.GetPageList(pagination, queryJson);
            var data = new List<RawMaterialAnalysisModel>();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    var _model = new RawMaterialAnalysisModel();
                    _model.Id = item.Id;
                    _model.RawMaterialCategory = model.Category;
                    _model.RawMaterialStandard = model.RawMaterialModel;
                    _model.RawMaterialDosage = item.RawMaterialDosage;
                    _model.RawMaterialUnit = model.Unit;
                    _model.Description = item.Description;
                    _model.IsSubmitReview = item.IsSubmitReview;
                    _model.IsPassed = item.IsPassed;
                    data.Add(_model);

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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="type">��ѯ����</param>
        /// <returns>�����б�Json</returns>
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
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

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
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
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
            return Success("�����ɹ���");
        }
        
        /// <summary>
        /// �ύ���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ</param>
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
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ��˴���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ</param>
        /// <param name="type">1ͨ����2ʧ��</param>
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
            return Success("�����ɹ���");
        }
        #endregion
    }
}
