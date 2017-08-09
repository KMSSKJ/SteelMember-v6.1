using System;
using LeaRun.Application.Code;
using System.Collections.Generic;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-06 22:03
    /// �� ����ԭ���Ϸ���
    /// </summary>
    public class RawMaterialAnalysisEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// RawMaterialId
        /// </summary>
        /// <returns></returns>
        public string RawMaterialId { get; set; }
        /// <summary>
        /// RawMaterialDosage
        /// </summary>
        /// <returns></returns>
        public int RawMaterialDosage { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// IsSubmitReview
        /// </summary>
        public int IsSubmitReview { get; set; }
        /// <summary>
        /// IsPassed
        /// </summary>
        public int IsPassed { get; set; }
        /// <summary>
        /// Unit
        /// </summary>
        /// <returns></returns>
        public string Unit { get; set; }
        /// <summary>
        /// RawMaterialLibraryEntitys
        /// </summary>
        public RawMaterialLibraryEntity RawMaterialEntitys { get; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}