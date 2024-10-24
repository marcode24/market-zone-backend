using System.Linq.Expressions;
using Application.Abstractions.Services;
using LinqKit;
using System.Collections.Generic;

namespace Application.Services.Filter
{
  public class FilterService<TEntity> : IFilterService<TEntity>
  {
    public ExpressionStarter<TEntity> BuildFilterPredicate(
        List<(string PropertyName, string SearchValue)> searchCriteria)
    {
      var predicate = PredicateBuilder.New<TEntity>(true);

      if (searchCriteria == null || !searchCriteria.Any())
        return predicate;

      foreach (var criteria in searchCriteria)
      {
        var property = typeof(TEntity).GetProperty(criteria.PropertyName);
        if (property == null || !IsSearchable(property.PropertyType))
          continue;

        var normalizedSearch = criteria.SearchValue.ToLower();
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        Expression propertyAccess = Expression.Property(parameter, property);

        // Si la propiedad es un Value Object, accede a su propiedad de cadena directamente
        if (!PrimitiveHelper.PrimitiveTypes.Contains(property.PropertyType))
        {
          var encapsulatedProperty = property.PropertyType.GetProperty("Value");
          if (encapsulatedProperty != null && encapsulatedProperty.PropertyType == typeof(string))
          {
            // Acceder a la propiedad Value solo si es de tipo string
            propertyAccess = Expression.Property(Expression.Property(parameter, property.Name), encapsulatedProperty);
          }
        }

        // Construir la expresión para búsqueda de cadenas
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var containsExpression = Expression.Call(propertyAccess, containsMethod!, Expression.Constant(normalizedSearch));

        // Agregar la expresión al predicate
        predicate = predicate.Or(Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter));
      }

      return predicate;
    }

    private bool IsSearchable(Type propertyType)
    {
      return PrimitiveHelper.PrimitiveTypes.Contains(propertyType) ||
             !propertyType.IsValueType && propertyType != typeof(string);
    }
  }
}
