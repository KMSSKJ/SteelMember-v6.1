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
    public class CollarMemberBLL : CollarMemberIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public CollarMemberIDAL CurrentDAL { get; set; }
        public CollarMemberBLL()
        {

        }
        public RMC_CollarMember Add(RMC_CollarMember model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.CollarMemberId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_CollarMember> Find(Expression<Func<RMC_CollarMember, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_CollarMember> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_CollarMember, bool>> whereLambda, bool isAsc, Expression<Func<RMC_CollarMember, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_CollarMember> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_CollarMember, bool>> whereLambda, Expression<Func<RMC_CollarMember, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_CollarMember, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_CollarMember> SaveList(Stream file, RMC_CollarMember model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_CollarMember model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

