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
    /// �� �ڣ�2017-07-26 17:19 
    /// �� �������ù��� RawMterialCollarEntity
    ///</summary>

    public class RawMterialCollarService : RepositoryFactory,RawMterialCollarIService
    {
        //RepositoryFactory<RawMterialCollarEntity>, RawMterialCollarIService
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<RawMterialCollarEntity> GetList(string queryJson)
        {
            //return this.BaseRepository().IQueryable().ToList();
            return this.BaseRepository().FindList<RawMterialCollarEntity>(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(string keyValue)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().FindEntity<RawMterialCollarEntity>(keyValue);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public RawMterialCollarEntity GetEntity(Expression<Func<RawMterialCollarEntity,bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().FindEntity<RawMterialCollarEntity>(condition);
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
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="entryList"></param>
        public void SaveForm(string keyValue, RawMterialCollarEntity entity, List<RawMterialCollarInfoEntity> entryList)
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
                    db.Delete<RawMterialCollarInfoEntity>(t => t.CollarId.Equals(keyValue));
                    foreach (RawMterialCollarInfoEntity item in entryList)
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
                    foreach (RawMterialCollarInfoEntity item in entryList)
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
        public void UpdataList(List<RawMterialCollarEntity> list)
        {
            this.BaseRepository().Update(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<RawMterialCollarEntity> OutInventoryDetailInfo(Pagination pagination, string queryJson)
        {
            throw new NotImplementedException();
            //if (queryJson != null)
            //{

            //    return this.BaseRepository().FindList<RawMterialCollarEntity>(p => p.CollarId == queryJson, pagination);
            //}
            //return this.BaseRepository().FindList<RawMterialCollarEntity>(pagination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<RawMterialCollarEntity> GetCallarList(Expression<Func<RawMterialCollarEntity, bool>> condition)
        {
            //throw new NotImplementedException();
            return this.BaseRepository().IQueryable(condition).ToList();
           // return this.BaseRepository().FindList<RawMterialCollarEntity>(condition);
        }
        /// <summary>
        /// ��ҳ��ѯ������Ϣ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public List<RawMterialCollarEntity> GetPageList(Pagination pagination, string queryJson)
        {
           
            var expression = LinqExtensions.True<RawMterialCollarEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            var BeginTime = queryParam["BeginTime"].ToDate();
            var EndTime = queryParam["EndTime"].ToDate();
            if (!queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date>= BeginTime);
                expression = expression.And(t => t.Date<= EndTime);
            }
            else if (!queryParam["BeginTime"].IsEmpty() && queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date>= BeginTime);
            }
            else if (queryParam["BeginTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                expression = expression.And(t => t.Date<= EndTime);
            }

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    //case "Category":              //��������
                    //    expression = expression.And(t => t.Category.Contains(keyword));
                    //    break;
                    //case "CollarEngineering":             
                    //    expression = expression.And(t => t.CollarEngineering.Contains(keyword));
                    //    break;
                    case "CollarNumbering":              //�ͺ�
                        expression = expression.And(t => t.CollarNumbering.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["SubProjectId"].IsEmpty())
            {
                var SubProjectId = queryParam["SubProjectId"].ToString();
                expression = expression.And(t => t.CollarEngineering == SubProjectId);
            }
            expression = expression.And(t => t.CollarNumbering !=null&& t.CollarNumbering != "");
            return this.BaseRepository().FindList(expression, pagination).ToList();
        }

        #endregion
    }
}
