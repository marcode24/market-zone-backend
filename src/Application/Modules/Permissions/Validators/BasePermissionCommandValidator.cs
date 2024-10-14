namespace Application.Modules.Permissions.Validators;

using Application.Configurations.Permissions;
using Application.Modules.Permissions.Interfaces;
using Application.Validations;
using Domain.Shared.Extensions;
using FluentValidation;

public abstract class BasePermissionCommandValidator<T> : AbstractValidator<T>
    where T : IPermissionCommand
{
  protected BasePermissionCommandValidator()
  {
    RuleFor(command => command.Name)
      .Required(NameFields.Name)
      .MinLength(PermissionValidationConfig.NameMinLength, NameFields.Name)
      .MaxLength(PermissionValidationConfig.NameMaxLength, NameFields.Name)
      .When(ShouldValidateName);

    RuleFor(command => command.Type)
      .Required(NameFields.Type)
      .MinLength(PermissionValidationConfig.TypeMinLength, NameFields.Type)
      .MaxLength(PermissionValidationConfig.TypeMaxLength, NameFields.Type)
      .When(ShouldValidateType);
  }

  protected virtual bool ShouldValidateName(T command) => true;
  protected virtual bool ShouldValidateType(T command) => true;
}

