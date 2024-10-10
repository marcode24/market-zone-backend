using FluentValidation;

namespace Application.Validations;

public static class CustomValidations
{
  public static IRuleBuilder<T, string> FieldRequired<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    string field)
  {
    return ruleBuilder
      .NotEmpty()
      .WithMessage(ValidationsMessages.FieldRequired(field));
  }

  public static IRuleBuilderOptions<T, string> MinLength<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    int length,
    string field)
  {
    return ruleBuilder
      .MinimumLength(length)
      .WithMessage(ValidationsMessages.MinLength(field, length));
  }

  public static IRuleBuilderOptions<T, string> MaxLength<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    int longitud,
    string campo)
  {
    return ruleBuilder
      .MaximumLength(longitud)
      .WithMessage(ValidationsMessages.MaxLength(campo, longitud));
  }

  public static IRuleBuilderOptions<T, string> EmailFormat<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    string campo)
  {
    return ruleBuilder
      .EmailAddress()
      .WithMessage(ValidationsMessages.EmailFormat(campo));
  }
}
