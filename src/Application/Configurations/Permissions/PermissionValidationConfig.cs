namespace Application.Configurations.Permissions;

using Application.Configurations.Common;
using Domain.Entities.Permissions;

public static class PermissionValidationConfig
{
  static PermissionValidationConfig()
  {
    EntityConfigurations.AddEntityConfig(nameof(Permission), new Dictionary<string, EntityConfigurations.PropertyConfig>
    {
      {
        nameof(Permission.Name),
        new EntityConfigurations.PropertyConfig(maxLength: 50, minLength: 3)
      },
      {
        nameof(Permission.Type),
        new EntityConfigurations.PropertyConfig(maxLength: 50, minLength: 3)
      }
    });
  }

  public static int NameMaxLength => PropertiesGetters.GetMaxLength<Permission>(p => p.Name!);
  public static int NameMinLength => PropertiesGetters.GetMinLength<Permission>(p => p.Name!);
  public static int TypeMaxLength => PropertiesGetters.GetMaxLength<Permission>(p => p.Type!);
  public static int TypeMinLength => PropertiesGetters.GetMinLength<Permission>(p => p.Type!);
}
