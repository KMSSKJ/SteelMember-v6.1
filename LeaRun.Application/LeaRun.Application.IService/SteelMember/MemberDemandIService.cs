using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 16:54
    /// �� ������������
    /// </summary>
    public interface MemberDemandIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<MemberDemandEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<MemberDemandEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        MemberDemandEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, MemberDemandEntity entity);
        #endregion

        #region ��֤����
        /// <summary>
        /// �ֶβ����ظ�����ȫ����������֤��
        /// </summary>
        /// <param name="query">Ҫ��֤���ֶ�</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool Exist(string query, string keyValue);

        /// <summary>
        /// �ֶβ����ظ�����ȫ�������ﰴ������֤��
        /// </summary>
        /// <param name="query">Ҫ��֤���ֶ�</param>
        /// <param name="category">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool Exist(string query, string category, string keyValue);
        #endregion
    }
}
