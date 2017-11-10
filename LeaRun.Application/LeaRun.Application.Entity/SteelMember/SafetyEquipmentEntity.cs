using System;
using LeaRun.Application.Code;
using System.ComponentModel.DataAnnotations;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// 版 本
    /// 日 期：2017-08-29 15:39
    /// 描 述：安全设备
    /// </summary>
    public class SafetyEquipmentEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// StandardModel
        /// </summary>
        /// <returns></returns>
        public string StandardModel { get; set; }
        /// <summary>
        /// SafetyResponsiblePerson
        /// </summary>
        /// <returns></returns>
        public string SafetyResponsiblePerson { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        /// <returns></returns>
        public string Icon { get; set; }
        /// <summary>
        /// ProduceManufacturers
        /// </summary>
        /// <returns></returns>
        public string ProduceManufacturers { get; set; }
        /// <summary>
        /// ManufacturersTel
        /// </summary>
        /// <returns></returns>
        public string ManufacturersTel { get; set; }
        /// <summary>
        /// ApproachDate
        /// </summary>
        /// <returns></returns>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ApproachDate { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// MaintenanceDate
        /// </summary>
        /// <returns></returns>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? MaintenanceDate { get; set; }
        /// <summary>
        /// WarrantyDate
        /// </summary>
        /// <returns></returns>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? WarrantyDate { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// UploadTime
        /// </summary>
        /// <returns></returns>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? UpdateTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}