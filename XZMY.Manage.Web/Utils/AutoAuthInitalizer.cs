using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Web.Controllers;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Utils
{
    public class AutoAuthInitalizer
    {
        public static void CreateActionData()
        {
            var ass = Assembly.GetExecutingAssembly();
            var ctrls = ass.GetTypes().Where(m => m.IsSubclassOf(typeof(ControllerBase)));
            var attrs = new List<AutoCreateAuthActionAttribute>();
            foreach (var item in ctrls)
            {
                var acts = item.GetMethods().Where(m => m.GetCustomAttribute<AutoCreateAuthActionAttribute>() != null).ToList();
                foreach (var act in acts)
                {
                    var attr = act.GetCustomAttribute<AutoCreateAuthActionAttribute>();
                    attrs.Add(attr);
                }
            }
            CreateActionData(attrs);
        }

        private static void CreateActionData(List<AutoCreateAuthActionAttribute> attrs)
        {
            try
            {
                var moduleids = new Dictionary<string, Guid>();
                var moduleNames = attrs.Select(m => m.ModuleCode).Distinct().ToList();
                foreach (var name in moduleNames)
                {
                    var service = new GetEntityBySingleColumnService<Sys_Module>();
                    service.ColumnMember = m => m.Code;
                    service.ColumnValue = name;
                    var res = service.Invoke();
                    if (res.Count > 0)
                    {
                        moduleids[name] = res[0].DataId;
                    }
                }

                foreach (var attr in attrs)
                {
                    using (var wrapper = new SqlTransactionWrapper())
                    {
                        var code = attr.Code;
                        var os = new GetEntityBySingleColumnService<Sys_Action>()
                        {
                            ColumnMember = m => m.Code,
                            ColumnValue = code
                        };
                        if (os.Invoke().Count > 0) continue;
                        var model = new Sys_Action
                        {
                            DataId = Guid.NewGuid(),
                            Name = attr.Name,
                            Code = attr.Code,
                            Description = attr.Remark,
                            ModuleCode = attr.ModuleCode,
                            ModuleId = moduleids.ContainsKey(attr.ModuleCode) ? moduleids[attr.ModuleCode] : Guid.Empty,
                            Url = attr.Url,
                            Visible = attr.Visible ? Model.Enum.EVisible.显示 : Model.Enum.EVisible.隐藏,
                            State = Model.Enum.EState.启用
                        };
                        model.SetActorInfomation(LoggedUserManager.GetSystemAccount().GetActorInfomationSynchronizer());
                        var service = new BaseCreateService<Sys_Action>(model);
                        service.Invoke(wrapper.Transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AutoAuthInitalizer", "测试用例异常", LogLevel.Error, ex);
            }
        }
    }
}