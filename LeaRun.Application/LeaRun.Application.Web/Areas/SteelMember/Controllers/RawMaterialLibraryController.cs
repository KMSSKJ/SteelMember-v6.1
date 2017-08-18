using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Busines.SystemManage;
using System.IO;
using System.Data.OleDb;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-06 10:42
    /// 描 述：原材料管理
    /// </summary>
    public class RawMaterialLibraryController : MvcControllerBase
    {
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private Cache.DataItemCache dataItemCache = new Cache.DataItemCache();


        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart(); 
            var data = rawmateriallibrarybll.GetList(pagination, queryJson);
            List<Models.RMLibraryModel> list = new List<Models.RMLibraryModel>();
            try {
                if (data!=null) {
                    foreach (var item in data) {
                        var dataItem = dataItemDetailBLL.GetEntity(item.Category);
                        Models.RMLibraryModel rmlibrarymodel = new Models.RMLibraryModel();
                        rmlibrarymodel.Category = dataItem.ItemName;
                        rmlibrarymodel.RawMaterialId = item.RawMaterialId;
                        rmlibrarymodel.RawMaterialName = item.RawMaterialName;
                        rmlibrarymodel.RawMaterialModel = item.RawMaterialModel;
                        rmlibrarymodel.Unit = item.Unit;
                        rmlibrarymodel.Description = item.Description;

                        list.Add(rmlibrarymodel);
                    }
                }
            } catch (System.Exception e) {
                throw;
            }
            var JsonData = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        //获取树字节子节点(自循环)
        //public List<DataItemDetailEntity> GetSonId(string ItemDetailId)
        //{
        //    List<DataItemDetailEntity> list = dataItemDetailBLL.GetByParentToItemIdIdList(ItemDetailId);
        //    return list.Concat(list.SelectMany(t => GetSonId(t.ParentId))).ToList();
        //}
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmateriallibrarybll.GetEntity(keyValue);
            return ToJsonResult(data);
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
            rawmateriallibrarybll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, RawMaterialLibraryEntity entity)
        {
            rawmateriallibrarybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion

        #region 验证数据

        /// <summary>
        /// 原材料中型号不能重复
        /// </summary>
        /// <param name="RawMaterialModel">型号</param>
        /// <param name="keyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ExistFullName(string RawMaterialModel,string category, string keyValue)
        {
            bool IsOk = rawmateriallibrarybll.ExistFullName(RawMaterialModel, category, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
        #region 导入excel
        public ActionResult ImportFile()
        {
            return View();
        }
        public ActionResult SubmitImportFile(string category,RawMaterialLibraryEntity MemberLibrary)
        {
            try
            {
               System.Web.HttpFileCollectionBase Filedatas = Request.Files;
                string FileName;
                string savePath;
                string fileEx;
                if (Filedatas.Count == 0) {
                    return Content("文件不能为空,请选择！！");
                }
                else
                {
                    var Filedata = Request.Files[0];
                    string filename = Path.GetFileName(Filedata.FileName);
                    int filesize = Filedata.ContentLength;//获取上传文件的大小（以字节为单位
                    fileEx = Path.GetExtension(filename);//获取上传文件的拓展名
                    string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名 

                    //规定上传文件的大小
                    //int Maxsize = 10000 * 1024;
                    int Maxsize = 10485760;
                    string FileType = ".xls,.xlsx";//规定上传的文件类型字符串
                    FileName = NoFileName + System.DateTime.Now.ToString("yyyyMMddhhmmssffff") + fileEx;

                    if (!FileType.Contains(fileEx))
                    {
                        return Content("文件类型不正确，请重新选择文件！！");
                    }
                    if (filesize>Maxsize)
                    {
                        return Content("文件大小不能超过10M");
                    }

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
                string strConn;
                switch (fileEx)
                {
                    case ".xls":
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                    case ".xlsx":
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                        break;
                    default:
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                }
                //连接串  
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
                conn.Open();
                //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等　
                System.Data.DataTable dtSheetName = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                //包含excel中表名的字符串数组
                string[] strTableNames = new string[dtSheetName.Rows.Count];
                for (int i = 0; i < strTableNames.Length; i++) {
                    strTableNames[i] = dtSheetName.Rows[i]["TABLE_NAME"].ToString();
                }
                OleDbDataAdapter myCommand = null;
                System.Data.DataTable dt = new System.Data.DataTable();
                //从指定的表明查询数据,可先把所有表明列出来供用户选择 
                string strExcel = "select*from[" + strTableNames[0] + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                System.Data.DataSet myDataSet = new System.Data.DataSet();
                myCommand.Fill(myDataSet, "ExcelInfo");

                System.Data.DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
                
                //int count = 0;
                

                if (table.Columns.Count!=5)
                {
                    return Content("操作失败！！，请使用系统提供的模板！！");
                }

                var data = dataItemCache.GetDataItemList("RawMaterialType");
                
                

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    // for (int j=0;j<table.Columns.Count;j++) {
                    for (int j = 0; j <= 2; j++)
                    {
                        var s = table.Rows[i][j].ToString();
                        if (table.Rows[i][j].ToString() == "")
                        {
                            var inu = i + 2;
                            var tshi = string.Format("表格中{0}的前三列为空行！！请修改后在导入！！", inu.ToString());
                            return Content(tshi);       
                        } 

                    }
                  
                }
                bool ISRawMaterialType = false;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    foreach (Entity.SystemManage.ViewModel.DataItemModel item in data)
                    {
                        var dd = table.Rows[i][0].ToString();
                        if (table.Rows[i][0].ToString() == item.ItemName)
                        {
                            ISRawMaterialType = true;
                        }

                    }
                    if (ISRawMaterialType != true)
                    {
                        var inu = i + 2;
                        var tshi = string.Format("{0}行的原材料类型是数据库中不存在类型！！请修改后再导入或联系管理员！！", inu.ToString());
                        return Content(tshi);
                    }
                    else
                    {
                        ISRawMaterialType = false;
                    }
                }
                //入库
                for (int i=0;i<table.Rows.Count;i++)
                {
                    //for (int j=0;j<table.Columns.Count;i++)
                    //for (int j = 1; j < table.Columns.Count; i++)
                    //{
                        string itemid="";
                        foreach (Entity.SystemManage.ViewModel.DataItemModel item in data)
                        {
                            var dd = table.Rows[i][0].ToString();
                            if (table.Rows[i][0].ToString() == item.ItemName)
                            {
                                itemid = item.ItemDetailId;
                            }

                        }
                        if (itemid != "") {
                        var rawMaterialname = table.Rows[i][1].ToString();
                        var rawMaterialModel = table.Rows[i][2].ToString();
                        var unit = table.Rows[i][3].ToString();
                       var ishave=rawmateriallibrarybll.GetList(p => p.Category == itemid && p.RawMaterialName== rawMaterialname && p.RawMaterialModel== rawMaterialModel
                        && p.Unit== unit );
                        if (ishave.Count >= 1)
                        {

                        }
                        else {
                            string keyvalue = null;
                            RawMaterialLibraryEntity entity = new RawMaterialLibraryEntity();
                            entity.Category = itemid;
                            entity.RawMaterialName = table.Rows[i][1].ToString();
                            entity.RawMaterialModel = table.Rows[i][2].ToString();
                            entity.Unit = table.Rows[i][3].ToString();
                            entity.Description = table.Rows[i][4].ToString();

                            rawmateriallibrarybll.SaveForm(keyvalue, entity);
                            itemid = "";
                        }
                            
                        }
                   // }
                       
                }
               
            }
            catch (System.Exception e)
            {
                return Content("操作失败：" + e.Message);
            }

            return Content("操作成功！！");
        }
        #endregion
        #region 下载模板
        public ActionResult GetTemplet(string templetname) {
            
            string fileName = templetname+ ".zip";
            string filePath = "C:\\Users\\Luo\\Desktop\\"+fileName+"";
            string MIME = "aplication/zip";
           
            return File(filePath, MIME,fileName);
        }
        #endregion
    }
}
