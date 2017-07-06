using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    using System.Text;
    using System.IO;
    using System.Threading;
    using System.Linq.Expressions;
    using System.Collections.Specialized;
    using System.Data;
    using System.Reflection;
    using System.Diagnostics;
    using System.Collections;
    using SteelMember.Controllers;
    using System.Data.OleDb;
    using Ninject;
    using LeaRun.Application.Repository.SteelMember.IBLL;
    using LeaRun.Data.Entity;
    using LeaRun.Util.WebControl;
    using LeaRun.Util;
    using LeaRun.Application.Web;
    using LeaRun.Util.Offices;
    using LeaRun.Application.Busines.BaseManage;
    using LeaRun.Application.Code;
    using LeaRun.Application.Web.Areas.SteelMember.Models;
    using LeaRun.Util.Extension;
    using LeaRun.Application.Busines.SteelMember;
    using LeaRun.Application.Entity.SteelMember;

    public class FileController : MvcControllerBase
    {

        private UserBLL userBLL = new UserBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();
        [Inject]
        public TreeIBLL TreeCurrent { get; set; }
        [Inject]
        public FileIBLL MemberLibraryCurrent { get; set; }
        [Inject]
        public ProjectInfoIBLL ProjectInfoCurrent { get; set; }
        [Inject]
        public MemberMaterialIBLL MemberMaterialCurrent { get; set; }
        [Inject]
        public RawMaterialIBLL RawMaterialLibraryCurrent { get; set; }
        [Inject]
        public MemberUnitIBLL MemberUnitCurrent { get; set; }
        [Inject]
        public MemberProcessIBLL MemberProcessCurrent { get; set; }

        [Inject]
        public ProcessManagementIBLL ProcessManagementCurrent { get; set; }

        


        #region 试图功能

        /// <summary>
        /// 数据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            Session["moduleId"] = Request.QueryString["moduleId"];
            return View();
        }

        /// <summary>
        /// 表单视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public virtual ActionResult Form()
        {
            return View();
        }


        #endregion

        #region 获取数据
        /// <summary>
        /// 获取工程树JONS
        /// </summary>
        /// <returns></returns>      
        [HttpGet]
        public ActionResult SubProjectTreeJson(int Levels)
        {
            
            var data = subprojectbll.GetList(null).ToList();
            data = data.FindAll(t => t.Levels == Levels);
            List<TreeEntity> TreeList = new List<TreeEntity>();
            foreach (SubProjectEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;
                List<SubProjectEntity> childnode = data.FindAll(t => t.ParentId == item.Id);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.Id;
                tree.isexpand = true; //item.State == 1 ? true : false;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                //tree.iconCls = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
        /// <summary>
        /// 获取构件类型
        /// </summary>
        /// <param name="Levels"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberClassJson(int Levels)
        {

            var data = subprojectbll.GetList(null).ToList();
            data = data.FindAll(t => t.Levels == Levels);
            List<TreeEntity> TreeList = new List<TreeEntity>();
            foreach (SubProjectEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;
                List<SubProjectEntity> childnode = data.FindAll(t => t.ParentId == item.Id);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.Id;
                tree.isexpand = true; //item.State == 1 ? true : false;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                //tree.iconCls = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }


        /// <summary>
        /// 获取原材料
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public virtual ActionResult GetRawMaterialJson(string KeyValue)
        {

            var RawMaterial = RawMaterialLibraryCurrent.Find(f => f.TreeId == KeyValue).ToList();
            return Content(RawMaterial.ToJson());
        }


        #region 验证数据
        /// <summary>
        ///构件不能重复
        /// </summary>
        /// <param name="MemberName">型号</param>
        /// <param name="MemberId"></param>
        /// <param name="SubProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMember(string MemberName, string MemberId, string SubProjectId)
        {
            bool IsOk = false;

            var expression = LinqExtensions.True<RMC_MemberLibrary>();
            expression = expression.And(t => t.MemberModel.Trim() == MemberName.Trim() && t.SubProjectId.Trim() == SubProjectId.Trim());
            if (!string.IsNullOrEmpty(MemberId))
            {
                expression = expression.And(t => t.MemberName.ToString() != MemberName);
            }

            IsOk = MemberLibraryCurrent.Find(expression).Count() == 0 ? true : false;
            return Content(IsOk.ToString());
        }
        #endregion
        ///// <summary>
        ///// 获取原材料
        ///// </summary>
        ///// <param name="MaterialClassId"></param>
        ///// <param name="MemberId"></param>
        ///// <returns></returns>
        //public virtual ActionResult GetMaterialName(string MaterialClassId, string MemberId)
        //{
        //    var Entitys = new List<RMC_RawMaterialLibrary>();
        //    if (MaterialClassId != "")
        //    {//筛选出还没有添加至该构件的材料
        //        List<string> MemberRawMaterial1 = new List<string>();
        //        List<string> MemberRawMaterial2 = new List<string>();

        //        int _MemberId = Convert.ToInt32(MemberId);
        //        var Entity = RawMaterialCurrent.Find(f => f.TreeId == MaterialClassId).ToList();
        //        if (Entity.Count() > 0)
        //        {
        //            foreach (var item in Entity)
        //            {
        //                MemberRawMaterial1.Add(item.RawMaterialId.ToString());
        //            }
        //        }
        //        var Entity1 = MemberMaterialCurrent.Find(f => f.MemberId == _MemberId).ToList();
        //        if (Entity1.Count() > 0)
        //        {
        //            foreach (var item1 in Entity1)
        //            {
        //                MemberRawMaterial2.Add(item1.RawMaterialId.ToString());
        //            }
        //        }
        //        var MemberRawMaterial3 = MemberRawMaterial1.Where(c => !MemberRawMaterial2.Contains(c)).ToList();
        //        foreach (var item in MemberRawMaterial3)
        //        {
        //            int _item = Convert.ToInt32(item);
        //            var Model = Entity.Where(f => f.RawMaterialId == _item).SingleOrDefault();
        //            Entitys.Add(Model);
        //        }
        //        //
        //    }
        //    return Json(Entitys);
        //}
        #endregion

        /// <summary>
        /// 【控制测量文档管理】返回对象JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult SetForm(string KeyValue)
        {
            string JsonData = "";
            if (KeyValue != "")
            {
                RMC_MemberLibrary entity = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();
                JsonData = entity.ToJson();
            }
            //自定义
            //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(JsonData);
        }
        /// <summary>
        /// 提交文件表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult SubmitData(RMC_MemberLibrary entity, string KeyValue)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    RMC_MemberLibrary Oldentity = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.MemberName = entity.MemberName;//给旧实体重新赋值
                    Oldentity.ModifiedTime = DateTime.Now;
                    MemberLibraryCurrent.Modified(Oldentity);
                }
                else
                {
                    entity.IsProcess = 0;
                    entity.IsRawMaterial = 0;
                    MemberLibraryCurrent.Add(entity);
                }
                return Success(Message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 删除（销毁）文件
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public ActionResult DeleteFile(string MemberID)
        {
            try
            {
                //删除构件
                var ids = new List<string>();
                ids.Add(MemberID);
                MemberLibraryCurrent.Remove_str(ids);
                //

                //删除构件原材料

                var MemberMaterial = MemberMaterialCurrent.Find(f => f.MemberId == MemberID).ToList();
                if (MemberMaterial.Count() > 0)
                {
                    var ids1 = new List<int>();
                    for (int i = 0; i < MemberMaterial.Count(); i++)
                    {
                        ids1.Add(Convert.ToInt32(MemberMaterial[i].MemberId));
                    }
                    MemberMaterialCurrent.Remove(ids1);
                }
                //

                //删除构件制程
                var MemberProcess = MemberProcessCurrent.Find(f => f.MemberId == MemberID).ToList();
                if (MemberProcess.Count() > 0)
                {
                    List<int> ids2 = new List<int>();
                    for (int i = 0; i < MemberProcess.Count(); i++)
                    {
                        ids2.Add(Convert.ToInt32(MemberProcess[i].MemberId));
                    }
                    MemberProcessCurrent.Remove(ids2);
                }
                //

                return Success("删除成功");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #region 数据查询与呈现
        /// <summary>
        /// 
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
                        List<FileViewModel> query_member = LeaRun.Util.Json.ToList<FileViewModel>(ParameterJson);
                        for (int i = 0; i < query_member.Count(); i++)
                        {
                            model.MemberModel = query_member[i].MemberModel;
                            model.InBeginTime = query_member[i].InBeginTime;
                            model.InEndTime = query_member[i].InEndTime;
                        }
                    }
                }

                Expression<Func<RMC_MemberLibrary, bool>> func = ExpressionExtensions.True<RMC_MemberLibrary>();
                Func<RMC_MemberLibrary, bool> func1 = f => f.SubProjectId != "";

                var _a = model.MemberModel != null && model.MemberModel.ToString() != "";
                var _b = model.InBeginTime != null && model.InBeginTime.ToString() != "0001/1/1 0:00:00";
                var _c = model.InEndTime != null && model.InEndTime.ToString() != "0001/1/1 0:00:00";

                if (_a && _b && _c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.UploadTime >= model.InBeginTime && f.UploadTime <= model.InEndTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.UploadTime >= model.InBeginTime && f.UploadTime <= model.InEndTime;
                }
                else if (_a && !_b && !_c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel));
                    func1 = f => f.MemberModel.Contains(model.MemberModel);
                }
                else if (_b && !_a && !_c)
                {
                    func = func.And(f => f.UploadTime >= model.InBeginTime);
                    func1 = f => f.UploadTime >= model.InBeginTime;
                }
                else if (_c && !_b && !_a)
                {
                    func = func.And(f => f.UploadTime <= model.InEndTime);
                    func1 = f => f.UploadTime <= model.InEndTime;
                }
                else if (_a && _b && !_c)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.UploadTime >= model.InBeginTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.UploadTime >= model.InBeginTime;
                }
                else if (_a && _c && !_b)
                {
                    func = func.And(f => f.MemberModel.Contains(model.MemberModel) && f.UploadTime <= model.InEndTime);
                    func1 = f => f.MemberModel.Contains(model.MemberModel) && f.UploadTime <= model.InEndTime;
                }
                else if (_b && _c && !_a)
                {
                    func = func.And(f => f.UploadTime >= model.InBeginTime && f.UploadTime <= model.InEndTime);
                    func1 = f => f.UploadTime >= model.InBeginTime && f.UploadTime <= model.InEndTime;
                }
                #endregion

                var MemberList_ = new List<RMC_MemberLibrary>();
                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                List<RMC_MemberLibrary> MemberList = new List<RMC_MemberLibrary>();
                if (TreeId == "" || TreeId == null)
                {
                    func.And(f => f.MemberId !=""|| f.MemberId !=null);
                    MemberList = MemberList_ = MemberLibraryCurrent.FindPage<string>(jqgridparam.page
                                             , jqgridparam.rows
                                             , func
                                             , false
                                             , f => f.UploadTime.ToString()
                                             , out total
                                             ).ToList();
                }
                else
                {
                    var list = GetSonId(TreeId).ToList();

                    list.Add(TreeCurrent.Find(p => p.TreeID == TreeId).Single());

                    foreach (var item in list)
                    {
                        var _MemberList = MemberLibraryCurrent.Find(m => m.SubProjectId == item.TreeID).ToList();
                        if (_MemberList.Count() > 0)
                        {
                            MemberList = MemberList.Concat(_MemberList).ToList();
                        }
                    }

                    MemberList = MemberList.Where(func1).OrderByDescending(f => f.UploadTime).ToList();
                    MemberList_ = MemberList.Take(jqgridparam.rows * jqgridparam.page).Skip(jqgridparam.rows * (jqgridparam.page - 1)).ToList();
                    total = MemberList.Count();
                }
                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = MemberList_,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        //获取树子节点(自循环)
        public IEnumerable<RMC_Tree> GetSonId(string p_id)
        {
            List<RMC_Tree> list = TreeCurrent.Find(p => p.ParentID == p_id).ToList();
            return list.Concat(list.SelectMany(t => GetSonId(t.TreeID)));
        }

        //获取树父节点(自循环)
        public IEnumerable<RMC_Tree> GetParentId(string p_id)
        {
            List<RMC_Tree> list = TreeCurrent.Find(p => p.TreeID == p_id).ToList();
            return list.Concat(list.SelectMany(t => GetParentId(t.TreeID)));
        }
        #endregion

        #region Excel 数据导入
        // 将Excel导入数据库  

        public ActionResult ImportFile()
        {
            return View();
        }

        public ContentResult SubmitImportFile(string KeyValue, RMC_MemberLibrary MemberLibrary)
        {
            try
            {
                HttpFileCollectionBase Filedatas = Request.Files;

                //HttpPostedFileBase file = Request.Files["FileUpload"];//获取上传的文件  
                string FileName;
                string savePath;
                string Photo = "";
                //int IsOk = 1;
                if (Filedatas.Count == 0)
                {
                    return Content("文件不能为空");
                }
                else
                {
                    var Filedata = Request.Files[0];
                    string filename = Path.GetFileName(Filedata.FileName);
                    int filesize = Filedata.ContentLength;//获取上传文件的大小单位为字节byte  
                    string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名  
                    string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名  
                    int Maxsize = 10000 * 1024;//定义上传文件的最大空间大小为10M  
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串  

                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmssffff") + fileEx;
                    if (!FileType.Contains(fileEx))
                    {
                        return Content("文件类型不对，只能导入xls和xlsx格式的文件");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content("上传的文件不能超过10M");
                    }
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "uploads\\excel\\";
                    string virtualPath = string.Format("~/Resource/Document/NetworkDisk/Excel");
                    string fullFileName = this.Server.MapPath(virtualPath);
                    //如果文件存在，则删除
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~") + "~/Resource/Document/NetworkDisk/Excel"))
                    {

                        Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + "~/Resource/Document/NetworkDisk/Excel", true);//pdf路径
                    }
                    //如果文件不存在，则从新创建，文件夹以(yyyy-MM-d)的格式创建
                    if (!Directory.Exists(fullFileName))
                    {
                        Directory.CreateDirectory(fullFileName);//创建swf路径
                    }
                    savePath = Path.Combine(fullFileName, FileName);
                    Filedata.SaveAs(savePath);
                }
                string result = string.Empty;
                string strConn;
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + savePath + "; " + "Extended Properties=Excel 8.0;";  
                strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + savePath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'"; //此连接可以操作.xls与.xlsx文件 (支持Excel2003 和 Excel2007 的连接字符串)  

                //OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);  
                //连接串  
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等　  
                DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                //包含excel中表名的字符串数组  
                string[] strTableNames = new string[dtSheetName.Rows.Count];
                for (int k = 0; k < dtSheetName.Rows.Count; k++)
                {
                    strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                }
                OleDbDataAdapter myCommand = null;
                DataTable dt = new DataTable();
                //从指定的表明查询数据,可先把所有表明列出来供用户选择  
                string strExcel = "select*from[" + strTableNames[0] + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                //myCommand.Fill(dt);  
                DataSet myDataSet = new DataSet();
                myCommand.Fill(myDataSet, "ExcelInfo");
                // Data.Deleted();
                DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
                if (table.Columns.Count != 37)
                {
                    return Content("文件数据格式不正确");
                }
                int count = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i].IsNull(0))
                    {
                        count++;
                    }
                }
                if (table.Rows.Count == count)
                {
                    return Content("文件不包含任何数据");
                }
                else
                {
                    int _count = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (!table.Rows[i].IsNull(0))
                        {
                            string MemberModel = table.Rows[i][0].ToString();
                            var _MemberLibrary = MemberLibraryCurrent.Find(f => f.MemberModel == MemberModel).SingleOrDefault();
                            if (_MemberLibrary == null)
                            {
                                MemberLibrary.SubProjectId = KeyValue;
                                var tree = TreeCurrent.Find(f => f.TreeID == KeyValue).SingleOrDefault();
                                MemberLibrary.MemberName = tree.TreeName;
                                MemberLibrary.UploadTime = DateTime.Now;
                                MemberLibrary.MemberModel = table.Rows[i][0].ToString();
                                MemberModel = MemberLibrary.MemberModel;
                                char[] Number = MemberModel.ToArray();
                                string MemberNumbering = "";
                                for (int I = 0; I < Number.Length; I++)
                                {
                                    if (("0123456789").IndexOf(Number[I] + "") != -1)
                                    {
                                        MemberNumbering += Number[I];
                                    }
                                }

                                MemberLibrary.MemberNumbering = (MemberNumbering + "-" + DateTime.Now.ToString("yyyyMMddhhmmssffff")).ToString();
                                //MemberLibrary.SectionalArea = Convert.ToDecimal(table.Rows[i][1]);
                                //MemberLibrary.SurfaceArea = Convert.ToDecimal(table.Rows[i][2]);
                                //MemberLibrary.TheoreticalWeight = table.Rows[i][3].ToString();
                                //MemberLibrary.SectionalSize_h = Convert.ToInt32(table.Rows[i][4]);
                                //MemberLibrary.SectionalSizeB = Convert.ToInt32(table.Rows[i][5]);
                                //MemberLibrary.SectionalSize_b = Convert.ToInt32(table.Rows[i][6]);
                                //MemberLibrary.SectionalSizeD = Convert.ToDecimal(table.Rows[i][7]);
                                //MemberLibrary.SectionalSize_d = Convert.ToInt32(table.Rows[i][8]);
                                //MemberLibrary.SectionalSize_t = Convert.ToDecimal(table.Rows[i][9]);
                                //MemberLibrary.SectionalSize_r = Convert.ToDecimal(table.Rows[i][10]);
                                //MemberLibrary.SectionalSize_r1 = Convert.ToDecimal(table.Rows[i][11]);
                                //MemberLibrary.InertiaDistance_x = Convert.ToDecimal(table.Rows[i][12]);
                                //MemberLibrary.InertiaDistance_y = Convert.ToDecimal(table.Rows[i][13]);
                                //MemberLibrary.InertiaDistance_x0 = Convert.ToDecimal(table.Rows[i][14]);
                                //MemberLibrary.InertiaDistance_y0 = Convert.ToDecimal(table.Rows[i][15]);
                                //MemberLibrary.InertiaDistance_x1 = Convert.ToDecimal(table.Rows[i][16]);
                                //MemberLibrary.InertiaDistance_y1 = Convert.ToDecimal(table.Rows[i][17]);
                                //MemberLibrary.InertiaDistance_u = Convert.ToDecimal(table.Rows[i][18]);
                                //MemberLibrary.InertiaRadius_x = Convert.ToDecimal(table.Rows[i][19]);
                                //MemberLibrary.InertiaRadius_x0 = Convert.ToDecimal(table.Rows[i][20]);
                                //MemberLibrary.InertiaRadius_y = Convert.ToDecimal(table.Rows[i][21]);
                                //MemberLibrary.InertiaRadius_y0 = Convert.ToDecimal(table.Rows[i][22]);
                                //MemberLibrary.InertiaRadius_u = Convert.ToDecimal(table.Rows[i][23]);
                                //MemberLibrary.SectionalModulus_x = Convert.ToDecimal(table.Rows[i][24]);
                                //MemberLibrary.SectionalModulus_y = Convert.ToDecimal(table.Rows[i][25]);
                                //MemberLibrary.SectionalModulus_x0 = Convert.ToDecimal(table.Rows[i][26]);
                                //MemberLibrary.SectionalModulus_y0 = Convert.ToDecimal(table.Rows[i][27]);
                                //MemberLibrary.SectionalModulus_u = Convert.ToDecimal(table.Rows[i][28]);
                                //MemberLibrary.GravityCenterDistance_0 = Convert.ToDecimal(table.Rows[i][29]);
                                //MemberLibrary.GravityCenterDistance_x0 = Convert.ToDecimal(table.Rows[i][30]);
                                //MemberLibrary.GravityCenterDistance_y0 = Convert.ToDecimal(table.Rows[i][31]);
                                MemberLibrary.MemberUnit = table.Rows[i][32].ToString();
                                MemberLibrary.UnitPrice = table.Rows[i][33].ToString();
                                string CAD_Drawing = "1.png";
                                string Model_Drawing = "1.png";
                                string Icon = "1.png";
                                if (table.Rows[i][34].ToString() != "")
                                {
                                    CAD_Drawing = table.Rows[i][34].ToString().Trim();
                                    if (CAD_Drawing != "1.png")
                                    {
                                        Photo += CAD_Drawing + "、";

                                    }
                                }
                                if (table.Rows[i][35].ToString() != "")
                                {
                                    Model_Drawing = table.Rows[i][35].ToString().Trim();
                                    if (Model_Drawing != "1.png")
                                    {
                                        Photo += Model_Drawing + "、";
                                    }
                                }
                                if (table.Rows[i][36].ToString() != "")
                                {
                                    Icon = table.Rows[i][36].ToString().Trim();
                                    if (Icon != "1.png")
                                    {
                                        Photo += Icon + "、";
                                    }
                                }

                                MemberLibrary.CAD_Drawing = CAD_Drawing;
                                MemberLibrary.Model_Drawing = Model_Drawing;
                                MemberLibrary.Icon = Icon;
                                MemberLibrary.IsProcess = 0;
                                MemberLibrary.IsRawMaterial = 0;
                                MemberLibraryCurrent.Add(MemberLibrary);
                                // Data.AddData(data);
                            }
                            else
                            {
                                _count++;
                            }
                        }
                    }
                    if (table.Rows.Count - count == _count || (_count == 1 && table.Rows.Count - count == _count))
                    {
                        return Content("操作失败：要导入的数据在该构件类型或其他构件类型下已存在");
                    }
                }
                Session["photo"] = Photo;
                if (Photo == "")
                {
                    return Content("1");
                }
                else
                {
                    return Content("2");
                }

                //return View(Photo);
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }
        #endregion

        #region JqGrid导出Excel
        /// <summary>
        /// 导出Excell模板
        /// </summary>
        /// <returns></returns>
        public void GetExcellTemperature(string ImportId)
        {

            DataTable data = new DataTable();
            string fileName = "导入构件模板.xlsx";
            //string TableHeader = "构件模板";
            string DataColumn = "型号 | 截面面积/cm²| 外表面积/(m²/m)|理论重量/(㎏/m)|";
            DataColumn += "h|B|b|D|d| t|r|r1|Ix | Ix0 | Ix1 | Iy | Iy0 | Iy1 | Iu | ix |";
            DataColumn += " iy | ix0 | iy0 | iu | Wx | Wy | Wx0 | Wy0 | Wu | Z0 | X0 | Yu|单位|单价|图纸|模型|图标";
            //DeriveExcel.DataTableToExcel(data, DataColumn.Split('|'), fileName);
            // ExcelHelper.ExcelDownload(exportTable, excelconfig);
            // userBLL.GetExportList();
        }

        ///// <summary>                                                                                            
        ///// 获取要导出表头字段                                                                                   
        ///// </summary>                                                                                          
        ///// <returns></returns>                                                                                 
        //public ActionResult GetDeriveExcelColumn()
        //{
        //    string JsonColumn = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonColumn_DeriveExcel"));
        //    return Content(JsonColumn);
        //}
        ///// <summary>                                                                                            
        ///// 导出Excel                                                                                            
        ///// </summary>                                                                                           
        ///// <param name="ExportField">要导出字段</param>                                                         
        //public void GetDeriveExcel(string ExportField)
        //{
        //    string JsonColumn = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonColumn_DeriveExcel"));
        //    string JsonData = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonData_DeriveExcel"));
        //    string JsonFooter = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonFooter_DeriveExcel"));
        //    string fileName = GZipHelper.Uncompress(CookieHelper.GetCookie("FileName_DeriveExcel"));
        //    DeriveExcel.JqGridToExcel(JsonColumn, JsonData, ExportField, fileName);
        //}
        ///// <summary>
        ///// 写入数据到Cookie
        ///// </summary>
        ///// <param name="JsonColumn">表头</param>
        ///// <param name="JsonData">数据</param>
        ///// <param name="JsonFooter">底部合计</param>
        //[ValidateInput(false)]
        //public void SetDeriveExcel(string JsonColumn, string JsonData, string JsonFooter, string FileName,string TableHeader)
        //{
        //    CookieHelper.WriteCookie("JsonColumn_DeriveExcel", GZipHelper.Compress(JsonColumn));
        //    CookieHelper.WriteCookie("JsonData_DeriveExcel", GZipHelper.Compress(JsonData));
        //    CookieHelper.WriteCookie("JsonFooter_DeriveExcel", GZipHelper.Compress(JsonFooter));
        //    CookieHelper.WriteCookie("FileName_DeriveExcel", GZipHelper.Compress(FileName));
        //    CookieHelper.WriteCookie("TabelHeader_DeriveExcel", GZipHelper.Compress(TableHeader));
        //}

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult DeriveFile()
        {
            return View();
        }

        #endregion

        #region 文件上传、下载
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            ViewData["_photo"] = Session["photo"].ToString();
            return View();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="Img"></param>
        /// <param name="file"></param>
        /// <param name="Filedata"></param>
        /// <returns></returns>
        public ActionResult SubmitUpLoadFile(string KeyValue, string Img, RMC_MemberLibrary file, HttpPostedFileBase Filedata)/*RMC_MemberLibrary File,  */
        {
            try
            {
                int key_value = Convert.ToInt32(KeyValue);
                RMC_MemberLibrary oldentity = new RMC_MemberLibrary();
                RMC_ProjectInfo oldentity1 = new RMC_ProjectInfo();
                int IsOk = 0;
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return HttpNotFound();
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/Document/NetworkDisk/{日期}/{guid}.{后缀名}
                //例如：/Resource/Document/Email/20130913/43CA215D947F8C1F1DDFCED383C4D706.jpg
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                //string UserId = ManageProvider.Provider.Current().UserId;
                string NewPath = "";
                string virtualPath = "";
                string virtualPath1 = "";
                string fullFileName = "";
                string UserId = "System";
                string filename = Filedata.FileName.Substring(0, Filedata.FileName.LastIndexOf('.'));//获取文件名称，去除后缀名
                oldentity = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();
                oldentity1 = ProjectInfoCurrent.Find(f => f.ProjectId == key_value).SingleOrDefault();
                if (Img == "Logo")
                {
                    NewPath = string.Format("~/Resource/Document/NetworkDisk/{0}/{1}/{2}", UserId, "Project", filename);
                    virtualPath = this.Server.MapPath(NewPath);// UserId,
                    fullFileName = virtualPath + "/" + Filedata.FileName;

                    if (oldentity1.ProjectLogo == null || oldentity1.ProjectLogo == "")
                    {
                        oldentity1.ProjectLogo = "1.png";
                    }
                    else
                    {
                        string filename1 = oldentity1.ProjectLogo.Substring(0, oldentity1.ProjectLogo.LastIndexOf('.'));//获取文件名称，去除后缀名
                        virtualPath1 = "~/Resource/Document/NetworkDisk/System/Project/" + filename1;
                        if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~") + virtualPath1))
                        {
                            Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + virtualPath1, true);//pdf路径
                        }
                    }

                    //创建文件夹，保存文件
                    Directory.CreateDirectory(virtualPath);
                    Filedata.SaveAs(fullFileName);
                    oldentity1.ProjectLogo = Filedata.FileName;
                    oldentity1.ModifiedTime = DateTime.Now;
                    ProjectInfoCurrent.Modified(oldentity1);
                }
                else if (Img == "Background")
                {
                    NewPath = string.Format("~/Resource/Document/NetworkDisk/{0}/{1}/{2}", UserId, "Project", filename);
                    virtualPath = this.Server.MapPath(NewPath);// UserId,
                    fullFileName = virtualPath + "/" + Filedata.FileName;

                    if (oldentity1.ProjectBackground == null || oldentity1.ProjectBackground == "")
                    {
                        oldentity1.ProjectBackground = "1.png";
                    }
                    else
                    {
                        string filename1 = oldentity1.ProjectBackground.Substring(0, oldentity1.ProjectBackground.LastIndexOf('.'));//获取文件名称，去除后缀名
                        virtualPath1 = "~/Resource/Document/NetworkDisk/System/Project/" + filename1;
                        if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~") + virtualPath1))
                        {
                            Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + virtualPath1, true);//pdf路径
                        }
                    }

                    //创建文件夹，保存文件
                    Directory.CreateDirectory(virtualPath);
                    Filedata.SaveAs(fullFileName);
                    oldentity1.ProjectBackground = Filedata.FileName;
                    oldentity1.ModifiedTime = DateTime.Now;
                    ProjectInfoCurrent.Modified(oldentity1);
                }
                IsOk = 1;
                //IsOk = DataFactory.Database().Insert<Base_NetworkFile>(entity).ToString();
                var JsonData = new
                {
                    Status = IsOk,
                    NetworkFile = oldentity,
                };
                NewPath = NewPath.Replace("~", "../..");
                return Content(NewPath + "/" + Filedata.FileName);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ContentResult UploadPhoto()
        {
            HttpFileCollectionBase fileToUpload = Request.Files;
            List<string> list = new List<string>();
            int count = 0;
            foreach (string file in fileToUpload)
            {
                var fileName = fileToUpload[file].FileName.Trim();
                string suffix = fileName.Substring(fileName.LastIndexOf('.') + 1);

                if (suffix != "jpg" && suffix != "gif" && suffix != "jpeg" && suffix != "png")
                {
                    count++;
                }
                list.Add(fileName);

            }

            string photo = Session["photo"].ToString();

            string _photo = photo.Substring(0, photo.Length - 1);
            string[] fileArray = _photo.Split('、');
            var _list = fileArray.ToList();

            if (_list.Count() != list.Count())
            {
                return Content("必须同时选择要上传的所有图片！");
            }
            else
            {
                if (count > 0)
                {
                    return Content("所选的图片格式有误，请重选！");
                }
                else
                {
                    var diffArr = list.Where(c => !_list.Contains(c)).ToList();
                    if (diffArr.Count == 0)
                    {
                        foreach (string file in fileToUpload)
                        {
                            var fileName = fileToUpload[file].FileName.Trim();
                            string filename1 = fileName.Substring(0, fileName.LastIndexOf('.'));//获取文件名称，去除后缀名
                            string virtualPath = "/Resource/Document/NetworkDisk/System/Member/" + filename1;

                            string path = Server.MapPath(virtualPath);
                            if (Directory.Exists(path))
                            {
                                Directory.Delete(path, true);//pdf路径
                            }
                            //创建文件夹，保存文件
                            Directory.CreateDirectory(path);

                            fileToUpload[file].SaveAs(path + "/" + fileName);
                        }
                        return Content("操作成功！");
                    }
                    else
                    {
                        return Content("所选的图片名称有误，请重选！");
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_list"></param>
        /// <returns></returns>
        public bool Exists(string[] _list)
        {
            string _photo = Session["photo"].ToString();

            _photo = _photo.Substring(0, _photo.Length - 1);
            string[] FieldInfo = _photo.Split('、');

            var sameArr = FieldInfo.Intersect(_list).ToArray();

            //找出不同的元素(即交集的补集)
            var diffArr = FieldInfo.Where(c => !_list.Contains(c)).ToArray();
            //if (_list. == FieldInfo)
            //{
            //    return true;
            //}
            return true;
        }

        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <param name="Filedata">用户图片对象</param>
        /// <returns></returns>
        public ActionResult SubmitUploadify(HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(1000);////延迟500毫秒
                                   //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return HttpNotFound();
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/Document/NetworkDisk/{日期}/{guid}.{后缀名}
                //例如：/Resource/Document/Email/20130913/43CA215D947F8C1F1DDFCED383C4D706.jpg

                string filename = Filedata.FileName;
                string filename1 = filename.Substring(0, filename.LastIndexOf('.'));//获取文件名称，去除后缀名
                string NewPath = string.Format("/Resource/Document/NetworkDisk/System/{0}/{1}", "Member", filename1);
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);

                // virtualPath = string.Format("/Content/Images/Avatar/{0}/{1}/{2}{3}", UserId, uploadDate, fileGuid, FileEextension);
                string fullFileName = this.Server.MapPath(NewPath + "/");
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    Filedata.SaveAs(fullFileName + filename);
                }
                return Content("../.." + NewPath + "/" + filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取文件类型、文件图标
        /// </summary>
        /// <param name="Eextension">后缀名</param>
        /// <param name="FileType">要返回文件类型</param>
        /// <param name="Icon">要返回文件图标</param>
        public void DocumentType(string Eextension, ref string FileType, ref string Icon)
        {
            string _FileType = "";
            string _Icon = "";
            switch (Eextension)
            {
                case ".exe":
                    _FileType = "PC软件";
                    _Icon = "exe";
                    break;
                case ".docx":
                    _FileType = "word文件";
                    _Icon = "doc";
                    break;
                case ".doc":
                    _FileType = "word文件";
                    _Icon = "doc";
                    break;
                case ".xlsx":
                    _FileType = "excel文件";
                    _Icon = "xls";
                    break;
                case ".xls":
                    _FileType = "excel文件";
                    _Icon = "xls";
                    break;
                case ".pptx":
                    _FileType = "ppt文件";
                    _Icon = "ppt";
                    break;
                case ".ppt":
                    _FileType = "ppt文件";
                    _Icon = "ppt";
                    break;
                case ".txt":
                    _FileType = "记事本文件";
                    _Icon = "txt";
                    break;
                case ".pdf":
                    _FileType = "pdf文件";
                    _Icon = "pdf";
                    break;
                case ".zip":
                    _FileType = "压缩文件";
                    _Icon = "zip";
                    break;
                case ".rar":
                    _FileType = "压缩文件";
                    _Icon = "rar";
                    break;
                case ".png":
                    _FileType = "png图片";
                    _Icon = "png";
                    break;
                case ".gif":
                    _FileType = "gif图片";
                    _Icon = "gif";
                    break;
                case ".jpg":
                    _FileType = "jpg图片";
                    _Icon = "jpeg";
                    break;
                case ".mp3":
                    _FileType = "mp3文件";
                    _Icon = "mp3";
                    break;
                case ".html":
                    _FileType = "html文件";
                    _Icon = "html";
                    break;
                case ".css":
                    _FileType = "css文件";
                    _Icon = "css";
                    break;
                case ".mpeg":
                    _FileType = "mpeg文件";
                    _Icon = "mpeg";
                    break;
                case ".pds":
                    _FileType = "pds文件";
                    _Icon = "pds";
                    break;
                case ".ttf":
                    _FileType = "ttf文件";
                    _Icon = "ttf";
                    break;
                case ".swf":
                    _FileType = "swf文件";
                    _Icon = "swf";
                    break;
                case ".apk":
                    _FileType = "手机";
                    _Icon = "apk";
                    break;
                default:
                    _FileType = "其他文件";
                    _Icon = "new";
                    //return "else.png";
                    break;
            }
            FileType = _FileType;
            Icon = _Icon;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public void Download(string KeyValue)
        {
            RMC_MemberLibrary entity = MemberLibraryCurrent.Find(f => f.MemberId== KeyValue).SingleOrDefault();
            string filename = Server.UrlDecode(entity.MemberName);//返回客户端文件名称
            string filepath = Server.UrlDecode(entity.FullPath);//文件虚拟路径
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        #endregion

        #region 编辑构件信息
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public virtual ActionResult MemberForm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SetMemberForm(string KeyValue)
        {
            RMC_MemberLibrary entity = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();
            return ToJsonResult(entity);
        }
        /// <summary>
        /// 提交文件夹表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <param name="MemberRawMaterialListJson">主键值</param>
        ///  <param name="TreeId">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult SubmitMember(RMC_MemberLibrary entity,string TreeId, string MemberRawMaterialListJson, string KeyValue)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                var moduleButtonList = MemberRawMaterialListJson.ToList<RMC_MemberMaterial>();
                if (!string.IsNullOrEmpty(KeyValue))
                {
                   
                    RMC_MemberLibrary Oldentity = MemberLibraryCurrent.Find(t => t.MemberId == KeyValue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.MemberName = entity.MemberName;
                      //Oldentity.SectionalArea = entity.SectionalArea;
                    //Oldentity.SurfaceArea = entity.SurfaceArea;
                    //Oldentity.TheoreticalWeight = entity.TheoreticalWeight;
                    //Oldentity.SectionalSize_h = entity.SectionalSize_h;
                    //Oldentity.SectionalSizeB = entity.SectionalSizeB;
                    //Oldentity.SectionalSize_b = entity.SectionalSize_b;
                    //Oldentity.SectionalSizeD = entity.SectionalSizeD;
                    //Oldentity.SectionalSize_d = entity.SectionalSize_d;
                    //Oldentity.SectionalSize_t = entity.SectionalSize_t;
                    //Oldentity.SectionalSize_r = entity.SectionalSize_r;
                    //Oldentity.SectionalSize_r1 = entity.SectionalSize_r1;
                    //Oldentity.InertiaDistance_x = entity.InertiaDistance_x;
                    //Oldentity.InertiaDistance_y = entity.InertiaDistance_y;
                    //Oldentity.InertiaDistance_x0 = entity.InertiaDistance_x0;
                    //Oldentity.InertiaDistance_y0 = entity.InertiaDistance_y0;
                    //Oldentity.InertiaDistance_x1 = entity.InertiaDistance_x1;
                    //Oldentity.InertiaDistance_y1 = entity.InertiaDistance_y1;
                    //Oldentity.InertiaDistance_u = entity.InertiaDistance_u;
                    //Oldentity.InertiaRadius_x = entity.InertiaRadius_x;
                    //Oldentity.InertiaRadius_x0 = entity.InertiaRadius_x0;
                    //Oldentity.InertiaRadius_y = entity.InertiaRadius_y;
                    //Oldentity.InertiaRadius_y0 = entity.InertiaRadius_y0;
                    //Oldentity.InertiaRadius_u = entity.InertiaRadius_u;
                    //Oldentity.SectionalModulus_x = entity.SectionalModulus_x;
                    //Oldentity.SectionalModulus_y = entity.SectionalModulus_y;
                    //Oldentity.SectionalModulus_x0 = entity.SectionalModulus_x0;
                    //Oldentity.SectionalModulus_y0 = entity.SectionalModulus_y0;
                    //Oldentity.SectionalModulus_u = entity.SectionalModulus_u;
                    //Oldentity.GravityCenterDistance_0 = entity.GravityCenterDistance_0;
                    //Oldentity.GravityCenterDistance_x0 = entity.GravityCenterDistance_x0;
                    //Oldentity.GravityCenterDistance_y0 = entity.GravityCenterDistance_y0;
                                        if (entity.CAD_Drawing == null)
                    {
                        entity.CAD_Drawing = "1.png";
                    }
                    string filename = System.IO.Path.GetFileName(entity.CAD_Drawing);
                    Oldentity.CAD_Drawing = filename;

                    if (entity.Model_Drawing == null)
                    {
                        entity.Model_Drawing = "1.png";
                    }
                    string filename1 = System.IO.Path.GetFileName(entity.Model_Drawing);
                    Oldentity.Model_Drawing = filename1;

                    if (entity.Icon == null)
                    {
                        entity.Icon = "1.png";
                    }
                    string filename2 = System.IO.Path.GetFileName(entity.Icon);
                    Oldentity.Icon = filename2;

                    Oldentity.MemberUnit = entity.MemberUnit;
                    Oldentity.UnitPrice = entity.UnitPrice;
                    MemberLibraryCurrent.Modified(Oldentity);
                }
                else
                {
                    string str = "";
                    string str1 = "";
                    RMC_MemberLibrary entitys = new RMC_MemberLibrary();
                
                    var data = subprojectbll.GetList(null).ToList().FindAll(f=>f.Id == entity.SubProjectId).SingleOrDefault();
                    str = Str.PinYin(data.FullName.Substring(0, 1)+ entity.Category.Substring(0, 1)).ToUpper();

                    int Num = 0001;
                    var MemberList = MemberLibraryCurrent.Find(f => f.SubProjectId == entity.SubProjectId).ToList();
                    Num = Num + MemberList.Count();
                    entitys.MemberId = entity.MemberId;
                   
                    for (int i = 0; i <4-Num.ToString().Length; i++)
                    {
                        str1 += "0";
                    }
                    entitys.MemberNumbering = str + str1+ Num.ToString();
                    entitys.SubProjectId = entity.SubProjectId;
                    entitys.MemberName = entity.MemberName;
                    entitys.UploadTime = DateTime.Now;
                    entitys.MemberUnit = entity.MemberUnit;
                    entitys.UnitPrice = entity.UnitPrice;
                    entitys.IsRawMaterial = 0;
                    entitys.IsProcess = 0;

                     //entitys.SectionalArea = entity.SectionalArea;
                    //entitys.SurfaceArea = entity.SurfaceArea;
                    //entitys.TheoreticalWeight = entity.TheoreticalWeight;
                    //entitys.SectionalSize_h = entity.SectionalSize_h;
                    //entitys.SectionalSizeB = entity.SectionalSizeB;
                    //entitys.SectionalSize_b = entity.SectionalSize_b;
                    //entitys.SectionalSizeD = entity.SectionalSizeD;
                    //entitys.SectionalSize_d = entity.SectionalSize_d;
                    //entitys.SectionalSize_t = entity.SectionalSize_t;
                    //entitys.SectionalSize_r = entity.SectionalSize_r;
                    //entitys.SectionalSize_r1 = entity.SectionalSize_r1;
                    //entitys.InertiaDistance_x = entity.InertiaDistance_x;
                    //entitys.InertiaDistance_y = entity.InertiaDistance_y;
                    //entitys.InertiaDistance_x0 = entity.InertiaDistance_x0;
                    //entitys.InertiaDistance_y0 = entity.InertiaDistance_y0;
                    //entitys.InertiaDistance_y1 = entity.InertiaDistance_y1;
                    //entitys.InertiaDistance_u = entity.InertiaDistance_u;
                    //entitys.InertiaRadius_x = entity.InertiaRadius_x;
                    //entitys.InertiaRadius_x0 = entity.InertiaRadius_x0;
                    //entitys.InertiaRadius_y = entity.InertiaRadius_y;
                    //entitys.InertiaRadius_y0 = entity.InertiaRadius_y0;
                    //entitys.InertiaRadius_u = entity.InertiaRadius_u;
                    //entitys.SectionalModulus_x = entity.SectionalModulus_x;
                    //entitys.SectionalModulus_y = entity.SectionalModulus_y;
                    //entitys.SectionalModulus_x0 = entity.SectionalModulus_x0;
                    //entitys.SectionalModulus_y0 = entity.SectionalModulus_y0;
                    //entitys.SectionalModulus_u = entity.SectionalModulus_u;
                    //entitys.GravityCenterDistance_0 = entity.GravityCenterDistance_0;
                    //entitys.GravityCenterDistance_x0 = entity.GravityCenterDistance_x0;
                    //entitys.GravityCenterDistance_y0 = entity.GravityCenterDistance_y0;
                    if (entity.CAD_Drawing == null)
                    {
                        entity.CAD_Drawing = "1.png";
                    }
                    string filename = System.IO.Path.GetFileName(entity.CAD_Drawing);
                    entitys.CAD_Drawing = filename;

                    if (entity.Model_Drawing == null)
                    {
                        entity.Model_Drawing = "1.png";
                    }
                    string filename1 = System.IO.Path.GetFileName(entity.Model_Drawing);
                    entitys.Model_Drawing = filename1;

                    if (entity.Icon == null)
                    {
                        entity.Icon = "1.png";
                    }
                    string filename2 = System.IO.Path.GetFileName(entity.Icon);
                    entitys.Icon = filename2;
                    MemberLibraryCurrent.Add(entitys);

                    var MemberRawMaterial = MemberMaterialCurrent.Find(f => f.MemberId == entity.MemberId).ToList();
                    
                    //moduleButtonList



                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public virtual ActionResult DeleteMember(RMC_MemberLibrary entity, string KeyValue)
        {

            //删除构件
            List<string> ids = new List<string>();
            ids.Add(KeyValue);
            MemberLibraryCurrent.Remove_str(ids);
            //

            //删除构件材料
            List<int> ids1 = new List<int>();
            var MemberMaterial = MemberMaterialCurrent.Find(f => f.MemberId == KeyValue).ToList();
            if (MemberMaterial.Count() > 0)
            {
                foreach (var item in MemberMaterial)
                {
                    ids1.Add(Convert.ToInt32(item.MemberId));
                }
                MemberMaterialCurrent.Remove(ids1);
            }
            //

            //删除构件制程
            List<int> ids2 = new List<int>();
            var MemberProcess = MemberProcessCurrent.Find(f => f.MemberId == KeyValue).ToList();
            if (MemberProcess.Count() > 0)
            {
                foreach (var item in MemberProcess)
                {
                    ids1.Add(Convert.ToInt32(item.MemberId));
                }
               MemberProcessCurrent.Remove(ids1);
            }

            return Success("删除成功");
        }

        #endregion

        #region 构件数控参数
        [ValidateInput(false)]
        public virtual ActionResult CNCParameterForm()
        {
            return View();
        }
        #endregion

        #region 构件图纸模型管理 
        public ActionResult DrawingManagement(string KeyValue, string FileNamePath)
        {
            if (KeyValue == "")
            {
                ViewData["CADDrawing"] = FileNamePath;
                ViewData["ModelDrawing"] = FileNamePath;
            }
            else
            {
                var ht = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();
                if (ht.CAD_Drawing == null || ht.CAD_Drawing == "")
                {
                    ht.CAD_Drawing = "1.png";
                }
                if (ht.Model_Drawing == null || ht.Model_Drawing == "")
                {
                    ht.Model_Drawing = "1.png";
                }
                var filename = ht.CAD_Drawing.Substring(0, ht.CAD_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath = "../../Resource/Document/NetworkDisk/System/Member/" + filename + "/";

                var filename1 = ht.Model_Drawing.Substring(0, ht.Model_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath1 = "../../Resource/Document/NetworkDisk/System/Member/" + filename1 + "/";
                //string fullFileName = this.Server.MapPath(virtualPath);
                var file = virtualPath + ht.CAD_Drawing;
                var file1 = virtualPath1 + ht.Model_Drawing;

                ViewData["CADDrawing"] = file;
                ViewData["ModelDrawing"] = file1;
                ViewData["MemberId"] = KeyValue;
            }
            return View();
        }
        public ActionResult CADDrawingManagement(string KeyValue, string FileNamePath)
        {
            if (KeyValue == "")
            {
                ViewData["CADDrawing"] = FileNamePath;
                // ViewData["ModelDrawing"] = FileNamePath;
            }
            else
            {
                var ht = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();
                if (ht.CAD_Drawing == null || ht.CAD_Drawing == "")
                {
                    ht.CAD_Drawing = "1.png";
                }
                if (ht.Model_Drawing == null || ht.Model_Drawing == "")
                {
                    ht.Model_Drawing = "1.png";
                }
                var filename = ht.CAD_Drawing.Substring(0, ht.CAD_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath = "../../Resource/Document/NetworkDisk/System/Member/" + filename + "/";

                var filename1 = ht.Model_Drawing.Substring(0, ht.Model_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath1 = "../../Resource/Document/NetworkDisk/System/Member/" + filename1 + "/";
                //string fullFileName = this.Server.MapPath(virtualPath);
                var file = virtualPath + ht.CAD_Drawing;
                var file1 = virtualPath1 + ht.Model_Drawing;

                ViewData["CADDrawing"] = file;
                //ViewData["ModelDrawing"] = file1;
                ViewData["MemberId"] = KeyValue;
            }
            return View();
        }
        public ActionResult ModelDrawingManagement(string KeyValue, string FileNamePath)
        {
            if (KeyValue == "")
            {
                // ViewData["CADDrawing"] = FileNamePath;
                ViewData["ModelDrawing"] = FileNamePath;
            }
            else
            {
                var ht = MemberLibraryCurrent.Find(f => f.MemberId == KeyValue).SingleOrDefault();
                if (ht.CAD_Drawing == null || ht.CAD_Drawing == "")
                {
                    ht.CAD_Drawing = "1.png";
                }
                if (ht.Model_Drawing == null || ht.Model_Drawing == "")
                {
                    ht.Model_Drawing = "1.png";
                }
                var filename = ht.CAD_Drawing.Substring(0, ht.CAD_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath = "../../Resource/Document/NetworkDisk/System/Member/" + filename + "/";

                var filename1 = ht.Model_Drawing.Substring(0, ht.Model_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
                string virtualPath1 = "../../Resource/Document/NetworkDisk/System/Member/" + filename1 + "/";
                //string fullFileName = this.Server.MapPath(virtualPath);
                var file = virtualPath + ht.CAD_Drawing;
                var file1 = virtualPath1 + ht.Model_Drawing;

                //ViewData["CADDrawing"] = file;
                ViewData["ModelDrawing"] = file1;
                ViewData["MemberId"] = KeyValue;
            }
            return View();
        }

        /// <summary>
        /// 删除图纸
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="CAD"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public ActionResult DelDrawing(string KeyValue, string CAD, string Model)
        {
            string filename = "";
            var file = MemberLibraryCurrent.Find(u => u.MemberId == KeyValue).First();
            if (CAD != "" || CAD != null && Model == "")
            {
                filename = file.CAD_Drawing.Substring(0, file.CAD_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
            }
            else
            {
                filename = file.Model_Drawing.Substring(0, file.Model_Drawing.LastIndexOf('.'));//获取文件名称，去除后缀名
            }
            string virtualPath = "~/Resource/Document/NetworkDisk/System/Member/" + filename;
            //string fullFileName = this.Server.MapPath(virtualPath);
            if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~") + virtualPath))
            {
                Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + virtualPath, true);//pdf路径
            }
            file.ModifiedTime = DateTime.Now;
            file.CAD_Drawing = "1.png";
            MemberLibraryCurrent.Modified(file);

            return Success("删除成功。");
        }
        #endregion

        #region 打印报表
        public ActionResult PrintPage()
        {
            return View();
        }

        #endregion

        #region 构件原材料用量
        public ActionResult RawMaterialsDosageIndex()
        {
            return View();
        }
        public ActionResult GridListJsonMemberMaterial(string KeyValue, Pagination jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                int total = 0;
                Expression<Func<RMC_MemberMaterial, bool>> func = ExpressionExtensions.True<RMC_MemberMaterial>();
                func = f => f.MemberId == KeyValue;
                #region 查询条件拼接
                //if (keywords != null && keywords != "&nbsp;")
                //{
                //    func = func.And(f => f.MemberNumbering.Contains(MemberNumbering));
                //}
                //if (!string.IsNullOrEmpty(MemberModel))
                //{
                //    func = func.And(f => f.MemberModel.Contains(MemberModel)); /*func = func.And(f => f.FullPath.Contains(model.FilePath))*/
                //}
                #endregion

                List<RMC_MemberMaterial> listfile = MemberMaterialCurrent.FindPage<string>(jqgridparam.page
                                         , jqgridparam.rows
                                         , func
                                         , false
                                         , f => f.MemberMaterialId.ToString()
                                         , out total
                                         ).ToList();
                List<MemberMaterialModel> EntityModelList = new List<MemberMaterialModel>();
                foreach (var item in listfile)
                {
                    MemberMaterialModel EntityModel = new MemberMaterialModel();
                    EntityModel.MemberMaterialId = item.MemberMaterialId;
                    EntityModel.MemberId = item.MemberId;
                    EntityModel.RawMaterialId = item.RawMaterialId;
                   
                    EntityModel.MaterialNumber =item.RawMaterialNumber;
                    EntityModel.Description = item.Description;
                    var RawMaterial =RawMaterialLibraryCurrent.Find(f => f.RawMaterialId == item.RawMaterialId).SingleOrDefault();
                    var Unit = MemberUnitCurrent.Find(f => f.UnitId == RawMaterial.UnitId).SingleOrDefault();
                    var Tree = TreeCurrent.Find(f => f.TreeID == item.TreeId).SingleOrDefault();
                    EntityModel.RawMaterialName = Tree.TreeName + item.RawMaterialModel;
                    EntityModel.RawMaterialStandard = RawMaterial.RawMaterialStandard;
                    EntityModel.UnitName = Unit.UnitName;
                    EntityModelList.Add(EntityModel);
                }
                var JsonData = new
                {
                    total = total / jqgridparam.rows + 1,
                    page = jqgridparam.page,
                    records = total,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = EntityModelList,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                //return null;
            }

        }


        //表单
        public ActionResult RawMaterialsDosageForm()
        {
            return View();
        }

        public ActionResult SetDataForm(string KeyValue)
        {
            int _KeyValue = Convert.ToInt32(KeyValue);
            RMC_MemberMaterial entity = MemberMaterialCurrent.Find(f => f.MemberMaterialId == _KeyValue).SingleOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 提交文件夹表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <param name="TreeId">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SubmitDataForm(RMC_MemberMaterial entity, string KeyValue, string TreeId)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int _KeyValue = Convert.ToInt32(KeyValue);
                    RMC_MemberMaterial Oldentity = MemberMaterialCurrent.Find(t => t.MemberMaterialId == _KeyValue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.RawMaterialId = entity.RawMaterialId;
                    Oldentity.RawMaterialModel = entity.RawMaterialModel;
                    Oldentity.Description = entity.Description;
                    MemberMaterialCurrent.Modified(Oldentity);
                }
                else
                {
                    var MemberMaterialList = MemberMaterialCurrent.Find(f => f.MemberId == entity.MemberId).ToList();
                    var a = MemberMaterialList.Where(f => f.RawMaterialId == entity.RawMaterialId).SingleOrDefault();
                    if (a == null)
                    {
                        MemberMaterialCurrent.Add(entity);

                        var Member = MemberLibraryCurrent.Find(f => f.MemberId == entity.MemberId).SingleOrDefault();
                        Member.IsRawMaterial = 1;
                        MemberLibraryCurrent.Modified(Member);
                    }
                    else
                    {
                        return Success("该数据已存在！");
                    }

                    //this.WSectionalSize_r = entity.SectionalSize_r;riteLog(IsOk, entity, null, KeyValue, Message);
                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public virtual ActionResult DeleteMemberMaterial(string KeyValue, string MemberId)
        {

            int IsOk = -1;
            int key_value = Convert.ToInt32(KeyValue);
            List<int> ids = new List<int>();
            ids.Add(key_value);
            IsOk = MemberMaterialCurrent.Remove(ids);

            var MemberMaterial = MemberMaterialCurrent.Find(f => f.MemberId == MemberId).ToList();
            if (MemberMaterial.Count() == 0)
            {
                var Member = MemberLibraryCurrent.Find(f => f.MemberId == MemberId).SingleOrDefault();
                Member.IsRawMaterial = 0;
                MemberLibraryCurrent.Modified(Member);
            }
            return Success("删除成功。");
        }

      
        #endregion

        #region 构件制程
        public ActionResult MemberProcessIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取制程表
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="jqgridparam"></param>
        /// <returns></returns>
        public ActionResult GridListJsonMemberProcess(string KeyValue, Pagination jqgridparam)
        {
            try
            {
                int total = 0;
                Expression<Func<RMC_MemberProcess, bool>> func = ExpressionExtensions.True<RMC_MemberProcess>();
                func = f => f.MemberId == KeyValue;
                #region 查询条件拼接
                //if (keywords != null && keywords != "&nbsp;")
                //{
                //    func = func.And(f => f.MemberNumbering.Contains(MemberNumbering));
                //}
                //if (!string.IsNullOrEmpty(MemberModel))
                //{
                //    func = func.And(f => f.MemberModel.Contains(MemberModel)); /*func = func.And(f => f.FullPath.Contains(model.FilePath))*/
                //}
                #endregion

                List<RMC_MemberProcess> listfile = MemberProcessCurrent.FindPage<string>(jqgridparam.page
                                         , jqgridparam.rows
                                         , func
                                         , false
                                         , f => f.SortCode.ToString()
                                         , out total
                                         ).ToList();
                var JsonData = new
                {
                    rows = listfile,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception)
            {

                return null;
            }

        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetUserName(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = userBLL.GetPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }

        //public readonly RepositoryFactory<Base_User> repositoryfactory = new RepositoryFactory<Base_User>();
        //public ActionResult GetUserName()
        //{

        //    List<Base_User> entity = repositoryfactory.Repository().FindListTop(25);
        //    //string JsonData = entity.ToJson();
        //    ////自定义
        //    //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
        //    return Json(entity);
        //}

        /// <summary>
        /// 制程表单
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberProcessForm()
        {
            return View();
        }

        /// <summary>
        ///获取制程表单
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult SetMemberProcessForm(string KeyValue)
        {
            int _KeyValue = Convert.ToInt32(KeyValue);
            RMC_MemberProcess entity = MemberProcessCurrent.Find(f => f.MemberProcessId == _KeyValue).SingleOrDefault();
            //string JsonData = entity.ToJson();
            ////自定义
            //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 提交文件夹表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <param name="OrderId">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult SubmitMemberProcessForm(RMC_MemberProcess entity, string KeyValue, string OrderId)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";

                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int _KeyValue = Convert.ToInt32(KeyValue);
                    RMC_MemberProcess Oldentity = MemberProcessCurrent.Find(t => t.MemberProcessId == _KeyValue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.OperationTime = entity.OperationTime;
                    Oldentity.ProcessName = entity.ProcessName;
                    Oldentity.ProcessRequirements = entity.ProcessRequirements;
                    Oldentity.SortCode = entity.SortCode;
                    Oldentity.ProcessMan = entity.ProcessMan;
                    Oldentity.Description = entity.Description;
                    MemberProcessCurrent.Modified(Oldentity);
                }
                else
                {
                    int MemberProcessId = MemberProcessCurrent.Add(entity).MemberProcessId;

                    int _OrderId = Convert.ToInt32(OrderId);
                    if (_OrderId != 0)
                    {
                        RMC_ProcessManagement Entity = new RMC_ProcessManagement();
                        Entity.OrderId = _OrderId;
                        Entity.MemberId = entity.MemberId;
                        Entity.MemberProcessId = MemberProcessId;
                        Entity.IsProcessStatus = 0;
                        ProcessManagementCurrent.Add(Entity);

                        var Member = MemberLibraryCurrent.Find(f => f.MemberId == entity.MemberId).SingleOrDefault();
                        Member.IsProcess = 1;
                        MemberLibraryCurrent.Modified(Member);
                    }

                    //this.WSectionalSize_r = entity.SectionalSize_r;riteLog(IsOk, entity, null, KeyValue, Message);
                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 删除制程
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public virtual ActionResult DeleteMemberProcess(RMC_MemberLibrary entity, string KeyValue)
        {

            int IsOk = -1;
            int key_value = Convert.ToInt32(KeyValue);
            List<int> ids = new List<int>();
            ids.Add(key_value);
            IsOk = MemberProcessCurrent.Remove(ids);
            return Success("删除成功。");
        }
        #endregion
    }
}








