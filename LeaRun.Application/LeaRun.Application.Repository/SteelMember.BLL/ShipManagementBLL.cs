
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
    public class ShipManagementBLL : ShipManagementIBLL
    {
        [Ninject.Inject]
        public ShipManagementIDAL CurrentDAL { get; set; }
        public ShipManagementBLL()
        {

        }
        public RMC_ShipManagement Add(RMC_ShipManagement model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.ShipId== id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_ShipManagement> Find(Expression<Func<RMC_ShipManagement, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_ShipManagement> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_ShipManagement, bool>> whereLambda, bool isAsc, Expression<Func<RMC_ShipManagement, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_ShipManagement> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_ShipManagement, bool>> whereLambda, Expression<Func<RMC_ShipManagement, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_ShipManagement, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_ShipManagement> SaveList(Stream file, RMC_ShipManagement model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_ShipManagement model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

