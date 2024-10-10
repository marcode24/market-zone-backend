using Application.Configurations.Permissions;
using Application.Validations;
using Domain.Entities.Permissions;
using Domain.Utilities;
using FluentValidation;

namespace Application.Modules.Permissions.Commands.RegisterPermission;

public sealed class RegisterPermissionCommandValidator
  : AbstractValidator<RegisterPermissionCommand>
{
  public RegisterPermissionCommandValidator()
  {
    RuleFor(user => user.Name)
    .FieldRequired(NameFields.Name)
    .MinLength(PermissionConfigurations.NameMinLength, NameFields.Name)
    .MaxLength(PermissionConfigurations.NameMaxLength, NameFields.Name);

    RuleFor(user => user.Type)
    .FieldRequired(NameFields.Type)
    .MinLength(PermissionConfigurations.TypeMinLength, NameFields.Type)
    .MaxLength(PermissionConfigurations.TypeMaxLength, NameFields.Type);
  }
}

internal static class NameFields
{
  public static string Name { get; } = ValidationHelpers
    .GetDisplayName<Permission>(r => r.Name!);

  public static string Type { get; } = ValidationHelpers
    .GetDisplayName<Permission>(r => r.Type!);
}
