using System.Data;
using System.Data.SqlClient;

namespace T2M.Common.DataServiceComponents.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInvokeService<T>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        T Invoke();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IInvokeService<TViewModel, TResult> : IInvokeService<TResult>
    {
        TViewModel ViewModel { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IInvokeService<TModel, TViewModel, TResult> : IInvokeService<TViewModel, TResult>
    {
        TModel Model { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IInvokeTransactionService
    {
        void Invoke(IDbTransaction transaction);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IInvokeTransactionService<TResult>
    {
        TResult Invoke(IDbTransaction transaction);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IInvokeTransactionService<TViewModel, TResult> : IInvokeTransactionService<TResult>
    {
        TViewModel ViewModel { get; set; }
    }
     
    public interface IInvokeTransactionService<TModel, TViewModel, TResult> : IInvokeTransactionService<TViewModel, TResult>
    {
        TModel Model { get; set; }
    }
}

