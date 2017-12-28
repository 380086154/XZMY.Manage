using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Program;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Program
{
    /// <summary>
    /// 活动课程历练的能力名称及描述表
    /// </summary>
    public class VmProgramAbility : ViewBase, IActionViewModel<ProgramAbility>
    {
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        public EProgramType Type { get; set; }
        public String TypeName  { get; set; }
        /// <summary>
        /// 能力名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 能力描述
        /// </summary>
        public String Description { get; set; }
        #region Extendsions

        public ProgramAbility CreateNewDataModel()
        {
            var model = new ProgramAbility();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.Type = Type;
            model.Description = Description;
            return model;
        }

        public ProgramAbility MergeDataModel(ProgramAbility model)
        {
            model.Name = Name;
            model.Type = Type;
            model.Description = Description;
            return model;
        }
        #endregion
    }
}
