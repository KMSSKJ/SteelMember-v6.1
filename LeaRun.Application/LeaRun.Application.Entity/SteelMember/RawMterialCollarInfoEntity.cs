using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-12 09:47
    /// �� ����������������
    /// </summary>
    public class RawMterialCollarInfoEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        public string InfoId { get; set; }
        /// <summary>
        /// InventoryId
        /// </summary>
        /// <returns></returns>
        public string InventoryId { get; set; }
    /// <summary>
    /// CollarId
    /// </summary>
    /// <returns></returns>
    public string CollarId { get; set; }
        /// <summary>
        /// InventoryId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// RawMaterialAnalysisId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialAnalysisId { get; set; }
        /// <summary>
        /// CollarQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? CollarQuantity { get; set; }
        /// <summary>
        /// CollarQuantity
        /// </summary>
        /// <returns></returns>
        public decimal? CollaredQuantity { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        /// <returns></returns>
        public decimal? Quantity { get; set; }
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
            this.InfoId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.InfoId = keyValue;
                                            }
        #endregion
    }
}