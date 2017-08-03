using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-26 17:19  RepositoryFactory<RawMterialCollarEntity>
    /// �� �������ù���
    ///</summary>
    
    public class RawMterialCollarService : RepositoryFactory, RawMterialCollarIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMterialCollarEntity> GetList(string queryJson)
        {
            //return this.BaseRepository().IQueryable().ToList();
            //return this.BaseRepository().FindEntity<RawMterialCollarEntity>(queryJson);
            throw new NotImplementedException();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<RawMterialCollarEntity>(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// <param name="keyValue">����</param>
        /// </summary>


        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMterialCollarEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson)
        {
            //throw new NotImplementedException();
            if (queryJson != null)
            {
                return this.BaseRepository().FindList<RawMterialCollarEntity>(p => p.CollarId == queryJson, pagination);
            }
            return this.BaseRepository().FindList<RawMterialCollarEntity>(pagination);
        }
        #endregion
    }
}
