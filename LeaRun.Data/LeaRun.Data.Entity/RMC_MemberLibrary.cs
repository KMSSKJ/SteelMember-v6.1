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
    
    public partial class RMC_MemberLibrary
    {
        public int MemberID { get; set; }
        public Nullable<int> TreeID { get; set; }
        public string MemberName { get; set; }
        public string MemberModel { get; set; }
        public string MemberNumbering { get; set; }
        public string MemberUnit { get; set; }
        public string EngineeringClass { get; set; }
        public Nullable<int> SectionalSizeB { get; set; }
        public Nullable<int> SectionalSize_h { get; set; }
        public Nullable<int> SectionalSize_b { get; set; }
        public Nullable<decimal> SectionalSizeD { get; set; }
        public Nullable<int> SectionalSize_d { get; set; }
        public Nullable<decimal> SectionalSize_t { get; set; }
        public Nullable<decimal> SectionalSize_r { get; set; }
        public Nullable<decimal> SectionalSize_r1 { get; set; }
        public Nullable<decimal> SectionalArea { get; set; }
        public Nullable<decimal> SurfaceArea { get; set; }
        public string TheoreticalWeight { get; set; }
        public string UnitPrice { get; set; }
        public Nullable<decimal> InertiaDistance_x { get; set; }
        public Nullable<decimal> InertiaDistance_y { get; set; }
        public Nullable<decimal> InertiaDistance_y1 { get; set; }
        public Nullable<decimal> InertiaDistance_x1 { get; set; }
        public Nullable<decimal> InertiaDistance_x0 { get; set; }
        public Nullable<decimal> InertiaDistance_y0 { get; set; }
        public Nullable<decimal> InertiaDistance_u { get; set; }
        public Nullable<decimal> InertiaRadius_x { get; set; }
        public Nullable<decimal> InertiaRadius_y { get; set; }
        public Nullable<decimal> InertiaRadius_x0 { get; set; }
        public Nullable<decimal> InertiaRadius_y0 { get; set; }
        public Nullable<decimal> InertiaRadius_u { get; set; }
        public Nullable<decimal> SectionalModulus_x { get; set; }
        public Nullable<decimal> SectionalModulus_y { get; set; }
        public Nullable<decimal> SectionalModulus_x0 { get; set; }
        public Nullable<decimal> SectionalModulus_y0 { get; set; }
        public Nullable<decimal> SectionalModulus_u { get; set; }
        public Nullable<decimal> GravityCenterDistance_0 { get; set; }
        public Nullable<decimal> GravityCenterDistance_x0 { get; set; }
        public Nullable<decimal> GravityCenterDistance_y0 { get; set; }
        public string CAD_Drawing { get; set; }
        public string Model_Drawing { get; set; }
        public Nullable<int> IsRawMaterial { get; set; }
        public Nullable<int> IsProcess { get; set; }
        public string Icon { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<int> IsReview { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public string FullPath { get; set; }
        public Nullable<System.DateTime> UploadTime { get; set; }
        public Nullable<int> Sort { get; set; }
    }
}
