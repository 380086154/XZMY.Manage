using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Models
{
   /// <summary>
    /// 微信企业号成员详细信息 结果实体
    /// </summary>
    public class WxQyUserinfo
    {
        public string userid { get; set; }

        public string openid { get; set; }

        public string name { get; set; }

        public List<long> department { get; set; }

        //职位
        public string position { get; set; }

        //性别
        public int gender { get; set; }

        //电话
        public string mobile { get; set; }

        //email
        public string email { get; set; }

        //头像
        public string avatar { get; set; }
    }
}
