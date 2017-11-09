using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    public class DepartmentDelete : OperationRequestBase<OperationResultsBase,HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token=ACCESS_TOKEN&id={0}";
        protected override string Url()
        {
            return string.Format(url, id);
        }

        /// <summary>
        /// 申请部门id。（注：不能删除根申请部门；不能删除含有子申请部门、成员的申请部门）
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string id { get; set; }
    }
}
