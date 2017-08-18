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
    public class OrderMemberBLL : OrderMemberIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public OrderMemberIDAL CurrentDAL { get; set; }
        public OrderMemberBLL()
        {

        }
        public RMC_OrderMember Add(RMC_OrderMember model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.OrderMemberId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_OrderMember> Find(Expression<Func<RMC_OrderMember, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_OrderMember> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_OrderMember, bool>> whereLambda, bool isAsc, Expression<Func<RMC_OrderMember, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_OrderMember> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_OrderMember, bool>> whereLambda, Expression<Func<RMC_OrderMember, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_OrderMember, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_OrderMember> SaveList(Stream file, RMC_OrderMember model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_OrderMember model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

