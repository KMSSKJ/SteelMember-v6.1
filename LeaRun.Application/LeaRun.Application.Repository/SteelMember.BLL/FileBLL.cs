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
    public class FileBLL : FileIBLL
    {
        [Ninject.Inject]
        public FileIDAL CurrentDAL { get; set; }
        public FileBLL()
        {

        }
        public RMC_MemberLibrary Add(RMC_MemberLibrary model)
        {
            return CurrentDAL.Add(model);
        }
        public int Remove(List<int> delbyid)
        {
            throw new NotImplementedException();
        }
        public string Remove_str(List<string> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.MemberId == id).Single());
            });

            return CurrentDAL.SaveChange().ToString();
        }

        public IEnumerable<RMC_MemberLibrary> Find(Expression<Func<RMC_MemberLibrary, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_MemberLibrary> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberLibrary, bool>> whereLambda, bool isAsc, Expression<Func<RMC_MemberLibrary, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_MemberLibrary> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberLibrary, bool>> whereLambda, Expression<Func<RMC_MemberLibrary, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_MemberLibrary, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_MemberLibrary> SaveList(Stream file, RMC_MemberLibrary model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_MemberLibrary model)
        {
            return CurrentDAL.Modified(model);
        }
    }
}

