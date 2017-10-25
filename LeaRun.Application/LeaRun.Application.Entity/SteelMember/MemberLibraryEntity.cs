using System;
using LeaRun.Application.Code;
using System.Collections.Generic;
using LeaRun.Application.Entity.SystemManage;

namespace LeaRun.Application.Entity.SteelMember
{
    /// <summary>
    /// �� ��
    /// �� �ڣ�2017-07-05 17:15
    /// �� �������������
    /// </summary>
    public class MemberLibraryEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// MemberId
        /// </summary>
        /// <returns></returns>
        public string MemberId { get; set; }
        /// <summary>
        /// MarkId
        /// </summary>
        /// <returns></returns>
        public int? MarkId { get; set; }
        /// <summary>
        /// SubProjectId
        /// </summary>
        /// <returns></returns>
        public string EngineeringId { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// MemberName
        /// </summary>
        /// <returns></returns>
        public string MemberName { get; set; }
        /// <summary>
        /// MemberNumbering
        /// </summary>
        /// <returns></returns>
        public string MemberNumbering { get; set; }
        /// <summary>
        /// ������λ
        /// </summary>
        public string UnitId { get; set; }
        //CONSTRAINT[FK_RMC_MemberLibrary_Base_DataItemDetail_UnitId] FOREIGN KEY([UnitId]) REFERENCES[Base_DataItemDetail] ([ItemDetailId])
     
        /// <summary>
        /// UnitPrice
        /// </summary>
        /// <returns></returns>
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// CAD_Drawing
        /// </summary>
        /// <returns></returns>
        public string CAD_Drawing { get; set; }
        /// <summary>
        /// Model_Drawing
        /// </summary>
        /// <returns></returns>
        public string Model_Drawing { get; set; }
        /// <summary>
        /// IsRawMaterial
        /// </summary>
        /// <returns></returns>
        public int? IsRawMaterial { get; set; }
        /// <summary>
        /// IsProcess
        /// </summary>
        /// <returns></returns>
        public int? IsProcess { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        /// <returns></returns>
        public string Icon { get; set; }
        /// <summary>
        /// ModifiedTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifiedTime { get; set; }
        /// <summary>
        /// IsReview
        /// </summary>
        /// <returns></returns>
        public int? IsReview { get; set; }
        /// <summary>
        /// UploadTime
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// FullPath
        /// </summary>
        /// <returns></returns>
        public string FullPath { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public decimal? MemberWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.MemberId = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MemberId = keyValue;
                                            }
        #endregion
    }
}