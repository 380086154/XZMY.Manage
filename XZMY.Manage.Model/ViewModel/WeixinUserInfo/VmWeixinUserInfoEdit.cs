using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;

namespace XZMY.Manage.Model.ViewModel.WeixinUserInfo
{
    /// <summary>
    /// 编辑微信用户
    /// </summary>
    [Serializable]
    public class VmWeixinUserInfoEdit : IActionViewModel2M<WeixinUserInfoDto>
    {
        #region Properties 

        /// <summary>
        /// 主键id
        /// </summary>
        public Guid DataId { get; set; }

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

        #endregion

        #region Extendsions

        public WeixinUserInfoDto CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return this.ConvertTo<WeixinUserInfoDto>();
        }

        public WeixinUserInfoDto MergeDataModel(WeixinUserInfoDto model)
        {
            return model.ConvertTo<WeixinUserInfoDto>();
        }

        #endregion
    }
}
