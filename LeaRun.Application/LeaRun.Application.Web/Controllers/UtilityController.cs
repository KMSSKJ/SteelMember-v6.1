using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using LeaRun.Util.Offices;
using System.Text;
using LeaRun.Utilities;

namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本
    /// 日 期：2016.2.03 10:58
    /// 描 述：公共控制器
    /// </summary>
    public class UtilityController : Controller
    {
        #region 验证对象值不能重复
        #endregion

        #region 导出Excel
        /// <summary>
        /// 请选择要导出的字段页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelExportForm()
        {
            return View();
        }

        /// <summary>
        /// 执行Json导出Excel
        /// </summary>
        /// <param name="columnJson">表头</param>
        /// <param name="rowJson">数据</param>
        /// <param name="exportField">导出字段</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public void ExecuteExportExcel(string columnJson, string rowJson, string exportField, string filename)
        {
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = Server.UrlDecode(filename);
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 15;
            excelconfig.FileName = Server.UrlDecode(filename) + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnEntity>();
            //表头
            List<GridColumnModel> columnData = columnJson.ToList<GridColumnModel>();
            //行数据
            DataTable rowData = rowJson.ToTable();
            //写入Excel表头
            string[] fieldInfo = exportField.Split(',');
            foreach (string item in fieldInfo)
            {
                var list = columnData.FindAll(t => t.name == item);
                foreach (GridColumnModel gridcolumnmodel in list)
                {
                    if (gridcolumnmodel.hidden.ToLower() == "false" && gridcolumnmodel.label != null)
                    {
                        string align = gridcolumnmodel.align;
                        excelconfig.ColumnEntity.Add(new ColumnEntity()
                        {
                            Column = gridcolumnmodel.name,
                            ExcelColumn = gridcolumnmodel.label,
                            //Width = gridcolumnmodel.width,
                            Alignment = gridcolumnmodel.align,
                        });
                    }
                }
            }
            ExcelHelper.ExcelDownload(rowData, excelconfig);
        }
        #endregion

        #region JqGrid导出Excel
        /// <summary>
        /// 获取要导出表头字段
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeriveExcelColumn()
        {
            string JsonColumn = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonColumn_DeriveExcel"));
            JsonColumn = JsonColumn.Replace("\"Icon\",\"hidden\":false", "\"Icon\",\"hidden\":true");
            return Content(JsonColumn);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="ExportField">要导出字段</param>
        public void GetDeriveExcel(string ExportField)
        {
            string JsonColumn = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonColumn_DeriveExcel"));
            string JsonFooter = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonFooter_DeriveExcel"));
            string fileName = GZipHelper.Uncompress(CookieHelper.GetCookie("FileName_DeriveExcel"));
            string TableHeader = GZipHelper.Uncompress(CookieHelper.GetCookie("TableHeader_DeriveExcel"));
            string TableObject = GZipHelper.Uncompress(CookieHelper.GetCookie("TableObject_DeriveExcel"));
            string JsonData = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonData_DeriveExcel"));
            //<img style=\"width:44px;height:32px;\" src=\"工字钢GB10/工字钢GB10.jpg\" onmouseover=\"showBigImg(event,'工字钢GB10/工字钢GB10.jpg')\" onmouseout=\"leaveBigImg(event)\">
            JsonData = JsonData.Replace("../../Resource/Document/NetworkDisk/System/Member/", "").Replace("style=\\\"width:44px;height:32px;\\\"", "").Replace("onmouseout=\\\"leaveBigImg(event)\\\"", "").Replace("onmouseover=\\\"showBigImg(event,", "");
            //JsonData = JsonData.Replace("<img src=\....ContentImagescheckokmark.gif\>", "有").Replace("<img src=\....ContentImageschecknomark.gif\>", "无");
            DeriveExcel.JqGridToExcel(JsonColumn, JsonData, ExportField, fileName, TableHeader, TableObject);


            //CookieHelper.DelCookie("JsonColumn_DeriveExcel");
            //CookieHelper.DelCookie("JsonData_DeriveExcel");
            //CookieHelper.DelCookie("JsonFooter_DeriveExcel");
            //CookieHelper.DelCookie("FileName_DeriveExcel");
        }
        /// <summary>
        /// 写入数据到Cookie
        /// </summary>
        /// <param name="JsonColumn">表头</param>
        /// <param name="JsonData">数据</param>
        /// <param name="JsonFooter">底部合计</param>
        /// <param name="FileName"></param>
        /// <param name="TableHeader"></param>
        /// <param name="TableObject"></param>
        [ValidateInput(false)]
        public void SetDeriveExcel(string JsonColumn, string JsonData, string JsonFooter, string FileName, string TableHeader, string TableObject)
        {
            CookieHelper.WriteCookie("JsonColumn_DeriveExcel", GZipHelper.Compress(JsonColumn));
            CookieHelper.WriteCookie("JsonData_DeriveExcel", GZipHelper.Compress(JsonData));
            CookieHelper.WriteCookie("JsonFooter_DeriveExcel", GZipHelper.Compress(JsonFooter));
            CookieHelper.WriteCookie("FileName_DeriveExcel", GZipHelper.Compress(FileName));
            CookieHelper.WriteCookie("TableHeader_DeriveExcel", GZipHelper.Compress(TableHeader));
            if (TableObject != null)
            {
                CookieHelper.WriteCookie("TableObject_DeriveExcel", GZipHelper.Compress(TableObject));
            }
        }
        #endregion

        #region 生成打印
        /// <summary>
        /// 打印当前页
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintPage()
        {
            return View();
        }
        #endregion
    }
}
