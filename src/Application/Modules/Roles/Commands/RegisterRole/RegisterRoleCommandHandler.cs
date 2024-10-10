using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Repositories.Roles;
using Application.Modules.Roles.Commands.RegisterRole;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;
using Application.Core.Responses;
using Application.Modules.Permissions.Commands.RegisterPermission;

internal class RegisterRoleCommandHandler
  : ICommandHandler<RegisterRoleCommand, CreateResponse<RegisterRoleResponse>>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IRoleRepository _roleRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterRoleCommandHandler(
      IPermissionRepository permissionRepository,
      IRoleRepository roleRepository,
      IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _roleRepository = roleRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CreateResponse<RegisterRoleResponse>>> Handle(
    RegisterRoleCommand roleCommand,
    CancellationToken cancellationToken)
  {
    List<Permission> permissions = [];
    foreach (var permissionId in roleCommand.Permissions)
    {
      var permission = await _permissionRepository
        .GetByIdAsync(new PermissionId(permissionId), cancellationToken);

      if (permission is null)
        return Result.Failure<CreateResponse<RegisterRoleResponse>>(PermissionErrors.ManyNotFound);

      permissions.Add(permission);
    }

    var newRole = Role.Create(
      new Name(roleCommand.Name),
      permissions
    );

    _roleRepository.Add(newRole);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    if (newRole.Id is null)
      return Result.Failure<CreateResponse<RegisterRoleResponse>>(RoleErrors.ErrorCreating);

    var result = CreateResponse<RegisterRoleResponse>.Success(
      RegisterRoleResponse.FromEntity(newRole),
      newRole.Id.Value
    );

    return Result.Success(result);
  }
}
