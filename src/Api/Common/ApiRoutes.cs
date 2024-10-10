namespace Api.Common;

public static class ApiRoutes
{
  private const string BaseRoute = "api/v{version:apiVersion}";
  public const string UsersRoute = $"{BaseRoute}/users";
  public const string RolesRoute = $"{BaseRoute}/roles";
  public const string PermissionsRoute = $"{BaseRoute}/permissions";
}
