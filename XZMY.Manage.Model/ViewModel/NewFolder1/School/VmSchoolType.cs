
using System;
using XZMY.Manage.Model.DataModel.School;
using XZMY.Manage.Model.Enum;
using XZMY.Manage.Model.Utils;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.School
{
    [Serializable]
    public class VmSchoolType : ViewBase,IActionViewModel<SchoolType>
    {
        #region Properties 

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 学校类型名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 类型学校费用
        /// </summary>
        public Int32 Fee { get; set; }
        /// <summary>
        /// 英语
        /// </summary>
        public Decimal EnglishScore { get; set; }
        /// <summary>
        /// 学术
        /// </summary>
        public Decimal LearnScore { get; set; }
        /// <summary>
        /// 素质
        /// </summary>
        public Decimal QualityScore { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        public String StateName  { get; set; }
        #endregion

        #region Extendsions

        public SchoolType CreateNewDataModel()
        {
            var model = new SchoolType();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Name = Name;
            model.EnglishScore = EnglishScore;
            model.QualityScore = QualityScore;
            model.LearnScore = LearnScore;
            model.State = State;
            model.Fee = Fee;
            return model;
        }

        public SchoolType MergeDataModel(SchoolType model)
        {
            model.Name = Name;
            model.EnglishScore = EnglishScore;
            model.QualityScore = QualityScore;
            model.LearnScore = LearnScore;
            model.State = State;
            model.Fee = Fee;
            return model;
        }
        #endregion
    }

}
