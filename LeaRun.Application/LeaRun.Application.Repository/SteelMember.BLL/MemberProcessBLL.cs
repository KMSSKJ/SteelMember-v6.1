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
    public class MemberProcessBLL : MemberProcessIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public MemberProcessIDAL CurrentDAL { get; set; }
        public MemberProcessBLL()
        {

        }
        public RMC_MemberProcess Add(RMC_MemberProcess model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.MemberProcessId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_MemberProcess> Find(Expression<Func<RMC_MemberProcess, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_MemberProcess> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberProcess, bool>> whereLambda, bool isAsc, Expression<Func<RMC_MemberProcess, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_MemberProcess> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberProcess, bool>> whereLambda, Expression<Func<RMC_MemberProcess, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_MemberProcess, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_MemberProcess> SaveList(Stream file, RMC_MemberProcess model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_MemberProcess model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

