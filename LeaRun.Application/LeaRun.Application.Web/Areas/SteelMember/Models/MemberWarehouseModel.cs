using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberWarehouseModel
    {
        public string MemberWarehouseId { get; set; }
        public string OrderId { get; set; }
        public string MemberId { get; set; }
        public string EngineeringId { get; set; }
        public string Category { get; set; }
        public string MemberModel { get; set; }
        public string MemberName { get; set; }
        public string MemberNumbering { get; set; }
        public string UnitId { get; set; }
        public decimal? ProductionQuantity { get; set; }
        public decimal? ProductionedQuantity { get; set; }
        public decimal? WarehousedQuantity { get; set; }
        public decimal? QualityInspectionNumber { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Librarian { get; set; }
        public string Leader { get; set; }
        public decimal? InStock { get; set; }
        public string Description { get; set; }
    }
}