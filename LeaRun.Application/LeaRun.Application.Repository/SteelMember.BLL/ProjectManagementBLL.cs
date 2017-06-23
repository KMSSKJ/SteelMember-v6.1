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
    public class ProjectManagementBLL : ProjectManagementIBLL
    {
        [Ninject.Inject]
        public ProjectManagementIDAL CurrentDAL { get; set; }
        public ProjectManagementBLL()
        {

        }
        public RMC_ProjectDemand Add(RMC_ProjectDemand model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.ProjectDemandId== id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_ProjectDemand> Find(Expression<Func<RMC_ProjectDemand, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_ProjectDemand> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectDemand, bool>> whereLambda, bool isAsc, Expression<Func<RMC_ProjectDemand, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_ProjectDemand> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectDemand, bool>> whereLambda, Expression<Func<RMC_ProjectDemand, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_ProjectDemand, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_ProjectDemand> SaveList(Stream file, RMC_ProjectDemand model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_ProjectDemand model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

