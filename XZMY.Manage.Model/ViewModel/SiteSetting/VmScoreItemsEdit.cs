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
   public class VmScoreItemsEdit : ViewBase, IActionViewModel<ScoreItems>
    {
        /// <summary>
        /// 分值名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分值编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 分值项目类型     1英语分值  2学科分值  3素质分值
        /// </summary>
        public ScoreItemType Type { get; set; }
        public ScoreItems CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            return new ScoreItems()
            {
                DataId = DataId,
                Name = Name,
                Code = Code,
                Type = Type
            };
        }

        public ScoreItems MergeDataModel(ScoreItems model)
        {
            model.Name = Name;
            model.Code = Code;
            model.Type = Type;
            return model;
        }
    }
}
