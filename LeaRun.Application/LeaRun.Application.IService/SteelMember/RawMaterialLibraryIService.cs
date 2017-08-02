using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 10:42
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public interface RawMaterialLibraryIService:IBaseService<RawMaterialLibraryEntity>
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        List<RawMaterialLibraryEntity> GetList(Expression<Func<RawMaterialLibraryEntity, bool>> condition);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<RawMaterialLibraryEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ģ����ѯ(Category)
        /// </summary>
        /// <param name="category">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<RawMaterialLibraryEntity> GetPageListByLikeCategory(Pagination pagination, string category);
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
    }
}
