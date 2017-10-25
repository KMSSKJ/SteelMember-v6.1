using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialNumberModel
    {
        public decimal? RawMaterialNumber { get; set; }
        public string Category { get; set; }
        public string InventoryId { get; set; }
        public string RawMaterialId { get; set; }
        public string RawMaterialAnalysisId { get; set; }
        public string Description { get; set; }
        public string RawMaterialModel { get; set; }
        public string UnitId { get; set; }
        public string RawMaterialName { get; set; }
    }
}