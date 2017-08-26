using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class MemberMaterialModel
    {
        public string RawMaterialModel { get; set; }
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }
        public int? RawMaterialNumber { get; set; }
        public string Description { get; set; }

        public string TreeName { get; set; }
        public string Category { get; set; }
        public string MemberMaterialId { get; set; }
        public string RawMaterialId { get; set; }
        public string MemberId { get; set; }

        #region 原材料订单
        public string InfoId { get; set; }
        #endregion
    }
}