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
    public class RawMaterialBLL : RawMaterialIBLL
    {
        [Ninject.Inject]
        public RawMaterialIDAL CurrentDAL { get; set; }
        public RawMaterialBLL()
        {

        }
        public RMC_RawMaterialLibrary Add(RMC_RawMaterialLibrary model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.RawMaterialId== id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_RawMaterialLibrary> Find(Expression<Func<RMC_RawMaterialLibrary, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_RawMaterialLibrary> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_RawMaterialLibrary, bool>> whereLambda, bool isAsc, Expression<Func<RMC_RawMaterialLibrary, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_RawMaterialLibrary> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_RawMaterialLibrary, bool>> whereLambda, Expression<Func<RMC_RawMaterialLibrary, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_RawMaterialLibrary, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_RawMaterialLibrary> SaveList(Stream file, RMC_RawMaterialLibrary model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_RawMaterialLibrary model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

