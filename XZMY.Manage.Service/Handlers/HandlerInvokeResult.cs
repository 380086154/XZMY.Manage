using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Handlers
{

    [Serializable]
    [DataContract]
    public class HandlerInvokeResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [DataMember]
        public bool Success { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Message { get; set; }

        protected object _output;
        [IgnoreDataMember]
        public virtual object DynamicOutput { get { return _output; } set { _output = value; } }
        [DataMember]
        public Exception Exception { get; set; }


        public static HandlerInvokeResult NULL_VIEWMODEL = new HandlerInvokeResult { Code = (int)HandlerInvokeResultCode.参数异常, Message = "Viewmodel is null!" };
        public static HandlerInvokeResult SUCCESS_VIEWMODEL = new HandlerInvokeResult { Success = true, Code = (int)HandlerInvokeResultCode.执行成功, Message = "Success!" };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class HandlerInvokeResult<T> : HandlerInvokeResult
    {
        [DataMember]
        public T Output
        {
            get
            {
                if (_output == null) return default(T);
                if (!(_output is T)) return default(T); return (T)_output;
            }
            set { _output = value; }
        }
        public static HandlerInvokeResult<T> NULL_VIEWMODEL = new HandlerInvokeResult<T> { Code = (int)HandlerInvokeResultCode.参数异常, Message = "Viewmodel is null!" };
        public static HandlerInvokeResult<T> SUCCESS_VIEWMODEL = new HandlerInvokeResult<T> { Success = true, Code = (int)HandlerInvokeResultCode.执行成功, Message = "Success!" };

    }
    [Serializable]
    [DataContract]
    public class ApiHandlerInvokeResult<T> : HandlerInvokeResult
    {
        [DataMember]
        public T Output
        {
            get
            {
                if (_output == null) return default(T);
                if (!(_output is T)) return default(T); return (T)_output;
            }
            set { _output = value; }
        }
        public static ApiHandlerInvokeResult<T> NULL_VIEWMODEL = new ApiHandlerInvokeResult<T> { Code = (int)HandlerInvokeResultCode.参数异常, Message = "Viewmodel is null!" };
        public static ApiHandlerInvokeResult<T> SUCCESS_VIEWMODEL = new ApiHandlerInvokeResult<T> { Success = true, Code = (int)HandlerInvokeResultCode.执行成功, Message = "Success!" };

    }

    public enum HandlerInvokeResultCode
    {
        执行成功 = 0,
        参数异常 = 1,
        服务器异常 = 2
    }
}
