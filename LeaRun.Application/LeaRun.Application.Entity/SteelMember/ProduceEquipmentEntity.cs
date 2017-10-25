using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-08-29 15:00
    /// �� ���������豸
    /// </summary>
    public class ProduceEquipmentEntity : BaseEntity
    {
        #region ʵ���Ա
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
        /// Operator
        /// </summary>
        /// <returns></returns>
        public string Operator { get; set; }
        /// <summary>
        /// ServicePrincipal
        /// </summary>
        /// <returns></returns>
        public string ServicePrincipal { get; set; }
        /// <summary>
        /// ElectricPrincipal
        /// </summary>
        /// <returns></returns>
        public string ElectricPrincipal { get; set; }
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
        public DateTime? MaintenanceDate { get; set; }
        /// <summary>
        /// WarrantyDate
        /// </summary>
        /// <returns></returns>
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
        public DateTime? UpdateTime { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}