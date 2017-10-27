using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class SystemConfigurationModel
    {
        public string SystemLogo { get; set; }
        public string SystemName { get; set; }
        public string ConstructUnit { get; set; }
        public string Cu_principal { get; set; }
        public string InvestigationUnit { get; set; }
        public string Iu_principal { get; set; }
        public string DesignUnit { get; set; }
        public string Du_principal { get; set; }
        public string ConstructionUnit { get; set; }
        public string Ctu_principal { get; set; }
        public string SupervisionUnit { get; set; }
        public string Su_principal { get; set; }
        public string EngineeringName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedDuration { get; set; }
        public DateTime UploadDate { get; set; }
        public string EngineeringImg { get; set; }
        public string EngineeringOverview { get; set; }
    }
}