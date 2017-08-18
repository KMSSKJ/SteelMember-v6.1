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
    public class CollarBLL : CollarIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public CollarIDAL CurrentDAL { get; set; }
        public CollarBLL()
        {

        }
        public RMC_Collar Add(RMC_Collar model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.CollarId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_Collar> Find(Expression<Func<RMC_Collar, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_Collar> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_Collar, bool>> whereLambda, bool isAsc, Expression<Func<RMC_Collar, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_Collar> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_Collar, bool>> whereLambda, Expression<Func<RMC_Collar, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_Collar, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_Collar> SaveList(Stream file, RMC_Collar model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_Collar model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

