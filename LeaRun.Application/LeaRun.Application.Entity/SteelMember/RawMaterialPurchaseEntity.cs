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
    public class RawMaterialPurchaseEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// �ɹ�����
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALPURCHASEID")]
        public string RawMaterialPurchaseId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("CATEGORY")]
        public string Category { get; set; }
        /// <summary>
        /// �ύ
        /// </summary>
        /// <returns></returns>
        [Column("ISSUBMIT")]
        public int? IsSubmit { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("ISPASSED")]
        public int? IsPassed { get; set; }
        /// <summary>
        /// �Ƶ���
        /// </summary>
        /// <returns></returns>
        [Column("CREATEMAN")]
        public string CreateMan { get; set; }
        /// <summary>
        /// �Ƶ�ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN")]
        public string ReviewMan { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWTIME")]
        public DateTime? ReviewTime { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.RawMaterialPurchaseId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.RawMaterialPurchaseId = keyValue;
                                            }
        #endregion
    }
}