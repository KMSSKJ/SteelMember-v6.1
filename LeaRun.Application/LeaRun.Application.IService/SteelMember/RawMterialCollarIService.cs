using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 17:19
    /// �� �������ù���
    /// </summary>
    public interface RawMterialCollarIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<RawMterialCollarEntity> GetList(string queryJson);

        List<RawMterialCollarEntity> GetCallarList(Expression<System.Func<RawMterialCollarEntity, bool>> condition);
        /// <summary>
        /// ��ҳ��ѯ������Ϣ
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson);

        /// <summary>
        /// ��ҳ��ѯ������Ϣ
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        List<RawMterialCollarEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        RawMterialCollarEntity GetEntity(string keyValue);

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        RawMterialCollarEntity GetEntity(Expression<Func<RawMterialCollarEntity,bool>>condition);
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
        void SaveForm(string keyValue, RawMterialCollarEntity entity);

        void SaveForm(string keyValue, RawMterialCollarEntity entity, List<RawMterialCollarInfoEntity> entryList);
        void UpdataList(List<RawMterialCollarEntity> list);
        #endregion
    }
}
