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
    public class PurchaseBLL : PurchaseIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public PurchaseIDAL CurrentDAL { get; set; }
        public PurchaseBLL()
        {

        }
        public RMC_Purchase Add(RMC_Purchase model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.PurchaseId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_Purchase> Find(Expression<Func<RMC_Purchase, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_Purchase> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_Purchase, bool>> whereLambda, bool isAsc, Expression<Func<RMC_Purchase, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_Purchase> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_Purchase, bool>> whereLambda, Expression<Func<RMC_Purchase, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_Purchase, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_Purchase> SaveList(Stream file, RMC_Purchase model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_Purchase model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

