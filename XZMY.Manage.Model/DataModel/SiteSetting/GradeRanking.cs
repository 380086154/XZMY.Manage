using System;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.SiteSetting
{
    /// <summary>
    /// 年级排名
    /// </summary>
    [Serializable]
    [DBTable("GradeRanking")]
    public class GradeRanking : EntityBase, IDataModel
    {
        #region Properties 

        public String Name { get; set; }

        #endregion
    }
}
