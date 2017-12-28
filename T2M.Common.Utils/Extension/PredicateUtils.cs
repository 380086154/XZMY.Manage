using System.Linq;
using System.Linq.Expressions;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class PredicateUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// 按或合并条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        /// <summary>
        /// 按与合并条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        
        public static String GetExpressionMemberName<T>(this Expression<Func<T, Object>> m)
        {
            if (m.Body is MemberExpression)
                return ((MemberExpression)m.Body).Member.Name;

            if (m.Body is UnaryExpression)
                return ((MemberExpression)((UnaryExpression)m.Body).Operand).Member.Name;
            return null;
        }
        public static Type GetExpressionMemberType<T>(this Expression<Func<T, Object>> m)
        {
            if (m.Body is MemberExpression)
                return ((MemberExpression)m.Body).Type;

            if (m.Body is UnaryExpression)
                return ((MemberExpression)((UnaryExpression)m.Body).Operand).Type;
            return null;
        }
    }
}
