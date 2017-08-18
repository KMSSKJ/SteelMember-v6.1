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
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 10:42
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public class RawMaterialLibraryController : MvcControllerBase
    {
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private Cache.DataItemCache dataItemCache = new Cache.DataItemCache();


        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
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
        //��ȡ���ֽ��ӽڵ�(��ѭ��)
        //public List<DataItemDetailEntity> GetSonId(string ItemDetailId)
        //{
        //    List<DataItemDetailEntity> list = dataItemDetailBLL.GetByParentToItemIdIdList(ItemDetailId);
        //    return list.Concat(list.SelectMany(t => GetSonId(t.ParentId))).ToList();
        //}
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
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            rawmateriallibrarybll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
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
            rawmateriallibrarybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion

        #region ��֤����

        /// <summary>
        /// ԭ�������ͺŲ����ظ�
        /// </summary>
        /// <param name="RawMaterialModel">�ͺ�</param>
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
        #region ����excel
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
                    return Content("�ļ�����Ϊ��,��ѡ�񣡣�");
                }
                else
                {
                    var Filedata = Request.Files[0];
                    string filename = Path.GetFileName(Filedata.FileName);
                    int filesize = Filedata.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С�����ֽ�Ϊ��λ
                    fileEx = Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                    string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ��� 

                    //�涨�ϴ��ļ��Ĵ�С
                    //int Maxsize = 10000 * 1024;
                    int Maxsize = 10485760;
                    string FileType = ".xls,.xlsx";//�涨�ϴ����ļ������ַ���
                    FileName = NoFileName + System.DateTime.Now.ToString("yyyyMMddhhmmssffff") + fileEx;

                    if (!FileType.Contains(fileEx))
                    {
                        return Content("�ļ����Ͳ���ȷ��������ѡ���ļ�����");
                    }
                    if (filesize>Maxsize)
                    {
                        return Content("�ļ���С���ܳ���10M");
                    }

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
                //���Ӵ�  
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
                conn.Open();
                //����Excel�ļܹ�����������sheet�������,���ͣ�����ʱ����޸�ʱ��ȡ�
                System.Data.DataTable dtSheetName = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                //����excel�б������ַ�������
                string[] strTableNames = new string[dtSheetName.Rows.Count];
                for (int i = 0; i < strTableNames.Length; i++) {
                    strTableNames[i] = dtSheetName.Rows[i]["TABLE_NAME"].ToString();
                }
                OleDbDataAdapter myCommand = null;
                System.Data.DataTable dt = new System.Data.DataTable();
                //��ָ���ı�����ѯ����,���Ȱ����б����г������û�ѡ�� 
                string strExcel = "select*from[" + strTableNames[0] + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                System.Data.DataSet myDataSet = new System.Data.DataSet();
                myCommand.Fill(myDataSet, "ExcelInfo");

                System.Data.DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
                
                //int count = 0;
                

                if (table.Columns.Count!=5)
                {
                    return Content("����ʧ�ܣ�������ʹ��ϵͳ�ṩ��ģ�壡��");
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
                            var tshi = string.Format("�����{0}��ǰ����Ϊ���У������޸ĺ��ڵ��룡��", inu.ToString());
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
                        var tshi = string.Format("{0}�е�ԭ�������������ݿ��в��������ͣ������޸ĺ��ٵ������ϵ����Ա����", inu.ToString());
                        return Content(tshi);
                    }
                    else
                    {
                        ISRawMaterialType = false;
                    }
                }
                //���
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
                return Content("����ʧ�ܣ�" + e.Message);
            }

            return Content("�����ɹ�����");
        }
        #endregion
        #region ����ģ��
        public ActionResult GetTemplet(string templetname) {
            
            string fileName = templetname+ ".zip";
            string filePath = "C:\\Users\\Luo\\Desktop\\"+fileName+"";
            string MIME = "aplication/zip";
           
            return File(filePath, MIME,fileName);
        }
        #endregion
    }
}
