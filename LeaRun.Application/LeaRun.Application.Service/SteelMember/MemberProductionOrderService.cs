using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.IService.SteelMember;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.SteelMember
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-11 10:12
    /// �� ����������������
    /// </summary>
    public class MemberProductionOrderService : RepositoryFactory, MemberProductionOrderIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<MemberProductionOrderEntity> GetPageList(Pagination pagination,string queryJson)
        {
           var expression = LinqExtensions.True<MemberProductionOrderEntity>();
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
            else if(queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.CreateTime <= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {

                    //case "CategoryName":              //������
                    //    expression = expression.And(t => t.CategoryName.Contains(keyword));
                    //    break;
                    case "CreateMan":              //��������
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
            if (!queryParam["IsConfirm"].IsEmpty())
            {
                int IsConfirm = Convert.ToInt32(queryParam["IsConfirm"]);
                expression = expression.And(t => t.IsConfirm == IsConfirm);
            }
            if (!queryParam["ProductionStatus"].IsEmpty())
            {
                int ProductionStatus = Convert.ToInt32(queryParam["ProductionStatus"]);
                expression = expression.And(t => t.ProductionStatus == ProductionStatus);
            }
            if (!queryParam["IsPassed"].IsEmpty())
            {
                int IsPassed = Convert.ToInt32(queryParam["IsPassed"]);
                expression = expression.And(t =>t.IsPassed == IsPassed);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        ///// <summary>
        ///// ��ȡ�б�
        ///// </summary>
        ///// <param name="pagination">��ҳ</param>
        ///// <param name="queryJson">��ѯ����</param>
        ///// <returns>���ط�ҳ�б�</returns>
        //public IEnumerable<MemberProductionOrderEntity> GetPageList(Pagination pagination, string queryJson)
        //{
        //    return this.BaseRepository().FindList<MemberProductionOrderEntity>(pagination);
        //}

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>���ط�ҳ�б�</returns>
        public List<MemberProductionOrderEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<MemberProductionOrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    //case "Category":              //��������
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    //case "MemberName":              //��������
                    //    expression = expression.And(t => t..Contains(keyword));
                    //    break;
                    case "OrderNumbering":              //���
                        expression = expression.And(t => t.OrderNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            //expression = expression.And(t => t.OrderId==);
            return this.BaseRepository().IQueryable(expression).ToList();
            //return this.BaseRepository().FindList<MemberProductionOrderEntity>(pagination);
        }


        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>���ط�ҳ�б�</returns>
        public List<MemberProductionOrderEntity> GetList(Expression<Func<MemberProductionOrderEntity,bool>>condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
            
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public MemberProductionOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<MemberProductionOrderEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<MemberProductionOrderInfoEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<MemberProductionOrderInfoEntity>("select * from RMC_MemberProductionOrderInfo where OrderId='" + keyValue + "'");
        }

        /// <summary>
        /// ��ȡ�б�(������)
        /// <param name="pagination"></param>
        /// <param name="condition">��ѯ����</param>
        /// <returns></returns>
        /// </summary>
        public IEnumerable<MemberProductionOrderEntity> GetPageListByProductionOrderStatus(Pagination pagination, Expression<Func<MemberProductionOrderEntity,bool>> condition)
        {
            //var expression = LinqExtensions.True<MemberProductionOrderEntity>();
            //expression = expression.And(t => t.ProductionStatus == IsWarehousing);
            return this.BaseRepository().FindList(condition,pagination);
        }

        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<MemberProductionOrderEntity>(keyValue);
                db.Delete<MemberProductionOrderInfoEntity>(t => t.InfoId.Equals(keyValue));
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
        /// <param name="entryList"></param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MemberProductionOrderEntity entity, List<MemberProductionOrderInfoEntity> entryList)
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
                    db.Delete<MemberProductionOrderInfoEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (MemberProductionOrderInfoEntity item in entryList)
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
                    foreach (MemberProductionOrderInfoEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
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
            /// <param name="keyValue"></param>
            /// <param name="entity"></param>
            public void SaveForm(string keyValue, MemberProductionOrderEntity entity)
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
                //return entity.OrderId;
            }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void RemoveList(List<MemberProductionOrderEntity> list)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// ���¼���
        /// </summary>
        /// <param name="list"></param>
        public void UpdataList(List<MemberProductionOrderEntity> list)
        {
            this.BaseRepository().Update(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string keyValue)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="category"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Exist(string query, string category, string keyValue)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
   