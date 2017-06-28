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
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 构件需求
    /// </summary>
    public class ProjectManagementController : MvcControllerBase
    {
        //
        // GET: /Hello/
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public TreeIBLL TreeCurrent { get; set; }
        [Inject]
        public ProjectInfoIBLL ProjectInfoCurrent { get; set; }
        [Inject]
        public FileIBLL MemberLibraryCurrent { get; set; }
        [Inject]
        public ProjectManagementIBLL ProjectManagementCurrent { get; set; }
        [Inject]
        public OrderManagementIBLL OrderManagementCurrent { get; set; }
        [Inject]
        public RawMaterialIBLL RawMaterialCurrent { get; set; }
        [Inject]
        public ShipManagementIBLL ShipManagementCurrent { get; set; }
        [Inject]
        public ProjectWarehouseIBLL ProjectWarehouseCurrent { get; set; }

        [Inject]
        public MemberUnitIBLL MemberUnitCurrent { get; set; }

        [Inject]
        public OrderMemberIBLL OrderMemberCurrent { get; set; }

        /// <summary>
        /// 订单首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询页面
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPage()
        {
            return View();
        }

        /// <summary>
        /// 【项目需求管理】返回树JONS
        /// </summary>
        /// <returns></returns>      
        public ActionResult TreeJson(string ItemId)
        {
            List<RMC_Tree> list, list1, list2;
            list1 = TreeCurrent.Find(t => t.DeleteFlag != 1 && t.ItemClass == 0).ToList();
            list2 = TreeCurrent.Find(t => t.ModuleId == ItemId && t.DeleteFlag != 1 && t.ItemClass == 2).ToList();
            list = list1.Concat(list2).Distinct().ToList();
            //list = list1;

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
                tree.ismenu = item.IsMenu.ToString();
                tree.url = item.Url;
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

        #region 项目构件需求管理
     
        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="TreeId"></param>
        /// <param name="jqgridparam"></param>
        /// <param name="IsPublic"></param>
        /// <param name="ParameterJson"></param>
        /// <returns></returns>
        public ActionResult GridListJson(FileViewModel model, string TreeId, Pagination jqgridparam, string IsPublic, string ParameterJson)
        {
            try
            {
                #region 查询条件拼接
                if (ParameterJson != null)
                {
                    if (ParameterJson != "[{\"MemberModel\":\"\",\"InBeginTime\":\"\",\"InEndTime\":\"\"}]")
                    {
                        List<FileViewModel> query_member =LeaRun.Util.Json.ToList<FileViewModel>(ParameterJson);
                        for (int i = 0; i < query_member.Count(); i++)
                        {
                            model.MemberModel = query_member[i].MemberModel;
                            model.InBeginTime = query_member[i].InBeginTime;
                            model.InEndTime = query_member[i].InEndTime;
                        }
                    }
                }
                Expression<Func<RMC_ProjectDemand, bool>> func = ExpressionExtensions.True<RMC_ProjectDemand>();
                Func<RMC_ProjectDemand, bool> func1 = f => f.TreeId != "";

                var _a = model.MemberModel != null && model.MemberModel.ToString() != "";
                var _b = model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00";
                var _c = model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00";

                if (_a && _b && _c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime;
                }
                else if (_a&&!_b&&!_c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel));
                    func1 = f => f.MemberModel.Contains(model.MemberModel);
                }
                else if (_b&&!_a&&!_c)
                {
                    func = func.And(f => f.CreateTime >= model.InBeginTime);
                    func1 = f => f.CreateTime >= model.InBeginTime;
                }
                else if (_c&&!_a&&!_b)
                {
                    func = func.And(f => f.CreateTime <= model.InEndTime);
                    func1 = f => f.CreateTime <= model.InEndTime;
                }
                else if (_a && _b&&!_c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.CreateTime >= model.InBeginTime);
                      func1 = f => f.MemberModel.Contains(model.MemberModel) && f.CreateTime >= model.InBeginTime;
                }
                else if (_a && _c&&!_b)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.CreateTime <= model.InEndTime);
                      func1 = f => f.MemberModel.Contains(model.MemberModel) && f.CreateTime <= model.InEndTime;
                }
                else if (_b && _c&&!_a)
                {
                    func = func.And(f => f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime);
                      func1 = f => f.CreateTime >= model.InBeginTime && f.CreateTime <= model.InEndTime;
                }
                #endregion

                var ProjectDemandList_ = new List<RMC_ProjectDemand>();
                var ProjectDemandModelList_ = new List<ProjectDemandModel>();

                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                List<RMC_ProjectDemand> ProjectDemandList = new List<RMC_ProjectDemand>();
                if (TreeId == "")
                {
                    func.And(f => f.DeleteFlag != 1 & f.ProjectDemandId > 0);

                    ProjectDemandList = ProjectDemandList_ = ProjectManagementCurrent.FindPage<string>(jqgridparam.page
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
                        var _ProjectDemandList = ProjectManagementCurrent.Find(m => m.TreeId.ToString() == item.TreeID).ToList();
                        if (_ProjectDemandList.Count() > 0)
                        {
                            ProjectDemandList = ProjectDemandList.Concat(_ProjectDemandList).ToList();
                        }
                    }

                    ProjectDemandList = ProjectDemandList.Where(func1).ToList();
                    ProjectDemandList_ = ProjectDemandList.Take(jqgridparam.rows * jqgridparam.page).Skip(jqgridparam.rows * (jqgridparam.page - 1)).ToList();
                    total = ProjectDemandList.Count();
                }

                foreach (var item in ProjectDemandList)
                {
                    ProjectDemandModel projectdemand = new ProjectDemandModel();
                    projectdemand.ProjectDemandId = item.ProjectDemandId;
                    var memberunit = MemberUnitCurrent.Find(f => f.UnitId == item.UnitId).SingleOrDefault();
                    var memberlibrary = MemberLibraryCurrent.Find(f => f.MemberID == item.MemberId).SingleOrDefault();
                    projectdemand.MemberName = memberlibrary.MemberName;
                    projectdemand.MemberModel = memberlibrary.MemberModel;
                    projectdemand.Icon = memberlibrary.Icon;
                    projectdemand.CreateTime = item.CreateTime;
                    projectdemand.MemberUnit = memberunit.UnitName;
                    projectdemand.UnitPrice = memberlibrary.UnitPrice;
                    projectdemand.MemberId = memberlibrary.MemberID;
                    projectdemand.MemberNumbering = memberlibrary.MemberNumbering.ToString();
                    projectdemand.IsReview = item.IsReview;
                    projectdemand.ReviewMan = item.ReviewMan;
                    projectdemand.MemberNumber = item.MemberNumber;
                    projectdemand.OrderQuantityed = item.OrderQuantityed;
                    projectdemand.Productioned = item.Productioned;
                    projectdemand.ProductionNumber = item.ProductionNumber;
                    projectdemand.CollarNumbered = item.CollarNumbered;
                    projectdemand.Description = item.Description;
                    ProjectDemandModelList_.Add(projectdemand);
                }

                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = ProjectDemandModelList_.OrderByDescending(f => f.CreateTime),
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取树字节子节点(自循环)
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns></returns>
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

        ///// <summary>
        ///// 上传用户头像
        ///// </summary>
        ///// <param name="Filedata">用户图片对象</param>
        ///// <returns></returns>
        //public ActionResult SubmitUploadify(HttpPostedFileBase Filedata)
        //{
        //    try
        //    {
        //        Thread.Sleep(1000);////延迟500毫秒
        //        //没有文件上传，直接返回
        //        if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
        //        {
        //            return HttpNotFound();
        //        }
        //        //获取文件完整文件名(包含绝对路径)
        //        //文件存放路径格式：/Resource/Document/NetworkDisk/{日期}/{guid}.{后缀名}
        //        //例如：/Resource/Document/Email/20130913/43CA215D947F8C1F1DDFCED383C4D706.jpg
        //        string fileGuid = CommonHelper.GetGuid;
        //        long filesize = Filedata.ContentLength;
        //        string FileEextension = Path.GetExtension(Filedata.FileName);
        //        string uploadDate = DateTime.Now.ToString("yyyyMMdd");
        //        string UserId = ManageProvider.Provider.Current().UserId;

        //        string virtualPath = string.Format("/Content/Images/Avatar/{0}/{1}/{2}{3}", UserId, uploadDate, fileGuid, FileEextension);
        //        string fullFileName = this.Server.MapPath(virtualPath);
        //        //创建文件夹，保存文件
        //        string path = Path.GetDirectoryName(fullFileName);
        //        Directory.CreateDirectory(path);
        //        if (!System.IO.File.Exists(fullFileName))
        //        {
        //            Filedata.SaveAs(fullFileName);
        //        }
        //        return Content(virtualPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
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
            //List<ProjectDemandModel> projectdemandlist =null;
            ProjectDemandModel projectdemand = new ProjectDemandModel();
            if (KeyValue != "")
            {
                int ProjectDemandId = Convert.ToInt32(KeyValue);
                var entity = ProjectManagementCurrent.Find(f => f.ProjectDemandId == ProjectDemandId).SingleOrDefault();
                var entity1 = MemberLibraryCurrent.Find(f => f.MemberID == entity.MemberId).SingleOrDefault();
                var entity_tree = TreeCurrent.Find(f => f.TreeID == entity.MemberClassId.ToString()).SingleOrDefault();
                projectdemand.TreeName = entity.TreeName;
                projectdemand.ProjectDemandId = ProjectDemandId;
                projectdemand.MemberClassId = entity.MemberClassId;
                projectdemand.MemberClassName = entity_tree.TreeName;
                projectdemand.MemberId = entity1.MemberID;
                projectdemand.UnitId = entity.UnitId;
                projectdemand.MemberModel = entity1.MemberModel;
                projectdemand.MemberNumber = entity.MemberNumber;
                projectdemand.Description = entity.Description;
                var filename = entity1.CAD_Drawing.Substring(0, entity1.CAD_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath = "../../Resource/Document/NetworkDisk/System/Member/" + filename + "/";
                var filename1 = entity1.Model_Drawing.Substring(0, entity1.Model_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath1 = "../../Resource/Document/NetworkDisk/System/Member/" + filename1 + "/";
                projectdemand.CADDrawing = virtualPath + entity1.CAD_Drawing;
                projectdemand.ModelDrawing = virtualPath1 + entity1.Model_Drawing;
            }


            return Content(projectdemand.ToJson());
            //return Json(entity);
        }

        /// <summary>
        /// 提交文件夹表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <param name="TreeID">外键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult SubmitDataForm(RMC_ProjectDemand entity, string KeyValue, string TreeID)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int keyvalue = Convert.ToInt32(KeyValue);
                    RMC_ProjectDemand Oldentity = ProjectManagementCurrent.Find(t => t.ProjectDemandId == keyvalue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.MemberId = entity.MemberId;
                    Oldentity.UnitId = entity.UnitId;
                    Oldentity.MemberClassId = entity.MemberClassId;
                    Oldentity.MemberNumber = entity.MemberNumber;
                    Oldentity.MemberWeight = entity.MemberWeight;
                    Oldentity.MemberCompanyId = entity.MemberCompanyId;
                    Oldentity.Description = entity.Description;
                    Oldentity.IsReview = 0;
                    ProjectManagementCurrent.Modified(Oldentity);
                }
                else
                {
                    int treeid = Convert.ToInt32(TreeID);
                    RMC_ProjectDemand Oldentity = new RMC_ProjectDemand();
                    Oldentity.TreeId = TreeID;
                    Oldentity.MemberClassId = entity.MemberClassId;
                    Oldentity.TreeName = entity.TreeName;
                    Oldentity.MemberId = entity.MemberId;
                    var Member = MemberLibraryCurrent.Find(f => f.MemberID == entity.MemberId).SingleOrDefault();
                    Oldentity.MemberNumbering = Member.MemberNumbering;
                    Oldentity.MemberModel = Member.MemberModel;
                    Oldentity.UnitId = entity.UnitId;
                    Oldentity.IsSubmit = 0;
                    Oldentity.IsDemandSubmit = 0;
                    Oldentity.IsReview = 0;
                    Oldentity.OrderQuantityed = 0;
                    Oldentity.CreateTime = DateTime.Now;
                    Oldentity.MemberNumber = entity.MemberNumber;
                    Oldentity.MemberWeight = entity.MemberWeight;
                    Oldentity.MemberCompanyId = entity.MemberCompanyId;
                    Oldentity.Description = entity.Description;
                    ProjectManagementCurrent.Add(Oldentity);
                }
                return Success (Message);
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
        public ActionResult DeleteProjectDemand(string KeyValue)
        {
            try
            {
                List<int> ids = new List<int>();
                int ProjectDemandId = Convert.ToInt32(KeyValue);
                ids.Add(ProjectDemandId);
                ProjectManagementCurrent.Remove(ids);
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 获取分项目名称
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult GetTreeName(string KeyValue)
        {
            int TreeId = Convert.ToInt32(KeyValue);
            RMC_Tree entity = TreeCurrent.Find(f => f.TreeID == KeyValue).SingleOrDefault();
            //string JsonData = entity.ToJson();
            ////自定义
            //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(entity.ToJson());
            //return Json(entity);
        }

        /// <summary>
        /// 获取构件名称
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="TreeId"></param>
        /// <returns></returns>
        public ActionResult GetMemderName(string KeyValue,string TreeId)
        {
            var Entitys=new List<RMC_MemberLibrary>();
            if (KeyValue != "")
            {
                List<string> MemberModel1 = new List<string>();
                List<string> MemberModel2 = new List<string>();

                int _TreeId = Convert.ToInt32(TreeId);
                int treeid = Convert.ToInt32(KeyValue);
                var Entity = MemberLibraryCurrent.Find(f => f.TreeId == TreeId).ToList();
                if (Entity.Count()>0)
                {
                    foreach (var item in Entity)
                    {
                        MemberModel1.Add(item.MemberModel);
                    }
                }
                 var Entity1 = ProjectManagementCurrent.Find(f=>f.TreeId== TreeId && f.MemberClassId== treeid).ToList();
                if (Entity1.Count()>0)
                {
                    foreach (var item1 in Entity1)
                    {
                        MemberModel2.Add(item1.MemberModel);
                    }
                }
                var MemberModel3=MemberModel1.Where(c => !MemberModel2.Contains(c)).ToList();
                foreach (var item in MemberModel3)
                {
                    var Model= Entity.Where(f=>f.MemberModel==item).SingleOrDefault();
                    Entitys.Add(Model);
                }
                
            }
            return Json(Entitys);
        }

        /// <summary>
        /// 获取构件单位
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMemderUnit()
        {
            List<RMC_MemberUnit> Entity = null;
            Entity = MemberUnitCurrent.Find(f => f.UnitId != 0).ToList();
            return Content(Entity.ToJson());

        }

        /// <summary>
        /// 获取构件图纸模型
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult GetMemderDrawing(string KeyValue)
        {
            ProjectDemandModel projectdemand = new ProjectDemandModel();
            if (KeyValue != "")
            {
                int memberid = Convert.ToInt32(KeyValue);
                var Entity = MemberLibraryCurrent.Find(f => f.MemberID == memberid).SingleOrDefault();
                var filename = Entity.CAD_Drawing.Substring(0, Entity.CAD_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath = "../../Resource/Document/NetworkDisk/System/Member/" + filename + "/";
                var filename1 = Entity.Model_Drawing.Substring(0, Entity.Model_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath1 = "../../Resource/Document/NetworkDisk/System/Member/" + filename1 + "/";

                projectdemand.CADDrawing = virtualPath + Entity.CAD_Drawing;
                projectdemand.ModelDrawing = virtualPath1 + Entity.Model_Drawing;
            }
            return Json(projectdemand);
        }
        #endregion

        #region 审核需求

        /// <summary>
        /// 审核需求
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="IsReview"></param>
        /// <returns></returns>
        public ActionResult ReviewProjectDemand(string KeyValue, string IsReview)
        {
            try
            {
                string Message = IsReview == "2" ? "驳回成功。" : "审核成功。";
                int ProjectDemandId = Convert.ToInt32(KeyValue);
                var file = ProjectManagementCurrent.Find(f => f.ProjectDemandId == ProjectDemandId).First();
                file.ModifiedTime = DateTime.Now;
                file.ReviewMan = OperatorProvider.Provider.Current().UserName;
                file.IsReview = Convert.ToInt32(IsReview);
                ProjectManagementCurrent.Modified(file);
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 批量提交项目
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public ActionResult SubmitProjectItemDemand(string KeyValue, RMC_ProjectDemand Entity)
        {
            var Message = "";
            try
            {
                int TreeId = Convert.ToInt32(KeyValue);
                List<RMC_ProjectDemand> OldEntity = ProjectManagementCurrent.Find(f => f.TreeId == KeyValue).ToList();
                if (OldEntity.Count() > 0)
                {
                    for (int i = 0; i < OldEntity.Count(); i++)
                    {

                        ProjectManagementCurrent.Find(f => f.ProjectDemandId == OldEntity[i].ProjectDemandId).SingleOrDefault();
                        Entity.ModifiedTime = DateTime.Now;
                        Entity.IsSubmit = 1;
                        ProjectManagementCurrent.Modified(Entity);
                        RMC_ProjectOrder Entity1 = new RMC_ProjectOrder();
                        Entity.ProjectDemandId = OldEntity[i].ProjectDemandId;
                        OrderManagementCurrent.Add(Entity1);

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
                    Message = "该项目无项目信息";
                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion
    }
}
