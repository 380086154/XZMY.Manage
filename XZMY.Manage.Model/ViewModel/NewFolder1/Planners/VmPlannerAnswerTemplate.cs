
using System;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    [Serializable]
    public class VmPlannerAnswerTemplate : IActionViewModel<PlannerAnswerTemplate>
    {
        #region Properties 

        /// <summary>
        /// 规划师回答模板Id
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("规划师回答模板Id")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 模板标题
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("模板标题")] 
        public String Title { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        //[EntAttributes.DBColumn("Code")] 
        //[DisplayName("模板编码")] 
        public String Code { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        //[EntAttributes.DBColumn("Content")] 
        //[DisplayName("模板内容")] 
        public String Content { get; set; }

        #endregion

        #region Extendsions

        public PlannerAnswerTemplate CreateNewDataModel()
        {
            var model = new PlannerAnswerTemplate();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Title = Title;
            model.Code = Code;
            model.Content = Content;
            return model;
        }

        public PlannerAnswerTemplate MergeDataModel(PlannerAnswerTemplate model)
        {
            model.Title = Title;
            model.Code = Code;
            model.Content = Content;
            return model;
        }
        #endregion
    }

}

