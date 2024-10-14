namespace Application.Modules.Roles.Commands.CreateRole;

using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Repositories.Roles;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;
using Application.Core.Responses;
using Application.Abstractions.Messaging;

internal class RegisterRoleCommandHandler
  : ICommandHandler<CreateRoleCommand, CreateResponse<CreateRoleResponse>>
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

  public async Task<Result<CreateResponse<CreateRoleResponse>>> Handle(
    CreateRoleCommand roleCommand,
    CancellationToken cancellationToken)
  {
    List<Permission> permissions = [];
    foreach (var permissionId in roleCommand.Permissions)
    {
      var permission = await _permissionRepository
        .GetByIdAsync(new PermissionId(permissionId), cancellationToken);

      if (permission is null)
        return Result.Failure<CreateResponse<CreateRoleResponse>>(PermissionErrors.ManyNotFound);

      permissions.Add(permission);
    }

    var newRole = Role.Create(
      new Name(roleCommand.Name),
      permissions
    );

    _roleRepository.Add(newRole);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    if (newRole.Id is null)
      return Result.Failure<CreateResponse<CreateRoleResponse>>(RoleErrors.ErrorCreating);

    var result = CreateResponse<CreateRoleResponse>.Success(
      CreateRoleResponse.FromEntity(newRole),
      newRole.Id.Value
    );

    return Result.Success(result);
  }
}
