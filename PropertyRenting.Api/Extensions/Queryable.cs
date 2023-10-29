using System.Linq.Expressions;

namespace PropertyRenting.Api.Extensions;

public static class Queryable
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> filter)
    {
        if (condition)
            query = query.Where(filter);
        return query;
    }
}
