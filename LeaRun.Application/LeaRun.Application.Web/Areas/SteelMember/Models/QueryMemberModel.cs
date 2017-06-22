using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteelMember.Models
{
    public class QueryMemberModel
    {
        public string MemberModel { get; set; }
        public Nullable<DateTime> InBeginTime { get; set; }
        public Nullable<DateTime> InEndTime { get; set; }
    }
}
