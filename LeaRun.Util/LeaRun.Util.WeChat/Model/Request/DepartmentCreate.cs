using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    /// <summary>
    /// 创建申请部门
    /// </summary>
    public class DepartmentCreate : OperationRequestBase<DepartmentResult, HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 申请部门名称。长度限制为1~64个字符
        /// </summary>
        /// <returns></returns>
        [Length(1, 64)]
        [IsNotNull]
        public string name { get; set; }

        /// <summary>
        /// 父亲申请部门id。根申请部门id为1
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string parentid { get; set; }

        /// <summary>
        /// 在父申请部门中的次序。从1开始，数字越大排序越靠后
        /// </summary>
        /// <returns></returns>
        public string order { get; set; }
    }
}
