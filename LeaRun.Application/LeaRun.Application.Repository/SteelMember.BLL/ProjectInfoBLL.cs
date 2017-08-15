using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LeaRun.Application.Repository.SteelMember.IDAL;

namespace LeaRun.Application.Repository.SteelMember.BLL
{
    public class ProjectInfoBLL : ProjectInfoIBLL
    {
        [Ninject.Inject]
        public ProjectInfoIDAL CurrentDAL { get; set; }
        public ProjectInfoBLL()
        {

        }
        public RMC_ProjectInfo Add(RMC_ProjectInfo model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.ProjectId == id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_ProjectInfo> Find(Expression<Func<RMC_ProjectInfo, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }

        public IEnumerable<RMC_ProjectInfo> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectInfo, bool>> whereLambda, bool isAsc, Expression<Func<RMC_ProjectInfo, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_ProjectInfo> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_ProjectInfo, bool>> whereLambda, Expression<Func<RMC_ProjectInfo, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_ProjectInfo, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_ProjectInfo> SaveList(Stream file, RMC_ProjectInfo model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_ProjectInfo model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}
