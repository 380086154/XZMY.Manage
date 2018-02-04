using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel
{
    /// <summary>
    /// 微信用户
    /// </summary>
    [Serializable]
    [DBTable("WeixinUserInfo")]
    public class WeixinUserInfoDto : EntityBase, IDataModel
    {
        public WeixinUserInfoDto()
        {
        }

        /// <summary>
        /// 微信用户唯一 Id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        /// 备注名称
        /// </summary>
        public string RemarkName { get; set; }

        /// <summary>
        /// Xml 
        /// </summary>
        public string XmlDocument { get; set; }    
    }
}
