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
      .Required(NameFields.Name)
      .MinLength(3, NameFields.Name)
      .MaxLength(50, NameFields.Name);
  }
}

internal static class NameFields
{
  public static string Name { get; } = ValidationHelpers.GetDisplayName<RegisterUserCommand>(x => x.Name);
}
