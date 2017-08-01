using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialLibraryModel
    {
        public int PurchaseId { get; set; }

        public string UnitName { get; set; }
        public string RawMaterialId { get; set; }
        public string RawMaterialModel { get; set; }
        public DateTime? WarehousingTime { get; set; }
        public string RawMaterialName { get; set; }
        public Nullable<int> RawMaterialNumber { get; set; }
        public string RawMaterialStandard { get; set; }
        public string Price { get; set; }
        public string UnitPrice { get; set; }
        public string PriceAmount { get; set; }
        public string Qty { get; set; }
        public string TreeId { get; set; }
        public string TreeName { get; set; }
        public string Description { get; set; }
    }
    public class RawMaterialLibraryEntityBill : RawMaterialLibraryModel {
        public string RawMaterialDosage { get; set; }
        public string AnalysisId { get; set; }
    }
}