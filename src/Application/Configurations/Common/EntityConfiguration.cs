namespace Application.Configurations.Common;

using System.Linq.Expressions;

public static class EntityConfigurations
{
  private static readonly Dictionary<string, Dictionary<string, PropertyConfig>> _configurations = new();

  public static PropertyConfig GetPropertyConfig<T, TProperty>(Expression<Func<T, TProperty>> expression)
  {
    var entityName = typeof(T).Name;
    var propertyName = GetPropertyName(expression);

    if (_configurations.TryGetValue(entityName, out var properties) &&
        properties.TryGetValue(propertyName, out var config))
      return config;

    throw new InvalidOperationException($"Property configuration not found for {entityName}.{propertyName}");
  }

  private static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
  {
    return expression.Body switch
    {
      MemberExpression memberExpression => memberExpression.Member.Name,
      UnaryExpression unaryExpression when unaryExpression.Operand is MemberExpression member => member.Member.Name,
      _ => throw new ArgumentException("Expression must target a property")
    };
  }

  public static void AddEntityConfig(string entityName, Dictionary<string, PropertyConfig> propertyConfigs)
  {
    _configurations[entityName] = propertyConfigs;
  }

  public class PropertyConfig(int maxLength, int minLength)
  {
    public int MaxLength { get; } = maxLength;
    public int MinLength { get; } = minLength;
  }
}
