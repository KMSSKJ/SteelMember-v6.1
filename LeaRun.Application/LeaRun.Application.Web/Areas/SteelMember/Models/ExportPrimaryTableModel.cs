using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class ExportPrimaryTableModel
    {

        /// <summary>
        /// 导出表名
        /// </summary>
        public string ExportFileName { get; set; }

        /// <summary>
        /// 导出表标题
        /// </summary>
        public string ExportTableTitle { get; set; }

        /// <summary>
        ///工程名称
        /// </summary>
        public string SubProjectId { get; set; }
        
        /// <summary>
        /// 单项工程名
        /// </summary>
        public string IndividualProjectName { get; set; }

        /// <summary>
        /// 施工单位
        /// </summary>
        public string ConstructionUnit { get; set; }

        /// <summary>
        /// 制表人
        /// </summary>
        public string Tabulators { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }
    }
}