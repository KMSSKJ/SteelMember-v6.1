using LeaRun.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeaRun.Application.Repository.SteelMember.DAL
{
    public class BaseDAL<T> where T : class
    {
        public LeaRunFramework_Base_2016Entities dbContext = new LeaRunFramework_Base_2016Entities();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda">Lambda条件</param>
        /// <returns></returns>           
        public IEnumerable<T> Find(Expression<Func<T, bool>> whereLambda)
        {
            IEnumerable<T> result = dbContext.Set<T>().Where(whereLambda);//db.RDC_BrokenChainage.Where(p => p.RouteID == 142).ToList();
            return result;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public T Add(T entity)
        {
            T model = dbContext.Set<T>().Add(entity);
            SaveChange();
            return model; //返回的model中带有ID
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>0
        public int Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return 0;
        }

        public string Remove_str(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return "0";
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        public int Modified(T entity)
        {
            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return SaveChange();
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChange()
        {
            return dbContext.SaveChanges();
        }
        public string SaveChange_str()
        {
            return dbContext.SaveChanges().ToString();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="total">查询总数</param>
        /// <returns></returns>
        public IEnumerable<T> FindPaging<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderbyLambda, out int total)
        {
            var tempData = dbContext.Set<T>().Where<T>(whereLambda);

            total = tempData.Count();

            if (isAsc)
            {
                return tempData.OrderBy(orderbyLambda)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize);
            }
            else
            {
                return tempData.OrderByDescending(orderbyLambda)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize);
            }
        }

        /// <summary>
        ///筛选分页查询（两个集合）
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereLambda">查询条件</para
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderbyLambda">排序条件</pa
        /// <param name="total">查询总数</param>
        /// <returns></returns>
        public IEnumerable<T> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> whereLambda1, bool isAsc, Expression<Func<T, S>> orderbyLambda, out int total)
        {
            var tempData = dbContext.Set<T>().Where<T>(whereLambda);
            var tempData1 = dbContext.Set<T>().Where<T>(whereLambda1);
            IQueryable<T> RTS = tempData.Concat(tempData1);//两个集合相加（合并）
            var temp = RTS.Distinct();//去除重复项
            total = temp.Count();

            if (isAsc)
            {
                return temp.OrderByDescending(orderbyLambda)
                              .Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize);
            }
            else
            {
                return temp.OrderBy(orderbyLambda)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize);
            }
        }
    }
}
