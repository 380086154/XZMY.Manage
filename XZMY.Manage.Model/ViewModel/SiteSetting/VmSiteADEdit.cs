using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    public class VmSiteADEdit : ViewBase, IActionViewModel<SiteAD>
    {
        public Guid DataId { get; set; }
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

      

        public SiteAD CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return new SiteAD()
            {
                DataId = DataId,
                Name = Name,
                Code = Code,
                Type = Type,
                ImageUrl = ImageUrl,
                Url = Url,
                Width = Width,
                Height = Height,
                State = State,
                
            };
        }

        public SiteAD MergeDataModel(SiteAD model)
        {
            model.Name = Name;
            model.Code = Code;
            model.Type = Type;
            model.ImageUrl = ImageUrl;
            model.Url = Url;
            model.Width = Width;
            model.Height = Height;
            model.State = State;
            return model;
        }
    }
}
