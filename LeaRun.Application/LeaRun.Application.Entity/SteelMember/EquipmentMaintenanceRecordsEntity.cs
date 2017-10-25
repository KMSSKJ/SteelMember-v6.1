using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-09-27 09:17
    /// �� ���������豸
    /// </summary>
    public class EquipmentMaintenanceRecordsEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// InfoId
        /// </summary>
        /// <returns></returns>
        [Column("INFOID")]
        public string InfoId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// ά����Ŀ
        /// </summary>
        /// <returns></returns>
        [Column("MAINTENANCECONTENT")]
        public string MaintenanceContent { get; set; }
        /// <summary>
        /// ����ԭ��
        /// </summary>
        /// <returns></returns>
        [Column("MALFUNCTIONREASON")]
        public string MalfunctionReason { get; set; }
        /// <summary>
        /// ά��ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("MAINTENANCEDATE")]
        public DateTime? MaintenanceDate { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [Column("SOLVEMETHOD")]
        public string SolveMethod { get; set; }
        /// <summary>
        /// ά����
        /// </summary>
        /// <returns></returns>
        [Column("MAINTENANCEPEOPLE")]
        public string MaintenancePeople { get; set; }
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