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
    
    public partial class RMC_SubProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RMC_SubProject()
        {
            this.RMC_MemberLibrary = new HashSet<RMC_MemberLibrary>();
        }
    
        public string Id { get; set; }
        public string ParentId { get; set; }
        public Nullable<int> Levels { get; set; }
        public string FullName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RMC_MemberLibrary> RMC_MemberLibrary { get; set; }
    }
}