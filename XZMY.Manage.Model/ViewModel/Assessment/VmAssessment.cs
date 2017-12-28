using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Assessment;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Assessment
{
    /// <summary>
    /// 问题答案
    /// </summary>
    [Serializable]
    public class VmAssessment :ViewBase, IActionViewModel<DataModel.Assessment.Assessment>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 试卷编码
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 试卷总分
        /// </summary>
        public decimal TotalScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        public String StateName { get; set; }

        public DataModel.Assessment.Assessment CreateNewDataModel()
        {
            var model = new DataModel.Assessment.Assessment();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.Code = Code;
            model.TotalScore = TotalScore;
            model.State = State;
            return model;
        }

        public DataModel.Assessment.Assessment MergeDataModel(DataModel.Assessment.Assessment model)
        {
            model.Name = Name;
            model.Code = Code;
            model.TotalScore = TotalScore;
            model.State = State;
            return model;
        }
    }
}