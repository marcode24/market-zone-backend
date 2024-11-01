using Domain.Abstractions;
using Domain.Shared;

namespace Domain.Entities.Permissions;

public static class PermissionMessages
{
  public static readonly Success Created = EntityMessages.Created.Format(nameof(Permission));
  public static readonly Success Deleted = EntityMessages.Deleted.Format(nameof(Permission));
  public static readonly Success Updated = EntityMessages.Updated.Format(nameof(Permission));
  public static readonly Success Restored = EntityMessages.Restored.Format(nameof(Permission));
  public static readonly Success BulkCreated = EntityMessages.BulkCreated.Format($"{nameof(Permission)}s");
}
