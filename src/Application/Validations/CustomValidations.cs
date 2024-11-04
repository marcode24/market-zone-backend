using Application.Validations.FileHandling;
using Domain.Shared.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NPOI.SS.Formula.Functions;

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

  public static IRuleBuilderOptions<T, IFormFile> FileRequired<T>(
    this IRuleBuilder<T, IFormFile> ruleBuilder)
  {
    return ruleBuilder
      .Must(file => file is not null && file.Length > 0)
      .WithMessage(ValidationsMessages.FileRequired);
  }

  public static IRuleBuilderOptions<T, IFormFile> BeExcelFile<T>(
    this IRuleBuilder<T, IFormFile> ruleBuilder)
  {
    return ruleBuilder
      .Must(ExcelFileValidator.IsExcelFile)
      .When(file => file is not null)
      .WithMessage(ValidationsMessages.BeExcelFile);
  }

  public static IRuleBuilderOptions<T, IFormFile> BeExcelFileTemplate<T>(
    this IRuleBuilder<T, IFormFile> ruleBuilder
  )
  {
    return ruleBuilder
      .Must(file => ExcelFileValidator.ValidatePermissionTemplate(file.OpenReadStream()))
      .When(file => file is not null)
      .WithMessage(ValidationsMessages.BeExcelFileTemplate);
  }
}
