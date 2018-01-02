using System;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 远程调用回滚
    /// </summary>
    public interface IRemoteCallRollbackQuery : IDisposable
    {
        /// <summary>
        /// 是否存在错误
        /// </summary>
        Boolean HasError { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        Exception Exception { get; set; }
    }
}
