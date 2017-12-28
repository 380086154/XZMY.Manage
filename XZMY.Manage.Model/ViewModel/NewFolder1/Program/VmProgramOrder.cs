using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Program;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Program
{
    public class VmProgramOrder :ViewBase, IActionViewModel<ProgramOrder>
    {
        /// <summary>
        /// 历练ID
        /// </summary>
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 历练模板ID
        /// </summary>
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 类型 1活动 2课程
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 历练名称
        /// </summary>
        public String ProgramName { get; set; }
        /// <summary>
        /// 报名学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 报名学生MemberID
        /// </summary>
        public Guid MemberId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }
        #region Extendsions

        public ProgramOrder CreateNewDataModel()
        {
            var model = new ProgramOrder();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.ProgramId = ProgramId;
            model.TemplateId = TemplateId;
            model.Type = Type;
            model.ProgramName = ProgramName;
            model.StudentId = StudentId;
            model.MemberId = MemberId;
            model.OrderId = OrderId;
            return model;
        }

        public ProgramOrder MergeDataModel(ProgramOrder model)
        {
            model.ProgramId = ProgramId;
            model.TemplateId = TemplateId;
            model.Type = Type;
            model.ProgramName = ProgramName;
            model.StudentId = StudentId;
            model.MemberId = MemberId;
            model.OrderId = OrderId;
            return model;
        }
        #endregion
    }
}
