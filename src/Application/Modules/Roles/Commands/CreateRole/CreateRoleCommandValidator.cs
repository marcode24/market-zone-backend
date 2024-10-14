using Application.Configurations.Roles;
using Application.Validations;
using Domain.Utilities;
using FluentValidation;

namespace Application.Modules.Roles.Commands.CreateRole;

internal sealed class CreateRoleCommandValidator
  : AbstractValidator<CreateRoleCommand>
{
  public CreateRoleCommandValidator()
  {
    RuleFor(user => user.Name)
      .Required(NameFields.Name)
      .MinLength(RoleConfigurations.NameMinLength, NameFields.Name)
      .MaxLength(RoleConfigurations.NameMaxLength, NameFields.Name);
  }
}

internal static class NameFields
{
  public static string Name { get; } = ValidationHelpers
    .GetDisplayName<CreateRoleCommand>(r => r.Name);
}
