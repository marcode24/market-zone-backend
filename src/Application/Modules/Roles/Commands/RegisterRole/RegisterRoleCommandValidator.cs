using Application.Configurations.Roles;
using Application.Validations;
using Domain.Utilities;
using FluentValidation;

namespace Application.Modules.Roles.Commands.RegisterRole;

internal sealed class RegisterRoleCommandValidator
  : AbstractValidator<RegisterRoleCommand>
{
  public RegisterRoleCommandValidator()
  {
    RuleFor(user => user.Name)
      .FieldRequired(NameFields.Name)
      .MinLength(RoleConfigurations.NameMinLength, NameFields.Name)
      .MaxLength(RoleConfigurations.NameMaxLength, NameFields.Name);
  }
}

internal static class NameFields
{
  public static string Name { get; } = ValidationHelpers
    .GetDisplayName<RegisterRoleCommand>(r => r.Name);
}
