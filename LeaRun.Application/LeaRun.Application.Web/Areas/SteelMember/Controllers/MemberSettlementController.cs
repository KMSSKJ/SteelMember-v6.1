using LeaRun.Entity.SteelMember;
using LeaRun.Repository.SteelMember.IBLL;
using LeaRun.Utilities;
using LeaRun.WebApp.Controllers;
using Ninject;
using SteelMember.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class MemberSettlementController : MvcControllerBase
    {
        //
        // GET: /SteelMember/MemberSettlement/
        [Inject]
        public FileIBLL MemberLibraryCurrent { get; set; }
        [Inject]
        public CollarMemberIBLL CollarMemberCurrent { get; set; }

        [Inject]
        public MemberMaterialIBLL MemberMaterialCurrent { get; set; }
        [Inject]
        public RawMaterialIBLL RawMaterialCurrent { get; set; }

        [Inject]
        public ProjectManagementIBLL ProjectManagementCurrent { get; set; }

        [Inject]
        public CollarIBLL CollarManagementCurrent { get; set; }
        [Inject]
        public TreeIBLL TreeCurrent { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QueryPage()
        {
            return View();
        }
        public ActionResult GridListJson(FileViewModel model, string TreeId, Pagination jqgridparam, string IsPublic, string ParameterJson)
        {
            try
            {
                #region 查询条件拼接
                if (ParameterJson != null)
                {
                    if (ParameterJson != "[{\"CollarNumbering\":\"\",\"InBeginTime\":\"\",\"InEndTime\":\"\"}]")
                    {
                        List<FileViewModel> query_member = JsonHelper.JonsToList<FileViewModel>(ParameterJson);
                        for (int i = 0; i < query_member.Count(); i++)
                        {
                            model.CollarNumbering = query_member[i].CollarNumbering;
                            model.InBeginTime = query_member[i].InBeginTime;
                            model.InEndTime = query_member[i].InEndTime;
                        }
                    }
                }
                Expression<Func<RMC_Collar, bool>> func = ExpressionExtensions.True<RMC_Collar>();
                Func<RMC_Collar, bool> func1 = f => f.TreeId != 0;

                var _a = model.CollarNumbering != null && model.CollarNumbering.ToString() != "";
                var _b = model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00";
                var _c = model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00";

                if (_a && _b && _c)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime;
                }
                else if (_a && !_b &&! _c)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering));
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering);
                }
                else if (_b && !_c && !_a)
                {
                    func = func.And(f => f.CollarTime >= model.InBeginTime);
                    func1 = f => f.CollarTime >= model.InBeginTime;
                }
                else if (_c && !_b && !_a)
                {
                    func = func.And(f => f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarTime <= model.InEndTime;
                }
                else if (_a && _b && !_c)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime);
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime;
                }
                else if (_a && _c && !_b)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime;
                }
                else if (_b && _c && !_a)
                {
                    func = func.And(f => f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime;
                }
                #endregion

                var MemberList_ = new List<RMC_Collar>();
                var projectdemandlist = new List<ProjectDemandModel>();

                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                List<RMC_Collar> MemberList = new List<RMC_Collar>();
                if (TreeId == "")
                {
                    func.And(f => f.CollarId> 0);
                    MemberList = MemberList_ = CollarManagementCurrent.FindPage<string>(jqgridparam.page
                                             , jqgridparam.rows
                                             , func
                                             , false
                                             , f => f.CollarTime.ToString()
                                             , out total
                                             ).ToList();
                }
                else
                {
                    int _id = Convert.ToInt32(TreeId);
                    var list = GetSonId(_id).ToList();

                    list.Add(TreeCurrent.Find(p => p.TreeID == _id).Single());

                    foreach (var item in list)
                    {
                        var _MemberList = CollarManagementCurrent.Find(m => m.TreeId == item.TreeID).ToList();
                        if (_MemberList.Count() > 0)
                        {
                            MemberList = MemberList.Concat(_MemberList).ToList();
                        }
                    }

                    MemberList = MemberList.Where(func1).ToList();
                    MemberList_ = MemberList.Take(jqgridparam.rows * jqgridparam.page).Skip(jqgridparam.rows * (jqgridparam.page - 1)).ToList();
                    total = MemberList.Count();
                }

                foreach (var item in MemberList_)
                {
                    ProjectDemandModel projectdemand = new ProjectDemandModel();
                    var CollarMember = CollarMemberCurrent.Find(f => f.CollarId == item.CollarId).ToList();
                    int Number = 0;
                    int UnitPrice = 0;
                    int CostBudget = 0;
                    int MemberId = 0;
                    int ProjectDemandId = 0;
                    var CollarNumbering = "";
                    for (int i = 0; i < CollarMember.Count(); i++)
                    {
                        CollarNumbering = CollarMember[i].CollarNumbering;
                        MemberId = Convert.ToInt32(CollarMember[i].MemberId);
                        ProjectDemandId = Convert.ToInt32(CollarMember[i].ProjectDemandId);

                        var MemberMaterial = MemberMaterialCurrent.Find(f => f.MemberId == MemberId).ToList();
                        for (int i0 = 0; i0 < MemberMaterial.Count(); i0++)
                        {
                            //Numbers = MemberMaterial[i0].MaterialNumber;
                            int RawMaterialId = Convert.ToInt32(MemberMaterial[i0].RawMaterialId);
                            var Material = RawMaterialCurrent.Find(f => f.RawMaterialId == RawMaterialId).SingleOrDefault();
                            CostBudget += Convert.ToInt32(MemberMaterial[i0].MaterialNumber) * Convert.ToInt32(Material.UnitPrice) * Convert.ToInt32(CollarMember[i].Qty);
                            UnitPrice += Convert.ToInt32(MemberMaterial[i0].MaterialNumber) * Convert.ToInt32(Material.UnitPrice);
                        }
                        Number += Convert.ToInt32(CollarMember[i].Qty);
                    }
                    var Member = MemberLibraryCurrent.Find(f => f.MemberID == MemberId).SingleOrDefault();
                    var ProjectDemand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == ProjectDemandId).SingleOrDefault();
                    projectdemand.CollarId = item.CollarId;
                    projectdemand.CollarNumbering = CollarNumbering;
                    projectdemand.MemberName = Member.MemberName;
                    projectdemand.UnitPrice = UnitPrice.ToString();
                    projectdemand.LeaderNumber = Number;
                    projectdemand.CostBudget = CostBudget.ToString();
                    projectdemand.LeaderTime = item.CollarTime;
                    projectdemand.CreateMan = ProjectDemand.CreateMan;
                    projectdemand.ReviewMan =OperatorProvider.Provider.Current().UserName;
                    projectdemand.Description = item.Description;
                    projectdemandlist.Add(projectdemand);
                }

                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = projectdemandlist.OrderByDescending(f => f.CollarTime),
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        //获取树字节子节点(自循环)
        public IEnumerable<RMC_Tree> GetSonId(int p_id)
        {
            List<RMC_Tree> list = TreeCurrent.Find(p => p.ParentID == p_id).ToList();
            return list.Concat(list.SelectMany(t => GetSonId(t.TreeID)));
        }

    }
}
