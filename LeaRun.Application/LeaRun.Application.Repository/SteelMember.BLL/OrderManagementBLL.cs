using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Application.Repository.SteelMember.IDAL;
using LeaRun.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeaRun.Application.Repository.SteelMember.BLL
{
    public class OrderManagementBLL : OrderManagementIBLL
    {
        [Ninject.Inject]
        public OrderManagementIDAL CurrentDAL { get; set; }
        public OrderManagementBLL()
        {

        }
        public RMC_ProjectOrder Add(RMC_ProjectOrder model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.OrderId == id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_ProjectOrder> Find(Expression<Func<RMC_ProjectOrder, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_ProjectOrder> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectOrder, bool>> whereLambda, bool isAsc, Expression<Func<RMC_ProjectOrder, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_ProjectOrder> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectOrder, bool>> whereLambda, Expression<Func<RMC_ProjectOrder, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_ProjectOrder, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_ProjectOrder> SaveList(Stream file, RMC_ProjectOrder model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_ProjectOrder model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

