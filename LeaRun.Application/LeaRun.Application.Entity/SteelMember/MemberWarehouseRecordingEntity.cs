using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-05 17:15
    /// �� �������������
    /// </summary>
    public class MemberWarehouseRecordingEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// RecordingId
        /// </summary>
        /// <returns></returns>
        public string RecordingId { get; set; }

        /// <summary>
        /// MemberWarehouseId
        /// </summary>
        /// <returns></returns>
        public string MemberWarehouseId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        /// <returns></returns>
        public int? InStock { get; set; }
        /// <summary>
        /// SubProject
        /// </summary>
        /// <returns></returns>
        public string SubProject { get; set; }
        /// <summary>
        /// Model_Drawing
        /// </summary>
        /// <returns></returns>
        public string ToReportPeople { get; set; }
        /// <summary>
        /// IsRawMaterial
        /// </summary>
        /// <returns></returns>
        public string CollarDepartment { get; set; }
        /// <summary>
        /// IsProcess
        /// </summary>
        /// <returns></returns>
        public string Receiver { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        /// <returns></returns>
        public string ReceiverTel { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// FullPath
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// Librarian
        /// </summary>
        public string Librarian { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.RecordingId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.RecordingId = keyValue;
                                            }
        #endregion
    }
}