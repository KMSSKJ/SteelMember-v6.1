using LeaRun.Application.Repository.SteelMember.DAL;
using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeaRun.Application.Repository.SteelMember.BLL
{
    public class MemberMaterialBLL :MemberMaterialIBLL
    {
        //ICompanyDAL CurrentDAL = IoC.Resolve<ICompanyDAL>();

        [Ninject.Inject]
        public MemberMaterialDAL CurrentDAL { get; set; }
        public MemberMaterialBLL()
        {

        }
        public RMC_MemberMaterial Add(RMC_MemberMaterial model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.MemberMaterialId == id).Single());
            }); 

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_MemberMaterial> Find(Expression<Func<RMC_MemberMaterial, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_MemberMaterial> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberMaterial, bool>> whereLambda, bool isAsc, Expression<Func<RMC_MemberMaterial, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_MemberMaterial> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_MemberMaterial, bool>> whereLambda, Expression<Func<RMC_MemberMaterial, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_MemberMaterial, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_MemberMaterial> SaveList(Stream file, RMC_MemberMaterial model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_MemberMaterial model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

