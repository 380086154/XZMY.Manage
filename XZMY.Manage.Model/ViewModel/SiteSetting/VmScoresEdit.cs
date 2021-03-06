﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    public class VmScoresEdit : ViewBase, IActionViewModel<Scores>
    {
       
        /// <summary>
        /// 分值项目Id
        /// </summary>
        public Guid ScoreItemsId { get; set; }
        /// <summary>
        /// 分值名称
        /// </summary>
        public string ScoreItemsName { get; set; }
        /// <summary>
        /// 分值来源项目Id  如ProjectID
        /// </summary>
        public Guid SourceId { get; set; }
        /// <summary>
        /// 分值来源表名称
        /// </summary>
        public String SourceType { get; set; }
        /// <summary>
        /// 具体分值
        /// </summary>
        public decimal ScoreValue { get; set; }

        
        public Scores CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return new Scores()
            {
                DataId = DataId,
                ScoreItemsId = ScoreItemsId,
                ScoreItemsName = ScoreItemsName,
                SourceId = SourceId,
                SourceType = SourceType,
                ScoreValue = ScoreValue
            };
        }

        public Scores MergeDataModel(Scores model)
        {
            model.ScoreItemsId = ScoreItemsId;
            model.ScoreItemsName = ScoreItemsName;
            model.SourceId = SourceId;
            model.SourceType = SourceType;
            model.ScoreValue = ScoreValue;
            return model;
        }
    }
}
