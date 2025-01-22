using System.Net;
using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;

namespace Application.Modules.Permissions.Commands.DeletePermission;

internal class DeletePermissionCommandHandler(
  IPermissionRepository permissionRepository,
  IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePermissionCommand, DeletePermissionResponse>
{
  private readonly IPermissionRepository _permissionRepository = permissionRepository;
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Response<DeletePermissionResponse>> Handle(
    DeletePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var permission = await _permissionRepository.GetByIdAsync(
      new PermissionId(request.Id),
      cancellationToken
    );

    if (permission is null)
      return Response.Failure<DeletePermissionResponse>(
        error: PermissionErrors.NotFound,
        statusCode: (int)HttpStatusCode.NotFound
      );

    permission.SoftDelete();

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Response.Success(
      DeletePermissionResponse.FromEntity(permission),
      PermissionMessages.Deleted.Message,
      (int)HttpStatusCode.OK
    );
  }
}
