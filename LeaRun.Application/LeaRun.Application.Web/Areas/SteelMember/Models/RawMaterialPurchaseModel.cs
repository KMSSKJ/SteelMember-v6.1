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
       
#pragma warning disable CS0108 // “RawMaterialPurchaseModel.InfoId”隐藏继承的成员“RawMaterialLibraryModel.InfoId”。如果是有意隐藏，请使用关键字 new。
        public string InfoId { get; set; }
#pragma warning restore CS0108 // “RawMaterialPurchaseModel.InfoId”隐藏继承的成员“RawMaterialLibraryModel.InfoId”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 申请单据
        /// </summary>
        /// <returns></returns>
       
        public string RawMaterialPurchaseId { get; set; }
#pragma warning disable CS0108 // “RawMaterialPurchaseModel.RawMaterialAnalysisId”隐藏继承的成员“RawMaterialLibraryModel.RawMaterialAnalysisId”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 分析主键
        /// </summary>
        /// <returns></returns>
        public string RawMaterialAnalysisId { get; set; }
#pragma warning restore CS0108 // “RawMaterialPurchaseModel.RawMaterialAnalysisId”隐藏继承的成员“RawMaterialLibraryModel.RawMaterialAnalysisId”。如果是有意隐藏，请使用关键字 new。

        /// <summary>
        /// 申请主键
        /// </summary>
        /// <returns></returns>
        public string RawMaterialOrderInfoId { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        /// <returns></returns>
       
        public decimal? PurchaseQuantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>

        public decimal? RawMaterialPurchaseModelPrice { get; set; }

#pragma warning disable CS0108 // “RawMaterialPurchaseModel.RawMaterialManufacturer”隐藏继承的成员“RawMaterialLibraryModel.RawMaterialManufacturer”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 生产商
        /// </summary>
        /// <returns></returns>
        public string RawMaterialManufacturer { get; set; }
#pragma warning restore CS0108 // “RawMaterialPurchaseModel.RawMaterialManufacturer”隐藏继承的成员“RawMaterialLibraryModel.RawMaterialManufacturer”。如果是有意隐藏，请使用关键字 new。
#pragma warning disable CS0108 // “RawMaterialPurchaseModel.RawMaterialSupplier”隐藏继承的成员“RawMaterialLibraryModel.RawMaterialSupplier”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 供应商
        /// </summary>
        /// <returns></returns>
        public string RawMaterialSupplier { get; set; }
#pragma warning restore CS0108 // “RawMaterialPurchaseModel.RawMaterialSupplier”隐藏继承的成员“RawMaterialLibraryModel.RawMaterialSupplier”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 生产商
        /// </summary>
        /// <returns></returns>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 库存量
        /// </summary>
        /// <returns></returns>
        public string Inventory { get; set; }
#pragma warning disable CS0108 // “RawMaterialPurchaseModel.Quantity”隐藏继承的成员“RawMaterialLibraryModel.Quantity”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 建议申请量
        /// </summary>
        /// <returns></returns>
        public decimal? Quantity { get; set; }
#pragma warning restore CS0108 // “RawMaterialPurchaseModel.Quantity”隐藏继承的成员“RawMaterialLibraryModel.Quantity”。如果是有意隐藏，请使用关键字 new。

    }
}