using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Planners;

namespace XZMY.Manage.Model.ViewModel.Question
{
    public class VmAnswerQuestion : IActionViewModel<ProblemPlanner>
    {
        public Guid DataId
        {
            get; set;
        }

        public String Answer { get; set; }

        public ProblemPlanner CreateNewDataModel()
        {
            throw new NotSupportedException();
        }

        public ProblemPlanner MergeDataModel(ProblemPlanner model)
        {
            model.Answer = Answer;
            model.AnswerTime = DateTime.Now;
            //model.State = 2;
            return model;
        }
    }
}
