using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMember.Models
{
    public class ProjectWarehouseModel
    {

        public int ProjectWarehouseId { get; set; }
        public int? ProjectDemandId { get; set; }
        public int? MemberId { get; set; }
        public string MemberModel { get; set; }
        public string MemberName { get; set; }
        public string MemberNumbering { get; set; }//Nullable<long>
        public string MemberUnit { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public Nullable<int> TreeId { get; set; }
        public Nullable<int> InStock { get; set; }
        public Nullable<int> CollarNumbered { get; set; }
        public Nullable<int> Damage { get; set; }
        public string Class { get; set; }
        public string Librarian { get; set; }
        public string Leader { get; set; }
        public string Description { get; set; }
    }
}