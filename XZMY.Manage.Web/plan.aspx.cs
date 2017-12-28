using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Model.ViewModel.Planners;
using XZMY.Manage.Web.Controllers.Customers;
using XZMY.Manage.Web.Controllers.Planners;
using XZMY.Manage.Web.Controllers.Sys;
using XZMY.Manage.Web.Controllers.WebApis;

using XZMY.Manage.Service.Utils;

namespace XZMY.Manage.Web
{
    public partial class plan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Model.Utils.PlanQuery planQuery = new Model.Utils.PlanQuery();

            planQuery.AnnualBudget = int.Parse(txtGeneralBudget.Text);
            planQuery.Grade = (Model.Enum.EGrade)int.Parse(ddlGrade.SelectedValue);
            planQuery.GradeAbroad = (Model.Enum.EGradeAbroad)int.Parse(ddlAbroadGrade.SelectedValue);
            planQuery.SchoolType = (Model.Enum.ESchoolType)int.Parse(ddlSchoolType.SelectedValue);
            planQuery.EnglishScore = int.Parse(txtEnglishScore.Text);
            planQuery.LearnScore = int.Parse(txtLearnScore.Text);
            planQuery.QualityScore = int.Parse(txtQualityScore.Text);



            Label2.Text = string.Format(" 当前年级[{3}]当前学校等级[{4}],出国年级[{5}]   英语：{0}学科：{1}素质：{2}.年预算：{6},总预算{7}", planQuery.EnglishScore, planQuery.LearnScore, planQuery.QualityScore, planQuery.Grade, planQuery.SchoolType, planQuery.GradeAbroad, planQuery.AnnualBudget, planQuery.GeneralBudget);

            Controllers.WebApis.APlanController ap = new Controllers.WebApis.APlanController();
            var pnList = ap.GetPlanningNote(planQuery);
            StringBuilder sb = new StringBuilder();
            foreach (Model.DataModel.Plan.PlanningNote m in pnList.Output["PlanningNote"])
            {
                sb.AppendFormat("{0},{1},{2}[英语{3}学科{4}素质{5}]<br/>", m.Grade, m.SchoolPlace, m.SchoolType, m.EnglishScore, m.LearnScore, m.QualityScore);
            }
            Label1.Text = sb.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 测试留学意向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button31_Click(object sender, EventArgs e)
        {
            APlanController bllAPlan = new APlanController();
            SmPlanIntention model = new SmPlanIntention();

            model.PlanRecordId = Guid.Parse("9F80638E-4F5E-48D7-9DE5-4403FD615F84");
            model.StudentId = Guid.Parse("0F9202BB-E1D5-4574-993D-A6FF269920CA");
            model.TargetCountryId = Guid.Parse("377A2371-D1F3-4597-8F70-508B7BE8E486");
            model.TargetCountryName = "日本";
            model.EducationId = Guid.Parse("AAE59001-21FE-4D37-B1D4-5EC83FB6874E");
            model.EducationName = "初中三年级";
            model.GoAbroadEducationId = Guid.Parse("16CD6207-0595-4FBA-B0CA-F76F8ED25F99");
            model.GoAbroadEducationIName = "预科";
            model.Fee = 100000.00M;
            model.FeeInterval = "10W以内";
            model.IntentionalSchoolTop = 1;
            model.IntentionalSchoolName = "重庆大学";
            model.GradeRanking = Guid.Parse("69BCD414-D133-4AFB-B5B0-11CBF42E463D");
            model.currentSchoolType = Guid.Parse("29B53DD3-76C9-4273-8F4A-61E78548F24A");
            model.SchoolName = "重庆中学";
            //model.currentSchoolType = 2;
            model.MajorName = "没得专业";
            model.GraduationDate = DateTime.Now;
            model.LearnScore = 80;
            model.listLearn = "小学三好学生,中学二好学生";
            model.listEnglishItem = string.Format("ItemName:托福,ListeningScore:,VerbalScore:,ReadingScore:,WritingScore:,ItemTotalScore:9");
            //model.listEnglishOtherItem
            model.SchoolTypeId = "ADD8F26E-4E9D-48A3-8787-2CBF9F92B714";
            bllAPlan.SetPlanIntention(model);
        }
        /// <summary>
        /// 规划年级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button32_Click(object sender, EventArgs e)
        {
            PlanController bllplan = new PlanController();
            bllplan.PlanStudnetGrade(Guid.Parse("9C077A79-E6A7-413C-9CF5-228243D0F738"));
        }

        protected void Button33_Click(object sender, EventArgs e)
        {
            PlanController bllplan = new PlanController();
            bllplan.PlanStudentPlanProgram(Guid.Parse("3121163a-d67c-4ff9-9d5d-738adaaf16af"));
        }
        /// <summary>
        /// 导出WORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button34_Click(object sender, EventArgs e)
        {

            
        }
        public Stream FileToStream(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                // 打开文件
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                // 读取文件的 byte[]
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
            else
            {
                return null;
            }

        }



    }
}