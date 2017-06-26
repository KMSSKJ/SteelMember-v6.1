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
    public class TreeBLL : TreeIBLL
    {
        [Ninject.Inject]
        public TreeIDAL CurrentDAL { get; set; }
        public TreeBLL()
        {

        }
        public RMC_Tree Add(RMC_Tree model)
        {
            return CurrentDAL.Add(model);
        }

        public int Remove(List<string> delbyid)
        {
            delbyid.ToList().ForEach(id =>
            {
                CurrentDAL.Remove(Find(p => p.TreeID == id).Single());
            });

            return CurrentDAL.SaveChange();
        }

        public string Remove_str(List<string> delbyid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RMC_Tree> Find(Expression<Func<RMC_Tree, bool>> whereLambda)
        {
            return CurrentDAL.Find(whereLambda);
        }

        public List<RMC_Tree> SaveList(Stream file, RMC_Tree model)
        {
            throw new NotImplementedException();
        }

        public int Modified(RMC_Tree model)
        {
            return CurrentDAL.Modified(model);
        }

        public IEnumerable<RMC_Tree> FindPage<S>(int pageIndex, int pageSize, Expression<Func<RMC_Tree, bool>> whereLambda, bool isAsc, Expression<Func<RMC_Tree, S>> orderbyLambda, out int total)
        {
            return CurrentDAL.FindPaging(pageIndex, pageSize, whereLambda, isAsc, orderbyLambda, out total);
        }

        public int Remove(List<int> delbyid)
        {
            throw new NotImplementedException();
        }
    }
}
