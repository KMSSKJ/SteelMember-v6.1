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
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util.Offices;
using System.Data;
using LeaRun.Application.Code;
using System.Data.OleDb;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 22:03
    /// �� �������Ϸ���
    /// </summary>
    public class RawMaterialAnalysisController : MvcControllerBase
    {
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index1()
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

        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotReviewForm()
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
            if (pagination.sidx== "RawMaterialName"|| pagination.sidx == "RawMaterialCategory")
            {
                pagination.sidx = "RawMaterialId";
            }
         
            var data = new List<RawMaterialAnalysisModel>();
            var watch = CommonHelper.TimerStart();
            var HavesChildren = "";
            var SubProjectId = "";
            var queryParam = queryJson.ToJObject();
            if (queryJson != null)
            {
                if (!queryParam["SubProjectId"].IsEmpty())
                {
                    HavesChildren = queryParam["HavesChildren"].ToString();
                    SubProjectId = queryParam["SubProjectId"].ToString();
                }
            }
            if (HavesChildren == "True")
            {
                //List<string> SubProjectIds = new List<string>();
                var list = GetSonId(SubProjectId);

                foreach (var item1 in list)
                {
                    //var _model = new RawMaterialAnalysisModel();
                    var E = rawmaterialanalysisbll.GetPageList1(f => f.Category == item1.Id, pagination);
                    if (E.Count > 0)
                    {
                        foreach (var item in E)
                        {
                            var _model = new RawMaterialAnalysisModel();
                            var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                            _model.RawMaterialId = model.RawMaterialId;
                            _model.Id = item.Id;
                            _model.Category = subprojectbll.GetEntity(item.Category).FullName;
                            _model.UpdateTime = item.UpdateTime;
                            _model.RawMaterialCategory = dataitemdetailbll.GetEntity(model.Category).ItemName;
                            _model.RawMaterialName = model.RawMaterialName;
                            _model.RawMaterialModel = model.RawMaterialModel;
                            _model.RawMaterialUnit = dataitemdetailbll.GetEntity(model.Unit).ItemName;
                            _model.Description = item.Description;
                            _model.IsSubmitReview = item.IsSubmitReview;
                            _model.IsPassed = item.IsPassed;
                            _model.RawMaterialDosage = item.RawMaterialDosage;
                            _model.ApplicationPurchasedQuantity = item.ApplicationPurchasedQuantity;
                            _model.PurchasedQuantity = item.PurchasedQuantity;
                            _model.WarehousedQuantity = item.WarehousedQuantity;
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
                            if (data[i].RawMaterialId == data[j].RawMaterialId)
                            {

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
            else
            {
                var list = rawmaterialanalysisbll.GetPageList(pagination, queryJson);
                //var data = new List<RawMaterialAnalysisModel>();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                        var _model = new RawMaterialAnalysisModel();
                        _model.Id = item.Id;
                        _model.UpdateTime = item.UpdateTime;
                        _model.Category = subprojectbll.GetEntity(item.Category).FullName;
                        _model.RawMaterialCategory = dataitemdetailbll.GetEntity(model.Category).ItemName; ;
                        _model.RawMaterialName = model.RawMaterialName;
                        _model.RawMaterialModel = model.RawMaterialModel;
                        _model.RawMaterialDosage = item.RawMaterialDosage;
                        _model.ApplicationPurchasedQuantity = item.ApplicationPurchasedQuantity;
                        _model.PurchasedQuantity = item.PurchasedQuantity;
                        _model.WarehousedQuantity = item.WarehousedQuantity;
                        _model.RawMaterialUnit = dataitemdetailbll.GetEntity(model.Unit).ItemName;
                        _model.Description = item.Description;
                        _model.IsSubmitReview = item.IsSubmitReview;
                        _model.IsPassed = item.IsPassed;
                        data.Add(_model);
                    }
                }
            }

            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                data = data.FindAll(t => t.UpdateTime >= BeginTime);
                data = data.FindAll(t => t.UpdateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                data = data.FindAll(t => t.UpdateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                data = data.FindAll(t => t.UpdateTime <= EndTime);
            }

            //var queryParam = queryJson.ToJObject();
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                data = data.FindAll(t => t.Category == Category);
            }
            if (!queryParam["RawMaterialName"].IsEmpty())
            {
                string RawMaterialName = queryParam["RawMaterialName"].ToString();
                data = data.FindAll(t => t.RawMaterialName.Contains(RawMaterialName));
            }
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString();
                data = data.FindAll(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }
            //

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

        //��ȡ���ֽ��ӽڵ�(��ѭ��)
        public List<SubProjectEntity> GetSonId(string SubProjectId)
        {
            List<SubProjectEntity> list = subprojectbll.GetListWant(f => f.ParentId == SubProjectId);
            var sb = list.SelectMany(p => GetSonId(p.Id));
            return list.Concat(list.SelectMany(t => GetSonId(t.Id))).ToList();
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
                expression = expression.And(r => r.RawMaterialName.Trim() == type.Trim());
            }
            var data = rawmateriallibrarybll.GetList(expression);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="rawmaterianame">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult RawMateriaName(string rawmaterianame)
        {
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            if (!string.IsNullOrEmpty(rawmaterianame))
            {
                expression = expression.And(r => r.Category.Trim() == rawmaterianame.Trim());
            }
            var data = rawmateriallibrarybll.GetList(expression);
            foreach (var item in data)
            {
                item.RawMaterialModel = item.RawMaterialName + item.RawMaterialModel;
            }
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="rawmaterianame">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult RawMateriaName1(string rawmaterianame)
        {
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            
            if (!string.IsNullOrEmpty(rawmaterianame))
            {
                expression = expression.And(r => r.Category.Trim() == rawmaterianame.Trim());
            }
            var data = rawmateriallibrarybll.GetList(expression);
            var JsonData = data.Select(p => new
            {
                str = p.RawMaterialName,
            });
            return ToJsonResult(JsonData.Distinct());
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
            _model.RawMaterialId = data.RawMaterialId;
            _model.RawMaterialCategory = model.Category;
            _model.RawMaterialModel = model.RawMaterialModel;
            _model.RawMaterialName = model.RawMaterialName;
            _model.RawMaterialDosage = data.RawMaterialDosage;
            _model.RawMaterialUnit = model.Unit;
            _model.Description = data.Description;

            return Content(_model.ToJson());
        }

        /// <summary>
        /// ����������ϸ��Excelģ�嵼����
        /// </summary>
        /// <param name="rowJson"></param>
        /// <param name="postData"></param>
        /// <returns></returns>

        [ValidateInput(false)]
        public void OutExcel(string rowJson, string postData)
        {
            var entity = postData.ToObject<ExportPrimaryTableModel>();
            var SubId = GetParentId(entity.SubProjectId);
            entity.SubProjectId = SubId.Where(w => w.ParentId == "0").SingleOrDefault().Id;
            entity.IndividualProjectName = subprojectbll.GetList(null).ToList().Find(f => f.ParentId == entity.SubProjectId).FullName;

            //������
            DataTable rowData = rowJson.ToTable();

            var rawmaterialanalysis = rawmaterialanalysisbll.GetEntity(rowData.Rows[0][10].ToString());

            List<TemplateMode> list = new List<TemplateMode>();
            //����������Ϣ(�ϲ�)
            list.Add(new TemplateMode() { row = 0, cell = 0, value = entity.ExportTableTitle});
            list.Add(new TemplateMode() { row = 1, cell = 0, value = "�������ƣ�"+ subprojectbll.GetEntity(entity.SubProjectId).FullName });
            list.Add(new TemplateMode() { row = 1, cell = 5, value = "��������ƣ�"+ entity.IndividualProjectName });
            list.Add(new TemplateMode() { row = 2, cell = 0, value = "ʩ����λ��" + entity.ConstructionUnit });

            //������ϸ��Ϣ
            for (int r = 0; r < rowData.Rows.Count; r++)
            {
                list.Add(new TemplateMode() { row = r + 4, cell = 1, value = rowData.Rows[r][0].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 2, value = rowData.Rows[r][1].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 3, value = rowData.Rows[r][2].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 4, value = rowData.Rows[r][3].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 5, value = rowData.Rows[r][4].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 6, value = rowData.Rows[r][5].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 7, value = rowData.Rows[r][8].ToString() });
                list.Add(new TemplateMode() { row = r + 4, cell = 8, value = rowData.Rows[r][9].ToString() });
            }

            //����������Ϣ(�²�)
            list.Add(new TemplateMode() { row = 36, cell = 0, value = "����Ա��" + rawmaterialanalysis.CreateMan });
            list.Add(new TemplateMode() { row = 36, cell = 4, value = "����ˣ�" + rawmaterialanalysis.ReviewMan });
            list.Add(new TemplateMode() { row = 36, cell = 7, value = "����:" + DateTime.Now.Year + "��" + DateTime.Now.Month + "��" + DateTime.Now.Day + "��" });

            ExcelHelper.ExcelDownload(list, "���Ϸ���ģ��.xlsx", entity.ExportFileName + ".xlsx");
        }
        /// <summary>
        /// ��ȡ���ֽڸ��ڵ�(��ѭ��)
        /// </summary>
        /// <param name="SubProjectId"></param>
        /// <returns></returns>
        public List<SubProjectEntity> GetParentId(string SubProjectId)
        {
            List<SubProjectEntity> list = subprojectbll.GetListWant(f => f.Id == SubProjectId);
            var sb = list.SelectMany(p => GetParentId(p.ParentId));
            return list.Concat(list.SelectMany(t => GetParentId(t.ParentId))).ToList();
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
       // [HandlerAuthorize(PermissionMode.Enforce)]
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
            var model = new RawMaterialAnalysisEntity();
            model.RawMaterialId = entity.RawMaterialModel;
            model.RawMaterialDosage = entity.RawMaterialDosage;
            model.Category = entity.Category;
            model.CreateMan = OperatorProvider.Provider.Current().UserName;
            model.Description = entity.Description;
            model.IsSubmitReview = 0;
            model.IsPassed = 0;
            if (keyValue == "" || keyValue == null)
            {
                model.UpdateTime = DateTime.Now;
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
        //[HandlerAuthorize(PermissionMode.Enforce)]
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
                        model.ReviewMan = OperatorProvider.Provider.Current().UserName;
                        model.ReviewTime = DateTime.Now;
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
        /// <param name="keyValue">Ҫ��˵����ݵ�����Щ</param>
        /// <param name="entity"></param>
        /// <param name="type">1ͨ����2ʧ��</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
       // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReviewOperation(string keyValue, RawMaterialAnalysisEntity entity, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValue))
            {
                ids = keyValue.Split(',');
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
                        model.Description = entity.Description;
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

        #region ��֤����

        /// <summary>
        /// �������ͺŲ����ظ�
        /// </summary>
        /// <param name="RawMaterialId"></param>
        /// <param name="SubProjectId"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ExistFullName(string RawMaterialId, string SubProjectId, string keyValue)
        {
            bool IsOk = rawmaterialanalysisbll.ExistFullName(RawMaterialId, SubProjectId, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
