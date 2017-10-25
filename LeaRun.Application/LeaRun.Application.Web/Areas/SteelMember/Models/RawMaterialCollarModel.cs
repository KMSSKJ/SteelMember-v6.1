using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    public class RawMaterialCollarModel: Entity.SteelMember.RawMaterialLibraryEntity
    {
        /// <summary>
        /// CollarId
        /// </summary>
        /// <returns></returns>
        public string CollarId { get; set; }
        /// <summary>
        /// InventoryId
        /// </summary>
        /// <returns></returns>
        public string InventoryId { get; set; }
        /// <summary>
        /// CollarType
        /// </summary>
        /// <returns></returns>
        //public int? CollarType { get; set; }
        public string CollarType { get; set; }
        /// <summary>
        /// CollarEngineering
        /// </summary>
        /// <returns></returns>
        public string CollarEngineering { get; set; }
        /// <summary>
        /// CollarTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CollarTime { get; set; }
        /// <summary>
        /// CollarQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? CollarQuantity { get; set; }
        /// <summary>
        /// CollarMan
        /// </summary>
        /// <returns></returns>
        public string CollarMan { get; set; }
      
    }
}