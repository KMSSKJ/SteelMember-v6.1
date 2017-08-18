using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Code;
using LeaRun.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ninject;
using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Application.Repository.SteelMember.BLL;

namespace LeaRun.Application.Cache
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.3.4 9:56
    /// 描 述：原材料/构件单位信息缓存
    /// </summary>
    public class UnitCache
    {
        LeaRunFramework_Base_2016Entities context = new LeaRunFramework_Base_2016Entities();
        

        /// <summary>
        /// 单位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RMC_MemberUnit> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RMC_MemberUnit>>("UnitCache");
            if (cacheList == null)
            {
                var data = context.RMC_MemberUnit.ToList();
                CacheFactory.Cache().WriteCache(data, "UnitCache");
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 单位列表
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        public IEnumerable<RMC_MemberUnit> GetList(string unitId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(unitId))
            {
                data = data.Where(t => t.UnitId.ToString() == unitId);
            }
            return data;
        }
        //public Dictionary<string,appUserInfoModel> GetListToApp()
        //{
        //    Dictionary<string, appUserInfoModel> data = new Dictionary<string,appUserInfoModel>();
        //    var datalist = this.GetList();
        //    foreach (var item in datalist)
        //    {
        //        appUserInfoModel one = new appUserInfoModel {
        //            UserId = item.UserId,
        //            Account = item.Account,
        //            RealName = item.RealName,
        //            OrganizeId = item.OrganizeId,
        //            DepartmentId = item.DepartmentId
        //        };
        //        data.Add(item.UserId, one);
        //    }

        //    return data;
        //}
    }
}
