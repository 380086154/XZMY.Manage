using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XZMY.Manage.Model.ServiceModel.Plan;
using XZMY.Manage.Web.Controllers.Planners;
using XZMY.Manage.Web.Controllers.SiteSetting;

namespace XZMY.Manage.Web
{
    public partial class TestDataDictionary : System.Web.UI.Page
    {
        Service.Utils.DataDictionary.DataDictionaryItem dataItem = new Service.Utils.DataDictionary.DataDictionaryItem();
        public string s1 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //s1 = "<p>代理商等级代理商等级代理商等级<b>代理商等级代理</b>商等<br/>级代理商等级代理商等级代理商等级代理商等级代理商等级</p>";
            //AddScoresItems();
            //AddProjectType();//活动路线类型
            //AddCourseType();//活动路线类型
            
            //AddAgentLevel();//代理商等级

            //AddNature(); //代理商性质

            //AddCategory();//代理商类型

            //AddAdvisoryType();     //咨询类型
            //AddAdvisoryContactType();        //咨询联系方式
            //AddPlanner();//添加规划师的数据字典


        }
        //活动路线类型   ProjectType
        public void AddProjectType()
        {
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.RemoveCatagory("ProjectType");
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory("ProjectType");
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("A82456EF-BF63-4EDC-AB51-DEC22FDB7420"),
                Name = "夏令营",
                Descr = "夏令营",
                EName = "SummerCamp",
                IsDefault=false,
                IsSystem=false,
                Sort=1,
                State=1
            };
            
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData("ProjectType", dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("BDA3E825-31FC-4C9D-AF06-B010B40EEF13"),
                Name = "冬令营",
                Descr = "冬令营",
                EName = "WinterCamp",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData("ProjectType", dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("0699D17B-6D04-4DE6-8E50-0B0BEEC36E7A"),
                Name = "军训",
                Descr = "军训",
                EName = "MilitaryTraining",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData("ProjectType", dataItem);
        }
        //课程类型   CourseType
        public void AddCourseType()
        {
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.RemoveCatagory("CourseType");
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory("CourseType");
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("A7C87011-B6C1-4A9B-B275-C4905067F3B7"),
                Name = "语言课程",
                Descr = "语言课程",
                EName = "CourseType001",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData("CourseType", dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("F9BDB8FE-A84E-47C4-8393-B8E3503BDEDE"),
                Name = "暑期课程",
                Descr = "暑期课程",
                EName = "CourseType002",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            XZMY.Manage.Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData("CourseType", dataItem);
        }
        //代理商等级
        public void AddAgentLevel(string catagoryName = "AgentLevel")
        {
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("6F97B0E4-80DC-4567-91EF-F7A801EBAD3F"),
                Name = "普通",
                Descr = "普通",
                EName = "Normal",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId = Guid.Parse("8D7BECB9-8434-434D-B6EF-4300525D8209"),
                Name = "中级",
                Descr = "中级",
                EName = "Middle",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("4BDC8916-DB15-4AC6-8BC4-6354BBFCFBDC"),
                Name = "高级",
                Descr = "高级",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
        }
        //代理商性质
        public void AddNature(string catagoryName = "AgentNature")
        {
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("09BEAEFA-AC72-410A-A2AB-6E2CD4D20CF7"),
                Name = "普通",
                Descr = "普通",
                EName = "Normal",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("BF35AF44-CDCA-4B6E-B157-77D561540604"),
                Name = "中级",
                Descr = "中级",
                EName = "Middle",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("6D0F08C0-0752-4ECF-92A9-DD9EFDE4AB33"),
                Name = "高级",
                Descr = "高级",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
        }

        //代理商类型
        public void AddCategory(string catagoryName = "AgentCategory")
        {
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("FC31E4A0-38DA-4071-932F-B0990331B9E5"),
                Name = "留学中介",
                Descr = "留学中介",
                EName = "Normal",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("DAAA6591-6DA7-474A-9DEE-43B114B6B777"),
                Name = "语培机构",
                Descr = "语培机构",
                EName = "Middle",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("BF9D5773-1B7A-4328-8C87-E533B60DE038"),
                Name = "国际学校",
                Descr = "国际学校",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("138820DD-9779-4A02-84E9-9B0AEC58BF9B"),
                Name = "移民公司",
                Descr = "移民公司",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("CE310122-58BA-4581-BFEB-6C47EEF8E97A"),
                Name = "其他渠道",
                Descr = "其他渠道",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
        }
        
        //咨询类型
        public void AddAdvisoryType(string catagoryName = "AdvisoryType")
        {
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("42ABA2BD-DBDC-4EAF-97F9-55B21643129E"),
                Name = "课程",
                Descr = "课程",
                EName = "Normal",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("E695D7A4-C1DA-4A01-AC46-21DC77A71329"),
                Name = "活动",
                Descr = "活动",
                EName = "Middle",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("D7C64166-8712-4EBF-9A18-255EE28DEEFD"),
                Name = "服务",
                Descr = "服务",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("D3182786-E4D0-4645-9966-DB87154E474B"),
                Name = "留学规划",
                Descr = "留学规划",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("B18AFBFE-735C-46F8-9FE4-47F28E5A5F2E"),
                Name = "其它",
                Descr = "其它",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
        }
        
        //咨询联系方式
        public void AddAdvisoryContactType(string catagoryName = "AdvisoryContactType")
        {
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("EF67747D-4A5A-4B89-B413-A269851B75BE"),
                Name = "手机",
                Descr = "手机",
                EName = "Normal",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("BFC43DE5-7C0B-4087-897B-B231E2C14735"),
                Name = "QQ",
                Descr = "QQ",
                EName = "Middle",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("997BB3E7-845E-44B5-AA39-9600D6791E8D"),
                Name = "微信",
                Descr = "微信",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);

            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("91BC9E2E-40BD-4A74-B409-0FDE223C68F1"),
                Name = "邮箱",
                Descr = "邮箱",
                EName = "Advanced",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/CourseTemplate/Edit");
        }
        /// <summary>
        /// 添加默认数据
        /// </summary>
        public void AddScoresItems()
        {
            //ScoreItemsController bll = new ScoreItemsController();
            //Model.ViewModel.SiteSetting.VmScoreItemsEdit model = new Model.ViewModel.SiteSetting.VmScoreItemsEdit();
            ////model.Id =Guid.Parse("61296AF4-7852-4174-B63E-3C77360D6F4F");
            //model.Code = "G001";
            //model.Name = "自然探索";
            //model.Type = Model.DataModel.SiteSetting.ScoreItemType.素质;
            //bll.Create(model);
            //model = new Model.ViewModel.SiteSetting.VmScoreItemsEdit();
            ////model.DataId =Guid.Parse("BC70C47A-FAB2-4BB2-A31D-CCFC4E2172F4");
            //model.Code = "G002";
            //model.Name = "自我认识";
            //model.Type = Model.DataModel.SiteSetting.ScoreItemType.素质;
            //bll.Create(model);
            //model = new Model.ViewModel.SiteSetting.VmScoreItemsEdit();
            ////model.DataId =Guid.Parse("7459DFE4-229F-4135-8D36-4DE02F7BEA10");
            //model.Code = "G003";
            //model.Name = "人际关系";
            //model.Type = Model.DataModel.SiteSetting.ScoreItemType.素质;
            //bll.Create(model);
            //model = new Model.ViewModel.SiteSetting.VmScoreItemsEdit();
            ////model.DataId =Guid.Parse("F91B38FD-6171-4585-B5DF-09759CF42445");
            //model.Code = "G004";
            //model.Name = "空间思维";
            //model.Type = Model.DataModel.SiteSetting.ScoreItemType.素质;
            //bll.Create(model);
            //model = new Model.ViewModel.SiteSetting.VmScoreItemsEdit();
            ////model.DataId =Guid.Parse("E57E1CDB-9862-4195-81C7-BE57FD47289C");
            //model.Code = "G005";
            //model.Name = "数学逻辑";
            //model.Type = Model.DataModel.SiteSetting.ScoreItemType.素质;
            //bll.Create(model);
            //model = new Model.ViewModel.SiteSetting.VmScoreItemsEdit();
            ////model.DataId =Guid.Parse("E57E1CDB-9862-4195-81C7-BE57FD47289C");
            //model.Code = "G006";
            //model.Name = "身体运动";
            //model.Type = Model.DataModel.SiteSetting.ScoreItemType.素质;
            //bll.Create(model);

        }

        public void AddMember(Model.ViewModel.Members.VmMember modelMember, Model.ViewModel.Members.VmStudent modelStudent, Model.ViewModel.Members.VmParents modelParents)
        {
            var handlerMember = new Service.Handlers.BaseCreateHandler<Model.DataModel.Members.Member>(modelMember);
            handlerMember.Invoke();

            modelParents.MemberId= modelMember.DataId;
            var handlerParents = new Service.Handlers.BaseCreateHandler<Model.DataModel.Members.Parent>(modelParents);
            handlerParents.Invoke();

            //modelMember.Id
            modelStudent.MemberId = modelMember.DataId;
            modelStudent.ParentsId = modelParents.DataId;
            var handlerStudent = new Service.Handlers.BaseCreateHandler<Model.DataModel.Members.Student>(modelStudent);
            handlerStudent.Invoke();

            
            


        }
        
        /// <summary>
        /// 添加规划师的数据字典
        /// </summary>
        public void AddPlanner()
        {
            String catagoryName = "";
            #region PlannerLevel
            catagoryName = "PlannerLevel";
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("72B40DCF-BE0F-4A37-91DF-D1EF71B1AC1D"),
                Name = "超级规划师",
                Descr = "超级规划师",
                EName = "P001",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("8E8E372B-20C6-4FE2-BE14-474A205A280B"),
                Name = "低级规划师",
                Descr = "低级规划师",
                EName = "P002",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("EC336A21-9503-44DD-9D4B-0746A819D386"),
                Name = "一般规划师",
                Descr = "一般规划师",
                EName = "P003",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
            #endregion
            #region PlannerQualifications
            catagoryName = "PlannerQualifications";
            Service.Utils.DataDictionary.DataDictionaryManager.AddCatagory(catagoryName);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("83B653BD-ABBC-4C3F-BC42-03ED04622146"),
                Name = "一般资质",
                Descr = "一般资质",
                EName = "PZ001",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("EC343652-2795-4697-888F-4CD0068576E3"),
                Name = "没有资质",
                Descr = "没有资质",
                EName = "PZ002",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
            dataItem = new Service.Utils.DataDictionary.DataDictionaryItem
            {
                DataId =Guid.Parse("C4F3B819-9315-4F9C-8081-61A6A86E6557"),
                Name = "有资质",
                Descr = "有资质",
                EName = "PZ003",
                IsDefault = false,
                IsSystem = false,
                Sort = 1,
                State = 1
            };
            Service.Utils.DataDictionary.DataDictionaryManager.SaveOrUpdateData(catagoryName, dataItem);
            #endregion
          
        }
        #region 创建规划
        protected void btnPlan_Click(object sender, EventArgs e)
        {
           
        }
        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {
            PlanController bllPlan = new PlanController();
            bllPlan.PlanStudnetGrade(Guid.Parse("92D69319-C014-4EBA-8784-02F56B5CD790"));
        }
    }
}