using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Shared.ValueObjects;

namespace Application.Modules.Permissions.Commands.RegisterPermission;

internal class RegisterPermissionCommandHandler
  : ICommandHandler<RegisterPermissionCommand, CreateResponse<RegisterPermissionResponse>>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterPermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CreateResponse<RegisterPermissionResponse>>> Handle(
    RegisterPermissionCommand request,
    CancellationToken cancellationToken)
  {
    var newPermission = Permission.Create(
      new Name(request.Name),
      new TypePermission(request.Type)
    );

    _permissionRepository.Add(newPermission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    if (newPermission.Id is null)
      return Result.Failure<CreateResponse<RegisterPermissionResponse>>(PermissionErrors.ErrorCreating);

    var result = CreateResponse<RegisterPermissionResponse>.Success(
      RegisterPermissionResponse.FromEntity(newPermission),
      newPermission.Id.Value
    );

    return Result.Success(result);
  }
}
