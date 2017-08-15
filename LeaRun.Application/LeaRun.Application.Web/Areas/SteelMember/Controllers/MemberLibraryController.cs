using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.IO;
using System;
using System.Data.OleDb;
using LeaRun.Util.Extension;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using LeaRun.Application.Busines.SystemManage;
using System.Threading;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-05 17:15
    /// 描 述：构件库管理
    /// </summary>
    public class MemberLibraryController : MvcControllerBase
    {
        private MemberLibraryBLL memberlibrarybll = new MemberLibraryBLL();
        private SubProjectBLL subprojectbll = new SubProjectBLL();
        private MemberMaterialBLL membermaterialbll = new MemberMaterialBLL();
        private MemberProcessBLL memberprocessbll = new MemberProcessBLL();
        private MemberWarehouseBLL memberwarehousebll = new MemberWarehouseBLL();
        private ProjectInfoBLL projectinfobll = new ProjectInfoBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private DataItemDetailBLL dataitemdetailbll = new DataItemDetailBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 材料用量表单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RawMaterialNumForm()
        {
            return View();
        }
        /// <summary>
        /// 构件类别页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberCategoryIndex()
        {
            return View();
        }
        /// <summary>
        ///  构件详情
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="FileNamePath"></param>
        /// <returns></returns>
        public ActionResult DrawingManagement(string KeyValue, string FileNamePath)
        {
            if (KeyValue == "")
            {
                ViewData["CADDrawing"] = FileNamePath;
                ViewData["ModelDrawing"] = FileNamePath;
            }
            else
            {

                var ht = memberlibrarybll.GetList(null).ToList().Find(f => f.MemberId == KeyValue);
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
                var file = virtualPath + ht.CAD_Drawing;
                var file1 = virtualPath1 + ht.Model_Drawing;

                ViewData["CADDrawing"] = file;
                ViewData["ModelDrawing"] = file1;
                ViewData["MemberId"] = KeyValue;
            }
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = memberlibrarybll.GetPageList(pagination, queryJson).OrderBy(o=>o.MemberNumbering);
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = memberlibrarybll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = memberlibrarybll.GetEntity(keyValue);
            //data.Category = dataitemdetailbll.GetEntity(data.Category).ItemName;
            return ToJsonResult(data);
        }

        /// <summary>
        /// 按钮列表Json转换原材料Json 
        /// </summary>
        /// <param name="RawMaterialJson">按钮列表</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult ListToRawMaterialJson(string RawMaterialJson)
        {
            var data0 = RawMaterialJson.Replace(",\"undefined\":\"\"", "");
            var data = data0.ToList<MemberMaterialModel>();
            var DataList = new List<MemberMaterialModel>();
            foreach (MemberMaterialModel item in data)
            {
                MemberMaterialModel Entity = new MemberMaterialModel();
                // Entity.MemberMaterialId = item.MemberMaterialId;
                Entity.RawMaterialModel = item.RawMaterialModel;
                Entity.TreeName = item.TreeName;
                Entity.RawMaterialNumber = item.RawMaterialNumber;
                DataList.Add(Entity);
            }
            return ToJsonResult(DataList);
        }

        /// <summary>
        /// 获取构件原材料
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public ActionResult GetMemberRawMaterialJson(string MemberId)
        {
            var data = membermaterialbll.GetList(null).ToList().FindAll(f => f.MemberId == MemberId);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取原材料
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public virtual ActionResult GetRawMaterialJson(string KeyValue)
        {
            var expression = LinqExtensions.True<RawMaterialLibraryEntity>();
            if (!string.IsNullOrEmpty(KeyValue.Trim()))
            {
                expression = expression.And(r => r.Category.Trim() == KeyValue.Trim());
            }
            var RawMaterial = rawmateriallibrarybll.GetList(expression);
            return ToJsonResult(RawMaterial);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            string[] Arry = keyValue.Split(',');//字符串转数组
            foreach (var item in Arry)
            {
              var data= memberlibrarybll.GetList(null).ToList();
              var MemberEntity = data.Find(f => f.MemberId == item);
               memberlibrarybll.RemoveForm(item);
               memberwarehousebll.RemoveForm(item);
            
                 var MemberEntity1 = data.FindAll(f =>f.MarkId > MemberEntity.MarkId);//&& f.EngineeringId == MemberEntity.EngineeringId
                if (MemberEntity1.Count()>0)
                {
                    foreach (var item1 in MemberEntity1)
                    {
                        var MemberEntity2 = data.Find(f => f.MemberId == item1.MemberId);

                        char[] Number = MemberEntity2.MemberNumbering.ToArray();
                        string MemberNumbering = "";
                        string Letter = "";
                        for (int I = 0; I < Number.Length; I++)
                        {
                            if (("0123456789").IndexOf(Number[I] + "") != -1)
                            {
                                //if (Number[I].ToString() != "0")
                                //{
                                //    MemberNumbering += Number[I];//获取不等于0的数字
                                //}
                                //else
                                //{
                                //    Letter += Number[I];//获取0
                                //}
                                MemberNumbering += Number[I];//获取数字
                            }
                            else
                            {
                                Letter += Number[I];//获取字母（字符）
                            }
                        }
                        MemberEntity2.MarkId--;
                        MemberEntity2.MemberNumbering = (Convert.ToInt64(MemberNumbering) -1).ToString();
                        memberlibrarybll.SaveForm(item1.MemberId, MemberEntity2);
                    }
                }

                //删除构件原材料
                var MemberMaterial = membermaterialbll.GetList(null).ToList().FindAll(t => t.MemberId == keyValue);

                if (MemberMaterial.Count() > 0)
                {
                    for (int i = 0; i < MemberMaterial.Count(); i++)
                    {
                        membermaterialbll.RemoveForm(MemberMaterial[i].MemberMaterialId.ToString());
                    }
                }
                //

                //删除构件制程
                var MemberProcess = memberprocessbll.GetList(null).ToList().FindAll(t => t.MemberId == keyValue);
                if (MemberProcess.Count() > 0)
                {
                    for (int i = 0; i < MemberProcess.Count(); i++)
                    {
                        memberprocessbll.RemoveForm(MemberProcess[i].MemberProcessId.ToString());
                    }
                }
                //
            }

            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="MemberRawMaterialListJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, MemberLibraryEntity entity, string MemberRawMaterialListJson)
        {
            string str = "";
            string str1 = "";
            MemberLibraryEntity entitys = new MemberLibraryEntity();
            MemberWarehouseEntity entitys1 = new MemberWarehouseEntity();
            //var data = subprojectbll.GetList(null).ToList().Find(f => f.Id == entity.SubProjectId);
            //str = Str.PinYin(data.FullName.Substring(0, 1) + entity.Category.Substring(0, 1)).ToUpper();

            entitys.MemberId = entity.MemberId;
            entitys1.EngineeringId = entitys.EngineeringId = entity.EngineeringId;
            entitys1.Category=entitys.Category = entity.Category;
            entitys1.UpdateTime=entitys.UploadTime = DateTime.Now;
            entitys1.MemberModel=entitys.MemberModel = entity.MemberModel.Trim();
            entitys1.MemberName = entitys.MemberName = entity.MemberName.Trim();
            entitys1.MemberUnit = entitys.MemberUnit = entity.MemberUnit;

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

            str = DateTime.Now.ToString("yyyyMMdd");
            if (keyValue == null || keyValue == "")
            {
                int Num = 1;
                var MemberList = memberlibrarybll.GetList(null).ToList().FindAll(f => f.MemberId != "");
                Num = Num + MemberList.Count();

                for (int i = 0; i < 6 - Num.ToString().Length; i++)
                {
                    str1 += "0";
                }
                entitys.MarkId = Num;
                entitys.MemberNumbering = str + str1 + Num.ToString();
                entitys.IsRawMaterial = 0;
                entitys.IsProcess = 0;

                entitys1.InStock = 0;
              
            }
            var MemberId = memberlibrarybll.SaveForm(keyValue, entitys);
            if (keyValue == null || keyValue == "")
            {
                   entitys1.MemberId = MemberId;
                   memberwarehousebll.SaveForm(keyValue, entitys1);
            }

            var data1 = MemberRawMaterialListJson.ToList<MemberMaterialModel>();
            if (data1.Count() > 0)
            {
                // if (keyValue != null || keyValue != "") {
                var MemberMaterial = membermaterialbll.GetList(null).ToList().FindAll(f => f.MemberId == keyValue);
                if (MemberMaterial.Count() > 0)
                {
                    foreach (var item in MemberMaterial)
                    {
                        membermaterialbll.RemoveForm(item.MemberMaterialId);
                    }
                }
                foreach (var item in data1)
                {
                    MemberMaterialEntity _MemberLibraryEntity = new MemberMaterialEntity();
                    _MemberLibraryEntity.MemberId = MemberId;
                   
                    _MemberLibraryEntity.RawMaterialModel = item.RawMaterialModel;
                    var data2 = rawmateriallibrarybll.GetList(f => f.RawMaterialModel == item.RawMaterialModel).Find(f => f.RawMaterialModel == item.RawMaterialModel);
                    _MemberLibraryEntity.RawMaterialId = data2.RawMaterialId;
                    _MemberLibraryEntity.RawMaterialNumber = item.RawMaterialNumber;
                    _MemberLibraryEntity.TreeName = item.TreeName;
                    _MemberLibraryEntity.CreatTime = DateTime.Now;
                    membermaterialbll.SaveForm("", _MemberLibraryEntity);
                }
            }
            return Success("操作成功。");
        }

        #region Excel 数据导入
        // 将Excel导入数据库  

        public ActionResult ImportFile()
        {
            return View();
        }

        public ContentResult SubmitImportFile(string KeyValue, MemberLibraryEntity MemberLibrary)
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
                            string MemberModel = table.Rows[i][0].ToString().Trim();
                            var _MemberLibrary = memberlibrarybll.GetList(null).ToList().Find(f => f.MemberModel == MemberModel);
                            if (_MemberLibrary == null)
                            {
                                MemberLibrary.EngineeringId = KeyValue;
                                MemberLibrary.UploadTime = DateTime.Now;
                                MemberLibrary.MemberModel = MemberModel;

                                //生成构件编号
                                string str = "";
                                string str1 = "";
                                var data = subprojectbll.GetList(null).ToList().Find(f => f.Id == KeyValue);
                                str = Str.PinYin(data.FullName.Substring(0, 1) + table.Rows[i][1].ToString().Substring(0, 1)).ToUpper().Trim();
                                int Num = 1;
                                var MemberList = memberlibrarybll.GetList(null).ToList().FindAll(f => f.EngineeringId == KeyValue);
                                Num = Num + MemberList.Count();

                                for (int i0 = 0; i0 < 4 - Num.ToString().Length; i0++)
                                {
                                    str1 += "0";
                                }
                                MemberLibrary.MemberNumbering = str + str1 + Num.ToString();
                                //end
                                MemberLibrary.MarkId = Num;
                                MemberLibrary.Category = table.Rows[i][1].ToString().Trim();
                                MemberLibrary.MemberName = table.Rows[i][2].ToString().Trim();
                                MemberLibrary.MemberUnit = table.Rows[i][3].ToString().Trim();
                                string CAD_Drawing = "1.png";
                                string Model_Drawing = "1.png";
                                string Icon = "1.png";
                                if (table.Rows[i][4].ToString() != "")
                                {
                                    CAD_Drawing = table.Rows[i][4].ToString().Trim();
                                    if (CAD_Drawing != "1.png")
                                    {
                                        Photo += CAD_Drawing + "、";

                                    }
                                }
                                if (table.Rows[i][5].ToString() != "")
                                {
                                    Model_Drawing = table.Rows[i][5].ToString().Trim();
                                    if (Model_Drawing != "1.png")
                                    {
                                        Photo += Model_Drawing + "、";
                                    }
                                }
                                if (table.Rows[i][6].ToString() != "")
                                {
                                    Icon = table.Rows[i][6].ToString().Trim();
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
                                memberlibrarybll.SaveForm("", MemberLibrary);
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
        public ActionResult SubmitUpLoadFile(string KeyValue, string Img, MemberLibraryEntity file, HttpPostedFileBase Filedata)
        {
            try
            {
                int key_value = Convert.ToInt32(KeyValue);
                MemberLibraryEntity oldentity = new MemberLibraryEntity();
                ProjectInfoEntity oldentity1 = new ProjectInfoEntity();
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
                oldentity = memberlibrarybll.GetList(null).ToList().Find(f => f.MemberId == KeyValue);
                oldentity1 = projectinfobll.GetList(null).ToList().Find(f => f.ProjectId == key_value);
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
                    projectinfobll.SaveForm(KeyValue, oldentity1);
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
                    projectinfobll.SaveForm(KeyValue, oldentity1);
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
        #endregion

        #region 图片上传
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
        #endregion

        #region JqGrid导出Excel
        /// <summary>
        /// 导出Excell模板
        /// </summary>
        /// <returns></returns>
        public void GetExcellTemperature(string ImportId)
        {

            DataTable data = new DataTable();
            //string fileName = "导入构件模板.xlsx";
            //string TableHeader = "构件模板";
            string DataColumn = "型号|";
            DataColumn += "构件类型|构件名称|单位|图纸|模型|图标";
            //DeriveExcel.DataTableToExcel(data, DataColumn.Split('|'), fileName);
            // ExcelHelper.ExcelDownload(exportTable, excelconfig);
            // userBLL.GetExportList();
        }
        #endregion
        #endregion

        #region 验证数据
        /// <summary>
        ///构件不能重复
        /// </summary>
        /// <param name="MemberName">型号</param>
        /// <param name="Category"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMember(string MemberName, string Category, string KeyValue)
        {
            bool IsOk = memberlibrarybll.ExistFullName(MemberName.Trim(), Category, KeyValue);
            return Content(IsOk.ToString());
        }

        /// <summary>
        ///构件材料中型号不能重复
        /// </summary>
        /// <param name="RawMaterialModel">型号</param>
        /// <param name="TreeName"></param>
        /// <param name="MemberId"></param>

        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMemberMaterial(string RawMaterialModel, string TreeName, string MemberId)
        {
            bool IsOk = membermaterialbll.ExistFullName(RawMaterialModel, TreeName, MemberId);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
