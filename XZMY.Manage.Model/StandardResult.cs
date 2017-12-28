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
    public class StandardResult : IStandardResult
    {
        #region Implementation of ICustomResult

        public bool Success { get; set; }

        public string Message { get; set; }

        public int Code { get; set; }

        public void Succeed()
        {
            Success = true;
        }

        public void Fail()
        {
            Success = false;
        }

        public void Succeed(string message)
        {
            Success = true;
            Message = message;
        }

        public void Fail(string message)
        {
            Success = false;
            Message = message;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StandardResult<T> : StandardResult, IStandardResult<T>
    {
        #region Implementation of ICustomResult<T>

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; set; }

        #endregion
    }
}
