using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 10:42
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public interface RawMaterialLibraryIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        IEnumerable<RawMaterialLibraryEntity> GetList();
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<RawMaterialLibraryEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        RawMaterialLibraryEntity GetEntity(string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, RawMaterialLibraryEntity entity);
        #endregion

        #region ��֤����
        /// <summary>
        /// ���Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����ֵ</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool ExistFullName(string FullName,string category, string keyValue);
        #endregion
    }
}
