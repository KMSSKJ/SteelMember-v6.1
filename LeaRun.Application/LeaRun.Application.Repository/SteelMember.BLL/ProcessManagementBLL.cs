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
    public class ProcessManagementBLL : ProcessManagementIBLL
    {
        [Ninject.Inject]
        public ProcessManagementIDAL CurrentDAL { get; set; }
        public ProcessManagementBLL()
        {

        }
        public RMC_ProcessManagement Add(RMC_ProcessManagement model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.ProcessId == id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_ProcessManagement> Find(Expression<Func<RMC_ProcessManagement, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_ProcessManagement> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProcessManagement, bool>> whereLambda, bool isAsc, Expression<Func<RMC_ProcessManagement, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_ProcessManagement> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProcessManagement, bool>> whereLambda, Expression<Func<RMC_ProcessManagement, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_ProcessManagement, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_ProcessManagement> SaveList(Stream file, RMC_ProcessManagement model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_ProcessManagement model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

