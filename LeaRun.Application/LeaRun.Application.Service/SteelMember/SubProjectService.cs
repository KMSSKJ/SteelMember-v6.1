using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System;
using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-06-30 22:01
    /// �� �����ӹ�����Ϣ
    /// </summary>
    public class SubProjectService : RepositoryFactory<SubProjectEntity>, SubProjectIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<SubProjectEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<SubProjectEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "FullName":              //��������
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public SubProjectEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// ���Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string FullName, string keyValue)
        {
            var expression = LinqExtensions.True<SubProjectEntity>();
            expression = expression.And(t => t.FullName == FullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id == keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        public void SaveForm(string keyValue, SubProjectEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                if (entity.ParentId == null)
                {
                    entity.ParentId = "0";
                    entity.Levels = 1;
                }
                else
                {
                    var level = BaseRepository().IQueryable(s => s.Id == entity.ParentId).ToList()[0].Levels;
                    entity.Levels = level+1;
                }

                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
