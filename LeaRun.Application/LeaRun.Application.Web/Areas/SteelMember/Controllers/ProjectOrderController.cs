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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class ProjectOrderController : MvcControllerBase
    {
        //
        // GET: /Hello/
        //public Base_ModuleBll Sys_modulebll = new Base_ModuleBll();
        //public Base_ButtonBll Sys_buttonbll = new Base_ButtonBll();
        [Inject]
        public ProjectInfoIBLL ProjectInfoCurrent { get; set; }
        [Inject]
        public ProjectManagementIBLL ProjectManagementCurrent { get; set; }
        [Inject]
        public TreeIBLL TreeCurrent { get; set; }
        [Inject]
        public FileIBLL MemberLibraryCurrent { get; set; }
        //[Inject]
        //public CompanyIBLL CompanyCurrent { get; set; }
        [Inject]
        public OrderManagementIBLL OrderManagementCurrent { get; set; }
        [Inject]
        public ProjectWarehouseIBLL ProjectWarehouseCurrent { get; set; }
        [Inject]
        public ShipManagementIBLL ShipManagementCurrent { get; set; }
        [Inject]
        public OrderMemberIBLL OrderMemberCurrent { get; set; }

        public MemberUnitIBLL MemberUnitCurrent { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 【工程项目管理】返回树JONS
        /// </summary>
        /// <returns></returns>      
        public ActionResult TreeJson(string ItemId)
        {
            int _ItemId = Convert.ToInt32(ItemId);
            List<RMC_Tree> list, list1, list2;
            list1 = TreeCurrent.Find(t => t.DeleteFlag != 1 && t.ItemClass == 0).ToList();
            list2 = TreeCurrent.Find(t => t.ModuleId == _ItemId.ToString() && t.DeleteFlag != 1 && t.ItemClass == 2).ToList();
            list = list1.Concat(list2).Distinct().ToList();

            List<TreeEntity> TreeList = new List<TreeEntity>();
            foreach (RMC_Tree item in list)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;
                List<RMC_Tree> childnode = list.FindAll(t => t.ParentID == item.TreeID);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                tree.id = item.TreeID.ToString();
                tree.text = item.TreeName;
                tree.value = item.TreeID.ToString();
                tree.isexpand = item.State == 1 ? true : false;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentID.ToString();
                //tree.iconCls = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }

        public ActionResult TreeName(string TreeId)
        {
            int _TreeId = Convert.ToInt32(TreeId);
            RMC_Tree Entity = TreeCurrent.Find(t => t.TreeID == TreeId && t.DeleteFlag != 1).SingleOrDefault();
            return Content(Entity.ToJson());
            //return Json(entity);
        }

        #region 构件订单管理

        /// <summary>
        ///构件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemList()
        {
            return View();
        }

        /// <summary>
        /// 订单表单
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult OrderForm(string KeyValue)
        {
            if (KeyValue == "" || KeyValue == null)
            {
                ViewBag.OrderNumbering = "DD" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
                ViewData["CreateMan"] =OperatorProvider.Provider.Current().UserName;
            }
            return View();
        }

        public ActionResult DetailForm()
        {
            return View();
        }
        /// <summary>
        /// 控制订单构件的数量（新增）
        /// </summary>
        /// <returns></returns>
        public ContentResult AddMemberNumber(string KeyValue,string TreeId)
        {  
            int _TreeId = Convert.ToInt32(TreeId);
            var MemberDemand= ProjectManagementCurrent.Find(f=>f.MemberId== KeyValue&&f.TreeId== TreeId).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number=0;
            var Order = OrderManagementCurrent.Find(f => f.TreeId == TreeId).ToList();
            foreach (var item in Order)
            {
                var OrderMember = OrderMemberCurrent.Find(f => f.OrderId == item.OrderId&&f.MemberId== KeyValue).SingleOrDefault();
                if (OrderMember != null) {
                    Number +=Convert.ToInt32(OrderMember.Qty);
                }

            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.MemberNumber) - Number;

            return Content(MemberDemandNumber.ToString());
        }

        /// <summary>
        /// 控制订单构件的数量(编辑)
        /// </summary>
        /// <returns></returns>
        public ContentResult EditMemberNumber(string KeyValue,string MemberId)
        {
            int _KeyValue = Convert.ToInt32(KeyValue);
            int TreeId;
            var OrderList=new List<RMC_ProjectOrder>();
            var Order = OrderManagementCurrent.Find(f => f.OrderId == _KeyValue).SingleOrDefault();
            TreeId = Convert.ToInt32(Order.TreeId);
            OrderList = OrderManagementCurrent.Find(f => f.TreeId == TreeId.ToString()).ToList();

            var MemberDemand = ProjectManagementCurrent.Find(f => f.MemberId == MemberId && f.TreeId == TreeId.ToString()).SingleOrDefault();
            int MemberDemandNumber = 0;
            int Number = 0;

            foreach (var item in OrderList)
            {
                var OrderMember = OrderMemberCurrent.Find(f => f.OrderId == item.OrderId && f.MemberId == MemberId).SingleOrDefault();
                if (OrderMember != null)
                {
                    Number += Convert.ToInt32(OrderMember.Qty);
                }
            }
            MemberDemandNumber = Convert.ToInt32(MemberDemand.MemberNumber) - Number;

            return Content(MemberDemandNumber.ToString());
        }
        /// <summary>
        /// 控制创建订单（新增）
        /// </summary>
        /// <returns></returns>
        public ContentResult Add_MemberNumber(string TreeId)
        {
            int _TreeId = Convert.ToInt32(TreeId);
            int MemberDemandNumber = 0;
            int Number = 0;
            //int _Number = 0;
            //int Toal = 0;
            var MemberDemand = ProjectManagementCurrent.Find(f => f.TreeId == TreeId && f.IsReview==1).ToList();
            if (MemberDemand.Count()>0)
            {
                foreach (var item in MemberDemand)
                {
                    MemberDemandNumber += Convert.ToInt32(item.MemberNumber);
                    //if (item.IsReview == 1)
                    //{
                    //    _Number++;
                    //}
                }
            }

            var Order = OrderManagementCurrent.Find(f => f.TreeId == TreeId).ToList();
            foreach (var item in Order)
            {
                var OrderMember = OrderMemberCurrent.Find(f => f.OrderId == item.OrderId).ToList();
                foreach (var item1 in OrderMember)
                {
                    Number += Convert.ToInt32(item1.Qty);
                }
            }

           if (MemberDemandNumber > 0)
            {
                MemberDemandNumber = MemberDemandNumber - Number;
           }
            return Content(MemberDemandNumber.ToString());
        }
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SubmitOrderForm(RMC_ProjectOrder entity, string KeyValue, string TreeId, string POOrderEntryJson)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "变更成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int _OrderId = Convert.ToInt32(KeyValue);

                    RMC_ProjectOrder Oldentity = OrderManagementCurrent.Find(f => f.OrderId == _OrderId).SingleOrDefault();
                    Oldentity.IsSubmit = 0;
                    Oldentity.IsReview = 0;
                    Oldentity.Priority = entity.Priority;
                    Oldentity.Description = entity.Description;
                    OrderManagementCurrent.Modified(Oldentity);


                    List<int> Ids = new List<int>();
                    List<RMC_OrderMember> CollarMemberList = OrderMemberCurrent.Find(f => f.OrderId == _OrderId).ToList();
                    for (int i = 0; i < CollarMemberList.Count(); i++)
                    {
                        int OrderMemberId = CollarMemberList[i].OrderMemberId;
                        Ids.Add(OrderMemberId);
                        var ProjectDemandId = Convert.ToInt32(CollarMemberList[i].ProjectDemandId);
                        var OrderMember = OrderMemberCurrent.Find(f => f.OrderMemberId == OrderMemberId).SingleOrDefault();
                        var ProjectDemand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == ProjectDemandId).SingleOrDefault();
                        ProjectDemand.OrderQuantityed = ProjectDemand.OrderQuantityed - OrderMember.Qty;
                        ProjectManagementCurrent.Modified(ProjectDemand);
                    }
                    OrderMemberCurrent.Remove(Ids);

                    //构件单
                    List<OrderModel> POOrderEntryList =LeaRun.Util.Json.ToList<OrderModel>(POOrderEntryJson);
                    //int index = 1;
                    foreach (OrderModel poorderentry in POOrderEntryList)
                    {
                        if (!string.IsNullOrEmpty(poorderentry.MemberNumbering))
                        {
                            RMC_OrderMember OrderMember = new RMC_OrderMember();
                            OrderMember.OrderId = _OrderId;
                            OrderMember.ProjectDemandId = Convert.ToInt32(poorderentry.ProjectDemandId);
                            OrderMember.MemberId = poorderentry.MemberId;
                            OrderMember.Description = poorderentry.Description;
                            OrderMember.MemberNumbering = poorderentry.MemberNumbering;
                            OrderMember.MemberModel = poorderentry.MemberModel;
                            OrderMember.MemberName = poorderentry.MemberName;
                            OrderMember.MemberUnit = poorderentry.MemberUnit;
                            OrderMember.Price = Convert.ToDecimal(poorderentry.Price);
                            OrderMember.PriceAmount = Convert.ToDecimal(poorderentry.PriceAmount);
                            OrderMember.Qty = Convert.ToInt32(poorderentry.Qty);

                            var ProjectDemand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == OrderMember.ProjectDemandId).SingleOrDefault();
                            ProjectDemand.OrderQuantityed = ProjectDemand.OrderQuantityed + Convert.ToInt32(poorderentry.Qty);
                            ProjectManagementCurrent.Modified(ProjectDemand);

                            OrderMemberCurrent.Add(OrderMember);
                            //index++;
                        }
                    }

                }
                else
                {
                    int _TreeId = Convert.ToInt32(TreeId);
                    RMC_ProjectOrder Oldentity = new RMC_ProjectOrder();
                    Oldentity.TreeId = TreeId;
                    Oldentity.OrderNumbering = entity.OrderNumbering;//给旧实体重新赋值
                    Oldentity.IsSubmit = 0;
                    Oldentity.IsReview = 0;
                    Oldentity.ConfirmOrder = 0;
                    Oldentity.Productioned = 0;
                    Oldentity.Priority = entity.Priority;
                    Oldentity.TreeName = entity.TreeName;
                    Oldentity.CreateMan =OperatorProvider.Provider.Current().UserName;
                    Oldentity.CreateTime = entity.CreateTime;
                    Oldentity.Description = entity.Description;
                    int OrderId = OrderManagementCurrent.Add(Oldentity).OrderId;

                    RMC_OrderMember OrderMember = new RMC_OrderMember();
                    List<OrderModel> POOrderEntryList =LeaRun.Util.Json.ToList<OrderModel>(POOrderEntryJson);
                    int index = 1;
                    foreach (OrderModel poorderentry in POOrderEntryList)
                    {
                        if (!string.IsNullOrEmpty(poorderentry.MemberNumbering))
                        {
                            //poorderentry.SortCode = index;
                            //poorderentry.Create();
                            OrderMember.OrderId = OrderId;
                            OrderMember.ProjectDemandId = Convert.ToInt32(poorderentry.ProjectDemandId);
                            OrderMember.MemberId = poorderentry.MemberId;
                            OrderMember.Description = poorderentry.Description;
                            OrderMember.MemberNumbering = poorderentry.MemberNumbering;
                            OrderMember.MemberModel = poorderentry.MemberModel;
                            OrderMember.MemberName = poorderentry.MemberName;
                            OrderMember.MemberUnit = poorderentry.MemberUnit;
                            OrderMember.Price = Convert.ToDecimal(poorderentry.Price);
                            OrderMember.PriceAmount = Convert.ToDecimal(poorderentry.PriceAmount);
                            OrderMember.Qty = Convert.ToInt32(poorderentry.Qty);

                            var Demand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == OrderMember.ProjectDemandId).SingleOrDefault();
                            Demand.OrderQuantityed = Demand.OrderQuantityed + OrderMember.Qty;
                            ProjectManagementCurrent.Modified(Demand);

                            OrderMemberCurrent.Add(OrderMember);
                            index++;
                        }
                    }
                }
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
        /// <param name="model"></param>
        /// <param name="jqgridparam"></param>
        ///<param name="TreeId">文件夹ID</param>
        /// <param name="IsPublic">是否公共 1-公共、0-我的</param>
        /// <param name="ParameterJson">文件名搜索条件</param>
        /// <returns></returns>         
        public ActionResult GridListJson(FileViewModel model, string TreeId, Pagination jqgridparam, string IsPublic, string ParameterJson)
        {
            try
            {
                #region 查询条件拼接
                if (ParameterJson != null)
                {
                    if (ParameterJson != "[{\"OrderNumbering\":\"\",\"InBeginTime\":\"\",\"InEndTime\":\"\"}]")
                    {
                        List<FileViewModel> query_member =LeaRun.Util.Json.ToList<FileViewModel>(ParameterJson);
                        for (int i = 0; i < query_member.Count(); i++)
                        {
                            model.OrderNumbering = query_member[i].OrderNumbering;
                            model.InBeginTime = query_member[i].InBeginTime;
                            model.InEndTime = query_member[i].InEndTime;
                        }
                    }
                }
                Expression<Func<RMC_ProjectOrder, bool>> func = ExpressionExtensions.True<RMC_ProjectOrder>();
                Func<RMC_ProjectOrder, bool> func1 = f => f.TreeId != "";

                var _a = model.OrderNumbering != null && model.OrderNumbering.ToString() != "";
                var _b = model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00";
                var _c = model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00";

                if (_a && _b && _c)
                {
                    func = func.And(f => f.OrderNumbering.Contains(model.OrderNumbering) && f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime);
                    func1 = f => f.OrderNumbering.Contains(model.OrderNumbering) && f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime;
                }
                else if (_a && !_b && !_c)
                {
                    func = func.And(f => f.OrderNumbering.Contains(model.OrderNumbering));
                    func1 = f => f.OrderNumbering.Contains(model.OrderNumbering);
                }
                else if (_b && !_a && !_c)
                {
                    func = func.And(f => f.CreateTime >= model.InBeginTime);
                    func1 = f => f.CreateTime >= model.InBeginTime;
                }
                else if (_c && !_b && !_a)
                {
                    func = func.And(f => f.CreateTime <= model.InEndTime);
                    func1 = f => f.CreateTime <= model.InEndTime;
                }
                else if (_a && _b && !_c)
                {
                    func = func.And(f => f.OrderNumbering.Contains(model.OrderNumbering) && f.CreateTime >= model.InBeginTime);
                    func1 = f => f.OrderNumbering.Contains(model.OrderNumbering) && f.CreateTime >= model.InBeginTime;
                }
                else if (_a && _c && !_b)
                {
                    func = func.And(f => f.OrderNumbering.Contains(model.OrderNumbering) && f.CreateTime <= model.InEndTime);
                    func1 = f => f.OrderNumbering.Contains(model.OrderNumbering) && f.CreateTime <= model.InEndTime;
                }
                else if (_b && _c && !_a)
                {
                    func = func.And(f => f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime);
                    func1 = f => f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime;
                }
                #endregion

                var ProjectDemandList_ = new List<RMC_ProjectOrder>();
                //var ProjectDemandModelList_ = new List<RMC_ProjectOrder>();

                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                List<RMC_ProjectOrder> ProjectDemandList = new List<RMC_ProjectOrder>();
                if (TreeId == "")
                {
                    func.And(f => f.DeleteFlag != 1 & f.ProjectDemandId > 0);

                    ProjectDemandList = ProjectDemandList_ = OrderManagementCurrent.FindPage<string>(jqgridparam.page
                                             , jqgridparam.rows
                                             , func
                                             , false
                                             , f => f.CreateTime.ToString()
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
                        var _ProjectDemandList = OrderManagementCurrent.Find(m => m.TreeId.ToString() == item.TreeID).ToList();
                        if (_ProjectDemandList.Count() > 0)
                        {
                            ProjectDemandList = ProjectDemandList.Concat(_ProjectDemandList).ToList();
                        }
                    }

                    ProjectDemandList = ProjectDemandList.Where(func1).ToList();
                    ProjectDemandList_ = ProjectDemandList.Take(jqgridparam.rows * jqgridparam.page).Skip(jqgridparam.rows * (jqgridparam.page - 1)).ToList();
                    total = ProjectDemandList.Count();
                }

                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = ProjectDemandList_.OrderByDescending(f => f.CreateTime),
                };
                //List<RMC_ProjectOrder> listfile = OrderManagementCurrent.FindPage<string>(jqgridparam.page
                //                             , jqgridparam.rows
                //                             , func
                //                             , false
                //                             , f => f.TreeId.ToString()
                //                             , out total
                //                             ).ToList();
                //    List<ProjectDemandModel> projectdemandlist = new List<ProjectDemandModel>();
                //    foreach (var item in listfile)
                //    {
                //        ProjectDemandModel projectdemand = new ProjectDemandModel();
                //        projectdemand.OrderNumbering = item.OrderNumbering;
                //        projectdemand.Priority = item.Priority;
                //        projectdemand.OrderId = item.OrderId;
                //        projectdemand.IsReview = item.IsReview;
                //        projectdemand.ReviewMan = item.ReviewMan;
                //        projectdemand.IsSubmit = item.IsSubmit;
                //        projectdemand.CreateTime = item.CreateTime;
                //        projectdemand.CreateMan = item.CreateMan;
                //        projectdemand.Description = item.Description;
                //        projectdemand.Productioned = item.Productioned;
                //        projectdemandlist.Add(projectdemand);
                //    }

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
        /// 【工程项目管理】返回文件（夹）列表JSON
        /// </summary>
        /// <param name="TreeID">文件夹ID</param>
        /// <param name="jqgridparam"></param>
        /// <param name="keywords">文件名搜索条件</param>
        /// <param name="IsPublic">是否公共 1-公共、0-我的</param>
        /// <returns></returns>         
        public ActionResult GridListJsonDemand(/*ProjectInfoViewModel model,*/ string TreeID, Pagination jqgridparam, string IsPublic, string keywords)
        {
            try
            {
                int TreeId;
                string MemberModel = "";
                string MemberNumbering = "";
                //int FolderId = Convert.ToInt32(FolderId);
                if (TreeID == "" || TreeID == null)
                {
                    TreeId = 1;
                }
                else
                {
                    TreeId = Convert.ToInt32(TreeID);
                }
                if (keywords != null)
                {
                    if (keywords.Length > 10)
                    {
                        MemberNumbering = keywords;
                    }
                    else
                    {
                        MemberModel = keywords;
                    }
                }

                int total = 0;
                Expression<Func<RMC_ProjectDemand, bool>> func = ExpressionExtensions.True<RMC_ProjectDemand>();
                func = f => f.DeleteFlag != 1 && f.TreeId == TreeId.ToString() && f.IsReview == 1;
                #region 查询条件拼接
                if (keywords != null && keywords != "&nbsp;")
                {
                    func = func.And(f => f.MemberNumbering.Contains(MemberNumbering));
                }
                if (!string.IsNullOrEmpty(MemberModel))
                {
                    func = func.And(f => f.MemberModel.Contains(MemberModel)); /*func = func.And(f => f.FullPath.Contains(model.FilePath))*/
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
                List<RMC_ProjectDemand> listfile = ProjectManagementCurrent.FindPage<string>(jqgridparam.page
                                         , jqgridparam.rows
                                         , func
                                         , false
                                         , f => f.ProjectDemandId.ToString()
                                         , out total
                                         ).ToList();
                List<ProjectDemandModel> projectdemandlist = new List<ProjectDemandModel>();
                foreach (var item in listfile)
                {
                    ProjectDemandModel projectdemand = new ProjectDemandModel();
                    projectdemand.ProjectDemandId = item.ProjectDemandId;
                    var memberlibrary = MemberLibraryCurrent.Find(f => f.MemberId == item.MemberId).SingleOrDefault();
                    projectdemand.MemberName = memberlibrary.MemberName;
                    projectdemand.MemberModel = memberlibrary.MemberModel;
                    projectdemand.MemberUnit = memberlibrary.MemberUnit;
                    projectdemand.UnitPrice = memberlibrary.UnitPrice;
                    projectdemand.MemberId = memberlibrary.MemberId;
                    projectdemand.MemberNumbering = memberlibrary.MemberNumbering.ToString();
                    projectdemand.IsReview = item.IsReview;
                    projectdemand.ReviewMan = item.ReviewMan;
                    projectdemand.MemberNumber = item.MemberNumber;
                    projectdemand.OrderQuantityed = item.OrderQuantityed;
                    projectdemand.Productioned = item.Productioned;
                    //projectdemand.MemberWeight = item.MemberWeight;
                    //var company = CompanyCurrent.Find(f=>f.MemberCompanyId==item.MemberCompanyId).SingleOrDefault();
                    //projectdemand.MemberCompany = company.FullName;
                    projectdemand.Description = item.Description;

                    if (item.MemberNumber- item.OrderQuantityed!=0)
                    {
                        projectdemandlist.Add(projectdemand);
                    }
                   
                }

                if (projectdemandlist.Count() > 0)// && listtree.Count() > 0
                {
                    //ListData0 = ListToDataTable(listtree);
                    ListData1 = DataHelper.ListToDataTable(projectdemandlist);
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
                    ListData = DataHelper.ListToDataTable(projectdemandlist);
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

        /// <summary>
        /// 订单明细列表（返回Json）
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public ActionResult GetOrderEntryList(string OrderId)
        {
            int _OrderId = Convert.ToInt32(OrderId);
            try
            {
                List<OrderModel> OrderModellist = new List<OrderModel>();
                var listfile = OrderMemberCurrent.Find(f => f.OrderId == _OrderId).ToList();
                foreach (var item in listfile)
                {
                    OrderModel OrderModel = new OrderModel();
                    OrderModel.MemberNumbering = item.MemberNumbering;
                    OrderModel.MemberId= item.MemberId;
                    OrderModel.ProjectDemandId = item.ProjectDemandId.ToString();
                    OrderModel.MemberName = item.MemberName;
                    OrderModel.MemberModel = item.MemberModel;
                    OrderModel.MemberUnit = item.MemberUnit;
                    OrderModel.Qty = item.Qty.ToString();
                    OrderModel.Price = item.Price.ToString();
                    OrderModel.PriceAmount = item.PriceAmount.ToString();
                    OrderModel.Description = item.Description;
                    OrderModellist.Add(OrderModel);
                }



                var JsonData = new
                {
                    rows = OrderModellist,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                // return null;
                throw new Exception(ex.ToString());

            }
        }
        public ActionResult ListMember(string KeyValue,string TreeId)
        {
            var listmember = new List<ProjectDemandModel>();
            if (KeyValue != null) { 
            string[] array = KeyValue.Split(',');
            if (array.Count() > 0)
            {
                if (array != null)
                    foreach (var item in array)
                    {
                        //int _TreeId = Convert.ToInt32(TreeId);
                        //var a = ProjectManagementCurrent.Find(f => f.MemberId == item && f.TreeId == TreeId).SingleOrDefault();
                        ProjectDemandModel projectdemand = new ProjectDemandModel();
                        projectdemand.MemberName = "11111";
                        projectdemand.MemberModel = "2222";
                        projectdemand.MemberUnit = "个";
                        projectdemand.UnitPrice = "2";
                        projectdemand.MemberNumbering = item;
                        projectdemand.MemberNumber = 1000;
                        listmember.Add(projectdemand);
                    }
            }
            }
            else
            {
                listmember = null;
            }
            return Json(listmember);
        }

        /// <summary>
        /// 表单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectName()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<RMC_ProjectInfo> ProjectList = ProjectInfoCurrent.Find(f => f.ProjectId > 0).ToList();
            foreach (var Item in ProjectList)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Item.ProjectName;
                item.Value = Item.ProjectId.ToString();
                List.Add(item);
            }
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取构件名称
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberName(string SubProjectId)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<RMC_MemberLibrary> ProjectList = MemberLibraryCurrent.Find(f => f.SubProjectId == SubProjectId).ToList();
            foreach (var Item in ProjectList)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Item.MemberModel;
                item.Value = Item.MemberId;
                List.Add(item);
            }
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// 获取构件厂名称
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult MemberCompanyName()
        //{
        //    List<SelectListItem> List = new List<SelectListItem>();
        //    List<RMC_Company> MemberCompanyList = CompanyCurrent.Find(f => f.MemberCompanyId > 0).ToList();
        //    foreach (var Item in MemberCompanyList)
        //    {
        //        SelectListItem item = new SelectListItem();
        //        item.Text = Item.FullName;
        //        item.Value = Item.MemberCompanyId.ToString();
        //        List.Add(item);
        //    }
        //    return Json(List, JsonRequestBehavior.AllowGet);
        //}

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
            int OrderId = Convert.ToInt32(KeyValue);
            RMC_ProjectOrder entity = OrderManagementCurrent.Find(f => f.OrderId == OrderId).SingleOrDefault();
            return Content(entity.ToJson());
            //return Json(entity);
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteProjectOrder(string KeyValue)
        {
            try
            {
                List<int> ids = new List<int>();
                List<int> ids1 = new List<int>();
                int OrderId = Convert.ToInt32(KeyValue);
                ids.Add(OrderId);
                OrderManagementCurrent.Remove(ids);


                List<RMC_OrderMember> OrderMemberList = OrderMemberCurrent.Find(f => f.OrderId == OrderId).ToList();
                if (OrderMemberList.Count() > 0)
                {
                    for (int i = 0; i < OrderMemberList.Count(); i++)
                    {
                        int OrderMemberId = Convert.ToInt32(OrderMemberList[i].OrderMemberId);
                        ids1.Add(OrderMemberId);

                        var OrderMember = OrderMemberCurrent.Find(f => f.OrderMemberId == OrderMemberId).SingleOrDefault();
                        var Demand = ProjectManagementCurrent.Find(f => f.ProjectDemandId == OrderMember.ProjectDemandId).SingleOrDefault();
                        Demand.OrderQuantityed = Demand.OrderQuantityed - OrderMemberList[i].Qty;
                        ProjectManagementCurrent.Modified(Demand);
                    }
                    OrderMemberCurrent.Remove(ids1);
                }
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }


        /// <summary>
        /// 审核需求
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="IsReview"></param>
        /// <returns></returns>
        public ActionResult ReviewProjectOrder(string KeyValue, string IsReview)
        {
            try
            {
                string Message = IsReview == "2" ? "操作成功。" : "审核成功。";
                int OrderId = Convert.ToInt32(KeyValue);
                var file = OrderManagementCurrent.Find(f => f.OrderId == OrderId).First();
                file.ModifiedTime = DateTime.Now;
                file.IsReview = Convert.ToInt32(IsReview);
                file.ReviewMan=OperatorProvider.Provider.Current().UserName;
                OrderManagementCurrent.Modified(file);
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 提交项目订单
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public ActionResult SubmitProjectOrder(string KeyValue, RMC_ProjectOrder Entity)
        {
            try
            {
                int OrderId = Convert.ToInt32(KeyValue);
                var file = OrderManagementCurrent.Find(f => f.OrderId == OrderId).First();
                file.ModifiedTime = DateTime.Now;
                file.IsSubmit = 1;
                file.SubmitTime = DateTime.Now;
                file.SubmitMan =OperatorProvider.Provider.Current().UserName;
                OrderManagementCurrent.Modified(file);

                List<RMC_OrderMember> OrderMemberList = OrderMemberCurrent.Find(f => f.OrderId == OrderId).ToList();
                foreach (var item in OrderMemberList)
                {
                    List<RMC_ProjectWarehouse> projectwarehouselist = ProjectWarehouseCurrent.Find(f => f.MemberId == item.MemberId).ToList();
                    if (projectwarehouselist.Count() == 0)
                    {
                        RMC_ProjectWarehouse projectwarehouse = new RMC_ProjectWarehouse();
                        projectwarehouse.OrderId = OrderId;
                        projectwarehouse.MemberId = item.MemberId;
                        var Member = MemberLibraryCurrent.Find(f => f.MemberId == item.MemberId).SingleOrDefault();
                        projectwarehouse.MemberTreeId = Member.SubProjectId;
                        projectwarehouse.TreeId = file.TreeId;
                        projectwarehouse.ModifyTime = DateTime.Now;
                        projectwarehouse.MemberModel = item.MemberModel;
                        projectwarehouse.IsShiped = 0;
                        var OrderMember = OrderMemberCurrent.Find(f => f.OrderId == OrderId && f.MemberId == item.MemberId).ToList();
                        //projectwarehouse.ProjectDemandId = OrderMember.ProjectDemandId;
                        ProjectWarehouseCurrent.Add(projectwarehouse);
                    }

                    List<RMC_ShipManagement> shipmanagementlist = ShipManagementCurrent.Find(f => f.MemberId == item.MemberId).ToList();
                    if (shipmanagementlist.Count() == 0)
                    {
                        RMC_ShipManagement shipmanagement = new RMC_ShipManagement();
                        shipmanagement.OrderId = OrderId;
                        shipmanagement.ShipNumbering = "FHD" + file.OrderNumbering.Replace("DD", "");
                        shipmanagement.MemberId = item.MemberId;
                        shipmanagement.TreeId = file.TreeId;
                        shipmanagement.IsPackaged = 0;
                        shipmanagement.ShipNumber = "0";
                        ShipManagementCurrent.Add(shipmanagement);
                    }

                }

                return Success("提交成功");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// 批量提交项目
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public ActionResult SubmitProjectItemOrder(string KeyValue, RMC_ProjectDemand Entity)
        {
            var message = "";
            try
            {
                int TreeId = Convert.ToInt32(KeyValue);
                List<RMC_ProjectDemand> OldEntity = ProjectManagementCurrent.Find(f => f.TreeId == TreeId.ToString()).ToList();
                if (OldEntity.Count() > 0)
                {
                    for (int i = 0; i < OldEntity.Count(); i++)
                    {

                        ProjectManagementCurrent.Find(f => f.ProjectDemandId == OldEntity[i].ProjectDemandId).SingleOrDefault();
                        Entity.ModifiedTime = DateTime.Now;
                        Entity.IsSubmit = 1;
                        ProjectManagementCurrent.Modified(Entity);
                        //RMC_ProjectOrder Entity1 = new RMC_ProjectOrder();
                        //Entity.ProjectDemandId = OldEntity[i].ProjectDemandId;
                        //OrderManagementCurrent.Add(Entity1);

                    }
                    //foreach (var item in OldEntity)
                    //{
                    //    ProjectManagementCurrent.Find(f => f.ProjectDemandId == item.ProjectDemandId).SingleOrDefault();
                    //    Entity.ModifiedTime = DateTime.Now;
                    //    Entity.IsSubmit = 1;
                    //    ProjectManagementCurrent.Modified(Entity);
                    //    RMC_ProjectOrder Entity1 = new RMC_ProjectOrder();
                    //    Entity.ProjectDemandId = item.ProjectDemandId;
                    //    OrderManagementCurrent.Add(Entity1);
                    //}
                }
                else
                {
                    message = "该项目无项目信息";
                }
                return Success(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion
    }
}
