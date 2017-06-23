using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class ProcessManagementModel
    {
        public int ProcessId { get; set; }
        public Nullable<int> MemberNumber { get; set; }
        public string ProcessName { get; set; }
        public int? IsProcessTask { get; set; }
        public string ProcessRequirements { get; set; }
        public int? IsProcessStatus { get; set; }
        public string ProcessMan { get; set; }
        public string ProcessManImge { get; set; }
        public int? ProcessNumbered { get; set; }
        public int? UnqualifiedNumber { get; set; }
        public int? SortCode { get; set; }
        public int? MemberId { get; set; }
        public string MemberNumbering { get; set; }//Nullable<long>MemberNumbering
        public Nullable<System.DateTime> ProduceStartDate { get; set; }
        public Nullable<System.DateTime> ProduceEndDate { get; set; }
        public byte[] QRCode { get; set; }
        public string Description { get; set; }
    }
}