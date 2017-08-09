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
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 16:54
    /// �� ������������
    /// </summary>
    public class MemberDemandController : MvcControllerBase
    {
        private MemberDemandBLL memberdemandbll = new MemberDemandBLL();
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();

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
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
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
            var data = new List<MemberDemandEntity>();
            var watch = CommonHelper.TimerStart();
            var HavesChildren = "";
            var SubProjectId = "";
            var queryParam = queryJson.ToJObject();
            if (queryJson != null)
            {
                HavesChildren = queryParam["HavesChildren"].ToString();
                SubProjectId = queryParam["SubProjectId"].ToString();
            }
            if (HavesChildren == "True")
            {
                var list = GetSonId(SubProjectId);

                foreach (var item1 in list)
                {
                   var E = memberdemandbll.GetPageList1(f => f.SubProjectId == item1.Id,pagination).ToList();
                    if (E.Count() > 0)
                    {
                        foreach (var item in E)
                        {
                            var _model = new MemberDemandEntity();

                                var data1 = memberlibrarybll.GetList(null).ToList().Find(f => f.MemberId == item.MemberId);
                            _model.MemberModel = data1.MemberModel;
                            _model.MemberUnit = data1.MemberUnit;
                            _model.Icon = data1.Icon;
                            _model.Category = item.Category;
                            _model.MemberId = item.MemberId;
                            _model.CollarNumbered = item.CollarNumbered;
                            _model.CostBudget = item.CostBudget;
                            _model.CreateMan = item.CreateMan;
                            _model.CreateTime = item.CreateTime;
                            _model.Description = item.Description;
                            _model.EngineeringId = item.EngineeringId;
                            _model.FullName = item.FullName;
                            _model.IsReview = item.IsReview;
                            _model.MemberDemandId= item.MemberDemandId;
                            _model.MemberName= item.MemberName;
                            _model.MemberNumber = item.MemberNumber;
                            _model.MemberNumbering = item.MemberNumbering;
                            _model.MemberWeight= item.MemberWeight;
                            _model.OrderQuantityed = item.OrderQuantityed;
                            _model.Productioned = item.Productioned;
                            _model.ProductionNumber = item.ProductionNumber;
                            _model.ReviewMan = item.ReviewMan;
                            _model.UnitPrice = item.UnitPrice;
                         
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
        /// ��ȡ���ֽ��ӽڵ�(��ѭ��)
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = memberdemandbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡ�������б�
        /// </summary>
        /// <param name="EngineeringId">��ѯ����</param>
        /// <returns>�����б�Json</returns>
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = memberdemandbll.GetEntity(keyValue);
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
        public ActionResult RemoveForm(string keyValue)
        {
            string[] Arry = keyValue.Split(',');//�ַ���ת����
            foreach (var item in Arry)
            {
                memberdemandbll.RemoveForm(item);
            }
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
            return Success("�����ɹ���");
        }

        #region �������

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult ReviewMemberDemand(string keyValue, int type)
        {
            try
            {
                string[] Arry = keyValue.Split(',');//�ַ���ת����
                string Message = type == 2 ? "���سɹ���" : "��˳ɹ���";
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

        #region ��֤����
        /// <summary>
        ///���������ظ�
        /// </summary>
        /// <param name="Member"></param>
        /// <param name="SubProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMember(string Member, string SubProjectId)
        { string[] arry = Member.Split('>');//�ַ���ת����
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
