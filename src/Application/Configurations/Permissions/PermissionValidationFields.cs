namespace Application.Configurations.Permissions;

using Domain.Entities.Permissions;
using Domain.Utilities;

internal static class NameFields
{
  public static string Id { get; } = ValidationHelpers
    .GetDisplayName<Permission>(r => r.Id!);
  public static string Name { get; } = ValidationHelpers
    .GetDisplayName<Permission>(r => r.Name!);

  public static string Type { get; } = ValidationHelpers
    .GetDisplayName<Permission>(r => r.Type!);

  public static string IsActive { get; } = ValidationHelpers
    .GetDisplayName<Permission>(r => r.IsActive);
}
