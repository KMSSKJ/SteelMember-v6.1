using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 10:42
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public class RawMaterialLibraryEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// �������
        /// </summary>		
        public string Category { get; set; }

        /// <summary>
        /// RawMaterialName
        /// </summary>
        /// <returns></returns>
        public string RawMaterialName { get; set; }
        /// <summary>
        /// RawMaterialStandard
        /// </summary>
        /// <returns></returns>
        public string RawMaterialStandard { get; set; }
        /// <summary>
        /// UnitId
        /// </summary>
        /// <returns></returns>
        public string Unit { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// RawMaterialModel
        /// </summary>
        /// <returns></returns>
        public string RawMaterialModel { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// DeleteFlag
        /// </summary>
        /// <returns></returns>
        public int? DeleteFlag { get; set; }
        /// <summary>
        /// Sort
        /// </summary>
        /// <returns></returns>
        public int? Sort { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.RawMaterialId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.RawMaterialId = keyValue;
        }
        #endregion
    }

}