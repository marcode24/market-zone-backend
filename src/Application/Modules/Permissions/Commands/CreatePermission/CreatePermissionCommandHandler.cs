using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Shared.ValueObjects;

namespace Application.Modules.Permissions.Commands.CreatePermission;

internal class CreatePermissionCommandHandler
  : ICommandHandler<CreatePermissionCommand, CreateResponse<CreatePermissionResponse>>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CreateResponse<CreatePermissionResponse>>> Handle(
    CreatePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var newPermission = Permission.Create(
      new Name(request.Name),
      new TypePermission(request.Type)
    );

    _permissionRepository.Add(newPermission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    if (newPermission.Id is null)
      return Result.Failure<CreateResponse<CreatePermissionResponse>>(PermissionErrors.ErrorCreating);

    var result = CreateResponse<CreatePermissionResponse>.Success(
      CreatePermissionResponse.FromEntity(newPermission),
      newPermission.Id.Value
    );

    return Result.Success(result);
  }
}
