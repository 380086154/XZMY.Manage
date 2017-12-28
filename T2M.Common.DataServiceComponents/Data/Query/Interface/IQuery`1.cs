namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<out T>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        T Execute();
    }
}