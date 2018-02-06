using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ViewModel.WeixinManage
{
    public class VmWeixinManageIndex
    {

        public string AccessToken { get; set; }

        public string AccessTokenExpired { get; set; }

        /// <summary>
        /// 关注自动回复内容
        /// </summary>
        public string AutoResponseContent { get; set; }
    }
}
