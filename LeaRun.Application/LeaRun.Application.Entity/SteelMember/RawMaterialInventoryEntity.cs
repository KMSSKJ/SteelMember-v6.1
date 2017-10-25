using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-19 10:03
    /// �� �������Ͽ��
    /// </summary>
    public class RawMaterialInventoryEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// InventoryId
        /// </summary>
        /// <returns></returns>
        public string InventoryId { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        /// <returns></returns>
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// UnitPrice
        /// </summary>
        /// <returns></returns>
        public string UnitPrice { get; set; }
        /// <summary>
        /// RawMaterialManufacturer
        /// </summary>
        /// <returns></returns>
        public string RawMaterialManufacturer { get; set; }
        /// <summary>
        /// RawMaterialSupplier
        /// </summary>
        /// <returns></returns>
        public string RawMaterialSupplier { get; set; }
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
            this.InventoryId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.InventoryId = keyValue;
                                            }
        #endregion
    }
}