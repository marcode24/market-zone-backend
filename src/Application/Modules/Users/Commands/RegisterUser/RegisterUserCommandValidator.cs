using Application.Validations;
using Domain.Utilities;
using FluentValidation;

namespace Application.Modules.Users.Commands.RegisterUser;

internal sealed class RegisterUserCommandValidator
  : AbstractValidator<RegisterUserCommand>
{
  public RegisterUserCommandValidator()
  {
    RuleFor(user => user.Name)
      // .FieldRequired(NameFields.Name)
      // .MinLength(3, NameFields.Name)
      // .MaxLength(50, NameFields.Name);
      .NotEmpty()
      .WithMessage("Nombre es requerido");
  }
}

internal static class NameFields
{
  public static string Name { get; } = ValidationHelpers.GetDisplayName<RegisterUserCommand>(x => x.Name);
}
