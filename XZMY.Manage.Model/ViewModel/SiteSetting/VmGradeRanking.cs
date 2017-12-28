using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.SiteSetting;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.SiteSetting
{
    [Serializable]
    public class VmGradeRanking : ViewBase, IActionViewModel<GradeRanking>
    {
        public Guid DataId { get; set; }
        #region Properties 
        public String Name { get; set; }
        #endregion


        #region Extendsions

        public GradeRanking CreateNewDataModel()
        {
            var model = new GradeRanking();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            return model;
        }

        public GradeRanking MergeDataModel(GradeRanking model)
        {
            model.Name = Name;
            return model;
        }
        #endregion
    }
}
