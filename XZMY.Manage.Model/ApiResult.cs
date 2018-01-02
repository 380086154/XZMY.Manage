using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiResult : StandardResult
    {
        /// <summary>
        /// Code定义规则请参加API文档
        /// </summary>
        public int Code { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult, IStandardResult<T>
    {
        #region Implementation of ICustomResult<T>

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; set; }

        #endregion
    }
}
