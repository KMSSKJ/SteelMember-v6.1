//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.Application.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaRun.Application.Web.Areas.SteelMember.Models
{
    /// <summary>
    /// ������¼
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.27 12:04</date>
    /// </author>
    /// </summary>
    [Description("������¼")]
    //[PrimaryKey("POOrderEntryId")]
    public class OrderModel : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ������¼����
        /// </summary>
        /// <returns></returns>
        [DisplayName("������¼����")]
        public int OrderMemberId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string OrderId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string MemberId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string ProjectDemandId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���")]
        public string OrderNumbering { get; set; }
        /// <summary>
        /// ���ϴ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������")]
        public string MemberNumbering { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string MemberName { get; set; }
        /// <summary>
        /// �����ͺ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����ͺ�")]
        public string MemberModel { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string MemberUnit { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string Qty { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string Price { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("���")]
        public string PriceAmount { get; set; }
        /// <summary>
        /// ��˰����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˰����")]
        public string PlusPrice { get; set; }
        /// <summary>
        /// ��˰���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˰���")]
        public string PlusPriceAmount { get; set; }
        /// <summary>
        /// ˰��(%)
        /// </summary>
        /// <returns></returns>
        [DisplayName("˰��(%)")]
        public string CESS { get; set; }
        /// <summary>
        /// ˰��
        /// </summary>
        /// <returns></returns>
        [DisplayName("˰��")]
        public string CESSAmount { get; set; }
        /// <summary>
        /// ˵��
        /// </summary>
        /// <returns></returns>
        [DisplayName("˵��")]
        public string Description { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ�����")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�")]
        public string ModifyUserName { get; set; }
        #endregion

        //#region ��չ����
        ///// <summary>
        ///// ��������
        ///// </summary>
        //public override void Create()
        //{
        //    this.POOrderEntryId = CommonHelper.GetGuid;
        //    this.CreateDate = DateTime.Now;
        //    this.CreateUserId = ManageProvider.Provider.Current().UserId;
        //    this.CreateUserName =OperatorProvider.Provider.Current().UserName;
        //}
        ///// <summary>
        ///// �༭����
        ///// </summary>
        ///// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.POOrderEntryId = KeyValue;
        //    this.ModifyDate = DateTime.Now;
        //    this.ModifyUserId = ManageProvider.Provider.Current().UserId;
        //    this.ModifyUserName =OperatorProvider.Provider.Current().UserName;
        //}
        //#endregion
    }
}