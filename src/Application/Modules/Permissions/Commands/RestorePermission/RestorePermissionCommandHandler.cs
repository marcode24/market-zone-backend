namespace Application.Modules.Permissions.Commands.RestorePermission;

using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;

internal class RestorePermissionCommandHandler
  : ICommandHandler<RestorePermissionCommand, CreateResponse<RestorePermissionResponse>>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RestorePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CreateResponse<RestorePermissionResponse>>> Handle(
    RestorePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var permission = await _permissionRepository.GetByIdIncludingDeletedAsync(
      new PermissionId(request.Id),
      cancellationToken);

    if (permission is null)
      return Result.Failure<CreateResponse<RestorePermissionResponse>>(PermissionErrors.NotFound);

    if (permission.IsDeleted is false)
      return Result.Success(
        CreateResponse<RestorePermissionResponse>.Success(
          RestorePermissionResponse.FromEntity(permission),
          PermissionErrors.NotDeleted.Message,
          permission.Id!.Value
        )
      );

    permission.RestoreSoftDelete();

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var result = CreateResponse<RestorePermissionResponse>.Success(
      RestorePermissionResponse.FromEntity(permission),
      PermissionMessages.Restored.Message
    );

    return Result.Success(result);
  }
}
