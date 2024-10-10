using Application.Configurations.Common;
using Domain.Entities.Roles;

namespace Application.Configurations.Roles;

public static class RoleConfigurations
{
  static RoleConfigurations()
  {
    EntityConfigurations.AddEntityConfig(nameof(Role), new Dictionary<string, EntityConfigurations.PropertyConfig>
    {
      {
        nameof(Role.Name),
        new EntityConfigurations.PropertyConfig(maxLength: 50, minLength: 3)
      },
    });
  }

  public static int NameMaxLength => PropertiesGetters.GetMaxLength<Role>(p => p.Name!);
  public static int NameMinLength => PropertiesGetters.GetMinLength<Role>(p => p.Name!);
}
