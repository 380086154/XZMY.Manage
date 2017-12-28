using System;
using System.Collections.Generic;
using System.Linq;
using XZMY.Manage.Log.Models;
using XZMY.Manage.Model.DataModel.Courses;
using XZMY.Manage.Model.ViewModel.Courses;
using XZMY.Manage.Service.Utils;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Utils;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Service.Handlers.Course
{
    public class CourseDateRefreshHandler
    {
        public CourseDateRefreshHandler(IList<VmCourseDate> models)
        {
            Model = models;
        }

        public IList<VmCourseDate> Model;

        public HandlerInvokeResult Invoke()
        {
            if (Model == null || Model.Count == 0) return HandlerInvokeResult.NULL_VIEWMODEL;

            try
            {
                using (var wrapper = new SqlTransactionWrapper())
                {
                    try
                    {
                        var deleteSerivce = new BaseDeleteByForeignIdQuery<CourseDate>
                        {
                            ForeignId = Model.First().CourseId,
                            ForeignMember = m => m.CourseId
                        };
                        deleteSerivce.Execute(wrapper.Transaction);
                    }
                    catch
                    {
                        wrapper.HasError = true;
                        throw;
                    }

                    foreach (var answer in Model)
                    {
                        var datamodel = answer.CreateNewDataModel();
                        answer.DataId = datamodel.DataId;
                        datamodel.SetActorInfomation(LoggedUserManager.GetCurrentUserAccount().GetActorInfomationSynchronizer());

                        try
                        {
                            var createservice = new BaseCreateService<CourseDate>(datamodel);
                            createservice.Invoke(wrapper.Transaction);
                        }
                        catch
                        {
                            wrapper.HasError = true;
                            throw;
                        }
                    }
                    return HandlerInvokeResult.SUCCESS_VIEWMODEL;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CourseDateRefreshHandler", "编辑时异常", LogLevel.Error, ex);
                return new HandlerInvokeResult
                {
                    Code = (int)HandlerInvokeResultCode.服务器异常,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }
}

