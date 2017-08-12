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
    public class MemberProductionOrderEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("ORDERNUMBERING")]
        public string OrderNumbering { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("CATEGORY")]
        public string Category { get; set; }
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
        /// ���ȼ�
        /// </summary>
        /// <returns></returns>
        [Column("PRIORITY")]
        public int? Priority { get; set; }
        /// <summary>
        /// �Ƿ�ר��
        /// </summary>
        /// <returns></returns>
        [Column("ISDDDICATED")]
        public int? IsDddicated { get; set; }
        /// <summary>
        /// �ύ״̬
        /// </summary>
        /// <returns></returns>
        [Column("ISSUBMIT")]
        public int? IsSubmit { get; set; }
        /// <summary>
        /// ���״̬
        /// </summary>
        /// <returns></returns>
        [Column("ISPASSED")]
        public int? IsPassed { get; set; }
        /// <summary>
        /// ��ȡ״̬
        /// </summary>
        /// <returns></returns>
        [Column("ISRECEIVE")]
        public int? IsReceive { get; set; }
        /// <summary>
        /// ������ȡ״̬
        /// </summary>
        /// <returns></returns>
        [Column("ISRECEIVERAWMATERIAL")]
        public int? IsReceiveRawMaterial { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        /// <returns></returns>
        [Column("ORDERSTATUS")]
        public int? OrderStatus { get; set; }
        /// <summary>
        /// ProductionStatus
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONSTATUS")]
        public int? ProductionStatus { get; set; }
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
            this.OrderId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
                                            }
        #endregion
    }
}