using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialAnalysisModel
    {
        public string Id { get; set; }
        public string RawMaterialCategory { get; set; }
        public string RawMaterialStandard { get; set; }
        public string RawMaterialDosage { get; set; }
        public string RawMaterialUnit { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int IsPassed { get; set; }
        public int IsSubmitReview { get; set; }
    }
}