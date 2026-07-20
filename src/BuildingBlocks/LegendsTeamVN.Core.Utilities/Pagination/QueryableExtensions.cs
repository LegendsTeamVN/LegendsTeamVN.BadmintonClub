using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace LegendsTeamVN.Core.Utilities.Pagination;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(items, count, pageNumber, pageSize);
    }

    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> query, 
        string? sortColumn, 
        SortDirection sortDirection, 
        string defaultSortColumn = "Id")
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
        {
            sortColumn = defaultSortColumn;
        }

        var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(p => p.Name.Equals(sortColumn, StringComparison.OrdinalIgnoreCase));

        if (propertyInfo == null)
        {
            propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name.Equals(defaultSortColumn, StringComparison.OrdinalIgnoreCase));

            if (propertyInfo == null) return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var methodName = sortDirection == SortDirection.Ascending ? "OrderBy" : "OrderByDescending";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), propertyInfo.PropertyType },
            query.Expression,
            Expression.Quote(orderByExpression)
        );

        return query.Provider.CreateQuery<T>(resultExpression);
    }

    public static IQueryable<T> ApplyBaseFilter<T>(
        this IQueryable<T> query, 
        SearchFilter filter, 
        params string[] searchColumns)
    {
        var filteredQuery = query.ApplySearch(filter.SearchTerm, searchColumns);
        return filteredQuery.ApplySort(filter.SortColumn, filter.SortDirection);
    }

    public static IQueryable<T> ApplySearch<T>(
        this IQueryable<T> query, 
        string? searchTerm, 
        params string[] searchColumns)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) || searchColumns.Length == 0)
        {
            return query;
        }

        searchTerm = searchTerm.ToLower();
        var parameter = Expression.Parameter(typeof(T), "x");
        var searchTermConstant = Expression.Constant(searchTerm);
        
        var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        Expression? combinedExpression = null;

        foreach (var column in searchColumns)
        {
            var property = Expression.Property(parameter, column);
            var toLower = Expression.Call(property, toLowerMethod!);
            var contains = Expression.Call(toLower, containsMethod!, searchTermConstant);

            combinedExpression = combinedExpression == null 
                ? contains 
                : Expression.OrElse(combinedExpression, contains);
        }

        if (combinedExpression != null)
        {
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            return query.Where(lambda);
        }

        return query;
    }
}
