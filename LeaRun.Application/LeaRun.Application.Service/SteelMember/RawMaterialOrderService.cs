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
    /// �� �ڣ�2017-08-07 17:28
    /// �� �������϶���
    /// </summary>
    public class RawMaterialOrderService : RepositoryFactory, RawMaterialOrderIService
    {

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<RawMaterialOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialOrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime >= BeginTime);
                expression = expression.And(t => t.CreateTime <= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime >= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    case "Category":              //����
                        expression = expression.And(t => t.Category.Contains(keyword));
                        break;
                    case "MemberName":              //��������
                        expression = expression.And(t => t.CreateMan.Contains(keyword));
                        break;
                    case "OrderNumbering":              //���
                        expression = expression.And(t => t.OrderNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.Category == SubProjectId);
            }
            if (!queryParam["IsPassed"].IsEmpty())
            {
                var IsPassed = queryParam["IsPassed"].ToInt();
                expression = expression.And(t => t.IsPassed == IsPassed);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMaterialOrderEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<RawMaterialOrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "OrderNumbering":              //���
                        expression = expression.And(t => t.OrderNumbering.Contains(keyword));
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
        public IEnumerable<RawMaterialOrderEntity> GetList(Expression<Func<RawMaterialOrderEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }
       
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMaterialOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<RawMaterialOrderEntity>(keyValue);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public RawMaterialOrderEntity GetEntity(Expression<Func<RawMaterialOrderEntity, bool>>condition)
        {
            return this.BaseRepository().FindEntity<RawMaterialOrderEntity>(condition);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            // this.BaseRepository().Delete(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<RawMaterialOrderEntity>(keyValue);
                db.Delete<RawMaterialOrderInfoEntity>(t => t.InfoId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RawMaterialOrderEntity entity)
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
        public void SaveForm(string keyValue, RawMaterialOrderEntity entity, List<RawMaterialOrderInfoEntity> entryList)
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
                    db.Delete<RawMaterialOrderInfoEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (RawMaterialOrderInfoEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
                        db.Insert(item);
                    }
                }
                else
                {
                    //����
                    entity.Create();
                    db.Insert(entity);
                    //��ϸ
                    foreach (RawMaterialOrderInfoEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
                        item.Time = entity.CreateTime;
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
        public void UpdataList(List<RawMaterialOrderEntity> list)
        {
            this.BaseRepository().Update(list);
        }



        #endregion
    }
}
