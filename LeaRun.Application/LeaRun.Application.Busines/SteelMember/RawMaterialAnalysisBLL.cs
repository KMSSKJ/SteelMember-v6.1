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
    /// �� �ڣ�2017-07-06 22:03
    /// �� �������Ϸ���
    /// </summary>
    public class RawMaterialAnalysisBLL
    {
        private RawMaterialAnalysisIService service = new RawMaterialAnalysisService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public List<RawMaterialAnalysisEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public List<RawMaterialAnalysisEntity> GetPageList1(Expression<Func<RawMaterialAnalysisEntity, bool>> condition,Pagination pagination)
        {
            return service.GetPageList1(condition, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>�����б�</returns>
        public List<RawMaterialAnalysisEntity> GetList(Expression<Func<RawMaterialAnalysisEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialAnalysisEntity GetEntity(string keyValue)
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
        public void RemoveList(List<RawMaterialAnalysisEntity> list)
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
        public void SaveForm(string keyValue, RawMaterialAnalysisEntity entity)
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
        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<RawMaterialAnalysisEntity> list)
        {
            try
            {
                service.UpdataList(list);
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
