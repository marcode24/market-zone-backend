using System.Linq.Expressions;
using System.Reflection;

namespace Application.Extensions;

public static class IQueryableExtensions
{
  public static IQueryable<TEntity> OrderByProperty<TEntity>(this IQueryable<TEntity> source, string propertyName) =>
    ApplyOrder(source, propertyName, "OrderBy");

  public static IQueryable<TEntity> OrderByDescendingProperty<TEntity>(this IQueryable<TEntity> source, string propertyName) =>
    ApplyOrder(source, propertyName, "OrderByDescending");

  private static IQueryable<TEntity> ApplyOrder<TEntity>(IQueryable<TEntity> source, string propertyName, string methodName)
  {
    var entityType = typeof(TEntity);

    var property = entityType.GetProperty(propertyName,
      BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
      ?? throw new ArgumentException($"Property {propertyName} not found in {entityType}");

    var parameter = Expression.Parameter(entityType, "x");
    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
    var orderByExp = Expression.Lambda(propertyAccess, parameter);

    var resultExp = Expression.Call(
      typeof(Queryable),
      methodName,
      [entityType, property.PropertyType],
      source.Expression,
      Expression.Quote(orderByExp));

    return source.Provider.CreateQuery<TEntity>(resultExp);
  }
}
