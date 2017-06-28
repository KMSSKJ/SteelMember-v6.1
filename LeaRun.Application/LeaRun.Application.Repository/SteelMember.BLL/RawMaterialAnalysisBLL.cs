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
    public class RawMaterialAnalysisBLL : RawMaterialAnalysisIBLL
    {
        [Ninject.Inject]
        public RawMaterialAnalysisIDAL CurrentDAL { get; set; }
        public RawMaterialAnalysisBLL()
        {

        }
        public RMC_RawMaterialAnalysis Add(RMC_RawMaterialAnalysis model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<int> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.Id== id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public IEnumerable<RMC_RawMaterialAnalysis> Find(Expression<Func<RMC_RawMaterialAnalysis, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }
 
        public IEnumerable<RMC_RawMaterialAnalysis> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_RawMaterialAnalysis, bool>> whereLambda, bool isAsc, Expression<Func<RMC_RawMaterialAnalysis, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public IEnumerable<RMC_RawMaterialAnalysis> SelectPaging<S>(int pageIndex, int pageSize, Expression<Func<RMC_RawMaterialAnalysis, bool>> whereLambda, Expression<Func<RMC_RawMaterialAnalysis, bool>> whereLambda1, bool isAsc, Expression<Func<RMC_RawMaterialAnalysis, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public List<RMC_RawMaterialAnalysis> SaveList(Stream file, RMC_RawMaterialAnalysis model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_RawMaterialAnalysis model)
        {
            return CurrentDAL.Modified(model);
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}

