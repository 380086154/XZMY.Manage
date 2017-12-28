using System;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Planners
{
    [Serializable]
    [DBTable("V_PlannerList")]
    public class V_PlannerList : EntityBase, IDataModel
    {
        public String Name { get; set; }
        public String Code { get; set; }
        public String LevelName { get; set; }
        public String QualificationsName { get; set; }
        public String LoginName { get; set; }
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName  { get; set; }
    }
}
