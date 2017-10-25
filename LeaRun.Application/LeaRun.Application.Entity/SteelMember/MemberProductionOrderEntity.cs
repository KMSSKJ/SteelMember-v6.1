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
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("ORGANIZEID")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("DEPARTMENTID")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("SHIPPINGADDRESS")]
        public string ShippingAddress { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("CONTACTPERSON")]
        public string ContactPerson { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("CONTACTPERSONTEL")]
        public string ContactPersonTel { get; set; }
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
        [Column("ISDEDICATED")]
        public int? IsDedicated { get; set; }
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
        [Column("ISCONFIRM")]
        public int? IsConfirm { get; set; }
        /// <summary>
        /// ������ȡ״̬
        /// </summary>
        /// <returns></returns>
        [Column("ISRECEIVERAWMATERIAL")]
        public int? IsReceiveRawMaterial { get; set; }
        /// <summary>
        /// ProductionStatus
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTIONSTATUS")]
        public int? ProductionStatus { get; set; }
        /// <summary>
        /// ProductionStatus
        /// </summary>
        /// <returns></returns>
        [Column("SELFDETECTSTATUS")]
        public int? SelfDetectStatus { get; set; }
        /// <summary>
        /// ProductionStatus
        /// </summary>
        /// <returns></returns>
        [Column("QUALITYINSPECTIONSTATUS")]
        public int? QualityInspectionStatus { get; set; }

        /// <summary>
        /// ORDERWAREHOUSINGSTATUS
        /// </summary>
        /// <returns></returns>
        [Column("ORDERWAREHOUSINGSTATUS")]
        public int? OrderWarehousingStatus  { get; set; }
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
        /// Ԥ�����ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("ESTIMATEDFINISHTIME")]
        public DateTime? EstimatedFinishTime { get; set; }
       
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("ISPACKAGE")]
        public int IsPackage { get; set; }
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