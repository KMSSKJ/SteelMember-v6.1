using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class ProjectDemandModel
    {
        //领用开始
        public string CollarNumbering { get; set; }
        public int? CollarId { get; set; }
        public DateTime? CollarTime { get; set; }
        public string CollarMan { get; set; }
        public string Librarian { get; set; }
        //领用结束

        public int ProjectDemandId { get; set; }
        public string TreeName { get; set; }
        public int? ProductionNumber { get; set; }
        public int OrderId { get; set; }
        public Nullable<int> MemberClassId { get; set; }
        public string MemberClassName { get; set; }
        public string ProjectName { get; set; }
        public string MemberId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string MemberUnit { get; set; }
        public string MemberName { get; set; }
        public string MemberCompany { get; set; }
        public Nullable<int> MemberNumber { get; set; }
        public string Icon { get; set; }
        public Nullable<int> LeaderNumber { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public string Priority { get; set; }
        public string Use { get; set; }
        public DateTime? LeaderTime { get; set; }
        public string LeaderMan { get; set; }
        public string MemberModel { get; set; }
        public string MemberNumbering { get; set; }//Nullable<long>
        public string OrderNumbering { get; set; }
        public string MemberWeight { get; set; }
        public string Description { get; set; }
        public string TheoreticalWeight { get; set; }
        public string UnitPrice { get; set; }
        public string CostBudget { get; set; }
        public Nullable<int> IsSubmit { get; set; }
        public Nullable<int> IsReview { get; set; }
        public string ReviewMan { get; set; }
        public Nullable<int> OrderQuantityed { get; set; }
        public Nullable<int> Productioned { get; set; }
        public Nullable<DateTime> CreateTime { get; set; }
        public Nullable<DateTime> VeliveryTime { get; set; }
        public string CreateMan { get; set; }
        public Nullable<int> IsDemandSubmit { get; set; }
        public string CADDrawing { get; set; }
        public string ModelDrawing { get; set; }
        public int? CollarNumbered { get; set; }
    }
}