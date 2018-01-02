using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.DataModel
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class WxUser
    {
        public Guid DataId { get; set; }

        /// <summary>
        /// 关联的用户Id
        /// </summary>
        public Guid RelatedUserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 用户性别 值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public string Privilege { get; set; }

        public WxUserLogon WxUserLogon { get; set; }
    }
}
