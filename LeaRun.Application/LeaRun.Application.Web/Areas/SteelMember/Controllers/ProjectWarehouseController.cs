
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
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class ProjectWarehouseController : MvcControllerBase
    {

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
            //var userid = 1;
            //List<DOC_R_Tree_Role> TRR = new List<DOC_R_Tree_Role>();
            //var userrole = UserRoleRepository.Find(ur => ur.UserID == userid).SingleOrDefault();
            //TRR = TreeRoleRepository.Find(tr => tr.RoleID == userrole.RoleID).ToList();
            int _ItemId = Convert.ToInt32(ItemId);
            List<RMC_Tree> list = TreeCurrent.Find(t => t.ModuleId == ItemId).ToList();
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

        #region 项目仓库管理
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPage()
        {
            return View();
        }
        /// <summary>
        /// 【工程项目管理】返回文件（夹）列表JSON
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ParameterJson">文件名搜索条件</param>
        /// <param name="TreeId">文件夹ID</param>
        /// <param name="jqgridparam"></param>
        /// <param name="IsPublic">是否公共 1-公共、0-我的</param>
        /// <returns></returns>         
        public ActionResult GridListJson(FileViewModel model, string ParameterJson, string TreeId, Pagination jqgridparam, string IsPublic)
        {
            try
            {
                #region 查询条件拼接
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

                Expression<Func<RMC_ProjectWarehouse, bool>> func = ExpressionExtensions.True<RMC_ProjectWarehouse>();
                func=f => f.ProjectWarehouseId > 0 && f.IsShiped == 1;
                Func<RMC_ProjectWarehouse, bool> func1 = f => f.MemberTreeId!= "";

                var _a = model.MemberModel != null && model.MemberModel.ToString() != "";
                var _b = model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00";
                var _c = model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00";

                if (_a && _b && _c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.ModifyTime >= model.InBeginTime && f.ModifyTime <= model.InEndTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.ModifyTime >= model.InBeginTime && f.ModifyTime <= model.InEndTime;
                }
                else if (_a && !_b &&! _c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel));
                    func1 = f => f.MemberModel.Contains(model.MemberModel);
                }
                else if (_b && !_a && !_c)
                {
                    func = func.And(f => f.ModifyTime >= model.InBeginTime);
                    func1 = f => f.ModifyTime >= model.InBeginTime;
                }
                else if (_c && !_b && !_a)
                {
                    func = func.And(f => f.ModifyTime <= model.InEndTime);
                    func1 = f => f.ModifyTime <= model.InEndTime;
                }
                else if (_a && _b && !_c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.ModifyTime >= model.InBeginTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.ModifyTime >= model.InBeginTime;
                }
                else if (_a && _c && !_b)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.ModifyTime <= model.InEndTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.ModifyTime <= model.InEndTime;
                }
                else if (_b && _c && !_a)
                {
                    func = func.And(f => f.ModifyTime >= model.InBeginTime && f.ModifyTime <= model.InEndTime);
                    func1 = f => f.ModifyTime >= model.InBeginTime && f.ModifyTime <= model.InEndTime;
                }
                #endregion

                var MemberList_ = new List<RMC_ProjectWarehouse>();
                var ProjectWarehouseModellist = new List<ProjectWarehouseModel>();

                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                List<RMC_ProjectWarehouse> MemberList = new List<RMC_ProjectWarehouse>();
                if (TreeId == ""||TreeId==null)
                {
                    //func.And(f =>f.ProjectWarehouseId> 0&&f.IsShiped==1);
                    MemberList = MemberList_ = ProjectWarehouseCurrent.FindPage<string>(jqgridparam.page
                                             , jqgridparam.rows
                                             , func
                                             , false
                                             , f => f.ModifyTime.ToString()
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
                        var _MemberList = ProjectWarehouseCurrent.Find(m => m.MemberTreeId.ToString() == item.TreeID && m.IsShiped == 1).ToList();
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
                    ProjectWarehouseModel ProjectWarehouse = new ProjectWarehouseModel();
                    ProjectWarehouse.ProjectWarehouseId = item.ProjectWarehouseId;
                    //var projectinfo = ProjectInfoCurrent.Find(f => f.ProjectId == item.ProjectId).SingleOrDefault();
                    //projectdemand.ProjectName = projectinfo.ProjectName;
                    var memberlibrary = MemberLibraryCurrent.Find(f => f.MemberId == item.MemberId).SingleOrDefault();
                    ProjectWarehouse.MemberName = memberlibrary.MemberName;
                    ProjectWarehouse.MemberModel = item.MemberModel;
                    ProjectWarehouse.MemberNumbering = memberlibrary.MemberNumbering.ToString();
                    ProjectWarehouse.ModifyTime = item.ModifyTime;
                    ProjectWarehouse.Class = item.Class;
                    ProjectWarehouse.Damage = item.Damage;
                    ProjectWarehouse.InStock = item.InStock;
                    ProjectWarehouse.Leader = item.Leader;
                    ProjectWarehouse.Librarian = item.Librarian;
                    ProjectWarehouse.Description = item.Description;
                    ProjectWarehouseModellist.Add(ProjectWarehouse);
                }

                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = ProjectWarehouseModellist.OrderByDescending(f => f.ModifyTime)
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
        /// 表单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 【信息管理】返回文件夹对象JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public ActionResult SetDataForm(string KeyValue)
        {
            int key_value = Convert.ToInt32(KeyValue);
            RMC_ProjectWarehouse entity = ProjectWarehouseCurrent.Find(f => f.ProjectWarehouseId == key_value).SingleOrDefault();
            //string JsonData = entity.ToJson();
            ////自定义
            //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(entity.ToJson());
            //return Json(entity);
        }

        /// <summary>
        /// 提交文件夹表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <param name="TreeId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SubmitDataForm(RMC_ProjectWarehouse entity, string KeyValue, string TreeId)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int key_value = Convert.ToInt32(KeyValue);
                    RMC_ProjectWarehouse Oldentity = ProjectWarehouseCurrent.Find(t => t.ProjectWarehouseId == key_value).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.Damage = entity.Damage;//给旧实体重新赋值
                    Oldentity.Class = entity.Class;
                    Oldentity.Librarian = "1";
                    Oldentity.Leader = entity.Leader;
                    Oldentity.Description = entity.Description;
                    ProjectWarehouseCurrent.Modified(Oldentity);
                    //this.WriteLog(IsOk, entity, Oldentity, KeyValue, Message);
                }
                else
                {
                    //int treeid = Convert.ToInt32(TreeId);
                    //RMC_ProjectWarehouse Oldentity = new RMC_ProjectWarehouse();
                    //Oldentity.TreeId = treeid;
                    //Oldentity.Damage = entity.Damage;//给旧实体重新赋值
                    //Oldentity.Class = entity.Class;
                    //Oldentity.Librarian = "1";
                    //Oldentity.ModifyTime = DateTime.Now;
                    //Oldentity.Leader = entity.Leader;
                    //Oldentity.Description = entity.Description;
                    //ProjectWarehouseCurrent.Add(Oldentity);
                    //IsOk = 1;
                    //this.WriteLog(IsOk, entity, null, KeyValue, Message);
                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                //this.WriteLog(-1, entity, null, KeyValue, "操作失败：" + ex.Message);
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteData(string KeyValue)
        {
            try
            {
                List<int> ids = new List<int>();
                int Id = Convert.ToInt32(KeyValue);
                ids.Add(Id);
                ProjectWarehouseCurrent.Remove(ids);
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }
        #endregion
    }
}
