using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZMY.Manage.Web.Content.Code
{
    public partial class PlanRecordToWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid PlanRecordId = Request.QueryString["PlanRecordId"].ToGuid(Guid.Empty);
            if (PlanRecordId != Guid.Empty)
            {
                PlanRecordWord bllPlanRecordWord = new PlanRecordWord();
                bllPlanRecordWord.ToWord(PlanRecordId);
            }
            
        }
    }
}