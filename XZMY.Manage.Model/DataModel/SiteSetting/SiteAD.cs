using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    [DBTable("SiteAD")]
    public class SiteAD : EntityBase, IDataModel
    {
        /// <summary>
        /// 网站广告名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 网站广告编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 广告类型 1单图片  2多图片 3视频
        /// </summary>
        public SiteADType Type { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string TypeName  { get; set; }
        /// <summary>
        /// 广告图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 倒转地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 广告宽度
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        /// 广告高度
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        /// 状态  1启用 2禁用
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }

    }
    public enum SiteADType
    {
        未知 = 0,
        单图 = 1,
        视频 = 2,
        多图 = 3
    }
}
