using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-09-13 22:58
    /// �� ������������
    /// </summary>
    public class MemberCollarService : RepositoryFactory, MemberCollarIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<MemberCollarEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MemberCollarEntity>();
            var queryParam = queryJson.ToJObject();

            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date >= BeginTime);
                expression = expression.And(t => t.Date <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date <= EndTime);
            }

            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "CollarNumbering":
                        expression = expression.And(t => t.CollarNumbering.Contains(keyword));
                        break;
                    //case "CollarEngineering":
                    //    expression = expression.And(t => t.CollarEngineering.Contains(keyword));
                    //    break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<MemberCollarEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberCollarEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "CollarNumbering":              //CollarNumbering
                        expression = expression.And(t => t.CollarNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<MemberCollarEntity> GetList(Expression<Func<MemberCollarEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public MemberCollarEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<MemberCollarEntity>(keyValue);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="condition">����ֵ</param>
        /// <returns></returns>
        public MemberCollarEntity GetEntity(Expression<Func<MemberCollarEntity,bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
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
        public void SaveForm(string keyValue, MemberCollarEntity entity)
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
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="entryList"></param>
        public void SaveForm(string keyValue, MemberCollarEntity entity, List<MemberCollarInfoEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //��ϸ
                    db.Delete<MemberCollarInfoEntity>(t => t.CollarId.Equals(keyValue));
                    foreach (MemberCollarInfoEntity item in entryList)
                    {
                        item.Create();
                        item.CollarId = entity.CollarId;
                        db.Insert(item);
                    }
                }
                else
                {
                    //����
                    entity.Create();
                    db.Insert(entity);
                    //��ϸ
                    foreach (MemberCollarInfoEntity item in entryList)
                    {
                        item.Create();
                        item.CollarId = entity.CollarId;
                        db.Insert(item);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<MemberCollarEntity> list)
        {
            this.BaseRepository().Update(list);
        }
        #endregion
    }
}
