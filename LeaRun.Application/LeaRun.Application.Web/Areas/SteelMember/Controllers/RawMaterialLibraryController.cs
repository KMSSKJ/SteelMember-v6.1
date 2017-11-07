using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util.Extension;
using System;
using LeaRun.Application.Code;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Data;
using LeaRun.Application.Entity.SystemManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2017-07-06 10:42
    /// 描 述：材料管理
    /// </summary>
    public class RawMaterialLibraryController : MvcControllerBase
    {
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
        public ActionResult ImportFile()
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
            var data = rawmateriallibrarybll.GetPageList(pagination, queryJson).ToList();
            if (data.Count() > 0)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    string a = data[i].Category;
                    if (!ValidateUtil.IsHasCHZN(a))
                    {
                        data[i].Unit = dataitemdetailbll.GetEntity(data[i].Unit).ItemName;
                        data[i].Category = dataitemdetailbll.GetEntity(data[i].Category).ItemName;
                    }
                }
            }
            var queryParam = queryJson.ToJObject();
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                data = data.FindAll(t => t.UpdateTime >= BeginTime);
                data = data.FindAll(t => t.UpdateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                data = data.FindAll(t => t.UpdateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                data = data.FindAll(t => t.UpdateTime <= EndTime);
            }

            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                data = data.FindAll(t => t.Category == Category);
            }
            if (!queryParam["RawMaterialName"].IsEmpty())
            {
                string RawMaterialName = queryParam["RawMaterialName"].ToString();
                data = data.FindAll(t => t.RawMaterialName.Contains(RawMaterialName));
            }
            if (!queryParam["RawMaterialModel"].IsEmpty())
            {
                string RawMaterialModel = queryParam["RawMaterialModel"].ToString();
                data = data.FindAll(t => t.RawMaterialModel.Contains(RawMaterialModel));
            }

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
        /// 
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="RawMaterialLibrary"></param>
        /// <returns></returns>
         [HttpPost]
        public ContentResult SubmitImportFile(string KeyValue, RawMaterialLibraryEntity RawMaterialLibrary)
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
                if (table.Columns.Count != 4)
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
                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        if (!table.Rows[i].IsNull(0))
                        {
                            string RawMaterialName = table.Rows[i][0].ToString().Trim();
                            string RawMaterialModel = table.Rows[i][1].ToString().Trim();

                            var _RawMaterialLibrary = rawmateriallibrarybll.GetList(f => f.RawMaterialName == RawMaterialName && f.RawMaterialModel == RawMaterialModel);
                            if (_RawMaterialLibrary.Count() == 0)
                            {
                                RawMaterialLibrary.Category = KeyValue;
                                RawMaterialLibrary.UpdateTime = DateTime.Now;

                                RawMaterialLibrary.RawMaterialName = table.Rows[i][0].ToString().Trim();
                                RawMaterialLibrary.RawMaterialModel = table.Rows[i][1].ToString().Trim();

                                //自动获取计量单位ID,没有就添加
                                var MemberUnit = dataItemCache.GetDataItemList("UnitName");
                                var a = MemberUnit.FirstOrDefault();
                                if (table.Rows[i][2].ToString().Trim().Count() <= 4 && table.Rows[i][2].ToString().Trim().Count() > 0)
                                {
                                    var Unit = MemberUnit.Where(w => w.ItemName == table.Rows[i][2].ToString().Trim()).SingleOrDefault();
                                    if (Unit != null)
                                    {
                                        RawMaterialLibrary.Unit = Unit.ItemDetailId;
                                    }
                                    else
                                    {
                                        var DataItemDetail = new DataItemDetailEntity()
                                        {
                                            ItemId = a.ItemId,
                                            ItemName = table.Rows[i][2].ToString().Trim(),
                                            ItemValue = table.Rows[i][2].ToString().Trim()
                                        };
                                        var UnitId = dataitemdetailbll.ReturnSaveForm("", DataItemDetail);
                                        RawMaterialLibrary.Unit = UnitId;
                                    }
                                }
                                else
                                {
                                    return Content("操作失败：要导入的单位数据" + table.Rows[i][2].ToString().Trim() + "数据长度过大");
                                }
                               // RawMaterialLibrary.Description = table.Rows[i][3].ToString() + "—外部导入数据，请添加该构件所需材料！";
                                rawmateriallibrarybll.SaveForm("", RawMaterialLibrary);

                                //var entitys1 = new MemberWarehouseEntity()
                                //{
                                //    MemberId = memberId,
                                //    InStock = 0,
                                //    EngineeringId = KeyValue,
                                //    UpdateTime = DateTime.Now,
                                //};
                                //memberwarehousebll.SaveForm("", entitys1);
                            }
                            else
                            {
                                _count++;
                            }
                        }
                    }
                    if (table.Rows.Count - count == _count || (_count == 1 && table.Rows.Count - count == _count))
                    {
                        return Content("操作失败：要导入的数据在该分类或其他材料分类下已存在");
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
            }
            catch (Exception ex)
            {
                return Content("操作失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {

            string[] idsArr = keyValue.Split(',');
            foreach (var item in idsArr)
            {
                int number = 0;
                var memberMaterial = membermaterialbll.GetList(f => f.RawMaterialId == item);
                var MaterialAnalysis = rawmaterialanalysisbll.GetList(f => f.RawMaterialId == item);
                number = memberMaterial.Count() + MaterialAnalysis.Count();
                if (number == 0)
                {
                    rawmateriallibrarybll.RemoveForm(item);
                }
                else
                {
                    return Error("数据中存在关联数据");

                }
            }
            return Success("删除成功");
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
            entity.UpdateTime = DateTime.Now;
            rawmateriallibrarybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 导出Excell模板
        /// </summary>
        /// <returns></returns>
        public void GetExcellTemperature()
        {
            var Path = this.Server.MapPath("/Resource/ExcelTemplate/材料信息导入模板.xls");// UserId,
            FileInfo file = new FileInfo(Path);
            var name = "材料信息导入模板.xls";
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
            Response.AppendHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(file.FullName);
            Response.Flush();
        }
        #endregion

        #region 验证数据

        /// <summary>
        /// 材料中型号不能重复
        /// </summary>
        /// <param name="RawMaterialModel">型号</param>
        /// <param name="RawMaterialName"></param>
        /// <param name="keyValue"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ExistFullName(string RawMaterialModel, string RawMaterialName, string category, string keyValue)
        {
            bool IsOk = rawmateriallibrarybll.ExistFullName(RawMaterialModel.Trim(), RawMaterialName.Trim(), category, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
