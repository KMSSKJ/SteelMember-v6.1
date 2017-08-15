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
    public class ProjectWarehouseBLL : ProjectWarehouseIBLL
    {
        [Ninject.Inject]
        public ProjectWarehouseIDAL CurrentDAL { get; set; }
        public ProjectWarehouseBLL()
        {

        }
        public RMC_ProjectWarehouse Add(RMC_ProjectWarehouse model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.ProjectWarehouseId == id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_ProjectWarehouse> Find(Expression<Func<RMC_ProjectWarehouse, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_ProjectWarehouse> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectWarehouse, bool>> whereLambda, bool isAsc, Expression<Func<RMC_ProjectWarehouse, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_ProjectWarehouse> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectWarehouse, bool>> whereLambda, Expression<Func<RMC_ProjectWarehouse, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_ProjectWarehouse, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_ProjectWarehouse> SaveList(Stream file, RMC_ProjectWarehouse model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_ProjectWarehouse model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

