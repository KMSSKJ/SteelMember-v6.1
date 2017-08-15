using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeaRun.Application.Repository.SteelMember.IBLL
{
    public interface BaseIBLL<T> where T : class
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> whereLambda);
        IEnumerable<T> FindPage<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderbyLambda, out int total);
        T Add(T model);
        int Modified(T model);
        int Remove(List<int> delbyid);
        string Remove_str(List<string> delbyid);
        List<T> SaveList(Stream file, T model);
    }
}
