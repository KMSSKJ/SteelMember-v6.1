using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-26 17:17
    /// �� ����������
    /// </summary>
    public class RawMaterialWarehouseEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// WarehouseId
        /// </summary>
        /// <returns></returns>
        public string WarehouseId { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// WarehouseQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? WarehouseQuantity { get; set; }

        /// <summary>
        /// RawMaterialSupplier
        /// </summary>
        /// <returns></returns>
        public string RawMaterialSupplier { get; set; }

        /// <summary>
        /// WarehouseTime
        /// </summary>
        /// <returns></returns>
        public DateTime? WarehouseTime { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.WarehouseId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.WarehouseId = keyValue;
                                            }
        #endregion
    }
}