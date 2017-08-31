using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialPurchaseModel : RawMaterialLibraryModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
       
        public string InfoId { get; set; }
        /// <summary>
        /// 采购单据
        /// </summary>
        /// <returns></returns>
       
        public string RawMaterialPurchaseId { get; set; }
        /// <summary>
        /// 分析主键
        /// </summary>
        /// <returns></returns>
       
        public string RawMaterialAnalysisId { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        /// <returns></returns>
       
        public decimal? PurchaseQuantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>

        public decimal? RawMaterialPurchaseModelPrice { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        /// <returns></returns>
        public string RawMaterialSupplier { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        /// <returns></returns>
        public string Inventory { get; set; }
        /// <summary>
        /// 建议采购量
        /// </summary>
        /// <returns></returns>
        public string SuggestQuantity { get; set; }

    }
}