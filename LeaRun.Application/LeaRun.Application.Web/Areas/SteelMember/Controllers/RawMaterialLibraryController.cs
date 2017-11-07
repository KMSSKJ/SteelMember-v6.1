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
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 10:42
    /// �� �������Ϲ���
    /// </summary>
    public class RawMaterialLibraryController : MvcControllerBase
    {
        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportFile()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmateriallibrarybll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        #endregion

        #region �ύ����
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

                //HttpPostedFileBase file = Request.Files["FileUpload"];//��ȡ�ϴ����ļ�  
                string FileName;
                string savePath;
                string Photo = "";
                //int IsOk = 1;
                if (Filedatas.Count == 0)
                {
                    return Content("�ļ�����Ϊ��");
                }
                else
                {
                    var Filedata = Request.Files[0];
                    string filename = Path.GetFileName(Filedata.FileName);
                    int filesize = Filedata.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte  
                    string fileEx = System.IO.Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��  
                    string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���  
                    int Maxsize = 10000 * 1024;//�����ϴ��ļ������ռ��СΪ10M  
                    string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���  

                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmssffff") + fileEx;
                    if (!FileType.Contains(fileEx))
                    {
                        return Content("�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content("�ϴ����ļ����ܳ���10M");
                    }
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "uploads\\excel\\";
                    string virtualPath = string.Format("~/Resource/Document/NetworkDisk/Excel");
                    string fullFileName = this.Server.MapPath(virtualPath);
                    //����ļ����ڣ���ɾ��
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~") + "~/Resource/Document/NetworkDisk/Excel"))
                    {

                        Directory.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + "~/Resource/Document/NetworkDisk/Excel", true);//pdf·��
                    }
                    //����ļ������ڣ�����´������ļ�����(yyyy-MM-d)�ĸ�ʽ����
                    if (!Directory.Exists(fullFileName))
                    {
                        Directory.CreateDirectory(fullFileName);//����swf·��
                    }
                    savePath = Path.Combine(fullFileName, FileName);
                    Filedata.SaveAs(savePath);
                }
                string result = string.Empty;
                string strConn;
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + savePath + "; " + "Extended Properties=Excel 8.0;";  
                strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + savePath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'"; //�����ӿ��Բ���.xls��.xlsx�ļ� (֧��Excel2003 �� Excel2007 �������ַ���)  

                //OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);  
                //���Ӵ�  
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                //����Excel�ļܹ�����������sheet�������,���ͣ�����ʱ����޸�ʱ��ȡ�  
                DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                //����excel�б������ַ�������  
                string[] strTableNames = new string[dtSheetName.Rows.Count];
                for (int k = 0; k < dtSheetName.Rows.Count; k++)
                {
                    strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                }
                OleDbDataAdapter myCommand = null;
                DataTable dt = new DataTable();
                //��ָ���ı�����ѯ����,���Ȱ����б����г������û�ѡ��  
                string strExcel = "select*from[" + strTableNames[0] + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                //myCommand.Fill(dt);  
                DataSet myDataSet = new DataSet();
                myCommand.Fill(myDataSet, "ExcelInfo");
                // Data.Deleted();
                DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
                if (table.Columns.Count != 4)
                {
                    return Content("�ļ����ݸ�ʽ����ȷ");
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
                    return Content("�ļ��������κ�����");
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

                                //�Զ���ȡ������λID,û�о����
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
                                    return Content("����ʧ�ܣ�Ҫ����ĵ�λ����" + table.Rows[i][2].ToString().Trim() + "���ݳ��ȹ���");
                                }
                               // RawMaterialLibrary.Description = table.Rows[i][3].ToString() + "���ⲿ�������ݣ�����Ӹù���������ϣ�";
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
                        return Content("����ʧ�ܣ�Ҫ����������ڸ÷�����������Ϸ������Ѵ���");
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
                return Content("����ʧ�ܣ�" + ex.Message);
            }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
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
                    return Error("�����д��ڹ�������");

                }
            }
            return Success("ɾ���ɹ�");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, RawMaterialLibraryEntity entity)
        {
            entity.UpdateTime = DateTime.Now;
            rawmateriallibrarybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ����Excellģ��
        /// </summary>
        /// <returns></returns>
        public void GetExcellTemperature()
        {
            var Path = this.Server.MapPath("/Resource/ExcelTemplate/������Ϣ����ģ��.xls");// UserId,
            FileInfo file = new FileInfo(Path);
            var name = "������Ϣ����ģ��.xls";
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

        #region ��֤����

        /// <summary>
        /// �������ͺŲ����ظ�
        /// </summary>
        /// <param name="RawMaterialModel">�ͺ�</param>
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
