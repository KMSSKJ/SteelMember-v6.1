using LeaRun.Business;
using LeaRun.Entity.SteelMember;
using LeaRun.Repository.SteelMember.IBLL;
using LeaRun.Utilities;
using LeaRun.WebApp.Controllers;
using Ninject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class ProjectInfoManagementController : MvcControllerBase
    {
        //
        // GET: /ProjectInfo/
        public Base_ModuleBll Sys_modulebll = new Base_ModuleBll();
        public Base_ButtonBll Sys_buttonbll = new Base_ButtonBll();

        [Inject]
        public TreeIBLL TreeCurrent { get; set; }

        [Inject]
        public ProjectInfoIBLL ProjectInfoCurrent { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }
    
        public ActionResult DepartmentIndex()
        {
            return View();
        }
        public ActionResult ManIndex()
        {
            return View();
        }


        /// <summary>
        /// 【工程项目管理】返回树JONS
        /// </summary>
        /// <returns></returns>      
        public ActionResult TreeJson(string ItemId)
        {
            int itemid = Convert.ToInt32(ItemId);
            List<RMC_Tree> list, list1, list2;
            list1 = TreeCurrent.Find(t => t.DeleteFlag != 1 && t.ItemClass == 0).ToList();
            list2 = TreeCurrent.Find(t => t.ItemID == itemid && t.DeleteFlag != 1 && t.ItemClass == 1).ToList();
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
                tree.ismenu = item.IsMenu.ToString();
                tree.url = item.Url;
                tree.isexpand = item.State == 1 ? true : false;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentID.ToString();
                //tree.iconCls = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }

        #region 项目信息管理
        /// <summary>
        /// 【项目管理】返回文件（夹）列表JSON
        /// </summary>
        /// <param name="keywords">文件名搜索条件</param>
        /// <param name="FolderId">文件夹ID</param>
        /// <param name="IsPublic">是否公共 1-公共、0-我的</param>
        /// <returns></returns>         
        public ActionResult GridListJson(/*ProjectInfoViewModel model,*/ string TreeID, Pagination jqgridparam, string IsPublic)
        {
            try
            {
                int TreeId;
                //int FolderId = Convert.ToInt32(FolderId);
                if (TreeID == "" || TreeID == null)
                {
                    TreeId = 1;
                }
                else
                {
                    TreeId = Convert.ToInt32(TreeID);
                }

                int total = 0;
                Expression<Func<RMC_ProjectInfo, bool>> func = ExpressionExtensions.True<RMC_ProjectInfo>();
                func = f => f.DeleteFlag != 1 && f.TreeID == TreeId;
                #region 查询条件拼接
                //if (model.ProjectName != null && model.ProjectName != "&nbsp;")
                //{
                //    func = func.And(f => f.ProjectName.Contains(model.ProjectName));
                //}
                //if (!string.IsNullOrEmpty(model.ProjectAddress))
                //{
                //    func = func.And(f => f.ProjectAddress == model.ProjectAddress); /*func = func.And(f => f.FullPath.Contains(model.FilePath))*/
                //}
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
                List<RMC_ProjectInfo> listfile = ProjectInfoCurrent.FindPage<string>(jqgridparam.page
                                         , jqgridparam.rows
                                         , func
                                         , false
                                         , f => f.ProjectId.ToString()
                                         , out total
                                         ).ToList();
                //List<ProjectDemandModel> projectdemandlist = new List<ProjectDemandModel>();
                //foreach (var item in listfile)
                //{
                //    ProjectDemandModel projectdemand = new ProjectDemandModel();
                //    projectdemand.ProjectId = item.ProjectId;
                //    projectdemand.MemberNumbering = item.MemberNumbering;
                //    var projectinfo = ProjectInfoCurrent.Find(f => f.ProjectId == item.ProjectId).SingleOrDefault();
                //    projectdemand.ProjectName = projectinfo.ProjectName;
                //    var memberlibrary = MemberLibraryCurrent.Find(f => f.MemberID == item.MemberId).SingleOrDefault();
                //    projectdemand.MemberName = memberlibrary.MemberModel;
                //    projectdemand.MemberNumber = item.MemberNumber;
                //    projectdemand.MemberWeight = item.MemberWeight;
                //    var company = CompanyCurrent.Find(f => f.MemberCompanyId == item.MemberCompanyId).SingleOrDefault();
                //    projectdemand.MemberCompany = company.FullName;
                //    projectdemand.Description = item.Description;
                //    projectdemandlist.Add(projectdemand);
                //}

                if (listfile.Count() > 0)// && listtree.Count() > 0
                {
                    //ListData0 = ListToDataTable(listtree);
                    ListData1 = DataHelper.ListToDataTable(listfile);
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
                    ListData = DataHelper.ListToDataTable(listfile);
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
        /// 表单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
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
        public ActionResult SetDataForm()
        {
            RMC_ProjectInfo entity = ProjectInfoCurrent.Find(f => f.ProjectId>0).SingleOrDefault();
            //string JsonData = entity.ToJson();
            ////自定义D:\SSKJProject\LeaRun.WebApp\Areas\CodeMaticModule\
            //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(entity.ToJson());
            //return Json(entity);
        }
        public ActionResult GetItemInfo(string KeyValue)
        {
            //int TreeId = Convert.ToInt32(KeyValue);
            RMC_ProjectInfo entity = ProjectInfoCurrent.Find(f => f.TreeID>0).SingleOrDefault();
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
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SubmitDataForm(RMC_ProjectInfo entity, string KeyValue, string TreeId)
        {

            try
            {
                int IsOk = 0;
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int keyvalue = Convert.ToInt32(KeyValue);
                    RMC_ProjectInfo Oldentity = ProjectInfoCurrent.Find(t => t.ProjectId == keyvalue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.ProjectSystemTitel = entity.ProjectSystemTitel;
                    Oldentity.ConstructionUnit = entity.ConstructionUnit;
                    Oldentity.ConstructionPrincipal = entity.ConstructionPrincipal;
                    Oldentity.ConstructionPrincipalTEL = entity.ConstructionPrincipalTEL;
                    Oldentity.DesignUnit = entity.DesignUnit;
                    Oldentity.DesignPrincipal = entity.DesignPrincipal;
                    Oldentity.DesignPrincipalTEL = entity.DesignPrincipalTEL;
                    Oldentity.GeneralContractor = entity.GeneralContractor;
                    Oldentity.GeneralContractorPrincipal = entity.GeneralContractorPrincipal;
                    Oldentity.GeneralContractorPrincipalTEL = entity.GeneralContractorPrincipalTEL;
                    Oldentity.ProfessionalContractor = entity.ProfessionalContractor;
                    Oldentity.ProfessionalContractorPrincipal = entity.ProfessionalContractorPrincipal;
                    Oldentity.ProfessionalContractorPrincipalTEL = entity.ProfessionalContractorPrincipalTEL;
                    Oldentity.SupervisionUnit = entity.SupervisionUnit;
                    Oldentity.SupervisionPrincipal = entity.SupervisionPrincipal;
                    Oldentity.SupervisionPrincipalTEL = entity.SupervisionPrincipalTEL;
                    Oldentity.ProjectAddress = entity.ProjectAddress;
                    Oldentity.ModifiedTime = entity.ModifiedTime;
                    Oldentity.Description = entity.Description;
                    ProjectInfoCurrent.Modified(Oldentity);
                    IsOk = 1;//更新实体对象
                    //this.WriteLog(IsOk, entity, Oldentity, KeyValue, Message);
                }
                else
                {
                    int treeid = Convert.ToInt32(TreeId);
                    var ProjectInfo = ProjectInfoCurrent.Find(f => f.TreeID == treeid).ToList();
                    if (ProjectInfo.Count() > 0)
                    {
                        Message = "操作失败项目中已存在该信息！";
                    }
                    else
                    {
                        RMC_ProjectInfo Oldentity = new RMC_ProjectInfo();
                        Oldentity.TreeID = treeid;
                        var tree = TreeCurrent.Find(f => f.TreeID == treeid).SingleOrDefault();
                        Oldentity.ProjectSystemTitel = entity.ProjectSystemTitel;
                        Oldentity.ConstructionUnit = entity.ConstructionUnit;
                        Oldentity.ConstructionPrincipal = entity.ConstructionPrincipal;
                        Oldentity.ConstructionPrincipalTEL = entity.ConstructionPrincipalTEL;
                        Oldentity.DesignUnit = entity.DesignUnit;
                        Oldentity.DesignPrincipal = entity.DesignPrincipal;
                        Oldentity.DesignPrincipalTEL = entity.DesignPrincipalTEL;
                        Oldentity.GeneralContractor = entity.GeneralContractor;
                        Oldentity.GeneralContractorPrincipal = entity.GeneralContractorPrincipal;
                        Oldentity.GeneralContractorPrincipalTEL = entity.GeneralContractorPrincipalTEL;
                        Oldentity.ProfessionalContractor = entity.ProfessionalContractor;
                        Oldentity.ProfessionalContractorPrincipal = entity.ProfessionalContractorPrincipal;
                        Oldentity.ProfessionalContractorPrincipalTEL = entity.ProfessionalContractorPrincipalTEL;
                        Oldentity.SupervisionUnit = entity.SupervisionUnit;
                        Oldentity.SupervisionPrincipal = entity.SupervisionPrincipal;
                        Oldentity.SupervisionPrincipalTEL = entity.SupervisionPrincipalTEL;
                        Oldentity.ProjectAddress = entity.ProjectAddress;
                        Oldentity.Description = entity.Description;
                        ProjectInfoCurrent.Add(Oldentity);
                        IsOk = 1;
                    }
                    //this.WriteLog(IsOk, entity, null, KeyValue, Message);
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                //this.WriteLog(-1, entity, null, KeyValue, "操作失败：" + ex.Message);
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 删除（销毁）文件
        /// </summary>
        /// <param name="FolderId"></param>
        /// <returns></returns>
        public ActionResult DeleteProjectInfo(string KeyValue)
        {
            try
            {
                List<int> ids = new List<int>();
                int ProjectId = Convert.ToInt32(KeyValue);
                ids.Add(ProjectId);
                ProjectInfoCurrent.Remove(ids);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage
                {
                    Success = false,
                    Code = "-1",
                    Message = "操作失败：" + ex.Message
                }.ToString());
            }
        }
        #endregion
    }
}
