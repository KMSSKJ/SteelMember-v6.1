using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeaRun.Application.Repository.SteelMember.IDAL
{
    public interface BaseIDAL<T> : IQuery<T>, IAdd<T>, IRemove<T>, IRemove_str<T>, IModified<T> where T : class
    {
        int SaveChange();
        string SaveChange_str();
    }
    public interface IQuery<T> where T : class
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> whereLambda);
        IEnumerable<T> FindPaging<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderbyLambda, out int total);
        IEnumerable<T> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> whereLambda1, bool isAsc, Expression<Func<T, S>> orderbyLambda, out int total);
    }

    public interface IAdd<T> where T : class
    {
        T Add(T entity);
    }

    public interface IRemove<T> where T : class
    {
        int Remove(T entity);
    }

    public interface IRemove_str<T> where T : class
    {
        string Remove_str(T entity);
    }

    public interface IModified<T> where T : class
    {
        int Modified(T entity);
    }

    public interface ISaveList<T> where T : class
    {
        T SaveList(List<T> list, T model);
    }
}
