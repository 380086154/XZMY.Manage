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
    public interface IStandardResult
    {
        bool Success { get; set; }

        string Message { get; set; }

        int Code { get; set; }

        void Succeed();

        void Fail();

        void Succeed(string message);

        void Fail(string message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStandardResult<T> : IStandardResult
    {
        T Value { get; set; }
    }
}
