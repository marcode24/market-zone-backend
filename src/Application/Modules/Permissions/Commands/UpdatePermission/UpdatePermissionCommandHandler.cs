using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Shared.ValueObjects;

namespace Application.Modules.Permissions.Commands.UpdatePermission;

internal class UpdatePermissionCommandHandler
  : ICommandHandler<UpdatePermissionCommand, UpdateResponse<UpdatePermissionResponse>>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<UpdateResponse<UpdatePermissionResponse>>> Handle(
    UpdatePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var permission = await _permissionRepository.GetByIdAsync(
      new PermissionId(request.Id),
      cancellationToken
    );

    if (permission is null)
      return Result.Failure<UpdateResponse<UpdatePermissionResponse>>(PermissionErrors.NotFound);

    permission.Update(
      new Name(request.Name),
      new TypePermission(request.Type),
      request.IsActive
    );

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var result = UpdateResponse<UpdatePermissionResponse>.Success(
      UpdatePermissionResponse.FromEntity(permission)
    );

    return Result.Success(result);
  }
}
