using Domain.Abstractions;
using Domain.Shared;

namespace Domain.Entities.Roles;

public static class RoleErrors
{
  public static readonly Error NotFound = EntityErrors.NotFound.Format(nameof(Role));
  public static readonly Error ErrorCreating = EntityErrors.ErrorCreating.Format(nameof(Role));
  public static readonly Error SuccessCreating = EntityErrors.SuccessCreating.Format(nameof(Role));
}
