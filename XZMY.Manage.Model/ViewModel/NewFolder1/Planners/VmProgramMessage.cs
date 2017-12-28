using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Planners;
using XZMY.Manage.Model.Enum;

namespace XZMY.Manage.Model.ViewModel.Planners
{
    [Serializable]
    public class VmProgramMessage : IActionViewModel<ProgramMessage>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 接收人ID
        /// </summary>
        public Guid MemberId { get; set; }
        /// <summary>
        /// 接收学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        /// 历练ID
        /// </summary>
        public Guid ProgramId { get; set; }
        /// <summary>
        /// 历练类型
        /// </summary>
        public EProgramType ProgramType { get; set; }
        /// <summary>
        /// 规划师ID
        /// </summary>
        public Guid PlannerId { get; set; }
        /// <summary>
        /// 规划师姓名
        /// </summary>
        public String PlannerName { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public Boolean IsRead { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime MessageTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public EMessageType MessageType { get; set; }


        public ProgramMessage CreateNewDataModel()
        {
            var model = new ProgramMessage();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.MemberId = MemberId;
            model.StudentId = StudentId;
            model.Message = Message;
            model.ProgramId = ProgramId;
            model.ProgramType = ProgramType;
            model.PlannerId = PlannerId;
            model.PlannerName = PlannerName;
            model.IsRead = IsRead;
            model.MessageTime = MessageTime;
            model.MessageType = MessageType;
            return model;
        }

        public ProgramMessage MergeDataModel(ProgramMessage model)
        {
            model.MemberId = MemberId;
            model.StudentId = StudentId;
            model.Message = Message;
            model.ProgramId = ProgramId;
            model.ProgramType = ProgramType;
            model.PlannerId = PlannerId;
            model.PlannerName = PlannerName;
            model.IsRead = IsRead;
            model.MessageTime = MessageTime;
            model.MessageType = MessageType;
            return model;
        }
    }
}
