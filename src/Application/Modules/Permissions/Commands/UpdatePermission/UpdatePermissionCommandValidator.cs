namespace Application.Modules.Permissions.Commands.UpdatePermission;

using Application.Configurations.Permissions;
using Application.Modules.Permissions.Validators;
using Application.Validations;
using FluentValidation;

public sealed class UpdatePermissionCommandValidator
  : BasePermissionCommandValidator<UpdatePermissionCommand>
{
  public UpdatePermissionCommandValidator()
  {
    RuleFor(command => command.Id.ToString())
      .Required(NameFields.Id)
      .BeANumber(NameFields.Id);

    RuleFor(command => command.IsActive)
      .BeABoolean(NameFields.IsActive)
      .When(command => command.IsActive.HasValue);
  }

  protected override bool ShouldValidateName(UpdatePermissionCommand command) =>
    !string.IsNullOrWhiteSpace(command.Name);

  protected override bool ShouldValidateType(UpdatePermissionCommand command) =>
    !string.IsNullOrWhiteSpace(command.Type);
}
