using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class ToBeDoneModel
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string FullHead { get; set; }
        public string FullHeadColor { get; set; }
        public DateTime? ReleaseTime { get; set; }
    }
}