using System;
using System.Linq;
using System.Linq.Expressions;

namespace Projects.Common
{
    public static class QueriableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string itemName)
        {
            var expression = source.Expression;

            var resultQuery = source;

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, itemName);

                expression = Expression.Call(typeof(Queryable), "OrderBy"
                   , new Type[] { source.ElementType, selector.Type }, expression,
                   Expression.Quote(Expression.Lambda(selector, parameter)));

                resultQuery = source.Provider.CreateQuery<T>(expression);
            }

            return resultQuery;
        }
    }
}
