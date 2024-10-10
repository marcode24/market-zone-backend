namespace Application.Configurations.Common;

using System.Linq.Expressions;

public static class PropertiesGetters
{
  public static int GetMaxLength<T>(Expression<Func<T, object>> propertySelector) =>
      EntityConfigurations.GetPropertyConfig(propertySelector).MaxLength;

  public static int GetMinLength<T>(Expression<Func<T, object>> propertySelector) =>
      EntityConfigurations.GetPropertyConfig(propertySelector).MinLength;
}
