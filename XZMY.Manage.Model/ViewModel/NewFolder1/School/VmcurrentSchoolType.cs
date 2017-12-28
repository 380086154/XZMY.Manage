using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.School;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.School
{
    [Serializable]
    public class VmcurrentSchoolType : ViewBase,IActionViewModel<currentSchoolType>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 当前就读学校类型数字ID
        /// </summary>
        public int currentSchoolTypeId { get; set; }
        /// <summary>
        /// 当前就读学校类型名称
        /// </summary>
        public String currentSchoolTypeName { get; set; }
        /// <summary>
        /// 系数
        /// </summary>
        public Decimal coefficient { get; set; }
        #region Extendsions

        public currentSchoolType CreateNewDataModel()
        {
            var model = new currentSchoolType();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.currentSchoolTypeId = currentSchoolTypeId;
            model.currentSchoolTypeName = currentSchoolTypeName;
            model.coefficient = coefficient;
            return model;
        }

        public currentSchoolType MergeDataModel(currentSchoolType model)
        {
            model.currentSchoolTypeId = currentSchoolTypeId;
            model.currentSchoolTypeName = currentSchoolTypeName;
            model.coefficient = coefficient;
            return model;
        }
        #endregion
    }
}
