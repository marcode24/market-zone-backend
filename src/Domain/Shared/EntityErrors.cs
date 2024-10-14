using Domain.Abstractions;

namespace Domain.Shared;

public static class EntityErrors
{
  public static readonly Error NotFound = new(
      "NOT_FOUND",
      "{0} not found."
    );
  public static readonly Error ErrorCreating = new(
    "ERROR_CREATING",
    "Error creating {0}."
  );
  public static readonly Error SuccessCreating = new(
    "SUCCESS_CREATING",
    "{0} created successfully."
  );

  public static readonly Error NotDeleted = new(
    "NOT_DELETED",
    "{0} is not deleted."
  );
}
