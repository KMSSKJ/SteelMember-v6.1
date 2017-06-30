using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberMaterialModel
    {
        public int MemberMaterialId { get; set; }
        public string MemberId { get; set; }
        public int? MaterialNumber { get; set; }
        public string Description { get; set; }
        public int? RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public string RawMaterialStandard { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
    }
}