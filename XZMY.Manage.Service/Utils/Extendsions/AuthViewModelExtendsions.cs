using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;
using XZMY.Manage.Service.Auth.Models.ViewModel;

namespace XZMY.Manage.Service.Utils.Extendsions
{
    public static class ExceptionFormatExtend
    {
        public static string FormatMessage(this Exception ex)
        {
            return ex.Message + Environment.NewLine + ex.StackTrace;
        }
    }
    public static class AuthViewModelExtendsions
    {
        public static List<Guid> GetModuleIdList(this VmRoleEdit vm)
        {
            try
            {
                if (vm.Modules == null) return null;
                return vm.Modules.Split(',').Select(m => m.ToGuid()).Where(m => m != null).Select(m => m.Value).ToList();
            }
            catch
            {
                return null;
            }
        }
        public static List<Guid> GetActionIdList(this VmRoleEdit vm)
        {
            try
            {
                if (vm.Modules == null) return null;
                return vm.Actions.Split(',').Select(m => m.ToGuid()).Where(m => m != null).Select(m => m.Value).ToList();
            }
            catch
            {
                return null;
            }
        }

        internal static Sys_Role GetDataModel(this VmRoleEdit vm, bool newmodel = false)
        {
            return new Sys_Role
            {
                DataId = newmodel ? Guid.NewGuid() : vm.DataId,
                Name = vm.Name ?? string.Empty,
                State = vm.State,
                Description = vm.Description ?? string.Empty
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="newmodel"></param>
        /// <returns></returns>
        internal static Sys_Module GetDataModel(this VmModuleEdit vm, bool newmodel = false)
        {
            return new Sys_Module
            {
                DataId = newmodel ? Guid.NewGuid() : vm.DataId,
                Name = vm.Name ?? string.Empty,
                State = vm.State,
                Description = vm.Description ?? string.Empty,
                Code = vm.Code ?? string.Empty,
                ParentId = vm.ParentId,
                Sort = vm.Sort,
                Visible = vm.Visible
            };
        }

        internal static Sys_Action GetDataModel(this VmActionEdit vm, bool newmodel = false)
        {
            return new Sys_Action
            {
                DataId = newmodel ? Guid.NewGuid() : vm.DataId,
                Name = vm.Name ?? string.Empty,
                State = vm.State,
                Description = vm.Description ?? string.Empty,
                Code = vm.Code ?? string.Empty,
                ModuleId = vm.ModuleId,
                ModuleCode = vm.ModuleCode,
                Sort = vm.Sort,
                Visible = vm.Visible,
                Url = vm.Url ?? string.Empty
            };
        }
    }
}
