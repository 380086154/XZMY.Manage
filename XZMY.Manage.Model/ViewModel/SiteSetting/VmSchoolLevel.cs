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
    [Serializable]
    public class VmSchoolLevel : ViewBase, IActionViewModel<SchoolLevel>
    {
        public Guid DataId { get; set; }
        #region Properties 
        /// <summary>
        /// 类型名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int32 Code { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 分值 [区间 0 - 2]
        /// </summary>
        public decimal Score { get; set; }

        public string ScoreFormat { get
            {
                return Score.ToString("N2");
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }

        public string StateName
        {
            get
            {
                string esn = "";
                switch (State)
                {
                    case EState.其它:
                        esn = "禁用";
                        break;
                    case EState.启用:
                        esn = "启用";
                        break;
                    case EState.禁用:
                        esn = "禁用";
                        break;
                    default:
                        esn = "禁用";
                        break;
                }
                return esn;
            }
        }

        #endregion


        #region Extendsions

        public SchoolLevel CreateNewDataModel()
        {
            var model = new SchoolLevel();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.Description = Description;
            model.Score = Score;
            model.State = State;
            model.Code = Code;
            return model;
        }

        public SchoolLevel MergeDataModel(SchoolLevel model)
        {
            model.Name = Name;
            model.Description = Description;
            model.Score = Score;
            model.State = State;
            model.Code = Code;
            return model;
        }
        #endregion

    }
}
