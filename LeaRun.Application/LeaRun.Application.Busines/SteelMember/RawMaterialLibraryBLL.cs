using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Application.Service.SteelMember;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace LeaRun.Application.Busines.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-06 10:42
    /// �� ����ԭ���Ϲ���
    /// </summary>
    public class RawMaterialLibraryBLL
    {
        private RawMaterialLibraryIService service = new RawMaterialLibraryService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        public List<RawMaterialLibraryEntity> GetList(Expression<Func<RawMaterialLibraryEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialLibraryEntity> GetList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ģ����ѯ(Category)
        /// </summary>
        /// <param name="category">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialLibraryEntity> GetPageListByLikeCategory(Pagination pagination, string category)
        {
            return service.GetPageListByLikeCategory(pagination, category);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialLibraryEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ɾ������(����)
        /// </summary>
        public void RemoveList(List<RawMaterialLibraryEntity>list)
        {
            try
            {
                service.RemoveList(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialLibraryEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// ���Ʋ����ظ�
        /// </summary>
        public bool ExistFullName(string FullName, string category, string keyValue = "")
        {
            return service.Exist(FullName, category, keyValue);
        }
        #endregion
    }
}
