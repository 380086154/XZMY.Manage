using System;
using XZMY.Manage.Model.DataModel.User;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;

namespace XZMY.Manage.Service.Auth.Models.ViewModel
{
    public class VmMenu
    {
        #region zTree 必要属性

        /// <summary>
        /// 
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public Guid pId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary> 
        /// 字体、颜色
        /// </summary>
        public string font { get; set; }

        //public string checked { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }

        #endregion

        /// <summary>
        /// Action 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 类型：1：Moddule 2：Action
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 字体图标样式
        /// </summary>
        public string FontIconsClass { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 可见状态：1显示 2隐藏
        /// </summary>
        public int Visible { get; set; }
        /// <summary>
        /// 状态 1启用 2禁用
        /// </summary>
        public int State { get; set; }
    }
}
