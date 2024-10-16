using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;

namespace Application.Modules.Permissions.Commands.DeletePermission;

internal class DeletePermissionCommandHandler
  : ICommandHandler<DeletePermissionCommand, CreateResponse<DeletePermissionResponse>>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeletePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CreateResponse<DeletePermissionResponse>>> Handle(
    DeletePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var permission = await _permissionRepository.GetByIdAsync(
      new PermissionId(request.Id),
      cancellationToken
    );

    if (permission is null)
      return Result.Failure<CreateResponse<DeletePermissionResponse>>(PermissionErrors.NotFound);

    permission.SoftDelete();

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var result = CreateResponse<DeletePermissionResponse>.Success(
      DeletePermissionResponse.FromEntity(permission),
      PermissionMessages.Deleted.Message,
      permission.Id!.Value
    );

    return Result.Success(result);
  }
}
