using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-08 11:58
    /// �� ���������������
    /// </summary>
    public class RawMaterialPurchaseEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���뵥��
        /// </summary>
        /// <returns></returns>
        [Column("RAWMATERIALPURCHASEID")]
        public string RawMaterialPurchaseId { get; set; }
        /// <summary>
        /// ���뵥��
        /// </summary>
        /// <returns></returns>
        [Column("PURCHASENUMBERING")]
        public string PurchaseNumbering { get; set; }
       
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
        /// ���
        /// </summary>
        [Column("ISWAREHOUSING")]
        public int? IsWarehousing { get; set; }
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
        [Column("REVIEWMAN1")]
        public string ReviewMan1 { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN2")]
        public string ReviewMan2 { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN3")]
        public string ReviewMan3 { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWMAN4")]
        public string ReviewMan4 { get; set; }


        /// <summary>
        /// ReviewDescription
        /// </summary>
        /// <returns></returns>
        [Column("REVIEWDESCRIPTION")]
        public string ReviewDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        ///// <summary>
        /////��֯ID
        ///// </summary>
        ///// <returns></returns>
        //[Column("ORGANIZEID")]
        //public string OrganizeId { get; set; }

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