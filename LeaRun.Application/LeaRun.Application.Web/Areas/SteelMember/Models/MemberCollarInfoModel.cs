using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberCollarInfoModel
    {
        public string MemberWarehouseId { get; set; }
        public string OrderId { get; set; }
        public string MemberId { get; set; }
        public string InfoId { get; set; }
        public string Category { get; set; }
        public string MemberName { get; set; }
        public string MemberNumbering { get; set; }
        public string UnitId { get; set; }
        public decimal? CollarQuantity { get; set; }
        public decimal? CollaredQuantity { get; set; }
        public decimal? InStock { get; set; }
        public decimal? Quantity { get; set; }
        public string Description { get; set; }
    }
}