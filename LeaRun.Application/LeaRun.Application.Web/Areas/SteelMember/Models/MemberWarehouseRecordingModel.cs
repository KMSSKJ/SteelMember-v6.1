using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberWarehouseRecordingModel
    {
        public string RecordingId { get; set; }
        public string MemberNumbering { get; set; }
        public string MemberName { get; set; }
        public string Category { get; set; }
        public string CollarEngineering { get; set; }
        public string UnitId { get; set; }
        public decimal? InStock { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ToReportPeople { get; set; }
        public string CollarDepartment { get; set; }
        public string Receiver { get; set; }
        public string ReceiverTel { get; set; }
        public string Librarian { get; set; }
        public string Description { get; set; }
    }
}