using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMember.Models
{
    public class ProduceOrderModel
    {
        public int OrderId { get; set; }
        public String OrderNumbering { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateMan { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public Nullable<int> MemberNumber { get; set; }
        public Nullable<int> ProductionNumber { get; set; }
        public Nullable<int> TreeId { get; set; }
        public string TreeName { get; set; }
        public string MemberNumbering { get; set; }//Nullable<long>
        public Nullable<int> MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberModel { get; set; }
        public Nullable<int> ProjectDemandId { get; set; }
        public string Description { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public Nullable<int> IsSubmit { get; set; }
        public string SubmitMan { get; set; }
        public string SubmitTime { get; set; }
        public Nullable<int> ConfirmOrder { get; set; }
        public string ConfirmMan { get; set; }
        public Nullable<int> RawMaterialId { get; set; }
        public string RawMaterialUnitPrice { get; set; }
        public string RawMaterialConsumption { get; set; }
        public Nullable<decimal> OrderBudget { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
       
    }
}