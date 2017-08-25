using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-11 10:12
    /// �� ����������������
    /// </summary>
    public class MemberProductionOrderInfoEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        [Column("INFOID")]
        public string InfoId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        /// <returns></returns>
        [Column("MEMBERID")]
        public string MemberId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONQUANTITY")]
        public int? ProductionQuantity { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONEDQUANTITY")]
        public int? ProductionedQuantity { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// �Լ�ϸ���
        /// </summary>
        /// <returns></returns>
        [Column("SELFDETECTNUMBER")]
        public int? SelfDetectNumber { get; set; }
        /// <summary>
        /// �Լ챸ע
        /// </summary>
        /// <returns></returns>
        [Column("SELFDETECTREMARKS")]
        public string SelfDetectRemarks { get; set; }
        /// <summary>
        /// �����ʼ�ϸ���
        /// </summary>
        /// <returns></returns>
        [Column("QUALITYINSPECTIONNUMBER")]
        public int? QualityInspectionNumber { get; set; }

        /// <summary>
        /// �����ʼ챸ע
        /// </summary>
        /// <returns></returns>
        [Column("QUALITYINSPECTIONREMARKS")]
        public string QualityInspectionRemarks { get; set; }
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