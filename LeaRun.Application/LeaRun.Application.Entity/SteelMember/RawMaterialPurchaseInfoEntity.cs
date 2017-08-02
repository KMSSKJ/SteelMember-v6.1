using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-08 11:58
    /// �� ����ԭ���ϲɹ�����
    /// </summary>
    public class RawMaterialPurchaseInfoEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("INFOID")]
        public string InfoId { get; set; }
        /// <summary>
        /// �ɹ�����
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALPURCHASEID")]
        public string RawMaterialPurchaseId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALANALYSISID")]
        public string RawMaterialAnalysisId { get; set; }
        /// <summary>
        /// �ɹ�����
        /// </summary>
        /// <returns></returns>
        [Column("PURCHASEQUANTITY")]
        public decimal? PurchaseQuantity { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }
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