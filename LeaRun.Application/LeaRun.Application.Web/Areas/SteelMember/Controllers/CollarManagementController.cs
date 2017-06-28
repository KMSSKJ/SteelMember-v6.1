using LeaRun.Application.Code;
using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using LeaRun.Data.Entity;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Ninject;
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
    public class CollarManagementController : MvcControllerBase
    {
        //
        // GET: /SteelMember/MemberSettlement/
        [Inject]
        public CollarIBLL CollarManagementCurrent { get; set; }
        [Inject]
        public CollarMemberIBLL CollarMemberCurrent { get; set; }
        [Inject]
        public MemberMaterialIBLL MemberMaterialCurrent { get; set; }
        [Inject]
        public ProjectManagementIBLL ProjectManagementCurrent { get; set; }
        [Inject]
        public ProjectWarehouseIBLL ProjectWarehouseCurrent { get; set; }
        [Inject]
        public RawMaterialIBLL RawMaterialCurrent { get; set; }
        [Inject]
        public FileIBLL MemberLibraryCurrent { get; set; }

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

        public ActionResult ItemList()
        {
            return View();
        }

        /// <summary>
        /// 领用单表单
        /// </summary>
        /// <returns></returns>
        public ActionResult CollarForm(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.CollarNumbering = "LYD" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
                ViewData["Librarian"] = OperatorProvider.Provider.Current().UserName;
            }
            return View();
        }

        public ActionResult DetailForm()
        {
            return View();
        }

        /// <summary>
        /// 【项目信息管理】返回文件夹对象JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public ActionResult SetDataForm(string KeyValue)
        {
            int CollarId = Convert.ToInt32(KeyValue);
            RMC_Collar entity = CollarManagementCurrent.Find(f => f.CollarId == CollarId).SingleOrDefault();
            return Content(entity.ToJson());
            //return Json(entity);
        }


        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SubmitCollarForm(RMC_Collar entity, string KeyValue, string TreeId, string POOrderEntryJson)
        {
            try
            {
                int IsOk = 0;
                string Message = KeyValue == "" ? "新增成功。" : "变更成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int _CollarId = Convert.ToInt32(KeyValue);
                    RMC_Collar Oldentity = CollarManagementCurrent.Find(f => f.CollarId == _CollarId).SingleOrDefault();
                    Oldentity.Borrow = entity.Borrow;
                    Oldentity.CollarMan =entity.CollarMan;
                    Oldentity.CollarTime= entity.CollarTime;
                    Oldentity.Description = entity.Description;

                    List<int> Ids = new List<int>();
                    List<RMC_CollarMember> CollarMemberList = CollarMemberCurrent.Find(f => f.CollarId == _CollarId).ToList();
                    for (int i = 0; i < CollarMemberList.Count(); i++)
                    {
                        int CollarMemberId = CollarMemberList[i].CollarMemberId;
                        Ids.Add(CollarMemberId);
                        int ProjectDemandId = Convert.ToInt32(CollarMemberList[i].ProjectDemandId);
                        var CollarMember = CollarMemberCurrent.Find(f => f.CollarMemberId == CollarMemberId).SingleOrDefault();
                        var ProjectDemand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == ProjectDemandId).SingleOrDefault();
                        ProjectDemand.CollarNumbered = ProjectDemand.CollarNumbered - CollarMember.Qty;
                        ProjectManagementCurrent.Modified(ProjectDemand);
                    }
                    CollarMemberCurrent.Remove(Ids);

                    //构件单
                    List<CollarModel> POOrderEntryList =LeaRun.Util.Json.ToList<CollarModel>(POOrderEntryJson);
                    //int index = 1;
                    foreach (CollarModel poorderentry in POOrderEntryList)
                    {
                        if (!string.IsNullOrEmpty(poorderentry.MemberNumbering))
                        {
                            RMC_CollarMember CollarModel = new RMC_CollarMember();
                            CollarModel.CollarId = _CollarId;
                            CollarModel.ProjectDemandId = Convert.ToInt32(poorderentry.ProjectDemandId);
                            CollarModel.MemberId = Convert.ToInt32(poorderentry.MemberID);
                            CollarModel.Description = poorderentry.Description;
                            CollarModel.MemberNumbering = poorderentry.MemberNumbering;
                            CollarModel.MemberModel = poorderentry.MemberModel;
                            CollarModel.MemberName = poorderentry.MemberName;
                            CollarModel.MemberUnit = poorderentry.MemberUnit;
                            //CollarModel.Price = Convert.ToDecimal(poorderentry.Price);
                            //CollarModel.PriceAmount = Convert.ToDecimal(poorderentry.PriceAmount);
                            CollarModel.Qty = Convert.ToInt32(poorderentry.Qty);


                            var ProjectDemand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == CollarModel.ProjectDemandId).SingleOrDefault();
                            ProjectDemand.OrderQuantityed = ProjectDemand.CollarNumbered+ Convert.ToInt32(poorderentry.Qty);
                            ProjectManagementCurrent.Modified(ProjectDemand);

                            CollarMemberCurrent.Add(CollarModel);
                            //index++;
                        }
                    }

                }
                else
                {
                    int _TreeId = Convert.ToInt32(TreeId);
                    RMC_Collar Oldentity = new RMC_Collar();
                    Oldentity.TreeId = _TreeId;
                    Oldentity.Borrow = entity.Borrow;
                    Oldentity.CollarNumbering = entity.CollarNumbering;
                    Oldentity.Use = entity.Use;
                    Oldentity.Librarian = entity.Librarian;
                    Oldentity.CollarMan = entity.CollarMan;
                    Oldentity.CollarTime = entity.CollarTime;
                    Oldentity.Description = entity.Description;
                    int CollarId = CollarManagementCurrent.Add(Oldentity).CollarId;

                    RMC_CollarMember CollarModel = new RMC_CollarMember();
                    List<CollarModel> POOrderEntryList = LeaRun.Util.Json.ToList<CollarModel>(POOrderEntryJson);
                    int index = 1;
                    foreach (CollarModel poorderentry in POOrderEntryList)
                    {
                        if (!string.IsNullOrEmpty(poorderentry.MemberNumbering))
                        {
                            //poorderentry.SortCode = index;
                            //poorderentry.Create();
                            CollarModel.CollarId = CollarId;
                            CollarModel.CollarNumbering = entity.CollarNumbering;
                            CollarModel.ProjectDemandId = Convert.ToInt32(poorderentry.ProjectDemandId);
                            CollarModel.MemberId = Convert.ToInt32(poorderentry.MemberID);
                            CollarModel.Description = poorderentry.Description;
                            CollarModel.MemberNumbering = poorderentry.MemberNumbering;
                            CollarModel.MemberModel = poorderentry.MemberModel;
                            CollarModel.MemberName = poorderentry.MemberName;
                            CollarModel.MemberUnit = poorderentry.MemberUnit;
                            //CollarModel.Price = Convert.ToDecimal(poorderentry.Price);
                            //CollarModel.PriceAmount = Convert.ToDecimal(poorderentry.PriceAmount);
                            CollarModel.Qty = Convert.ToInt32(poorderentry.Qty);

                            var Demand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == CollarModel.ProjectDemandId).SingleOrDefault();
                            Demand.OrderQuantityed = Demand.CollarNumbered + CollarModel.Qty;
                            ProjectManagementCurrent.Modified(Demand);

                            CollarMemberCurrent.Add(CollarModel);
                            index++;
                        }
                    }
                }
                IsOk = 1;
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 【工程项目管理】返回文件（夹）列表JSON
        /// </summary>
        /// <param name="keywords">文件名搜索条件</param>
        /// <param name="FolderId">文件夹ID</param>
        /// <param name="IsPublic">是否公共 1-公共、0-我的</param>
        /// <returns></returns>         
        public ActionResult GridListJson(FileViewModel model, string TreeId, Pagination jqgridparam, string IsPublic, string ParameterJson)
        {
            try
            {
                #region 查询条件拼接
                if (ParameterJson != null)
                {
                    if (ParameterJson != "[{\"CollarNumbering\":\"\",\"InBeginTime\":\"\",\"InEndTime\":\"\",\"Class\":\"\"}]")
                    {
                        List<FileViewModel> query_member =LeaRun.Util.Json.ToList<FileViewModel>(ParameterJson);
                        for (int i = 0; i < query_member.Count(); i++)
                        {
                            model.CollarNumbering = query_member[i].CollarNumbering;
                            model.InBeginTime = query_member[i].InBeginTime;
                            model.InEndTime = query_member[i].InEndTime;
                            model.Class = query_member[i].Class;
                        }
                    }
                }

                Expression<Func<RMC_Collar, bool>> func = ExpressionExtensions.True<RMC_Collar>();
                Func<RMC_Collar, bool> func1 = f => f.TreeId != 0;

                var _a = model.CollarNumbering != null && model.CollarNumbering.ToString() != "";
                var _b = model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00";
                var _c = model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00";
                var _d = model.Class != null && model.Class.ToString() != "";

                if (_a && _b && _c && _d)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.Use.Contains(model.Class) && f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime);
                      func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.Use.Contains(model.Class) && f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime;
                }
                else if (_a&&!_b && !_c && !_d)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering));
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering);
                }
                else if (_b && !_a && !_c && !_d)
                {
                    func = func.And(f => f.CollarTime >= model.InBeginTime);
                    func1 = f => f.CollarTime >= model.InBeginTime;
                }
                else if (_c && !_b && !_a && !_d)
                {
                    func = func.And(f => f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarTime <= model.InEndTime;
                }
                else if (_d && !_b && !_c && !_a)
                {
                    func = func.And(f => f.Use.Contains(model.Class));
                    func1 = f => f.Use.Contains(model.Class);
                }
                else if (_a && _b&& !_c && !_d)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime);
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime;
                }
                else if (_a && _c && !_b && !_d)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime;
                }
                else if (_a && _d && !_b && !_c)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.Use.Contains(model.Class));
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.Use.Contains(model.Class);
                }
                else if (_b && _c && !_a && !_d)
                {
                    func = func.And(f => f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime);
                    func1 = f => f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime;
                }
                else if (_b && _d && !_a && !_c)
                {
                    func = func.And(f => f.CollarTime >= model.InBeginTime && f.Use.Contains(model.Class));
                    func1 = f => f.CollarTime >= model.InBeginTime && f.Use.Contains(model.Class);
                }
                else if (_c && _d && !_b && !_a)
                {
                    func = func.And(f => f.CollarTime <= model.InEndTime && f.Use.Contains(model.Class));
                    func1 = f => f.CollarTime <= model.InEndTime && f.Use.Contains(model.Class);
                }
                else if (_a && _b && _c && !_d)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime);
                      func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime && f.CollarTime <= model.InEndTime;
                }
                else if (_a && _b && _d && !_c)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime && f.Use.Contains(model.Class));
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime >= model.InBeginTime && f.Use.Contains(model.Class);
                }
                else if (_a && _c && _d && !_b)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime && f.Use.Contains(model.Class));
                    func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime && f.Use.Contains(model.Class);
                }
                else if (_b && _c && _d && !_a)
                {
                    func = func.And(f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime && f.Use.Contains(model.Class));
                      func1 = f => f.CollarNumbering.Contains(model.CollarNumbering) && f.CollarTime <= model.InEndTime && f.Use.Contains(model.Class);
                }
                #endregion

                var CollarList_ = new List<RMC_Collar>();
                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                List<RMC_Collar> CollarList = new List<RMC_Collar>();
                if (TreeId == "")
                {
                    func.And(f => f.CollarId > 0);

                    CollarList = CollarList_ = CollarManagementCurrent.FindPage<string>(jqgridparam.page
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

                    var list = GetSonId(TreeId).ToList();

                    list.Add(TreeCurrent.Find(p => p.TreeID == TreeId).Single());

                    foreach (var item in list)
                    {
                        var _MemberList = CollarManagementCurrent.Find(m => m.TreeId.ToString() == item.TreeID).ToList();
                        if (_MemberList.Count() > 0)
                        {
                            CollarList = CollarList.Concat(_MemberList).ToList();
                        }

                    }

                    CollarList = CollarList.Where(func1).ToList();
                    CollarList_ = CollarList.Take(jqgridparam.rows * jqgridparam.page).Skip(jqgridparam.rows * (jqgridparam.page - 1)).ToList();

                    total = CollarList.Count();
                }

                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = CollarList_.OrderByDescending(f => f.CollarTime),

                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        //获取树字节子节点(自循环)
        public IEnumerable<RMC_Tree> GetSonId(string p_id)
        {
            List<RMC_Tree> list = TreeCurrent.Find(p => p.ParentID == p_id).ToList();
            return list.Concat(list.SelectMany(t => GetSonId(t.TreeID)));
        }


        /// <summary>
        /// 订单明细列表（返回Json）
        /// </summary>
        /// <param name="POOrderId">订单主键</param>
        /// <returns></returns>
        public ActionResult GetCollarEntryList(string CollarId)
        {
            int _CollarId = Convert.ToInt32(CollarId);
            try
            {
                List<CollarModel> CollarModelList = new List<CollarModel>();
                var listfile = CollarMemberCurrent.Find(f => f.CollarId == _CollarId).ToList();
                foreach (var item in listfile)
                {
                    CollarModel CollarModel = new CollarModel();
                    CollarModel.ProjectDemandId = item.ProjectDemandId.ToString();
                    CollarModel.MemberID = item.MemberId.ToString();
                    CollarModel.MemberNumbering = item.MemberNumbering;
                    CollarModel.MemberName = item.MemberName;
                    CollarModel.MemberModel = item.MemberModel;
                    CollarModel.MemberUnit = item.MemberUnit;
                    CollarModel.Qty = item.Qty.ToString();
                    //OrderModel.Price = item.Price.ToString();
                    //OrderModel.PriceAmount = item.PriceAmount.ToString();
                    CollarModel.Description = item.Description;
                    CollarModelList.Add(CollarModel);
                }



                var JsonData = new
                {
                    rows = CollarModelList,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public ActionResult GridListJsonCollar (FileViewModel model, string ParameterJson, string TreeID, Pagination jqgridparam, string IsPublic)
        {
            if (ParameterJson != null)
            {
                if (ParameterJson != "[{\"MemberModel\":\"\",\"InBeginTime\":\"\",\"InEndTime\":\"\",\"Class\":\"\"}]")
                {
                    List<FileViewModel> query_member =LeaRun.Util.Json.ToList<FileViewModel>(ParameterJson);
                    for (int i = 0; i < query_member.Count(); i++)
                    {
                        model.MemberModel = query_member[i].MemberModel;
                        model.InBeginTime = query_member[i].InBeginTime;
                        model.InEndTime = query_member[i].InEndTime;
                        model.Class = query_member[i].Class;
                    }
                }
            }
            try
            {
                model.TreeID = TreeID;
                int total = 0;
                Expression<Func<RMC_ProjectWarehouse, bool>> func = ExpressionExtensions.True<RMC_ProjectWarehouse>();
                func = f => f.DeleteFlag != 1 && f.IsShiped == 1&&f.InStock>0;
                #region 查询条件拼接
                if (model.TreeID != null && model.TreeID.ToString() != "")
                {
                    func = func.And(f => f.TreeId == model.TreeID);
                }
                if (model.MemberModel != null && model.MemberModel != "")
                {
                    var member = MemberLibraryCurrent.Find(fm => fm.MemberModel == model.MemberModel).SingleOrDefault();
                    func = func.And(f => f.MemberId == member.MemberID);
                }
                if (model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00")
                {
                    func = func.And(f => f.ModifyTime >= model.InBeginTime);
                }
                if (model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00")
                {
                    func = func.And(f => f.ModifyTime <= model.InEndTime);
                }
                if (!string.IsNullOrEmpty(model.Class))
                {
                    func = func.And(f => f.Class == model.Class.Trim()); /*func = func.And(f => f.FullPath.Contains(model.FilePath))*/
                }
                #endregion

                DataTable ListData, ListData1;
                ListData = null;
                //List<RMC_Tree> listtree = TreeCurrent.FindPage<string>(jqgridparam.page
                //                         , jqgridparam.rows
                //                         , func1
                //                         , false
                //                         , f => f.TreeID.ToString()
                //                         , out total
                //                         ).ToList();
                List<RMC_ProjectWarehouse> listfile = ProjectWarehouseCurrent.FindPage<string>(jqgridparam.page
                                         , jqgridparam.rows
                                         , func
                                         , false
                                         , f => f.ModifyTime.ToString()
                                         , out total
                                         ).ToList();
                List<ProjectWarehouseModel> ProjectWarehouselist = new List<ProjectWarehouseModel>();
                foreach (var item in listfile)
                {
                    ProjectWarehouseModel ProjectWarehouse = new ProjectWarehouseModel();
                    ProjectWarehouse.ProjectWarehouseId = item.ProjectWarehouseId;
                    ProjectWarehouse.ProjectDemandId = item.ProjectDemandId;
                    ProjectWarehouse.MemberId = item.MemberId;
                    var memberlibrary = MemberLibraryCurrent.Find(f => f.MemberID == item.MemberId).SingleOrDefault();
                    ProjectWarehouse.MemberName = memberlibrary.MemberName;
                    ProjectWarehouse.MemberModel = memberlibrary.MemberModel;
                    ProjectWarehouse.MemberNumbering = memberlibrary.MemberNumbering.ToString();
                    ProjectWarehouse.MemberUnit = memberlibrary.MemberUnit;
                    ProjectWarehouse.InStock = item.InStock;
                    var ProjectDomend = ProjectManagementCurrent.Find(f=>f.ProjectDemandId==item.ProjectDemandId).SingleOrDefault();
                    ProjectWarehouse.CollarNumbered = ProjectDomend.CollarNumbered;
                    ProjectWarehouse.Description = item.Description;
                    ProjectWarehouselist.Add(ProjectWarehouse);
                }
                if (ProjectWarehouselist.Count() > 0)// && listtree.Count() > 0
                {

                    //ListData0 = ListToDataTable(listtree);
                    ListData1 = DataHelper.ListToDataTable(ProjectWarehouselist);
                    ListData = ListData1.Clone();
                    object[] obj = new object[ListData.Columns.Count];
                    ////添加DataTable0的数据
                    //for (int i = 0; i < ListData0.Rows.Count; i++)
                    //{
                    //    ListData0.Rows[i].ItemArray.CopyTo(obj, 0);
                    //    ListData.Rows.Add(obj);
                    //}
                    //添加DataTable1的数据
                    for (int i = 0; i < ListData1.Rows.Count; i++)
                    {
                        ListData1.Rows[i].ItemArray.CopyTo(obj, 0);
                        ListData.Rows.Add(obj);
                    }

                }
                //else if (listtree.Count() > 0)
                //{
                //    ListData = ListToDataTable(listtree);
                //}
                else if (listfile.Count() > 0)
                {
                    ListData = DataHelper.ListToDataTable(ProjectWarehouselist);
                }
                else
                {
                    ListData = null;
                }

                var JsonData = new
                {
                    rows = ListData,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public ActionResult DeleteProjectCollar(string KeyValue)
        {
            try
            {
                List<int> ids = new List<int>();
                List<int> ids1 = new List<int>();
                int CollarId = Convert.ToInt32(KeyValue);
                ids.Add(CollarId);
                CollarManagementCurrent.Remove(ids);


                List<RMC_CollarMember> CollarMemberList = CollarMemberCurrent.Find(f => f.CollarId== CollarId).ToList();
                if (CollarMemberList.Count() > 0)
                {
                    for (int i = 0; i < CollarMemberList.Count(); i++)
                    {
                        int CollarMemberId = Convert.ToInt32(CollarMemberList[i].CollarMemberId);
                        ids1.Add(CollarMemberId);

                        var OrderMember = CollarMemberCurrent.Find(f => f.CollarMemberId == CollarMemberId).SingleOrDefault();
                        var Demand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == OrderMember.ProjectDemandId).SingleOrDefault();
                        Demand.CollarNumbered = Demand.CollarNumbered - CollarMemberList[i].Qty;
                        ProjectManagementCurrent.Modified(Demand);
                    }
                    CollarMemberCurrent.Remove(ids1);
                }
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public ActionResult ListMember(string KeyValue)
        {
            var ProjectWarehouselist = new List<ProjectWarehouseModel>();
            if (KeyValue != null)
            {
                string[] array = KeyValue.Split(',');
                if (array.Count() > 0)
                {
                    foreach (var item in array)
                    {
                        int id = Convert.ToInt32(item);
                        var listfile = ProjectWarehouseCurrent.Find(f=>f.MemberId== id).SingleOrDefault();
                        ProjectWarehouseModel ProjectWarehouse = new ProjectWarehouseModel();
                        ProjectWarehouse.ProjectWarehouseId = listfile.ProjectWarehouseId;
                        ProjectWarehouse.ProjectDemandId = listfile.ProjectDemandId;
                        ProjectWarehouse.MemberId = listfile.MemberId;
                        var memberlibrary = MemberLibraryCurrent.Find(f => f.MemberID == listfile.MemberId).SingleOrDefault();
                        ProjectWarehouse.MemberName = memberlibrary.MemberName;
                        ProjectWarehouse.MemberModel = memberlibrary.MemberModel;
                        ProjectWarehouse.MemberNumbering = memberlibrary.MemberNumbering.ToString();
                        ProjectWarehouse.MemberUnit = memberlibrary.MemberUnit;
                        ProjectWarehouse.InStock = listfile.InStock;
                        var ProjectDomend = ProjectManagementCurrent.Find(f => f.ProjectDemandId == listfile.ProjectDemandId).SingleOrDefault();
                        ProjectWarehouse.CollarNumbered = ProjectDomend.CollarNumbered;
                        ProjectWarehouse.Description = listfile.Description;
                        ProjectWarehouselist.Add(ProjectWarehouse);
                    }
                }
            } 
            else
            {
                ProjectWarehouselist = null;
            }
            return Json(ProjectWarehouselist);
        }
    }
}
