using Domain.Shared.Extensions;
using FluentValidation;

namespace Application.Validations;

public static class CustomValidations
{
  public static IRuleBuilder<T, string> Required<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    string field)
  {
    return ruleBuilder
      .Must(value => !string.IsNullOrWhiteSpace(value.ToNormalize()))
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

  public static IRuleBuilderOptions<T, bool?> BeABoolean<T>(
    this IRuleBuilder<T, bool?> ruleBuilder,
    string campo)
  {
    return ruleBuilder
      .Must(value => value == null || (value == true || value == false))
      .WithMessage(ValidationsMessages.BeABoolean(campo));
  }

  public static IRuleBuilderOptions<T, string> BeANumber<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    string field)
  {
    return ruleBuilder
    .Must(value => string.IsNullOrWhiteSpace(value) || double.TryParse(value, out _))
      .WithMessage(ValidationsMessages.BeANumber(field));
  }
}
