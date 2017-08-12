using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberDemandModel
    {
        public string MemberUnit { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string MemberId { get; set; }
        public int? CollarNumber { get; set; }
        public string CreateMan { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Description { get; set; }
        public string SubProject { get; set; }
        public string SubProjectId { get; set; }
        public int? IsReview { get; set; }
        public string MemberDemandId { get; set; }
        public string MemberName { get; set; }
        public int? MemberNumber { get; set; }
        public string MemberNumbering { get; set; }
        public int? ProductionNumber { get; set; }
        public string ReviewMan { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? MemberWeight { get; set; }
        public string CADDrawing { get; set; }
        public string ModelDrawing { get; set; }
    }
}