namespace Application.Modules.Permissions.Interfaces;

public interface IPermissionCommand
{
  string Name { get; }
  string Type { get; }
}
