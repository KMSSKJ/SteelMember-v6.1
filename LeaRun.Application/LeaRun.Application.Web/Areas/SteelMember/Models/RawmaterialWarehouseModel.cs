using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialwarehouseModel : Entity.SteelMember.RawMaterialWarehouseEntity
    {
    }
    public class RawMaterialWarehouseModel: Entity.SteelMember.RawMaterialLibraryEntity
    {
       
        /// <summary>
        /// WarehouseId
        /// </summary>
        /// <returns></returns>
        public string WarehouseId { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public new string RawMaterialId { get; set; }
        /// <summary>
        /// WarehouseQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? WarehouseQuantity { get; set; }
        /// <summary>
        /// WarehouseTime
        /// </summary>
        /// <returns></returns>
        public DateTime? WarehouseTime { get; set; }
      
        public string RawMaterialSupplier { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public new string Description { get; set; }
    }
}