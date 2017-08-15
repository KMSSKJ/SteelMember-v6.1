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
    public class MemberUnitBLL : MemberUnitIBLL
    {
        [Ninject.Inject]
        public MemberUnitIDAL CurrentDAL { get; set; }
        public MemberUnitBLL()
        {

        }
        public RMC_MemberUnit Add(RMC_MemberUnit model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.UnitId == id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_MemberUnit> Find(Expression<Func<RMC_MemberUnit, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_MemberUnit> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberUnit, bool>> whereLambda, bool isAsc, Expression<Func<RMC_MemberUnit, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_MemberUnit> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberUnit, bool>> whereLambda, Expression<Func<RMC_MemberUnit, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_MemberUnit, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_MemberUnit> SaveList(Stream file, RMC_MemberUnit model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_MemberUnit model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

