using Domain.Abstractions;
using Domain.Shared;

namespace Domain.Entities.Permissions;

public static class PermissionErrors
{
  public static readonly Error ManyNotFound = new(
    "MANY_NOT_FOUND",
    "One or more permissions not found."
  );

  public static readonly Error NotFound = EntityErrors.NotFound.Format(nameof(Permission));
  public static readonly Error ErrorCreating = EntityErrors.ErrorCreating.Format(nameof(Permission));
  public static readonly Error SuccessCreating = EntityErrors.SuccessCreating.Format(nameof(Permission));
  public static readonly Error NotDeleted = EntityErrors.NotDeleted.Format(nameof(Permission));
}
