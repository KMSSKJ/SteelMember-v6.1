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
    public class FactoryWarehouseBLL : FactoryWarehouseIBLL
    {
        [ Ninject.Inject]
        public FactoryWarehouseIDAL CurrentDAL { get; set; }
        public FactoryWarehouseBLL()
        {

        }
        public RMC_FactoryWarehouse Add(RMC_FactoryWarehouse model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.FactoryWarehouseId== id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_FactoryWarehouse> Find(Expression<Func<RMC_FactoryWarehouse, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_FactoryWarehouse> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_FactoryWarehouse, bool>> whereLambda, bool isAsc, Expression<Func<RMC_FactoryWarehouse, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_FactoryWarehouse> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_FactoryWarehouse, bool>> whereLambda, Expression<Func<RMC_FactoryWarehouse, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_FactoryWarehouse, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_FactoryWarehouse> SaveList(Stream file, RMC_FactoryWarehouse model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_FactoryWarehouse model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

