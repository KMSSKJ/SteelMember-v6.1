//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaRun.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class RMC_ProjectInfo
    {
        public int ProjectId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> TreeID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectSystemTitel { get; set; }
        public string ProjectLogo { get; set; }
        public string ProjectBackground { get; set; }
        public Nullable<int> State { get; set; }
        public string ProjectAddress { get; set; }
        public Nullable<int> ConstructionPermitId { get; set; }
        public string ConstructionUnit { get; set; }
        public string ConstructionPrincipal { get; set; }
        public string ConstructionPrincipalTEL { get; set; }
        public string DesignUnit { get; set; }
        public string DesignPrincipal { get; set; }
        public string DesignPrincipalTEL { get; set; }
        public string SupervisionUnit { get; set; }
        public string SupervisionPrincipal { get; set; }
        public string SupervisionPrincipalTEL { get; set; }
        public string GeneralContractor { get; set; }
        public string GeneralContractorPrincipal { get; set; }
        public string GeneralContractorPrincipalTEL { get; set; }
        public string ProfessionalContractor { get; set; }
        public string ProfessionalContractorPrincipal { get; set; }
        public string ProfessionalContractorPrincipalTEL { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public string Description { get; set; }
    }
}
