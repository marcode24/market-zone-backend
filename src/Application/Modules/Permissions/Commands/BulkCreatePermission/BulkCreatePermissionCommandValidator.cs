namespace Application.Modules.Permissions.Commands.BulkCreatePermission;

using Application.Validations;
using FluentValidation;

public sealed class BulkCreatePermissionCommandValidator : AbstractValidator<BulkCreatePermissionCommand>
{
  public BulkCreatePermissionCommandValidator()
  {
    RuleFor(command => command.ExcelFile)
        .Cascade(CascadeMode.Stop)
        .FileRequired()
        .BeExcelFile()
        .BeExcelFileTemplate();
  }

}
