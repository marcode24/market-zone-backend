using Domain.Abstractions;

namespace Domain.Shared;

public static class EntityMessages
{
  public static readonly Success Restored = new(
    "RESTORED",
    "{0} was restored successfully."
  );

  public static readonly Success Created = new(
    "CREATED",
    "{0} was created successfully."
  );

  public static readonly Success Updated = new(
    "UPDATED",
    "{0} was updated successfully."
  );

  public static readonly Success Deleted = new(
    "DELETED",
    "{0} was deleted successfully."
  );
}
