using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialAnalysisModel
    {
        public string Id { get; set; }
        public string RawMaterialId { get; set; }
        public string Category { get; set; }
        public string RawMaterialCategory { get; set; }
        public string RawMaterialName { get; set; }
        public string RawMaterialModel { get; set; }
        public decimal RawMaterialDosage { get; set; }
        public decimal? ApplicationPurchasedQuantity { get; set; }
        public decimal? PurchasedQuantity { get; set; }
        public decimal? WarehousedQuantity { get; set; }
        public string RawMaterialUnit { get; set; }
        public string Description { get; set; }
        public int IsPassed { get; set; }
        public int IsSubmitReview { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
    public class Text {
        
        public string RawMaterialSupplier { get; set; }
        public string RawMaterialManufacturer { get; set; }
        public string InfoId { get; set; }
        public string RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public string RawMaterialModel { get; set; }
        public string RawMaterialDosage { get; set; }
        public string UnitId { get; set; }
        public string Description { get; set; }
        public string PurchaseQuantity { get; set; }
        public decimal Quantity { get; set; }
        public string RawMaterialPurchaseId { get; set; }
        public string RawMaterialOrderInfoId { get; set; }
        public decimal? Price { get; set; }
    }
}