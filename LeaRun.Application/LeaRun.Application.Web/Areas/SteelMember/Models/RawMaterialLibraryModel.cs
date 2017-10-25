using LeaRun.Application.Entity.SteelMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RMLibraryModel:RawMaterialLibraryEntity
        {
         }
    public class RawMaterialLibraryModel
    {
        public string PurchaseId { get; set; }
        public string InventoryId { get; set; }
        public string UnitId { get; set; }
        public string InfoId { get; set; }
        public string RawMaterialId { get; set; }
        public string RawMaterialAnalysisId { get; set; }
        public string RawMaterialModel { get; set; }
        public string Category { get; set; }
        public DateTime? WarehousingTime { get; set; }
        public string RawMaterialName { get; set; }
        public Nullable<int> RawMaterialNumber { get; set; }
        public decimal? PurchasedQuantity { get; set; }
        public decimal? CollarQuantity { get; set; }
        public decimal? CollaredQuantity { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? InventoryQuantity { get; set; }
        public decimal? Price { get; set; }
       // public string UnitPrice { get; set; }
        public string PriceAmount { get; set; }
        public decimal? Qty { get; set; }
        public string RawMaterialManufacturer { get; set; }
        public string RawMaterialSupplier { get; set; }
        public decimal? InventoryQty { get; set; }
        public string TreeId { get; set; }
        public string TreeName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
    public class RawMaterialLibraryEntityBill : RawMaterialLibraryModel {
        public string RawMaterialDosage { get; set; }
        public string AnalysisId { get; set; }
    }
}