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
    /// �� �ڣ�2017-07-19 10:03
    /// �� �������Ͽ��
    /// </summary>
    public class RawMaterialInventoryBLL
    {
        private RawMaterialInventoryIService service = new RawMaterialInventoryService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public List<RawMaterialInventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialInventoryEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntity(Expression<Func<RawMaterialInventoryEntity,bool>> condition)
        {
            return service.GetEntity(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        ///  <param name="rawMaterialId">���ֵ</param>
        /// <returns></returns>
        public RawMaterialInventoryEntity GetEntityByRawMaterialId(string rawMaterialId)
        {
            return service.GetEntityByRawMaterialId(rawMaterialId);
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
        /// ����������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialInventoryEntity entity)
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
    }
}