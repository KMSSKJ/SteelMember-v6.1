using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialInventoryModel: Entity.SteelMember.RawMaterialLibraryEntity
    {
      
        public string InventoryId { get; set; }
        
        //public string RawMaterialId { get; set; }
       
        public decimal? Quantity { get; set; }
       
        //public string Category { get; set; }

    }
    public class InventoryInfoModel: Entity.SteelMember.RawMaterialLibraryEntity
    {
        public decimal? PurchaseQuantity { get; set; }
        public decimal? Price { get; set; }
        ///<summary>
        ///订单编号
        ///</summary>
        public string RawMaterialPurchaseId { get; set; }
        ///<summary>
        ///订单明细ID
        ///</summary>
        public string InfoId { get; set; }
    }
}