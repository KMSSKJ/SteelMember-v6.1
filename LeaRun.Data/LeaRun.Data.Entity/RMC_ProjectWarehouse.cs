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
    
    public partial class RMC_ProjectWarehouse
    {
        public int ProjectWarehouseId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public string TreeId { get; set; }
        public Nullable<int> InStock { get; set; }
        public Nullable<int> Damage { get; set; }
        public string Class { get; set; }
        public Nullable<int> IsShiped { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string Librarian { get; set; }
        public string Leader { get; set; }
        public string Description { get; set; }
        public string MemberTreeId { get; set; }
        public Nullable<int> ProjectDemandId { get; set; }
        public string MemberModel { get; set; }
    }
}
