
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
    public class RawMaterialPurchaseBLL : RawMaterialPurchaseIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public RawMaterialPurchaseIDAL CurrentDAL { get; set; }
        public RawMaterialPurchaseBLL()
        {

        }
        public RMC_RawMaterialPurchase Add(RMC_RawMaterialPurchase model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.RawMaterialPurchaseId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_RawMaterialPurchase> Find(Expression<Func<RMC_RawMaterialPurchase, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_RawMaterialPurchase> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_RawMaterialPurchase, bool>> whereLambda, bool isAsc, Expression<Func<RMC_RawMaterialPurchase, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_RawMaterialPurchase> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_RawMaterialPurchase, bool>> whereLambda, Expression<Func<RMC_RawMaterialPurchase, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_RawMaterialPurchase, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_RawMaterialPurchase> SaveList(Stream file, RMC_RawMaterialPurchase model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_RawMaterialPurchase model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

