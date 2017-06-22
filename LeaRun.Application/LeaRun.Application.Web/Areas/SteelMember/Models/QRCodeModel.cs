using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMember.Models
{
    public class QRCodeModel
    {
        public string ProjectName { get; set; }
        public string MemberName { get; set; }
        public string MemberNumbering { get; set; }// Nullable<long> 
        public string TheoreticalWeight { get; set; }
        public string Dimensions { get; set; }
        public string MemberModel { get; set; }
    }
}