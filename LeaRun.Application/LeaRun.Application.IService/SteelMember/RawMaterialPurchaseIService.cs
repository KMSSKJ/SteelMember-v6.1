using LeaRun.Application.Entity.SteelMember;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LeaRun.Application.IService.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-08 11:58
    /// �� ����ԭ���ϲɹ�����
    /// </summary>
    public interface RawMaterialPurchaseIService: IService.IBaseService<RawMaterialPurchaseEntity>
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<RawMaterialPurchaseEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        RawMaterialPurchaseEntity GetEntity(string keyValue);
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        IEnumerable<RawMaterialPurchaseInfoEntity> GetDetails(string keyValue);
        List<RawMaterialPurchaseInfoEntity> GetList(Expression<System.Func<RawMaterialPurchaseInfoEntity, bool>> condition);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        

        void SaveForm(string keyValue, RawMaterialPurchaseEntity entity,List<RawMaterialPurchaseInfoEntity> entryList);
        #endregion
    }
}
