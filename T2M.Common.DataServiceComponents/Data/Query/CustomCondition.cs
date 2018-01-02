using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace T2M.Common.DataServiceComponents.Data.Query
{
    public abstract class CustomCondition<T>
    {
        public object Value { get; set; }
        public SqlOperation Operation { get; set; }
    }

    public class CustomConditionBase<T> : CustomCondition<T>
    {
        public Expression<Func<T, object>> Member { get; set; }
    }

    public class CustomConditionPlus<T> : CustomCondition<T>
    {
        public Expression<Func<T, object>>[] Member { get; set; }
    }
    public enum SqlOperation
    {
        Equals = 0,
        Greater = 1,
        Lesser = 2,
        GreaterOrEquals = 3,
        LesserOrEquals = 4,
        NotEquals = 5,
        Like = 6,
        StartWith = 7,
        EndWith = 8,
        /// <summary>
        /// 时间区间
        /// </summary>
        DateRange = 9,
    }
}
